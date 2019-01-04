using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.DesireManagement
{
    public class SearchDesireManagementModel
    {
        /// <summary>
        /// 心愿名称
        /// </summary>
        public string DesireName { get; set; }
        /// <summary>
        /// 心愿分类
        /// </summary>
        public Guid DesireCategory { get; set; }
        /// <summary>
        /// 街道Id
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 驿站Id
        /// </summary>
        public Guid PostStationId { get; set; }

    }
}
