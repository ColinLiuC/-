using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities.Base;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 菜单表，可以无限级循环
    /// </summary>
    public class Menu : EntityBaseFull
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string CustomData { get; set; }
        public string Icon { get; set; }
        public string ClassName { get; set; }
        public int OrderIn { get; set; }
        public Guid? CreatorUserId { get; set; }
        public virtual User CreatorUser { get; set; }
        public Guid? DeleterUserId { get; set; }
        public virtual User DeleterUser { get; set; }

        /// <summary>
        /// 所需权限名称对象，如：“ShiMinList”
        /// </summary>
        public string PermissionTarget { get; set; }

        public Guid? ParentId { get; set; }

    }
}
