using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
   public class ParkingLotprofile : Profile, IProfile
    {
        public ParkingLotprofile()
        {
            CreateMap<ParkingLot, ParkingLotDto>();
            CreateMap<ParkingLotDto, ParkingLot>();
        }
    }
}
