using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ProjectServiceRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class ProjectServiceRecordProfile : Profile, IProfile
    {
        public ProjectServiceRecordProfile()
        {
            CreateMap<ProjectServiceRecord, ProjectServiceRecordDto>();
            CreateMap<ProjectServiceRecordDto, ProjectServiceRecord>();

            CreateMap<InputProjectServiceRecordModel, ProjectServiceRecordDto>();
            CreateMap<ProjectServiceRecordDto, InputProjectServiceRecordModel>();

            CreateMap<OutputProjectServiceRecordModel, ProjectServiceRecordDto>();
            CreateMap<ProjectServiceRecordDto, OutputProjectServiceRecordModel>();
        }
    }
}
