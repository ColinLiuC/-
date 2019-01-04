using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Interceptor;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.Permission;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    /// <summary>
    /// 权限操作控制器
    /// </summary>
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Permission")]
    //[CheckAuthorize("Admin")]
    public class PermissionController : Controller
    {
        private readonly IPermissionService _permissionService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;

        public PermissionController(IPermissionService permissionService, IRoleService roleService,IUserService userService)
        {
            _permissionService = permissionService;
            _roleService = roleService;
            _userService = userService;
        }


        /// <summary>
        /// 新增权限。200成功，400权限名不能为空,404角色不存在
        /// </summary>
        /// <param name="inputPermissionModel">权限参数model</param>
        /// <returns></returns>
        [Route("addNew")]
        [HttpPost]
        public async Task<JsonResult> AddNew( InputPermissionModel inputPermissionModel)
        {
            if (inputPermissionModel.DisplayName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "权限名不能为空");
            } 
            if (inputPermissionModel.TargetName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "权限对应名不能为空");
            }

            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            var userDto =  _userService.Get(userId);
            var permissionDto = new PermissionDto
            {
                DisplayName = inputPermissionModel.DisplayName,
                TargetName= inputPermissionModel.TargetName,
                Description = inputPermissionModel.Description,
                CreatorUserId = userId
              
            };
            var id = await _permissionService.InsertAndGetIdAsync(permissionDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "注册成功", id);
        }

        /// <summary>
        /// 删除权限。200删除成功，404该权限不存在
        /// </summary>
        /// <param name="permissionId">权限Id</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid permissionId)
        {
            var permission = await _permissionService.GetAsync(permissionId);
            if (permission == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该权限不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            permission.DeleterUserId = currentUserId;
            permission.DeletionTime = DateTime.Now;
            await _permissionService.DeleteAsync(permission);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 检查权限名是否重复。200成功，400权限名不能为空
        /// </summary>
        /// <param name="permissionName">权限名</param>
        /// <returns>true存在，false不存在</returns>
        [Route("checkPermissionNameDup")]
        [HttpPost]
        public async Task<JsonResult> CheckUserNameDup(string permissionName)
        {
            if (permissionName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "权限名不能为空");
            }
            var result = await _permissionService.CheckPermissionNameDupAsync(permissionName);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据Id获取权限。200获取成功
        /// </summary>
        /// <param name="permissionId">权限Id</param>
        /// <returns></returns>
        [Route("getPermission")]
        [HttpGet]
        public async Task<JsonResult> GetPermissionById(Guid permissionId)
        {
            var result = await _permissionService.GetAsync(permissionId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", Mapper.Map<OutputPermissionModel>(result));
        }

        /// <summary>
        /// 分页获取权限列表。200获取成功
        /// </summary>
        /// <returns></returns>
        [Route("getPermissionsPaged")]
        [HttpGet]
        public async Task<JsonResult> GetPermissionsPaged(string key, int pageIndex = 1, int pageSize = 10)
        {
            var where = PredicateExtensions.True<Permission>();
            if (!key.IsNullOrWhiteSpace())
            {
                where = where.And(i => i.DisplayName.Contains(key));
            }
            var result = await _permissionService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputPermissionModel> { Total = result.Item1, List = Mapper.Map<IList<OutputPermissionModel>>(result.Item2) });
        }
        [Route("GetPermissionAll")]
        [HttpGet]
        public async Task<JsonResult> GetPermissionAll()
        {
            var result = await _permissionService.GetAllListAsync(p=>p.permissionType!=1);
            return AjaxHelper.JsonResult(HttpStatusCode.OK,"成功",result);
        }

        /// <summary>
        /// 更新权限信息。400 权限Id不能为空，权限名不能为空，200成功
        /// </summary>
        /// <param name="inputPermissionModel">权限参数model</param>
        /// <returns></returns>
        [Route("updatePermissionInfo")]
        [HttpPost]
        public async Task<JsonResult> UpdatePermissionInfo( InputPermissionModel inputPermissionModel)
        {
            if (inputPermissionModel.PermissionId == null || inputPermissionModel.PermissionId.Value.IsNullOrEmpty())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "权限Id不能为空");
            }
            if (inputPermissionModel.DisplayName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "权限名不能为空");
            }
            if (inputPermissionModel.TargetName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "权限对应名不能为空");
            }
            var permissionDto = new PermissionDto
            {
                Id = inputPermissionModel.PermissionId.GetValueOrDefault(),
               DisplayName = inputPermissionModel.DisplayName,
               TargetName =inputPermissionModel.TargetName,
                Description = inputPermissionModel.Description
            };
          
            await _permissionService.UpdateAsync(permissionDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
        }
    }
}