using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.DoorCard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class DoorCardProfile : Profile, IProfile
    {
        public DoorCardProfile()
        {
            CreateMap<DoorCard, DoorCardDto>();
            CreateMap<DoorCardDto, DoorCard>();

            CreateMap<InputDoorCardModel, DoorCardDto>();
            CreateMap<DoorCardDto, InputDoorCardModel>();
        }
    }
}
