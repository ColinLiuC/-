using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models.OtherPeople;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models.Register;
using CoreSolution.WebApi.Models;
using AutoMapper;
using CoreSolution.Dto.MyModel;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/OtherPeople")]
    public class OtherPeopleController : Controller
    {
        private readonly IOtherPeopleService _otherPeopleService;
        private readonly IRegisterService _RegisterService;
        private readonly IRegisterHistoryService _RegisterHistoryService;
        private readonly IDataDictionaryService _dataDictionaryService;
        private readonly IJuWeiService _juWeiService;
        private readonly IStreetService _streetService;
        private readonly IStationService _stationService;
        public OtherPeopleController(IOtherPeopleService otherPeopleService, IRegisterService RegisterService, IRegisterHistoryService RegisterHistoryService, IDataDictionaryService dataDictionaryService, IJuWeiService juWeiService, IStreetService streetService, IStationService stationService)
        {
            _otherPeopleService = otherPeopleService;
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
        public async Task<JsonResult> Index(OtherPeopleDto otherPeopleDto, int? sAge, int? eAge, int pageIndex = 1, int pageSize = 10, int level1 = 0, int level2 = 0, int level3 = 0)
        {
            int total = 0;
            var list = _otherPeopleService.GetOtherPeopleInfo(otherPeopleDto, sAge, eAge, out total, pageIndex, pageSize, level1, level2, level3);
            if (list.Count > 0)
            {
                foreach (var item in list)
                {
                    item.BelongTypeName = _dataDictionaryService.GetDataName(item.BelongType);
                    if (item.Category != null && item.Category != Guid.Parse("00000000-0000-0000-0000-000000000000"))
                    {
                        item.CategoryName = _dataDictionaryService.GetDataName(item.Category);
                    }
                    item.BelongTypeName = _dataDictionaryService.GetDataName(item.BelongType);
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
        public async Task<JsonResult> Create(InputOtherPeopleModel inputOtherPeopleModel)
        {
            if (inputOtherPeopleModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "姓名不能为空");
            }
            if (inputOtherPeopleModel.Card.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "身份证号不能为空");
            }
            if (inputOtherPeopleModel.Address.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "居住地址不能为空");
            }
            if (inputOtherPeopleModel.Phone.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系电话不能为空");
            }
            if (inputOtherPeopleModel.BelongType == null || inputOtherPeopleModel.BelongType == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "所属类型不能为空");
            }
            //if (inputOtherPeopleModel.JuWei == null || inputOtherPeopleModel.JuWei == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            //{
            //    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "所属居委不能为空");
            //}
            string token = HttpContext.Request.Headers["token"];
            var otherPeopleDto = new OtherPeopleDto
            {
                Card = inputOtherPeopleModel.Card,
                Name = inputOtherPeopleModel.Name,
                Sex = inputOtherPeopleModel.Sex,
                Age = inputOtherPeopleModel.Age,
                BirthDay = inputOtherPeopleModel.BirthDay,
                Marriage = inputOtherPeopleModel.Marriage,
                Phone = inputOtherPeopleModel.Phone,
                Address = inputOtherPeopleModel.Address,
                JuWei = inputOtherPeopleModel.JuWei,
                Street = inputOtherPeopleModel.Street,
                Station = inputOtherPeopleModel.Station,
                BelongType = inputOtherPeopleModel.BelongType,
                Remarks = inputOtherPeopleModel.Remarks,
                User = inputOtherPeopleModel.User,
                CreateDate = inputOtherPeopleModel.CreateDate
            };
            await _otherPeopleService.InsertAsync(otherPeopleDto);
            var register = new RegisterDto
            {
                OldPeopleId = otherPeopleDto.Id,
                Name = "reload",
                RegisterType = 5,
                Street = inputOtherPeopleModel.Street,
                Station = inputOtherPeopleModel.Station,
                JuWei = inputOtherPeopleModel.JuWei
            };
            await _RegisterService.InsertAsync(register);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", new { otherPeopleDto.Id, ok });
        }


        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputOtherPeopleModel inputOtherPeopleModel)
        {
            if (inputOtherPeopleModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "姓名不能为空");
            }
            if (inputOtherPeopleModel.Card.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "身份证号不能为空");
            }
            if (inputOtherPeopleModel.Address.ToString().IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "居住地址不能为空");
            }
            if (inputOtherPeopleModel.Phone.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系电话不能为空");
            }
            if (inputOtherPeopleModel.BelongType == null || inputOtherPeopleModel.BelongType == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "所属类型不能为空");
            }
            //if (inputOtherPeopleModel.JuWei == null || inputOtherPeopleModel.JuWei == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            //{
            //    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "所属居委不能为空");
            //}
            string token = HttpContext.Request.Headers["token"];
            var result = await _otherPeopleService.GetAsync(inputOtherPeopleModel.Id);
            result.Card = inputOtherPeopleModel.Card;
            result.Name = inputOtherPeopleModel.Name;
            result.Sex = inputOtherPeopleModel.Sex;
            result.Age = inputOtherPeopleModel.Age;
            result.BirthDay = inputOtherPeopleModel.BirthDay;
            result.Marriage = inputOtherPeopleModel.Marriage;
            result.Phone = inputOtherPeopleModel.Phone;
            result.Address = inputOtherPeopleModel.Address;
            result.JuWei = inputOtherPeopleModel.JuWei;
            result.Street = inputOtherPeopleModel.Street;
            result.Station = inputOtherPeopleModel.Station;
            result.BelongType = inputOtherPeopleModel.BelongType;
            result.Remarks = inputOtherPeopleModel.Remarks;
            result.User = inputOtherPeopleModel.User;
            result.CreateDate = inputOtherPeopleModel.CreateDate;

            await _otherPeopleService.UpdateAsync(result);
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
            var info = await _otherPeopleService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该用户不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //info.IsDeleted = true;
            await _otherPeopleService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetOtherPeople")]
        [HttpGet]
        public async Task<JsonResult> GetOtherPeople(Guid id)
        {
            var result = await _otherPeopleService.GetAsync(id);
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
            result.BelongTypeName = _dataDictionaryService.GetDataName(result.BelongType);
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
                var peopleregister = new RegisterDto
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
                    RegisterType = 5,
                    Street = inputregisterModel.Street,
                    Station = inputregisterModel.Station

                };
                await _RegisterService.InsertAsync(peopleregister);
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
                RegisterType = 5,
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
            var model = await _RegisterHistoryService.GetPagedAsync(p => p.OldPeopleId == PeopleId && p.RegisterType == 5, i => i.CreationTime, pageIndex, pageSize, true);
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
            var juweiids = _otherPeopleService.GetJuWeiIds(streetid, poststationid);
            var result1 = _otherPeopleService.StatisticsByType(juweiids);
            var result2 = _otherPeopleService.StatisticsByAge(juweiids);
            var result3 = _otherPeopleService.StatisticsByMonth(juweiids);
            var result4 = _otherPeopleService.StatisticsByCategory(juweiids);
            var data = new List<List<MyKeyAndValue>>() { result1, result2, result3, result4 };
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
            var juweiids = _otherPeopleService.GetJuWeiIds(streetid, poststationid);
            var data = _otherPeopleService.StatisticsByJuWei(juweiids);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", data);
        }

    }
}