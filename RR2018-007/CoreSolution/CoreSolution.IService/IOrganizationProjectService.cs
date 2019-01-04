using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
   public interface IOrganizationProjectService:IEfCoreRepository<OrganizationProject, OrganizationProjectDto>, IServiceSupport
    {
        List<OrganizationProject> GetOrganizationProjects(Guid? street, Guid? station);
    }
}
