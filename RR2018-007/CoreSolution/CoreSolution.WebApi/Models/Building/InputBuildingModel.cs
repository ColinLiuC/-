using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.Building
{
    public class InputBuildingModel
    {

        /// <summary>
        /// 地址
        /// </summary>
        [Required]
        public string Address { get; set; }
        /// <summary>
        /// 所属小区
        /// </summary>
        [Required]
        public Guid QuartersId { get; set; }
        /// <summary>
        /// 是否有电梯
        /// </summary>
        [Required]
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

        [Required]
        public Guid StreetId { get; set; }
        [Required]
        public Guid StationId { get; set; }
        [Required]
        public Guid JuWeiId { get; set; }
        [Required]
        public Guid PropertyId { get; set; }
    }

    public class SearchBuildingModel
    {
        public string Address { get; set; }
        public bool? isElevator { get; set; }
        public string belongedYear { get; set; }

        public Guid? StreetId { get; set; }
        public Guid? StationId { get; set; }
        public Guid? JuWeiId { get; set; }
        public Guid? QuartersId { get; set; }
        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 10;

    }
}
