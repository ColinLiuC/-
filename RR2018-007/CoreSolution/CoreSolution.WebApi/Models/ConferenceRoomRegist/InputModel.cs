using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ConferenceRoomRegist
{
    public class InputModel
    {
        /// <summary>
        /// 会议/活动主题
        /// </summary>
        public string ConferenceTheme { get; set; }
        /// <summary>
        /// 会议/会议/活动类型
        /// </summary>
        public Guid ConferenceType { get; set; }
        /// <summary>
        /// 会议/活动室
        /// </summary>
        public Guid ConferenceRoomId { get; set; }
        /// <summary>
        /// 主办单位
        /// </summary>
        public string HostUnit { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// 申请单位
        /// </summary>
        public string ApplicationUnit { get; set; }
        /// <summary>
        /// 申请人
        /// </summary>
        public string Applicant { get; set; }
        /// <summary>
        /// 申请人联系电话
        /// </summary>
        public string ApplicanContactNumbert { get; set; }
        /// <summary>
        /// 会议/活动开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 会议/活动结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 时间标识 1-上午 2-下午
        /// </summary>
        public int TimeStamp { get; set; }
        /// <summary>
        /// 参加人数
        /// </summary>
        public int participants { get; set; }
        /// <summary>
        /// 是否公开
        /// </summary>
        public bool IsOpen { get; set; }
        /// <summary>
        /// 所需服务分类 
        /// </summary>
        public string ServiceClassification { get; set; }
        /// <summary>
        /// 会前服务
        /// </summary>
        public string PreConferenceService { get; set; }
        /// <summary>
        /// 会中服务
        /// </summary>
        public string ServiceInMeeting { get; set; }
        /// <summary>
        /// 会后服务
        /// </summary>
        public string PostConferenceService { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
