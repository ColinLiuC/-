using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 题库选项表
    /// </summary>
   public class QuestionBankOption : EntityBaseFull
    {
        /// <summary>
        /// 题库题目Id
        /// </summary>
        public Guid QuestionId { get; set; }
        /// <summary>
        /// 题库题目选项
        /// </summary>
        public string TitleOptions { get; set; }
    }
}
