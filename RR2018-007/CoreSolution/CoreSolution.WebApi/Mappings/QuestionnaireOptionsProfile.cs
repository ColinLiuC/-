using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.QuestionnaireOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class QuestionnaireOptionsProfile : Profile, IProfile
    {
        public QuestionnaireOptionsProfile()
        {          
            CreateMap<QuestionnaireOptionsDto, InputQuestionnaireOptionsModel>();
            CreateMap<InputQuestionnaireOptionsModel, QuestionnaireOptionsDto>();
        }
    }
}
