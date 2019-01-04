using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
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
