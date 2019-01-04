using AutoMapper;
using CoreSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
   public class ActivityProfile: Profile, IProfile
    {
        public ActivityProfile()
        {
            CreateMap<Activity, Dto.ActivityDto>();
            CreateMap<Dto.ActivityDto, Activity>();
        }
    }
}
