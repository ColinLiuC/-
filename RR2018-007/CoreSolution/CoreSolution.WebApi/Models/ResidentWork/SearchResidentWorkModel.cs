using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ResidentWork
{
    public class SearchResidentWorkModel
    {
        /// <summary>
        /// 事项名称
        /// </summary>
        public string ResidentWorkName { get; set; }
        /// <summary>
        /// 事项分类
        /// </summary>
        public int? ResidentWorkType { get; set; }
       
        /// <summary>
        /// 是否发布
        /// </summary>
        public bool? IsPublish { get; set; }

        /// <summary>
        /// 是否归档
        /// </summary>
        public bool? IsGuiDang { get; set; }

        //所属街道
        public Guid StreetId { get; set; }

        //所属驿站（多个）
        public string StationIds { get; set; }

    }
}
