using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.QuestionnaireTitle
{
    public class InputQuestionnaireTitleModel
    {
        /// <summary>
        /// 问卷题目
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 题目类型 1-单选题 2-多选题 3-问答题
        /// </summary>
        public int TitleType { get; set; }
        /// <summary>
        /// 所属问卷
        /// </summary>
        public Guid WenJuanId { get; set; }
        /// <summary>
        /// 题目图片名称
        /// </summary>
        public string TitleImgName { get; set; }
        /// <summary>
        /// 题目图片路径
        /// </summary>
        public string TitleImgPath { get; set; }
        public string TitleOptions { get; set; }
        public List<QuestionnaireOptionsDto> WenJuanTitleOptions { get; set; }
    }
}
