using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ResourcePerson;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ResourcePerson")]
    //[ApiController]
    public class ResourcePersonController : ControllerBase
    {
        private readonly IResourcePersonService _resourcePersonService;
        private readonly IDataDictionaryService _dataDictionaryService;
        private readonly IJuWeiService _juweiService;
        private readonly IStreetService _streetService;
        private readonly IStationService _stationService;
        public ResourcePersonController(IResourcePersonService resourcePersonService, IDataDictionaryService dataDictionaryService, IJuWeiService juweiService, IStreetService streetService, IStationService stationService)
        {
            _resourcePersonService = resourcePersonService;
            _dataDictionaryService = dataDictionaryService;
            _juweiService = juweiService;
            _streetService = streetService;
            _stationService = stationService;
        }

        /// <summary>
        /// 查询符合条件的数据
        /// </summary>
        /// <returns></returns>
        [Route("index")]
        [HttpGet]
        public async Task<JsonResult> Index(string Name, string Card, int? sAge, int? eAge, Guid? PerType, Guid? Street, Guid? Station, Guid? JuWei,  int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<ResourcePerson, bool>> where = PredicateExtensions.True<ResourcePerson>();
            if (!string.IsNullOrEmpty(Name))
            {
                where = where.And(p => p.Name == Name);
            }
            if (!string.IsNullOrEmpty(Card))
            {
                where = where.And(p => p.Card == Card);
            }
            if (sAge!=null)
            {
                where = where.And(p => p.Age >= sAge);
            }
            if (eAge!=null)
            {
                where = where.And(p => p.Age <= eAge);
            }
            if (PerType != null)
            {
                where = where.And(p => p.PerType == PerType);
            }
            if (Street != null)
            {
                where = where.And(p => p.Street == Street);
            }
            if (Station != null)
            {
                where = where.And(p => p.Station == Station);
            }
            if (JuWei != null)
            {
                where = where.And(p => p.JuWei == JuWei);
            }
            var model = await _resourcePersonService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize,true);
            if (model.Item2.Count > 0)
            {
                foreach (var item in model.Item2)
                {
                    item.PerTypeName = _dataDictionaryService.GetDataName(item.PerType);
                    item.StreetName = _streetService.GetStreetName(item.Street);
                    item.StationName = _stationService.GetStationName(item.Station);
                    item.JuWeiName = _juweiService.GetJuWeiName(item.JuWei);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputResourcePersonModel> { Total = model.Item1, List = Mapper.Map<IList<OutputResourcePersonModel>>(model.Item2) });
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputResourcePersonModel inputResourcePersonModel)
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
            var resourceOrgDto = new ResourcePersonDto
            {
                Name = inputResourcePersonModel.Name,
                Card = inputResourcePersonModel.Card,
                BirthDay = inputResourcePersonModel.BirthDay,
                Sex = inputResourcePersonModel.Sex,
                Age = inputResourcePersonModel.Age,
                Phone = inputResourcePersonModel.Phone,
                JuWei = inputResourcePersonModel.JuWei,
                Street = inputResourcePersonModel.Street,
                Station = inputResourcePersonModel.Station,
                Address = inputResourcePersonModel.Address,
                PerType =inputResourcePersonModel.PerType,
                Degree=inputResourcePersonModel.Degree,
                Strength=inputResourcePersonModel.Strength
            };
            await _resourcePersonService.InsertAsync(resourceOrgDto);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", ok);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputResourcePersonModel inputResourcePersonModel)
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
            var result = await _resourcePersonService.GetAsync(inputResourcePersonModel.Id);
                result.Name = inputResourcePersonModel.Name;
                result.Card = inputResourcePersonModel.Card;
                result.BirthDay = inputResourcePersonModel.BirthDay;
                result.Sex = inputResourcePersonModel.Sex;
                result.Age = inputResourcePersonModel.Age;
                result.Phone = inputResourcePersonModel.Phone;
                result.JuWei = inputResourcePersonModel.JuWei;
                result.Street = inputResourcePersonModel.Street;
                result.Station = inputResourcePersonModel.Station; 
                result.Address = inputResourcePersonModel.Address;
                result.PerType = inputResourcePersonModel.PerType;
                result.Degree = inputResourcePersonModel.Degree;
                result.Strength = inputResourcePersonModel.Strength;
            await _resourcePersonService.UpdateAsync(result);
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
            var info = await _resourcePersonService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该用户不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            await _resourcePersonService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetResourcePerson")]
        [HttpGet]
        public async Task<JsonResult> GetResourcePerson(Guid id)
        {
            var result = await _resourcePersonService.GetAsync(id);
            result.PerTypeName = _dataDictionaryService.GetDataName(result.PerType);
            result.DegreeName = _dataDictionaryService.GetDataName(result.Degree);
            result.StreetName = _streetService.GetStreetName(result.Street);
            result.JuWeiName = _juweiService.GetJuWeiName(result.JuWei);
            result.StationName = _stationService.GetStationName(result.Station);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }






    }
}