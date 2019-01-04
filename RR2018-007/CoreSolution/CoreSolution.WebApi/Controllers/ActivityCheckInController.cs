using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Redis.Helper;
using CoreSolution.Service;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Helper;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ActivityCheckIn;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ActivityCheckIn")]
    public class ActivityCheckInController : Controller
    {
        private readonly IActivityCheckInService _activityCheckInService;
        public ActivityCheckInController(IActivityCheckInService activityCheckInService)
        {
            _activityCheckInService = activityCheckInService;            
        }
        /// <summary>
        /// 获取活动签到信息列表。
        /// </summary>
        /// <param name="activityId">活动id值</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("get")]
        [HttpGet]
        public async Task<JsonResult> GetListData(Guid activityId, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _activityCheckInService.GetPagedAsync(i => i.ActivityId == activityId, i => i.CreationTime, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputActivityCheckInModel> { Total = result.Item1, List = Mapper.Map<IList<OutputActivityCheckInModel>>(result.Item2) });
        }

        /// <summary>
        /// 新增一条签到信息
        /// </summary>
        /// <returns></returns>
        [Route("Create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputActivityCheckInModel inputActivityCheckInModel)
        {
            var activityRegisterDto = new ActivityCheckInDto
            {
                EnrolmentName = inputActivityCheckInModel.EnrolmentName,
                ContactNumber = inputActivityCheckInModel.ContactNumber,
                ActivityId = inputActivityCheckInModel.ActivityId,
                SignUpDate = DateTime.Now
            };
            var id = await _activityCheckInService.InsertAndGetIdAsync(activityRegisterDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }
        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _activityCheckInService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该报名人不存在");
            }
            await _activityCheckInService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        [Route("ExportExcel")]
        [HttpGet]
        public FileResult Export(Guid activityId)
        {
            List<ActivityCheckInDto> activityRegister = _activityCheckInService.GetAllList(p => p.ActivityId == activityId);
            //创建Excel文件对象
            XSSFWorkbook book = new XSSFWorkbook();
            //添加一个Sheet
            ISheet sheet = book.CreateSheet("Sheet1");
            //设置首行标题
            IRow row = sheet.CreateRow(0);
            row.CreateCell(0).SetCellValue("序号");
            row.CreateCell(1).SetCellValue("姓名");
            row.CreateCell(2).SetCellValue("联系电话");
            row.CreateCell(3).SetCellValue("签到日期");
            //写入数据
            for (int i = 0; i < activityRegister.Count; i++)
            {
                IRow rows = sheet.CreateRow(i + 1);
                rows.CreateCell(0).SetCellValue(i + 1);
                rows.CreateCell(1).SetCellValue(activityRegister[i].EnrolmentName);
                rows.CreateCell(2).SetCellValue(activityRegister[i].ContactNumber);
                rows.CreateCell(3).SetCellValue(activityRegister[i].SignUpDate.ToString("yyyy-MM-dd"));
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
        /// 发送短信验证码
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        [Route("sendCode")]
        [HttpPost]
        public async Task<JsonResult> SendCode(string mobile)
        {
            ValidCodeHelper vCode = new ValidCodeHelper();
            string uservalidcode = vCode.CreateValidateCode(6);
            //存入redis
            await RedisHelper.StringSetAsync("validcode", uservalidcode, TimeSpan.FromMinutes(5));
            //发送短信
            new SmsService().SendCode(mobile, uservalidcode);
            return AjaxHelper.JsonResult(HttpStatusCode.Created, "成功");//201
        }

        /// <summary>
        /// 手机端签到
        /// </summary>
        /// <returns></returns>
        [Route("CheckIn")]
        [HttpPost]
        public async Task<JsonResult> CheckIn(InputActivityCheckInModel inputActivityCheckInModel,string tokenCode)
        {
          var list=  _activityCheckInService.GetAllList(p => p.ActivityId == inputActivityCheckInModel.ActivityId && p.ContactNumber == inputActivityCheckInModel.ContactNumber);
            if (list.Count>0)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotAcceptable, "你已经签到过来，不要重复签到！");
            }
            //检测验证码是否匹配
            string code = await RedisHelper.StringGetAsync("validcode"); 
            string salt = new Random().Next(100000, 999999).ToString();
            if (tokenCode != code)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotAcceptable, "验证码不正确");
            }
            var activityRegisterDto = new ActivityCheckInDto
            {
                EnrolmentName = inputActivityCheckInModel.EnrolmentName,
                ContactNumber = inputActivityCheckInModel.ContactNumber,
                ActivityId = inputActivityCheckInModel.ActivityId,
                SignUpDate = DateTime.Now
            };
            var id = await _activityCheckInService.InsertAndGetIdAsync(activityRegisterDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }
    }
}