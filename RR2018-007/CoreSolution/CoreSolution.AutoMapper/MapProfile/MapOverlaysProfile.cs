
using AutoMapper;
using CoreSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class MapOverlaysProfile : Profile, IProfile
    {
        public MapOverlaysProfile()
        {
            CreateMap<MapOverlays, Dto.MapOverlaysDto>();
            CreateMap<Dto.MapOverlaysDto, MapOverlays>();
        }
    }
}
