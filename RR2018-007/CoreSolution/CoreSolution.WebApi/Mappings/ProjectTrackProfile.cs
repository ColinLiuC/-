using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ProjectTrack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class ProjectTrackProfile : Profile, IProfile
    {
        public ProjectTrackProfile()
        {
            CreateMap<ProjectTrack, ProjectTrackDto>();
            CreateMap<ProjectTrackDto, ProjectTrack>();

            CreateMap<InputProjectTrackModel, ProjectTrackDto>();
            CreateMap<ProjectTrackDto, InputProjectTrackModel>();
        }
    }
}
