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
using CoreSolution.WebApi.Models.ServiceAgency;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ServiceAgency")]
    public class ServiceAgencyController : ControllerBase
    {
        private readonly IServiceAgencyService _serviceAgencyService;
        private readonly IDataDictionaryService _dataDictionaryService;
        public ServiceAgencyController(IServiceAgencyService serviceAgencyService, IDataDictionaryService dataDictionaryService)
        {
            _serviceAgencyService = serviceAgencyService;
            _dataDictionaryService = dataDictionaryService;
        }

        /// <summary>
        /// 获取服务列表(根据传递不同的参数值做查询)
        /// </summary>
        /// <param name="SRSM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetListData")]
        [HttpGet]
        public async Task<JsonResult> GetListData(SearchServiceAgencyModel SRSM, int pageIndex = 1, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<ServiceAgency, bool>> where = p =>
                 (string.IsNullOrEmpty(SRSM.AgencyName) || p.AgencyName.Contains(SRSM.AgencyName)) &&
                 (SRSM.AgencyCategory == null || p.AgencyCategory == SRSM.AgencyCategory) && (SRSM.StreetId == default(Guid) || p.StreetId == SRSM.StreetId) && (SRSM.PostStationId ==default(Guid) || p.PostStationId == SRSM.PostStationId);
            var result = await _serviceAgencyService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            if (result != null)
            {
                foreach (var item in result.Item2)
                {
                    item.AgencyCategoryName = _dataDictionaryService.GetItemNameById(item.AgencyCategory);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputServiceAgencyModel> { Total = result.Item1, List = Mapper.Map<IList<OutputServiceAgencyModel>>(result.Item2) });
        }
        /// <summary>
        /// 新增一条服务机构信息
        /// </summary>
        /// <param name="inputServiceAgencyModel"></param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> AddServiceAgency(InputServiceAgencyModel inputServiceAgencyModel)
        {
            var serviceAgencyDto = new ServiceAgencyDto
            {
                AgencyName=inputServiceAgencyModel.AgencyName,
                AgencyCategory=inputServiceAgencyModel.AgencyCategory,
                AgencyLeader=inputServiceAgencyModel.AgencyLeader,
                ContactAddress=inputServiceAgencyModel.ContactAddress,
                ContactPhone=inputServiceAgencyModel.ContactPhone,
                Description=inputServiceAgencyModel.Description,
                SaImgName = inputServiceAgencyModel.SaImgName,
                SaImgPath = inputServiceAgencyModel.SaImgPath,
                QualificationsName = inputServiceAgencyModel.QualificationsName,
                QualificationsPath = inputServiceAgencyModel.QualificationsPath,
                StreetId=inputServiceAgencyModel.StreetId,
                StreetName=inputServiceAgencyModel.StreetName,
                PostStationId=inputServiceAgencyModel.PostStationId,
                PostStationName=inputServiceAgencyModel.PostStationName
            };
           // var serviceAgencyDto = Mapper.Map<ServiceAgencyDto>(inputServiceAgencyModel);
            await _serviceAgencyService.InsertAsync(serviceAgencyDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
        }
        /// <summary>
        /// 修改一条服务机构信息
        /// </summary>
        /// <param name="inputServiceAgencyModel"></param>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> EditServiceAgency(InputServiceAgencyModel inputServiceAgencyModel)
        {
            var info = await _serviceAgencyService.GetAsync(Guid.Parse(inputServiceAgencyModel.Id.ToString()));
            info.AgencyName = inputServiceAgencyModel.AgencyName;
            info.AgencyCategory = inputServiceAgencyModel.AgencyCategory;
            info.AgencyLeader = inputServiceAgencyModel.AgencyLeader;
            info.ContactAddress = inputServiceAgencyModel.ContactAddress;
            info.ContactPhone = inputServiceAgencyModel.ContactPhone;
            info.Description = inputServiceAgencyModel.Description;
            info.SaImgName = inputServiceAgencyModel.SaImgName;
            info.SaImgPath = inputServiceAgencyModel.SaImgPath;
            info.QualificationsName = inputServiceAgencyModel.QualificationsName;
            info.QualificationsPath = inputServiceAgencyModel.QualificationsPath;
            info.StreetId = inputServiceAgencyModel.StreetId;
            info.StreetName = inputServiceAgencyModel.StreetName;
            info.PostStationId = inputServiceAgencyModel.PostStationId;
            info.PostStationName = inputServiceAgencyModel.PostStationName;
            await _serviceAgencyService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDataById")]
        [HttpGet]
        public async Task<JsonResult> GetServiceAgencyById(Guid id)
        {
            var result = await _serviceAgencyService.GetAsync(id);
            if (result.AgencyCategory != default(Guid))
            {
                result.AgencyCategoryName = _dataDictionaryService.GetDataName(result.AgencyCategory);
            }
            else
            {
                result.AgencyCategoryName = "";
            }
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
            var info = await _serviceAgencyService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该服务机构不存在");
            }
            await _serviceAgencyService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 获取所有的服务机构集合GetServiceAgencyListBy
        /// </summary>
        /// <returns></returns>
        [Route("GetServiceAgencyList")]
        [HttpGet]
        public async Task<JsonResult> GetServiceAgencyList()
        {
            var model = await _serviceAgencyService.GetAllListAsync();
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputServiceAgencyModel> { Total = model.Count, List = Mapper.Map<IList<OutputServiceAgencyModel>>(model) });
        }


        /// <summary>
        /// 获取所有的服务机构集合
        /// </summary>
        /// <returns></returns>
        [Route("GetServiceAgencyListBy")]
        [HttpGet]
        public async Task<JsonResult> GetServiceAgencyListBy(Guid? StreetId,Guid? StationId)
        {
            Expression<Func<ServiceAgency, bool>> where = p =>
                 (StreetId == null || p.StreetId == StreetId) &&
                 (StationId == null || p.PostStationId == StationId);

            var model = await _serviceAgencyService.GetAllListAsync(where);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputServiceAgencyModel> { Total = model.Count, List = Mapper.Map<IList<OutputServiceAgencyModel>>(model) });
        }



    }
}