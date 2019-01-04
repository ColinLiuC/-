using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.KarmaMember;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class KarmaMemberConfig : Profile, IProfile
    {
        public KarmaMemberConfig()
        {
            CreateMap<KarmaMember, KarmaMemberDto>();
            CreateMap<KarmaMemberDto, KarmaMember>();

            CreateMap<InputKarmaMemberModel, KarmaMemberDto>();
            CreateMap<KarmaMemberDto, InputKarmaMemberModel>();
        }
    }
}