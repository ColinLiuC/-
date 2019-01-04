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
using CoreSolution.Tools;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.FixedAssets;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/FixedAssets")]
    public class FixedAssetsController : Controller
    {
        private readonly IFixedAssetsService _fixedAssetsService;

        public FixedAssetsController(IFixedAssetsService fixedAssetsService)
        {
            _fixedAssetsService = fixedAssetsService;
           
        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(FixedAssetsDto model)
        {
            if (string.IsNullOrWhiteSpace(model.Name))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "固定资产名称不能为空");
            }
            var id = await _fixedAssetsService.InsertAndGetIdAsync(model);
            if (id!=null&&id!=default(Guid))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功",model);
            }
            else
            {
                return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "添加失败");

            }
            
           
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid Id)
        {
            var residentWorkdto = await _fixedAssetsService.GetAsync(Id);
            if (residentWorkdto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该固定资产不存在");
            }
            residentWorkdto.DeletionTime = DateTime.Now;
            await _fixedAssetsService.DeleteAsync(residentWorkdto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(FixedAssetsDto model)
        {
            var dto = await _fixedAssetsService.GetAsync(model.Id);
            if (dto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该固定资产不存在");
            }
            dto.Number = model.Number;
            dto.Name = model.Name;
            dto.Version = model.Version;
            dto.DutyPeople = model.DutyPeople;
            dto.Telephone = model.Telephone;
            dto.PurchaseDate = model.PurchaseDate;
            dto.Description = model.Description;
            dto.Photo = model.Photo;
            dto.PhotoUrl = model.PhotoUrl;
            dto.RegisterPeople = model.RegisterPeople;
            dto.RegisterDate = model.RegisterDate;
            dto.StreetId = model.StreetId;
            dto.StationId = model.StationId;	
            var result= await _fixedAssetsService.UpdateAsync(dto);
            return result != null ? AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功") : AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "添加失败");
        }


        /// <summary>
        /// 根据Id获取事项。200获取成功;404未找到
        /// </summary>
        /// <param name="id">事项Id</param>
        /// <returns></returns>
        [Route("GetFixedAssetsById")]
        [HttpGet]
        public async Task<JsonResult> GetFixedAssetsById(Guid id)
        {
            var result = await _fixedAssetsService.GetFixedAssetsById(id);
          
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该固定资产不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 根据搜索条件查询办事
        /// </summary>
        /// <param name="model">搜索条件model</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("getFixedAssetsPaged")]
        [HttpGet]
        public async Task<JsonResult> GetFixedAssetsPaged(FixedAssetsDto model, int pageIndex = 1, int pageSize = 10)
        {
            var result = await _fixedAssetsService.GetFixedAssetsPagedAsync(model, pageIndex, pageSize);
            //格式化枚举数据
            if (result != null && result.Item2.Count > 0)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<FixedAssetsDto>
                {
                    Total = result.Item1,
                    List = result.Item2
                });
            }
            else
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "失败", new ListModel<FixedAssetsDto>());
            }
           
        }
    }
}