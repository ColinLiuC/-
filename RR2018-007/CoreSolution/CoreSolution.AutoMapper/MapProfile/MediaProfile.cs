using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{

    public class MediaProfile : Profile, IProfile
    {
        public MediaProfile()
        {
            CreateMap<Media, MediaDto>();
            CreateMap<MediaDto, Media>();

        }
    }
}
