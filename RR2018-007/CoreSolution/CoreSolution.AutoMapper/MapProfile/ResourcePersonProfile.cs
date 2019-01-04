using AutoMapper;
using CoreSolution.Domain;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class ResourcePersonProfile : Profile, IProfile
    {
        public ResourcePersonProfile()
        {
            CreateMap<ResourcePerson, ResourcePersonDto>();
            CreateMap<ResourcePersonDto, ResourcePerson>();
        }

    }
}
