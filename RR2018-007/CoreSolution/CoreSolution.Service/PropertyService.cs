using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreSolution.Service
{
   public class PropertyService : EfCoreRepositoryBase<Property, PropertyDto, Guid>, 
        IPropertyService
    {
        public string GetPropertyName(Guid id)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var info = db.Property.FirstOrDefault(p => p.Id == id);
                if (info != null)
                {
                    return info.Name;
                }
                return "";
            }
        }



    }
}
