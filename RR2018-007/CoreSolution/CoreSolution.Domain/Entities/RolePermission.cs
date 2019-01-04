

using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities.Base;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 角色权限关联表
    /// </summary>
    public class RolePermission : EntityBaseFull
    {
        public Guid PermissionId { get; set; }
    
        public Guid RoleId { get; set; }

        public Role Role { get; set; }
      
    }
}
