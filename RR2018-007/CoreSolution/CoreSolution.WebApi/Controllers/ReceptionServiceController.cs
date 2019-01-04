using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models.ReceptionService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Dto;
using CoreSolution.IService;
using AutoMapper;
using CoreSolution.WebApi.Models;
using Microsoft.AspNetCore.Cors;
using System.Linq.Expressions;
using CoreSolution.Domain.Entities;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ReceptionService")]
    public class ReceptionServiceController : Controller
    {
        private readonly IReceptionService _receptionService;

        public ReceptionServiceController(IReceptionService receptionService)
        {
            _receptionService = receptionService;
        }

        /// <summary>
        /// 新增一条服务信息
        /// </summary>
        /// <param name="inputServiceModel"></param>
        /// <returns></returns>
        [Route("AddService")]
        [HttpPost]
        public async Task<JsonResult> AddService(InputReceptionServiceModel inputServiceModel)
        {
            if (inputServiceModel.ServiceName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "服务名称不能为空");
            }
            var receptionServiceDto = new ReceptionServiceDto
            {
                ServiceName = inputServiceModel.ServiceName,
                Category = (int)inputServiceModel.Category,
                ServiceDescription = inputServiceModel.ServiceDescription,
                ServiceFlow = inputServiceModel.ServiceFlow,
                ServiceBasis = inputServiceModel.ServiceBasis,
                ApplicationConditions = inputServiceModel.ApplicationConditions,
                ServiceAddress = inputServiceModel.ServiceAddress,
                TimeDescription = inputServiceModel.TimeDescription,
                CaChargeSituationtegory = (int)inputServiceModel.CaChargeSituationtegory,
                ServiceCost = inputServiceModel.ServiceCost,
                MattersAttention = inputServiceModel.MattersAttention,
                StreetId = inputServiceModel.StreetId,
                StreetName = inputServiceModel.StreetName,
                PostStationId = inputServiceModel.PostStationId,
                PostStationName = inputServiceModel.PostStationName,
                ActivityImg = inputServiceModel.ActivityImg,
                AttachmentPath = inputServiceModel.AttachmentPath,
                ServiceAgencyId = inputServiceModel.ServiceAgencyId,
                ServiceAgencyName = inputServiceModel.ServiceAgencyName,
                Phone=inputServiceModel.Phone
            };
            var id = await _receptionService.InsertAndGetIdAsync(receptionServiceDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", id);
        }

        /// <summary>
        /// 根据Id获取服务信息。200获取成功
        /// </summary>
        /// <param name="serviceId">fuwuId</param>
        /// <returns></returns>
        [Route("getRService")]
        [HttpGet]
        public async Task<JsonResult> GetReceptionServiceById(Guid serviceId)
        {
            var result = await _receptionService.GetAsync(serviceId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", Mapper.Map<OutputReceptionServiceModel>(result));
        }

        /// <summary>
        /// 删除服务。200删除成功，404该服务不存在
        /// </summary>
        /// <param name="serviceId">服务Id</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete([FromBody]Guid serviceId)
        {
            var service = await _receptionService.GetAsync(serviceId);
            if (service == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务不存在");
            }
            service.DeletionTime = DateTime.Now;
            await _receptionService.DeleteAsync(service);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 分页获取服务信息列表。200获取成功
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("getServicePaged")]
        [HttpGet]
        public async Task<JsonResult> GetReceptionServicesPaged(int pageIndex = 1, int pageSize = 10)
        {
            var result = await _receptionService.GetPagedAsync(i => true, i => i.CreationTime, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputReceptionServiceModel> { Total = result.Item1, List = Mapper.Map<IList<OutputReceptionServiceModel>>(result.Item2) });
        }


        /// <summary>
        /// 获取服务列表(根据传递不同的参数值做查询)
        /// </summary>
        /// <param name="SRSM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetListData")]
        [HttpGet]
        public async Task<JsonResult> GetListData(SearchReceptionServiceModel SRSM, int pageIndex = 1, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<ReceptionService, bool>> where = p =>
                 (string.IsNullOrEmpty(SRSM.serviceName) || p.ServiceName.Contains(SRSM.serviceName)) &&
                 (SRSM.category == null || p.Category == SRSM.category) && (SRSM.streetId == null || p.StreetId == SRSM.streetId) && (SRSM.PostStationId == null || p.PostStationId == SRSM.PostStationId);

            var result = await _receptionService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputReceptionServiceModel> { Total = result.Item1, List = Mapper.Map<IList<OutputReceptionServiceModel>>(result.Item2) });
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputReceptionServiceModel inputServiceModel)
        {
            if (inputServiceModel.ServiceName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "服务名称不能为空");
            }
            var info = await _receptionService.GetAsync(Guid.Parse(inputServiceModel.Id.ToString()));
            info.ServiceName = inputServiceModel.ServiceName;
            info.Category = (int)inputServiceModel.Category;
            info.ServiceDescription = inputServiceModel.ServiceDescription;
            info.ServiceFlow = inputServiceModel.ServiceFlow;
            info.ServiceBasis = inputServiceModel.ServiceBasis;
            info.ApplicationConditions = inputServiceModel.ApplicationConditions;
            info.ServiceAddress = inputServiceModel.ServiceAddress;
            info.TimeDescription = inputServiceModel.TimeDescription;
            info.CaChargeSituationtegory = (int)inputServiceModel.CaChargeSituationtegory;
            info.ServiceCost = inputServiceModel.ServiceCost;
            info.MattersAttention = inputServiceModel.MattersAttention;
            info.ArchivalRemark = inputServiceModel.ArchivalRemark;
            info.Archiving = inputServiceModel.Archiving;
            info.ActivityImg = inputServiceModel.ActivityImg;
            info.AttachmentPath = inputServiceModel.AttachmentPath;
            info.StreetId = inputServiceModel.StreetId;
            info.StreetName = inputServiceModel.StreetName;
            info.PostStationId = inputServiceModel.PostStationId;
            info.PostStationName = inputServiceModel.PostStationName;
            info.ServiceAgencyId = inputServiceModel.ServiceAgencyId;
            info.ServiceAgencyName = inputServiceModel.ServiceAgencyName;
            info.Phone = inputServiceModel.Phone;
            await _receptionService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("IsGuiDang")]
        [HttpPost]
        public async Task<JsonResult> IsGuiDang(InputReceptionServiceModel inputServiceModel)
        {
            //var receptionServiceDto = new ReceptionServiceDto
            //{
            //    Id = inputServiceModel.Id.GetValueOrDefault(),//获取这个可空值的默认值
            //    IsGuiDang = inputServiceModel.IsGuiDang,
            //    ArchivalRemark = inputServiceModel.ArchivalRemark,
            //    Archiving = inputServiceModel.Archiving,
            //    FilingDate = DateTime.Now
            //};


            var receptionServiceDto = await _receptionService.GetAsync(inputServiceModel.Id.Value);
            if (receptionServiceDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "不存在");
            }
            receptionServiceDto.IsGuiDang = inputServiceModel.IsGuiDang;
            receptionServiceDto.ArchivalRemark = inputServiceModel.ArchivalRemark;
            receptionServiceDto.Archiving = inputServiceModel.Archiving;
            receptionServiceDto.FilingDate = DateTime.Now;

            var result =await _receptionService.UpdateAsync(receptionServiceDto);

            return result != null ? AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功") : AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "修改");
           
        }


        




    }
}