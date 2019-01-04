using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.Station;

namespace CoreSolution.WebApi.Mappings
{
    public class StationModelProfile : Profile, IProfile
    {
        public StationModelProfile()
        {
            CreateMap<StationDto,OutputStationModel >();
        }
    }
}
