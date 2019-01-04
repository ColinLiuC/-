using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.FixedAssets;

namespace CoreSolution.WebApi.Mappings
{
    public class FixedAssetsModelProfile : Profile, IProfile
    {
        public FixedAssetsModelProfile()
        {
            CreateMap<FixedAssetsDto, OutputFixedAssetsModel>();
            CreateMap<OutputFixedAssetsModel, FixedAssetsDto>();

            CreateMap<FixedAssetsDto, InputFixedAssetsModel>();
            CreateMap<InputFixedAssetsModel, FixedAssetsDto>();

            CreateMap<FixedAssetsDto, ResidentWork>();
            CreateMap<ResidentWork, FixedAssetsDto>();

            CreateMap<FixedAssetsDto, SearchFixedAssetsModel>();
            CreateMap<SearchFixedAssetsModel, FixedAssetsDto>();

        }
    }
}
