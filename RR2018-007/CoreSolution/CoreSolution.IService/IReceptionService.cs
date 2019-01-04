using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.IService
{
   public interface IReceptionService: IEfCoreRepository<ReceptionService, ReceptionServiceDto>, IServiceSupport
    {
       
    }
}
