using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CoreSolution.Service
{
   public class OrganizationProjectService : EfCoreRepositoryBase<OrganizationProject, OrganizationProjectDto, Guid>,IOrganizationProjectService
    {

        public List<OrganizationProject> GetOrganizationProjects(Guid? street, Guid? station)
        {
            Expression<Func<OrganizationProject, bool>> where = PredicateExtensions.True<OrganizationProject>();
            if (street != null)
            {
                where = where.And(p => p.Street == street);
            }
            if (station != null)
            {
                where = where.And(p => p.Station == station);
            }

            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var list = db.OrganizationProject.Where(where).ToList();
                return list;
            }
        }
    }
}
