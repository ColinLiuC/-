using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ServiceApplication
{
    public class InputServiceApplicationModel
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 用户市民云Id
        /// </summary>
        public string ShiMinYunId { get; set; }
        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplicantName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// pc端申请备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string Registrant { get; set; }
        /// <summary>
        /// 登记日期 -- 服务申请日期
        /// </summary>
        public DateTime RegisterDate { get; set; }
        /// <summary>
        /// 服务Id
        /// </summary>
        public Guid ServiceId { get; set; }
        #region app端申请相关信息
        /// <summary>
        /// 地址--App端申请
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 申请服务备注 App端
        /// </summary>
        public string ApplicationNotes { get; set; }
        /// <summary>
        /// 申请日期 App端
        /// </summary>
        public DateTime? ApplicationDate { get; set; }
        /// <summary>
        /// 申请来源 0-pc端登记 1-App申请
        /// </summary>
        public int? ApplicationSource { get; set; }
        /// <summary>
        /// 是否接收申请 app端
        /// </summary>
        public int? IsReceive { get; set; }
        /// <summary>
        /// 接收app端申请备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 接收人
        /// </summary>
        public string Recipient { get; set; }
        /// <summary>
        /// 接收日期
        /// </summary>
        public DateTime? ReceivingDate { get; set; }
        #endregion
        #region pc端申请的结果登记信息
        /// <summary>
        /// 服务结果
        /// </summary>
        public string ServiceResults { get; set; }
        /// <summary>
        /// 评价登记人
        /// </summary>
        public string PJ_Registrant { get; set; }
        /// <summary>
        /// 评价登记日期
        /// </summary>
        public DateTime? PJ_RegistDate { get; set; }
        /// <summary>
        /// 评价登记状态 1-未登记 2-已登记
        /// </summary>
        public int? PJ_RegistStatus { get; set; }
        #endregion
        /// <summary>
        /// 当前状态 1-未接收 2-接收未评价 3-未通过 4-已评价
        /// </summary>
        public int? CurrentState { get; set; }
        #region 用户对服务的评价
        /// <summary>
        /// 当前状态
        /// 1:已完成
        /// 2:未完成
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 评价
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 满意度
        /// </summary>
        public int Satisfaction { get; set; }
        #endregion
    }
}
