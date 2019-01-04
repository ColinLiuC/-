using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ConferenceEquipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class ConferenceEquipmentProfile : Profile, IProfile
    {
        public ConferenceEquipmentProfile()
        {
            CreateMap<ConferenceEquipmentDto, OutputModel>();
            CreateMap<OutputModel, ConferenceEquipmentDto>();

            CreateMap<ConferenceEquipmentDto, InputModel>();
            CreateMap<InputModel, ConferenceEquipmentDto>();

            CreateMap<ConferenceEquipmentDto, ConferenceEquipment>();
            CreateMap<ConferenceEquipment, ConferenceEquipmentDto>();
        }
    }
}
