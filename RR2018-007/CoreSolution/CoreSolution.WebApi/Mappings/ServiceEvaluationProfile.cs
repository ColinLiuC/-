using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ServiceEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class ServiceEvaluationProfile : Profile, IProfile
    {
        public ServiceEvaluationProfile()
        {
            CreateMap<ServiceEvaluationDto, OutputServiceEvaluationModel>();
            CreateMap<OutputServiceEvaluationModel, ServiceEvaluationDto>();

            CreateMap<ServiceEvaluationDto, InputServiceEvaluationModel>();
            CreateMap<InputServiceEvaluationModel, ServiceEvaluationDto>();

            CreateMap<ServiceEvaluationDto, ServiceEvaluation>();
            CreateMap<ServiceEvaluation, ServiceEvaluationDto>();
        }
    }
}
