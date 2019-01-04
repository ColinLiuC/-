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
using CoreSolution.WebApi.Models.Building;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Building")]
    public class BuildingController : ControllerBase
    {
        private readonly IBuildingService _buildingService;

        public BuildingController(IBuildingService buildingService)
        {
            _buildingService = buildingService;
        }

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputBuildingModel model)
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
            var infoDto = Mapper.Map<BuildingDto>(model);
            var id = await _buildingService.InsertAndGetIdAsync(infoDto);
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
            var infoDto = await _buildingService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _buildingService.DeleteAsync(infoDto);
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
        public async Task<JsonResult> Modify(InputBuildingModel model, Guid id)
        {
            var infoDto = await _buildingService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区不存在");
            }
            infoDto = Mapper.Map<BuildingDto>(model);
            infoDto.Id = id;
            await _buildingService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }


        /// <summary>
        /// 根据Id获取楼栋。200获取成功;404未找到
        /// </summary>
        /// <param name="id">小区Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _buildingService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该楼栋不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        [Route("getMyBuilding")]
        [HttpGet]
        public JsonResult GetMyBuilding(Guid id)
        {
            var result = _buildingService.GetBuildingInfo(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该楼栋不存在", result);
            }
            StreetService streetService = new StreetService();
            JuWeiService juweiService = new JuWeiService();
            QuartersService quartersService = new QuartersService();
            StationService stationService = new StationService();
            PropertyService propertyService = new PropertyService();
            result.StreetName = streetService.GetStreetName(result.building.StreetId);
            result.JuWeiName = juweiService.GetJuWeiName(result.building.JuWeiId);
            result.QuartersName = quartersService.GetQuartersName(result.building.QuartersId);
            result.StationName = stationService.GetStationName(result.building.StationId);
            result.PropertyName = propertyService.GetPropertyName(result.building.PropertyId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <returns></returns>
        [Route("getBuildings")]
        [HttpGet]
        public async Task<JsonResult> GetBuildings(SearchBuildingModel model)
        {
            //拼接过滤条件
            Expression<Func<Building, bool>> where = p =>
               (string.IsNullOrEmpty(model.Address) || p.Address.Contains(model.Address)) &&
               (model.isElevator == null || p.IsElevator == model.isElevator) &&
               (string.IsNullOrEmpty(model.belongedYear) || p.BelongedYear == model.belongedYear) &&
              (model.StreetId == null || p.StreetId == model.StreetId) &&
              (model.StationId == null || p.StationId == model.StationId) &&
             (model.JuWeiId == null || p.JuWeiId == model.JuWeiId) &&
             (model.QuartersId == null || p.QuartersId == model.QuartersId);

            var result = await _buildingService.GetPagedAsync(where, i => i.CreationTime, model.pageIndex, model.pageSize, true);

            #region 处理list
            if (result != null && result.Item2.Count > 0)
            {
                StreetService streetService = new StreetService();
                StationService stationService = new StationService();
                JuWeiService juweiService = new JuWeiService();
                QuartersService quartersService = new QuartersService();
                foreach (var item in result.Item2)
                {
                    item.StreetName = streetService.GetStreetName(item.StreetId);
                    item.StationName = stationService.GetStationName(item.StationId);
                    item.JuWeiName = juweiService.GetJuWeiName(item.JuWeiId);
                    item.QuartersName = quartersService.GetQuartersName(item.QuartersId);
                }
            }
            #endregion

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputBuildingModel>
            { Total = result.Item1, List = Mapper.Map<IList<OutputBuildingModel>>(result.Item2) });
        }
    }
}