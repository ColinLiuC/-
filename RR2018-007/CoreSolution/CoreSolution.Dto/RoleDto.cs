using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Dto.Base;

namespace CoreSolution.Dto
{
    public class RoleDto : EntityBaseFullDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? CreatorUserId { get; set; }
        public UserDto CreatorUser { get; set; }
        public Guid? DeleterUserId { get; set; }
        public UserDto DeleterUser { get; set; }
        public IList<UserRoleDto> UserRoles { get; set; }
        public IList<RolePermissionDto> RolePermissions { get; set; }
    }
}
