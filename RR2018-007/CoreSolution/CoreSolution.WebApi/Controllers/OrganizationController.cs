using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.IService;
using CoreSolution.WebApi.Models.Organization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using System.Net;
using AutoMapper;
using CoreSolution.Dto;
using System.Text;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Organization")]
    public class OrganizationController : Controller
    {
        private readonly IOrganizationService _organizationService;
        private readonly IOrganizationProjectService _organizationProjectService;
        private readonly IDataDictionaryService _dataDictionaryService;
        private readonly IStreetService _streetService;
        private readonly IStationService _stationService;

        public OrganizationController(IOrganizationService organizationService, IOrganizationProjectService organizationProjectService, IDataDictionaryService dataDictionaryService,IStreetService streetService,IStationService stationService)
        {
            _organizationService = organizationService;
            _organizationProjectService = organizationProjectService;
            _dataDictionaryService = dataDictionaryService;
            _streetService = streetService;
            _stationService = stationService;
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputOrganizationModel inputOrganizationModel)
        {
            if (inputOrganizationModel.OrganizationName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "组织名称不能为空");
            }
            if (inputOrganizationModel.IndustryCategory == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择行业类别");
            }
            if (inputOrganizationModel.OrganizationType == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择组织类型");
            }
            if (inputOrganizationModel.Street == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属街道");
            }
            if (inputOrganizationModel.Station == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属驿站");
            }
            var organizationDto = Mapper.Map<OrganizationDto>(inputOrganizationModel);
            var id = await _organizationService.InsertAndGetIdAsync(organizationDto);
            var idd = organizationDto.Id;
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputOrganizationModel inputOrganizationModel,Guid Id)
        {
            if (inputOrganizationModel.OrganizationName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "活动名称不能为空");
            }
            if (inputOrganizationModel.IndustryCategory == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择行业类别");
            }
            if (inputOrganizationModel.OrganizationType == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择组织类型");
            }
            if (inputOrganizationModel.Street == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属街道");
            }
            if (inputOrganizationModel.Station == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属驿站");
            }
            var organizationDto = new OrganizationDto
            {
                Id = Id,//获取这个可空值的默认值
                OrganizationName = inputOrganizationModel.OrganizationName,
                OrganizationType = Guid.Parse(inputOrganizationModel.OrganizationType.ToString()),
                IndustryCategory = Guid.Parse(inputOrganizationModel.IndustryCategory.ToString()),
                Contacts = inputOrganizationModel.Contacts,
                ContactNumber = inputOrganizationModel.ContactNumber,
                Members = inputOrganizationModel.Members,
                Address = inputOrganizationModel.Address,
                DetailsIntroduction = inputOrganizationModel.DetailsIntroduction,
                AttachmentName = inputOrganizationModel.AttachmentName,
                AttachmentPath = inputOrganizationModel.AttachmentPath,
                Station = Guid.Parse(inputOrganizationModel.Station.ToString()),
                Street = Guid.Parse(inputOrganizationModel.Street.ToString())
            };
            await _organizationService.UpdateAsync(organizationDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetOrganizationById")]
        [HttpGet]
        public async Task<JsonResult> GetOrganizationById(Guid id)
        {
            var result = await _organizationService.GetAsync(id);
            result.OrganizationTypeName = _dataDictionaryService.GetDataName(result.OrganizationType);
            result.IndustryCategoryName = _dataDictionaryService.GetDataName(result.IndustryCategory);
            result.StreetName = _streetService.GetStreetName(result.Street);
            result.StationName = _stationService.GetStationName(result.Station);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        [Route("getTreeOrganization")]
        [HttpGet]
        public JsonResult GetTreeOrganization(Guid? street,Guid? station)
        {
            var result = _organizationService.GetTreeList(street,station);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

    }
}