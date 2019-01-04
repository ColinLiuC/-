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
    public interface IReceptionServiceCountService : IEfCoreRepository<ReceptionService, ReceptionServiceCountDto>, IServiceSupport
    {
        Task<List<StatisticstDto<ReceptionServiceCatagory>>> GetCategoriesCount(ReceptionServiceCountDto dto);
        Task<List<StatisticstDto<int>>> GetSourceCount(ReceptionServiceCountDto dto);
        Task<List<StatisticstDto<Guid>>> GetStationCount(ReceptionServiceCountDto dto);
        Task<List<StatisticstDto<Guid?>>> GetSatisfactionCount(ReceptionServiceCountDto dto);

        Task<List<StatisticstDto<int>>> GetHoursCount(ReceptionServiceCountDto dto);
        Task<List<dynamic>> GetMonthsCount(ReceptionServiceCountDto dto);
        Task<List<StatisticstDto<Guid>>> GetServiceProvider(ReceptionServiceCountDto dto);
    }
}
