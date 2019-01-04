using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;
using CoreSolution.Dto.Base;

namespace CoreSolution.Dto
{
    public class NoticeDto : EntityBaseFullDto
    {
        //公告标题
        public string NoticeTitle { get; set; }
        //公告内容
        public string NoticeInfo { get; set; }
        //发布渠道
        public NoticeChannel NoticeChannel { get; set; }
        //下发人
        public string NoticePeople { get; set; }
        //下发时间
        public DateTime NoticeTime { get; set; }
        //公告状态
        public NoticeState NoticeState { get; set; }
        //所属街道ID
        public Guid StreetId { get; set; }
        //所属街道名称
        public string StreetName { get; set; }
    }
}
