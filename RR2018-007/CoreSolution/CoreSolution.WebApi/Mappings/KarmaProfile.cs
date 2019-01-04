using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.Karma;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class KarmaProfile : Profile, IProfile
    {
        public KarmaProfile()
        {
            CreateMap<Karma, KarmaDto>();
            CreateMap<KarmaDto, Karma>();

            CreateMap<InputKarmaModel, KarmaDto>();
            CreateMap<KarmaDto, InputKarmaModel>();
        }
    }
}
