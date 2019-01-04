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
using System.Threading.Tasks;

namespace CoreSolution.Service
{
    public class HouseService : EfCoreRepositoryBase<House, HouseDto, Guid>, IHouseService
    {
        public MyHouse GetHouseInfo(Guid houseid)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                try
                {
                    var result = from h in db.House.Where(p => p.Id == houseid)
                                 join d in db.DoorCard
                                 on h.DoorCardId equals d.Id
                                 join b in db.Building
                                 on d.BuildingId equals b.Id
                                 select new MyHouse()
                                 {
                                     building = b,
                                     house = h,
                                     doorCard = d
                                 };
                    return result.FirstOrDefault();
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 拼接房屋地址
        /// </summary>
        /// <param name="houseid">房屋室号</param>
        /// <returns></returns>
        public string GetHouseAddress(Guid houseid)
        {
            try
            {
                MyHouse myHouse = GetHouseInfo(houseid);
                if (myHouse != null)
                {
                    return myHouse.building.Address + myHouse.doorCard.DoorCardNumber + "号" + myHouse.house.HouseNumber + "室";
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public void DeleteByDoorCard(Guid doorcardId)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var list = db.House.Where(p => p.DoorCardId == doorcardId).ToList();
                if (list != null)
                {
                    db.House.RemoveRange(list);
                    db.SaveChanges();
                }
            }
        }

    }
}
