﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.QuestionnaireOptions
{
    public class InputQuestionnaireOptionsModel
    {
        /// <summary>
        /// 问卷题目Id
        /// </summary>
        public Guid QuestionnaireTitleId { get; set; }
        /// <summary>
        /// 问卷题目选项
        /// </summary>
        public string WenJuanTitleOptions { get; set; }
    }
}