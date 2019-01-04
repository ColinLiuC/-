using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.Dto.Base;

namespace CoreSolution.WebApi.Models.Permission
{
    public class OutputPermissionModel : EntityBaseFullDto
    {
        public string DisplayName { get; set; }
        public string TargetName { get; set; }
        public string Description { get; set; }
    
    }
}
