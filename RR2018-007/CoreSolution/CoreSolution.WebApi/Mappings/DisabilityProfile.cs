using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
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
