using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class ApplicationServiceDto
    {
        /// <summary>
        /// 服务信息表Id值
        /// </summary>
        public Guid serviceId { get; set; }
        /// <summary>
        /// 服务所属街道Id值
        /// </summary>
        public Guid? streetId { get; set; }
        /// <summary>
        /// 服务所属驿站Id值
        /// </summary>
        public Guid? postStationId { get; set; }
        /// <summary>
        /// 当前状态 1-未接收 2-接收未评价 3-未通过 4-已评价
        /// </summary>
        public int? CurrentState { get; set; }

        /// <summary>
        /// 申请来源 0-pc端登记 1-App申请
        /// </summary>
        public int? ApplicationSource { get; set; }

        /// <summary>
        /// 评价登记状态 1-未登记 2-已登记
        /// </summary>
        public int? PJ_RegistStatus { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public int Category { get; set; }

        /// <summary>
        /// 接收日期 (和pc端申请服务的登记日期同一字段)
        /// </summary>
        public DateTime? RegisterDate { get; set; }

        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
    }
}
