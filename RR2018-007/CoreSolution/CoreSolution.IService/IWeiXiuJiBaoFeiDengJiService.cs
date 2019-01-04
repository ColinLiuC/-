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
    public interface IWeiXiuJiBaoFeiDengJiService : IEfCoreRepository<WeiXiuJiBaoFeiDengJi, WeiXiuJiBaoFeiDengJiDto>, IServiceSupport
    {
        Task<WeiXiuJiBaoFeiDengJiDto> GetWeiXiuJiBaoFeiDengJiById(Guid id);
        Task<WeiXiuJiBaoFeiDengJiDto> GetWeiXiuJiBaoFeiDengJiByFixedAssetsId(Guid id);
        Task<List<WeiXiuJiBaoFeiDengJiDto>> GetWeiXiuJiBaoFeiDengJiPaged(Guid id);
    }
}
