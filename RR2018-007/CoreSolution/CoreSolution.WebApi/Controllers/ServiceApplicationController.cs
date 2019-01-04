using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.IService;
using CoreSolution.WebApi.Models.ServiceApplication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using System.Net;
using CoreSolution.Dto;
using AutoMapper;
using CoreSolution.WebApi.Models;
using System.Linq.Expressions;
using CoreSolution.Domain.Entities;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ServiceApplication")]
    public class ServiceApplicationController : Controller
    {
        private readonly IServiceApplication _serviceApplicationService;
        private readonly IReceptionService _receptionService;
        public ServiceApplicationController(IServiceApplication serviceApplicationService, IReceptionService receptionService)
        {
            _serviceApplicationService = serviceApplicationService;
            _receptionService = receptionService;
        }

        /// <summary>
        /// 新增一条服务申请信息 pc端
        /// </summary>
        /// <param name="inputServiceApplicationModel"></param>
        /// <returns></returns>
        [Route("AddServiceApplication")]
        [HttpPost]
        public async Task<JsonResult> AddServiceApplication(InputServiceApplicationModel inputServiceApplicationModel)
        {
            if (inputServiceApplicationModel.ApplicantName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "申请人名称不能为空");
            }
            var serviceApplicationDto = new ServiceApplicationDto
            {
                ApplicantName = inputServiceApplicationModel.ApplicantName,
                ContactNumber = inputServiceApplicationModel.ContactNumber,
                Remarks = inputServiceApplicationModel.Remarks,
                Remark = inputServiceApplicationModel.Remarks,//接收App端申请备注和pc端申请备注共用同一字段
                Registrant = inputServiceApplicationModel.Registrant,
                //RegisterDate = inputServiceApplicationModel.RegisterDate,
                RegisterDate = DateTime.Now,
                Recipient = inputServiceApplicationModel.Registrant,  //接收App端申请服务的接收人可以和pc端申请服务的登记人使用同一字段
                ReceivingDate= inputServiceApplicationModel.RegisterDate,
                ServiceId = inputServiceApplicationModel.ServiceId,
                ApplicationSource = 0, //表明是从pc端申请的,
                Type = 2,//用户评价 默认为未评价:2
                CurrentState = 2,//默认为已接收申请人申请的这条服务信息
                 PJ_RegistStatus = 1  //默认为对服务结果未登记
            };
            var id = await _serviceApplicationService.InsertAndGetIdAsync(serviceApplicationDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", id);
        }

        /// <summary>
        /// 新增一条服务申请信息 App端
        /// </summary>
        /// <param name="inputServiceApplicationModel"></param>
        /// <returns></returns>
        [Route("AddServicebyApp")]
        [HttpPost]
        public async Task<JsonResult> AddServiceApplicationApp(InputServiceApplicationModel inputServiceApplicationModel)
        {
            if (inputServiceApplicationModel.ApplicantName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "申请人名称不能为空");
            }
            var serviceApplicationDto = new ServiceApplicationDto
            {
                ShiMinYunId=inputServiceApplicationModel.ShiMinYunId,
                ApplicantName = inputServiceApplicationModel.ApplicantName,
                ContactNumber = inputServiceApplicationModel.ContactNumber,
                Address=inputServiceApplicationModel.Address,
                ApplicationNotes = inputServiceApplicationModel.ApplicationNotes, 
                RegisterDate=DateTime.Now,//表示从App端的申请日期
                ServiceId = inputServiceApplicationModel.ServiceId,
                ApplicationSource = 1, //表明是从App端申请的
                Type = 2,//用户评价 默认为未评价:2
                CurrentState = 1,//默认为未接收
                IsReceive=0, //默认为未接收
                PJ_RegistStatus=1  //默认为对服务结果未登记
            };
            var id = await _serviceApplicationService.InsertAndGetIdAsync(serviceApplicationDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", id);
        }

        /// <summary>
        /// 接收服务
        /// </summary>
        /// <returns></returns>
        [Route("ReceiveService")]
        [HttpPost]
        public async Task<JsonResult> ReceiveService(InputServiceApplicationModel inputServiceApplicationModel)
        {
            var info = await _serviceApplicationService.GetAsync(Guid.Parse(inputServiceApplicationModel.Id.ToString()));
            info.IsReceive = inputServiceApplicationModel.IsReceive;
            info.ReceivingDate = inputServiceApplicationModel.ReceivingDate;
            info.Recipient = inputServiceApplicationModel.Recipient;
            info.CurrentState = inputServiceApplicationModel.CurrentState;
            await _serviceApplicationService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 获取服务申请人信息
        /// </summary>
        /// <param name="applicationName">申请人名称</param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetList")]
        [HttpGet]
        public async Task<JsonResult> GetServiceApplicationData(string applicationName,  DateTime startDate, DateTime endDate, int pageIndex = 1, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<ServiceApplication, bool>> where = p =>
                 (string.IsNullOrEmpty(applicationName) || p.ApplicantName.Contains(applicationName));
                 
            var result = await _serviceApplicationService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputServiceApplicationModel> { Total = result.Item1, List = Mapper.Map<IList<OutputServiceApplicationModel>>(result.Item2) });
        }
        /// <summary>
        /// 根据服务id获取申请信息
        /// </summary>
        /// <param name="serviceId">服务id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetDataByServiceId")]
        [HttpGet]
        public async Task<JsonResult> GetDataByServiceId(Guid serviceId, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _serviceApplicationService.GetPagedAsync(p=>p.ServiceId==serviceId, i => i.CreationTime, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputServiceApplicationModel> { Total = result.Item1, List = Mapper.Map<IList<OutputServiceApplicationModel>>(result.Item2) });
        }
        /// <summary>
        /// 是否接收申请
        /// </summary>
        /// <returns></returns>
        [Route("IsReceived")]
        [HttpPost]
        public async Task<JsonResult> IsReceived(InputServiceApplicationModel inputServiceApplicationModel)
        {
            var info = await _serviceApplicationService.GetAsync(Guid.Parse(inputServiceApplicationModel.Id.ToString()));
            info.IsReceive = 1;
            info.Remark = inputServiceApplicationModel.Remark;
            info.Recipient = inputServiceApplicationModel.Recipient;
            info.ReceivingDate = inputServiceApplicationModel.ReceivingDate;
            info.CurrentState = inputServiceApplicationModel.CurrentState;
            await _serviceApplicationService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }


        /// <summary>
        /// 是否登记服务结果
        /// </summary>
        /// <returns></returns>
        [Route("IsRegistered")]
        [HttpPost]
        public async Task<JsonResult> IsRegistered(InputServiceApplicationModel inputServiceApplicationModel)
        {
            var info = await _serviceApplicationService.GetAsync(Guid.Parse(inputServiceApplicationModel.Id.ToString()));
            info.ServiceResults = inputServiceApplicationModel.ServiceResults;
            info.PJ_Registrant = inputServiceApplicationModel.PJ_Registrant;
            info.PJ_RegistDate = inputServiceApplicationModel.PJ_RegistDate;
            info.PJ_RegistStatus = 2;
            //info.CurrentState = inputServiceApplicationModel.CurrentState;        
            await _serviceApplicationService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 根据申请人Id获取一条数据
        /// </summary>
        /// <param name="applicationId"></param>
        /// <returns></returns>
        [Route("GetDataById")]
        [HttpGet]
        public JsonResult GetDataById(Guid applicationId)
        {
            var result = _serviceApplicationService.GetApplicationInformationDataById(applicationId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功",result);
        }

        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetServiceApplicationById")]
        [HttpGet]
        public async Task<JsonResult> GetServiceApplicationById(Guid Id)
        {
            var result = await _serviceApplicationService.GetAsync(Id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        /// <summary>
        /// 查询服务申请信息集合
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetApplicationList")]
        [HttpGet]
        public  JsonResult GetApplicationData(ServiceResultDto dto, int pageIndex = 1, int pageSize = 10)
        {
            int total;
            var result = _serviceApplicationService.GetServiceResult(dto,pageIndex,pageSize,out total);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功",new ListModel<ApplicationInformationDto> { Total = total, List= result } );
        }

        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _serviceApplicationService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该数据不存在");
            }
            await _serviceApplicationService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 服务申请总数(pc和app端) 
        /// </summary>
        /// <param name="streetId"></param>
        /// <param name="postStationId"></param>
        /// <param name="applicationSource">0-pc端,1-app端</param>
        /// <returns></returns>
        [Route("OnlineApplication")]
        [HttpGet]
        public JsonResult OnlineApplicationPc(Guid? streetId, Guid? postStationId, int applicationSource)
        {
            var result = _serviceApplicationService.GetCount(streetId, postStationId, applicationSource);            
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 服务申请总数(未接收)
        /// </summary>
        /// <param name="streetId"></param>
        /// <param name="postStationId"></param>
        /// <returns></returns>
        [Route("ApplicationReceiveCount")]
        [HttpGet]
        public JsonResult ApplicationReceiveCount(Guid? streetId, Guid? postStationId)
        {
            var result = _serviceApplicationService.GetApplicationCount(streetId, postStationId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        /// <summary>
        /// 服务结果登记总数(未登记)
        /// </summary>
        /// <param name="streetId"></param>
        /// <param name="postStationId"></param>
        /// <returns></returns>
        [Route("ServiceResultCount")]
        [HttpGet]
        public JsonResult ServiceResultCount(Guid? streetId, Guid? postStationId)
        {
            var result = _serviceApplicationService.GetServiceResultCount(streetId, postStationId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 今日申请最多服务(PC端和APP端申请最多的服务类型)
        /// </summary>
        /// <param name="streetId"></param>
        /// <param name="postStationId"></param>
        /// <param name="applicationSource">0-pc端,1-app端</param>
        /// <returns></returns>
        [Route("GetServiceType")]
        [HttpGet]
        public JsonResult GetServiceType(Guid? streetId, Guid? postStationId,int applicationSource)
        {
            var result = _serviceApplicationService.GetServiceType(streetId, postStationId,applicationSource);
            if (result!=default(Guid))
            {
                ReceptionServiceDto dto=  _receptionService.Get(result);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", dto);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功","1");
        }


        /// <summary>
        /// 通过ID修改评论状态
        /// </summary>
        /// <returns></returns>
        [Route("isComment")]
        [HttpPost]
        public async Task<JsonResult> isComment(Guid id)
        {
            var info = await _serviceApplicationService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该数据不存在");
            }
            info.Type = 1;
            await _serviceApplicationService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
        }
    }
}