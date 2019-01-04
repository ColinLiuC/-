using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Models.QuestionnaireOptions;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/QuestionnaireOptions")]
    public class QuestionnaireOptionsController : Controller
    {
        private readonly IQuestionnaireOptionsService _questionnaireOptionsService;

        public QuestionnaireOptionsController(IQuestionnaireOptionsService questionnaireOptionsService)
        {
            _questionnaireOptionsService = questionnaireOptionsService;
        }
        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <returns></returns>
        [Route("Add")]
        [HttpPost]
        public async Task<JsonResult> Create(InputQuestionnaireOptionsModel inputQuestionnaireOptionsModel)
        {
            var conferenceRoomDto = Mapper.Map<QuestionnaireOptionsDto>(inputQuestionnaireOptionsModel);
            var id = await _questionnaireOptionsService.InsertAndGetIdAsync(conferenceRoomDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功", id);
        }

        [Route("get")]
        [HttpGet]
        public async Task<JsonResult> GetListData(Guid titleId)
        {
            var result = await _questionnaireOptionsService.GetAllListAsync(p => p.QuestionnaireTitleId ==titleId);        
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }
        /// <summary>
        /// 修改题目
        /// </summary>
        /// <returns></returns>
        [Route("editOption")]
        [HttpPost]
        public async Task<JsonResult> EditOption(string wenJuanTitleOptions,Guid wenJuanTitleId)
        {
            string[] str = wenJuanTitleOptions.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                var id = str[i].Split('|')[1];
                if (id!="0")
                {
                   var info=await _questionnaireOptionsService.GetAsync(Guid.Parse(id));
                   info.WenJuanTitleOptions = str[i].Split('|')[0];
                    await _questionnaireOptionsService.UpdateAsync(info);
                }
                else
                {
                    var questionnaireOptionsDto = new QuestionnaireOptionsDto
                    {
                        QuestionnaireTitleId= wenJuanTitleId,
                        WenJuanTitleOptions = str[i].Split('|')[0]
                    };
                    await _questionnaireOptionsService.InsertAsync(questionnaireOptionsDto);
                }
            }
           
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 通过ID删除一条数据
        /// </summary>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var info = await _questionnaireOptionsService.GetAsync(id);
            if (info == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该选项不存在");
            }
            await _questionnaireOptionsService.DeleteAsync(info);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }
    }
}