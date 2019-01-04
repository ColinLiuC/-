using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Service;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Helper;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ActivityRegister;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ActivityRegister")]
    public class ActivityRegisterController : Controller
    {
        private readonly IActivityRegisterService _activityRegisterService;
        private readonly IActivityService _activityService;
        public ActivityRegisterController(IActivityRegisterService activityRegisterService, IActivityService activityService)
        {
            _activityRegisterService = activityRegisterService;
            _activityService = activityService;
        }
        /// <summary>
        /// 分页获取活动信息列表。
        /// </summary>
        /// <param name="activityId">活动id值</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("get")]
        [HttpGet]
        public async Task<JsonResult> GetListData(Guid activityId, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _activityRegisterService.GetPagedAsync(i => i.ActivityId==activityId, i => i.CreationTime, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityRegisterModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityRegisterModel>>(result.Item2) });
        }

        /// <summary>
        /// 新增一条报名信息
        /// </summary>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputActivityRegisterModel inputActivityRegisterModel)
        {
            var activityRegisterDto = new ActivityRegisterDto
            {
                ActivityId = inputActivityRegisterModel.ActivityId,
                EnrolmentName = inputActivityRegisterModel.EnrolmentName,
                ContactNumber = inputActivityRegisterModel.ContactNumber,
                ShiMinYunId= inputActivityRegisterModel.ShiMinYunId,
                IsComment =false,
                RegistDate=DateTime.Now,
                IsShortInterest=0 
            };
            //根据ID获取相应活动并且报名人数加一
            var result = await _activityService.GetAsync(inputActivityRegisterModel.ActivityId);
            if (result.NumberParticipants==null)
            {
                result.NumberParticipants = 0;
            }
            result.NumberParticipants += 1;
            await _activityService.UpdateAsync(result);

            var id = await _activityRegisterService.InsertAndGetIdAsync(activityRegisterDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功",id);
        }

        /// <summary>
        /// 新增一条报名信息
        /// </summary>
        /// <returns></returns>
        [Route("IsExit")]
        [HttpGet]
        public JsonResult IsExit(string EnrolmentName, string ContactNumber)
        {
            var list= _activityRegisterService.GetAllList(p => p.EnrolmentName == EnrolmentName && p.ContactNumber == ContactNumber);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", list.Count);
        }

        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id,Guid activityId)
        {
            var info = await _activityRegisterService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该报名人不存在");
            }
            await _activityRegisterService.DeleteAsync(info);
            var result = await _activityService.GetAsync(activityId);
            result.NumberParticipants --;
            await _activityService.UpdateAsync(result);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
       
        [Route("ExportExcel")]
        [HttpGet]
        public FileResult Export(Guid activityId)
        {
            List<ActivityRegisterDto> activityRegister = _activityRegisterService.GetAllList(p => p.ActivityId == activityId);
            //创建Excel文件对象
            XSSFWorkbook book = new XSSFWorkbook();
            //添加一个Sheet
            ISheet sheet = book.CreateSheet("Sheet1");
            //设置首行标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("序号");
            row.CreateCell(1).SetCellValue("姓名");
            row.CreateCell(2).SetCellValue("联系电话");
            row.CreateCell(3).SetCellValue("报名日期");
            //写入数据
            for (int i = 0; i < activityRegister.Count; i++)
            {
                IRow rows = sheet.CreateRow(i + 1);
                rows.CreateCell(0).SetCellValue(i+1);
                rows.CreateCell(1).SetCellValue(activityRegister[i].EnrolmentName);
                rows.CreateCell(2).SetCellValue(activityRegister[i].ContactNumber);
                rows.CreateCell(3).SetCellValue(activityRegister[i].RegistDate.ToString("yyyy-MM-dd"));
            }
            //写入客户端
            NpoiMemoryStream ms = new NpoiMemoryStream();
            ms.AllowClose = false;
            book.Write(ms);
            ms.Flush();
            ms.Seek(0, SeekOrigin.Begin);
            ms.AllowClose = true;
            string filename = DateTime.Now.ToString("yyMMddHHmmssfff") + ".xlsx";
            return File(ms, "application/vnd.ms-excel", filename);
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="id">活动Id值</param>
        /// <returns></returns>
        [Route("sendMsg")]
        [HttpPost]
        public async Task<JsonResult> SendCode(string mobile,Guid id)
        {
            //发送短信
            string content = "您已成功报名";
            bool isReply=new SmsService().SendMessage(mobile,content);
            if (isReply)
            {
                var info = await _activityRegisterService.GetAsync(id);
                info.IsShortInterest = 1;
                await _activityRegisterService.UpdateAsync(info);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");//201
            }
            else
            {
                return AjaxHelper.JsonResult(HttpStatusCode.Forbidden, "发送失败");//201
            }                
        }
      
        /// <summary>
        /// 判断该手机号是否已报名
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <param name="activityId">活动id值</param>
        /// <returns></returns>
        [Route("getDataByMobile")]
        [HttpPost]
        public JsonResult IsExistData(string mobile,Guid activityId)
        {
            try
                {
                string str;
                var result = _activityRegisterService.GetAllList(p => p.ActivityId == activityId && p.ContactNumber == mobile).FirstOrDefault();
                if (result != null)
                {
                    str = result.EnrolmentName;
                }
                else
                {
                    str = "0";
                }            
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", str);
            }
            catch (Exception)
            {

                throw;
            }          
        }


        /// <summary>
        /// 修改评价状态
        /// </summary>
        /// <returns></returns>
        [Route("isComment")]
        [HttpPost]
        public async Task<JsonResult> isComment(Guid Id)
        {
           
            //根据ID获取相应活动并且报名人数加一
            var result = await _activityRegisterService.GetAsync(Id);
            result.IsComment = true;
            await _activityRegisterService.UpdateAsync(result);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }
    }
}