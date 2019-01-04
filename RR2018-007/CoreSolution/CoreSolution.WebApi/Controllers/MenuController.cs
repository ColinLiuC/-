using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.IService;
using CoreSolution.WebApi.Models.DataDictionary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using System.Net;
using CoreSolution.Dto;
using Microsoft.AspNetCore.Cors;
using CoreSolution.WebApi.Models;
using System.Data;
using CoreSolution.WebApi.Models.Menu;
using CoreSolution.WebApi.Manager;
using AutoMapper;

namespace CoreSolution.WebApi.Controllers
{
    /// <summary>
    /// 菜单操作控制器
    /// </summary>
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Menu")]
    //[CheckAuthorize("Admin")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IPermissionService _permissionService;


        public MenuController(IMenuService menuService,IPermissionService permissionService)
        {
            _menuService = menuService;
            _permissionService = permissionService;
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputMenuModel inputModel)
        {
            if (inputModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "名称不能为空");
            }
            var dto = await _menuService.GetAsync(inputModel.Id);


            dto.Name = inputModel.Name;
            dto.PermissionTarget = inputModel.PermissionTarget;
            dto.Url = inputModel.Url;
            dto.Icon = inputModel.Icon;
            dto.OrderIn = inputModel.OrderIn;
            dto.LastModificationTime = DateTime.Now;

            if (!String.IsNullOrEmpty(inputModel.PermissionTarget))
            {
                var permissions = _permissionService.GetAll().Where(p => p.TargetName == inputModel.PermissionTarget);
                if (permissions == null || permissions.Count() == 0)
                {
                    PermissionDto pdto = new PermissionDto
                    {

                        DisplayName = inputModel.Name,
                        TargetName = inputModel.PermissionTarget,
                        Description = "菜单权限",
                        permissionType =1
                       
                    };
                    _permissionService.Insert(pdto);

                }

            }
            _menuService.Update(dto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功", null);
        }


        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Add(InputMenuModel inputModel)
        {
            if (inputModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "名称不能为空");
            }
            //检查用户名是否重复
            bool isExist = await _menuService.CheckIfExist(inputModel.Name, inputModel.ParentId);
            if (isExist)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.Found, "改数据字典已存在");
            }
            var menuDto = new MenuDto
            {
                Name = inputModel.Name,
                PermissionTarget = inputModel.PermissionTarget,
                Icon = inputModel.Icon,
                OrderIn = inputModel.OrderIn,
                Url = inputModel.Url,
                ParentId = inputModel.ParentId
            };

            var id = await _menuService.InsertAndGetIdAsync(menuDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }

        [Route("getMenu")]
        [HttpGet]
        public async Task<JsonResult> GetMenu(Guid menuId)
        {
            var result = await _menuService.GetAsync(menuId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
       

        [Route("InsertTree")]
        [HttpPost]
        public async Task<JsonResult> InsertTree(Guid pid, string name)
        {
            bool isExist = await _menuService.CheckIfExist(name, pid);
            if (isExist)
                return AjaxHelper.JsonResult(HttpStatusCode.Found, "改菜单已存在");
            else
            {
                var menuDto = new MenuDto
                {
                    Name = name,
                    ParentId = pid
                };

                var id = await _menuService.InsertAndGetIdAsync(menuDto);

                return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
            }

        }

       

        [Route("LoadTree")]
        [HttpGet]
        public async Task<JsonResult> LoadTree()
        {
            var treeNodes = new List<TreeNode>();
            var result = _menuService.GetAllMenusForTree();
            if (result != null && result.Tables[0] != null)
            {
                foreach (DataRow row in result.Tables[0].Rows)
                {
                    var treeNode = new TreeNode()
                    {
                        showEdit = false,
                        showRemove = false
                    };
                    treeNode.id = row["Id"].ToString();
                    treeNode.parentid = row["ParentId"] == null ? "0" : row["ParentId"].ToString();
                    treeNode.name = row["Name"].ToString();
                    treeNode.isParent = int.Parse(row["ChildrenNumber"].ToString()) > 0 ? "true" : "false";
                    treeNodes.Add(treeNode);
                }
            }

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", treeNodes);
        }
        [Route("NodeView")]
        [HttpGet]
        public async Task<JsonResult> NodeView(Guid pid, string Name, int pageIndex = 0)
        {
            var treeNodes = new List<TreeNode>();
            var result = await _menuService.GetMenusPage(pid, Name, pageIndex, 10);

            var listModel = new ListModel<MenuDto>();
            listModel.Total = result.Item1;
            listModel.List = result.Item2;

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", listModel);
        }


        [Route("GetTreeSetting")]
        [HttpGet]
        public JsonResult GetTreeSetting()
        {
            TreeSetting treeSetting = new TreeSetting();
            treeSetting.ShowDelete = false;
            treeSetting.ShowCheck = false;
            //addHoverDom总开关, ShowEdit和ShowDelete无效果, 需要在TreeNode单独设置
            treeSetting.ShowAdd = true;
            treeSetting.ShowEdit = false;
            treeSetting.ShowParentTreeData = true;
            treeSetting.IsAsync = false;
            treeSetting.TreeName = "菜单表";
            treeSetting.ShowTreeData = true;

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", treeSetting);
        }

        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="menuId">权限Id</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid menuId)
        {
            var menu = await _menuService.GetAsync(menuId);
            if (menu == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该菜单不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            menu.DeleterUserId = currentUserId;
            menu.DeletionTime = DateTime.Now;
            await _menuService.DeleteAsync(menu);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        [Route("GetMenuAll")]
        [HttpGet]
        public async Task<JsonResult> GetMenuAll()
        {
            var result = await _menuService.GetAllListAsync(p=>p.OrderIn,true);
            var Outmodel = new List<OutputMenuModel>();
            var FirstRoot =  _menuService.GetAll().Where(p => p.Name == "Root").FirstOrDefault();
            var basemenu = result.Where(m => m.ParentId ==FirstRoot.Id).ToList();
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            var permissions = (await LoginManager.GetCurrentUserPermissionsAsync(currentUserId)).ToArray();

            foreach (var item in basemenu)
            {
                if (!permissions.Contains(item.PermissionTarget))
                {

                    continue;
                }
                var od = new OutputMenuModel();
                od = Mapper.Map<OutputMenuModel>(item);
                od.MenuItems = new List<OutputMenuModel>();
                var secondList = result.Where(m => m.ParentId == item.Id).ToList();
                if (secondList != null)
                {
                    foreach (var item1 in secondList)
                    {
                        if (!permissions.Contains(item1.PermissionTarget))
                        {
                            continue;
                        }
                        var sod = new OutputMenuModel();
                        sod = Mapper.Map<OutputMenuModel>(item1);
                        sod.MenuItems = new List<OutputMenuModel>();
                        var thirdList = result.Where(m => m.ParentId == item1.Id).ToList();
                        if (thirdList != null)
                        {
                            foreach (var item2 in thirdList)
                            {
                                if (!permissions.Contains(item2.PermissionTarget))
                                {
                                    continue;
                                }
                                var tod = new OutputMenuModel();
                                tod = Mapper.Map<OutputMenuModel>(item2);
                                sod.MenuItems.Add(tod);
                            }
                        }
                        od.MenuItems.Add(sod);

                    }

                }
                Outmodel.Add(od);

            }
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", Outmodel);
        }
    }
}