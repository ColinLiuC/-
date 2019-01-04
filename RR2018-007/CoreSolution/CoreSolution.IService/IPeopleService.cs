using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.IService
{
    public interface IPeopleService : IEfCoreRepository<User, UserDto>, IServiceSupport
    {
        List<ActivityAndEvaluationDto> TestAsync(string username, bool IsComment);
        List<ActivityRegisterAndEvaluationDto> GetActivity(string name, bool IsComment);
        List<ServiceApplicationAndEvaluationDto> GetService2();

        Task<object> GetWork(Guid Id,string StatusCode);

        List<ReceptionServiceAndEvaluationDto> GetService(string username, int? isComment, int? PJ_RegistStatus);
    }
}
