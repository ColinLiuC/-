using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class BuildingDto : EntityBaseFullDto
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
        public string StreetName { get; set; }

        public Guid StationId { get; set; }
        public string StationName { get; set; }

        public Guid JuWeiId { get; set; }
        public string JuWeiName { get; set; }

        public Guid PropertyId { get; set; }
        public string PropertyName { get; set; }

    }
}
