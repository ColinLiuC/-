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
using CoreSolution.WebApi.Models.ServiceEvaluation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ServiceEvaluation")]
    public class ServiceEvaluationController : Controller
    {
        private readonly IServiceEvaluationService _serviceEvaluationService;
        private readonly IServiceApplication _rerviceApplication;
        private readonly IReceptionService _receptionService;
        public ServiceEvaluationController(IServiceEvaluationService serviceEvaluationService, IServiceApplication serviceApplication, IReceptionService receptionService)
        {
            _serviceEvaluationService = serviceEvaluationService;
            _rerviceApplication = serviceApplication;
            _receptionService = receptionService;
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputServiceEvaluationModel inputServiceEvaluationModel)
        {
            var receptionService=_receptionService.Get(inputServiceEvaluationModel.ServiceGuid);
            if (receptionService!=null)
            {
                var rerviceApplication= _rerviceApplication.Get(receptionService.Id);
                if (rerviceApplication!=null)
                {
                    rerviceApplication.CurrentState = 4;
                   await _rerviceApplication.UpdateAsync(rerviceApplication);
                }
                
            }
            var serviceEvaluationDto = Mapper.Map<ServiceEvaluationDto>(inputServiceEvaluationModel);
            var id = await _serviceEvaluationService.InsertAndGetIdAsync(serviceEvaluationDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }
        /// <summary>
        /// 获取某个服务下所有的评价信息
        /// </summary>
        /// <returns></returns>
        [Route("GetEvaluationList")]
        [HttpGet]
        public async Task<JsonResult> GetListData(Guid serviceId)
        {
            var result = await _serviceEvaluationService.GetAllListAsync(p => p.ServiceGuid == serviceId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputServiceEvaluationModel> { Total = result.Count, List = Mapper.Map<IList<OutputServiceEvaluationModel>>(result) });
        }
    }
}