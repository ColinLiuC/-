using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
using CoreSolution.EntityFrameworkCore;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.Service
{
    public sealed class WorkDisposeService : EfCoreRepositoryBase<WorkDispose, WorkDisposeDto, Guid>, IWorkDisposeService
    {

        public List<MyWorkDispose> GetWorkDisposes(string shiminyunid,Guid? streetid,Guid? stationid,string username, string idcard, string residentWorkName, string statuscode, int? residentWorkType, DateTime? CreationTime_start, DateTime? CreationTime_end, out int total, int pageIndex = 1, int pageSize = 10)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                CreationTime_start = CreationTime_start ?? DateTime.MinValue;
                CreationTime_end = CreationTime_end != null ? CreationTime_end.Value.AddDays(1) : DateTime.MaxValue;
                //拼接过滤条件
                Expression<Func<MyWorkDispose, bool>> where =
                    p =>
                     (string.IsNullOrEmpty(shiminyunid) || p.workDispose.ShiMinYunId==shiminyunid) &&
                     (string.IsNullOrEmpty(username) || p.workDispose.UserName.Contains(username)) &&
                     (string.IsNullOrEmpty(idcard) || p.workDispose.IdCard == idcard) &&
                     (string.IsNullOrEmpty(statuscode) || p.workDispose.StatusCode == statuscode) &&
                     (string.IsNullOrEmpty(residentWorkName) || p.workDispose.ResidentWorkName.Contains(residentWorkName)) &&
                     (residentWorkType == 0 || p.residentWorkType == residentWorkType) &&
                     (streetid == null || p.workDispose.StreetId == streetid) &&
                     (stationid == null || p.workDispose.PostStationId == stationid) &&

                     (p.workDispose.CreationTime >= CreationTime_start && p.workDispose.CreationTime <= CreationTime_end);

                var query = db.WorkDispose.Join(db.ResidentWork, p => p.ResidentWorkId, q => q.Id, (p, q) => new MyWorkDispose()
                {
                    workDispose = p,
                    residentWorkType = q.ResidentWorkType
                }).Where(where);

                total = query.Count();
                var result = query.OrderByDescending(p => p.workDispose.CreationTime)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return result;
            }
        }


        public MyWorkDispose GetWorkDisposeById(Guid workDisposeId)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                return db.WorkDispose.Join(db.ResidentWork, p => p.ResidentWorkId, q => q.Id, (p, q) => new MyWorkDispose()
                {
                    workDispose = p,
                    residentWorkType = q.ResidentWorkType
                }).FirstOrDefault(p => p.workDispose.Id == workDisposeId);
            }
        }


        public int GetWorkDisposeCount(Expression<Func<WorkDispose, bool>> where)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {              
                return db.WorkDispose.Count(where);
            }
        }

    }
}
