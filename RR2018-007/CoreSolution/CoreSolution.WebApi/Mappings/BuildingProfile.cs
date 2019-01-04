using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.Building;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class BuildingProfile : Profile, IProfile
    {
        public BuildingProfile()
        {
            CreateMap<BuildingDto, Building>();
            CreateMap<Building, BuildingDto>();

            CreateMap<BuildingDto, InputBuildingModel>();
            CreateMap<InputBuildingModel, BuildingDto>();
        }
    }
}
