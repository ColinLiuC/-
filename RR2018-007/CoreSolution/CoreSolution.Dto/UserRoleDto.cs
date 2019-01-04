using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Dto.Base;

namespace CoreSolution.Dto
{
    public class UserRoleDto : EntityBaseFullDto
    {
        public Guid UserId { get; set; }
        public UserDto User { get; set; }
        public Guid RoleId { get; set; }
        public RoleDto Role { get; set; }
    }
}
