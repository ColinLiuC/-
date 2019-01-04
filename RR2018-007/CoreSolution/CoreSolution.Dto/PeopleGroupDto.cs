using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{

    /// <summary>
    /// 人员分组管理 1
    /// </summary>
    public class PeopleGroupDto : EntityBaseFullDto
    {
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid StationId { get; set; }
        /// <summary>
        ///  分组名称(第一组 张三)
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        ///  分组类型
        /// </summary>
        public GroupCateGory GroupCateGory { get; set; }

        public string GroupCateGoryDescription { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string DutyPeople { get; set; }
        /// <summary>
        ///  负责人联系电话
        /// </summary>
        public string DutyPeopleTelPhone { get; set; }
        /// <summary>
        ///   备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 工作人员Ids 941aa605-3b42-456d-b825-3295df2271c3,cc7d7d76-b6d4-4b44-9be0-b73d7e938434 ....
        /// </summary>
        public string WorkPersonIds { get; set; }

        public string WorkPersonNames { get; set; }
    }
}
