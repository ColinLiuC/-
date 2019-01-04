using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.IService
{
    public interface IExpenditureService : IEfCoreRepository<Expenditure, ExpenditureDto>, IServiceSupport
    {
        Task<(int, List<ExpenditureDto>)> GetExpenditurePagedAsync(ExpenditureDto dto, int pageIndex, int pageSize);
        Task<ExpenditureDto> GetExpenditureById(Guid id);
    }
}
