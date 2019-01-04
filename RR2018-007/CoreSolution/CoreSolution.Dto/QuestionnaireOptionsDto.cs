using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
  public  class QuestionnaireOptionsDto: EntityBaseFullDto
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
