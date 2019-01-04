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
using CoreSolution.WebApi.Models.ProjectTrack;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ProjectTrack")]
    public class ProjectTrackController : ControllerBase
    {
        private readonly IProjectTrackService _projectTrackService;

        public ProjectTrackController(IProjectTrackService projectTrackService)
        {
            _projectTrackService = projectTrackService;
        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputProjectTrackModel model)
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
            var infoDto = Mapper.Map<ProjectTrackDto>(model);
            var id = await _projectTrackService.InsertAndGetIdAsync(infoDto);
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
            var infoDto = await _projectTrackService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该项目跟踪不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _projectTrackService.DeleteAsync(infoDto);
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
        public async Task<JsonResult> Modify(InputProjectTrackModel model, Guid id)
        {
            var infoDto = await _projectTrackService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该项目跟踪不存在");
            }
            infoDto = Mapper.Map<ProjectTrackDto>(model);
            infoDto.Id = id;
            await _projectTrackService.UpdateAsync(infoDto);
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
            var result = await _projectTrackService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该项目跟踪不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <returns></returns>
        [Route("getProjectTracks")]
        [HttpGet]
        public async Task<JsonResult> GetProjectTracks(SearchProjectTrackModel model)
        {
            //拼接过滤条件
            Expression<Func<ProjectTrack, bool>> where = p =>
                 (model.QuarterProjectId == null || p.QuarterProjectId == model.QuarterProjectId);
            var result = await _projectTrackService.GetPagedAsync(where, i => i.CreationTime, model.pageIndex, model.pageSize, true);

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputProjectTrackModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputProjectTrackModel>>(result.Item2)
            });
        }

    }
}