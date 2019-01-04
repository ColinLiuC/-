using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.DesireManagement
{
    public class OutputDesireManagementModel : EntityBaseFullDto
    {
        #region 新增微心愿
        /// <summary>
        /// 发布人
        /// </summary>
        public string Publisher { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailedAddress { get; set; }
        /// <summary>
        /// 所属街道Id
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 所属街道名称
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 所属居委Id
        /// </summary>
        public Guid JuWeiId { get; set; }
        /// <summary>
        /// 所属居委名称
        /// </summary>
        public string JuWeiName { get; set; }
        /// <summary>
        /// 所属驿站Id
        /// </summary>
        public Guid? PostStationId { get; set; }
        /// <summary>
        /// 心愿名称
        /// </summary>
        public string DesireName { get; set; }
        /// <summary>
        /// 心愿分类
        /// </summary>
        public Guid DesireCategory { get; set; }
        /// <summary>
        /// 心愿分类名称
        /// </summary>
        public string DesireCategoryName { get; set; }
        /// <summary>
        /// 心愿内容
        /// </summary>
        public string DesireContent { get; set; }
        /// <summary>
        /// 当前状态 1-未认领 2-已认领
        /// </summary>
        public int CurrentState { get; set; }
        /// <summary>
        /// 认领期限 
        /// </summary>
        public int ClaimPeriod { get; set; }
        /// <summary>
        /// 上报人
        /// </summary>
        public string ReportPerson { get; set; }
        /// <summary>
        /// 上报日期
        /// </summary>
        public DateTime? ReportDate { get; set; }
        #endregion
        #region 认领登记
        /// <summary>
        /// 认领人
        /// </summary>
        public string Claimant { get; set; }
        /// <summary>
        /// 认领人联系电话
        /// </summary>
        public string ClaimantContactNumber { get; set; }
        /// <summary>
        /// 认领人详细地址
        /// </summary>
        public string ClaimantAddress { get; set; }
        /// <summary>
        ///认领人所属街道Id
        /// </summary>
        public Guid ClaimantStreetId { get; set; }
        /// <summary>
        /// 认领人所属居委Id
        /// </summary>
        public Guid ClaimantJuWeiId { get; set; }
        /// <summary>
        /// 认领情况
        /// </summary>
        public string ClaimSituation { get; set; }
        /// <summary>
        /// 认领日期
        /// </summary>
        public DateTime? ClaimDate { get; set; }
        #endregion
        #region 归档
        /// <summary>
        /// 实现情况
        /// </summary>
        public string RealizationSituation { get; set; }
        /// <summary>
        /// 评价意见
        /// </summary>
        public string EvaluationOpinion { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string Registrant { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime RegistionDate { get; set; }
        #endregion
    }
}
