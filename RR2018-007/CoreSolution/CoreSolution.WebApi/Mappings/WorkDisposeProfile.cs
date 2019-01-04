using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.WorkDispose;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class WorkDisposeProfile : Profile, IProfile
    {
        public WorkDisposeProfile()
        {
            CreateMap<WorkDisposeDto, OutputWorkDisposeModel>();
            CreateMap<ResidentWorkDto, InputWorkDisposeModel>();
            CreateMap<InputWorkDisposeModel, WorkDisposeDto>();
            CreateMap<ModifyWorkDisposeModel, WorkDisposeDto>();
        }
    }
}
