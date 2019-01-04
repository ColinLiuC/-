using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
  public class MeetingDto
    {
        /// <summary>
        /// 会议室Id
        /// </summary>
        public Guid ConferenceRoomId { get; set; }
        /// <summary>
        /// 会议活动室名称
        /// </summary>
        public string ConferenceRoomName { get; set; }
        /// <summary>
        /// 座位数
        /// </summary>
        public int Pedestal { get; set; }
        public List<ConferenceRoomRegistDto> ConferenceRoomRegists { get; set; }
    }
}
