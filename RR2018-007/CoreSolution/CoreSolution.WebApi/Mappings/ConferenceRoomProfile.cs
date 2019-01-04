using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ConferenceRoom;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class ConferenceRoomProfile : Profile, IProfile
    {
        public ConferenceRoomProfile()
        {
            CreateMap<ConferenceRoomDto, OutputConferenceRoomModel>();
            CreateMap<OutputConferenceRoomModel, ConferenceRoomDto>();

            CreateMap<ConferenceRoomDto, InputConferenceRoomModel>();
            CreateMap<InputConferenceRoomModel, ConferenceRoomDto>();

            CreateMap<ConferenceRoomDto, ConferenceRoom>();
            CreateMap<ConferenceRoom, ConferenceRoomDto>();
        }
    }
}
