using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class ResourceOrgProfile : Profile, IProfile
    {
        public ResourceOrgProfile()
        {
            CreateMap<ResourceOrg, ResourceOrgDto>();
            CreateMap<ResourceOrgDto, ResourceOrg>();
        }
    }
}
