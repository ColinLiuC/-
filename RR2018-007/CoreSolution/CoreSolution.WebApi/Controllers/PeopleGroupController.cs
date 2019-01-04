using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
   


    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/PeopleGroup")]
    public class PeopleGroupController : Controller
    {
        private readonly IPeopleGroupService _PeopleGroupService;

        public PeopleGroupController(IPeopleGroupService PeopleGroupService)
        {
            _PeopleGroupService = PeopleGroupService;

        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(PeopleGroupDto inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.GroupName))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "名称不能为空");
            }
            var id = await _PeopleGroupService.InsertAndGetIdAsync(inputModel);
            return id != null ? AjaxHelper.JsonResult(HttpStatusCode.OK, "成功") : AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "失败");

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
            var residentWorkdto = await _PeopleGroupService.GetAsync(Id);
            if (residentWorkdto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "不存在");
            }
            residentWorkdto.DeletionTime = DateTime.Now;
            await _PeopleGroupService.DeleteAsync(residentWorkdto);
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
        public async Task<JsonResult> Modify(PeopleGroupDto inputModel)
        {
            var residentWorkDto = await _PeopleGroupService.GetAsync(inputModel.Id);
            if (residentWorkDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "不存在");
            }
            
            residentWorkDto.Id = inputModel.Id;
            residentWorkDto.GroupName = inputModel.GroupName;
            residentWorkDto.GroupCateGory = inputModel.GroupCateGory;
            residentWorkDto.DutyPeople = inputModel.DutyPeople;
            residentWorkDto.DutyPeopleTelPhone = inputModel.DutyPeopleTelPhone;
            residentWorkDto.Remark = inputModel.Remark;
            residentWorkDto.WorkPersonIds = inputModel.WorkPersonIds;
             var result = await _PeopleGroupService.UpdateAsync(residentWorkDto);
            return result != null ? AjaxHelper.JsonResult(HttpStatusCode.OK, "成功") : AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "失败");
        }




        /// <summary>
        /// 根据Id获取事项。200获取成功;404未找到
        /// </summary>
        /// <param name="id">事项Id</param>
        /// <returns></returns>
        [Route("GetPeopleGroupById")]
        [HttpGet]
        public async Task<JsonResult> GetPeopleGroupById(Guid id)
        {
            var result = await _PeopleGroupService.GetPeopleGroupById(id);
            
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "不存在");
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
        [Route("getPeopleGroupPaged")]
        [HttpGet]
        public async Task<JsonResult> GetPeopleGroupPaged(PeopleGroupDto model, int pageIndex = 1, int pageSize = 10)
        {

            var result = await _PeopleGroupService.GetPeopleGroupPagedAsync(model, pageIndex, pageSize);

            //格式化枚举数据
            if (result != null && result.Item2.Count > 0)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<PeopleGroupDto>
                {
                    Total = result.Item1,
                    List = result.Item2
                });
            }
            else
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "失败");
            }

        }
    }
}