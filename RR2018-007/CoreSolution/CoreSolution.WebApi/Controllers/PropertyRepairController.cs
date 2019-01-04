using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Domain.Enum;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Service;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.PropertyRepair;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/PropertyRepair")]
    public class PropertyRepairController : ControllerBase
    {

        private readonly IPropertyRepairService _propertyRepairService;
        public PropertyRepairController(IPropertyRepairService propertyRepairService)
        {
            _propertyRepairService = propertyRepairService;
        }

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputPropertyRepairModel model)
        {
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<PropertyRepairDto>(model);
            var id = await _propertyRepairService.InsertAndGetIdAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");

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
            var infoDto = await _propertyRepairService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该物业报修不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _propertyRepairService.DeleteAsync(infoDto);
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
        public async Task<JsonResult> Modify(InputPropertyRepairModel model, Guid id)
        {
            var infoDto = await _propertyRepairService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该物业报修不存在");
            }
            infoDto = Mapper.Map<PropertyRepairDto>(model);
            infoDto.Id = id;
            await _propertyRepairService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 根据Id获取。200获取成功;404未找到
        /// </summary>
        /// <param name="id">小区Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _propertyRepairService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该物业报修不存在", result);
            }
            PropertyService propertyService = new PropertyService();
            StreetService streetServic = new StreetService();
            JuWeiService juWeiService = new JuWeiService();
            QuartersService quartersService = new QuartersService();
            StationService stationService = new StationService();

            result.StatusCodeStr = EnumExtensions.GetDescription(result.StatusCode);
            result.PropertyName = propertyService.GetPropertyName(result.PropertyId);
            result.StreetName = streetServic.GetStreetName(result.StreetId);
            result.JuWeiName = juWeiService.GetJuWeiName(result.JuWeiId);
            result.QuartersName = quartersService.GetQuartersName(result.QuartersId);
            result.StationName = stationService.GetStationName(result.StationId);

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        [Route("getPropertyRepairs")]
        [HttpGet]
        public async Task<JsonResult> GetPropertyRepairs(SearchPropertyRepairModel model)
        {
            #region 过滤条件
            Expression<Func<PropertyRepair, bool>> where = p =>
               (model.StationId == null || p.StationId == model.StationId) &&
               (model.JuWeiId == null || p.JuWeiId == model.JuWeiId) &&
               (model.QuartersId == null || p.QuartersId == model.QuartersId) &&
               (model.StreetId == null || p.StreetId == model.StreetId) &&
               (model.PropertyId == null || p.PropertyId == model.PropertyId) &&
               (model.StatusCode == null || p.StatusCode == model.StatusCode) &&
               (model.RepairTime_Start == null || p.RepairTime >= model.RepairTime_Start) &&
               (model.RepairTime_End == null || p.RepairTime <= model.RepairTime_End);
            #endregion

            var result = await _propertyRepairService.GetPagedAsync(where, i => i.CreationTime, model.pageIndex, model.pageSize, true);
            #region 处理list

            if (result != null && result.Item2.Count > 0)
            {
                PropertyService service = new PropertyService();
                foreach (var item in result.Item2)
                {
                    item.StatusCodeStr = EnumExtensions.GetDescription(item.StatusCode);
                    item.PropertyName = service.GetPropertyName(item.PropertyId);
                }
            }
            #endregion
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputPropertyRepairModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputPropertyRepairModel>>(result.Item2)
            });
        }


        /// <summary>
        /// 处理结果登记
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="disposeresult">完成情况</param>
        /// <param name="disposeuser">处理人</param>
        /// <param name="disposetime">处理日期</param>
        /// <returns></returns>
        [Route("disposeSubmit")]
        [HttpPost]
        public async Task<JsonResult> DisposeSubmit(Guid id, string disposeresult, string disposeuser, DateTime disposetime)
        {
            var infoDto = await _propertyRepairService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该物业报修不存在");
            }
            infoDto.Id = id;
            infoDto.DisposeResult = disposeresult;
            infoDto.DisposeUser = disposeuser;
            infoDto.DisposeTime = disposetime;
            infoDto.StatusCode = EnumCode.ProStatusCode.Yes;
            await _propertyRepairService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
    }
}