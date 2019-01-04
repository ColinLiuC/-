using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.OrganizationProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class OrganizationProjectProfile : Profile, IProfile
    {
        public OrganizationProjectProfile()
        {          
            CreateMap<OrganizationProjectDto, InputOrganizationProjectModel>();
            CreateMap<InputOrganizationProjectModel, OrganizationProjectDto>();

            CreateMap<OrganizationProjectDto, OrganizationProject>();
            CreateMap<OrganizationProject, OrganizationProjectDto>();
        }
    }
}
