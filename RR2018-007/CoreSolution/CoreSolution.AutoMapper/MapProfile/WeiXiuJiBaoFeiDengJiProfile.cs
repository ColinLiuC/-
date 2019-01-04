using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
namespace CoreSolution.AutoMapper.MapProfile
{
    public class WeiXiuJiBaoFeiDengJiProfile : Profile, IProfile
    {
        public WeiXiuJiBaoFeiDengJiProfile()
        {
            CreateMap<WeiXiuJiBaoFeiDengJi, WeiXiuJiBaoFeiDengJiDto>();

            CreateMap<WeiXiuJiBaoFeiDengJiDto, WeiXiuJiBaoFeiDengJi>();
        }
    }
}
