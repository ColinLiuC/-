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
    public sealed class ResidentWorkService : EfCoreRepositoryBase<ResidentWork, ResidentWorkDto, Guid>, IResidentWorkService
    {

        /// <summary>
        /// App端获取事项
        /// </summary>
        /// <param name="streetid">街道Id</param>
        /// <param name="stationid">驿站Id</param>
        /// <param name="residentWorktype">事项类别</param>
        /// <param name="residentWorkname">事项名称</param>
        /// <param name="total">总数</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns></returns>
        public List<MyResidentWork> GetResidentWorkApp(Guid streetid, Guid? stationid, int? residentWorktype, string residentWorkname, out int total, int pageIndex, int pageSize)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var list = db.ResidentWork_Attach.Join(db.ResidentWork, p => p.ResidentWorkId, q => q.Id, (p, q) => new MyResidentWork()
                {
                    residentWork = q,
                    residentWork_Attach = p
                }).Where(
                    i => i.residentWork_Attach.StreetId == streetid &&
                      (stationid == null || i.residentWork_Attach.StationId == stationid) &&
                      (residentWorktype == null || i.residentWork.ResidentWorkType == residentWorktype) &&
                      (string.IsNullOrEmpty(residentWorkname) || i.residentWork.ResidentWorkName.Contains(residentWorkname)) &&
                      i.residentWork.IsDeleted == false && i.residentWork.IsGuiDang == false &&
                      i.residentWork.IsPublish == true).ToList();

                //排除重复数据
                var result = list.Distinct(new RessdentWorkHelper()).ToList();
                total = result.Count();
                return result.OrderByDescending(p => p.residentWork.CreationTime)
                       .Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }

        public ResidentWork GetResidentWork(Guid id)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var info = db.ResidentWork.FirstOrDefault(p=>p.Id==id);
                if (info!=null)
                {
                    return info;
                }
                return null;
            }
        }

    }
}
