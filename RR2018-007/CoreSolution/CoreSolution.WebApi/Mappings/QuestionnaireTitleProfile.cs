using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.QuestionnaireTitle;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class QuestionnaireTitleProfile : Profile, IProfile
    {
        public QuestionnaireTitleProfile()
        {
            CreateMap<QuestionnaireTitleDto, InputQuestionnaireTitleModel>();
            CreateMap<InputQuestionnaireTitleModel, QuestionnaireTitleDto>();
        }
    }
}
