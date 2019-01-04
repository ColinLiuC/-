using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.WebResult;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Map")]
    public class MapController : Controller
    {


        private readonly IMapOverlaysService _mapOverlaysService;


        public MapController(IMapOverlaysService mapOverlaysService)
        {
            _mapOverlaysService = mapOverlaysService;
        }
        [HttpGet]
        [Route("MapCenterSet")]
        public JsonResult MapCenterSet(string street, string jobcenter, string juwei, string flg)
        {
            var result = _mapOverlaysService.GetStreetMaps( street,  jobcenter,  juwei,  flg);


            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);


        }

    }
}