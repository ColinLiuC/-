using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class ResourcePlaceProfile : Profile, IProfile
    {
        public ResourcePlaceProfile()
        {
            CreateMap<ResourcePlace, ResourcePlaceDto>();
            CreateMap<ResourcePlaceDto, ResourcePlace>();
        }
    }
}
