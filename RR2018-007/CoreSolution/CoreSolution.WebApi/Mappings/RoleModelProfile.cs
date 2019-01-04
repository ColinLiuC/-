using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.Role;

namespace CoreSolution.WebApi.Mappings
{
    public class RoleModelProfile : Profile, IProfile
    {
        public RoleModelProfile()
        {
            CreateMap<RoleDto, OutputRoleModel>()
                 .ForMember(i => i.Permissions, i => i.MapFrom(m => m.RolePermissions.Select(s => s.PermissionId).ToArray()));
        }
    }
}
