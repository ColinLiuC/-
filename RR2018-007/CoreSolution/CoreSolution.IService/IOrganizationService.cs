using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
   public interface IOrganizationService : IEfCoreRepository<Organization, OrganizationDto>, IServiceSupport
    {

        List<Organization> GetOrganizations(Guid? street, Guid? station);
        List<TreeModel> GetTreeList(Guid? street, Guid? station);
    }
}
