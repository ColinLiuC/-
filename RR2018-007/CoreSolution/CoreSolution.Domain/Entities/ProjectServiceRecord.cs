using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
   public class ProjectServiceRecord: EntityBaseFull
    {
        /// <summary>
        /// 所属项目Id
        /// </summary>
        public Guid ProjectId { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid Street { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid Station { get; set; }
        /// <summary>
        /// 服务地点
        /// </summary>
        public string ServicePlace { get; set; }
        /// <summary>
        /// 服务日期
        /// </summary>
        public DateTime ServiceDate { get; set; }
        /// <summary>
        /// 服务类型
        /// </summary>
        public Guid ServiceType { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string ChargePerson { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// 服务人数
        /// </summary>
        public int ServiceNumber { get; set; }
        /// <summary>
        /// 服务详情
        /// </summary>
        public string ServiceInfo { get; set; }
        /// <summary>
        /// 附件名称
        /// </summary>
        public string AttachmentName { get; set; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string AttachmentPath { get; set; }
        /// <summary>
        /// 服务评价
        /// </summary>
        public string ServicePingjia { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string Registrant { get; set; }
        /// <summary>
        ///  登记日期
        /// </summary>
        public DateTime RegistrationDate { get; set; }
    }
}
