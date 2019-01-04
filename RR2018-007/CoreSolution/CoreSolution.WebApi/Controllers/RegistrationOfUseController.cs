using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.RegistrationOfUse;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Controllers
{

    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/RegistrationOfUse")]
    public class RegistrationOfUseController : Controller
    {
        private readonly IRegistrationOfUseService _registrationOfUseService;
        private readonly IFixedAssetsService _fixedAssetsService;

        public RegistrationOfUseController(IRegistrationOfUseService registrationOfUseService, IFixedAssetsService fixedAssetsService)
        {
            _registrationOfUseService = registrationOfUseService;
            _fixedAssetsService = fixedAssetsService;

        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="inputModel">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(RegistrationOfUseDto inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.ReceivePeople))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "领用人不能为空");
            }

            var fixedAssets = await _fixedAssetsService.GetFixedAssetsById(inputModel.FixedAssetsId);
         

            if (_registrationOfUseService.Count(i => i.FixedAssetsId == inputModel.FixedAssetsId) > 0)//登记使用 有记录
            {
                switch (fixedAssets.CurrentState)
                {
                   
                    case FixedAssetsCurrentState.ShiYong://登记使用 记录 做修改

                        var registrationOfUse = await _registrationOfUseService.GetAsync(inputModel.Id);
                        registrationOfUse.ReceivePeople = inputModel.ReceivePeople;
                        registrationOfUse.ReceiveDate = inputModel.ReceiveDate;
                        registrationOfUse.PredictReturnDate = inputModel.PredictReturnDate;
                        registrationOfUse.CurrentState = inputModel.CurrentState;
                        registrationOfUse.ReturnDate = inputModel.ReturnDate;
                        registrationOfUse.Remark = inputModel.Remark;

                        var update = await _registrationOfUseService.UpdateAsync(registrationOfUse);
                        if (update != null)
                        {
                            //修改 固定资产  状态
                            if (update.CurrentState == UseCurrentState.WeiGuiHuan)
                            {
                                fixedAssets.CurrentState = FixedAssetsCurrentState.ShiYong;
                            }
                            else
                            {
                                fixedAssets.CurrentState = FixedAssetsCurrentState.KongXian;
                            }
                            await _fixedAssetsService.UpdateAsync(fixedAssets);
                            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功", "updateOk");
                        }
                        break;
                    case FixedAssetsCurrentState.WeiXiu:
                        return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "固定资产正在维修");
                    case FixedAssetsCurrentState.BaoFei:
                        return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "固定资产已报废");
                }

            }

            //登记表 为空 或者 状态 为空闲
            var id = await _registrationOfUseService.InsertAndGetIdAsync(inputModel);
            if (id != null && id != default(Guid))
            {
                //修改 固定资产  状态
                if (inputModel.CurrentState == UseCurrentState.WeiGuiHuan)
                {
                    fixedAssets.CurrentState = FixedAssetsCurrentState.ShiYong;
                }
                else
                {
                    fixedAssets.CurrentState = FixedAssetsCurrentState.KongXian;
                }

                await _fixedAssetsService.UpdateAsync(fixedAssets);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "添加或者修改失败");
        }




        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid Id)
        {
            var residentWorkdto = await _registrationOfUseService.GetAsync(Id);
            if (residentWorkdto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "固定资产不存在");
            }
            residentWorkdto.DeletionTime = DateTime.Now;
            await _registrationOfUseService.DeleteAsync(residentWorkdto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="inputModel"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(RegistrationOfUseDto inputModel)
        {
            if (inputModel.CurrentState == UseCurrentState.WeiGuiHuan)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "当前状态未变更");
            }

            var fixedAssets = await _fixedAssetsService.GetFixedAssetsById(inputModel.FixedAssetsId);
            var registrationOfUse = await _registrationOfUseService.GetAsync(inputModel.Id);
            if (registrationOfUse == null || fixedAssets == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "固定资产不存在");
            }
            registrationOfUse.CurrentState = inputModel.CurrentState;
            registrationOfUse.ReturnDate = inputModel.ReturnDate;
            registrationOfUse.Remark = inputModel.Remark;

            var result = await _registrationOfUseService.UpdateAsync(registrationOfUse);
            if (result != null)
            {
                //修改 固定资产 为使用 状态
                fixedAssets.CurrentState = FixedAssetsCurrentState.KongXian;
                await _fixedAssetsService.UpdateAsync(fixedAssets);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "修改失败");



        }




        /// <summary>
        /// 根据Id获取事项。200获取成功;404未找到
        /// </summary>
        /// <param name="id">事项Id</param>
        /// <returns></returns>
        [Route("GetRegistrationOfUseById")]
        [HttpGet]
        public async Task<JsonResult> GetRegistrationOfUseById(Guid id)
        {
            var result = await _registrationOfUseService.GetRegistrationOfUseById(id);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "固定资产不存在");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据Id获取事项。200获取成功;404未找到
        /// </summary>
        /// <param name="id">事项Id</param>
        /// <returns></returns>
        [Route("GetRegistrationOfUseByFixedAssetsId")]
        [HttpGet]
        public async Task<JsonResult> GetRegistrationOfUseByFixedAssetsId(Guid id)
        {
            var result = await _registrationOfUseService.GetRegistrationOfUseByFixedAssetsId(id);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "固定资产空闲状态");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 根据搜索条件查询办事
        /// </summary>
        /// <returns></returns>
        [Route("GetRegistrationOfUsePaged")]
        [HttpGet]
        public async Task<JsonResult> GetRegistrationOfUsePaged(Guid FixedAssetsId)
        {
            var result = await _registrationOfUseService.GetRegistrationOfUsePaged(FixedAssetsId);
            //格式化枚举数据
            if (result != null && result.Count > 0)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<RegistrationOfUseDto>()
                {
                    List = result
                });
            }
            else
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "没有找到记录");
            }

        }
    }
}