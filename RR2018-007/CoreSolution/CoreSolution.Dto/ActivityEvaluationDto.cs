using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class ActivityEvaluationDto : EntityBaseFullDto
    {
        /// <summary>
        /// 评价人姓名
        /// </summary>
        public string EvaluationName { get; set; }
        /// <summary>
        /// 对应的活动Id值
        /// </summary>
        public Guid ActivityId { get; set; }
        /// <summary>
        /// 活动评分
        /// </summary>
        public int Score { get; set; }
        /// <summary>
        /// 评价人头像
        /// </summary>
        public string EvaluatorImgPath { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string EvaluationContent { get; set; }
    }
}
