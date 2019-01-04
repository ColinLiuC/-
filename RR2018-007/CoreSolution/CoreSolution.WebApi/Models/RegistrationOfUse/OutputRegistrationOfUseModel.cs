using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.RegistrationOfUse
{
  
    /// <summary>
    /// 使用情况登记 n
    /// </summary>
    public class OutputRegistrationOfUseModel 
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 固定资产Id
        /// </summary>
        public Guid FixedAssetsId { get; set; }
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
        public int CurrentState { get; set; }
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
