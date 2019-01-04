using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreSolution.Service
{
    public class StreetService : EfCoreRepositoryBase<Street, StreetDto, Guid>, IStreetService
    {

        public string GetStreetName(Guid streetid)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var info = db.Street.FirstOrDefault(p => p.Id == streetid);
                if (info != null)
                {
                    return info.StreetName;
                }
                return "";
            }
        }
    }
}
