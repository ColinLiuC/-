using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.EventBurst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class EventBurstProfile : Profile, IProfile
    {
        public EventBurstProfile()
        {
            CreateMap<EventBurst, EventBurstDto>();
            CreateMap<EventBurstDto, EventBurst>();

            CreateMap<InputEventBurstModel, EventBurstDto>();
            CreateMap<EventBurstDto, InputEventBurstModel>();
        }
    }
}
