using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ConferenceRoomRegist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class ConferenceRoomRegistProfile:Profile, IProfile
    {
        public ConferenceRoomRegistProfile()
        {
            CreateMap<ConferenceRoomRegistDto, OutputModel>();
            CreateMap<OutputModel, ConferenceRoomRegistDto>();

            CreateMap<ConferenceRoomRegistDto, InputModel>();
            CreateMap<InputModel, ConferenceRoomRegistDto>();

            CreateMap<ConferenceRoomRegistDto, ConferenceRoomRegist>();
            CreateMap<ConferenceRoomRegist, ConferenceRoomRegistDto>();
        }
    }
}
