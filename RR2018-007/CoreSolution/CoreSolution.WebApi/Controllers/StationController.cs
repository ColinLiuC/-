using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models.Station;
using CoreSolution.Tools.Extensions;
using CoreSolution.WebApi.Models;

using System.Diagnostics;
using CoreSolution.Domain.Enum;
using CoreSolution.WebApi.Interceptor;
using System.Linq.Expressions;
using CoreSolution.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Cors;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Station")]
    public class StationController : Controller
    {
        private readonly IStationService _stationService;
       
        public StationController(IStationService stationService)
        {
            _stationService = stationService;
        }

        /// <summary>
        /// 查询符合条件的数据(使用index2)
        /// </summary>
        /// <returns></returns>
        [Route("index")]
        [HttpPost]
        public async Task<JsonResult> Index(StationDto stationDto, int pageIndex = 1, int pageSize = 10)
        {
            var model = await _stationService.GetStationPagedAsync(stationDto, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", model);
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create( InputStationModel inputstationModel)
        {
            if (inputstationModel.StationName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "驿站名称不能为空");
            }
            if (inputstationModel.StationAddress.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "驿站地址不能为空");
            }
            if (inputstationModel.StationPeople.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系人不能为空");
            }
            if (inputstationModel.StationTell.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系电话不能为空");
            }
            if ((inputstationModel.StationTime).ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "服务时间不能为空");
            }
            if ((inputstationModel.StreetID).ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "街道ID为空");
            }
            if ((inputstationModel.StreetName).IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "街道名称为空");
            }

            string token = HttpContext.Request.Headers["token"];
            var stationDto = new StationDto
            {
                StationName = inputstationModel.StationName,
                StationAddress = inputstationModel.StationAddress,
                StationPeople = inputstationModel.StationPeople,
                StationTell = inputstationModel.StationTell,
                StationTime = inputstationModel.StationTime,
                StationInfo = inputstationModel.StationInfo,
                StationImg = inputstationModel.StationImg,
                StationSrc= inputstationModel.StationSrc,
                StreetID=Guid.Parse(inputstationModel.StreetID.ToString()),
                StreetName=inputstationModel.StreetName,
                StationType=inputstationModel.StationType,
                Lat=inputstationModel.Lat,
                Lng=inputstationModel.Lng,
                Sort=100
            };
            await _stationService.InsertAsync(stationDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }

        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetStationById")]
        [HttpGet]
        public async Task<JsonResult> GetStationById(Guid id)
        {
            var result = await _stationService.GetAsync(id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit( StationDto stationDto)
        {
            if (stationDto.StationName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "驿站名称不能为空");
            }
            if (stationDto.StationAddress.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "驿站地址不能为空");
            }
            if (stationDto.StationPeople.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系人不能为空");
            }
            if (stationDto.StationTell.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系电话不能为空");
            }
            if ((stationDto.StationTime).ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "服务时间不能为空");
            }
            if ((stationDto.StreetID).ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "街道ID为空");
            }
            if ((stationDto.StreetName).IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "街道名称为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var result = await _stationService.GetAsync(stationDto.Id);

            result.StationName = stationDto.StationName;
            result.StationAddress = stationDto.StationAddress;
            result.StationPeople = stationDto.StationPeople;
            result.StationTell = stationDto.StationTell;
            result.StationTime = stationDto.StationTime;
            result.StationInfo = stationDto.StationInfo;
            result.StationImg = stationDto.StationImg;
            result.StationSrc = stationDto.StationSrc;
            result.Id = stationDto.Id;
            result.StreetID = stationDto.StreetID;
            result.StreetName = stationDto.StreetName;
            result.StationType = stationDto.StationType;
            result.Sort = stationDto.Sort;
            result.Lat = stationDto.Lat;
            result.Lng = stationDto.Lng;
            await _stationService.UpdateAsync(result);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }


        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete( Guid id)
        {
            var info = await _stationService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该用户不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //info.IsDeleted = true;
            await _stationService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 查询符合条件的数据2
        /// </summary>
        /// <returns></returns>
        [Route("index2")]
        [HttpGet]
        public async Task<JsonResult> Index2(InputStationModel inputStationModel, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<Station, bool>> where =
                p =>
               (string.IsNullOrEmpty(inputStationModel.StationName) || p.StationName.Contains(inputStationModel.StationName)) &&
               (inputStationModel.StreetID == null || p.StreetID == inputStationModel.StreetID) &&
               (string.IsNullOrEmpty(inputStationModel.StreetName) || p.StreetName == inputStationModel.StreetName);
            var model = await _stationService.GetPagedAsync(where, i => i.Sort, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputStationModel> { Total = model.Item1, List = Mapper.Map<IList<OutputStationModel>>(model.Item2) });
        }





        /// <summary>
        /// 查询符合条件的数据2
        /// </summary>
        /// <returns></returns>
        [Route("index3")]
        [HttpGet]
        public async Task<JsonResult> Index3(InputStationModel inputStationModel)
        {
            Expression<Func<Station, bool>> where =
                p =>
               (string.IsNullOrEmpty(inputStationModel.StationName) || p.StationName.Contains(inputStationModel.StationName))&&
               (inputStationModel.StreetID==null|| p.StreetID== inputStationModel.StreetID)&&
               (string.IsNullOrEmpty(inputStationModel.StreetName)||p.StreetName== inputStationModel.StreetName);           
            var model = await _stationService.GetAllListAsync(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputStationModel> { Total = model.Count, List = Mapper.Map<IList<OutputStationModel>>(model) });
        }


    }
}