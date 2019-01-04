using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Interceptor;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.DataDictionary;
using CoreSolution.WebApi.Models.Role;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    /// <summary>
    /// 角色操作控制器
    /// </summary>
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Role")]
    //[CheckAuthorize("Admin")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;



        public RoleController(IRoleService roleService, IPermissionService permissionservice)
        {
            _roleService = roleService;
            _permissionService = permissionservice;
        }


        /// <summary>
        /// 新增角色。200成功，400角色名不能为空、角色必须具有至少一种权限
        /// </summary>
        /// <param name="inputRoleModel">角色参数model</param>
        /// <returns></returns>
        [Route("addNew")]
        [HttpPost]
        public async Task<JsonResult> AddNew(InputRoleModel inputRoleModel)
        {
            if (inputRoleModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "角色名不能为空");
            }

            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            var roleDto = new RoleDto
            {
                Name = inputRoleModel.Name,
                Description = inputRoleModel.Description,
                CreatorUserId = userId

            };
            roleDto.RolePermissions = inputRoleModel.Permissions.Select(i => new RolePermissionDto
            {
                RoleDto = roleDto,
                PermissionId = i
            }).ToList();
            var id = await _roleService.InsertAndGetIdAsync(roleDto);

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", id);
        }

        /// <summary>
        /// 删除角色。200删除成功，404该角色不存在
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid roleId)
        {
            var role = await _roleService.GetAsync(roleId);
            if (role == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该角色不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            role.DeleterUserId = currentUserId;
            role.DeletionTime = DateTime.Now;
            await _roleService.DeleteAsync(role);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 检查角色名是否重复。200成功，400角色名不能为空
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns>true存在，false不存在</returns>
        [Route("checkRoleNameDup")]
        [HttpPost]
        public async Task<JsonResult> CheckUserNameDup([FromBody]string roleName)
        {
            if (roleName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "角色名不能为空");
            }
            var result = await _roleService.CheckRoleNameDupAsync(roleName);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据Id获取角色。200获取成功
        /// </summary>
        /// <param name="roleId">角色Id</param>
        /// <returns></returns>
        [Route("getRole")]
        [HttpGet]
        public async Task<JsonResult> GetRoleById(Guid roleId)
        {
            var result = await _roleService.GetAsync(roleId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", Mapper.Map<OutputRoleModel>(result));
        }

        /// <summary>
        /// 获取角色列表。200获取成功
        /// </summary>
        /// <returns></returns>
        [Route("getRoleList")]
        [HttpGet]
        public async Task<JsonResult> GetRoleList()
        {
            var result = await _roleService.GetAllListAsync();
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputRoleModel> { Total = result.Count, List = Mapper.Map<IList<OutputRoleModel>>(result) });
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <returns></returns>
        [Route("getRolesPaged")]
        [HttpGet]
        public async Task<JsonResult> GetRolesPaged(string Name, int pageIndex, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<Role, bool>> where = p =>
                 (string.IsNullOrEmpty(Name) || p.Name.Contains(Name));
            var result = await _roleService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);



            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputRoleModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputRoleModel>>(result.Item2)
            });
        }

        /// <summary>
        /// 更新角色信息。400角色名不能为空，200成功
        /// </summary>
        /// <param name="inputRoleModel">角色参数model</param>
        /// <returns></returns>
        [Route("updateRoleInfo")]
        [HttpPost]
        public async Task<JsonResult> UpdateRoleInfo(InputRoleModel inputRoleModel)
        {
            try
            {

                if (inputRoleModel.Name.IsNullOrWhiteSpace())
                {
                    return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "角色名不能为空");
                }
                var roleDto = _roleService.Get(inputRoleModel.RoleId.GetValueOrDefault());
                roleDto.Name = inputRoleModel.Name;
                roleDto.Description = inputRoleModel.Description;
                var userRoleDtos = new List<UserRoleDto>();
                foreach (var userrole in roleDto.UserRoles)
                {
                    userRoleDtos.Add(new UserRoleDto { UserId = userrole.UserId, RoleId = userrole.RoleId });
                }
                roleDto.UserRoles = userRoleDtos;

                await _roleService.UpdateAsync(roleDto);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
            }
            catch (Exception ex)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, ex.Message);
            }
        }

        /// <summary>
        /// 更新普通角色信息。400角色名不能为空，200成功
        /// </summary>
        /// <param name="inputRoleModel">角色参数model</param>
        /// <returns></returns>
        [Route("UpdateOtherPermissions")]
        [HttpPost]
        public JsonResult UpdateOtherPermissions(InputRoleModel inputRoleModel)
        {
            var list = inputRoleModel.Permissions.ToList();
            _roleService.SetRolePermissions(inputRoleModel.RoleId.GetValueOrDefault(), list);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
        }


        [Route("GetCheckedNodeIds")]
        [HttpGet]
        public List<Guid> GetCheckedNodeIds(string checkednodes)
        {
            string checkedNodeIds = checkednodes;
            var list = new List<Guid>();
            if (!string.IsNullOrEmpty(checkedNodeIds))
            {
                var tmpList = checkedNodeIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
                list = tmpList.Select(t =>
                {
                    return new Guid(t);
                }).ToList();
            }
            return list;
        }

        [Route("CheckTree")]
        [HttpGet]
        public JsonResult CheckTree(string roleid, string checkednodes)
        {
            Guid roleId = Guid.Empty;
            if (!Guid.TryParse(roleid, out roleId))
            {
                //   throw new NopException("角色Id不合法!");
            }
            else
            {
                var checkNodes = GetCheckedNodeIds(checkednodes);

                _roleService.SetRoleHasMenuPermissions(roleId, checkNodes);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "菜单权限设置成功");
        }
        [Route("LoadTree")]
        [HttpGet]
        public async Task<JsonResult> LoadTree(Guid roleid)
        {



            var permissionIds = await _roleService.GetPermissionIdsAsync(roleid);


            var treeNodes = new List<TreeNode>();
            var result = _roleService.GetAllPermissionsForTree();
            if (result != null && result.Tables[0] != null)
            {
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    var treeNode = new TreeNode();

                    treeNode.id = row["Id"].ToString();
                    treeNode.parentid = row["ParentId"] == null ? "0" : row["ParentId"].ToString();
                    treeNode.name = row["Name"].ToString();
                    treeNode.isParent = int.Parse(row["ChildrenNumber"].ToString()) > 0 ? "true" : "false";
                    if (row["PermissionId"] == DBNull.Value || row["PermissionId"].ToString() == "")
                    {
                        continue;
                    }
                    treeNode.PermissionId = row["PermissionId"].ToString();
                    if (permissionIds.Contains(new Guid(row["PermissionId"].ToString())))
                    {
                        treeNode.check = "true";
                    }
                    treeNodes.Add(treeNode);
                }
            }

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", treeNodes);
        }
        [Route("GetTreeSetting")]
        [HttpGet]
        //重写设置，屏蔽删除按钮
        public async Task<JsonResult> GetTreeSetting()
        {
            TreeSetting ts = new TreeSetting();
            ts.ShowDelete = false;
            //ts.ShowTreeData = false;
            ts.ShowAdd = false;
            ts.ShowEdit = false;
            ts.IsAsync = false;
            //ts.ShowParentTreeData = true;
            ts.TreeName = "菜单";
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", ts);
        }




    }
}