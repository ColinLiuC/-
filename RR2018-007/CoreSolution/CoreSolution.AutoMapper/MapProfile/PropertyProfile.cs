using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
   public class PropertyProfile : Profile, IProfile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyDto>();
            CreateMap<PropertyDto, Property>();
        }
    }
}

