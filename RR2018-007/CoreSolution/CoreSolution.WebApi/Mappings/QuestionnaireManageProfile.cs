using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.QuestionnaireManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class QuestionnaireManageProfile: Profile, IProfile
    {
        public QuestionnaireManageProfile()
        {
            CreateMap<QuestionnaireManageDto, OutputQuestionnaireManageModel>();
            CreateMap<OutputQuestionnaireManageModel, QuestionnaireManageDto>();

            CreateMap<QuestionnaireManageDto, InputQuestionnaireManageModel>();
            CreateMap<InputQuestionnaireManageModel, QuestionnaireManageDto>();

            CreateMap<QuestionnaireManageDto, QuestionnaireManage>();
            CreateMap<QuestionnaireManage, QuestionnaireManageDto>();
        }
    }
}
