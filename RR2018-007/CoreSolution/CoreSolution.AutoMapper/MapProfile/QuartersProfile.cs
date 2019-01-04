using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class QuartersProfile : Profile, IProfile
    {
        public QuartersProfile()
        {
            CreateMap<Quarters, QuartersDto>();
            CreateMap<QuartersDto, Quarters>();
        }
    }
}
