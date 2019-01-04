using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace CoreSolution.IService
{
    public interface IWorkDisposeService : IEfCoreRepository<WorkDispose, WorkDisposeDto>, IServiceSupport
    {
        List<MyWorkDispose> GetWorkDisposes(string shiminyunid,Guid? streetid, Guid? stationid, string username, string idcard, string residentWorkName,string statuscode, int? residentWorkType, DateTime? creationTime_start, DateTime? creationTime_end, out int total, int pageIndex = 1, int pageSize = 10);
        MyWorkDispose GetWorkDisposeById(Guid workDisposeId);

        int GetWorkDisposeCount(Expression<Func<WorkDispose, bool>> where);
    }
}
