using CoreSolution.Domain.Entities;
using CoreSolution.Domain.Enum;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Service
{

    public sealed class ExpenditureCountService : EfCoreRepositoryBase<Expenditure, ExpenditureDto, Guid>, IExpenditureCountService
    {
        public async System.Threading.Tasks.Task<List<StatisticstDto<ExpenditureCateGory>>> GetCategoriesCount(ExpenditureDto dto)
        {
            Expression<Func<Expenditure, bool>> where = PredicateExtensions.True<Expenditure>();
            if ( dto.StreetId != (default(Guid))&&(!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if ( dto.StationId != (default(Guid))&&(!string.IsNullOrWhiteSpace(dto.StationId.ToString())))
            {
                where = where.And(i => i.StationId == dto.StationId);
            }
            if (dto.StartUseDate != null && dto.StartUseDate.Value != default(DateTime))
            {
                where = where.And(i => i.UseDate >= dto.StartUseDate);
            }
            if (dto.EndUseDate != null && dto.EndUseDate.Value != default(DateTime))
            {
                where = where.And(i => i.UseDate <= dto.EndUseDate);
            }

            var table1 = _dbContext.Set<Expenditure>().Where(where);
            var  query =
                await (from item1 in table1
                       join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                from item3 in Stre.DefaultIfEmpty()
                join item4 in _dbContext.Set<Station>() on item1.StationId equals item4.Id into statin
                from item5 in statin.DefaultIfEmpty()
                group new { item1 } by new { item1.Category } into grouped
                orderby grouped.Sum(i => i.item1.UseMoney) descending
                select new StatisticstDto<ExpenditureCateGory>
                {
                    Key = grouped.Key.Category,
                    Count = grouped.Count(),
                    Sum = grouped.Sum(i => i.item1.UseMoney),
                }).ToListAsync();

             query.ForEach(i => i.KeyDescription = i.Key.GetDescription());
            return query;
        }

        public async Task<List<StatisticstDto<Guid>>> GetStationCount(ExpenditureDto dto)
        {
            Expression<Func<Expenditure, bool>> where = PredicateExtensions.True<Expenditure>();
            if (dto.StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.StationId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StationId.ToString())))
            {
                where = where.And(i => i.StationId == dto.StationId);
            }
            if (dto.StartUseDate != null && dto.StartUseDate.Value != default(DateTime))
            {
                where = where.And(i => i.UseDate >= dto.StartUseDate);
            }
            if (dto.EndUseDate != null && dto.EndUseDate.Value != default(DateTime))
            {
                where = where.And(i => i.UseDate <= dto.EndUseDate);
            }
            var table1 = _dbContext.Set<Expenditure>().Where(where);
            var query =
                await(from item1 in table1
                      join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                      from item3 in Stre.DefaultIfEmpty()
                      join item4 in _dbContext.Set<Station>() on item1.StationId equals item4.Id into statin
                      from item5 in statin.DefaultIfEmpty()
                      group new { item1,item5 } by new { item5.Id } into grouped
                      where grouped.Sum(i => i.item1.UseMoney)>0
                      orderby grouped.Sum(i => i.item1.UseMoney) descending
                      select new StatisticstDto<Guid>
                      {
                          Key = grouped.Key.Id,
                          KeyDescription= grouped.Select(i => i.item5.StationName).FirstOrDefault(),
                          Count = grouped.Count(),
                          Sum = grouped.Sum(i => i.item1.UseMoney),
                      }).ToListAsync();

            return query;
        }

        public async Task<List<StatisticstDto<int>>> GetUseDateCount(ExpenditureDto dto)
        {
            Expression<Func<Expenditure, bool>> where = PredicateExtensions.True<Expenditure>();
            if (dto.StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.StationId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StationId.ToString())))
            {
                where = where.And(i => i.StationId == dto.StationId);
            }
            where = where.And(i => i.UseDate!=null&& i.UseDate.Value.Year == DateTime.Now.Year);//当前年 如 2018

            var table1 = _dbContext.Set<Expenditure>().Where(where);
            var query =
                await (from item1 in table1
                       join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                       from item3 in Stre.DefaultIfEmpty()
                       join item4 in _dbContext.Set<Station>() on item1.StationId equals item4.Id into statin
                       from item5 in statin.DefaultIfEmpty()
                       group new { item1, item5 } by new { item1.UseDate.Value.Month } into grouped
                       where grouped.Sum(i => i.item1.UseMoney) > 0
                       orderby grouped.Key.Month 
                       select new StatisticstDto<int>
                       {
                           Key = grouped.Key.Month,
                           Count = grouped.Count(),
                           Sum = grouped.Sum(i => i.item1.UseMoney),
                       }).ToListAsync();

            return query;
        }
    }
}
