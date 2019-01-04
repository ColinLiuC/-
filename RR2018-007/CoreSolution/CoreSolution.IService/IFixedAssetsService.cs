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
    public interface IFixedAssetsService : IEfCoreRepository<FixedAssets, FixedAssetsDto>, IServiceSupport
    {
        Task<Tuple<int, List<FixedAssetsDto>>> GetFixedAssetsPagedAsync(FixedAssetsDto dto, int pageIndex, int pageSize);
        Task<FixedAssetsDto> GetFixedAssetsById(Guid id);
    }
}
