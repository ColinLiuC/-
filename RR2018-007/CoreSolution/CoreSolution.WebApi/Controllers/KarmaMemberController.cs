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
using CoreSolution.WebApi.Models.KarmaMember;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/KarmaMember")]
    public class KarmaMemberController : ControllerBase
    {
        private readonly IKarmaMemberService _karmaMemberService;
        public KarmaMemberController(IKarmaMemberService karmaMemberService)
        {
            _karmaMemberService = karmaMemberService;
        }

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputKarmaMemberModel model)
        {
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<KarmaMemberDto>(model);
            var id = await _karmaMemberService.InsertAndGetIdAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
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
            var infoDto = await _karmaMemberService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该业委会成员不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _karmaMemberService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputKarmaMemberModel model)
        {
            var infoDto = await _karmaMemberService.GetAsync(Guid.Parse(model.Id.ToString()));
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该业委会成员不存在");
            }
            infoDto = Mapper.Map<KarmaMemberDto>(model);
            await _karmaMemberService.UpdateAsync(infoDto);
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
            var result = await _karmaMemberService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该业委会成员不存在", result);
            }
            DataDictionaryService dataDictionaryService = new DataDictionaryService();
            result.DutiesName = dataDictionaryService.GetItemNameById(result.Duties);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }



        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        [Route("getKarmaMenbers")]
        [HttpGet]
        public async Task<JsonResult> GetKarmaMenbers(Guid? KarmaId, string IDCard, Guid? Duties, string Name, int pageIndex = 1, int pageSize=10 )
        {
            //拼接过滤条件
            Expression<Func<KarmaMember, bool>> where = p =>
               (KarmaId == null || p.KarmaId == KarmaId) &&
               (Duties == null || p.Duties == Duties) &&
               (string.IsNullOrEmpty(Name) || p.Name.Contains(Name)) &&
               (string.IsNullOrEmpty(IDCard) || p.IDCard.Contains(IDCard));
            var result = await _karmaMemberService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);

            #region 处理list

            if (result != null && result.Item2.Count > 0)
            {
                DataDictionaryService dataDictionaryService = new DataDictionaryService();
                foreach (var item in result.Item2)
                {
                    item.DutiesName = dataDictionaryService.GetItemNameById(item.Duties);
                }
            }
            #endregion

            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputKarmaMemberModel>
            { Total = result.Item1, List = Mapper.Map<IList<OutputKarmaMemberModel>>(result.Item2) });
        }
    }
}