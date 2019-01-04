

using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class RolePermissionProfile : Profile, IProfile
    {
        public RolePermissionProfile()
        {
            CreateMap<RolePermission, RolePermissionDto>();

            CreateMap<RolePermissionDto, RolePermission>()
                .ForMember(i => i.IsDeleted, i => i.Ignore())
                .ForMember(i => i.CreationTime, i => i.Ignore());
        }
    }
}

