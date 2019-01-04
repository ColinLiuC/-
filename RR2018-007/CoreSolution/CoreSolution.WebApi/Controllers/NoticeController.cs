using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models.Notice;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;

using System.Diagnostics;
using CoreSolution.Domain.Enum;
using CoreSolution.WebApi.Interceptor;
using Microsoft.AspNetCore.Cors;
using CoreSolution.WebApi.Models;
using AutoMapper;
using System.Linq.Expressions;
using CoreSolution.Domain.Entities;
using CoreSolution.Tools;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Notice")]
    public class NoticeController : Controller
    {
        private readonly INoticeService _noticeService;
        public NoticeController(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }
        /// <summary>
        /// 查询符合条件的数据
        /// </summary>
        /// <returns></returns>
        [Route("index")]
        [HttpPost]
        public async Task<JsonResult> Index(NoticeDto noticeDto,Guid? streetId, string stime, string etime, int pageIndex = 1, int pageSize = 10)
        {
            //var model = await _noticeService.GetNoticePagedAsync(noticeDto, pageIndex, pageSize);
            Expression<Func<Notice, bool>> where = p =>
               (string.IsNullOrEmpty(noticeDto.NoticeTitle) || p.NoticeTitle.Contains(noticeDto.NoticeTitle)) &&
               (string.IsNullOrEmpty((noticeDto.NoticeState).ToString()) || p.NoticeState == noticeDto.NoticeState) &&
               (streetId == null || p.StreetId == streetId) &&
               (string.IsNullOrEmpty(stime) || DateTime.Parse(stime) < p.NoticeTime) &&
               (string.IsNullOrEmpty(etime) || DateTime.Parse(etime) > p.NoticeTime);
            var model = await _noticeService.GetPagedAsync(where, i => i.Id, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputNoticeModel> { Total = model.Item1, List = Mapper.Map<IList<OutputNoticeModel>>(model.Item2) });
        }

        /// <summary>
        /// 查询街道所有公告
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("GetAll")]
        [HttpPost]
        public async Task<JsonResult> GetAll(Guid StreetId)
        {
            Expression<Func<Notice, bool>> where = p =>
               (string.IsNullOrEmpty(StreetId.ToString()) || p.StreetId == StreetId)&&
               (p.NoticeState==0);
            var list = await _noticeService.GetAllListAsync(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", list);
        }

        /// <summary>
        /// 查询街道所有公告2
        /// </summary>
        /// 
        /// <returns></returns>
        [Route("GetAll2")]
        [HttpPost]
        public async Task<JsonResult> GetAll2(Guid StreetId, int pageIndex = 1, int pageSize = 10, bool isDesc = true)
        {
            Expression<Func<Notice, bool>> where = p =>
               (string.IsNullOrEmpty(StreetId.ToString()) || p.StreetId == StreetId) &&
               (p.NoticeState == 0);
            var list = await _noticeService.GetPagedAsync(where, i => i.NoticeTime, pageIndex, pageSize,isDesc);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputNoticeModel> { Total = list.Item1, List = Mapper.Map<IList<OutputNoticeModel>>(list.Item2) });
        }


        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create( InputNoticeModel inputnoticeModel)
        {
            if (inputnoticeModel.NoticeTitle.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "标题不能为空");
            }
            if (inputnoticeModel.NoticeInfo.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "内容不能为空");
            }
            if (inputnoticeModel.NoticeChannel.ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "发布渠道不能为空");
            }
            if (inputnoticeModel.NoticePeople.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "下发人不能为空");
            }
            if ((inputnoticeModel.NoticeTime).ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "下发时间不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            DateTime time = new DateTime();
            if (inputnoticeModel.NoticeState == 0)
            {
                time = DateTime.Now;
            }
            else
            {
                time = inputnoticeModel.NoticeTime;
            }
            var noticeDto = new NoticeDto
            {
                NoticeTitle = inputnoticeModel.NoticeTitle,
                NoticeInfo = inputnoticeModel.NoticeInfo,
                NoticeChannel = inputnoticeModel.NoticeChannel,
                NoticePeople = inputnoticeModel.NoticePeople,
                NoticeTime = time,
                NoticeState = inputnoticeModel.NoticeState,
                StreetId=Guid.Parse(inputnoticeModel.StreetId.ToString()),
                StreetName=inputnoticeModel.StreetName
            };
            await _noticeService.InsertAsync(noticeDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }

        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetNoticeById")]
        [HttpGet]
        public async Task<JsonResult> GetNoticeById(Guid id)
        {
            var result = await _noticeService.GetAsync(id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit( NoticeDto noticedto)
        {
            if (noticedto.NoticeTitle.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "标题不能为空");
            }
            if (noticedto.NoticeInfo.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "内容不能为空");
            }
            if (noticedto.NoticeChannel.ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "发布渠道不能为空");
            }
            if (noticedto.NoticePeople.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "下发人不能为空");
            }
            if ((noticedto.NoticeTime).ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "下发时间不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            DateTime time = new DateTime();
            if (noticedto.NoticeState == 0)
            {
                time = DateTime.Now;
            }
            else
            {
                time = noticedto.NoticeTime;
            }
            var result = await _noticeService.GetAsync(noticedto.Id);
            result = new NoticeDto
            {
                NoticeTitle = noticedto.NoticeTitle,
                NoticeInfo = noticedto.NoticeInfo,
                NoticeChannel = noticedto.NoticeChannel,
                NoticePeople = noticedto.NoticePeople,
                NoticeTime = time,
                NoticeState = noticedto.NoticeState,
                Id=noticedto.Id,
                StreetId = noticedto.StreetId,
                StreetName = noticedto.StreetName

            }; 
            await _noticeService.UpdateAsync(result);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }


        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _noticeService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该用户不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //info.IsDeleted = true;
            await _noticeService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 获取最近3天的公告数量
        /// </summary>
        /// <returns></returns>
        [Route("GetNoticeNum")]
        [HttpGet]
        public async Task<JsonResult> GetNoticeNum(Guid street,Guid station)
        {
            Expression<Func<Notice, bool>> where = PredicateExtensions.True<Notice>();
            if (street != null && station == null && street != Guid.Parse("00000000-0000-0000-0000-000000000000") && station == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.StreetId == street);
                where = where.And(p => (DateTime.Now - p.NoticeTime).TotalDays <= 3 && p.NoticeState == 0);
            }
            else
            {
                where = where.And(p => (DateTime.Now - p.NoticeTime).TotalDays <= 3 && p.NoticeState == 0);
            }
            var result = await _noticeService.GetAllListAsync(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result.Count);
        }


    }
}