using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.Service
{

    public sealed class ResourcePlaceCountService : EfCoreRepositoryBase<ResourcePlace, ResourcePlaceDto, Guid>, IResourcePlaceCountService
    {
        public async Task<List<StatisticstDto<Guid>>> GetBigCategoriesCount(ResourcePlaceDto dto)
        {
            Expression<Func<ResourcePlace, bool>> where = PredicateExtensions.True<ResourcePlace>();
            if (dto.Street != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.Street.ToString())))
            {
                where = where.And(i => i.Street == dto.Street);
            }
            if (dto.Station != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.Station.ToString())))
            {
                where = where.And(i => i.Station== dto.Station);
            }

            var table1 = _dbContext.Set<ResourcePlace>().Where(where);
            var query =
                await(from item1 in table1
                      join item2 in _dbContext.Set<Street>() on item1.Street equals item2.Id 
                      join item4 in _dbContext.Set<Station>() on item1.Station equals item4.Id 
                      join item6 in _dbContext.Set<DataDictionary>() on item1.ResourceCategory equals item6.Id 
                      group new { item1, item6 } by new { item1.ResourceCategory } into grouped
                      select new StatisticstDto<Guid>
                      {
                          Key = grouped.Key.ResourceCategory,
                          KeyDescription=grouped.Select(i=>i.item6.Name).FirstOrDefault(),
                          Count = grouped.Count(),
                      }).ToListAsync();

            return query;
        }


        public async Task<List<StatisticstDto<Guid>>> GetJuweiCount(ResourcePlaceDto dto)
        {
            Expression<Func<ResourcePlace, bool>> where = PredicateExtensions.True<ResourcePlace>();
            if (dto.Street != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.Street.ToString())))
            {
                where = where.And(i => i.Street == dto.Street);
            }
            if (dto.Station != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.Station.ToString())))
            {
                where = where.And(i => i.Station == dto.Station);
            }
            var table1 = _dbContext.Set<ResourcePlace>().Where(where);
            var query =
                await(from item1 in table1
                      join item2 in _dbContext.Set<Street>() on item1.Street equals item2.Id 
                      join item4 in _dbContext.Set<Station>() on item1.Station equals item4.Id 
                      join item6 in _dbContext.Set<JuWei>() on item1.Street equals item6.StreetId 
                      group new { item1, item6 } by new { item6.Id } into grouped
                      where grouped.Count() > 0
                      select new StatisticstDto<Guid>
                      {
                          Key = grouped.Key.Id,
                          KeyDescription = grouped.Select(i => i.item6.Name).FirstOrDefault(),
                          Count = grouped.Count(),
                      }).ToListAsync();

            return query;
        }


        public async Task<List<StatisticstDto<Guid>>> GetSubCategoriesCount(ResourcePlaceDto dto)
        {
            Expression<Func<ResourcePlace, bool>> where = PredicateExtensions.True<ResourcePlace>();
            if (dto.Street != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.Street.ToString())))
            {
                where = where.And(i => i.Street == dto.Street);
            }
            if (dto.Station != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.Station.ToString())))
            {
                where = where.And(i => i.Station == dto.Station);
            }
            if (dto.ResourceCategory != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.ResourceCategory.ToString())))
            {
                where = where.And(i => i.ResourceCategory == dto.ResourceCategory);
            }

            var table1 = _dbContext.Set<ResourcePlace>().Where(where);
            var query =
                await (from item1 in table1
                       join item2 in _dbContext.Set<Street>() on item1.Street equals item2.Id
                       join item4 in _dbContext.Set<Station>() on item1.Station equals item4.Id
                       join item6 in _dbContext.Set<DataDictionary>() on item1.ResourceType equals item6.Id
                       group new { item1, item6 } by new { item1.ResourceType } into grouped
                       select new StatisticstDto<Guid>
                       {
                           Key = grouped.Key.ResourceType,
                           KeyDescription = grouped.Select(i => i.item6.Name).FirstOrDefault(),
                           Count = grouped.Count(),
                           Property1= grouped.Select(i => i.item6.CustomAttributes).FirstOrDefault()
                       }).ToListAsync();

            return query;
        }

        public async Task<List<ResourcePlaceDto>> GetAllPoints(Guid[] ResourceTypes,Guid StreetId)
        {
            
            if (ResourceTypes.Length<=0)
            {

                return null;  
            }
            Expression<Func<ResourcePlace, bool>> where = PredicateExtensions.True<ResourcePlace>();
            if (StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(StreetId.ToString())))
            {
                where = where.And(i => i.Street == StreetId);
            }
            where = where.And(i=>ResourceTypes.Contains(i.ResourceType));
            return await _dbContext.Set<ResourcePlace>().Where(where).Select(r=>new ResourcePlaceDto {
                Id=r.Id,
                Xaxis=r.Xaxis,
                Yaxis=r.Yaxis,
                Name=r.Name,
                Address=r.Address,
                ResourceType=r.ResourceType,
                Phone=r.Phone

            }).Distinct().ToListAsync();

        }


    }
}
