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
    [Route("api/ResourcePlaceCount")]
    public class ResourcePlaceCountController : Controller
    {
        private readonly IResourcePlaceCountService _resourcePlaceCountService;

        public ResourcePlaceCountController(IResourcePlaceCountService resourcePlaceCountService)
        {
            _resourcePlaceCountService = resourcePlaceCountService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetBigCategoriesCount")]
        public async Task<JsonResult> GetBigCategoriesCount(ResourcePlaceDto dto)
        {
            var result = await _resourcePlaceCountService.GetBigCategoriesCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该资源不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        [HttpGet]
        [Route("GetJuweiCount")]
        public async Task<JsonResult> GetJuweiCount(ResourcePlaceDto dto)
        {
            var result = await _resourcePlaceCountService.GetJuweiCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该资源不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

       

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSubPublicCount")]
        public async Task<JsonResult> GetSubPublicCount(ResourcePlaceDto dto)
        {
            var result = await _resourcePlaceCountService.GetSubCategoriesCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该资源不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSubInternalCount")]
        public async Task<JsonResult> GetSubInternalCount(ResourcePlaceDto dto)
        {
            var result = await _resourcePlaceCountService.GetSubCategoriesCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该资源不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSubMarketCount")]
        public async Task<JsonResult> GetSubMarketCount(ResourcePlaceDto dto)
        {
            var result = await _resourcePlaceCountService.GetSubCategoriesCount(dto);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该资源不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ResourceTypes"></param>
        /// <param name="StreetId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllPoints")]
        public async Task<JsonResult> GetAllPoints(string ResourceTypes, Guid StreetId)
        {
            Guid[] ids = { };
            if (ResourceTypes.Length>0)
            {
               ids= ResourceTypes.Split(',',StringSplitOptions.RemoveEmptyEntries).ToList().ConvertAll<Guid>(i => Guid.Parse(i)).ToArray();
            }
           
            var result = await _resourcePlaceCountService.GetAllPoints(ids,StreetId);

            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "该资源不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

    }
}