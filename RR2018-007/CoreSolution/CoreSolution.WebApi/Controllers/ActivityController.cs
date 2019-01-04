using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models.Activity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using AutoMapper;
using CoreSolution.WebApi.Models;
using Microsoft.AspNetCore.Cors;
using System.Linq.Expressions;
using CoreSolution.Domain.Entities;
using CoreSolution.Tools;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Activity")]
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IConferenceRoomService _conferenceRoomService;
        private readonly IActivityEvaluationService _activityEvaluationService;
        public ActivityController(IActivityService activityService, IConferenceRoomService conferenceRoomService, IActivityEvaluationService activityEvaluationService)
        {
            _activityService = activityService;
            _conferenceRoomService = conferenceRoomService;
            _activityEvaluationService = activityEvaluationService;
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
            var result = await _activityService.GetPagedAsync(i => true, i => i.CreationTime, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityModel>>(result.Item2) });
        }

        /// <summary>
        /// 获取活动信息列表根据传递不同的参数
        /// </summary>
        /// <param name="SAM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetDataByName")]
        [HttpGet]
        public async Task<JsonResult> GetListByParam(SearchActivityModel SAM, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<Activity, bool>> where = p =>
                   (string.IsNullOrEmpty(SAM.activityName) || p.ActivityName.Contains(SAM.activityName)) &&
                   (SAM.type == null || p.ActivityType == SAM.type) && 
                   (SAM.startDate == null || (p.StartTime >= SAM.startDate && p.EndTime <= SAM.endDate)) && 
                   (SAM.streetId == null || p.Street == SAM.streetId) && 
                   (SAM.PostStationId == null || p.PostStation == SAM.PostStationId) && 
                   (SAM.activeState == null || p.ActiveState == SAM.activeState);
            var result = await _activityService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityModel>>(result.Item2) });
        }
        /// <summary>
        /// 获取开展中活动数据
        /// </summary>
        /// <param name="SAM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetBmzData")]
        [HttpGet]
        public async Task<JsonResult> GetBmzListByParam(SearchActivityModel SAM, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<Activity, bool>> where = PredicateExtensions.True<Activity>();
            if (SAM.activityName != null)
            {
                where = where.And(p => p.ActivityName.Contains(SAM.activityName));
            }
            if (SAM.type != null)
            {
                where = where.And(p => p.ActivityType == SAM.type);
            }
            if (SAM.startDate != null && SAM.endDate != null)
            {
                where = where.And(p => p.StartTime >= SAM.startDate && p.EndTime <= SAM.endDate);
            }
            if (SAM.streetId != null)
            {
                where = where.And(p => p.Street == SAM.streetId);
            }
            if (SAM.PostStationId != null)
            {
                where = where.And(p => p.PostStation == SAM.PostStationId);
            }
            if (SAM.activeState != null && SAM.activeState == 2)
            {
                where = where.And(p => p.ActiveState == 2);
            }
            where = where.And(p => (p.StartTime > DateTime.Now&&p.ActivityCycleType==1 || p.ActivityCycleType == 2));

            var result = await _activityService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityModel>>(result.Item2) });
        }
        /// <summary>
        /// 获取开展中活动数据
        /// </summary>
        /// <param name="SAM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetHdkzData")]
        [HttpGet]
        public async Task<JsonResult> GetHdkzListByParam(SearchActivityModel SAM, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<Activity, bool>> where = PredicateExtensions.True<Activity>();
            if (SAM.activityName != null)
            {
                where = where.And(p => p.ActivityName.Contains(SAM.activityName));
            }
            if (SAM.type != null)
            {
                where = where.And(p => p.ActivityType == SAM.type);
            }
            if (SAM.startDate != null && SAM.endDate != null)
            {
                where = where.And(p => p.StartTime >= SAM.startDate && p.EndTime <= SAM.endDate);
            }
            if (SAM.streetId != null)
            {
                where = where.And(p => p.Street == SAM.streetId);
            }
            if (SAM.PostStationId != null)
            {
                where = where.And(p => p.PostStation == SAM.PostStationId);
            }
            if (SAM.activeState != null && SAM.activeState == 3)
            {
                where = where.And(p => p.ActiveState == 3);
            }
            if (SAM.activeState == null)
            {
                where = where.And(p => DateTime.Now >= p.StartTime && DateTime.Now <= p.EndTime && p.ActiveState == 2);
            }

            var result = await _activityService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityModel>>(result.Item2) });
        }

        /// <summary>
        /// 获取待归档数据
        /// </summary>
        /// <param name="SAM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetBeArchivedData")]
        [HttpGet]
        public async Task<JsonResult> GetDgdListByParam(SearchActivityModel SAM, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<Activity, bool>> where = PredicateExtensions.True<Activity>();
            if (SAM.activityName != null)
            {
                where = where.And(p => p.ActivityName.Contains(SAM.activityName));
            }
            if (SAM.type != null)
            {
                where = where.And(p => p.ActivityType == SAM.type);
            }
            if (SAM.streetId != null)
            {
                where = where.And(p => p.Street == SAM.streetId);
            }
            if (SAM.PostStationId != null)
            {
                where = where.And(p => p.PostStation == SAM.PostStationId);
            }
            if (SAM.startDate != null && SAM.endDate != null)
            {
                where = where.And(p => p.StartTime >= SAM.startDate && p.EndTime <= SAM.endDate);
            }
            where = where.And(p => DateTime.Now >= p.EndTime && p.ActiveState == 2);
            var result = await _activityService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityModel>>(result.Item2) });
        }

        /// <summary>
        /// 获取已归档数据
        /// </summary>
        /// <param name="SAM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetYgdData")]
        [HttpGet]
        public async Task<JsonResult> GetYgdListByParam(SearchActivityModel SAM, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<Activity, bool>> where = PredicateExtensions.True<Activity>();
            if (SAM.activityName != null)
            {
                where = where.And(p => p.ActivityName.Contains(SAM.activityName));
            }
            if (SAM.type != null)
            {
                where = where.And(p => p.ActivityType == SAM.type);
            }
            if (SAM.streetId != null)
            {
                where = where.And(p => p.Street == SAM.streetId);
            }
            if (SAM.PostStationId != null)
            {
                where = where.And(p => p.PostStation == SAM.PostStationId);
            }
            if (SAM.startDate != null && SAM.endDate != null)
            {
                where = where.And(p => p.StartTime >= SAM.startDate && p.EndTime <= SAM.endDate);
            }
            where = where.And(p => p.ActiveState == 5);
            var result = await _activityService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            if (SAM.Satisfaction != null)
            {
                List<ActivityDto> newList = new List<ActivityDto>();
                foreach (ActivityDto item in result.Item2)
                {
                    var aEResult = await _activityEvaluationService.GetAllListAsync(p => p.ActivityId == item.Id);
                    if (aEResult.Count() > 0)
                    {
                        int sumScore = aEResult.Sum(p => p.Score);
                        int count = aEResult.Count();
                        int a = Convert.ToInt32(sumScore / count);
                        if (SAM.Satisfaction == a)
                        {
                            newList.Add(item);
                        }
                    }
                }
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityModel>>(newList) });
            }

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityModel>>(result.Item2) });
        }
        /// <summary>
        /// 获取服务信息列表(浏览量排前五)
        /// </summary>
        /// <param name="streetid"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetListByBv")]
        [HttpGet]
        public async Task<JsonResult> GetListByBv(Guid streetid, int pageIndex = 1, int pageSize = 3)
        {
            var result = await _activityService.GetPagedAsync(i => i.Street == streetid, i => i.BrowsingVolume, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityModel>>(result.Item2) });
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputActivityModel inputactivityModel)
        {
            if (inputactivityModel.ActivityName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "活动名称不能为空");
            }
            var activityDto = new ActivityDto
            {
                Id = Guid.Parse(inputactivityModel.Id.ToString()),
                ActivityName = inputactivityModel.ActivityName,
                ActivityType = (int)inputactivityModel.ActivityType,
                HostUnit = inputactivityModel.HostUnit,
                MeetingRoom = inputactivityModel.MeetingRoom,
                PersonCharge = inputactivityModel.PersonCharge,
                ContactNumber = inputactivityModel.ContactNumber,
                ActivityAddress = inputactivityModel.ActivityAddress,
                ActivityCycleType = inputactivityModel.ActivityCycleType,
                StartTime = inputactivityModel.StartTime,
                EndTime = inputactivityModel.EndTime,
                ActivityTimeDesc = inputactivityModel.ActivityTimeDesc,
                Street = inputactivityModel.Street,
                StreetName = inputactivityModel.StreetName,
                PostStation = inputactivityModel.PostStation,
                PostStationName = inputactivityModel.PostStationName,
                ExpectedNumberParticipants = inputactivityModel.ExpectedNumberParticipants,
                DetailsActivities = inputactivityModel.DetailsActivities,
                ActivityImg = inputactivityModel.ActivityImg,
                AttachmentPath = inputactivityModel.AttachmentPath,
                ActiveState = 1  //待审核
            };
            await _activityService.InsertAsync(activityDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputActivityModel inputactivityModel)
        {
            if (inputactivityModel.ActivityName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "活动名称不能为空");
            }
            var info = await _activityService.GetAsync(Guid.Parse(inputactivityModel.Id.ToString()));
            info.ActivityName = inputactivityModel.ActivityName;
            info.ActivityType = (int)inputactivityModel.ActivityType;
            info.HostUnit = inputactivityModel.HostUnit;
            info.MeetingRoom = inputactivityModel.MeetingRoom;
            info.PersonCharge = inputactivityModel.PersonCharge;
            info.ContactNumber = inputactivityModel.ContactNumber;
            info.ActivityAddress = inputactivityModel.ActivityAddress;
            info.StartTime = inputactivityModel.StartTime;
            info.EndTime = inputactivityModel.EndTime;
            info.ActivityTimeDesc = inputactivityModel.ActivityTimeDesc;
            info.Street = inputactivityModel.Street;
            info.StreetName = inputactivityModel.StreetName;
            info.PostStation = inputactivityModel.PostStation;
            info.PostStationName = inputactivityModel.PostStationName;
            info.ExpectedNumberParticipants = inputactivityModel.ExpectedNumberParticipants;
            info.DetailsActivities = inputactivityModel.DetailsActivities;
            if (inputactivityModel.UpdateMark=="1")
            {
                info.ActiveState = 1;
            }
            info.ActivityImg = inputactivityModel.ActivityImg;
            info.AttachmentPath = inputactivityModel.AttachmentPath;

            await _activityService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetActivityById")]
        [HttpGet]
        public async Task<JsonResult> GetActivityById(Guid id)
        {
            var result = await _activityService.GetAsync(id);
            if (result.MeetingRoom != default(Guid))
            {
                var room = await _conferenceRoomService.GetAsync(result.MeetingRoom);
                result.MeetingRoomName = room.ConferenceRoomName;
            }
            else
            {
                result.MeetingRoomName = "";
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
            var info = await _activityService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该活动信息不存在");
            }
            await _activityService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("editLLL")]
        [HttpPost]
        public async Task<JsonResult> EditBrowsingVolume(Guid id)
        {
            var result = await _activityService.GetAsync(id);
            int? lLL = result.BrowsingVolume;
            if (lLL == null)
            {
                lLL = 0;
            }

            var activityDto = new ActivityDto
            {
                Id = id,
                BrowsingVolume = lLL + 1
            };
            await _activityService.UpdateAsync(activityDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功", id);
        }

        /// <summary>
        /// 审核活动信息
        /// </summary>
        /// <returns></returns>
        [Route("Aduit")]
        [HttpPost]
        public async Task<JsonResult> IsAduit(InputActivityModel inputActivityModel)
        {
            DateTime topDate = default(DateTime);
            int? flag = 0;
            int? state;
            if (inputActivityModel.Flag != null)
            {
                flag = inputActivityModel.Flag;
                topDate = DateTime.Now;
            }
            if (inputActivityModel.AduitIsPass == 1)
            {
                state = 2;
            }
            else
            {
                state = 0;
            }
            var info = await _activityService.GetAsync(Guid.Parse(inputActivityModel.Id.ToString()));
            info.AduitIsPass = inputActivityModel.AduitIsPass;
            info.AduitRemarks = inputActivityModel.AduitRemarks;
            info.AduitDate = inputActivityModel.AduitDate;
            info.Auditor = inputActivityModel.Auditor;
            info.ActiveState = state;
            info.Flag = flag;
            info.TopDate = topDate;
            await _activityService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 归档活动信息
        /// </summary>
        /// <returns></returns>
        [Route("IsGuiDang")]
        [HttpPost]
        public async Task<JsonResult> IsGuiDang(InputActivityModel inputActivityModel)
        {
            var info = await _activityService.GetAsync(Guid.Parse(inputActivityModel.Id.ToString()));
            info.IsGuiDang = inputActivityModel.IsGuiDang;
            info.ArchivalRemark = inputActivityModel.ArchivalRemark;
            info.Archiving = inputActivityModel.Archiving;
            info.FilingDate = inputActivityModel.FilingDate;
            //如果是归档改变状态值
            if (inputActivityModel.IsGuiDang == 1)
            {
                info.ActiveState = 5;
            }
            await _activityService.UpdateAsync(info);
            if (inputActivityModel.IsGuiDang == 1)
            {
                var result = await _activityService.GetAsync(Guid.Parse(inputActivityModel.Id.ToString()));
                if (result.ActivityCycleType == 2)
                {
                    //长期活动在活动表中新增一条活动信息
                    var activityDto1 = new ActivityDto
                    {
                        ActivityName = result.ActivityName,
                        ActivityType = (int)result.ActivityType,
                        HostUnit = result.HostUnit,
                        PersonCharge = result.PersonCharge,
                        ContactNumber = result.ContactNumber,
                        ActivityAddress = result.ActivityAddress,
                        ActivityCycleType = result.ActivityCycleType,
                        StartTime = result.StartTime,
                        EndTime = result.EndTime,
                        ActivityTimeDesc = result.ActivityTimeDesc,
                        Street = result.Street,
                        StreetName = result.StreetName,
                        PostStation = result.PostStation,
                        PostStationName = result.PostStationName,
                        ExpectedNumberParticipants = result.ExpectedNumberParticipants,
                        DetailsActivities = result.DetailsActivities,
                        ActivityImg = result.ActivityImg,
                        AttachmentPath = result.AttachmentPath,
                        ActiveState = 2
                    };
                    await _activityService.InsertAsync(activityDto1);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 重新申请
        /// </summary>
        /// <returns></returns>
        [Route("editState")]
        [HttpPost]
        public async Task<JsonResult> EditState(InputActivityModel inputActivityModel)
        {
            var info = await _activityService.GetAsync(Guid.Parse(inputActivityModel.Id.ToString()));
            if (inputActivityModel.ActiveState==5)
            {
                info.IsGuiDang = 1;
                info.ArchivalRemark = inputActivityModel.ArchivalRemark;
                info.Archiving = inputActivityModel.Archiving;
                info.FilingDate =DateTime.Now;
            }
            info.ActiveState = inputActivityModel.ActiveState;
            await _activityService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 分页获取活动信息列表。200获取成功
        /// </summary>
        /// <returns></returns>
        [Route("getCount")]
        [HttpGet]
        public async Task<JsonResult> GetListCount(Guid streetId,Guid postStationId)
        {
            Expression<Func<Activity, bool>> where = PredicateExtensions.True<Activity>();
            if (streetId != default(Guid)) {
                where = where.And(p => p.Street == streetId);
            };
            if (postStationId != default(Guid))
            {
                where = where.And(p => p.PostStation == postStationId);
            };
            var result = await _activityService.GetAllListAsync(where);
            int bmzCount = 0;
            int dshCount = result.Count(p => p.ActiveState == 1);
            int bmzCount1 = result.Count(p => p.ActiveState == 2 && p.StartTime > DateTime.Now&&p.ActivityCycleType==1);//单次活动报名中数量
            int bmzCount2= result.Count(p => p.ActiveState == 2  && p.ActivityCycleType == 2);//长期活动活动报名中数量
            bmzCount = bmzCount1 + bmzCount2;
            int kzzCount = result.Count(p => p.ActiveState == 3 || (DateTime.Now >= p.StartTime && DateTime.Now <= p.EndTime && p.ActiveState == 2));//开展中
            int dgdCount = result.Count(p => DateTime.Now > p.EndTime && p.ActiveState == 2);//待归档
            int ygdCount = result.Count(p => p.ActiveState == 5);
            string str = dshCount + "," + bmzCount + "," + kzzCount + "," + dgdCount + "," + ygdCount;
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", str);
        }

        /// <summary>
        /// 获取待审核数据
        /// </summary>
        /// <param name="streetId"></param>
        /// <param name="postStationId"></param>
        /// <returns></returns>
        [Route("GetDshList")]
        [HttpGet]
        public async Task<JsonResult> GetDshList(Guid? streetId,Guid? postStationId)
        {           
            Expression<Func<Activity, bool>> where = p =>         
                  (streetId == null || p.Street == streetId) && (postStationId == null || p.PostStation == postStationId) && (p.ActiveState ==1);
            var result = await _activityService.GetAllListAsync(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功",result);
        }
    }
}