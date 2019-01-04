using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.QuarterProject
{
    public class OutputQuarterProjectModel
    {

        public Guid Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 项目类型
        /// </summary>
        public Guid ProjectType { get; set; }
        public string ProjectTypeStr { get; set; }

        /// <summary>
        /// 申报日期
        /// </summary>
        public DateTime DeclareTime { get; set; }
        /// <summary>
        /// 实施单位
        /// </summary>
        public string Exploiting { get; set; }
        /// <summary>
        /// 实施开始时间
        /// </summary>
        public DateTime? ExploitingTime_Start { get; set; }
        /// <summary>
        /// 实施结束时间
        /// </summary>
        public DateTime? ExploitingTime_End { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string ContactUser { get; set; }
        /// <summary>
        /// 负责人电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 项目详情
        /// </summary>
        public string ProjectDetail { get; set; }
    }
}
