using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
   public class QuestionnaireAnswer : EntityBaseFull
    {
        /// <summary>
        /// 所属问卷Id
        /// </summary>
        public Guid WenJuanId { get; set; }
        /// <summary>
        /// 所属题目Id
        /// </summary>
        public Guid TitleId { get; set; }
        /// <summary>
        /// 答案
        /// </summary>
        public string Answer { get; set; }
        /// <summary>
        /// 选项名称
        /// </summary>
        public string OptionName { get; set; }
    }
}
