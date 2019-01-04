using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{

    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ReceptionServiceCount")]
    public class ReceptionServiceCountController : Controller
    {
        private readonly IReceptionServiceCountService _receptionServiceCountService;

        public ReceptionServiceCountController(IReceptionServiceCountService receptionServiceCountService)
        {
            _receptionServiceCountService = receptionServiceCountService;

        }

        [HttpGet]
        [Route("GetCategoriesCount")]
        public async Task<JsonResult> GetCategoriesCount(ReceptionServiceCountDto dto)
        {
            var result = await _receptionServiceCountService.GetCategoriesCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        [HttpGet]
        [Route("GetServiceProvider")]
        public async Task<JsonResult> GetServiceProvider(ReceptionServiceCountDto dto)
        {
            var result = await _receptionServiceCountService.GetServiceProvider(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        [HttpGet]
        [Route("GetSourceCount")]
        public async Task<JsonResult> GetSourceCount(ReceptionServiceCountDto dto)
        {
            var result = await _receptionServiceCountService.GetSourceCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        [HttpGet]
        [Route("GetStationCount")]
        public async Task<JsonResult> GetStationCount(ReceptionServiceCountDto dto)
        {
            var result = await _receptionServiceCountService.GetStationCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        [HttpGet]
        [Route("GetSatisfactionCount")]
        public async Task<JsonResult> GetSatisfactionCount(ReceptionServiceCountDto dto)
        {
            var result = await _receptionServiceCountService.GetSatisfactionCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        [HttpGet]
        [Route("GetHoursCount")]
        public async Task<JsonResult> GetHoursCount(ReceptionServiceCountDto dto)
        {
            var result = await _receptionServiceCountService.GetHoursCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        [HttpGet]
        [Route("GetMonthsCount")]
        public async Task<JsonResult> GetMonthsCount(ReceptionServiceCountDto dto)
        {
            var result = await _receptionServiceCountService.GetMonthsCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
    }
}