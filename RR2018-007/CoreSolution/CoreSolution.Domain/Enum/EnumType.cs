using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace CoreSolution.Domain.Enum
{

    /// <summary>
    /// 公告通知发布渠道
    /// </summary>
    public enum NoticeChannel
    {
        /// <summary>
        /// 全部
        /// </summary>
        [Description("全部")]
        quanbu = 0,
        /// <summary>
        /// 服务平台
        /// </summary>
        [Description("虹口市民驿站综合服务平台")]
        fuwupingtai = 1,
        /// <summary>
        /// 市民驿站APP
        /// </summary>
        [Description("市民驿站APP")]
        shiminapp = 2

    }

    /// <summary>
    /// 公告通知状态
    /// </summary>
    public enum NoticeState
    {
        /// <summary>
        /// 已发布
        /// </summary>
        [Description("已发布")]
        yifabu = 0,
        /// <summary>
        /// 未发布
        /// </summary>
        [Description("未发布")]
        weifabu = 1

    }


}
