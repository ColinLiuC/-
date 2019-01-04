using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ActivityEvaluation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ActivityEvaluation")]
    public class ActivityEvaluationController : Controller
    {
        private readonly IActivityEvaluationService _activityEvaluationService;
        public ActivityEvaluationController(IActivityEvaluationService activityEvaluationService)
        {
            _activityEvaluationService = activityEvaluationService;         
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputActivityEvaluationModel inputActivityEvaluationModel)
        {          
            var activityEvaluationDto = Mapper.Map<ActivityEvaluationDto>(inputActivityEvaluationModel);
            var id = await _activityEvaluationService.InsertAndGetIdAsync(activityEvaluationDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }
        /// <summary>
        /// 获取某个活动下所有的评价信息
        /// </summary>
        /// <returns></returns>
        [Route("GetEvaluationList")]
        [HttpGet]
        public async Task<JsonResult> GetListData(Guid activityId)
        {
            var result = await _activityEvaluationService.GetAllListAsync(p=>p.ActivityId==activityId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityEvaluationModel> { Total = result.Count, List = Mapper.Map<IList<OutputActivityEvaluationModel>>(result) });
        }
    }
}