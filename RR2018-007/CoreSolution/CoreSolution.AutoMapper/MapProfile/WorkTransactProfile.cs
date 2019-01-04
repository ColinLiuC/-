using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class WorkTransactProfile : Profile, IProfile
    {
        public WorkTransactProfile()
        {
            CreateMap<WorkTransact, WorkTransactDto>();
            CreateMap<WorkTransactDto, WorkTransact>();
        }
    }
}
