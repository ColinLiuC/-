using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ConferenceRoomRegist
{
    public class SearchConferenceRoomRegistModel
    {
        /// <summary>
        /// 会议/活动开始时间
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 会议/活动结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 街道Id
        /// </summary>
        public Guid streetId { get; set; }
        /// <summary>
        /// 驿站Id
        /// </summary>
        public Guid postStationId { get; set; }
    }
}
