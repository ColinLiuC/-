using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
   public class Activity: EntityBaseFull
    {

        /// <summary>
        /// 活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public int ActivityType { get; set; }
        /// <summary>
        /// 主办单位
        /// </summary>
        public string HostUnit { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonCharge { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// 会议室
        /// </summary>
        public Guid MeetingRoom { get; set; }
        
        /// <summary>
        /// 活动地址
        /// </summary>
        public string ActivityAddress { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
      
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid Street { get; set; }
        /// <summary>
        /// 街道名称
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid PostStation { get; set; }
        /// <summary>
        /// 驿站名称
        /// </summary>
        public string PostStationName { get; set; }
        ///<summary>
        ///报名人数
        ///</summary>
        public int? NumberParticipants { get; set; }
        /// <summary>
        /// 预计参加人数
        /// </summary>
        public int? ExpectedNumberParticipants { get; set; }
        /// <summary>
        /// 活动详情
        /// </summary>
        public string DetailsActivities { get; set; }
        public string Attachments { get; set; }
        /// <summary>
        /// 二维码地址
        /// </summary>
        public string QRCode { get; set; }
        /// <summary>
        ///  活动状态 0-审核不通过 1-待审核 2-报名中 3-开展中 4-待归档 5-已归档
        /// </summary>
        public int? ActiveState { get; set; }
        /// <summary>
        /// 活动图片名称
        /// </summary>
        public string ActivityImg { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string AttachmentPath { get; set; }
        /// <summary>
        /// 浏览量
        /// </summary>
        public int? BrowsingVolume { get; set; }
        /// <summary>
        /// 活动周期类型 1-单次活动  2-长期活动
        /// </summary>
        public int? ActivityCycleType { get; set; }
        /// <summary>
        /// 活动时间描述
        /// </summary>
        public string ActivityTimeDesc { get; set; }

        #region  审核信息
        /// <summary>
        /// 是否通过审核
        /// </summary>
        public int? AduitIsPass { get; set; }
        /// <summary>
        /// 审核备注
        /// </summary>
        public string AduitRemarks { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public DateTime? AduitDate { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string Auditor { get; set; }
        /// <summary>
        /// 是否置顶 1-置顶 0-未置顶
        /// </summary>
        public int? Flag { get; set; }
        /// <summary>
        /// 置顶时间
        /// </summary>
        public DateTime? TopDate { get; set; }
        #endregion
        #region 归档信息
        /// <summary>
        /// 是否归档
        /// </summary>
        public int? IsGuiDang { get; set; }
        /// <summary>
        /// 归档备注
        /// </summary>
        public string ArchivalRemark { get; set; }
        /// <summary>
        /// 归档人
        /// </summary>
        public string Archiving { get; set; }
        /// <summary>
        /// 归档日期
        /// </summary>
        public DateTime? FilingDate { get; set; }
        #endregion
    }
}
