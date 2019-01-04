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
using CoreSolution.WebApi.Models.EventBurst;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/EventBurst")]
    public class EventBurstController : ControllerBase
    {
        private readonly IEventBurstService _eventBurstService;

        public EventBurstController(IEventBurstService eventBurstService)
        {
            _eventBurstService = eventBurstService;
        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputEventBurstModel model)
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
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<EventBurstDto>(model);
            var id = await _eventBurstService.InsertAndGetIdAsync(infoDto);
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
            var infoDto = await _eventBurstService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _eventBurstService.DeleteAsync(infoDto);
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
        public async Task<JsonResult> Modify(InputEventBurstModel model, Guid id)
        {
            var infoDto = await _eventBurstService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该突发事件不存在");
            }
            infoDto = Mapper.Map<EventBurstDto>(model);
            infoDto.Id = id;
            await _eventBurstService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 根据Id获取单条信息。200获取成功;404未找到
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _eventBurstService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该突发事件不存在", result);
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
        [Route("getEventBursts")]
        [HttpGet]
        public async Task<JsonResult> GetEventBursts(SearchEventBurstModel model)
        {
            //拼接过滤条件
            Expression<Func<EventBurst, bool>> where = p =>
                 (string.IsNullOrEmpty(model.Name) || p.Name.Contains(model.Name)) &&
                 (string.IsNullOrEmpty(model.Address) || p.Address.Contains(model.Address)) &&
                 (model.HappenTime_Start == null || p.HappenTime >= model.HappenTime_Start) &&
                 (model.HappenTime_End == null || p.HappenTime <= model.HappenTime_End) &&
                (model.StreetId == null || p.StreetId == model.StreetId) &&
                (model.StationId == null || p.StationId == model.StationId) &&
                (model.QuartersId == null || p.QuartersId == model.QuartersId) &&
                (model.JuWeiId == null || p.JuWeiId == model.JuWeiId);

            var result = await _eventBurstService.GetPagedAsync(where, i => i.CreationTime, model.pageIndex, model.pageSize, true);

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputEventBurstModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputEventBurstModel>>(result.Item2)
            });
        }

    }
}