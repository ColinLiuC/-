using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
    /// <summary>
    /// 使用情况登记 n
    /// </summary>
    public class RegistrationOfUseDto : EntityBaseFullDto
    {
        /// <summary>
        /// 固定资产Id
        /// </summary>
        public Guid FixedAssetsId { get; set; }
        public string FixedAssetsNumber { get; set; }
        public string FixedAssetsName { get; set; }

        /// <summary>
        /// 领用人
        /// </summary>
        public string ReceivePeople { get; set; }
        /// <summary>
        /// 领用日期
        /// </summary>
        public DateTime? ReceiveDate { get; set; }
        /// <summary>
        /// 预计归还日期
        /// </summary>
        public DateTime? PredictReturnDate { get; set; }
        /// <summary>
        ///    当前状态 【使用情况】 （未归还，已归还）     
        /// </summary>
        public UseCurrentState CurrentState { get; set; }
        public string CurrentStateDescription{ get; set; }
        /// <summary>
        /// 归还日期
        /// </summary>
        public DateTime? ReturnDate { get; set; }
        /// <summary>
        /// 登记人  【使用情况】
        /// </summary>
        public string RegisterPeople { get; set; }
        /// <summary>
        /// 登记日期 【使用情况】
        /// </summary>
        public DateTime? RegisterDate { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
