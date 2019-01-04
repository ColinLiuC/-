using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.IService
{
   
    public interface IExpenditureCountService : IEfCoreRepository<Expenditure, ExpenditureDto>, IServiceSupport
    {
        Task<List<StatisticstDto<ExpenditureCateGory>>> GetCategoriesCount(ExpenditureDto dto);

        Task<List<StatisticstDto<Guid>>> GetStationCount(ExpenditureDto dto);

        Task<List<StatisticstDto<int>>> GetUseDateCount(ExpenditureDto dto);
        
    }
}
