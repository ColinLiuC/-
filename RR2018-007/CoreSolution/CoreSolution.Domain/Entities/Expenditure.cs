using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 经费使用登记管理
    /// </summary>
    public class Expenditure : EntityBaseFull
    {
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid StationId { get; set; }
        /// <summary>
        /// 经费名称
        /// </summary>
        public string ExpenditureName { get; set; }
        /// <summary>
        /// 经费类型
        /// </summary>
        public ExpenditureCateGory Category { get; set; }
        /// <summary>
        /// 使用金额
        /// </summary>
        public decimal UseMoney { get; set; }
        /// <summary>
        /// 使用日期
        /// </summary>
        public DateTime? UseDate { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string DutyPeople { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string Purpose { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string Accessory { get; set; }

        /// <summary>
        /// 附件地址
        /// </summary>
        public string AccessoryUrl { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string RegisterPeople { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime? RegisterDate { get; set; }
    }
}
