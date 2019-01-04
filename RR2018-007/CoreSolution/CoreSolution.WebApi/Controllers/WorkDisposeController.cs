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
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.WorkDispose;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/WorkDispose")]
    public class WorkDisposeController : Controller
    {

        private readonly IWorkDisposeService _iWorkDisposeService;
        private readonly IResidentWorkService _residentWorkService;

        public WorkDisposeController(IWorkDisposeService iWorkDisposeService, IResidentWorkService residentWorkService)
        {
            _iWorkDisposeService = iWorkDisposeService;
            _residentWorkService = residentWorkService;

        }

        /// <summary>
        /// 事项预约 （200添加成功 ；400必填项不能为空）
        /// </summary>
        /// <param name="inputWorkDisposeModel">事项预约参数model</param>
        ///<returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputWorkDisposeModel inputWorkDisposeModel)
        {
            if (string.IsNullOrWhiteSpace(inputWorkDisposeModel.ShiMinYunId))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "未登录");
            }
            if (string.IsNullOrWhiteSpace(inputWorkDisposeModel.UserName))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "预约人姓名不能为空");
            }
            else if (string.IsNullOrWhiteSpace(inputWorkDisposeModel.IdCard))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "身份证号不能为空");
            }
            else if (inputWorkDisposeModel.YuYueTime == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "预约时间不能为空");
            }

            //var workDisposeDto = Mapper.Map<InputWorkDisposeModel, WorkDisposeDto>(inputWorkDisposeModel);
            //获取事项名称
            var residentWork = await _residentWorkService.GetAsync(inputWorkDisposeModel.ResidentWorkId);
            var workDisposeDto = new WorkDisposeDto
            {
                ResidentWorkId = inputWorkDisposeModel.ResidentWorkId,
                ResidentWorkName = residentWork.ResidentWorkName,
                UserName = inputWorkDisposeModel.UserName,
                IdCard = inputWorkDisposeModel.IdCard,
                Address = inputWorkDisposeModel.Address,
                Phone = inputWorkDisposeModel.Phone,
                Remarks = inputWorkDisposeModel.Remarks,
                StatusCode = "WeiChuLi",
                YuYueTime = inputWorkDisposeModel.YuYueTime,

                StreetName = inputWorkDisposeModel.StreetName,
                StreetId = inputWorkDisposeModel.StreetId,
                PostStationName = inputWorkDisposeModel.PostStationName,
                PostStationId = inputWorkDisposeModel.PostStationId,
                ShiMinYunId=inputWorkDisposeModel.ShiMinYunId
            };
            var id = await _iWorkDisposeService.InsertAndGetIdAsync(workDisposeDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }


        /// <summary>
        /// 删除单个预约
        /// </summary>
        /// <param name="workDisposeId">预约Id</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete([FromBody]Guid workDisposeId)
        {
            var workDispose = await _iWorkDisposeService.GetAsync(workDisposeId);
            if (workDispose == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该预约不存在");
            }
            workDispose.DeletionTime = DateTime.Now;
            await _iWorkDisposeService.DeleteAsync(workDisposeId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 处理登记 （200成功 404该预约信息不存在）
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [Route("modifyDengJi")]
        [HttpPost]
        public async Task<JsonResult> ModifyDengJi(Guid id, string disposeUser, string disposerResult, DateTime disposeTime)
        {
            var workDisposeDto = await _iWorkDisposeService.GetAsync(id);
            if (workDisposeDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该预约信息不存在");
            }
            //workDisposeDto = Mapper.Map<ModifyWorkDisposeModel, WorkDisposeDto>(modifyWorkDisposeModel);
            workDisposeDto.DisposeResult = disposerResult;
            workDisposeDto.DisposeTime = disposeTime;
            workDisposeDto.DisposeUser = disposeUser;
            workDisposeDto.LastModificationTime = DateTime.Now;
            workDisposeDto.Id = id;
            workDisposeDto.StatusCode = "YiChuLi";
            await _iWorkDisposeService.UpdateAsync(workDisposeDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
        }


        ///// <summary>
        ///// 获取单个预约
        ///// </summary>
        ///// <param name="workDisposeId">预约Id</param>
        ///// <returns></returns>
        //[Route("getWorkDisposeById")]
        //[HttpGet]
        //public async Task<JsonResult> GetWorkDisposeById(Guid workDisposeId)
        //{
        //    var result = await _iWorkDisposeService.GetAsync(workDisposeId);
        //    return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        //}



        /// <summary>
        /// 获取单个预约
        /// </summary>
        /// <param name="workDisposeId">预约Id</param>
        /// <returns></returns>
        [Route("getWorkDisposeById")]
        [HttpGet]
        public JsonResult GetWorkDisposeById(Guid workDisposeId)
        {
            var result = _iWorkDisposeService.GetWorkDisposeById(workDisposeId);
            result.residentWorkType_ds = EnumExtensions.GetDescription((EnumCode.ResidentWorkType)result.residentWorkType);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该信息不存在");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 查询分页
        /// </summary>
        /// <param name="username"></param>
        /// <param name="idcard"></param>
        /// <param name="residentWorkName"></param>
        /// <param name="residentWorkType"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("getWorkDisposekPaged")]
        [HttpGet]
        public async Task<JsonResult> GetWorkDisposekPaged(string username, string idcard, string residentWorkName, string residentWorkType, int pageIndex = 1, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<WorkDispose, bool>> where =
                p =>
                 (string.IsNullOrEmpty(username) || p.UserName.Contains(username)) &&
                 (string.IsNullOrEmpty(idcard) || p.IdCard == idcard) &&
                 (string.IsNullOrEmpty(residentWorkName) || p.ResidentWorkName.Contains(residentWorkName));

            //Todo 项目分类暂时未加-涉及到多表联查
            var result = await _iWorkDisposeService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputWorkDisposeModel>
            { Total = result.Item1, List = Mapper.Map<IList<OutputWorkDisposeModel>>(result.Item2) });
        }



        /// <summary>
        /// 查询分页（获取预约列表）
        /// </summary>
        /// <param name="username">预约人姓名</param>
        /// <param name="idcard">身份证号码</param>
        /// <param name="residentWorkName">事项名称</param>
        /// <param name="residentWorkType">事项类别</param>
        /// <param name="statusCode">状态</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数</param>
        /// <returns></returns>

        [Route("getWorkDisposekPagedNew")]
        [HttpGet]
        public JsonResult GetWorkDisposekPagedNew(string shiminyunid,Guid? streetid, Guid? stationid, string username, string idcard, string residentWorkName, string statuscode, int residentWorkType, DateTime? creationTime_start, DateTime? creationTime_end, int pageIndex = 1, int pageSize = 10)
        {
            int total = 0;
            var list = _iWorkDisposeService.GetWorkDisposes(shiminyunid,streetid, stationid, username, idcard, residentWorkName, statuscode, residentWorkType, creationTime_start, creationTime_end, out total, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new { list, total });
        }


        /// <summary>
        /// 网上预约数量（待办任务）
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="stationid">驿站id</param>
        /// <returns></returns>
        [Route("getWorkDisposeCount")]
        [HttpGet]
        public JsonResult GetWorkDisposeCount(Guid? streetid, Guid? stationid)
        {
            Expression<Func<WorkDispose, bool>> where =
                  p =>
                 (streetid == null || p.StreetId == streetid) &&
                 (stationid == null || p.PostStationId == stationid) &&
                 (p.StatusCode == "WeiChuLi");
            var count = _iWorkDisposeService.GetWorkDisposeCount(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", count);
        }

        /// <summary>
        /// 网上预约今日数量（动态监控）
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="stationid">驿站id</param>
        /// <returns></returns>
        [Route("getWorkDisposeCountByDay")]
        [HttpGet]
        public JsonResult GetWorkDisposeCountByDay(Guid? streetid, Guid? stationid)
        {
            DateTime nowtime = DateTime.Now.Date;
            Expression<Func<WorkDispose, bool>> where =
                  p =>
                 (streetid == null || p.StreetId == streetid) &&
                 (stationid == null || p.PostStationId == stationid) &&
                 (p.StatusCode == "WeiChuLi") &&
                 (p.CreationTime >= nowtime);
            var count = _iWorkDisposeService.GetWorkDisposeCount(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", count);
        }


    }
}