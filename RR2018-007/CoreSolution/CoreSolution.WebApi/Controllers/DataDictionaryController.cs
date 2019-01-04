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
using CoreSolution.WebApi.Manager;

namespace CoreSolution.WebApi.Controllers
{
    /// <summary>
    /// 数据字典操作
    /// </summary>
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/DataDictionary")]
    public class DataDictionaryController : Controller
    {

        private readonly IDataDictionaryService _dataDictionaryService;

        public DataDictionaryController(IDataDictionaryService dictionaryService)
        {
            _dataDictionaryService = dictionaryService;
        }

        [Route("Edit")]
        [HttpPost]
        public async Task<JsonResult> Edit([FromBody] InputDataDictionaryModel inputModel)
        {
            if (inputModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "名称不能为空");
            }
           var dto= await _dataDictionaryService.GetAsync(inputModel.Id);


            dto.Name = inputModel.Name;
            dto.Value = inputModel.Value;
            dto.Description = inputModel.Description;
            dto.Sort = inputModel.Sort;
            dto.CustomAttributes = inputModel.CustomAttributes;
            dto.LastModificationTime = DateTime.Now;
           
            _dataDictionaryService.Update(dto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功", null);
        }


        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Add([FromBody] InputDataDictionaryModel inputModel)
        {
            if (inputModel.Name.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "名称不能为空");
            }
            //检查用户名是否重复
            bool isExist = await _dataDictionaryService.CheckIfExist(inputModel.Name, inputModel.ParentId);
            if (isExist)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.Found, "改数据字典已存在");
            }
            var dictionaryDto = new DataDictionaryDto
            {
                Name = inputModel.Name,
                Value = inputModel.Value,
                Description = inputModel.Description,
                Sort = inputModel.Sort,
                Tips = inputModel.Tips,
                CustomType = inputModel.CustomType,
                CustomAttributes = inputModel.CustomAttributes,
                ParentId = inputModel.ParentId
            };

            var id = await _dataDictionaryService.InsertAndGetIdAsync(dictionaryDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }

        [Route("GetDictionaryById")]
        [HttpGet]
        public async Task<JsonResult> GetDictionaryById(Guid id)
        {
            var result = await _dataDictionaryService.GetAsync(id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        /// <summary>
        /// 根据名称获取，获取格式  上海>虹口
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        [Route("getDicByExpr")]
        [HttpGet]
        public async Task<JsonResult> GetDataDictionaryByExpression(String expression)
        {
            var result = await _dataDictionaryService.GetDataDictionaryByExpression(expression);

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        [Route("InsertTree")]
        [HttpPost]
        public async Task<JsonResult> InsertTree(Guid pid, string name)
        {
            bool isExist = await _dataDictionaryService.CheckIfExist(name, pid);
            if (isExist)
                return AjaxHelper.JsonResult(HttpStatusCode.Found, "改数据字典已存在");
            else
            {
                var dictionaryDto = new DataDictionaryDto
                {
                    Name = name,
                    ParentId = pid
                };

                var id = await _dataDictionaryService.InsertAndGetIdAsync(dictionaryDto);

                return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
            }

        }

        //public  Task<JsonResult> EditTree()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<JsonResult> DeleteTree()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<JsonResult> CheckTree()
        //{
        //    throw new NotImplementedException();
        //}

        [Route("LoadTree")]
        [HttpGet]
        public async Task<JsonResult> LoadTree()
        {
            var treeNodes = new List<TreeNode>();
            var result =  _dataDictionaryService.GetAllDataDictionarysForTree();
            if (result != null&& result.Tables[0]!=null)
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
                    treeNode.isParent =int.Parse(row["ChildrenNumber"].ToString()) > 0 ? "true" : "false";
                    treeNodes.Add(treeNode);
                }
            }

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", treeNodes);
        }
        [Route("NodeView")]
        [HttpGet]
        public async Task<JsonResult> NodeView(Guid pid,string Name, int pageIndex = 0)
        {
            var treeNodes = new List<TreeNode>();
            var result = await _dataDictionaryService.GetDataDictionaryPage( pid, Name, pageIndex, 10);

            var listModel = new ListModel<DataDictionaryDto>();
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
            treeSetting.TreeName = "数据字典";
            treeSetting.ShowTreeData = true;

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", treeSetting);
        }


        /// <summary>
        /// 删除数据字典
        /// </summary>
        /// <param name="dataDicId">权限Id</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid dataDicId)
        {
            var datadic = await _dataDictionaryService.GetAsync(dataDicId);
            if (datadic == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该数据字典不存在");
            }
            string token = HttpContext.Request.Headers["token"];
            var currentUserId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
      
            datadic.DeletionTime = DateTime.Now;
            await _dataDictionaryService.DeleteAsync(datadic);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }



    }
}