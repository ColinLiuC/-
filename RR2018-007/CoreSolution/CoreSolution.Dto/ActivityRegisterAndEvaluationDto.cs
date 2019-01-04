using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    /// <summary>
    /// 活动，活动申请，活动评价表
    /// </summary>
    public class ActivityRegisterAndEvaluationDto
    {
        /// <summary>
        /// 对应的活动Id值
        /// </summary>
        public Guid? ActivityId { get; set; }
        /// <summary>
        /// 对应的活动名称
        /// </summary>
        public string ActivityName { get; set; }
        /// <summary>
        /// 对应的活动类型
        /// </summary>
        public int? ActivityType { get; set; }
        /// <summary>
        /// 对应的活动地址
        /// </summary>
        public string ActivityAddress { get; set; }
        /// <summary>
        /// 对应的活动会议室
        /// </summary>
        public Guid? MeetingRoom { get; set; }
        /// <summary>
        /// 对应的活动开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 对应的活动结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
        /// <summary>
        /// 对应的活动图片名称
        /// </summary>
        public string ActivityImg { get; set; }
        /// <summary>
        /// 对应的活动图片地址
        /// </summary>
        public string AttachmentPath { get; set; }
        /// <summary>
        /// 评价人姓名
        /// </summary>
        public string EvaluationName { get; set; }
        /// <summary>
        /// 评价内容
        /// </summary>
        public string EvaluationContent { get; set; }
        /// <summary>
        /// 评价满意度
        /// </summary>
        public int? Score { get; set; }
        /// <summary>
        /// 活动申请人姓名
        /// </summary>
        public string EnrolmentName { get; set; }
        /// <summary>
        /// 活动申请表Id
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int? ActiveState { get; set; }
        /// <summary>
        /// 活动申请时间
        /// </summary>
        public DateTime? CreationTime { get; set; }
    }
}
