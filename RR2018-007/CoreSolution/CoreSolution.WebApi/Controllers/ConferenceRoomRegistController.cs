using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.IService;
using CoreSolution.WebApi.Models.ConferenceRoomRegist;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Domain.Entities;
using CoreSolution.Tools.WebResult;
using System.Net;
using AutoMapper;
using CoreSolution.Dto;
using CoreSolution.Tools.Extensions;
using CoreSolution.WebApi.Models;
using System.Linq.Expressions;
using CoreSolution.Tools;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ConferenceRoomRegist")]
    public class ConferenceRoomRegistController : ControllerBase
    {
        private readonly IConferenceRoomRegistService _cRRService;
        private readonly IConferenceRoomService _conferenceRoomService;
        private readonly IDataDictionaryService _dataDictionaryService;
        public ConferenceRoomRegistController(IConferenceRoomRegistService cRRService, IConferenceRoomService conferenceRoomService, IDataDictionaryService dataDictionaryService)
        {
            _cRRService = cRRService;
            _conferenceRoomService = conferenceRoomService;
            _dataDictionaryService = dataDictionaryService;
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
        public  JsonResult GetListData(SearchConferenceRoomRegistModel SRSM, int pageIndex = 1, int pageSize = 10)
        {
            DateTime startWeek = default(DateTime);
            DateTime endWeek = default(DateTime);
            //拼接过滤条件
            Expression<Func<ConferenceRoomRegist, bool>> where = PredicateExtensions.True<ConferenceRoomRegist>();
            if (SRSM.StartDate==default(DateTime))
            {
                DateTime dt = DateTime.Now.Date;
                startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).Date;  
                endWeek = startWeek.AddDays(6).Date;
                where = where.And(p => p.StartDate>=startWeek&&p.StartDate<=endWeek);
            }
            else
            {
                if (SRSM.Type==1)
                {
                    DateTime dt = Convert.ToDateTime(SRSM.StartDate).AddDays(-3).Date;
                    startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).Date;
                    endWeek = startWeek.AddDays(6).Date;
                }
                else
                {
                    DateTime dt = Convert.ToDateTime(SRSM.StartDate).AddDays(1).Date;
                    startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d"))).Date;
                    endWeek = startWeek.AddDays(6).Date;
                }                
            }
            //首先查询出会议室集合
            Expression<Func<ConferenceRoom, bool>> where1 = PredicateExtensions.True<ConferenceRoom>();
            if (SRSM.streetId != default(Guid)) {
                where1= where1.And(p => p.StreetId == SRSM.streetId);
             }
            if (SRSM.postStationId != default(Guid))
            {
                where1= where1.And(p => p.PostStation == SRSM.postStationId);
            }
            List<ConferenceRoomDto> roomList =  _conferenceRoomService.GetAllList(where1);
            var returnList= new List<MeetingDto>();
            foreach (var item in roomList)
            {
                //遍历每一个会议室当前被申请的信息
                List<ConferenceRoomRegistDto> roomRegisterlist=  _cRRService.GetAllList(p => p.ConferenceRoomId == item.Id);
                var newList = new List<ConferenceRoomRegistDto>();             
                for (DateTime i = startWeek; i <= endWeek;)
                {
                    var reslut1 = roomRegisterlist.Where(p => p.StartDate.Date == i && p.TimeStamp == 1).FirstOrDefault();
                    var newDto1 = new ConferenceRoomRegistDto { ObjectIsNull = 2, RecordDt = i.ToString("yyyy-MM-dd") };
                    if (reslut1==null)
                    {
                        newDto1.Remarks = "1";
                        newList.Add(newDto1);
                    }
                    else
                    {
                        newList.Add(reslut1);
                    }
                    var result2 = roomRegisterlist.Where(p => p.StartDate.Date == i && p.TimeStamp == 2).FirstOrDefault();
                    var newDto = new ConferenceRoomRegistDto { ObjectIsNull = 2, RecordDt = i.ToString("yyyy-MM-dd") };
                    if (result2==null)
                    {
                        newDto.Remarks = "2";
                        newList.Add(newDto);
                    }
                    else
                    {
                        newList.Add(result2);
                    }
                    i = i.AddDays(1);
                }
                var listDto = new MeetingDto {
                    ConferenceRoomId=item.Id,
                    ConferenceRoomName=item.ConferenceRoomName,
                    Pedestal=item.Pedestal,
                    ConferenceRoomRegists= newList
                };
                returnList.Add(listDto);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", returnList);
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputModel inputModel)
        {
            if (inputModel.ConferenceTheme.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "会议/活动主题不能为空");
            }
            if (inputModel.EndDate.Hour<13) {
                inputModel.TimeStamp = 1;
            }
            else if (inputModel.StartDate.Hour>=13)
            {
                inputModel.TimeStamp = 2;
            }
            else if (inputModel.StartDate.Hour<13&&inputModel.EndDate.Hour>=13)
            {
                inputModel.TimeStamp = 1;
            }
            var conferenceRoomRegistDto = Mapper.Map<ConferenceRoomRegistDto>(inputModel);
            await _cRRService.InsertAsync(conferenceRoomRegistDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputModel inputModel,Guid id)
        {
            if (inputModel.ConferenceTheme.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "会议/活动主题不能为空");
            }
            var info = await _cRRService.GetAsync(id);
            if (inputModel.EndDate.Hour < 13)
            {
                inputModel.TimeStamp = 1;
            }
            else if (inputModel.StartDate.Hour >= 13)
            {
                inputModel.TimeStamp = 2;
            }
            else if (inputModel.StartDate.Hour < 13 && inputModel.EndDate.Hour >=13)
            {
                inputModel.TimeStamp = 1;
            }
            info = Mapper.Map<ConferenceRoomRegistDto>(inputModel);
            info.Id = id;
            await _cRRService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        [Route("GetDate")]
        [HttpGet]
        public JsonResult GetDate(string date,int type=0)
        {
            DateTime dt = default(DateTime);
            if (!string.IsNullOrEmpty(date))
            {
                if (type==1)
                {
                    //上一周
                    dt = Convert.ToDateTime(date).AddDays(-3).Date;
                }
                else if (type==2)
                {
                    //下一周
                    dt = Convert.ToDateTime(date).AddDays(1).Date;
                }                         
            }
            else
            {
                dt = DateTime.Now.Date;
            }
            DateTime startWeek = dt.AddDays(1 - Convert.ToInt32(dt.DayOfWeek.ToString("d")));  //本周周一
            DateTime endWeek = startWeek.AddDays(6);  //本周周日
            List<string> list = new List<string>();
            string dt1= startWeek.GetDateTimeFormats('D')[0].ToString();//年月日
            string dt2 = endWeek.GetDateTimeFormats('D')[0].ToString();//年月日
            list.Add(dt1);
            list.Add(dt2);
            string str = "";
            for (DateTime  i = startWeek; i <=endWeek ;)
            {               
               str=str+i.GetDateTimeFormats('D')[0].ToString().Split('年')[1]+",";
               i = i.AddDays(1);
            }
            list.Add(str.TrimEnd(','));
            return AjaxHelper.JsonResult(HttpStatusCode.OK,"",list);            
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDataById")]
        [HttpGet]
        public async Task<JsonResult> GetDataById(Guid id)
        {
            var result = await _cRRService.GetAsync(id);
            if (result.ConferenceType != default(Guid))
            {
                result.ConferenceTypeName = _dataDictionaryService.GetDataName(result.ConferenceType);
            }
            else
            {
                result.ConferenceTypeName = "";
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
            var info = await _cRRService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该数据不存在");
            }
            await _cRRService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
    }
}