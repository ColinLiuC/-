using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models.QuestionnaireTitle;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/QuestionnaireTitle")]
    public class QuestionnaireTitleController : Controller 
    {
        private readonly IQuestionnaireTitleService _questionnaireTitleService;
        private readonly IQuestionnaireOptionsService _questionnaireOptionsService;
        private readonly IQuestionBankManageService _questionBankManageService;
        private readonly IQuestionBankOptionService _questionBankOptionService;
        public QuestionnaireTitleController(IQuestionnaireTitleService questionnaireTitleService, IQuestionnaireOptionsService questionnaireOptionsService, IQuestionBankManageService questionBankManageService, IQuestionBankOptionService questionBankOptionService)
        {
            _questionnaireTitleService = questionnaireTitleService;
            _questionnaireOptionsService = questionnaireOptionsService;
            _questionBankManageService = questionBankManageService;
            _questionBankOptionService = questionBankOptionService;
        }
        [Route("get")]
        [HttpGet]
        public async Task<JsonResult> GetListData(int titleType,Guid wenJuanId)
        {
            var result = await _questionnaireTitleService.GetAllListAsync(p=>p.TitleType==titleType&&p.WenJuanId== wenJuanId);
            var newList = new List<QuestionnaireTitleDto>();
            foreach (QuestionnaireTitleDto item in result)
            {
                var newOptionsList = new List<QuestionnaireOptionsDto>();
                var optionsList = _questionnaireOptionsService.GetAllList(p => p.QuestionnaireTitleId == item.Id);
                foreach (QuestionnaireOptionsDto item1 in optionsList)
                {
                    var optionModel = new QuestionnaireOptionsDto
                    {
                        Id=item1.Id,
                        WenJuanTitleOptions=item1.WenJuanTitleOptions
                    };
                    newOptionsList.Add(optionModel);
                }
                var model = new QuestionnaireTitleDto
                {
                    Id = item.Id,
                    Title = item.Title,
                    WenJuanTitleOptions = newOptionsList
                };
                newList.Add(model);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功",newList);
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputQuestionnaireTitleModel inputQuestionnaireTitleModel)
        {            
            var questionnaireTitleDto = Mapper.Map<QuestionnaireTitleDto>(inputQuestionnaireTitleModel);
            var id = await _questionnaireTitleService.InsertAndGetIdAsync(questionnaireTitleDto);
            if (id!=default(Guid)&&inputQuestionnaireTitleModel.TitleType!=3)
            {
                if (inputQuestionnaireTitleModel.TitleOptions!=null)
                {
                    string[] arrayOptions = inputQuestionnaireTitleModel.TitleOptions.Split(',');
                    for (int i = 0; i < arrayOptions.Length; i++)
                    {
                        //将每一条选项都存储在数据库中
                        var questionnaireOptionsDto = new QuestionnaireOptionsDto
                        {
                            QuestionnaireTitleId = id,
                            WenJuanTitleOptions = arrayOptions[i]
                        };
                        await _questionnaireOptionsService.InsertAndGetIdAsync(questionnaireOptionsDto);
                    }
                }      
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }
        /// <summary>
        /// 从题库中查询到的数据插入到问卷表中
        /// </summary>
        /// <returns></returns>
        [Route("AddTitle")]
        [HttpPost]
        public async Task<JsonResult> Insert(string ids,Guid wenJuanId)
        {
            string[] str = ids.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
               var result = await _questionBankManageService.GetAsync(Guid.Parse(str[i]));
               //查找问卷中是否已存在该题目，存在不添加。
               var list=  _questionnaireTitleService.GetAllList(p => p.WenJuanId == wenJuanId && p.Title == result.TitleName);
                if (list.Count>0)
                {
                    continue;
                }
                var questionnaireTitleDto = new QuestionnaireTitleDto {
                    Title=result.TitleName,
                    TitleType=result.TitleType,
                    WenJuanId= wenJuanId
                };
               var id = await _questionnaireTitleService.InsertAndGetIdAsync(questionnaireTitleDto);
                if (id != default(Guid))
                {
                    List<QuestionBankOptionDto> optionList = _questionBankOptionService.GetAllList(p => p.QuestionId == result.Id);
                    foreach (QuestionBankOptionDto item in optionList)
                    {
                        var questionnaireOptionsDto = new QuestionnaireOptionsDto
                        {
                            QuestionnaireTitleId = id,
                            WenJuanTitleOptions =item.TitleOptions
                        };
                        await _questionnaireOptionsService.InsertAndGetIdAsync(questionnaireOptionsDto);
                    }            
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }
        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _questionnaireTitleService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该题目不存在");
            }
            info.DeletionTime = DateTime.Now;
            await _questionnaireTitleService.DeleteAsync(info);
            //删除该题目下的所有选项
            var options=  _questionnaireOptionsService.GetAllList(p=>p.QuestionnaireTitleId==id);
            foreach (QuestionnaireOptionsDto item in options)
            {
                item.DeletionTime = DateTime.Now;
                await _questionnaireOptionsService.DeleteAsync(item);
            }  
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
        /// <summary>
        /// 通过ID得到一条数据
        /// </summary>
        /// <returns></returns>
        [Route("GetDataById")]
        [HttpGet]
        public async Task<JsonResult> GetById(Guid id)
        {
            var result = await _questionnaireTitleService.GetAsync(id);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        /// <summary>
        /// 修改题目
        /// </summary>
        /// <returns></returns>
        [Route("editTitle")]
        [HttpPost]
        public async Task<JsonResult> EditTitle(Guid id,string title)
        {
            var info = await _questionnaireTitleService.GetAsync(id);
            info.Title = title;

            await _questionnaireTitleService.UpdateAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }


    }
}