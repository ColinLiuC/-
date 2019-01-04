using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities.Base;

namespace CoreSolution.Domain.Entities
{
    public class Permission : EntityBaseFull
    {
        public string DisplayName { get; set; }

        public string TargetName { get; set; }
        public string Description { get; set; }
        public Guid? CreatorUserId { get; set; }
        public virtual User CreatorUser { get; set; }
        public Guid? DeleterUserId { get; set; }
        public virtual User DeleterUser { get; set; }

        /// <summary>
        /// 0,1
        /// </summary>
        public int permissionType { get; set; }

    }
}
