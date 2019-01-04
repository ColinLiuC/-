using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Models.Notice
{
    /// <summary>
    /// 公告通知
    /// </summary>
    public class InputNoticeModel
    {
        /// <summary>
        /// 公告标题
        /// </summary>
        public string NoticeTitle { get; set; }
        /// <summary>
        /// 公告内容
        /// </summary>
        public string NoticeInfo { get; set; }
        /// <summary>
        /// 发布渠道
        /// </summary>
        public NoticeChannel NoticeChannel { get; set; }
        /// <summary>
        /// 下发人
        /// </summary>
        public string NoticePeople { get; set; }
        /// <summary>
        /// 下发时间
        /// </summary>
        public DateTime NoticeTime { get; set; }
        /// <summary>
        /// 公告状态
        /// </summary>
        public NoticeState NoticeState { get; set; }
        /// <summary>
        /// 所属街道ID
        /// </summary>
        public Guid? StreetId { get; set; }
        /// <summary>
        /// 所属街道名称
        /// </summary>
        public string StreetName { get; set; }

    }
}
