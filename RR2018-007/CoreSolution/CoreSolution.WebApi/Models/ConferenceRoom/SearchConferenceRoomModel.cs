using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ConferenceRoom
{
    public class SearchConferenceRoomModel
    {
        /// <summary>
        /// 会议活动室名称
        /// </summary>
        public string ConferenceRoomName { get; set; }
        /// <summary>
        /// 会议活动室类型
        /// </summary>
        public Guid ConferenceRoomType { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid PostStation { get; set; }
        /// <summary>
        /// 座位数
        /// </summary>
        public int? Pedestal { get; set; }
        /// <summary>
        /// 会议/活动室状态 
        /// </summary>
        public int? State { get; set; }

    }
}
