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
using CoreSolution.Dto.MyModel;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.WorkTransact;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/WorkTransact")]
    public class WorkTransactController : Controller
    {
        private readonly IWorkTransactService _iWorkTransactService;
        private readonly IResidentWorkService _residentWorkService;

        public WorkTransactController(IWorkTransactService iWorkTransactService, IResidentWorkService residentWorkService)
        {
            _iWorkTransactService = iWorkTransactService;
            _residentWorkService = residentWorkService;
        }

        /// <summary>
        /// 新增事项办理 （200添加成功 ；400必填项不能为空）
        /// </summary>
        /// <param name="inputWorkTransactModel">事项办理参数model</param>
        ///<returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputWorkTransactModel inputWorkTransactModel)
        {
            if (string.IsNullOrWhiteSpace(inputWorkTransactModel.UserName))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "办理人姓名不能为空");
            }
            else if (string.IsNullOrWhiteSpace(inputWorkTransactModel.IdCard))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "身份证号不能为空");
            }
            //获取事项名称
            var residentWork = await _residentWorkService.GetAsync(inputWorkTransactModel.ResidentWorkId);
            var workTransactDto = new WorkTransactDto
            {
                ResidentWorkId = inputWorkTransactModel.ResidentWorkId,
                ResidentWorkName = residentWork.ResidentWorkName,
                UserName = inputWorkTransactModel.UserName,
                IdCard = inputWorkTransactModel.IdCard,
                Address = inputWorkTransactModel.Address,
                Phone = inputWorkTransactModel.Phone,

                ShouliUser = inputWorkTransactModel.ShouliUser,
                ShouliContent = inputWorkTransactModel.ShouliContent,
                ShouliAddress = inputWorkTransactModel.ShouliAddress,
                ShouliTime = inputWorkTransactModel.ShouliTime,
                StatusCode = "WeiChuLi",
                StreetId = inputWorkTransactModel.StreetId,
                StationId = inputWorkTransactModel.StationId
            };
            var id = await _iWorkTransactService.InsertAndGetIdAsync(workTransactDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }


        /// <summary>
        /// 处理登记 （200成功 404该办理信息不存在）
        /// </summary>
        /// <param name="id"></param>
        /// <param name="disposeUser"></param>
        /// <param name="disposerResult"></param>
        /// <param name="disposeTime"></param>
        /// <returns></returns>
        [Route("modifyDengJi")]
        [HttpPost]
        public async Task<JsonResult> ModifyDengJi(Guid id, string disposeUser, string disposerResult, DateTime disposeTime)
        {
            var workTransactDto = await _iWorkTransactService.GetAsync(id);
            if (workTransactDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该办理信息不存在");
            }
            workTransactDto.DisposeResult = disposerResult;
            workTransactDto.DisposeTime = disposeTime;
            workTransactDto.DisposeUser = disposeUser;
            workTransactDto.LastModificationTime = DateTime.Now;
            workTransactDto.Id = id;
            workTransactDto.StatusCode = "YiChuLi";
            await _iWorkTransactService.UpdateAsync(workTransactDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
        }

        ///// <summary>
        ///// 获取单个办理
        ///// </summary>
        ///// <param name="workTransactid">办理Id</param>
        ///// <returns></returns>
        //[Route("getWorkTransactById")]
        //[HttpGet]
        //public async Task<JsonResult> GetWorkTransactById(Guid workTransactid)
        //{
        //    var result = await _iWorkTransactService.GetAsync(workTransactid);
        //    if (result == null)
        //    {
        //        return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该信息不存在", result);
        //    }
        //    return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        //}

        /// <summary>
        /// 获取单个办理
        /// </summary>
        /// <param name="workTransactid">办理Id</param>
        /// <returns></returns>
        [Route("getWorkTransactById")]
        [HttpGet]
        public JsonResult GetWorkTransactById(Guid workTransactid)
        {
            var result = _iWorkTransactService.GetMyWorkTransactById(workTransactid);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该信息不存在");
            }

            result.lastDeadline = DateTime.Parse(result.workTransact.ShouliTime.Value.ToString("yyyy-MM-dd")).AddDays(CommonHelper.ToInt(result.Deadline));
            result.residentWorkType_ds = EnumExtensions.GetDescription((EnumCode.ResidentWorkType)result.residentWorkType);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        //[Route("getWorkTransactPaged")]
        //[HttpGet]
        //public async Task<JsonResult> GetWorkTransactPaged(string username, string idcard, string residentWorkName, int residentWorkType, string statusCode, int pageIndex = 1, int pageSize = 10)
        //{
        //    //拼接过滤条件
        //    Expression<Func<WorkTransact, bool>> where =
        //        p =>
        //         (string.IsNullOrEmpty(username) || p.UserName.Contains(username)) &&
        //         (string.IsNullOrEmpty(idcard) || p.IdCard == idcard) &&
        //         (string.IsNullOrEmpty(residentWorkName) || p.ResidentWorkName.Contains(residentWorkName)) &&
        //         (string.IsNullOrEmpty(statusCode) || p.StatusCode == statusCode);

        //    //Todo 项目分类暂时未加-涉及到多表联查
        //    var result = await _iWorkTransactService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
        //    return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputWorkTransactModel>
        //    { Total = result.Item1, List = Mapper.Map<IList<OutputWorkTransactModel>>(result.Item2) });
        //}


        /// <summary>
        /// 查询分页（获取办理信息）
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="stationid">驿站id</param>
        /// <param name="username">预约人姓名</param>
        /// <param name="idcard">身份证号码</param>
        /// <param name="residentWorkName">事项名称</param>
        /// <param name="residentWorkType">事项类别</param>
        /// <param name="statusCode">状态</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页显示数</param>
        /// <returns></returns>
        [Route("getWorkTransactPagedNew")]
        [HttpGet]
        public JsonResult GetWorkTransactPagedNew(Guid? streetid,Guid? stationid,string username, string idcard, string residentWorkName, int residentWorkType, string statusCode, int pageIndex = 1, int pageSize = 10)
        {
            int total = 0;
            var list = _iWorkTransactService.GetWorkTransacts(streetid,stationid,username, idcard, residentWorkName, residentWorkType, statusCode, out total, pageIndex, pageSize);
            if (list != null)
            {
                foreach (var item in list)
                {
                    DateTime a = DateTime.Parse(item.workTransact.ShouliTime.Value.ToString("yyyy-MM-dd"));
                    //计算最晚处理日期
                    item.lastDeadline = a.AddDays(CommonHelper.ToInt(item.Deadline));
                    //计算状态值
                    item.statusNow = HelperWorkTransact.GetStatusNow(item.lastDeadline);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new { list, total });
        }


        /// <summary>
        /// 事项处置登记数量
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="stationid">驿站id</param>
        /// <returns></returns>
        [Route("getWorkTransactCount")]
        [HttpGet]
        public JsonResult GetWorkTransactCount(Guid? streetid, Guid? stationid)
        {
            Expression<Func<MyWorkTransact, bool>> where =
              p =>
               (p.workTransact.StatusCode == "WeiChuLi") &&
               (streetid == null || p.workTransact.StreetId == streetid) &&
                (stationid == null || p.workTransact.StationId == stationid);
            var count = _iWorkTransactService.GetWorkTransactCount(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", count);
        }

        /// <summary>
        /// 事项受理总数目
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="stationid">驿站id</param>
        /// <returns></returns>
        [Route("getWorkTransactCountAll")]
        [HttpGet]
        public JsonResult GetWorkTransactCountAll(Guid? streetid, Guid? stationid)
        {
            Expression<Func<MyWorkTransact, bool>> where =
              p =>
               (streetid == null || p.workTransact.StreetId == streetid) &&
               (stationid == null || p.workTransact.StationId==stationid);
            var count = _iWorkTransactService.GetWorkTransactCount(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", count);
        }

        /// <summary>
        /// 今日受理最多的事项
        /// </summary>
        /// <returns></returns>
        [Route("getWorkTransactMax")]
        [HttpGet]
        public  JsonResult GetWorkTransactMax()
        {
            var info =  _iWorkTransactService.GetWorkTransactMax();
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", info);
        }

    }
}