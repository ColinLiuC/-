using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ResidentWork
{
    public class OutputResidentWorkModel
    {
        /// <summary>
        /// 事项Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 事项名称
        /// </summary>
        public string ResidentWorkName { get; set; }
        /// <summary>
        /// 事项分类
        /// </summary>
        public int ResidentWorkType { get; set; }
        public string ResidentWorkType_ds { get; set; }     

        //所属街道
        public Guid StreetId { get; set; }


        //所属驿站（多个）
        public string StationIds { get; set; }

        public string StationNames { get; set; }
        public bool IsPublish { get; set; }
        public string IsPublish_ds { get; set; }

        public bool IsGuiDang { get; set; }

    }
}
