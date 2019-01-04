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
using CoreSolution.Service;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.QuarterProject;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/QuarterProject")]
    public class QuarterProjectController : ControllerBase
    {
        private readonly IQuarterProjectService _quarterProjectService;
        private readonly IDataDictionaryService _dataDictionaryService;

        public QuarterProjectController(IQuarterProjectService quarterProjectService, IDataDictionaryService dataDictionaryService)
        {
            _quarterProjectService = quarterProjectService;
            _dataDictionaryService = dataDictionaryService;
        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputQuarterProjectModel model)
        {
            if (!ModelState.IsValid)
            {
                var errorlist = new List<ErrorModel>();
                //获取所有错误的Key
                List<string> Keys = ModelState.Keys.ToList();
                //获取每一个key对应的ModelStateDictionary
                foreach (var key in Keys)
                {
                    var errors = ModelState[key].Errors.ToList();
                    //将错误描述添加到sb中
                    foreach (var error in errors)
                    {
                        errorlist.Add(new ErrorModel() { Name = key, ErrorMsg = error.ErrorMessage });
                    }
                }
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "数据格式不正确", errorlist);
            }
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<QuarterProjectDto>(model);
            var id = await _quarterProjectService.InsertAndGetIdAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var infoDto = await _quarterProjectService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区项目不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _quarterProjectService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <param name="id">id</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputQuarterProjectModel model, Guid id)
        {
            var infoDto = await _quarterProjectService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区项目不存在");
            }
            infoDto = Mapper.Map<QuarterProjectDto>(model);
            infoDto.Id = id;
            await _quarterProjectService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 根据Id获取单条信息。200获取成功;404未找到
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _quarterProjectService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区项目不存在", result);
            }
            StreetService streetService = new StreetService();
            JuWeiService juweiService = new JuWeiService();
            StationService stationService = new StationService();
            result.StreetName = streetService.GetStreetName(result.StreetId);
            result.JuWeiName = juweiService.GetJuWeiName(result.JuWeiId);
            result.StationName = stationService.GetStationName(result.StationId);
            result.ProjectTypeStr = _dataDictionaryService.GetItemNameById(result.ProjectType);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <returns></returns>
        [Route("getQuarterProjects")]
        [HttpGet]
        public async Task<JsonResult> GetQuarterProjects(SearchQuarterProjectModel model)
        {
            //拼接过滤条件
            Expression<Func<QuarterProject, bool>> where = p =>
                 (string.IsNullOrEmpty(model.Name) || p.Name.Contains(model.Name)) &&
                 (string.IsNullOrEmpty(model.Exploiting) || p.Exploiting.Contains(model.Exploiting)) &&
                (model.StreetId == null || p.StreetId == model.StreetId) &&
                (model.StationId == null || p.StationId == model.StationId) &&
                (model.JuWeiId == null || p.JuWeiId == model.JuWeiId) &&
                 (model.ProjectType == null || p.ProjectType == model.ProjectType) &&
                 (model.ExploitingTime_Start == null || p.ExploitingTime_Start <= model.ExploitingTime_Start) &&
                 (model.ExploitingTime_End == null || p.ExploitingTime_End >= model.ExploitingTime_End) &&
                 (model.DeclareTime_Start == null || p.DeclareTime >= model.DeclareTime_Start) &&
                 (model.DeclareTime_End == null || p.DeclareTime <= model.DeclareTime_End);

            var result = await _quarterProjectService.GetPagedAsync(where, i => i.CreationTime, model.pageIndex, model.pageSize, true);

            if (result != null)
            {
                foreach (var item in result.Item2)
                {
                    item.ProjectTypeStr = _dataDictionaryService.GetItemNameById(item.ProjectType);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputQuarterProjectModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputQuarterProjectModel>>(result.Item2)
            });
        }

    }
}