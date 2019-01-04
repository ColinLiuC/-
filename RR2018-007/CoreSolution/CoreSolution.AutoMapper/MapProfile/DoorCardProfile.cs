using AutoMapper;
using CoreSolution.Domain;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class DoorCardProfile : Profile, IProfile
    {
        public DoorCardProfile()
        {
            CreateMap<DoorCard, DoorCardDto>();
            CreateMap<DoorCardDto, DoorCard>();
        }
    }
}
