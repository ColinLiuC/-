using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.OldPeople;
using CoreSolution.WebApi.Models.WorkPerson;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools;
using static CoreSolution.Domain.Enum.EnumCode;
using CoreSolution.WebApi.Models.Register;
using CoreSolution.Dto.MyModel;
using CoreSolution.WebApi.Helper;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/OldPeople")]
    public class OldPeopleController : Controller
    {
        private readonly IOldPeopleService _oldpeopleService;
        private readonly IRegisterService _oldPeopleRegisterService;
        private readonly IRegisterHistoryService _oldPeopleHistoryService;
        private readonly IDataDictionaryService _dataDictionaryService;
        //private readonly IStationService _stationService;
        //private readonly IStreetService _streetService;
        private readonly IJuWeiService _juWeiService;
        private readonly IRegisterService _registerService;
        private readonly IStreetService _streetService;
        private readonly IStationService _stationService;
        public OldPeopleController(IOldPeopleService oldpeopleService, IRegisterService oldPeopleRegisterService, IRegisterHistoryService oldPeopleHistoryService, IDataDictionaryService dataDictionaryService, IJuWeiService juWeiService, IRegisterService registerService, IStreetService streetService, IStationService stationService)
        {
            _oldpeopleService = oldpeopleService;
            _oldPeopleRegisterService = oldPeopleRegisterService;
            _oldPeopleHistoryService = oldPeopleHistoryService;
            _dataDictionaryService = dataDictionaryService;
            //_stationService = stationService;
            //_streetService = streetService;
            _juWeiService = juWeiService;
            _registerService = registerService;
            _stationService = stationService;
            _streetService = streetService;
        }
        /// <summary>
        /// 查询符合条件的数据
        /// </summary>
        /// <returns></returns>
        [Route("index")]
        [HttpGet]
        public async Task<JsonResult> Index(OldPeopleDto oldpeopleDto, int statuscode, int? sAge, int? eAge, int pageIndex = 1, int pageSize = 10, int level1 = 0, int level2 = 0, int level3 = 0)
        {
            int total = 0;
            var list = _oldpeopleService.GetOldPeopleInfo(oldpeopleDto, sAge, eAge, out total, pageIndex, pageSize, level1, level2, level3);
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.OldTypeName = _dataDictionaryService.GetDataName(item.OldType);
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

                        //var result = await _oldPeopleRegisterService.FirstOrDefaultAsync(i => i.OldPeopleId == item.Id);
                        //result.level = item.level;
                        //await _oldPeopleRegisterService.UpdateAsync(result);
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
        public async Task<JsonResult> Create(InputOldPeopleModel inputoldpeopleModel)
        {
            if (inputoldpeopleModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "姓名不能为空");
            }
            if (inputoldpeopleModel.Card.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "身份证号不能为空");
            }
            if (inputoldpeopleModel.Address.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "居住地址不能为空");
            }
            if (inputoldpeopleModel.Phone.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系电话不能为空");
            }
            if (inputoldpeopleModel.OldType == null || inputoldpeopleModel.OldType == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "老人类型不能为空");
            }
            //if (inputoldpeopleModel.JuWei == null || inputoldpeopleModel.JuWei == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            //{
            //    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "所属居委不能为空");
            //}
            string token = HttpContext.Request.Headers["token"];
            var oldpeople = new OldPeopleDto
            {
                Card = inputoldpeopleModel.Card,
                Name = inputoldpeopleModel.Name,
                Sex = inputoldpeopleModel.Sex,
                Age = inputoldpeopleModel.Age,
                Category = inputoldpeopleModel.Category,
                BirthDay = inputoldpeopleModel.BirthDay,
                Marriage = inputoldpeopleModel.Marriage,
                Phone = inputoldpeopleModel.Phone,
                Address = inputoldpeopleModel.Address,
                JuWei = inputoldpeopleModel.JuWei,
                Street = inputoldpeopleModel.Street,
                Station = inputoldpeopleModel.Station,
                Contacts = inputoldpeopleModel.Contacts,
                ContactsPhone = inputoldpeopleModel.ContactsPhone,
                OldType = inputoldpeopleModel.OldType,
                yiyang = inputoldpeopleModel.yiyang,
                keji = inputoldpeopleModel.keji,
                niunai = inputoldpeopleModel.niunai,
                Remarks = inputoldpeopleModel.Remarks,
                User = inputoldpeopleModel.User,
                CreateDate = inputoldpeopleModel.CreateDate
            };
            await _oldpeopleService.InsertAsync(oldpeople);
            var oldpeopleregister = new RegisterDto
            {
                OldPeopleId = oldpeople.Id,
                Name = "reload",
                RegisterType = 1,
                Street = inputoldpeopleModel.Street,
                Station = inputoldpeopleModel.Station,
                JuWei = inputoldpeopleModel.JuWei
            };
            await _oldPeopleRegisterService.InsertAsync(oldpeopleregister);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", new { oldpeople.Id, ok });
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputOldPeopleModel inputOldPeopleModel)
        {
            if (inputOldPeopleModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "姓名不能为空");
            }
            if (inputOldPeopleModel.Card.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "身份证号不能为空");
            }
            if (inputOldPeopleModel.Address.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "居住地址不能为空");
            }
            if (inputOldPeopleModel.Phone.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系电话不能为空");
            }
            if (inputOldPeopleModel.OldType == null || inputOldPeopleModel.OldType == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "老人类型不能为空");
            }
            //if (inputOldPeopleModel.JuWei == null || inputOldPeopleModel.JuWei == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            //{
            //    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "所属居委不能为空");
            //}
            string token = HttpContext.Request.Headers["token"];
            var result = await _oldpeopleService.GetAsync(inputOldPeopleModel.Id);
            result.Card = inputOldPeopleModel.Card;
            result.Name = inputOldPeopleModel.Name;
            result.Sex = inputOldPeopleModel.Sex;
            result.Age = inputOldPeopleModel.Age;
            result.Category = inputOldPeopleModel.Category;
            result.BirthDay = inputOldPeopleModel.BirthDay;
            result.Marriage = inputOldPeopleModel.Marriage;
            result.Phone = inputOldPeopleModel.Phone;
            result.Address = inputOldPeopleModel.Address;
            result.JuWei = inputOldPeopleModel.JuWei;
            result.Street = inputOldPeopleModel.Street;
            result.Station = inputOldPeopleModel.Station;
            result.Contacts = inputOldPeopleModel.Contacts;
            result.ContactsPhone = inputOldPeopleModel.ContactsPhone;
            result.OldType = inputOldPeopleModel.OldType;
            result.yiyang = inputOldPeopleModel.yiyang;
            result.keji = inputOldPeopleModel.keji;
            result.niunai = inputOldPeopleModel.niunai;
            result.Remarks = inputOldPeopleModel.Remarks;
            result.User = inputOldPeopleModel.User;
            result.CreateDate = inputOldPeopleModel.CreateDate;

            await _oldpeopleService.UpdateAsync(result);
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
            var info = await _oldpeopleService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该用户不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //info.IsDeleted = true;
            await _oldpeopleService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetWorkPerson")]
        [HttpGet]
        public async Task<JsonResult> GetWorkPerson(Guid id)
        {
            var result = await _oldpeopleService.GetAsync(id);
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
            result.OldTypeName = _dataDictionaryService.GetDataName(result.OldType);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 修改一条服务信息或新增一条服务信息
        /// </summary>
        /// <returns></returns>
        [Route("create1")]
        [HttpPost]
        public async Task<JsonResult> Create1(InputRegisterModel inputoldpeopleregisterModel)
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
            if ((inputoldpeopleregisterModel.Time.AddDays(inputoldpeopleregisterModel.Cycle) - DateTime.Now).Days < 0)
            {
                inputoldpeopleregisterModel.level = 1;//超期
            }
            else if ((inputoldpeopleregisterModel.Time.AddDays(inputoldpeopleregisterModel.Cycle) - DateTime.Now).Days < 5)
            {
                inputoldpeopleregisterModel.level = 2;//距下次服务还剩5天
            }
            else
            {
                inputoldpeopleregisterModel.level = 4;//距下次服务还剩20天
            }

            var result = await _oldPeopleRegisterService.FirstOrDefaultAsync(i => i.OldPeopleId == inputoldpeopleregisterModel.OldPeopleId);
            if (result == null)
            {
                var oldpeopleregister = new RegisterDto
                {
                    OldPeopleId = inputoldpeopleregisterModel.OldPeopleId,
                    Name = inputoldpeopleregisterModel.Name,
                    Type = inputoldpeopleregisterModel.Type,
                    Category = inputoldpeopleregisterModel.Category,
                    Cycle = inputoldpeopleregisterModel.Cycle,
                    Time = inputoldpeopleregisterModel.Time,
                    Info = inputoldpeopleregisterModel.Info,
                    User = inputoldpeopleregisterModel.User,
                    CreateDate = inputoldpeopleregisterModel.CreateDate,
                    level = inputoldpeopleregisterModel.level,
                    RegisterType = 1,
                    Street = inputoldpeopleregisterModel.Street,
                    Station = inputoldpeopleregisterModel.Station

                };
                await _oldPeopleRegisterService.InsertAsync(oldpeopleregister);
            }
            else
            {
                //result.Id = inputoldpeopleregisterModel.Id;
                result.OldPeopleId = inputoldpeopleregisterModel.OldPeopleId;
                result.Name = inputoldpeopleregisterModel.Name;
                result.Type = inputoldpeopleregisterModel.Type;
                result.Category = inputoldpeopleregisterModel.Category;
                result.Cycle = inputoldpeopleregisterModel.Cycle;
                result.Time = inputoldpeopleregisterModel.Time;
                result.Info = inputoldpeopleregisterModel.Info;
                result.User = inputoldpeopleregisterModel.User;
                result.CreateDate = inputoldpeopleregisterModel.CreateDate;
                result.level = inputoldpeopleregisterModel.level;
                result.RegisterType = inputoldpeopleregisterModel.RegisterType;
                result.Street = inputoldpeopleregisterModel.Street;
                result.Station = inputoldpeopleregisterModel.Station;
                await _oldPeopleRegisterService.UpdateAsync(result);
            }
            var oldPeopleHistory = new RegisterHistoryDto
            {
                OldPeopleId = inputoldpeopleregisterModel.OldPeopleId,
                Name = inputoldpeopleregisterModel.Name,
                Type = inputoldpeopleregisterModel.Type,
                Category = inputoldpeopleregisterModel.Category,
                Cycle = inputoldpeopleregisterModel.Cycle,
                Time = inputoldpeopleregisterModel.Time,
                Info = inputoldpeopleregisterModel.Info,
                User = inputoldpeopleregisterModel.User,
                CreateDate = inputoldpeopleregisterModel.CreateDate,
                level = inputoldpeopleregisterModel.level,
                RegisterType = 1,
                Street = inputoldpeopleregisterModel.Street,
                Station = inputoldpeopleregisterModel.Station
            };
            await _oldPeopleHistoryService.InsertAsync(oldPeopleHistory);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "保存成功", ok);
        }

        /// <summary>
        /// 查询符合条件的服务登记历史数据
        /// </summary>
        /// <returns></returns>
        [Route("History")]
        [HttpGet]
        public async Task<JsonResult> History(Guid OldPeopleId, int pageIndex = 1, int pageSize = 10)
        {
            var model = await _oldPeopleHistoryService.GetPagedAsync(p => p.OldPeopleId == OldPeopleId && p.RegisterType == 1, i => i.CreationTime, pageIndex, pageSize, true);
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
            var result = await _oldPeopleHistoryService.GetAsync(id);
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
            var juweiids = _oldpeopleService.GetJuWeiIds(streetid, poststationid);
            var result1 = _oldpeopleService.StatisticsByType(juweiids);
            var result2 = _oldpeopleService.StatisticsByAge(juweiids);
            var result3 = _oldpeopleService.StatisticsByMonth(juweiids);
            var result4 = _oldpeopleService.StatisticsByCategory(juweiids);
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
            var juweiids = _oldpeopleService.GetJuWeiIds(streetid, poststationid);
            var data = _oldpeopleService.StatisticsByJuWei(juweiids);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", data);
        }

        /// <summary>
        /// 获取重点对象服务待办任务数量
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="poststationid">驿站id</param>
        /// <returns></returns>
        [Route("getRegistersNum")]
        [HttpGet]
        public async Task<JsonResult> getRegistersNum(Guid streetid, Guid? poststationid)
        {
            Expression<Func<Register, bool>> where = PredicateExtensions.True<Register>();
            if (streetid != null && poststationid == null)
            {
                where = where.And(p => p.Street == streetid);
                where = where.And(p => p.Cycle != 0);
                where = where.And(p => ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 0 || ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 5 && ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays > 0);
            }
            else if (streetid != null && poststationid != null)
            {
                where = where.And(p => p.Street == streetid && p.Station == poststationid);
                where = where.And(p => p.Cycle != 0);
                where = where.And(p => ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 0 || ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 5 && ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays > 0);
            }
            else
            {
                where = where.And(p => p.Cycle != 0);
                where = where.And(p => ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 0 || ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 5 && ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays > 0);
            }
            var result = await _registerService.GetAllListAsync(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result.Count);

        }


    }
}