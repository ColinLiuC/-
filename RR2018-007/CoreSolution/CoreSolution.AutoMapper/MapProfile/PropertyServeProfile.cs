using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
   public class PropertyServeProfile : Profile, IProfile
    {
        public PropertyServeProfile()
        {
            CreateMap<PropertyServe, PropertyServeDto>();
            CreateMap<PropertyServeDto, PropertyServe>();
        }
    }
}

