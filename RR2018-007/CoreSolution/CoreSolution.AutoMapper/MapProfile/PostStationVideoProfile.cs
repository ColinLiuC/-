using AutoMapper;
using CoreSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
   public class PostStationVideoProfile : Profile, IProfile
    {
        public PostStationVideoProfile()
        {
            CreateMap<PostStationVideo, Dto.PostStationVideoDto>();
            CreateMap<Dto.PostStationVideoDto, PostStationVideo>();
        }
    }
}
