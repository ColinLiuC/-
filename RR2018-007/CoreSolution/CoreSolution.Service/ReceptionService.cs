using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.Service
{
    public class ReceptionServiceS : EfCoreRepositoryBase<ReceptionService, ReceptionServiceDto, Guid>, IReceptionService
    {
        
    }
}
