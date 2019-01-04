using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.QuestionBankManage;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Dto;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/QuestionBankManage")]
    public class QuestionBankManageController : Controller
    {
        private readonly IQuestionBankManageService _questionBankManageService;
        private readonly IQuestionBankOptionService _questionBankOptionService;
        public QuestionBankManageController(IQuestionBankManageService questionBankManageService, IQuestionBankOptionService questionBankOptionService)
        {
            _questionBankManageService = questionBankManageService;
            _questionBankOptionService = questionBankOptionService;
        }
        /// <summary>
        /// 获取列表根据传递不同的参数
        /// </summary>
        /// <param name="SAM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetDataList")]
        [HttpGet]
        public async Task<JsonResult> GetListByParam(SearchQuestionBankManageModel SAM, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<QuestionBankManage, bool>> where = p =>
                   (SAM.TitleType == null || p.TitleType == SAM.TitleType) &&
                   (SAM.TitleName == null || p.TitleName.Contains(SAM.TitleName)) && (SAM.StreetId == null || p.StreetId == SAM.StreetId) && (SAM.PostStationId == null || p.PostStationId == SAM.PostStationId);
            var result = await _questionBankManageService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputQuestionBankManageModel> { Total = result.Item1, List = Mapper.Map<IList<OutputQuestionBankManageModel>>(result.Item2) });
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputQuestionBankManageModel inputQuestionBankManageModel)
        {
            if (inputQuestionBankManageModel.TitleName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "题目名称不能为空");
            }
            var questionBankManageDto = new QuestionBankManageDto
            {
                TitleType = inputQuestionBankManageModel.TitleType,
                TitleName = inputQuestionBankManageModel.TitleName,
                TitleRemarks = inputQuestionBankManageModel.TitleRemarks,
                StreetId=inputQuestionBankManageModel.StreetId,
                PostStationId=inputQuestionBankManageModel.PostStationId
            };
            var id = await _questionBankManageService.InsertAndGetIdAsync(questionBankManageDto);
            if (id != default(Guid) && inputQuestionBankManageModel.TitleType != 3)
            {
                string[] arrayOptions = inputQuestionBankManageModel.TitleOptions.Split(',');
                for (int i = 0; i < arrayOptions.Length; i++)
                {
                    //将每一条选项都存储在数据库中
                    var questionnaireOptionsDto = new QuestionBankOptionDto
                    {
                        QuestionId = id,
                        TitleOptions = arrayOptions[i]
                    };
                    await _questionBankOptionService.InsertAndGetIdAsync(questionnaireOptionsDto);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }
        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <returns></returns>
        [Route("edit")]
        [HttpPost]
        public async Task<JsonResult> Edit(InputQuestionBankManageModel inputQuestionBankManageModel)
        {
            var info = await _questionBankManageService.GetAsync(Guid.Parse( inputQuestionBankManageModel.Id.ToString()));
            info.StreetId = inputQuestionBankManageModel.StreetId;
            info.PostStationId = inputQuestionBankManageModel.PostStationId;
            await _questionBankManageService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDataById")]
        [HttpGet]
        public async Task<JsonResult> GetConferenceEquipmentById(Guid id)
        {
            var result = await _questionBankManageService.GetAsync(id);
            //将该题目下的所有选项返回到前端
            List<QuestionBankOptionDto> optionList = _questionBankOptionService.GetAllList(p => p.QuestionId == id);
            result.Options = optionList;
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _questionBankManageService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "数据不存在");
            }
            info.DeletionTime = DateTime.Now;
            await _questionBankManageService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
        /// <summary>
        /// 修改题目
        /// </summary>
        /// <returns></returns>
        [Route("editTitle")]
        [HttpPost]
        public async Task<JsonResult> EditTitle(Guid id, string title,string TitleRemarks)
        {
            var info = await _questionBankManageService.GetAsync(id);
            info.TitleName = title;

            if (!string.IsNullOrEmpty(TitleRemarks))
            {
                info.TitleRemarks = TitleRemarks;
            }
            await _questionBankManageService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 修改题目选项
        /// </summary>
        /// <returns></returns>
        [Route("editOption")]
        [HttpPost]
        public async Task<JsonResult> EditOption(string titleOptions, Guid questionId)
        {
            string[] str = titleOptions.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                if (!string.IsNullOrEmpty(str[i].Split('|')[1]))
                {
                    var questionBankOptionDto = new QuestionBankOptionDto
                    {
                        Id = Guid.Parse(str[i].Split('|')[1]),
                        TitleOptions = str[i].Split('|')[0]
                    };
                    await _questionBankOptionService.UpdateAsync(questionBankOptionDto);
                }
                else
                {
                    var questionBankOptionDto = new QuestionBankOptionDto
                    {
                        TitleOptions = str[i].Split('|')[0],
                        QuestionId = questionId
                    };
                    await _questionBankOptionService.InsertAsync(questionBankOptionDto);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
    }
}