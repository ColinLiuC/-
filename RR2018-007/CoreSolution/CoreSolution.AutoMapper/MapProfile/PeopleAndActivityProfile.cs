using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class PeopleAndActivityProfile : Profile, IProfile
    {
        public PeopleAndActivityProfile()
        {
            CreateMap<PeopleAndActivity, PeopleAndActivityDto>();
            CreateMap<PeopleAndActivityDto, PeopleAndActivity>();
        }
    }
}
