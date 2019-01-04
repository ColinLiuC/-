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
    [Route("api/WorkforceManagement")]
    public class WorkforceManagementController : ControllerBase
    {
        private readonly IWorkforceManagementService _workforceManagementService;

        public WorkforceManagementController(IWorkforceManagementService workforceManagementService)
        {
            _workforceManagementService = workforceManagementService;

        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(WorkforceManagementDto inputModel)
        {
            if (string.IsNullOrWhiteSpace(inputModel.RegisterPeople))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "固定资产名称不能为空");
            }
            var id = await _workforceManagementService.InsertAndGetIdAsync(inputModel);
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
            var residentWorkdto = await _workforceManagementService.GetAsync(Id);
            if (residentWorkdto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该事项不存在");
            }
            residentWorkdto.DeletionTime = DateTime.Now;
            await _workforceManagementService.DeleteAsync(residentWorkdto);
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
        public async Task<JsonResult> Modify(WorkforceManagementDto inputModel)
        {
            var residentWorkDto = await _workforceManagementService.GetAsync(inputModel.Id);
            if (residentWorkDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该事项不存在");
            }

            residentWorkDto.Id = inputModel.Id;
            residentWorkDto.BeginTime = inputModel.BeginTime;
            residentWorkDto.EndTime = inputModel.EndTime;
            residentWorkDto.PeopleGroupId = inputModel.PeopleGroupId;
            residentWorkDto.Remark = inputModel.Remark;
            residentWorkDto.RegisterPeople = inputModel.RegisterPeople;
            residentWorkDto.RegisterDate = inputModel.RegisterDate;
             var result = await _workforceManagementService.UpdateAsync(residentWorkDto);
            return result != null ? AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功") : AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "添加失败");
        }




        /// <summary>
        /// 根据Id获取事项。200获取成功;404未找到
        /// </summary>
        /// <param name="id">事项Id</param>
        /// <returns></returns>
        [Route("GetWorkforceManagementById")]
        [HttpGet]
        public async Task<JsonResult> GetWorkforceManagementById(Guid id)
        {
            var result = await _workforceManagementService.GetWorkforceManagementById(id);

            if (result == null)
            {
               
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "该分组 不存在");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="WorkforceYear"></param>
        /// <param name="WorkforceMonth"></param>
        /// <returns></returns>
        [Route("GetWeekByMonth")]
        [HttpGet]
        public JsonResult GetWeekByMonth(int WorkforceYear, int WorkforceMonth)
        {
            WorkforceYear = WorkforceMonth == 12 ? WorkforceYear+ 1 : WorkforceYear;
            WorkforceMonth = WorkforceMonth == 12 ? 1 : WorkforceMonth + 1;
            string format = WorkforceYear + "-" + WorkforceMonth + "-1";
            DateTime dateTime = Convert.ToDateTime(format).AddDays(-1);
            int dayiw = ((int)dateTime.DayOfWeek) == 0 ? 7 : (int)dateTime.DayOfWeek;//当前 星期
            double day = dateTime.AddDays(1 - dayiw).Day;//4
            int temp = Convert.ToInt32(Math.Round((day + 6) / 7, 0));
            var resultList = new List<dynamic>();
            string[] shuzi = { "一", "二", "三", "四", "五", "六", "七" };
            for (int i = 0; i < temp; i++)
            {
                resultList.Add(new { Key = i+1, Value = shuzi[i] });
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", resultList);
        }


        /// <summary>
        /// 根据搜索条件查询办事
        /// </summary>
        /// <param name="model">搜索条件model</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("getWorkforceManagementByWeek")]
        [HttpGet]
        public async Task<JsonResult> GetWorkforceManagementByWeek(WorkforceManagementDto model)
        {

            var result = await _workforceManagementService.GetWorkforceManagementByWeekAsync(model);

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", 
               new {  Title = result.Item1,
                List = result.Item2});
        }
    }
}