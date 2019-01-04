using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Models.PublicIncome
{
    public class InputPublicIncomeModel
    {
        /// <summary>
        /// 所属年月
        /// </summary>
        [Required]
        public DateTime BeYearMonth { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        [Required]
        public Guid StreetId { get; set; }

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
        /// 上周结余
        /// </summary>
        [Required]
        public double LastWeekBalance { get; set; }
        /// <summary>
        /// 本周结余
        /// </summary>
        [Required]
        public double NowWeekBalance { get; set; }
        /// <summary>
        /// 收入总金额
        /// </summary>
        public double IncomeAmount { get; set; }
        /// <summary>
        /// 支出总金额
        /// </summary>
        [Required]
        public double ExpenditureAmount { get; set; }

        /// <summary>
        /// 是否转入维修资金
        /// </summary>
        [Required]
        public bool IsRepairAmount { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        [Required]
        public string RegisterUser { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        [Required]
        public DateTime RegisterTime { get; set; }
    }
    public class SearchPublicIncomeModel
    {

        public Guid? StreetId { get; set; }
        public Guid? StationId { get; set; }
        /// <summary>
        /// 所属居委
        /// </summary>
        public Guid? JuWeiId { get; set; }
        /// <summary>
        /// 所属小区
        /// </summary>
        public Guid? QuartersId { get; set; }
        public DateTime? BeYearMonth_Start { get; set; }
        public DateTime? BeYearMonth_End { get; set; }
        public bool? IsRepairAmount { get; set; }
        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 10;

    }
}
