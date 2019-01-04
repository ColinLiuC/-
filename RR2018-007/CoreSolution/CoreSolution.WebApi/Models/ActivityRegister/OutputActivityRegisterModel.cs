using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ActivityRegister
{
    public class OutputActivityRegisterModel: EntityBaseFullDto
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
        /// 报名日期
        /// </summary>
        public DateTime? RegistDate { get; set; }
        /// <summary>
        /// 是否发送短信 1-已发送 0-未发送
        /// </summary>
        public int? IsShortInterest { get; set; }
    }
}
