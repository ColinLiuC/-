using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.PropertyRepair;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class PropertyRepairConfig : Profile, IProfile
    {
        public PropertyRepairConfig()
        {
            CreateMap<PropertyRepair, PropertyRepairDto>();
            CreateMap<PropertyRepairDto, PropertyRepair>();

            CreateMap<InputPropertyRepairModel, PropertyRepairDto>();
            CreateMap<PropertyRepairDto, InputPropertyRepairModel>();
        }
    }
}