using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Dto.Base;

namespace CoreSolution.Dto
{
    public class MenuDto : EntityBaseFullDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string CustomData { get; set; }
        public string Icon { get; set; }
        public string ClassName { get; set; }
        public int OrderIn { get; set; }

        /// <summary>
        /// 所需权限名称对象，如：“ShiMinList”
        /// </summary>
        public string PermissionTarget { get; set; }
        public Guid? CreatorUserId { get; set; }
        public UserDto CreatorUser { get; set; }
        public Guid? DeleterUserId { get; set; }
        public UserDto DeleterUser { get; set; }
        public Guid? ParentId { get; set; }
    }
}
