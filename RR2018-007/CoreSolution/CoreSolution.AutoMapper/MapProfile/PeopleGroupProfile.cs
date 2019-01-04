using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;

namespace CoreSolution.AutoMapper.MapProfile
{
    class PeopleGroupProfile : Profile, IProfile
    {
        public PeopleGroupProfile()
        {
            CreateMap<PeopleGroup, PeopleGroupDto>();

            CreateMap<PeopleGroupDto, PeopleGroup>();
        }
        
    }
}
