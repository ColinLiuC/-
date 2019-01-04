using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.Notice;

namespace CoreSolution.WebApi.Mappings
{
    public class NoticeModelProfile : Profile, IProfile
    {
        public NoticeModelProfile()
        {
            CreateMap<NoticeDto, OutputNoticeModel>();
        }

    }
}
