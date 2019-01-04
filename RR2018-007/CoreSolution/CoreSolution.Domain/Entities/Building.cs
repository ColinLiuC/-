using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 楼栋表
    /// </summary>
    public class Building : EntityBaseFull
    {
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 所属小区
        /// </summary>
        public Guid QuartersId { get; set; }
        public string QuartersName { get; set; }
        /// <summary>
        /// 是否有电梯
        /// </summary>

        public bool IsElevator { get; set; }
        /// <summary>
        /// 所属年份
        /// </summary>
        public string BelongedYear { get; set; }
        /// <summary>
        /// 物业
        /// </summary>
        public string Property { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        public Guid StreetId { get; set; }
        public Guid StationId { get; set; }
        public Guid JuWeiId { get; set; }
        public Guid PropertyId { get; set; }
    }
}
