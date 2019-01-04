using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.ActivityEvaluation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class ActivityEvaluationProfile : Profile, IProfile
    {
        public ActivityEvaluationProfile()
        {
            CreateMap<ActivityEvaluationDto, OutputActivityEvaluationModel>();
            CreateMap<OutputActivityEvaluationModel, ActivityEvaluationDto>();

            CreateMap<ActivityEvaluationDto, InputActivityEvaluationModel>();
            CreateMap<InputActivityEvaluationModel, ActivityEvaluationDto>();

            CreateMap<ActivityEvaluationDto, ActivityEvaluation>();
            CreateMap<ActivityEvaluation, ActivityEvaluationDto>();
        }
    }
}
