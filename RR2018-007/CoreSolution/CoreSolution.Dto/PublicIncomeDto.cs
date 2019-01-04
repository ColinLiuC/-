using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
   public class PublicIncomeDto : EntityBaseFullDto
    {
        /// <summary>
        /// 所属年月
        /// </summary>
        public DateTime BeYearMonth { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }

        public Guid StationId { get; set; }
        public string StationName { get; set; }

        /// <summary>
        /// 所属居委
        /// </summary>
        public Guid JuWeiId { get; set; }
        public string JuWeiName { get; set; }

        /// <summary>
        /// 所属小区
        /// </summary>
        public Guid QuartersId { get; set; }
        public string QuartersName { get; set; }

        /// <summary>
        /// 上周结余
        /// </summary>
        public double LastWeekBalance { get; set; }
        /// <summary>
        /// 本周结余
        /// </summary>
        public double NowWeekBalance { get; set; }
        /// <summary>
        /// 收入总金额
        /// </summary>
        public double IncomeAmount { get; set; }
        /// <summary>
        /// 支出总金额
        /// </summary>
        public double ExpenditureAmount { get; set; }

        /// <summary>
        /// 是否转入维修资金
        /// </summary>
        public bool IsRepairAmount { get; set; }
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
    }
}
