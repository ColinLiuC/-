using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
   public class ReceptionServiceProfile : Profile, IProfile
    {
        public ReceptionServiceProfile()
        {
            CreateMap<ReceptionService, ReceptionServiceDto>();
            CreateMap<ReceptionServiceDto, ReceptionService>();
        }       
    }
}
