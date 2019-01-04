using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
   public class ActivityRegister : EntityBaseFull
    {
        /// <summary>
        /// 报名人姓名
        /// </summary>
        public string EnrolmentName { get; set; }  
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// 对应的活动Id值
        /// </summary>
        public Guid ActivityId { get; set; }
        /// <summary>
        /// 是否评论
        /// </summary>
        public Boolean IsComment { get; set; }
        /// <summary>
        /// 评价
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 满意度
        /// </summary>
        public int Satisfaction { get; set; }
        /// <summary>
        /// 报名日期
        /// </summary>
        public DateTime? RegistDate { get; set; }
        /// <summary>
        /// 是否发送短信 1-已发送 0-未发送
        /// </summary>
        public int? IsShortInterest { get; set; }
        /// <summary>
        /// 市民云Id
        /// </summary>
        public string ShiMinYunId { get; set; }
    }
}
