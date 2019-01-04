using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models.Disability;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models.Register;
using CoreSolution.WebApi.Models;
using AutoMapper;
using CoreSolution.Dto.MyModel;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Disability")]
    public class DisabilityController : Controller
    {
        private readonly IDisabilityService _disabilityService;
        private readonly IRegisterService _RegisterService;
        private readonly IRegisterHistoryService _RegisterHistoryService;
        private readonly IDataDictionaryService _dataDictionaryService;
        private readonly IJuWeiService _juWeiService;
        private readonly IStreetService _streetService;
        private readonly IStationService _stationService;
        public DisabilityController(IDisabilityService disabilityService, IRegisterService RegisterService, IRegisterHistoryService RegisterHistoryService, IDataDictionaryService dataDictionaryService, IJuWeiService juWeiService, IStreetService streetService, IStationService stationService)
        {
            _disabilityService = disabilityService;
            _RegisterService = RegisterService;
            _RegisterHistoryService = RegisterHistoryService;
            _dataDictionaryService = dataDictionaryService;
            _juWeiService = juWeiService;
            _stationService = stationService;
            _streetService = streetService;
        }

        /// <summary>
        /// 查询符合条件的数据
        /// </summary>
        /// <returns></returns>
        [Route("index")]
        [HttpGet]
        public async Task<JsonResult> Index(DisabilityDto disabilityDto, int? sAge, int? eAge, int pageIndex = 1, int pageSize = 10, int level1 = 0, int level2 = 0, int level3 = 0)
        {
            int total = 0;
            var list = _disabilityService.GetDisabilityInfo(disabilityDto, sAge, eAge, out total, pageIndex, pageSize, level1, level2, level3);
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.DisabilityTypeName = _dataDictionaryService.GetDataName(item.DisabilityType);
                    if (item.Category != null && item.Category != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        item.CategoryName = _dataDictionaryService.GetDataName(item.Category);
                    }
                    if (item.JuWei != null && item.JuWei != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        item.JuWeiName = _juWeiService.GetJuWeiName(item.JuWei);
                    }
                    if (item.Time != null && item.Cycle != null && item.Cycle != 0)
                    {
                        item.level = Helper.CommonHelper.SetStatusCode(item.Time, Convert.ToInt32(item.Cycle));
                    }
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new { list, total });
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputDisabilityModel inputdisabilityModel)
        {
            if (inputdisabilityModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "姓名不能为空");
            }
            if (inputdisabilityModel.Card.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "身份证号不能为空");
            }
            if (inputdisabilityModel.Address.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "居住地址不能为空");
            }
            if (inputdisabilityModel.Phone.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系电话不能为空");
            }
            if (inputdisabilityModel.DisabilityType == null || inputdisabilityModel.DisabilityType == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "残疾类型不能为空");
            }
            //if (inputdisabilityModel.JuWei == null || inputdisabilityModel.JuWei == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            //{
            //    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "所属居委不能为空");
            //}
            if (inputdisabilityModel.DisabilityLevel == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "残疾级别不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var disability = new DisabilityDto
            {
                Card = inputdisabilityModel.Card,
                Name = inputdisabilityModel.Name,
                Sex = inputdisabilityModel.Sex,
                Age = inputdisabilityModel.Age,
                BirthDay = inputdisabilityModel.BirthDay,
                Marriage = inputdisabilityModel.Marriage,
                Phone = inputdisabilityModel.Phone,
                Address = inputdisabilityModel.Address,
                JuWei = inputdisabilityModel.JuWei,
                Street = inputdisabilityModel.Street,
                Station = inputdisabilityModel.Station,
                Contacts = inputdisabilityModel.Contacts,
                ContactsPhone = inputdisabilityModel.ContactsPhone,
                DisabilityType = inputdisabilityModel.DisabilityType,
                DisabilityLevel = (Level)inputdisabilityModel.DisabilityLevel,
                Employment = inputdisabilityModel.Employment,
                Remarks = inputdisabilityModel.Remarks,
                User = inputdisabilityModel.User,
                CreateDate = inputdisabilityModel.CreateDate
            };
            await _disabilityService.InsertAsync(disability);
            var register = new RegisterDto
            {
                OldPeopleId = disability.Id,
                Name = "reload",
                RegisterType = 2,
                Street = inputdisabilityModel.Street,
                Station = inputdisabilityModel.Station,
                JuWei = inputdisabilityModel.JuWei
            };
            await _RegisterService.InsertAsync(register);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", new { disability.Id, ok });
        }


        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputDisabilityModel inputdisabilityModel)
        {
            if (inputdisabilityModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "姓名不能为空");
            }
            if (inputdisabilityModel.Card.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "身份证号不能为空");
            }
            if (inputdisabilityModel.Address.ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "居住地址不能为空");
            }
            if (inputdisabilityModel.Phone.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系电话不能为空");
            }
            if (inputdisabilityModel.DisabilityType == null || inputdisabilityModel.DisabilityType == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "老人类型不能为空");
            }
            //if (inputdisabilityModel.JuWei == null || inputdisabilityModel.JuWei == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            //{
            //    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "所属居委不能为空");
            //}
            if (inputdisabilityModel.DisabilityLevel == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "残疾级别不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var result = await _disabilityService.GetAsync(inputdisabilityModel.Id);
            result.Card = inputdisabilityModel.Card;
            result.Name = inputdisabilityModel.Name;
            result.Sex = inputdisabilityModel.Sex;
            result.Age = inputdisabilityModel.Age;
            result.BirthDay = inputdisabilityModel.BirthDay;
            result.Marriage = inputdisabilityModel.Marriage;
            result.Phone = inputdisabilityModel.Phone;
            result.Address = inputdisabilityModel.Address;
            result.JuWei = inputdisabilityModel.JuWei;
            result.Street = inputdisabilityModel.Street;
            result.Station = inputdisabilityModel.Station;
            result.Contacts = inputdisabilityModel.Contacts;
            result.ContactsPhone = inputdisabilityModel.ContactsPhone;
            result.DisabilityType = inputdisabilityModel.DisabilityType;
            result.DisabilityLevel = (Level)inputdisabilityModel.DisabilityLevel;
            result.Employment = inputdisabilityModel.Employment;
            result.Remarks = inputdisabilityModel.Remarks;
            result.User = inputdisabilityModel.User;
            result.CreateDate = inputdisabilityModel.CreateDate;

            await _disabilityService.UpdateAsync(result);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功", ok);
        }

        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _disabilityService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该用户不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //info.IsDeleted = true;
            await _disabilityService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDisability")]
        [HttpGet]
        public async Task<JsonResult> GetDisability(Guid id)
        {
            var result = await _disabilityService.GetAsync(id);
            if (result.JuWei != null && result.JuWei != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                result.JuWeiName = _juWeiService.GetJuWeiName(result.JuWei);
            }
            if (result.Street != null && result.Street != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                result.StreetName = _streetService.GetStreetName(result.Street);
            }
            if (result.Station != null && result.Station != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                result.StationName = _stationService.GetStationName(result.Station);
            }
            result.DisabilityTypeName = _dataDictionaryService.GetDataName(result.DisabilityType);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 修改一条服务信息或新增一条服务信息
        /// </summary>
        /// <returns></returns>
        [Route("create1")]
        [HttpPost]
        public async Task<JsonResult> Create1(InputRegisterModel inputregisterModel)
        {
            //if (!ModelState.IsValid)
            //{
            //    var errorlist = new List<ErrorModel>();
            //    //获取所有错误的Key
            //    List<string> Keys = ModelState.Keys.ToList();
            //    //获取每一个key对应的ModelStateDictionary
            //    foreach (var key in Keys)
            //    {
            //        var errors = ModelState[key].Errors.ToList();
            //        //将错误描述添加到sb中
            //        foreach (var error in errors)
            //        {
            //            errorlist.Add(new ErrorModel() { Name = key, ErrorMsg = error.ErrorMessage });
            //        }
            //    }

            //    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "数据格式不正确", errorlist);


            //}

            string token = HttpContext.Request.Headers["token"];
            if ((inputregisterModel.Time.AddDays(inputregisterModel.Cycle) - DateTime.Now).Days < 0)
            {
                inputregisterModel.level = 1;//超期
            }
            else if ((inputregisterModel.Time.AddDays(inputregisterModel.Cycle) - DateTime.Now).Days < 5)
            {
                inputregisterModel.level = 2;//距下次服务还剩5天
            }
            else
            {
                inputregisterModel.level = 4;//距下次服务还剩20天
            }

            var result = await _RegisterService.FirstOrDefaultAsync(i => i.OldPeopleId == inputregisterModel.OldPeopleId);
            if (result == null)
            {
                var oldpeopleregister = new RegisterDto
                {
                    OldPeopleId = inputregisterModel.OldPeopleId,
                    Name = inputregisterModel.Name,
                    Type = inputregisterModel.Type,
                    Category = inputregisterModel.Category,
                    Cycle = inputregisterModel.Cycle,
                    Time = inputregisterModel.Time,
                    Info = inputregisterModel.Info,
                    User = inputregisterModel.User,
                    CreateDate = inputregisterModel.CreateDate,
                    level = inputregisterModel.level,
                    RegisterType = 2,
                    Street = inputregisterModel.Street,
                    Station = inputregisterModel.Station

                };
                await _RegisterService.InsertAsync(oldpeopleregister);
            }
            else
            {
                //result.Id = inputoldpeopleregisterModel.Id;
                result.OldPeopleId = inputregisterModel.OldPeopleId;
                result.Name = inputregisterModel.Name;
                result.Type = inputregisterModel.Type;
                result.Category = inputregisterModel.Category;
                result.Cycle = inputregisterModel.Cycle;
                result.Time = inputregisterModel.Time;
                result.Info = inputregisterModel.Info;
                result.User = inputregisterModel.User;
                result.CreateDate = inputregisterModel.CreateDate;
                result.level = inputregisterModel.level;
                result.RegisterType = inputregisterModel.RegisterType;
                result.Street = inputregisterModel.Street;
                result.Station = inputregisterModel.Station;
                await _RegisterService.UpdateAsync(result);
            }
            var RegisterHistory = new RegisterHistoryDto
            {
                OldPeopleId = inputregisterModel.OldPeopleId,
                Name = inputregisterModel.Name,
                Type = inputregisterModel.Type,
                Category = inputregisterModel.Category,
                Cycle = inputregisterModel.Cycle,
                Time = inputregisterModel.Time,
                Info = inputregisterModel.Info,
                User = inputregisterModel.User,
                CreateDate = inputregisterModel.CreateDate,
                level = inputregisterModel.level,
                RegisterType = 2,
                Street = inputregisterModel.Street,
                Station = inputregisterModel.Station
            };
            await _RegisterHistoryService.InsertAsync(RegisterHistory);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "保存成功", ok);
        }

        /// <summary>
        /// 查询符合条件的服务登记历史数据
        /// </summary>
        /// <returns></returns>
        [Route("History")]
        [HttpGet]
        public async Task<JsonResult> History(Guid PeopleId, int pageIndex = 1, int pageSize = 10)
        {
            var model = await _RegisterHistoryService.GetPagedAsync(p => p.OldPeopleId == PeopleId && p.RegisterType == 2, i => i.CreationTime, pageIndex, pageSize, true);
            if (model.Item2.Count > 0)
            {
                foreach (var item in model.Item2)
                {
                    item.CategoryName = _dataDictionaryService.GetDataName(item.Category);
                    item.TypeName = _dataDictionaryService.GetDataName(item.Type);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputRegisterModel> { Total = model.Item1, List = Mapper.Map<IList<OutputRegisterModel>>(model.Item2) });
        }
        /// <summary>
        /// 通过ID得到一条服务登记历史数据
        /// </summary>
        /// <returns></returns>
        [Route("GetHistory")]
        [HttpGet]
        public async Task<JsonResult> GetHistory(Guid id)
        {
            var result = await _RegisterHistoryService.GetAsync(id);
            result.CategoryName = _dataDictionaryService.GetDataName(result.Category);
            result.TypeName = _dataDictionaryService.GetDataName(result.Type);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 获取统计分析数据
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="poststationid">驿站id</param>
        /// <returns></returns>
        [Route("getStatisticsData")]
        [HttpGet]
        public JsonResult GetStatisticsData(Guid? streetid, Guid? poststationid)
        {
            var juweiids = _disabilityService.GetJuWeiIds(streetid, poststationid);
            var result1 = _disabilityService.StatisticsByType(juweiids);
            var result2 = _disabilityService.StatisticsByAge(juweiids);
            var result3 = _disabilityService.StatisticsByMonth(juweiids);
            var result4 = _disabilityService.StatisticsByCategory(juweiids);
            var data = new List<List<MyKeyAndValue>>() { result1.Result, result2.Result, result3, result4 };
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", data);
        }

        /// <summary>
        /// 获取地图统计数据
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="poststationid">驿站id</param>
        /// <returns></returns>
        [Route("getStatisticsDataByJuWei")]
        [HttpGet]
        public JsonResult GetStatisticsDataByJuWei(Guid? streetid, Guid? poststationid)
        {
            var juweiids = _disabilityService.GetJuWeiIds(streetid, poststationid);
            var data = _disabilityService.StatisticsByJuWei(juweiids);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", data);
        }
    }
}