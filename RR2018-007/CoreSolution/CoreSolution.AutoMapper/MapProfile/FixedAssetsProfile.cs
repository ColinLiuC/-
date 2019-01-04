using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class FixedAssetsProfile : Profile, IProfile
    {
        public FixedAssetsProfile()
        {
            CreateMap<FixedAssets, FixedAssetsDto>();

            CreateMap<FixedAssetsDto, FixedAssets>();
        }
    }
}
