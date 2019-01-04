using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
namespace CoreSolution.AutoMapper.MapProfile
{
    public class RegistrationOfUseProfile : Profile, IProfile
    {
        public RegistrationOfUseProfile()
        {
            CreateMap<RegistrationOfUse, RegistrationOfUseDto>();

            CreateMap<RegistrationOfUseDto, RegistrationOfUse>();
        }
    }
}
