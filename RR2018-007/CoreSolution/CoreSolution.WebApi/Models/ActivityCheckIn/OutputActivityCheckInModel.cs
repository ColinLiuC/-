using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ActivityCheckIn
{
    public class OutputActivityCheckInModel: EntityBaseFullDto
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
        /// 签到日期
        /// </summary>
        public DateTime? SignUpDate { get; set; }
    }
}
