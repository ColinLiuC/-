using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class ResidentWorkProfile : Profile, IProfile
    {
        public ResidentWorkProfile()
        {
            CreateMap<ResidentWork, ResidentWorkDto>();
            CreateMap<ResidentWorkDto, ResidentWork>();

            //CreateMap<InputResidentWorkModel, ResidentWorkDto>();


       

        }
    }
}
