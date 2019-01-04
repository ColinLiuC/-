using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.JuWei;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class JuWeiProfile : Profile, IProfile
    {
        public JuWeiProfile()
        {
            CreateMap<JuWeiDto, JuWei>();
            CreateMap<JuWei, JuWeiDto>();

            CreateMap<InputJuWeiModel, JuWeiDto>();
            CreateMap<JuWeiDto, InputJuWeiModel>();
        }
    }
}
