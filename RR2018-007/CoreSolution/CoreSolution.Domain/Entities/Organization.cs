using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
   public class Organization: EntityBaseFull
    {
        /// <summary>
        /// 组织名称
        /// </summary>
        public string OrganizationName { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid Street { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid Station { get; set; }
        /// <summary>
        /// 组织类型
        /// </summary>
        public Guid OrganizationType { get; set; }
        /// <summary>
        /// 行业类别
        /// </summary>
        public Guid IndustryCategory { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactNumber { get; set; }
        /// <summary>
        /// 成员人数
        /// </summary>
        public int Members { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 详情介绍
        /// </summary>
        public string DetailsIntroduction { get; set; }
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
