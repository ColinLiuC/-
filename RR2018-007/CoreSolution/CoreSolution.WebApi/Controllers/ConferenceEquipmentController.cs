using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ConferenceEquipment;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Dto;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ConferenceEquipment")]
    public class ConferenceEquipmentController : ControllerBase
    {
        private readonly IConferenceEquipmentService _conferenceEquipmentService;

        public ConferenceEquipmentController(IConferenceEquipmentService conferenceEquipmentService)
        {
            _conferenceEquipmentService = conferenceEquipmentService;
        }
        /// <summary>
        /// 获取活动信息列表根据传递不同的参数
        /// </summary>
        /// <param name="roomId">会议室Id</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetListData")]
        [HttpGet]
        public async Task<JsonResult> GetListByParam(Guid roomId, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<ConferenceEquipment, bool>> where = p =>
                   ( p.ConferenceRoomId==roomId);
            var result = await _conferenceEquipmentService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputModel> { Total = result.Item1, List = Mapper.Map<IList<OutputModel>>(result.Item2) });
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputModel inputconferenceRoomModel)
        {
            if (inputconferenceRoomModel.EquipmentName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "活动名称不能为空");
            }
            var conferenceRoomDto = Mapper.Map<ConferenceEquipmentDto>(inputconferenceRoomModel);
            var id= await _conferenceEquipmentService.InsertAndGetIdAsync(conferenceRoomDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功",id);
        }
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputModel inputconferenceRoomModel)
        {
            var info = await _conferenceEquipmentService.GetAsync(Guid.Parse(inputconferenceRoomModel.Id.ToString()));
            info.EquipmentName = inputconferenceRoomModel.EquipmentName;
            info.Count = inputconferenceRoomModel.Count;
            info.Description = inputconferenceRoomModel.Description;        
            await _conferenceEquipmentService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDataById")]
        [HttpGet]
        public async Task<JsonResult> GetConferenceEquipmentById(Guid id)
        {
            var result = await _conferenceEquipmentService.GetAsync(id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _conferenceEquipmentService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "设备不存在");
            }
            info.DeletionTime = DateTime.Now;
            await _conferenceEquipmentService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
    }
}