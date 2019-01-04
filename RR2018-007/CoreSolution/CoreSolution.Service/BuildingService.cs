using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
using CoreSolution.EntityFrameworkCore;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreSolution.Service
{
    public class BuildingService : EfCoreRepositoryBase<Building, BuildingDto, Guid>, IBuildingService
    {
        public MyBuilding GetBuildingInfo(Guid buildingid)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var result = from b in db.Building.Where(p => p.Id == buildingid)
                             join q in db.Quarters
                             on b.QuartersId equals q.Id
                             select new MyBuilding()
                             {
                                 building = b,
                                 quarters = q
                             };
                return result.FirstOrDefault();
            }
        }
    }
}
