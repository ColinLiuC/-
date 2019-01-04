using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities.Base;

namespace CoreSolution.Domain.Entities
{
    public class UserRole : EntityBaseFull
    {
        public Guid UserId { get; set; }
        public virtual User User { get; set; }
        public Guid RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
