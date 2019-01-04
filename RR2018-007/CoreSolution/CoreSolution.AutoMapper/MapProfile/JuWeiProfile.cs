using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class JuWeiProfile : Profile, IProfile
    {
        public JuWeiProfile()
        {
            CreateMap<JuWei, JuWeiDto>();
            CreateMap<JuWeiDto, JuWei>();
        }
    }
}

