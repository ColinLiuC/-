using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.PeopleAndActivity;

namespace CoreSolution.WebApi.Mappings
{
    public class PeopleAndActivityModelProfile : Profile, IProfile
    {
        public PeopleAndActivityModelProfile()
        {
            CreateMap<PeopleAndActivityDto, OutputPeopleAndActivityModel>();
        }
    }
}
