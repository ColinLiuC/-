using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 分组成员   n
    /// </summary>
    public class GroupMember : EntityBaseFull
    {

        /// <summary>
        /// 人员分组管理Id
        /// </summary>
        public Guid PeopleGroupId { get; set; }
        /// <summary>
        /// 工作人员Id
        /// </summary>
        public Guid WorkPersonId { get; set; }
        /// <summary>
        /// 工作人员姓名
        /// </summary>
        public string WorkPersonName { get; set; }

        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid StationId { get; set; }
    }
}
