using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class RegisterProfile : Profile, IProfile
    {
        public RegisterProfile()
        {
            CreateMap<Register, RegisterDto>();
            CreateMap<RegisterDto, Register>();
        }
    }
}
