using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class DisabilityProfile : Profile, IProfile
    {
        public DisabilityProfile()
        {
            CreateMap<Disability, DisabilityDto>();
            CreateMap<DisabilityDto, Disability>();
        }
    }
}
