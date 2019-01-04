using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class PropertyProfile : Profile, IProfile
    {
        public PropertyProfile()
        {
            CreateMap<Property, PropertyDto>();
            CreateMap<PropertyDto, Property>();

            CreateMap<InputPropertyModel, PropertyDto>();
            CreateMap<PropertyDto, InputPropertyModel>();

        }
    }
}
