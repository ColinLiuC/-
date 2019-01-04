using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.QuarterProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class QuarterProjectProfile : Profile, IProfile
    {
        public QuarterProjectProfile()
        {
            CreateMap<QuarterProject, QuarterProjectDto>();
            CreateMap<QuarterProjectDto, QuarterProject>();

            CreateMap<InputQuarterProjectModel, QuarterProjectDto>();
            CreateMap<QuarterProjectDto, InputQuarterProjectModel>();
        }
    }
}
