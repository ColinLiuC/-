using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Domain.Enum;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.Extensions;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.ResidentWork;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/ResidentWork")]
    public class ResidentWorkController : Controller
    {
        private readonly IResidentWorkService _residentWorkService;
        private readonly IResidentWork_AttachService _residentWork_AttachService;


        public ResidentWorkController(IResidentWorkService residentWorkService, IResidentWork_AttachService residentWork_AttachService)
        {
            _residentWorkService = residentWorkService;
            _residentWork_AttachService = residentWork_AttachService;
        }
        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="inputResidentWorkModel">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputResidentWorkModel inputResidentWorkModel)
        {
            if (string.IsNullOrWhiteSpace(inputResidentWorkModel.ResidentWorkName))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "事项名称不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var residentWorkDto = Mapper.Map<ResidentWorkDto>(inputResidentWorkModel);
            var id = await _residentWorkService.InsertAndGetIdAsync(residentWorkDto);

            #region 插入附表数据
            if (id != null && !string.IsNullOrWhiteSpace(inputResidentWorkModel.StationIds))
            {
                var stationIds_arr = inputResidentWorkModel.StationIds.Split(",");
                foreach (var stationId in stationIds_arr)
                {
                    var residentWork_AttachDto = new ResidentWork_AttachDto
                    {
                        ResidentWorkId = id,
                        StreetId = inputResidentWorkModel.StreetId,
                        StationId = Guid.Parse(stationId),
                    };
                    await _residentWork_AttachService.InsertAndGetIdAsync(residentWork_AttachDto);
                }
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "添加失败");
            #endregion
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="residentWorkId">事项ID</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid residentWorkId)
        {
            var residentWorkdto = await _residentWorkService.GetAsync(residentWorkId);
            if (residentWorkdto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该事项不存在");
            }
            residentWorkdto.DeletionTime = DateTime.Now;
            await _residentWorkService.DeleteAsync(residentWorkdto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="inputResidentWorkModel">输入参数model</param>
        /// <param name="inputResidentWorkId">事项id</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputResidentWorkModel inputResidentWorkModel, Guid inputResidentWorkId)
        {
            var residentWorkDto = await _residentWorkService.GetAsync(inputResidentWorkId);
            if (residentWorkDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该事项不存在");
            }
            //数据映射:inputResidentWorkModel > ResidentWorkDto
            residentWorkDto = Mapper.Map<ResidentWorkDto>(inputResidentWorkModel);
            residentWorkDto.Id = inputResidentWorkId;
            await _residentWorkService.UpdateAsync(residentWorkDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 分配驿站
        /// </summary>
        /// <param name="residentWorkId">事项id</param>
        /// <param name="streetid">街道id</param>
        /// <param name="stations">驿站id</param>
        /// <returns></returns>
        [Route("allotStation")]
        [HttpPost]
        public async Task<JsonResult> AllotStation(Guid residentWorkId, Guid streetid, Guid?[] stations)
        {
            //先清空该事项数据后添加
            _residentWork_AttachService.DeleteByResidentWorkId(residentWorkId, streetid);
            foreach (var stationId in stations)
            {
                var residentWork_AttachDto = new ResidentWork_AttachDto
                {
                    ResidentWorkId = residentWorkId,
                    StreetId = streetid,
                    StationId = Guid.Parse(stationId.ToString()),
                };
                await _residentWork_AttachService.InsertAndGetIdAsync(residentWork_AttachDto);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }


        /// <summary>
        /// 获取此事项街道下分配的驿站
        /// </summary>
        /// <param name="residentWorkId">事项id</param>
        /// <param name="streetid">街道id</param>
        /// <returns></returns>

        [Route("getAllotStation")]
        [HttpGet]
        public JsonResult GetAllotStation(Guid residentWorkId, Guid? streetid)
        {
            var result = _residentWork_AttachService.GetResidentWorkByStreet(residentWorkId, streetid);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功", result);
        }


        /// <summary>
        /// 根据Id获取事项。200获取成功;404未找到
        /// </summary>
        /// <param name="residentWorkId">事项Id</param>
        /// <returns></returns>
        [Route("getResidentWorkById")]
        [HttpGet]
        public async Task<JsonResult> GetResidentWorkById(Guid residentWorkId)
        {
            var result = await _residentWorkService.GetAsync(residentWorkId);
            result.ResidentWorkType_ds = EnumExtensions.GetDescription((ResidentWorkType)result.ResidentWorkType);
            result.IsPublish_ds = StringExtensions.ParseBool(result.IsPublish);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该事项不存在", result);
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
        [Route("getResidentWorkPaged")]
        [HttpGet]
        public async Task<JsonResult> GetResidentWorkPaged(SearchResidentWorkModel model, int pageIndex = 1, int pageSize = 10)
        {
            //拼接过滤条件
            Expression<Func<ResidentWork, bool>> where = p =>
                 (string.IsNullOrEmpty(model.ResidentWorkName) || p.ResidentWorkName.Contains(model.ResidentWorkName)) &&
                 (model.ResidentWorkType == null || p.ResidentWorkType == model.ResidentWorkType) &&
                 //(model.StreetId == null || p.StreetId == model.StreetId) &&
                 //(string.IsNullOrEmpty(model.StationIds) || p.StationIds.Contains(model.StationIds)) &&
                 (model.IsPublish == null || p.IsPublish == model.IsPublish) &&
                 (model.IsGuiDang == null || p.IsGuiDang == model.IsGuiDang);

            var result = await _residentWorkService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            //格式化枚举数据
            if (result != null && result.Item2.Count > 0)
            {
                foreach (var item in result.Item2)
                {
                    item.ResidentWorkType_ds = EnumExtensions.GetDescription((EnumCode.ResidentWorkType)item.ResidentWorkType);
                    item.IsPublish_ds = StringExtensions.ParseBool(item.IsPublish);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputResidentWorkModel>
            {
                Total = result.Item1,
                List = Mapper.Map<IList<OutputResidentWorkModel>>(result.Item2)
            });
        }


        /// <summary>
        /// App端获取事项
        /// </summary>
        /// <param name="streetid">街道Id</param>
        /// <param name="stationid">驿站Id</param>
        /// <param name="residentWorktype">事项类别</param>
        /// <param name="residentWorkname">事项名称</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        [Route("getResidentWorksApp")]
        [HttpGet]
        public JsonResult GetResidentWorksApp(Guid streetid, Guid? stationid, int? residentWorktype, string residentWorkname, int pageIndex = 1, int pageSize = 10)
        {
            int total;
            var result = _residentWorkService.GetResidentWorkApp(streetid, stationid, residentWorktype, residentWorkname, out total, pageIndex, pageSize);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new { list = result, total = total });
        }

        [Route("getResidentWorksPC")]
        [HttpGet]
        public JsonResult GetResidentWorksPC(Guid streetid, Guid? stationid, int? residentWorktype, string residentWorkname, int pageIndex = 1, int pageSize = 10)
        {
            int total;
            var result = _residentWorkService.GetResidentWorkApp(streetid, stationid, residentWorktype, residentWorkname, out total, pageIndex, pageSize);
            //格式化枚举数据
            if (result != null && result.Count > 0)
            {
                foreach (var item in result)
                {
                    item.ResidentWorkType_ds = EnumExtensions.GetDescription((ResidentWorkType)item.residentWork.ResidentWorkType);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new { list = result, total = total });
        }


        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="inputResidentWorkId">事项id</param>
        /// <param name="guiDangUser">归档人</param>
        /// <param name="guiDangRenark">备注</param>
        /// <param name="guiDangTime">归档时间</param>
        /// <returns></returns>
        [Route("guiDang")]
        [HttpPost]
        public async Task<JsonResult> GuiDang(Guid inputResidentWorkId, string guiDangUser, string guiDangRenark, DateTime guiDangTime)
        {
            var residentWork = await _residentWorkService.GetAsync(inputResidentWorkId);
            if (residentWork == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该事项不存在");
            }
            if (string.IsNullOrEmpty(guiDangUser))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "归档人不能为空");
            }
            if (guiDangTime == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "归档时间不能为空");
            }
            //数据映射:inputResidentWorkModel > ResidentWorkDto
            //residentWork = Mapper.Map<InputResidentWorkModel, ResidentWorkDto>(inputResidentWorkModel);
            residentWork.GuiDangUser = guiDangUser;
            residentWork.GuiDangRenark = guiDangRenark;
            residentWork.GuiDangTime = guiDangTime;
            residentWork.Id = inputResidentWorkId;
            residentWork.LastModificationTime = DateTime.Now;
            residentWork.IsGuiDang = true;
            await _residentWorkService.UpdateAsync(residentWork);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "归档成功");
        }


        [Route("GetSelectList")]
        [HttpGet]
        public JsonResult GetSelectList(string expression)
        {

            IList<SelectListItem> list = Helper.DataDictionaryHelper.GetSelectList(expression);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "归档成功", list);
        }


        [Route("GetMyStation")]
        [HttpGet]
        public JsonResult GetMyStation(Guid residentWorkId, Guid streetid)
        {
            var list = _residentWork_AttachService.GetMyStation(residentWorkId, streetid);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", list);
        }
        
    }
}