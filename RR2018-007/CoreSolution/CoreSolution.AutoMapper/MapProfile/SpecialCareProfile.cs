using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class SpecialCareProfile : Profile, IProfile
    {
        public SpecialCareProfile()
        {
            CreateMap<SpecialCare, SpecialCareDto>();
            CreateMap<SpecialCareDto, SpecialCare>();
        }

    }
}
