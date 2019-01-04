using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ProjectTrack
{
    public class InputProjectTrackModel
    {
        /// <summary>
        /// 项目id
        /// </summary>
        [Required]
        public Guid QuarterProjectId { get; set; }
        /// <summary>
        /// 跟踪日期
        /// </summary>
        [Required]
        public DateTime TrackTime { get; set; }
        /// <summary>
        /// 项目进展情况
        /// </summary>
        [Required]
        public string ProgressDetail { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string ProjectTrackDetail { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        [Required]
        public string RegisterUser { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        [Required]
        public DateTime RegisterTime { get; set; }
    }

    public class SearchProjectTrackModel
    {
        public Guid? QuarterProjectId { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }
}
