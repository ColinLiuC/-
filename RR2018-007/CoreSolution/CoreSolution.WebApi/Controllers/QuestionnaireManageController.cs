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
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.QuestionnaireManage;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/QuestionnaireManage")]
    public class QuestionnaireManageController : Controller
    {
        private readonly IQuestionnaireManageService _questionnaireManageService;
        private readonly IQuestionnaireTitleService _questionnaireTitleService;
        private readonly IQuestionnaireOptionsService _questionnaireOptionsService;
        private readonly IQuestionnaireAnswerService _questionnaireAnswerService;
        private readonly IDataDictionaryService _dataDictionaryService;
        public QuestionnaireManageController(IQuestionnaireManageService questionnaireManageService, IQuestionnaireTitleService questionnaireTitleService, IQuestionnaireOptionsService questionnaireOptionsService, IQuestionnaireAnswerService questionnaireAnswerService, IDataDictionaryService dataDictionaryService)
        {
            _questionnaireManageService = questionnaireManageService;
            _questionnaireTitleService = questionnaireTitleService;
            _questionnaireOptionsService = questionnaireOptionsService;
            _questionnaireAnswerService = questionnaireAnswerService;
            _dataDictionaryService = dataDictionaryService;
        }
        /// <summary>
        /// 获取列表根据传递不同的参数
        /// </summary>
        /// <param name="SAM"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [Route("GetDataByParam")]
        [HttpGet]
        public async Task<JsonResult> GetListByParam(SearchQuestionnaireManageModel SAM, int pageIndex = 1, int pageSize = 10)
        {
            Expression<Func<QuestionnaireManage, bool>> where = p =>
                   (string.IsNullOrEmpty(SAM.QuestionnaireName) || p.QuestionnaireName.Contains(SAM.QuestionnaireName)) &&
                   (SAM.QuestionnaireType == default(Guid) || p.QuestionnaireType == SAM.QuestionnaireType) && (SAM.CurrentState == null || p.CurrentState==SAM.CurrentState)&&((SAM.StartTime==default(DateTime)&&SAM.EndTime==default(DateTime))||p.CreationTime>=SAM.StartTime&&p.CreationTime<=SAM.EndTime.AddHours(23).AddMinutes(59)) && (SAM.StreetId == null || p.StreetId == SAM.StreetId) && (SAM.PostStationId == null || p.PostStationId == SAM.PostStationId);
            var result = await _questionnaireManageService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
            if (result.Item2 != null)
            {
                foreach (var item in result.Item2)
                {
                    item.TypeName = _dataDictionaryService.GetItemNameById(item.QuestionnaireType);
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputQuestionnaireManageModel> { Total = result.Item1, List = Mapper.Map<IList<OutputQuestionnaireManageModel>>(result.Item2) });
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputQuestionnaireManageModel inputQuestionnaireManageModel)
        {
            if (inputQuestionnaireManageModel.QuestionnaireName.IsNullOrWhiteSpace())
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "问卷名称不能为空");
            }
            inputQuestionnaireManageModel.CurrentState = 2;
            var conferenceRoomDto = Mapper.Map<QuestionnaireManageDto>(inputQuestionnaireManageModel);
            var id= await _questionnaireManageService.InsertAndGetIdAsync(conferenceRoomDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功",id);
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDataById")]
        [HttpGet]
        public async Task<JsonResult> GetConferenceEquipmentById(Guid id)
        {
            var result = await _questionnaireManageService.GetAsync(id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        /// <summary>
        /// 通过ID删除问卷数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _questionnaireManageService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该问卷不存在");
            }
            info.DeletionTime = DateTime.Now;
            await _questionnaireManageService.DeleteAsync(info);
            //删除该问卷下所有题目和选项
            var title = _questionnaireTitleService.GetAllList(p => p.WenJuanId == id);
            if (title.Count>0)
            {
                foreach (QuestionnaireTitleDto item in title)
                {
                    await _questionnaireTitleService.DeleteAsync(item);
                    var options = _questionnaireOptionsService.GetAllList(p => p.QuestionnaireTitleId == item.Id).FirstOrDefault();
                    options.DeletionTime = DateTime.Now;
                    await _questionnaireOptionsService.DeleteAsync(options);
                }
            }                   
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
        /// <summary>
        /// 获取问卷单选结果数据
        /// </summary>
        /// <param name="wenJuanId"></param>
        /// <param name="titleType"></param>
        /// <returns></returns>
        [Route("GetAnswerByParam")]
        [HttpGet]
        public  JsonResult GetAnswerByParam(Guid wenJuanId,int titleType)
        {
            var titleList = _questionnaireTitleService.GetAllList(p => p.WenJuanId == wenJuanId && p.TitleType == titleType);
            var newList = new List<AnswerDto>();
            foreach (var item in titleList)
            {
                var answerList =  _questionnaireAnswerService.GetAllList(p => p.WenJuanId == wenJuanId && p.TitleId == item.Id);
                var q =
                     (from p in answerList
                      group p by p.OptionName into g
                      where g.Count() > 0
                      select new AnswerContent
                      {
                          optionName = g.Key,
                          count = g.Count()
                      }).ToList();
                var answerDto = new AnswerDto
                {
                    titleName = item.Title,
                    answerContent = q
                };
                newList.Add(answerDto);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "", newList);
        }
        /// <summary>
        /// 发布问卷
        /// </summary>
        /// <returns></returns>
        [Route("release")]
        [HttpPost]
        public async Task<JsonResult> IssueQuestionnaire(Guid wenJuanId)
        {
            var info =await _questionnaireManageService.GetAsync(wenJuanId);
            info.CurrentState = 1;
            await _questionnaireManageService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }
    }
}