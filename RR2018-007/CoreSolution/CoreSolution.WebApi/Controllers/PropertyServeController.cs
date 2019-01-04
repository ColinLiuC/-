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
using CoreSolution.WebApi.Models.PropertyServe;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/PropertyServe")]
    public class PropertyServeController : ControllerBase
    {
        private readonly IPropertyServeService _propertyServeService;
        private readonly IDataDictionaryService _dataDictionaryService;


        public PropertyServeController(IPropertyServeService ropertyServeService, IDataDictionaryService dataDictionaryService)
        {
            _propertyServeService = ropertyServeService;
            _dataDictionaryService = dataDictionaryService;
        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputPropertyServeModel model)
        {
            if (model.ChargeSituationId == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "收费情况不能为空");
            }
            if (string.IsNullOrWhiteSpace(model.ServeTel))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "电话不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<PropertyServeDto>(model);
            var id = await _propertyServeService.InsertAndGetIdAsync(infoDto);
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
            var infoDto = await _propertyServeService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _propertyServeService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputPropertyServeModel model)
        {
            var infoDto = await _propertyServeService.GetAsync(Guid.Parse(model.Id.ToString()));
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该物业服务不存在");
            }
            infoDto = Mapper.Map<PropertyServeDto>(model);
            infoDto.Id = Guid.Parse(model.Id.ToString());
            await _propertyServeService.UpdateAsync(infoDto);
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
            var result = await _propertyServeService.GetAsync(id);
            result.ChargeSituation = _dataDictionaryService.GetItemNameById(result.ChargeSituationId);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该物业服务不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <returns></returns>
        [Route("getPropertys")]
        [HttpGet]
        public async Task<JsonResult> GetPropertys(Guid? propertyId, Guid? chargeSituationId, int pageIndex, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<PropertyServe, bool>> where = p =>
               (propertyId == null || p.PropertyId == propertyId) &&
            (chargeSituationId == null || p.ChargeSituationId == chargeSituationId);
            var result = await _propertyServeService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            if (result.Item2 != null)
            {
                foreach (var item in result.Item2)
                {
                    item.ChargeSituation = _dataDictionaryService.GetItemNameById(item.ChargeSituationId);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputPropertyServeModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputPropertyServeModel>>(result.Item2)
            });
        }
    }
}