using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Domain.Entities;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/People")]
    public class PeopleController : Controller
    {
        private readonly IPeopleService _peopleService;
        private readonly IPeopleAndActivityService _peopleAndActivityService;
        public PeopleController(IPeopleService peopleService, IPeopleAndActivityService peopleAndActivityService)
        {
            _peopleService = peopleService;
            _peopleAndActivityService = peopleAndActivityService;
        }



        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        [Route("GetPeopleById")]
        [HttpGet]
        public async Task<JsonResult> GetPeopleById(Guid id)
        {
            var result = await _peopleService.GetAsync(id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 通过市民云Id得到活动详细信息
        /// </summary>
        [Route("GetActivity")]
        [HttpGet]
        public async Task<JsonResult> GetActivity(string username, bool IsComment)
        {
            //var query = _peopleService.GetActivity(name, IsComment);
            var model = _peopleService.TestAsync(username, IsComment);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", model);
        }

        /// <summary>
        /// 通过市民云Id得到服务信息
        /// type:当前状态 1：已完成   2：未完成
        /// isComment:是否评价  1：已评价   2：未评价
        /// </summary>
        [Route("GetServes")]
        [HttpGet]
        public async Task<JsonResult> GetServes(string username, int? isComment, int? PJ_RegistStatus)
        {

            var model = _peopleService.GetService(username, isComment, PJ_RegistStatus);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", model);
        }


        /// <summary>
        /// 通过ID号得到事项信息
        /// </summary>
        [Route("GetWork")]
        [HttpGet]
        public async Task<JsonResult> GetWork(Guid Id, string StatusCode)
        {

            var model = await _peopleService.GetWork(Id, StatusCode);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", model);
        }





    }
}