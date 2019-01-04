using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System.Linq;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class StationProfile : Profile, IProfile
    {
        public StationProfile()
        {
            CreateMap<Station, StationDto>();
            CreateMap<StationDto, Station>();
        }

    }
}
