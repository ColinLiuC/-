using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ParkingLot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class ParkingLotProfile : Profile, IProfile
    {
        public ParkingLotProfile()
        {
            CreateMap<ParkingLotDto, InputParkingLotModel>();
            CreateMap<InputParkingLotModel, ParkingLotDto>();
        }
    }
}
