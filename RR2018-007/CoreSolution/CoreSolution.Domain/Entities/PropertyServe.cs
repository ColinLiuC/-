using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 物业服务
    /// </summary>
   public class PropertyServe : EntityBaseFull
    {
        /// <summary>
        /// 物业公司ID
        /// </summary>
        public Guid PropertyId { get; set; }

        public string ChargeSituation { get; set; }

        /// <summary>
        /// 收费情况
        /// </summary>
        public Guid ChargeSituationId { get; set; }

        public double? CostAmount { get; set; }

        /// <summary>
        /// 服务电话
        /// </summary>
        public string ServeTel { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        public string ServeContent { get; set; }


    }
}
