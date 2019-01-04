using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.PublicIncome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class PublicIncomeProfile : Profile, IProfile
    {
        public PublicIncomeProfile()
        {
            CreateMap<PublicIncome, PublicIncomeDto>();
            CreateMap<PublicIncomeDto, PublicIncome>();

            CreateMap<InputPublicIncomeModel, PublicIncomeDto>();
            CreateMap<PublicIncomeDto, InputPublicIncomeModel>();
        }
    }
}
