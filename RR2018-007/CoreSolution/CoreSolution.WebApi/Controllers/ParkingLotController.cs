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
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ParkingLot;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ParkingLot")]
    public class ParkingLotController : ControllerBase
    {
        private readonly IParkingLotService _parkingLotService;
        public ParkingLotController(IParkingLotService parkingLotService)
        {
            _parkingLotService = parkingLotService;
        }

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputParkingLotModel model)
        {
            if (model.StreetId == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "街道不能为空");
            }
            if (model.JuWeiId == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "居委不能为空");
            }
            if (model.QuartersId == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "小区不能为空");
            }
            if (model.ParkingCount == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "车位总数不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<ParkingLotDto>(model);
            var id = await _parkingLotService.InsertAndGetIdAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">车位ID</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var infoDto = await _parkingLotService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该车位不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _parkingLotService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputParkingLotModel model)
        {
            var infoDto = await _parkingLotService.GetAsync(Guid.Parse(model.Id.ToString()));
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该车位不存在");
            }
            infoDto = Mapper.Map<ParkingLotDto>(model);
            await _parkingLotService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }




        /// <summary>
        /// 根据Id获取车位。200获取成功;404未找到
        /// </summary>
        /// <param name="id">车位Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _parkingLotService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该车位不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <returns></returns>
        [Route("getParkingLots")]
        [HttpGet]
        public async Task<JsonResult> GetParkingLots(SearchParkingLotModel model)
        {
            Expression<Func<ParkingLot, bool>> where = p =>
               (model.StreetId == null || p.StreetId == model.StreetId) &&
               (model.StationId == null || p.StationId == model.StationId) &&
               (model.JuWeiId == null || p.JuWeiId == model.JuWeiId) &&
               (model.QuartersId == null || p.QuartersId == model.QuartersId) &&
               (model.ParkingCount_Start == null || p.ParkingCount >= model.ParkingCount_Start) &&
               (model.ParkingCount_End == null || p.ParkingCount <= model.ParkingCount_End) &&
                (model.PublicChargeCount_Start == null || p.PublicChargeCount >=model.PublicChargeCount_Start) &&
               (model.PublicChargeCount_End == null || p.PublicChargeCount <= model.PublicChargeCount_End) &&
               (model.PublicCount_Start == null || p.PublicCount >= model.PublicCount_Start) &&
               (model.PublicCount_End == null || p.PublicCount <= model.PublicCount_End) &&
               (model.ChanQuanCount_Start == null || p.ChanQuanCount >= model.ChanQuanCount_Start) &&
               (model.ChanQuanCount_End == null || p.ChanQuanCount <= model.ChanQuanCount_End);

            var result = await _parkingLotService.GetPagedAsync(where, i => i.CreationTime, model.pageIndex, model.pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputParkingLotModel>
            { Total = result.Item1, List = Mapper.Map<IList<OutputParkingLotModel>>(result.Item2) });
        }
    }
}