using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.Activity
{
    public class SearchActivityModel
    {
        /// <summary>
        /// 街道
        /// </summary>
        public Guid? streetId { get; set; }
        /// <summary>
        /// 驿站
        /// </summary>
        public Guid? PostStationId { get; set; }
        /// <summary>
        /// 活动名称
        /// </summary>
        public string activityName { get; set; }
        /// <summary>
        /// 活动类型
        /// </summary>
        public int? type { get; set; }
        /// <summary>
        /// 活动状态
        /// </summary>
        public int? activeState { get; set; }
        /// <summary>
        /// 活动开始时间
        /// </summary>
        public DateTime ? startDate { get; set; }
        /// <summary>
        /// 活动结束时间
        /// </summary>
        public DateTime? endDate { get; set; }
        /// <summary>
        /// 满意度 1-三星以下 2-三星 3-四星 4-五星
        /// </summary>
        public int? Satisfaction { get; set; }
    }
}
