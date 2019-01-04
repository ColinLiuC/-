using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.OrganizationProject
{
    public class InputOrganizationProjectModel
    {
        /// <summary>
        /// 所属组织Id
        /// </summary>
        public Guid OrganizationId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ProjectName { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid? Street { get; set; }
        public string StreetName { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid? Station { get; set; }
        public string StationName { get; set; }
        /// <summary>
        /// 项目类别
        /// </summary>
        public Guid? ProjectCategory { get; set; }
        public string ProjectCategoryName { get; set; }
        /// <summary>
        /// 委托方
        /// </summary>
        public string Client { get; set; }
        /// <summary>
        /// 项目受益对象
        /// </summary>
        public string TargetGroup { get; set; }
        /// <summary>
        /// 项目主要内容
        /// </summary>
        public string PrimaryCoverage { get; set; }
        /// <summary>
        /// 实施时间
        /// </summary>
        public DateTime ImplementationTime { get; set; }
        /// <summary>
        /// 项目经费
        /// </summary>
        public decimal ProjectFunds { get; set; }
        /// <summary>
        /// 项目资金来源
        /// </summary>
        public string SourceFunds { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string Registrant { get; set; }
        /// <summary>
        ///  登记日期
        /// </summary>
        public DateTime RegistrationDate { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string AttachmentName { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachmentPath { get; set; }
    }
}
