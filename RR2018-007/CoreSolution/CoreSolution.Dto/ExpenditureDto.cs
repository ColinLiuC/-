using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
    /// <summary>
    /// 经费使用登记管理
    /// </summary>
    public class ExpenditureDto : EntityBaseFullDto
    {
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid StationId { get; set; }
        public string StationName { get; set; }
        /// <summary>
        /// 经费名称
        /// </summary>
        public string ExpenditureName { get; set; }
        /// <summary>
        /// 经费类型
        /// </summary>
        public ExpenditureCateGory Category { get; set; }
        public string CategoryDescription { get; set; }
        /// <summary>
        /// 使用金额
        /// </summary>
        public decimal UseMoney { get; set; }
        /// <summary>
        /// 使用日期
        /// </summary>
        public DateTime? UseDate { get; set; }

        public DateTime? StartUseDate { get; set; }
        public DateTime? EndUseDate { get; set; }
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
