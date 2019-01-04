using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;


namespace CoreSolution.IService
{
    public interface IStationService : IEfCoreRepository<Station, StationDto>, IServiceSupport
    {
        Task<IList<StationDto>> GetStationPagedAsync(StationDto stationDto, int pageIndex, int pageSize);
        /// <summary>
        /// 通过Guid拿到驿站名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string GetStationName(Guid Id);
    }
}
