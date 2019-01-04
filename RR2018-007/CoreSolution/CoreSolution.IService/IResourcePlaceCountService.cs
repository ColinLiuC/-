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


    public interface IResourcePlaceCountService : IEfCoreRepository<ResourcePlace, ResourcePlaceDto>, IServiceSupport
    {
        Task<List<StatisticstDto<Guid>>> GetBigCategoriesCount(ResourcePlaceDto dto);

        Task<List<StatisticstDto<Guid>>> GetJuweiCount(ResourcePlaceDto dto);

        Task<List<StatisticstDto<Guid>>> GetSubCategoriesCount(ResourcePlaceDto dto);
        Task<List<ResourcePlaceDto>> GetAllPoints(Guid[] ResourceTypes, Guid StreetId);
    }
}
