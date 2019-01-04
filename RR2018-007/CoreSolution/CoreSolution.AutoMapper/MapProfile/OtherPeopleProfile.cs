using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class OtherPeopleProfile : Profile, IProfile
    {
        public OtherPeopleProfile()
        {
            CreateMap<OtherPeople, OtherPeopleDto>();
            CreateMap<OtherPeopleDto, OtherPeople>();
        }

    }
}
