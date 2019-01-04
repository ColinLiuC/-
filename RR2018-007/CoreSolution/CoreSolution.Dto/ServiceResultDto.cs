using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class ServiceResultDto
    {
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 服务所属分类
        /// </summary>
        public int? ServiceCategory { get; set; }
        /// <summary>
        /// 当前状态 1-未接收 2-接收未评价 3-未通过 4-已评价
        /// </summary>
        public int? CurrentState { get; set; }

        /// <summary>
        /// 评价登记状态 1-未登记 2-已登记
        /// </summary>
        public int? PJ_RegistStatus { get; set; }
        /// <summary>
        /// 申请日期 --开始时间
        /// </summary>
        public DateTime? startTime { get; set; }
        /// <summary>
        /// 申请日期 --结束时间
        /// </summary>
        public DateTime? endTime { get; set; }
        /// <summary>
        /// 类别 1--网上接收 2--服务结果
        /// </summary>
        public int ApplicationType { get; set; }
        /// <summary>
        /// 街道Id
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 驿站Id
        /// </summary>
        public Guid PostStationId { get; set; }
        /// <summary>
        /// 申请来源 0-pc端登记 1-App申请
        /// </summary>
        public int? ApplicationSource { get; set; }
    }
}
