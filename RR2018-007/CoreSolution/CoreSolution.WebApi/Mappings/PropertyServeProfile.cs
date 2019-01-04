using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.PropertyServe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class PropertyServeProfile : Profile, IProfile
    {
        public PropertyServeProfile()
        {
            CreateMap<PropertyServe, PropertyServeDto>();
            CreateMap<PropertyServeDto, PropertyServe>();

            CreateMap<InputPropertyServeModel, PropertyServeDto>();
            CreateMap<PropertyServeDto, InputPropertyServeModel>();
        }
    }
}
