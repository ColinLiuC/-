using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models.Discount;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models;
using AutoMapper;
using System.Linq.Expressions;
using CoreSolution.Domain.Entities;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/Discount")]
    public class DiscountController : Controller
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        /// <summary>
        /// 新增一条服务优惠信息
        /// </summary>
        /// <param name="inputDiscountModel"></param>
        /// <returns></returns>
        [Route("AddDiscount")]
        [HttpPost]
        public async Task<JsonResult> AddDiscount(InputDiscountModel inputDiscountModel)
        {
            if (inputDiscountModel.PreferentialName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "优惠名称不能为空");
            }
            var discountDto = new DiscountDto
            {
               PreferentialName=inputDiscountModel.PreferentialName,
               FavourableConditions=inputDiscountModel.FavourableConditions,
               PriceDescription=inputDiscountModel.PriceDescription,
               ServiceGuid=inputDiscountModel.ServiceGuid
            };
            var id = await _discountService.InsertAndGetIdAsync(discountDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", id);
        }
        /// <summary>
        /// 获取所有的优惠信息
        /// </summary>
        /// <returns></returns>
        [Route("GetDiscountList")]
        [HttpGet]
        public async Task<JsonResult> GetListData(Guid id,int pageIndex = 1, int pageSize = 10)
        {
            var result = await _discountService.GetAllListAsync(n => n.ServiceGuid == id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputDiscountModel> { Total = result.Count, List = Mapper.Map<IList<OutputDiscountModel>>(result) });
        }

        [Route("GetYouhui")]
        [HttpPost]
        public async Task<JsonResult> GetYouhui(Guid LifeServiceId, int pageIndex = 1, int pageSize = 10)
        {
            var model = await _discountService.GetPagedAsync(n => n.ServiceGuid == LifeServiceId, i => i.Id, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputDiscountModel> { Total = model.Item1, List = Mapper.Map<IList<OutputDiscountModel>>(model.Item2) });

        }

        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputDiscountModel inputDiscountModel,Guid Id)
        {
            var Dto = await _discountService.GetAsync(Id);
            if (Dto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该优惠不存在");
            }
            //数据映射:inputResidentWorkModel > ResidentWorkDto
            DiscountDto discountDto = new DiscountDto()
            {
                Id = Id,
                PreferentialName = inputDiscountModel.PreferentialName,
                FavourableConditions = inputDiscountModel.FavourableConditions,
                PriceDescription = inputDiscountModel.PriceDescription,
                ServiceGuid = inputDiscountModel.ServiceGuid
            };
            await _discountService.UpdateAsync(discountDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid Id)
        {
            var dto = await _discountService.GetAsync(Id);
            if (dto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该优惠不存在");
            }
            dto.DeletionTime = DateTime.Now;
            await _discountService.DeleteAsync(dto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        [Route("detail")]
        [HttpGet]
        public async Task<JsonResult> Detail(Guid Id)
        {
            var result = await _discountService.GetAsync(Id);
            
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该优惠不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }



    }
}