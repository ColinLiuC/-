using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
   public interface IStreetService : IEfCoreRepository<Street, StreetDto>, IServiceSupport
    {
        /// <summary>
        /// 通过Guid拿到街道名称
        /// </summary>
        /// <param name="streetid"></param>
        /// <returns></returns>
        string GetStreetName(Guid streetid);
    }
}
