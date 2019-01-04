using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ResidentWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class ResidentWorkModelProfile: Profile, IProfile
    {
        public ResidentWorkModelProfile()
        {
            CreateMap<ResidentWorkDto,OutputResidentWorkModel>();
            CreateMap<OutputResidentWorkModel, ResidentWorkDto>();

            CreateMap<ResidentWorkDto, InputResidentWorkModel>();
            CreateMap<InputResidentWorkModel, ResidentWorkDto>();

            CreateMap<ResidentWorkDto, ResidentWork>();
            CreateMap<ResidentWork, ResidentWorkDto>();

        }
    }
}
