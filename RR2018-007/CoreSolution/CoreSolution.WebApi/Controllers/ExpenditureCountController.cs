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
    [Route("api/ExpenditureCount")]
    public class ExpenditureCountController : Controller
    {
        private readonly IExpenditureCountService _expenditureCountService;

        public ExpenditureCountController(IExpenditureCountService expenditureCountService)
        {
            _expenditureCountService = expenditureCountService;

        }


        [Route("GetExpenditureById")]
        [HttpGet]
        public async Task<JsonResult> GetExpenditureById(Guid id)
        {
            var result = await _expenditureCountService.GetAsync(id);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该事项不存在");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCategoriesCount")]
        public async Task<JsonResult> GetCategoriesCount(ExpenditureDto dto)
        {
            var result = await _expenditureCountService.GetCategoriesCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "不存在数据", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        [HttpGet]
        [Route("GetStationCount")]
        public async Task<JsonResult> GetStationCount(ExpenditureDto dto)
        {
            var result = await _expenditureCountService.GetStationCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "不存在数据", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 获取 当前年 每月的经费
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUseDateCount")]
        public async Task<JsonResult> GetUseDateCount(ExpenditureDto dto)
        {
            var result = await _expenditureCountService.GetUseDateCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "不存在数据", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

    }
}