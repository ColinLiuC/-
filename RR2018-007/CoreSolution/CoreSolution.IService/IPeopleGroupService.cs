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
    public interface IPeopleGroupService : IEfCoreRepository<PeopleGroup, PeopleGroupDto>, IServiceSupport
    {
        Task<Tuple<int, List<PeopleGroupDto>>> GetPeopleGroupPagedAsync(PeopleGroupDto dto, int pageIndex, int pageSize);
        Task<PeopleGroupDto> GetPeopleGroupById(Guid id);


    }
}
