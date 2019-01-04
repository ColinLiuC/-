using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class PeopleProfile : Profile, IProfile
    {
        public PeopleProfile()
        {
            CreateMap<People, PeopleDto>();
            CreateMap<PeopleDto, People>();
        }
    }
}
