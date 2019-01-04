using CoreSolution.Domain;
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
    public class DoorCardService : EfCoreRepositoryBase<DoorCard, DoorCardDto, Guid>, IDoorCardService
    {

        /// <summary>
        /// 获取门牌详情
        /// </summary>
        /// <param name="doorCardId"></param>
        /// <returns></returns>
        public MyDoorCard GetDoorCardInfo(Guid doorCardId)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var result = from d in db.DoorCard.Where(p => p.Id == doorCardId)
                             join b in db.Building
                             on d.BuildingId equals b.Id
                             join q in db.Quarters
                             on b.QuartersId equals q.Id
                             select new MyDoorCard()
                             {
                                 doorcard = d,
                                 building = b,
                                 quarters = q
                             };
                return result.FirstOrDefault();
            }
        }
    }
}

