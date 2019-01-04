using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoreSolution.Service
{
    public class ResidentWork_AttachService : EfCoreRepositoryBase<ResidentWork_Attach, ResidentWork_AttachDto, Guid>, IResidentWork_AttachService
    {
        public bool DeleteByResidentWorkId(Guid residentWorkId, Guid? streetid = null)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                try
                {
                    Expression<Func<ResidentWork_Attach, bool>> where = p =>
                   p.ResidentWorkId == residentWorkId &&
                  (streetid == null || p.StreetId == Guid.Parse(streetid.ToString()));
                    var list = db.ResidentWork_Attach.Where(where).ToList();
                    db.ResidentWork_Attach.RemoveRange(list);
                    db.SaveChanges();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }

            }
        }


        public List<Guid> GetResidentWorkByStreet(Guid residentWorkId, Guid? streetid)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var result = new List<Guid>();
                try
                {
                    Expression<Func<ResidentWork_Attach, bool>> where = p =>
                   p.ResidentWorkId == residentWorkId &&
                  (streetid == null || p.StreetId == Guid.Parse(streetid.ToString()));
                    var list = db.ResidentWork_Attach.Where(where).ToList();
                    foreach (var item in list)
                    {
                        result.Add(item.StationId);
                    }
                    return result;
                }
                catch (Exception)
                {
                    return result;
                }

            }
        }


        public List<MyStationDto> GetMyStation(Guid residentWorkId, Guid streetid)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var result = new List<MyStationDto>();
                var list = db.Set<ResidentWork_Attach>().Where(p => p.ResidentWorkId == residentWorkId && p.StreetId == streetid).ToList();
                var stationService = new StationService();
                foreach (var item in list)
                {
                    var name = stationService.GetStationName(item.StationId);
                    if (result.FirstOrDefault(p => p.StationName == name) != null)
                    {
                        continue;
                    }
                    else
                    {
                        result.Add(new MyStationDto() { StationId = item.Id, StationName = name });
                    }
                }
                return result;
            }
        }



    }
}
