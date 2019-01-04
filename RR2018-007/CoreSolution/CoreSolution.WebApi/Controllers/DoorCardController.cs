using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.DoorCard;
using CoreSolution.WebApi.Models.HelperModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/DoorCard")]
    public class DoorCardController : ControllerBase
    {
        private readonly IDoorCardService _doorCardService;
        private readonly IHouseService _houseService;

        public DoorCardController(IDoorCardService doorCardService, IHouseService houseService)
        {
            _doorCardService = doorCardService;
            _houseService = houseService;
        }

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputDoorCardModel model)
        {
            if (model.DoorCardNumber == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "门牌不能为空");
            }
            if (string.IsNullOrEmpty(model.ChargeUser))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "负责人不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<DoorCardDto>(model);
            var id = await _doorCardService.InsertAndGetIdAsync(infoDto);

            //反序列化成房屋对象
            List<HouseDto> housemodel = JsonConvert.DeserializeObject<List<HouseDto>>(model.HouseJson);
            foreach (var houseitem in housemodel)
            {
                HouseDto housedto = new HouseDto();
                housedto.BuildArea = houseitem.BuildArea;
                housedto.OrientationId = houseitem.OrientationId;
                housedto.HouseNumber = houseitem.HouseNumber;
                housedto.DoorCardId = id;
                await _houseService.InsertAndGetIdAsync(housedto);
            }
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
            var infoDto = await _doorCardService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该门牌不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _doorCardService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <param name="DoorCardId">Id</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputDoorCardModel model, Guid DoorCardId)
        {
            var infoDto = await _doorCardService.GetAsync(DoorCardId);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该门牌号不存在");
            }
            infoDto = Mapper.Map<DoorCardDto>(model);
            infoDto.Id = DoorCardId;
            await _doorCardService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }


        /// <summary>
        /// 根据Id获取门牌。200获取成功;404未找到
        /// </summary>
        /// <param name="id">小区Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _doorCardService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该门牌不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 根据Id获取门牌（多表信息）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("getMyDoorCard")]
        [HttpGet]
        public JsonResult GetMyDoorCard(Guid id)
        {
            var result = _doorCardService.GetDoorCardInfo(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该门牌不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <param name="buildingid">楼栋</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("getDoorCards")]
        [HttpGet]
        public async Task<JsonResult> GetDoorCards(Guid? buildingid, int pageIndex = 1, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<DoorCard, bool>> where = p =>
               (buildingid == null || p.BuildingId == buildingid);

            var result = await _doorCardService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputDoorCardModel>
            { Total = result.Item1, List = Mapper.Map<IList<OutputDoorCardModel>>(result.Item2) });
        }


    }
}