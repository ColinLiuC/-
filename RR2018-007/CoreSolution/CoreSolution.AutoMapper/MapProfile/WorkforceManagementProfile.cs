using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
namespace CoreSolution.AutoMapper.MapProfile
{
    class WorkforceManagementProfile : Profile, IProfile
    {
        public WorkforceManagementProfile()
        {
            CreateMap<WorkforceManagement, WorkforceManagementDto>();

            CreateMap<WorkforceManagementDto, WorkforceManagement>();
        }
    }
}
