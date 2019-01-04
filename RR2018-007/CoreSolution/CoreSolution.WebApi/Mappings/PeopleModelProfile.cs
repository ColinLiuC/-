using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.People;

namespace CoreSolution.WebApi.Mappings
{
    public class PeopleModelProfile : Profile, IProfile
    {
        public PeopleModelProfile()
        {
            CreateMap<PeopleDto, OutputPeopleModel>();
        }
    }
}
