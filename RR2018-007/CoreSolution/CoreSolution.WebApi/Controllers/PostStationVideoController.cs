using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.PostStationVideo;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/PostStationVideo")]
    public class PostStationVideoController : Controller
    {
        private readonly IPostStationVideoService _postStationService;

        public PostStationVideoController(IPostStationVideoService postStationService)
        {
            _postStationService = postStationService;
        }
        /// <summary>
        /// 根据街道Id获取该街道下所有的视频
        /// </summary>
        /// <param name="streetId"></param>
        /// <returns></returns>
        [Route("GetVideo")]
        [HttpGet]
        public async Task<JsonResult> GetVideoByStreetId(Guid streetId)
        {
            var result = await _postStationService.GetAllListAsync(i=>i.StreetId==streetId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputPsVideoModel> { Total = result.Count, List = Mapper.Map<IList<OutputPsVideoModel>>(result) });
        }
    }
}