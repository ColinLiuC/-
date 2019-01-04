using System;
using System.Collections.Generic;
using System.IO;
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
using CoreSolution.WebApi.Models.Street;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Street")]

    public class StreetController : Controller
    {
        private readonly IStreetService _iStreetService;

        public StreetController(IStreetService iStreetService)
        {
            _iStreetService = iStreetService;
        }

        /// <summary>
        /// 新增街道
        /// </summary>
        /// <param name="inputStreetModel">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputStreetModel inputStreetModel)
        {
            if (string.IsNullOrWhiteSpace(inputStreetModel.StreetName))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "街道名称不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            var streetDto = new StreetDto
            {
                StreetName = inputStreetModel.StreetName,
                StreetAddress = inputStreetModel.StreetAddress,
                StreetPeople = inputStreetModel.StreetPeople,
                StreetTell = inputStreetModel.StreetTell,
                StreetInfo = inputStreetModel.StreetInfo,
                StreetImg = inputStreetModel.StreetImg,
                StreetPaths = inputStreetModel.StreetPaths,
                Lat = inputStreetModel.Lat,
                Lng = inputStreetModel.Lng,
            };
            var id = await _iStreetService.InsertAndGetIdAsync(streetDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }


        /// <summary>
        /// 删除街道
        /// </summary>
        /// <param name="streetId">街道Id</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid streetId)
        {
            var streetDto = await _iStreetService.GetAsync(streetId);
            if (streetDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该街道不存在");
            }
            streetDto.DeletionTime = DateTime.Now;
            await _iStreetService.DeleteAsync(streetDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 更新街道
        /// </summary>
        /// <param name="inputStreetModel"></param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputStreetModel inputStreetModel)
        {
            var info = await _iStreetService.GetAsync(Guid.Parse(inputStreetModel.Id.ToString()));
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该街道不存在");
            }
            info.StreetName = inputStreetModel.StreetName;
            info.StreetAddress = inputStreetModel.StreetAddress;
            info.StreetPeople = inputStreetModel.StreetPeople;
            info.StreetTell = inputStreetModel.StreetTell;
            info.StreetInfo = inputStreetModel.StreetInfo;
            info.StreetImg = inputStreetModel.StreetImg;
            info.StreetPaths = inputStreetModel.StreetPaths;
            info.Id = Guid.Parse(inputStreetModel.Id.ToString());
            info.LastModificationTime = DateTime.Now;
            info.Lat = inputStreetModel.Lat;
            info.Lng = inputStreetModel.Lng;
            await _iStreetService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }


        /// <summary>
        /// 获取街道详细
        /// </summary>
        /// <param name="streetId">街道Id</param>
        /// <returns></returns>
        [Route("getStreetById")]
        [HttpGet]
        public async Task<JsonResult> GetStreetById(Guid streetId)
        {
            var result = await _iStreetService.GetAsync(streetId);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该街道不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <param name="streetname">街道名称</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("getStreePaged")]
        [HttpGet]
        public async Task<JsonResult> GetStreePaged(string streetname, int pageIndex = 1, int pageSize = 10)
        {
            string filePath = @"E:\poststationLogs.txt";
            try
            {
                //拼接过滤条件
                Expression<Func<Street, bool>> where = p =>
                     (string.IsNullOrEmpty(streetname) || p.StreetName.Contains(streetname));
                var result = await _iStreetService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputStreetModel>
                { Total = result.Item1, List = Mapper.Map<IList<OutputStreetModel>>(result.Item2) });
            }
            catch (Exception ex)
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Append))
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.Message);
                }
                return null;
            }
        }

    }
}