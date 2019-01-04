using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ProjectServiceRecord;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ProjectServiceRecord")]
    public class ProjectServiceRecordController : ControllerBase
    {
        private readonly IProjectServiceRecordService _projectServiceRecordService;
        private readonly IDataDictionaryService _dataDictionaryService;
        private readonly IStreetService _streetService;
        private readonly IStationService _stationService;

        public ProjectServiceRecordController(IProjectServiceRecordService projectServiceRecordService, IDataDictionaryService dataDictionaryService,IStreetService streetService,IStationService stationService)
        {
            _projectServiceRecordService = projectServiceRecordService;
            _dataDictionaryService = dataDictionaryService;
            _streetService = streetService;
            _stationService = stationService;
        }
        /// <summary>
        /// 获取列表根据项目Id
        /// </summary>
        /// <param name="projectId">参数</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetListData")]
        [HttpGet]
        public async Task<JsonResult> GetListByParam(Guid projectId, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _projectServiceRecordService.GetPagedAsync(p=>p.ProjectId==projectId, i => i.CreationTime, pageIndex, pageSize, true);
            if (result.Item2.Count>0) {
                foreach (var item in result.Item2)
                {
                    item.ServiceTypeName = _dataDictionaryService.GetDataName(item.ServiceType);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputProjectServiceRecordModel> { Total = result.Item1, List = Mapper.Map<IList<OutputProjectServiceRecordModel>>(result.Item2) });
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputProjectServiceRecordModel inputProjectServiceRecordModel)
        {
            if (inputProjectServiceRecordModel.ServiceType == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择服务类型");
            }
            if (inputProjectServiceRecordModel.Street == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属街道");
            }
            if (inputProjectServiceRecordModel.Station == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属驿站");
            }
            var projectServiceRecordDto = Mapper.Map<ProjectServiceRecordDto>(inputProjectServiceRecordModel);
            await _projectServiceRecordService.InsertAsync(projectServiceRecordDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDataById")]
        [HttpGet]
        public async Task<JsonResult> GetDataById(Guid id)
        {
            var result = await _projectServiceRecordService.GetAsync(id);
            result.ServiceTypeName = _dataDictionaryService.GetDataName(result.ServiceType);
            result.StreetName = _streetService.GetStreetName(result.Street);
            result.StationName = _stationService.GetStationName(result.Station);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _projectServiceRecordService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务信息不存在");
            }
            await _projectServiceRecordService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputProjectServiceRecordModel inputProjectServiceRecordModel, Guid Id)
        {
            if (inputProjectServiceRecordModel.ServiceType == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择服务类型");
            }
            if (inputProjectServiceRecordModel.Street == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属街道");
            }
            if (inputProjectServiceRecordModel.Station == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "请选择所属驿站");
            }
            var projectServiceRecordDto = new ProjectServiceRecordDto
            {
                Id = Id,//获取这个可空值的默认值
                ProjectId = inputProjectServiceRecordModel.ProjectId,
                ServicePlace = inputProjectServiceRecordModel.ServicePlace,
                ServiceDate = inputProjectServiceRecordModel.ServiceDate,
                ServiceType = Guid.Parse(inputProjectServiceRecordModel.ServiceType.ToString()),
                ChargePerson = inputProjectServiceRecordModel.ChargePerson,
                ContactNumber = inputProjectServiceRecordModel.ContactNumber,
                ServiceNumber = inputProjectServiceRecordModel.ServiceNumber,
                ServiceInfo = inputProjectServiceRecordModel.ServiceInfo,
                ServicePingjia = inputProjectServiceRecordModel.ServicePingjia,
                Registrant = inputProjectServiceRecordModel.Registrant,
                RegistrationDate = inputProjectServiceRecordModel.RegistrationDate,
                AttachmentName = inputProjectServiceRecordModel.AttachmentName,
                AttachmentPath = inputProjectServiceRecordModel.AttachmentPath,
                Street = Guid.Parse(inputProjectServiceRecordModel.Street.ToString()),
                Station = Guid.Parse(inputProjectServiceRecordModel.Station.ToString())
            };
            await _projectServiceRecordService.UpdateAsync(projectServiceRecordDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }


    }
}