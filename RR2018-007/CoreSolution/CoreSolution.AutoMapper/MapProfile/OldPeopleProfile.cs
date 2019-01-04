using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class OldPeopleProfile : Profile, IProfile
    {
        public OldPeopleProfile()
        {
            CreateMap<OldPeople, OldPeopleDto>();
            CreateMap<OldPeopleDto, OldPeople>();
        }
    }
}
