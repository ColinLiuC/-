using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ResourcePlace;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ResourcePlace")]
    //[ApiController]
    public class ResourcePlaceController : ControllerBase
    {
        private readonly IResourcePlaceService _resourcePlaceService;
        private readonly IDataDictionaryService _dataDictionaryService;
        private readonly IStationService _stationService;
        private readonly IStreetService _streetService;
        public ResourcePlaceController(IResourcePlaceService resourcePlaceService, IDataDictionaryService dataDictionaryService, IStationService stationService, IStreetService streetService)
        {
            _resourcePlaceService = resourcePlaceService;
            _dataDictionaryService = dataDictionaryService;
            _stationService = stationService;
            _streetService = streetService;
        }


        /// <summary>
        /// 查询符合条件的数据
        /// </summary>
        /// <returns></returns>
        [Route("index")]
        [HttpGet]
        public async Task<JsonResult> Index(string Name,Guid? ResourceCategory,Guid? ResourceType,Guid? Street,Guid? Station, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<ResourcePlace, bool>> where = PredicateExtensions.True<ResourcePlace>();
            if (!string.IsNullOrEmpty(Name))
            {
                where = where.And(p => p.Name == Name);
            }
            if (ResourceCategory!=null)
            {
                where = where.And(p => p.ResourceCategory == ResourceCategory);
            }
            if (ResourceType!=null)
            {
                where = where.And(p => p.ResourceType == ResourceType);
            }
            if (Street != null)
            {
                where = where.And(p => p.Street == Street);
            }
            if (Station != null)
            {
                where = where.And(p => p.Station == Station);
            }
            var model = await _resourcePlaceService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize,true);
            if (model.Item2.Count > 0)
            {
                foreach (var item in model.Item2)
                {
                    item.ResourceCategoryName = _dataDictionaryService.GetDataName(item.ResourceCategory);
                    item.ResourceTypeName = _dataDictionaryService.GetDataName(item.ResourceType);
                    item.StreetName = _streetService.GetStreetName(item.Street);
                    item.StationName = _stationService.GetStationName(item.Station);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputResourcePlaceModel> { Total = model.Item1, List = Mapper.Map<IList<OutputResourcePlaceModel>>(model.Item2) });
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputResourcePlaceModel inputResourcePlaceModel)
        {
            if (!ModelState.IsValid)
            {
                var errorlist = new List<ErrorModel>();
                //获取所有错误的Key
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述添加到sb中
                    foreach (var error in errors)
                    {
                        errorlist.Add(new ErrorModel() { Name = key, ErrorMsg = error.ErrorMessage });
                    }
                }
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "数据格式不正确", errorlist);
            }

            string token = HttpContext.Request.Headers["token"];
            var resourcePlaceDto = new ResourcePlaceDto
            {
                Name = inputResourcePlaceModel.Name,
                ResourceCategory = inputResourcePlaceModel.ResourceCategory,
                ResourceType = inputResourcePlaceModel.ResourceType,
                Street = inputResourcePlaceModel.Street,
                Station = inputResourcePlaceModel.Station,
                Address = inputResourcePlaceModel.Address,
                Xaxis = inputResourcePlaceModel.Xaxis,
                Yaxis = inputResourcePlaceModel.Yaxis,
                Remarks = inputResourcePlaceModel.Remarks,
                User = inputResourcePlaceModel.User,
                CreateDate = inputResourcePlaceModel.CreateDate,
                Phone=inputResourcePlaceModel.Phone
            };
            await _resourcePlaceService.InsertAsync(resourcePlaceDto);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", ok);
        }


        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputResourcePlaceModel inputResourcePlaceModel)
        {
            if (!ModelState.IsValid)
            {
                var errorlist = new List<ErrorModel>();
                //获取所有错误的Key
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述添加到sb中
                    foreach (var error in errors)
                    {
                        errorlist.Add(new ErrorModel() { Name = key, ErrorMsg = error.ErrorMessage });
                    }
                }
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "数据格式不正确", errorlist);
            }
            string token = HttpContext.Request.Headers["token"];
            var result = await _resourcePlaceService.GetAsync(inputResourcePlaceModel.Id);
            result.Id = inputResourcePlaceModel.Id;
            result.Name = inputResourcePlaceModel.Name;
            result.ResourceCategory = inputResourcePlaceModel.ResourceCategory;
            result.ResourceType = inputResourcePlaceModel.ResourceType;
            result.Street = inputResourcePlaceModel.Street;
            result.Station = inputResourcePlaceModel.Station;
            result.Address = inputResourcePlaceModel.Address;
            result.Xaxis = inputResourcePlaceModel.Xaxis;
            result.Yaxis = inputResourcePlaceModel.Yaxis;
            result.Remarks = inputResourcePlaceModel.Remarks;
            result.User = inputResourcePlaceModel.User;
            result.CreateDate = inputResourcePlaceModel.CreateDate;
            result.Phone = inputResourcePlaceModel.Phone;
            await _resourcePlaceService.UpdateAsync(result);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功", ok);
        }


        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _resourcePlaceService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该用户不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            await _resourcePlaceService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetResourcePlace")]
        [HttpGet]
        public async Task<JsonResult> GetResourcePlace(Guid id)
        {
            var result = await _resourcePlaceService.GetAsync(id);
            result.ResourceCategoryName = _dataDictionaryService.GetDataName(result.ResourceCategory);
            result.ResourceTypeName = _dataDictionaryService.GetDataName(result.ResourceType);
            result.StreetName = _streetService.GetStreetName(result.Street);
            result.StationName = _stationService.GetStationName(result.Station);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据经纬度获取地址
        /// </summary>
        /// <returns></returns>
        [Route("GetLocation")]
        [HttpGet]
        public async Task<JsonResult> GetLocation(string lat, string lng)
        {
            var address = Helper.CommonHelper.GetLocation(lat,lng);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", address);
        }
    }
}