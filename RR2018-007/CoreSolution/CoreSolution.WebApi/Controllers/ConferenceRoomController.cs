using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ConferenceRoom;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Dto;
using Microsoft.AspNetCore.Cors;
using System.Linq.Expressions;
using CoreSolution.Domain.Entities;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ConferenceRoom")]
    public class ConferenceRoomController : ControllerBase
    {
        private readonly IConferenceRoomService _conferenceRoomService;
        private readonly IDataDictionaryService _dataDictionaryService;
        public ConferenceRoomController(IConferenceRoomService conferenceRoomService, IDataDictionaryService dataDictionaryService)
        {
            _conferenceRoomService = conferenceRoomService;
            _dataDictionaryService=dataDictionaryService;
    }
        /// <summary>
        /// 分页获取活动信息列表。200获取成功
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("get")]
        [HttpGet]
        public async Task<JsonResult> GetListData(int pageIndex = 1, int pageSize = 10)
        {
            var result = await _conferenceRoomService.GetPagedAsync(i => true, i => i.CreationTime, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputConferenceRoomModel> { Total = result.Item1, List = Mapper.Map<IList<OutputConferenceRoomModel>>(result.Item2) });
        }

        /// <summary>
        /// 获取活动信息列表根据传递不同的参数
        /// </summary>
        /// <param name="Scr">参数</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetListData")]
        [HttpGet]
        public async Task<JsonResult> GetListByParam(SearchConferenceRoomModel Scr, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<ConferenceRoom, bool>> where = p =>
                   (string.IsNullOrEmpty(Scr.ConferenceRoomName) || p.ConferenceRoomName.Contains(Scr.ConferenceRoomName)) &&
                   (Scr.ConferenceRoomType == default(Guid) || p.ConferenceRoomType ==Scr.ConferenceRoomType) && (Scr.StreetId ==default(Guid) || p.StreetId == Scr.StreetId) && (Scr.PostStation == default(Guid) || p.PostStation == Scr.PostStation) && (Scr.Pedestal==null||p.Pedestal==Scr.Pedestal)&&(Scr.State==null || p.State==Scr.State);
            var result = await _conferenceRoomService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            if (result != null)
            {
                foreach (var item in result.Item2)
                {
                    item.RoomTypeName = _dataDictionaryService.GetItemNameById(item.ConferenceRoomType);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputConferenceRoomModel> { Total = result.Item1, List = Mapper.Map<IList<OutputConferenceRoomModel>>(result.Item2) });
        }
        /// <summary>
        /// 获取所有的会议室设备信息
        /// </summary>
        /// <returns></returns>
        [Route("GetMeetRoomAll")]
        [HttpGet]
        public async Task<JsonResult> GetMeetRoomAll()
        {
            var result = await _conferenceRoomService.GetAllListAsync();
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputConferenceRoomModel> { Total = result.Count, List = Mapper.Map<IList<OutputConferenceRoomModel>>(result) });
        }

        /// <summary>
        /// 通过街道驿站获取所有的会议室设备信息
        /// </summary>
        /// <returns></returns>
        [Route("GetMeetRoomBy")]
        [HttpGet]
        public async Task<JsonResult> GetMeetRoomBy(Guid? StreetId,Guid? StationId)
        {
            Expression<Func<ConferenceRoom, bool>> where = p =>
            (StreetId == null || p.StreetId == StreetId) &&
            (StationId == null || p.PostStation == StationId);
            var result = await _conferenceRoomService.GetAllListAsync(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputConferenceRoomModel> { Total = result.Count, List = Mapper.Map<IList<OutputConferenceRoomModel>>(result) });
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputConferenceRoomModel inputconferenceRoomModel)
        {
            if (inputconferenceRoomModel.ConferenceRoomName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "活动名称不能为空");
            }
            // var conferenceRoomDto = Mapper.Map<ConferenceRoomDto>(inputconferenceRoomModel);
            var conferenceRoomDto = new ConferenceRoomDto
            {
                ConferenceRoomName = inputconferenceRoomModel.ConferenceRoomName,
                ConferenceRoomType = inputconferenceRoomModel.ConferenceRoomType,
                CompetentUnit = inputconferenceRoomModel.CompetentUnit,
                StreetId=inputconferenceRoomModel.StreetId,
                PostStation = inputconferenceRoomModel.PostStation,
                StreetName = inputconferenceRoomModel.StreetName,
                PostStationName = inputconferenceRoomModel.PostStationName,
                PersonInCharge = inputconferenceRoomModel.PersonInCharge,
                ContactPhone = inputconferenceRoomModel.ContactPhone,
                Pedestal = inputconferenceRoomModel.Pedestal,
                Address = inputconferenceRoomModel.Address,
                DetailedDescr = inputconferenceRoomModel.DetailedDescr,
                ImgName = inputconferenceRoomModel.ImgName,
                ImgPath = inputconferenceRoomModel.ImgPath,
                State =inputconferenceRoomModel.State,
                IsCharge = inputconferenceRoomModel.IsCharge
            };
            var id = await _conferenceRoomService.InsertAndGetIdAsync(conferenceRoomDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功",id);
        }
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputConferenceRoomModel inputconferenceRoomModel)
        {
            var info = await _conferenceRoomService.GetAsync(Guid.Parse(inputconferenceRoomModel.Id.ToString()));
            info.ConferenceRoomName = inputconferenceRoomModel.ConferenceRoomName;
                info.ConferenceRoomType = inputconferenceRoomModel.ConferenceRoomType;
            info.CompetentUnit = inputconferenceRoomModel.CompetentUnit;
            info.StreetId = inputconferenceRoomModel.StreetId;
            info.PostStation = inputconferenceRoomModel.PostStation;
            info.StreetName = inputconferenceRoomModel.StreetName;
            info.PostStationName = inputconferenceRoomModel.PostStationName;
            info.PersonInCharge = inputconferenceRoomModel.PersonInCharge;
            info.ContactPhone = inputconferenceRoomModel.ContactPhone;
            info.Pedestal = inputconferenceRoomModel.Pedestal;
            info.Address = inputconferenceRoomModel.Address;
            info.DetailedDescr = inputconferenceRoomModel.DetailedDescr;
            info.ImgName = inputconferenceRoomModel.ImgName;
            info.ImgPath = inputconferenceRoomModel.ImgPath;
            info.State = inputconferenceRoomModel.State;
            info.IsCharge = inputconferenceRoomModel.IsCharge;
            await _conferenceRoomService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetConferenceRoomById")]
        [HttpGet]
        public async Task<JsonResult> GetDataById(Guid id)
        {
            var result = await _conferenceRoomService.GetAsync(id);
            if (result.ConferenceRoomType!=default(Guid))
            {
                result.RoomTypeName = _dataDictionaryService.GetDataName(result.ConferenceRoomType);
            }
            else
            {
                result.RoomTypeName = "";
            }
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
            var info = await _conferenceRoomService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该会议/活动室不存在");
            }
            await _conferenceRoomService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
    }
}