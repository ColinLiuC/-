using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ResidentWork;
using CoreSolution.WebApi.Models.WorkTransact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class WorkTransactProfile : Profile, IProfile
    {
        public WorkTransactProfile()
        {
            CreateMap<WorkTransactDto, InputWorkTransactModel>();
            CreateMap<InputWorkTransactModel, WorkTransactDto>();

            CreateMap<WorkTransactDto, OutputWorkTransactModel>();
            
        }
    }
}
