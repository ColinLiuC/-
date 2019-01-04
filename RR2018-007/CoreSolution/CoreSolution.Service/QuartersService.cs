using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CoreSolution.Service
{
    public class QuartersService : EfCoreRepositoryBase<Quarters, QuartersDto, Guid>, IQuartersService
    {
        public string GetQuartersName(Guid id)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var info = db.Quarters.FirstOrDefault(p => p.Id == id);
                if (info != null)
                {
                    return info.Name;
                }
                return "";
            }
        }

        public List<Quarters> GetQuarters()
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                return db.Quarters.ToList();
            }
        }

        public DataSet GetAllQuarters()
        {
            string sql = "SELECT Name,Address,CompletedYear from T_Quarters";

            var cfg = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "configuration.json", ReloadOnChange = true }).Build();
            var connStr = cfg.GetSection("connStr");

            return DBHelper.ExecuteQuery(sql, connStr.Value); ;

        }


    }
}
