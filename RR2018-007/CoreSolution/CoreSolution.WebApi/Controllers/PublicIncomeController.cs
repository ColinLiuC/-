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
using CoreSolution.WebApi.Models.PublicIncome;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/PublicIncome")]
    public class PublicIncomeController : ControllerBase
    {
        private readonly IPublicIncomeService _publicIncomeService;
        public PublicIncomeController(IPublicIncomeService publicIncomeService)
        {
            _publicIncomeService = publicIncomeService;
        }

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputPublicIncomeModel model)
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
            var infoDto = Mapper.Map<PublicIncomeDto>(model);
            var id = await _publicIncomeService.InsertAndGetIdAsync(infoDto);
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
            var infoDto = await _publicIncomeService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该公共收益不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _publicIncomeService.DeleteAsync(infoDto);
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
        public async Task<JsonResult> Modify(InputPublicIncomeModel model, Guid id)
        {
            var infoDto = await _publicIncomeService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该公共收益不存在");
            }
            infoDto = Mapper.Map<PublicIncomeDto>(model);
            infoDto.Id = id;
            await _publicIncomeService.UpdateAsync(infoDto);
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
            var result = await _publicIncomeService.GetAsync(id);
            StreetService streetService = new StreetService();
            JuWeiService juweiService = new JuWeiService();
            QuartersService quartersService = new QuartersService();
            StationService stationService = new StationService();
            result.StreetName = streetService.GetStreetName(result.StreetId);
            result.JuWeiName = juweiService.GetJuWeiName(result.JuWeiId);
            result.QuartersName = quartersService.GetQuartersName(result.QuartersId);
            result.StationName = stationService.GetStationName(result.StationId);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该公共收益不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }

        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <returns></returns>
        [Route("getPublicIncomes")]
        [HttpGet]
        public async Task<JsonResult> GetPublicIncomes(SearchPublicIncomeModel model)
        {
            //拼接过滤条件
            Expression<Func<PublicIncome, bool>> where = p =>
                 (model.IsRepairAmount == null || p.IsRepairAmount == model.IsRepairAmount) &&
                 (model.StreetId == null || p.StreetId == model.StreetId) &&
                 (model.StationId == null || p.StationId == model.StationId) &&
                 (model.JuWeiId == null || p.JuWeiId == model.JuWeiId) &&
                 (model.QuartersId == null || p.QuartersId == model.QuartersId) &&
                 (model.BeYearMonth_Start == null || p.BeYearMonth >= model.BeYearMonth_Start) &&
                 (model.BeYearMonth_End == null || p.BeYearMonth <= model.BeYearMonth_End);

            var result = await _publicIncomeService.GetPagedAsync(where, i => i.CreationTime, model.pageIndex, model.pageSize, true);

            #region 处理list
            if (result != null && result.Item2.Count > 0)
            {
                StreetService streetService = new StreetService();
                JuWeiService juweiService = new JuWeiService();
                QuartersService quartersService = new QuartersService();
                StationService stationService = new StationService();
                foreach (var item in result.Item2)
                {
                    item.StreetName = streetService.GetStreetName(item.StreetId);
                    item.JuWeiName = juweiService.GetJuWeiName(item.JuWeiId);
                    item.QuartersName = quartersService.GetQuartersName(item.QuartersId);
                    item.StationName = stationService.GetStationName(item.StationId);
                }
            }
            #endregion

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputPublicIncomeModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputPublicIncomeModel>>(result.Item2)
            });
        }

    }
}