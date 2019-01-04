using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CoreSolution.Service
{
   public class ActivityRegisterService : EfCoreRepositoryBase<ActivityRegister, ActivityRegisterDto, Guid>, IActivityRegisterService
    {
        public DataSet GetDataByActivityId(Guid activityId)
        {
            string sql = "select EnrolmentName,ContactNumber,RegistDate from T_ActivityRegister where ActivityId='"+activityId+"'";
            var cfg = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "configuration.json", ReloadOnChange = true }).Build();
            var connStr = cfg.GetSection("connStr");
            return DBHelper.ExecuteQuery(sql,connStr.Value); 

        }
    }
}
