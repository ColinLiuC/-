using AutoMapper;
using CoreSolution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
   public class DiscountProfile : Profile, IProfile
    {
        public DiscountProfile()
        {
            CreateMap<Discount, Dto.DiscountDto>();
            CreateMap<Dto.DiscountDto, Discount>();
        }
    }
}
