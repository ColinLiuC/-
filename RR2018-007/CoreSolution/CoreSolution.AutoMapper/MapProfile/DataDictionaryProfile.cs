using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class DataDictionaryProfile : Profile, IProfile
    {
        public DataDictionaryProfile()
        {
            CreateMap<DataDictionary, DataDictionaryDto>();
            CreateMap<DataDictionaryDto, DataDictionary>();

        }
    }
}
