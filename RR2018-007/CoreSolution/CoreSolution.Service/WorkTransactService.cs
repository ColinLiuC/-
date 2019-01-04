using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
using CoreSolution.EntityFrameworkCore;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.Service
{
    public class WorkTransactService : EfCoreRepositoryBase<WorkTransact, WorkTransactDto, Guid>, IWorkTransactService
    {
        public List<MyWorkTransact> GetWorkTransacts(Guid? streetid, Guid? stationid, string username, string idcard, string residentWorkName, int? residentWorkType, string statusCode, out int total, int pageIndex = 1, int pageSize = 10)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                //拼接过滤条件
                Expression<Func<MyWorkTransact, bool>> where =
                    p =>
                     (string.IsNullOrEmpty(username) || p.workTransact.UserName.Contains(username)) &&
                     (string.IsNullOrEmpty(idcard) || p.workTransact.IdCard == idcard) &&
                     (string.IsNullOrEmpty(residentWorkName) || p.workTransact.ResidentWorkName.Contains(residentWorkName)) &&
                     (string.IsNullOrEmpty(statusCode) || p.workTransact.StatusCode == statusCode) &&
                     (streetid == null || p.workTransact.StreetId == streetid) &&
                     (stationid == null || p.workTransact.StationId == stationid) &&
                   (residentWorkType == 0 || p.residentWorkType == residentWorkType);

                var query = db.WorkTransact.Join(db.ResidentWork, p => p.ResidentWorkId, q => q.Id, (p, q) => new MyWorkTransact()
                {
                    workTransact = p,
                    residentWorkType = q.ResidentWorkType,
                    Deadline = q.Deadline
                }).Where(where);

                total = query.Count();
                var result = query.OrderByDescending(p => p.workTransact.CreationTime)
                    .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                return result;
            }
        }

        public MyWorkTransact GetMyWorkTransactById(Guid workDisposeId)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                return db.WorkTransact.Join(db.ResidentWork, p => p.ResidentWorkId, q => q.Id, (p, q) => new MyWorkTransact()
                {
                    workTransact = p,
                    residentWorkType = q.ResidentWorkType
                }).FirstOrDefault(p => p.workTransact.Id == workDisposeId);
            }
        }


        public int GetWorkTransactCount(Expression<Func<MyWorkTransact, bool>> where)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                return db.WorkTransact.Join(db.ResidentWork, p => p.ResidentWorkId, q => q.Id, (p, q) => new MyWorkTransact()
                {
                    workTransact = p,
                    residentWork = q
                }).Count(where);
            }
        }



        /// <summary>
        /// 今日受理最多的事项
        /// </summary>
        /// <returns></returns>
        public ResidentWork GetWorkTransactMax()
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                DateTime nowtime = DateTime.Now.Date;
                var info = db.WorkTransact.Where(p => p.ShouliTime >= nowtime).GroupBy(p => p.ResidentWorkId).Select(group => new
                {
                    residentWorkId = group.Key,
                    count = group.Count()
                }).OrderByDescending(p => p.count).FirstOrDefault();
                if (info != null)
                {
                    Guid residentWorkId = info.residentWorkId;
                    ResidentWorkService residentWorkService = new ResidentWorkService();
                    return residentWorkService.GetResidentWork(residentWorkId);
                }
                return null;
            }
        }

    }
}
