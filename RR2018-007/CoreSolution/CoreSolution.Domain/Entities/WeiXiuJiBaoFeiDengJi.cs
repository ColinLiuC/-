using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 维修及报废登记 n 
    /// </summary>
    public class WeiXiuJiBaoFeiDengJi : EntityBaseFull
    {
        /// <summary>
        /// 固定资产Id
        /// </summary>
        public Guid FixedAssetsId { get; set; }
        /// <summary>
        /// 类型 （）
        /// </summary>
        public WeiXiuJiBaoFei Category { get; set; }
        /// <summary>
        /// 发生日期
        /// </summary>
        public DateTime? HappenDate { get; set; }
        /// <summary>
        /// 当前状况 （已维修完成，已报废）
        /// </summary>
        public WeiXiuCurrentState CurrentState { get; set; }
        /// <summary>
        /// 维修完成日期
        /// </summary>
        public DateTime? FinishDate { get; set; }
        /// <summary>
        /// 登记人 【维修及报废】
        /// </summary>
        public string RegisterPeople { get; set; }
        /// <summary>
        /// 登记日期 【维修及报废】
        /// </summary>
        public DateTime? RegisterDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
