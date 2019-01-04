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
    public class JuWeiService : EfCoreRepositoryBase<JuWei, JuWeiDto, Guid>, IJuWeiService
    {

        public string GetJuWeiName(Guid id)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var info = db.JuWei.FirstOrDefault(p => p.Id == id);
                if (info != null)
                {
                    return info.Name;
                }
                return "";
            }
        }


        public JuWei GetJuWeiById(Guid id)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var info = db.JuWei.FirstOrDefault(p => p.Id == id);
                if (info != null)
                {
                    return info;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取街道下的所有居委
        /// </summary>
        /// <param name="streetid"></param>
        /// <returns></returns>
        public List<Guid> GetJuWeisByStreet(Guid streetid)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                List<Guid> js = new List<Guid>();
                var list = db.JuWei.Where(p => p.StreetId == streetid).ToList();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        js.Add(item.Id);
                    }
                }
                return js;
            }
        }



        /// <summary>
        /// 获取驿站下的所的居委
        /// </summary>
        /// <param name="poststationid"></param>
        /// <returns></returns>
        public List<Guid> GetJuWeisByStation(Guid poststationid)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                List<Guid> js = new List<Guid>();
                var list = db.JuWei.Where(p => p.PostStationId == poststationid).ToList();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        js.Add(item.Id);
                    }
                }
                return js;
            }
        }

        public List<Guid> GetJuWeiAll()
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                List<Guid> js = new List<Guid>();
                var list = db.JuWei.ToList();
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        js.Add(item.Id);
                    }
                }
                return js;
            }
        }

    }
}
