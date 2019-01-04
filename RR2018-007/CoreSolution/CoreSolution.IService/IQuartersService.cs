using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CoreSolution.IService
{
   public interface IQuartersService : IEfCoreRepository<Quarters, QuartersDto>, IServiceSupport
    {
        string GetQuartersName(Guid id);
        List<Quarters> GetQuarters();

        DataSet GetAllQuarters();
    }
}
