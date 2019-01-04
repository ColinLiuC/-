using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.DesireManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class DesireManagementProfile : Profile, IProfile
    {
        public DesireManagementProfile()
        {
            CreateMap<DesireManagementDto, OutputDesireManagementModel>();
            CreateMap<OutputDesireManagementModel, DesireManagementDto>();

            CreateMap<DesireManagementDto, InputDesireManagementModel>();
            CreateMap<InputDesireManagementModel, DesireManagementDto>();

            CreateMap<DesireManagementDto, DesireManagement>();
            CreateMap<DesireManagement, DesireManagementDto>();
        }
    }
}
