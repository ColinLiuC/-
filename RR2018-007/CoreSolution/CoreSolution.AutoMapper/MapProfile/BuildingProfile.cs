using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
   public class BuildingProfile : Profile, IProfile
    {
        public BuildingProfile()
        {
            CreateMap<Building, BuildingDto>();
            CreateMap<BuildingDto, Building>();
        }
    }
}
