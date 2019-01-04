using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.PropertyServe
{
    public class OutputPropertyServeModel
    {

        public Guid Id { get; set; }
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
