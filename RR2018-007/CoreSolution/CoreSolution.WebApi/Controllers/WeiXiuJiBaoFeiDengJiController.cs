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
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Controllers
{

    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/WeiXiuJiBaoFeiDengJi")]
    public class WeiXiuJiBaoFeiDengJiController : Controller
    {
        private readonly IWeiXiuJiBaoFeiDengJiService _weiXiuJiBaoFeiDengJiService;
        private readonly IFixedAssetsService _fixedAssetsService;

        public WeiXiuJiBaoFeiDengJiController(IWeiXiuJiBaoFeiDengJiService weiXiuJiBaoFeiDengJiService, IFixedAssetsService fixedAssetsService)
        {
            _weiXiuJiBaoFeiDengJiService = weiXiuJiBaoFeiDengJiService;
            _fixedAssetsService = fixedAssetsService;

        }


        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="inputModel">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(WeiXiuJiBaoFeiDengJiDto inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.RegisterPeople))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "登记人不能为空");
            }


            var fixedAssets = await _fixedAssetsService.GetFixedAssetsById(inputModel.FixedAssetsId);
            if (_weiXiuJiBaoFeiDengJiService.Count(i => i.FixedAssetsId == inputModel.FixedAssetsId )> 0)
            {
                switch (fixedAssets.CurrentState)
                {

                    case FixedAssetsCurrentState.ShiYong:
                        return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "固定资产正在使用");
                    case FixedAssetsCurrentState.WeiXiu:
                    case FixedAssetsCurrentState.BaoFei:
                        var dto = await _weiXiuJiBaoFeiDengJiService.GetAsync(inputModel.Id);
                        if (dto!=null)
                        {
                           
                       
                        dto.Category = inputModel.Category;
                        dto.HappenDate = inputModel.HappenDate;
                        dto.CurrentState = inputModel.CurrentState;
                        dto.FinishDate = inputModel.FinishDate;
                        dto.Remark = inputModel.Remark;

                        var update = await _weiXiuJiBaoFeiDengJiService.UpdateAsync(dto);
                        if (update != null)
                        {
                            //修改 固定资产  状态
                            if (update.CurrentState== WeiXiuCurrentState.YiBaoFei)
                            {
                                fixedAssets.CurrentState = FixedAssetsCurrentState.BaoFei;
                            }
                            else if(update.CurrentState == WeiXiuCurrentState.YiWanCheng)
                            {
                                fixedAssets.CurrentState = FixedAssetsCurrentState.KongXian;
                            }else if (update.CurrentState == WeiXiuCurrentState.ZaiWeiXiu)
                            {
                                fixedAssets.CurrentState = FixedAssetsCurrentState.WeiXiu;
                            }
                            await _fixedAssetsService.UpdateAsync(fixedAssets);
                            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功","updateOk");
                            }
                        }
                        break;
                }
            }


           // 维修及报废登记表 为空 或者 状态 为空闲

            var id = await _weiXiuJiBaoFeiDengJiService.InsertAndGetIdAsync(inputModel);
            if (id != null && id != default(Guid))
            {
                //修改 固定资产  状态
                if (inputModel.CurrentState == WeiXiuCurrentState.YiBaoFei)
                {
                    fixedAssets.CurrentState = FixedAssetsCurrentState.BaoFei;
                }
                else if (inputModel.CurrentState == WeiXiuCurrentState.YiWanCheng)
                {
                    fixedAssets.CurrentState = FixedAssetsCurrentState.KongXian;
                }
                else if (inputModel.CurrentState == WeiXiuCurrentState.ZaiWeiXiu)
                {
                    fixedAssets.CurrentState = FixedAssetsCurrentState.WeiXiu;
                }

                await _fixedAssetsService.UpdateAsync(fixedAssets);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "添加失败");


        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid Id)
        {
            var residentWorkdto = await _weiXiuJiBaoFeiDengJiService.GetAsync(Id);
            if (residentWorkdto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该事项不存在");
            }
            residentWorkdto.DeletionTime = DateTime.Now;
            await _weiXiuJiBaoFeiDengJiService.DeleteAsync(residentWorkdto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="input">输入数据</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(WeiXiuJiBaoFeiDengJiDto input)
        {
            var fixedAssets = await _fixedAssetsService.GetFixedAssetsById(input.FixedAssetsId);
            var weiXiuJiBaoFeiDengJi = await _weiXiuJiBaoFeiDengJiService.GetAsync(input.Id);
            if (weiXiuJiBaoFeiDengJi == null || fixedAssets == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "此固定资产 不存在");
            }
            weiXiuJiBaoFeiDengJi.CurrentState = input.CurrentState;
            weiXiuJiBaoFeiDengJi.FinishDate = input.FinishDate;
            weiXiuJiBaoFeiDengJi.Remark = input.Remark;

            var result = await _weiXiuJiBaoFeiDengJiService.UpdateAsync(weiXiuJiBaoFeiDengJi);
            if (result != null)
            {
                //修改 固定资产  状态
                if (weiXiuJiBaoFeiDengJi.CurrentState == WeiXiuCurrentState.YiWanCheng)
                {
                    fixedAssets.CurrentState = FixedAssetsCurrentState.KongXian;
                }
                else if (weiXiuJiBaoFeiDengJi.CurrentState == WeiXiuCurrentState.YiBaoFei)
                {
                    fixedAssets.CurrentState = FixedAssetsCurrentState.BaoFei;
                }

                await _fixedAssetsService.UpdateAsync(fixedAssets);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "修改失败");



        }



        /// <summary>
        /// 根据  维修及报废记录Id  获取 维修及报废记录
        /// </summary>
        /// <param name="id">维修及报废记录Id </param>
        /// <returns></returns>
        [Route("GetWeiXiuJiBaoFeiDengJiById")]
        [HttpGet]
        public async Task<JsonResult> GetWeiXiuJiBaoFeiDengJiById(Guid id)
        {
            var result = await _weiXiuJiBaoFeiDengJiService.GetWeiXiuJiBaoFeiDengJiById(id);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "此固定资产 不存在");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 根据  根据固定资产Id  获取 维修及报废记录
        /// </summary>
        /// <param name="id">根据固定资产Id </param>
        /// <returns></returns>
        [Route("GetWeiXiuJiBaoFeiDengJiByFixedAssetsId")]
        [HttpGet]
        public async Task<JsonResult> GetWeiXiuJiBaoFeiDengJiByFixedAssetsId(Guid id)
        {
            var result = await _weiXiuJiBaoFeiDengJiService.GetWeiXiuJiBaoFeiDengJiByFixedAssetsId(id);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "此固定资产 空闲状态");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }



        /// <summary>
        /// 根据固定资产Id 获取记录
        /// </summary>
        /// <param name="FixedAssetsId">固定资产Id</param>
        /// <returns></returns>
        [Route("GetWeiXiuJiBaoFeiDengJiPaged")]
        [HttpGet]
        public async Task<JsonResult> GetWeiXiuJiBaoFeiDengJiPaged(Guid FixedAssetsId)
        {
            var result = await _weiXiuJiBaoFeiDengJiService.GetWeiXiuJiBaoFeiDengJiPaged(FixedAssetsId);
            //格式化枚举数据
            if (result != null && result.Count > 0)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<WeiXiuJiBaoFeiDengJiDto>()
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