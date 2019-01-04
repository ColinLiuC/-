using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ProjectTrack
{
    public class OutputProjectTrackModel
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 项目id
        /// </summary>
        public Guid QuarterProjectId { get; set; }
        /// <summary>
        /// 跟踪日期
        /// </summary>
        public DateTime TrackTime { get; set; }
        /// <summary>
        /// 项目进展情况
        /// </summary>
        public string ProgressDetail { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string ProjectTrackDetail { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string RegisterUser { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime RegisterTime { get; set; }
    }
}
