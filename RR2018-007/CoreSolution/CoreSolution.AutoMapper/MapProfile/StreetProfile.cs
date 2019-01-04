using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class StreetProfile : Profile, IProfile
    {
        public StreetProfile()
        {
            CreateMap<Street, StreetDto>();
            CreateMap<StreetDto, Street>();
        }
    }
}
