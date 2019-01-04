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
using CoreSolution.WebApi.Models.Karma;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Karma")]
    public class KarmaController : ControllerBase
    {
        private readonly IKarmaService _karmaService;
        public KarmaController(IKarmaService karmaService)
        {
            _karmaService = karmaService;
        }

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputKarmaModel model)
        {
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<KarmaDto>(model);
            var id = await _karmaService.InsertAndGetIdAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功",id);
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
            var infoDto = await _karmaService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该业委会不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _karmaService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputKarmaModel model,Guid id)
        {
            var infoDto = await _karmaService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该业委会不存在");
            }
            infoDto = Mapper.Map<KarmaDto>(model);
            infoDto.Id = id;
            await _karmaService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 根据Id获取。200获取成功;404未找到
        /// </summary>
        /// <param name="id">小区Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _karmaService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该业委会不存在", result);
            }
            StreetService streetService = new StreetService();
            JuWeiService juweiService = new JuWeiService();
            QuartersService quartersService = new QuartersService();
            StationService stationService = new StationService();
            DataDictionaryService dataDictionaryService = new DataDictionaryService();
            result.StreetName = streetService.GetStreetName(result.StreetId);
            result.JuWeiName = juweiService.GetJuWeiName(result.JuWeiId);
            result.QuartersName = quartersService.GetQuartersName(result.QuartersId);
            result.StationName = stationService.GetStationName(result.StationId);
            result.RenQiName = dataDictionaryService.GetItemNameById(result.RenQiId);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }



        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        [Route("getKarmas")]
        [HttpGet]
        public async Task<JsonResult> GetKarmas(SearchKarmaModel model)
        {
            //拼接过滤条件
            Expression<Func<Karma, bool>> where = p =>
               (model.StationId == null || p.StationId == model.StationId) &&
               (model.JuWeiId == null || p.JuWeiId == model.JuWeiId) &&
               (model.QuartersId == null || p.QuartersId == model.QuartersId) &&
               (model.RenQiId == null || p.RenQiId == model.RenQiId) &&
               (model.StreetId == null || p.StreetId == model.StreetId) &&
               (string.IsNullOrEmpty(model.Name) || p.Name.Contains(model.Name));
            var result = await _karmaService.GetPagedAsync(where, i => i.CreationTime, model.pageIndex, model.pageSize, true);

            #region 处理list

            if (result != null && result.Item2.Count > 0)
            {
                StreetService streetService = new StreetService();
                JuWeiService juweiService = new JuWeiService();
                QuartersService quartersService = new QuartersService();
                StationService stationService = new StationService();
                DataDictionaryService dataDictionaryService = new DataDictionaryService();
                foreach (var item in result.Item2)
                {
                    item.StreetName = streetService.GetStreetName(item.StreetId);
                    item.JuWeiName = juweiService.GetJuWeiName(item.JuWeiId);
                    item.QuartersName = quartersService.GetQuartersName(item.QuartersId);
                    item.RenQiName = dataDictionaryService.GetItemNameById(item.RenQiId);
                    item.StationName = stationService.GetStationName(item.StationId);
                }
            }
            #endregion

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputKarmaModel>
            { Total = result.Item1, List = Mapper.Map<IList<OutputKarmaModel>>(result.Item2) });
        }
    }
}