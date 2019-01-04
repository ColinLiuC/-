
using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Dto.Base;

namespace CoreSolution.Dto
{
    public class RolePermissionDto : EntityBaseFullDto
    {
        public Guid PermissionId { get; set; }

        public Guid RoleId { get; set; }
        public RoleDto RoleDto { get; set; }
    }
}
