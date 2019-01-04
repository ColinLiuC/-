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
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.WorkPerson;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.WebApi.Manager;
using CoreSolution.Tools;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/WorkPerson")]
    public class WorkPersonController : Controller
    {
        private readonly IWorkPersonService _workpersonService;
        private readonly IDataDictionaryService _dataDictionaryService;
        private readonly IStationService _stationService;
        private readonly IStreetService _streetService;
        public WorkPersonController(IWorkPersonService workpersonService, IDataDictionaryService dataDictionaryService, IStationService stationService, IStreetService streetService)
        {
            _workpersonService = workpersonService;
            _dataDictionaryService = dataDictionaryService;
            _stationService = stationService;
            _streetService = streetService;
        }
        /// <summary>
        /// 查询符合条件的数据
        /// </summary>
        /// <returns></returns>
        [Route("index")]
        [HttpGet]
        public async Task<JsonResult> Index(WorkPersonDto workpersonDto, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<WorkPerson, bool>> where = PredicateExtensions.True<WorkPerson>();
            if (!string.IsNullOrEmpty(workpersonDto.Sex.ToString()))
            {
                where = where.And(p => p.Sex == workpersonDto.Sex);
            }
            if (!string.IsNullOrEmpty(workpersonDto.PerCard))
            {
                where = where.And(p => p.PerCard.Contains(workpersonDto.PerCard));
            }
            if (!string.IsNullOrEmpty(workpersonDto.PerName))
            {
                where = where.And(p => p.PerName.Contains(workpersonDto.PerName));
            }
            if (!string.IsNullOrEmpty(workpersonDto.PerId))
            {
                where = where.And(p => p.PerId.Contains(workpersonDto.PerId));
            }
            if (workpersonDto.Street != null && workpersonDto.Street != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.Street == workpersonDto.Street);
            }
            if (workpersonDto.Station != null && workpersonDto.Station != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.Station == workpersonDto.Station);
            }
            var model = await _workpersonService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            if (model.Item2.Count > 0)
            {
                foreach (var item in model.Item2)
                {
                    item.PerTypeName = _dataDictionaryService.GetDataName(item.PerType);
                    item.StreetName = _streetService.GetStreetName(item.Street);
                    item.StationName = _stationService.GetStationName(item.Station);
                }
            }

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputWorkPersonModel> { Total = model.Item1, List = Mapper.Map<IList<OutputWorkPersonModel>>(model.Item2) });
        }

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputWorkPersonModel inputworkpersonModel)
        {
            if (inputworkpersonModel.PerId.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工Id不能为空");
            }
            if (inputworkpersonModel.PerName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工姓名不能为空");
            }
            if (inputworkpersonModel.PerCard.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工身份证号不能为空");
            }
            if (inputworkpersonModel.Ethnic == null || inputworkpersonModel.Ethnic == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工民族不能为空");
            }
            if (inputworkpersonModel.Street == null || inputworkpersonModel.Street == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工所属街道不能为空");
            }
            if (inputworkpersonModel.Station == null || inputworkpersonModel.Station == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工所属驿站不能为空");
            }
            if (inputworkpersonModel.PoliticalAspects == null || inputworkpersonModel.PoliticalAspects == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工政治面貌不能为空");
            }
            if (inputworkpersonModel.PerType == null || inputworkpersonModel.PerType == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "人员类型不能为空");
            }
            if (inputworkpersonModel.Address == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "人员居住地址不能为空");
            }
            if (inputworkpersonModel.Post == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "人员职务不能为空");
            }
            //if (inputworkpersonModel.Phone == null)
            //{
            //    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "联系电话不能为空");
            //}
            string token = HttpContext.Request.Headers["token"];
            var workperson = new WorkPersonDto
            {
                PerId = inputworkpersonModel.PerId,
                PerImg = inputworkpersonModel.PerImg,
                PerImgSrc = inputworkpersonModel.PerImgSrc,
                PerName = inputworkpersonModel.PerName,
                PerCard = inputworkpersonModel.PerCard,
                Sex = inputworkpersonModel.Sex,
                Age = inputworkpersonModel.Age,
                BirthDay = inputworkpersonModel.BirthDay,
                Ethnic = inputworkpersonModel.Ethnic,
                Address = inputworkpersonModel.Address,
                Phone = Convert.ToInt32(inputworkpersonModel.Phone),
                Street = inputworkpersonModel.Street,
                Station = inputworkpersonModel.Station,
                Post = inputworkpersonModel.Post,
                PerType = inputworkpersonModel.PerType,
                Degree = inputworkpersonModel.Degree,
                PoliticalAspects = inputworkpersonModel.PoliticalAspects
            };
            await _workpersonService.InsertAsync(workperson);
            var ok = "yes";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", ok);
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(WorkPersonDto workpersondto)
        {
            if (workpersondto.PerId.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工Id不能为空");
            }
            if (workpersondto.PerName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工姓名不能为空");
            }
            if (workpersondto.PerCard.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工身份证号不能为空");
            }
            if (workpersondto.Ethnic == null || workpersondto.Ethnic == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工民族不能为空");
            }
            if (workpersondto.Street == null || workpersondto.Street == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工所属街道不能为空");
            }
            if (workpersondto.Station == null || workpersondto.Station == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工所属驿站不能为空");
            }
            if (workpersondto.PoliticalAspects == null || workpersondto.PoliticalAspects == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工政治面貌不能为空");
            }
            if (workpersondto.PerType == null || workpersondto.PerType == Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "员工类型不能为空");
            }
            if (workpersondto.Address == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "人员居住地址不能为空");
            }
            if (workpersondto.Post == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "人员职务不能为空");
            }
            //if (workpersondto.Phone == null)
            //{
            //    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "人员电话不能为空");
            //}
            string token = HttpContext.Request.Headers["token"];
            var result = await _workpersonService.GetAsync(workpersondto.Id);
            result.Id = workpersondto.Id;
            result.PerId = workpersondto.PerId;
            result.PerImg = workpersondto.PerImg;
            result.PerImgSrc = workpersondto.PerImgSrc;
            result.PerName = workpersondto.PerName;
            result.PerCard = workpersondto.PerCard;
            result.Sex = workpersondto.Sex;
            result.Age = workpersondto.Age;
            result.BirthDay = workpersondto.BirthDay;
            result.Ethnic = workpersondto.Ethnic;
            result.Address = workpersondto.Address;
            result.Phone = workpersondto.Phone;
            result.Street = workpersondto.Street;
            result.Station = workpersondto.Station;
            result.Post = workpersondto.Post;
            result.PerType = workpersondto.PerType;
            result.Degree = workpersondto.Degree;
            result.PoliticalAspects = workpersondto.PoliticalAspects;

            await _workpersonService.UpdateAsync(result);
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
            var info = await _workpersonService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该用户不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //info.IsDeleted = true;
            await _workpersonService.DeleteAsync(info);
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
            var result = await _workpersonService.GetAsync(id);
            result.PerTypeName = _dataDictionaryService.GetDataName(result.PerType);
            result.EthnicName = _dataDictionaryService.GetDataName(result.Ethnic);
            result.DegreeName = _dataDictionaryService.GetDataName(result.Degree);
            result.PoliticalAspectsName = _dataDictionaryService.GetDataName(result.PoliticalAspects);
            result.StreetName = _streetService.GetStreetName(result.Street);
            result.StationName = _stationService.GetStationName(result.Station);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


    }
}