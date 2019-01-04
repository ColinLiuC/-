using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class RegisterHistoryProfile : Profile, IProfile
    {
        public RegisterHistoryProfile()
        {
            CreateMap<RegisterHistory, RegisterHistoryDto>();
            CreateMap<RegisterHistoryDto, RegisterHistory>();
        }

    }
}
