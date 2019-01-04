using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.IService;
using CoreSolution.WebApi.Models.OrganizationProject;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using System.Net;
using CoreSolution.Dto;
using AutoMapper;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/OrganizationProject")]
    public class OrganizationProjectController : Controller
    {
        private readonly IOrganizationProjectService _organizationProjectService;
        private readonly IDataDictionaryService _dataDictionaryService;
        private readonly IStreetService _streetService;
        private readonly IStationService _stationService;
        public OrganizationProjectController(IOrganizationProjectService organizationProjectService, IDataDictionaryService dataDictionaryService,IStreetService streetService,IStationService stationService)
        {
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
        public async Task<JsonResult> Create(InputOrganizationProjectModel inputOrganizationProjectModel)
        {
            if (inputOrganizationProjectModel.ProjectName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "项目名称不能为空");
            }
            if (inputOrganizationProjectModel.ProjectCategory == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择项目类别");
            }
            if (inputOrganizationProjectModel.Street == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属街道");
            }
            if (inputOrganizationProjectModel.Station == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属驿站");
            }
            var organizationDto = Mapper.Map<OrganizationProjectDto>(inputOrganizationProjectModel);
            await _organizationProjectService.InsertAsync(organizationDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }


        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputOrganizationProjectModel inputOrganizationProjectModel, Guid Id)
        {
            if (inputOrganizationProjectModel.ProjectName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "活动名称不能为空");
            }
            if (inputOrganizationProjectModel.ProjectCategory == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择项目类别");
            }
            if (inputOrganizationProjectModel.Street == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属街道");
            }
            if (inputOrganizationProjectModel.Station == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属驿站");
            }
            var organizationProjectDto = new OrganizationProjectDto
            {
                Id = Id,//获取这个可空值的默认值
                OrganizationId = inputOrganizationProjectModel.OrganizationId,
                ProjectName = inputOrganizationProjectModel.ProjectName,
                ProjectCategory = Guid.Parse(inputOrganizationProjectModel.ProjectCategory.ToString()),
                Client = inputOrganizationProjectModel.Client,
                TargetGroup = inputOrganizationProjectModel.TargetGroup,
                PrimaryCoverage = inputOrganizationProjectModel.PrimaryCoverage,
                ImplementationTime = inputOrganizationProjectModel.ImplementationTime,
                ProjectFunds = inputOrganizationProjectModel.ProjectFunds,
                SourceFunds = inputOrganizationProjectModel.SourceFunds,
                Remarks = inputOrganizationProjectModel.Remarks,
                Registrant = inputOrganizationProjectModel.Registrant,
                RegistrationDate = inputOrganizationProjectModel.RegistrationDate,
                AttachmentName = inputOrganizationProjectModel.AttachmentName,
                AttachmentPath = inputOrganizationProjectModel.AttachmentPath,
                Street = Guid.Parse(inputOrganizationProjectModel.Street.ToString()),
                Station = Guid.Parse(inputOrganizationProjectModel.Station.ToString())
            };
            await _organizationProjectService.UpdateAsync(organizationProjectDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDetailById")]
        [HttpGet]
        public async Task<JsonResult> GetOrganizationById(Guid id)
        {
            var result = await _organizationProjectService.GetAsync(id);
            result.ProjectCategoryName = _dataDictionaryService.GetDataName(result.ProjectCategory);
            result.StreetName = _streetService.GetStreetName(result.Street);
            result.StationName = _stationService.GetStationName(result.Station);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }




    }
}