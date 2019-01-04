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
using CoreSolution.WebApi.Models.JuWei;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/JuWei")]
    public class JuWeiController : ControllerBase
    {
        private readonly IJuWeiService _juWeiService;

        public JuWeiController(IJuWeiService juWeiService)
        {
            _juWeiService = juWeiService;
        }

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputJuWeiModel model)
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
            var infoDto = Mapper.Map<JuWeiDto>(model);
            var id = await _juWeiService.InsertAndGetIdAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">居委Id</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var infoDto = await _juWeiService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该居委不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _juWeiService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <param name="id">居委Id</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputJuWeiModel model, Guid id)
        {
            var infoDto = await _juWeiService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该居委不存在");
            }
            infoDto = Mapper.Map<JuWeiDto>(model);
            infoDto.Id = id;
            await _juWeiService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 根据Id获取。200获取成功;404未找到
        /// </summary>
        /// <param name="id">居委Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _juWeiService.GetAsync(id);
            StreetService streetService = new StreetService();
            StationService stationService = new StationService();
            result.StreetName = streetService.GetStreetName(result.StreetId);
            result.PostStationName = stationService.GetStationName(result.PostStationId);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该居委不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <param name="name">居委名称</param>
        /// <param name="streetid">街道id</param>
        /// <param name="stationid">驿站id</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("getJuWeis")]
        [HttpGet]
        public async Task<JsonResult> GetJuWeis(string name, Guid? streetid,Guid? stationid, int pageIndex = 1, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<JuWei, bool>> where = p =>
                 (string.IsNullOrEmpty(name) || p.Name.Contains(name)) &&
                 (streetid == null || p.StreetId == streetid) &&
                 (stationid == null || p.PostStationId== stationid);
            var result = await _juWeiService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);

            #region 处理list
            if (result != null && result.Item2.Count > 0)
            {
                StreetService streetService = new StreetService();
                StationService stationService = new StationService();
                foreach (var item in result.Item2)
                {
                    item.StreetName = streetService.GetStreetName(item.StreetId);
                    item.PostStationName = stationService.GetStationName(item.PostStationId);
                }
            }
            #endregion

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputJuWeiModel>
            { Total = result.Item1, List = Mapper.Map<IList<OutputJuWeiModel>>(result.Item2) });
        }
    }
}