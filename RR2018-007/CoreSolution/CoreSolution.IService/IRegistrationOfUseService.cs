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
    public interface IRegistrationOfUseService : IEfCoreRepository<RegistrationOfUse, RegistrationOfUseDto>, IServiceSupport
    {
        Task<List<RegistrationOfUseDto>> GetRegistrationOfUsePaged(Guid id);
        Task<RegistrationOfUseDto> GetRegistrationOfUseById(Guid id);

        Task<RegistrationOfUseDto> GetRegistrationOfUseByFixedAssetsId(Guid id);
    }
}
