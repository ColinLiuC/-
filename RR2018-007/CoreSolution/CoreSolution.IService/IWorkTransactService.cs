using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.IService
{
   public interface IWorkTransactService : IEfCoreRepository<WorkTransact, WorkTransactDto>, IServiceSupport
    {
        List<MyWorkTransact> GetWorkTransacts(Guid? streetid, Guid? stationid, string username, string idcard, string residentWorkName, int? residentWorkType, string statusCode, out int total, int pageIndex = 1, int pageSize = 10);

        MyWorkTransact GetMyWorkTransactById(Guid workDisposeId);

        int GetWorkTransactCount(Expression<Func<MyWorkTransact, bool>> where);

        ResidentWork GetWorkTransactMax();
    }
}
