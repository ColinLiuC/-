using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
   public interface IJuWeiService : IEfCoreRepository<JuWei, JuWeiDto>, IServiceSupport
    {
        /// <summary>
        /// 通过Guid拿到居委名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string GetJuWeiName(Guid Id);
    }
}
