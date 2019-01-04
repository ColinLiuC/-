using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{

    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Expenditure")]
    public class ExpenditureController : Controller
    {
        private readonly IExpenditureService _expenditureService;

        public ExpenditureController(IExpenditureService expenditureService)
        {
            _expenditureService = expenditureService;

        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(ExpenditureDto model)
        {
            if (string.IsNullOrWhiteSpace(model.ExpenditureName))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "经费使用名称不能为空");
            }
            var id = await _expenditureService.InsertAndGetIdAsync(model);
            return id != null ? AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功") : AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "添加失败");

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
            var residentWorkdto = await _expenditureService.GetAsync(Id);
            if (residentWorkdto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该经费使用不存在");
            }
            residentWorkdto.DeletionTime = DateTime.Now;
            await _expenditureService.DeleteAsync(residentWorkdto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(ExpenditureDto model)
        {
            var dto = await _expenditureService.GetAsync(model.Id);
            if (dto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该经费使用不存在");
            }
            dto.StreetId = model.StreetId;
            dto.StationId = model.StationId;
            dto.ExpenditureName = model.ExpenditureName;
            dto.Category = model.Category;
            dto.UseMoney = model.UseMoney;
            dto.UseDate = model.UseDate;
            dto.DutyPeople = model.DutyPeople;
            dto.Purpose = model.Purpose;
            dto.Accessory = model.Accessory;
            dto.AccessoryUrl = model.AccessoryUrl;
            dto.Remark = model.Remark;
            dto.RegisterPeople = model.RegisterPeople;
            dto.RegisterDate = model.RegisterDate;

            var result = await _expenditureService.UpdateAsync(dto);
            return result != null ? AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功") : AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "添加失败");
        }


        /// <summary>
        /// 根据Id获取事项。200获取成功;404未找到
        /// </summary>
        /// <param name="id">事项Id</param>
        /// <returns></returns>
        [Route("GetExpenditureById")]
        [HttpGet]
        public async Task<JsonResult> GetExpenditureById(Guid id)
        {
            var result = await _expenditureService.GetExpenditureById(id);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该经费使用不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 根据搜索条件查询办事
        /// </summary>
        /// <param name="model">搜索条件model</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("getExpenditurePaged")]
        [HttpGet]
        public async Task<JsonResult> GetExpenditurePaged(ExpenditureDto model, int pageIndex = 1, int pageSize = 10)
        {

            var result = await _expenditureService.GetExpenditurePagedAsync(model, pageIndex, pageSize);
            //格式化枚举数据
            if (result.Item2 != null && result.Item2.Count > 0)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<ExpenditureDto>
                {
                    Total = result.Item1,
                    List = Mapper.Map<IList<ExpenditureDto>>(result.Item2)
                });
            }
            else
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "失败", new ListModel<ExpenditureDto>());
            }

        }
    }
}