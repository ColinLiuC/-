using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Dto.MyModel;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ObjectStatistics")]
    public class ObjectStatisticsController : ControllerBase
    {
        private readonly IOldPeopleService _oldpeopleService;
        private readonly IDisabilityService _disabilityService;
        private readonly ISpecialCareService _specialCareService;
        private readonly IOtherPeopleService _otherPeopleService;

        public ObjectStatisticsController(IOldPeopleService oldPeopleService, IDisabilityService disabilityService, ISpecialCareService specialCareService, IOtherPeopleService otherPeopleService)
        {

            _oldpeopleService = oldPeopleService;
            _disabilityService = disabilityService;
            _specialCareService = specialCareService;
            _otherPeopleService = otherPeopleService;
        }

        /// <summary>
        /// 获取统计分析数据
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="poststationid">驿站id</param>
        /// <returns></returns>
        [Route("getStatisticsData")]
        [HttpGet]
        public JsonResult GetStatisticsData(Guid? streetid, Guid? poststationid)
        {
            var juweiids = _oldpeopleService.GetJuWeiIds(streetid, poststationid);
            var result1 = _oldpeopleService.StatisticsByType(juweiids);
            var result2 = _oldpeopleService.StatisticsByAge(juweiids);
            var result3 = _disabilityService.StatisticsByType(juweiids);
            var result4 = _disabilityService.StatisticsByAge(juweiids);
            var result5 = _specialCareService.StatisticsByType(juweiids);
            var result6 = _specialCareService.StatisticsByAge(juweiids);
            var result7 = _otherPeopleService.StatisticsByType(juweiids);
            var result8 = _otherPeopleService.StatisticsByAge(juweiids);
            var result9 = _oldpeopleService.GetObjectUser(juweiids);
            var data = new List<List<MyKeyAndValue>>() { result1.Result, result2.Result, result3.Result,result4.Result, result5, result6, result7, result8, result9 };
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", data);
        }

        [Route("getStatisticsDataByJuWei")]
        [HttpGet]
        public JsonResult GetStatisticsDataByJuWei(Guid? streetid, Guid? poststationid)
        {
            var juweiids = _oldpeopleService.GetJuWeiIds(streetid, poststationid);
            var data = _oldpeopleService.StatisticsByJuWeiAll(juweiids);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", data);
        }

    }
}