using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class WorkDisposeProfile : Profile, IProfile
    {
        public WorkDisposeProfile()
        {
            CreateMap<WorkDispose, WorkDisposeDto>();
            CreateMap<WorkDisposeDto, WorkDispose>();
        }

    }
}
