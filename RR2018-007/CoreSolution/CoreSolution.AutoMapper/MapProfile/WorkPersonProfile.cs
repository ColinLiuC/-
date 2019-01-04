using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.AutoMapper.MapProfile
{
    public class WorkPersonProfile : Profile, IProfile
    {
        public WorkPersonProfile()
        {
            CreateMap<WorkPerson, WorkPersonDto>();
            CreateMap<WorkPersonDto, WorkPerson>();
        }
    }
}
