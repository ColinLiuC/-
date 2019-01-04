using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Dto.Base;

namespace CoreSolution.Dto
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PermissionDto : EntityBaseFullDto
    {
       /// <summary>
       /// 展示名称
       /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 对应名称
        /// </summary>
        public string TargetName { get; set; }
        public string Description { get; set; }
        public Guid? CreatorUserId { get; set; }
        public UserDto CreatorUser { get; set; }
        public Guid? DeleterUserId { get; set; }
        public UserDto DeleterUser { get; set; }
        /// <summary>
        /// 0,1
        /// </summary>
        public int permissionType { get; set; }

    }
}
