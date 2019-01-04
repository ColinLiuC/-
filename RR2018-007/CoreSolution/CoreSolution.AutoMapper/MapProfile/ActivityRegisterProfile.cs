using AutoMapper;
using CoreSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
   public class ActivityRegisterProfile : Profile, IProfile
    {
        public ActivityRegisterProfile()
        {
            CreateMap<ActivityRegister, Dto.ActivityRegisterDto>();
            CreateMap<Dto.ActivityRegisterDto, ActivityRegister>();
        }
    }
}
