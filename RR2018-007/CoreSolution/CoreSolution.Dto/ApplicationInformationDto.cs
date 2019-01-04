using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class ApplicationInformationDto
    {
        /// <summary>
        /// 服务信息表Id值
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// app端申请时备注
        /// </summary>
        public string ApplicationNotes { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public int Category { get; set; }
        /// <summary>
        /// 申请日期
        /// </summary>
        public DateTime ApplicationDate { get; set;}
        /// <summary>
        /// 申请来源
        /// </summary>
        public int? ApplicationSource { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public int? CurrentState { get; set; }

        /// <summary>
        /// 评价登记状态 1-未登记 2-已登记
        /// </summary>
        public int? PJ_RegistStatus { get; set; }

        /// <summary>
        /// 登记日期 -- 服务申请日期
        /// </summary>
        public DateTime RegisterDate { get; set; }
        /// <summary>
        /// 街道Id
        /// </summary>
        public Guid? StreetId { get; set; }
        /// <summary>
        /// 驿站Id
        /// </summary>
        public Guid? PostStationId { get; set; }
    }
}
