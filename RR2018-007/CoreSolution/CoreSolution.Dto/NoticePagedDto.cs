using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Dto.Base;

namespace CoreSolution.Dto
{
    public class NoticePagedDto
    {
        public int Count { get; set; }
        public IList<NoticeListDto> Data { get; set; }
        public class NoticeListDto : EntityDto<Guid>
        {
            public string NoticeTitle { get; set; }
            public string NoticeInfo { get; set; }
            public int NoticeChannel { get; set; }
            public string NoticePeople { get; set; }
            public DateTime NoticeTime { get; set; }
            public int NoticeState { get; set; }
        }
    }
}
