using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.Organization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class OrganizationProfile : Profile, IProfile
    {
        public OrganizationProfile()
        {
            CreateMap<Organization, OrganizationDto>();
            CreateMap<OrganizationDto, Organization>();

            CreateMap<InputOrganizationModel, OrganizationDto>();
            CreateMap<OrganizationDto, InputOrganizationModel>();

        }
    }
}
