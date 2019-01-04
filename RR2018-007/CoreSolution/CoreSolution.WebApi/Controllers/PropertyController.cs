using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Service;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.Property;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Property")]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyService _propertyService;

        public PropertyController(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputPropertyModel model)
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
                        errorlist.Add(new ErrorModel() { Name = key,ErrorMsg= error.ErrorMessage });
                    }
                }
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "数据格式不正确", errorlist);
            }
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<PropertyDto>(model);
            var id = await _propertyService.InsertAndGetIdAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var infoDto = await _propertyService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _propertyService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputPropertyModel model, Guid id)
        {
            var infoDto = await _propertyService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区不存在");
            }
            infoDto = Mapper.Map<PropertyDto>(model);
            infoDto.Id = id;
            await _propertyService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 根据Id获取单条信息。200获取成功;404未找到
        /// </summary>
        /// <param name="id">物业Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _propertyService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该物业不存在", result);
            }
            StreetService streetService = new StreetService();
            JuWeiService juweiService = new JuWeiService();
            QuartersService quartersService = new QuartersService();
            StationService stationService = new StationService();
            result.StreetName = streetService.GetStreetName(result.StreetId);
            result.JuWeiName = juweiService.GetJuWeiName(result.JuWeiId);
            result.QuartersName = quartersService.GetQuartersName(result.QuartersId);
            result.StationName = stationService.GetStationName(result.StationId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <returns></returns>
        [Route("getPropertys")]
        [HttpGet]
        public async Task<JsonResult> GetPropertys(string Name, Guid? StreetId, Guid? StationId, Guid? JuWeiId, Guid? QuartersId, int pageIndex, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<Property, bool>> where = p =>
                 (string.IsNullOrEmpty(Name) || p.Name.Contains(Name)) &&
               (StationId == null || p.StationId == StationId) &&
               (StreetId == null || p.StreetId == StreetId) &&
               (JuWeiId == null || p.JuWeiId == JuWeiId) &&
               (QuartersId == null || p.StreetId == QuartersId);
            var result = await _propertyService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);

            #region 处理list

            if (result != null && result.Item2.Count > 0)
            {
                StreetService streetService = new StreetService();
                JuWeiService juweiService = new JuWeiService();
                QuartersService quartersService = new QuartersService();
                StationService stationService = new StationService();

                foreach (var item in result.Item2)
                {
                    item.StreetName = streetService.GetStreetName(item.StreetId);
                    item.JuWeiName = juweiService.GetJuWeiName(item.JuWeiId);
                    item.QuartersName = quartersService.GetQuartersName(item.QuartersId);
                    item.StationName = stationService.GetStationName(item.StationId);
                }
            }
            #endregion

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputPropertyModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputPropertyModel>>(result.Item2)
            });
        }




        [Route("getPropertysByQuartersId")]
        [HttpGet]
        public async Task<JsonResult> GetPropertysByQuartersId(Guid? QuartersId, int pageIndex = 1, int pageSize = 100)
        {
            Expression<Func<Property, bool>> where = p =>
               (QuartersId == null || p.QuartersId == QuartersId);
            var result = await _propertyService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputPropertyModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputPropertyModel>>(result.Item2)
            });
        }

    }
}