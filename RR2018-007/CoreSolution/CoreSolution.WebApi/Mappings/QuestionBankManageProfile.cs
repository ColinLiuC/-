using AutoMapper;
using CoreSolution.AutoMapper.MapProfile;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.WebApi.Models.QuestionBankManage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Mappings
{
    public class QuestionBankManageProfile : Profile, IProfile
    {
        public QuestionBankManageProfile()
        {
            CreateMap<QuestionBankManageDto, OutputQuestionBankManageModel>();
            CreateMap<OutputQuestionBankManageModel, QuestionBankManageDto>();

            CreateMap<QuestionBankManageDto, InputQuestionBankManageModel>();
            CreateMap<InputQuestionBankManageModel, QuestionBankManageDto>();

            CreateMap<QuestionBankManageDto, QuestionBankManage>();
            CreateMap<QuestionBankManage, QuestionBankManageDto>();
        }
    }
}
