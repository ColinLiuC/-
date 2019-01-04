using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
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
   public class OrganizationService: EfCoreRepositoryBase<Organization, OrganizationDto, Guid>, IOrganizationService
    {

        public List<Organization> GetOrganizations(Guid? street, Guid? station)
        {
            Expression<Func<Organization, bool>> where = PredicateExtensions.True<Organization>();
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
                var list = db.Organization.Where(where).ToList();
                return list;
            }
        }

        public List<TreeModel> GetTreeList(Guid? street, Guid? station)
        {
            OrganizationProjectService organizationProjectService = new OrganizationProjectService();
            var list_Organization = GetOrganizations(street, station);
            var list_OrganizationProjects = organizationProjectService.GetOrganizationProjects(street, station);
            StringBuilder sb = new StringBuilder();
            List<TreeModel> treeModels = new List<TreeModel>();
            foreach (var item in list_Organization)
            {
                TreeModel model = new TreeModel() {
                    id = item.Id,
                    name = item.OrganizationName,
                    pId = null,
                    type= "Organization"
                };
                treeModels.Add(model);
            }

            foreach (var item in list_OrganizationProjects)
            {
                TreeModel model = new TreeModel()
                {
                    id = item.Id,
                    name = item.ProjectName,
                    pId = item.OrganizationId,
                    type = "OrganizationProject"
                };
                treeModels.Add(model);
            }
            return treeModels;
        }

    }
}
