﻿using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Service
{
   public class QuestionnaireTitleService : EfCoreRepositoryBase<QuestionnaireTitle, QuestionnaireTitleDto, Guid>,IQuestionnaireTitleService
    {
    }
}