using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System.Linq;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class NoticeProfile : Profile, IProfile
    {
        public NoticeProfile()
        {
            CreateMap<Notice, NoticeDto>();
            CreateMap<NoticeDto, Notice>();
        }
    }
}
