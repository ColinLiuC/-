using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public  class QuestionBankOptionDto : EntityBaseFullDto
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
