using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Service;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Helper;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.Quarters;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Quarters")]
    public class QuartersController : Controller
    {
        private readonly IQuartersService _quartersService;
        public QuartersController(IQuartersService quartersService)
        {
            _quartersService = quartersService;
        }


        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="inputQuartersModel">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputQuartersModel inputQuartersModel)
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
            var infoDto = Mapper.Map<QuartersDto>(inputQuartersModel);
            var id = await _quartersService.InsertAndGetIdAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }


        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">事项ID</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var infoDto = await _quartersService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _quartersService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="inputQuartersModel">输入参数model</param>
        /// <param name="id">小区id</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputQuartersModel inputQuartersModel, Guid id)
        {
            var infoDto = await _quartersService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区不存在");
            }
            infoDto = Mapper.Map<QuartersDto>(inputQuartersModel);
            infoDto.Id = id;
            await _quartersService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }




        /// <summary>
        /// 根据Id获取小区。200获取成功;404未找到
        /// </summary>
        /// <param name="id">小区Id</param>
        /// <returns></returns>
        [Route("getQuartersById")]
        [HttpGet]
        public async Task<JsonResult> GetQuartersById(Guid id)
        {
            var result = await _quartersService.GetAsync(id);
            StreetService streetService = new StreetService();
            JuWeiService juweiService = new JuWeiService();
            StationService stationService = new StationService();
            result.StreetName = streetService.GetStreetName(result.StreetId);
            result.JuWeiName = juweiService.GetJuWeiName(result.JuWeiId);
            result.StationName = stationService.GetStationName(result.StationId);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该小区不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="address">地址</param>
        /// <param name="streetid">所属街道</param>
        /// <param name="stationid">所属驿站</param>
        /// <param name="juweiid">所属居委</param>
        /// <param name="completedYear">竣工年份</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("getQuarters")]
        [HttpGet]
        public async Task<JsonResult> GetQuarters(string name, string address, Guid? streetid, Guid? stationid, Guid? juweiid, string completedYear, int pageIndex = 1, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<Quarters, bool>> where = p =>
                 (string.IsNullOrEmpty(name) || p.Name.Contains(name)) &&
               (string.IsNullOrEmpty(address) || p.Address.Contains(address)) &&
               (streetid == null || p.StreetId == streetid) &&
               (stationid == null || p.StationId == stationid) &&
               (juweiid == null || p.JuWeiId == juweiid) &&
               (string.IsNullOrEmpty(completedYear) || p.CompletedYear == completedYear);
            var result = await _quartersService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);

            #region 处理list
            if (result != null && result.Item2.Count > 0)
            {
                StreetService streetService = new StreetService();
                JuWeiService juweiService = new JuWeiService();
                StationService stationService = new StationService();

                foreach (var item in result.Item2)
                {
                    item.StreetName = streetService.GetStreetName(item.StreetId);
                    item.JuWeiName = juweiService.GetJuWeiName(item.JuWeiId);
                    item.StationName = stationService.GetStationName(item.StationId);
                }
            }
            #endregion
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputQuartersModel>
            { Total = result.Item1, List = Mapper.Map<IList<OutputQuartersModel>>(result.Item2) });
        }

        #region 执行sql语句

        //[Route("loadAllQuarters")]
        //[HttpGet]
        //public JsonResult LoadAllQuarters()
        //{
        //    var inputQuartersModel = new List<InputQuartersModel>();
        //    var result = _quartersService.GetAllQuarters();
        //    if (result != null && result.Tables[0] != null)
        //    {
        //        foreach (DataRow row in result.Tables[0].Rows)
        //        {
        //            var model = new InputQuartersModel()
        //            {
        //                Name = row["Name"].ToString(),
        //                Address = row["Address"].ToString(),
        //                CompletedYear = row["CompletedYear"].ToString(),
        //            };
        //            inputQuartersModel.Add(model);
        //        }
        //    }
        //    return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", inputQuartersModel);
        //}
        #endregion

        #region 导出Excel

    
        #endregion
    }
}