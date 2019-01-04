using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 问卷选项表
    /// </summary>
   public class QuestionnaireOptions : EntityBaseFull
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
