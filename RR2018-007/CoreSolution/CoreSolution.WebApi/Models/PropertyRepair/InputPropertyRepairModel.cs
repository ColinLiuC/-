using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Models.PropertyRepair
{
    public class InputPropertyRepairModel
    {
        /// <summary>
        /// 报修时间
        /// </summary>
        [Required]
        public DateTime RepairTime { get; set; }
        /// <summary>
        /// 报修事项
        /// </summary>
        [Required]
        public string RepairMatter { get; set; }
        /// <summary>
        /// 报修地点
        /// </summary>
        [Required]
        public string RepairAddress { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [Required]
        public string ContactUser { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [Required]
        public string ContactTel { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        [Required]
        public Guid StreetId { get; set; }

        /// <summary>
        /// 所属驿站
        /// </summary>
        [Required]
        public Guid StationId { get; set; }
        /// <summary>
        /// 所属居委
        /// </summary>
        [Required]
        public Guid JuWeiId { get; set; }
        /// <summary>
        /// 所属小区
        /// </summary>
        [Required]
        public Guid QuartersId { get; set; }
        /// <summary>
        /// 所属物业
        /// </summary>
        [Required]
        public Guid PropertyId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string RegisterUser { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public ProStatusCode StatusCode { get; set; } = ProStatusCode.No;

    }


    public class SearchPropertyRepairModel
    {
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid? StreetId { get; set; }
        /// <summary>
        /// 所属居委
        /// </summary>
        public Guid? JuWeiId { get; set; }
        public Guid? StationId { get; set; }
        /// <summary>
        /// 所属小区
        /// </summary>
        public Guid? QuartersId { get; set; }
        /// <summary>
        /// 所属物业
        /// </summary>
        public Guid? PropertyId { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public ProStatusCode? StatusCode { get; set; }

        public DateTime? RepairTime_Start { get; set; }
        public DateTime? RepairTime_End { get; set; }
        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;

    }
}
