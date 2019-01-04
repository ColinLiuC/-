using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.DesireManagement;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Dto;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/DesireManagement")]
    public class DesireManagementController : Controller
    {
        private readonly IDesireManagementService _desireManagementService;
        private readonly IDataDictionaryService _dataDictionaryService;
        public DesireManagementController(IDesireManagementService desireManagementService, IDataDictionaryService dataDictionaryService)
        {
            _desireManagementService = desireManagementService;
            _dataDictionaryService = dataDictionaryService;
        }
        /// <summary>
        /// 获取活动信息列表根据传递不同的参数
        /// </summary>
        /// <param name="SDM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetDataByParam")]
        [HttpGet]
        public async Task<JsonResult> GetListByParam(SearchDesireManagementModel SDM, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<DesireManagement, bool>> where = p =>
                   (string.IsNullOrEmpty(SDM.DesireName) || p.DesireName.Contains(SDM.DesireName)) &&
                   (SDM.DesireCategory == default(Guid) || p.DesireCategory == SDM.DesireCategory) &&
                   (SDM.StreetId == default(Guid) || p.StreetId == SDM.StreetId) &&
                   (SDM.PostStationId == default(Guid) || p.PostStationId == SDM.PostStationId);
            var result = await _desireManagementService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            if (result != null)
            {
                foreach (var item in result.Item2)
                {
                    item.DesireCategoryName = _dataDictionaryService.GetItemNameById(item.DesireCategory);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputDesireManagementModel> { Total = result.Item1, List = Mapper.Map<IList<OutputDesireManagementModel>>(result.Item2) });
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputDesireManagementModel inpuDesireManagementModel)
        {
            if (inpuDesireManagementModel.DesireName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "心愿名称不能为空");
            }
            inpuDesireManagementModel.CurrentState = 1;
            var desireManagementDto = Mapper.Map<DesireManagementDto>(inpuDesireManagementModel);
            var id = await _desireManagementService.InsertAndGetIdAsync(desireManagementDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDataById")]
        [HttpGet]
        public async Task<JsonResult> GetDesireManagementById(Guid id)
        {
            var result = await _desireManagementService.GetAsync(id);
            if (result.DesireCategory!=default(Guid))
            {
                result.DesireCategoryName = _dataDictionaryService.GetItemNameById(result.DesireCategory);
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
            var info = await _desireManagementService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该微心愿不存在");
            }
            info.DeletionTime = DateTime.Now;
            await _desireManagementService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputDesireManagementModel inpuDesireManagementModel)
        {
            var info = await _desireManagementService.GetAsync(Guid.Parse(inpuDesireManagementModel.Id.ToString()));
            info.Publisher = inpuDesireManagementModel.Publisher;
            info.ContactNumber = inpuDesireManagementModel.ContactNumber;
            info.DetailedAddress = inpuDesireManagementModel.DetailedAddress;
            info.StreetId = inpuDesireManagementModel.StreetId;
            info.StreetName = inpuDesireManagementModel.StreetName;
            info.JuWeiId = inpuDesireManagementModel.JuWeiId;
            info.PostStationId = inpuDesireManagementModel.PostStationId;
            info.DesireName = inpuDesireManagementModel.DesireName;
            info.DesireCategory = inpuDesireManagementModel.DesireCategory;
            info.DesireContent = inpuDesireManagementModel.DesireContent;
            info.ClaimPeriod = inpuDesireManagementModel.ClaimPeriod;
            info.ReportPerson = inpuDesireManagementModel.ReportPerson;
            info.ReportDate = inpuDesireManagementModel.ReportDate;
            await _desireManagementService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 认领心愿登记
        /// </summary>
        /// <returns></returns>
        [Route("claimWish")]
        [HttpPost]
        public async Task<JsonResult> ClaimWish(InputDesireManagementModel inpuDesireManagementModel)
        {
            var info = await _desireManagementService.GetAsync(Guid.Parse(inpuDesireManagementModel.Id.ToString()));
            info.Claimant = inpuDesireManagementModel.Claimant;
                info.ClaimantAddress = inpuDesireManagementModel.ClaimantAddress;
            info.ClaimantContactNumber = inpuDesireManagementModel.ClaimantContactNumber;
            info.ClaimantStreetId = inpuDesireManagementModel.ClaimantStreetId;
            info.ClaimantJuWeiId = inpuDesireManagementModel.ClaimantJuWeiId;
            info.ClaimSituation = inpuDesireManagementModel.ClaimSituation;
            info.ClaimDate = inpuDesireManagementModel.ClaimDate;
            info.CurrentState = 2;
            await _desireManagementService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 归档
        /// </summary>
        /// <returns></returns>
        [Route("GuiDang")]
        [HttpPost]
        public async Task<JsonResult> GuiDang(InputDesireManagementModel inpuDesireManagementModel)
        {
            var info = await _desireManagementService.GetAsync(Guid.Parse(inpuDesireManagementModel.Id.ToString()));
            info.RealizationSituation = inpuDesireManagementModel.RealizationSituation;
                info.EvaluationOpinion = inpuDesireManagementModel.EvaluationOpinion;
            info.Registrant = inpuDesireManagementModel.Registrant;
            info.RegistionDate = inpuDesireManagementModel.RegistionDate;
            await _desireManagementService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
    }
}