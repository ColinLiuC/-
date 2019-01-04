using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class ExpenditureProfile : Profile, IProfile
    {
        public ExpenditureProfile()
        {
            CreateMap<Expenditure, ExpenditureDto>();

            CreateMap<ExpenditureDto, Expenditure>();
        }
    }
}
