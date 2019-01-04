using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.House;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class HouseProfile : Profile, IProfile
    {
        public HouseProfile()
        {
            CreateMap<House, HouseDto>();
            CreateMap<HouseDto, House>();

            CreateMap<InputHouseModel, HouseDto>();
            CreateMap<HouseDto, InputHouseModel>();
        }
    }
}