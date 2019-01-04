using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.Quarters;
using CoreSolution.WebApi.Models.ResidentWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class QuartersProfile : Profile, IProfile
    {
        public QuartersProfile()
        {
            CreateMap<QuartersDto, InputQuartersModel>();
            CreateMap<InputQuartersModel, QuartersDto>();
        }
    }
}
