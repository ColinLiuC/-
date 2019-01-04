using CoreSolution.Domain.Entities;
using CoreSolution.Domain.Enum;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Service
{
    public sealed class ReceptionServiceCountService : EfCoreRepositoryBase<ReceptionService, ReceptionServiceCountDto, Guid>, IReceptionServiceCountService
    {
        public async Task<List<StatisticstDto<ReceptionServiceCatagory>>> GetCategoriesCount(ReceptionServiceCountDto dto)
        {
            Expression<Func<ReceptionService, bool>> where = PredicateExtensions.True<ReceptionService>();
            if (dto.StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.PostStationId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.PostStationId.ToString())))
            {
                where = where.And(i => i.PostStationId == dto.PostStationId);
            }
            where = where.And(i => i.StreetId != null);
            where = where.And(i => i.PostStationId != null);

            var receptionService = _dbContext.Set<ReceptionService>().Where(where);

            Expression<Func<ServiceApplication, bool>> predicate = PredicateExtensions.True<ServiceApplication>();
            predicate.And(i => i.RegisterDate != null);
            if (dto.StartRegisterDate != null && dto.StartRegisterDate.Value != default(DateTime))
            {
                predicate=predicate.And(i => i.RegisterDate >= dto.StartRegisterDate);
            }
            if (dto.EndRegisterDate != null && dto.EndRegisterDate.Value != default(DateTime))
            {
                predicate=predicate.And(i => i.RegisterDate <= dto.EndRegisterDate);
            }
            var serviceApplication = _dbContext.Set<ServiceApplication>().Where(predicate);

            var query =
                await (from item1 in receptionService
                       join app in serviceApplication on item1.Id equals app.ServiceId
                       join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                       from item3 in Stre.DefaultIfEmpty()
                       join item4 in _dbContext.Set<Station>() on item1.PostStationId equals item4.Id into statin
                       from item5 in statin.DefaultIfEmpty()
                       group new { item1 } by new { item1.Category } into grouped
                       orderby grouped.Count() descending
                       select new StatisticstDto<ReceptionServiceCatagory>
                       {
                           Key = (ReceptionServiceCatagory)grouped.Key.Category,
                           Count = grouped.Count(),
                       }).ToListAsync();
            query.ForEach(i => i.KeyDescription = i.Key.GetDescription());

            return query;
        }


        public async Task<List<StatisticstDto<Guid>>> GetServiceProvider(ReceptionServiceCountDto dto)
        {

            Expression<Func<ReceptionService, bool>> where = PredicateExtensions.True<ReceptionService>();
            where = where.And(i => i.StreetId != null);
            where = where.And(i => i.PostStationId != null);
            where = where.And(i => i.ServiceAgencyId != null);
            if (dto.StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.PostStationId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.PostStationId.ToString())))
            {
                where = where.And(i => i.PostStationId == dto.PostStationId);
            }
            if (dto.StartRegisterDate != null && dto.StartRegisterDate.Value != default(DateTime))
            {
                where = where.And(i => i.CreationTime >= dto.StartRegisterDate);
            }
            if (dto.EndRegisterDate != null && dto.EndRegisterDate.Value != default(DateTime))
            {
                where = where.And(i => i.CreationTime <= dto.EndRegisterDate);
            }

            var receptionService = _dbContext.Set<ReceptionService>().Where(where);

           
            var query =
                await (from item1 in receptionService
                       join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                       from item3 in Stre.DefaultIfEmpty()
                       join item4 in _dbContext.Set<Station>() on item1.PostStationId equals item4.Id into statin
                       from item5 in statin.DefaultIfEmpty()
                       group new { item1 } by new { item1.ServiceAgencyId } into grouped
                       orderby grouped.Count() descending
                       select new StatisticstDto<Guid>
                       {
                           Key = grouped.Key.ServiceAgencyId.Value,
                           Count = grouped.Count(),
                           KeyDescription= grouped.Select(i=>i.item1.ServiceAgencyName).FirstOrDefault()
                       }).ToListAsync();

            return query;
        }


        public async Task<List<StatisticstDto<int>>> GetSourceCount(ReceptionServiceCountDto dto)
        {
            Expression<Func<ReceptionService, bool>> where = PredicateExtensions.True<ReceptionService>();
            if (dto.StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.PostStationId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.PostStationId.ToString())))
            {
                where = where.And(i => i.PostStationId == dto.PostStationId);
            }
            where = where.And(i => i.StreetId != null);
            where = where.And(i => i.PostStationId != null);

            var table1 = _dbContext.Set<ReceptionService>().Where(where);

            Expression<Func<ServiceApplication, bool>> where2 = PredicateExtensions.True<ServiceApplication>();
            where2.And(i => i.RegisterDate != null);
            if (dto.StartRegisterDate != null && dto.StartRegisterDate.Value != default(DateTime))
            {
                where2=where2.And(i => i.RegisterDate >= dto.StartRegisterDate);
            }
            if (dto.EndRegisterDate != null && dto.EndRegisterDate.Value != default(DateTime))
            {
                where2=where2.And(i => i.RegisterDate <= dto.EndRegisterDate);
            }
            var table2 = _dbContext.Set<ServiceApplication>().Where(where2);


            var query =
                await (from item1 in table1
                       join item2 in table2 on item1.Id equals item2.ServiceId 
                       join item4 in _dbContext.Set<Street>() on item1.StreetId equals item4.Id into Stre
                       from item5 in Stre.DefaultIfEmpty()
                       join item6 in _dbContext.Set<Station>() on item1.PostStationId equals item6.Id into statin
                       from item7 in statin.DefaultIfEmpty()
                       group new { item1, item2 } by new { item2.ApplicationSource } into grouped
                       orderby grouped.Count() descending
                       select new StatisticstDto<int>
                       {
                           Key = grouped.Key.ApplicationSource ?? 2,
                           Count = grouped.Count(),
                       }).ToListAsync();

            query.ForEach(i => i.KeyDescription = ((ServiceResoure)i.Key).GetDescription());
            return query;
        }

        public async Task<List<StatisticstDto<Guid>>> GetStationCount(ReceptionServiceCountDto dto)
        {
            Expression<Func<ReceptionService, bool>> where = PredicateExtensions.True<ReceptionService>();
            if (dto.StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.PostStationId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.PostStationId.ToString())))
            {
                where = where.And(i => i.PostStationId == dto.PostStationId);
            }

            where = where.And(i => i.StreetId != null);
            where = where.And(i => i.PostStationId != null);

            var table1 = _dbContext.Set<ReceptionService>().Where(where);


            Expression<Func<ServiceApplication, bool>> where2 = PredicateExtensions.True<ServiceApplication>();
            where2.And(i => i.RegisterDate != null);
            if (dto.StartRegisterDate != null && dto.StartRegisterDate.Value != default(DateTime))
            {
                where2=where2.And(i => i.RegisterDate >= dto.StartRegisterDate);
            }
            if (dto.EndRegisterDate != null && dto.EndRegisterDate.Value != default(DateTime))
            {
                where2=where2.And(i => i.RegisterDate <= dto.EndRegisterDate);
            }
            var table2 = _dbContext.Set<ServiceApplication>().Where(where2);



            var query =
                await (from item1 in table1
                       join app in table2 on item1.Id equals app.ServiceId
                       join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                       from item3 in Stre.DefaultIfEmpty()
                       join item4 in _dbContext.Set<Station>() on item1.PostStationId equals item4.Id into statin
                       from item5 in statin.DefaultIfEmpty()
                       group new { item1, item5 } by new { item5.Id } into grouped
                       where grouped.Count() > 0
                       orderby grouped.Count() descending
                       select new StatisticstDto<Guid>
                       {
                           Key = grouped.Key.Id,
                           KeyDescription = grouped.Select(i => i.item5.StationName).FirstOrDefault(),
                           Count = grouped.Count(),
                       }).ToListAsync();

            return query;
        }

        public async Task<List<StatisticstDto<Guid?>>> GetSatisfactionCount(ReceptionServiceCountDto dto)
        {
            Expression<Func<ReceptionService, bool>> where = PredicateExtensions.True<ReceptionService>();
            if (dto.StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.PostStationId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.PostStationId.ToString())))
            {
                where = where.And(i => i.PostStationId == dto.PostStationId);
            }
            where = where.And(i => i.StreetId != null);
            where = where.And(i => i.PostStationId != null);

            var table1 = _dbContext.Set<ReceptionService>().Where(where);



            Expression<Func<ServiceApplication, bool>> where2 = PredicateExtensions.True<ServiceApplication>();
            where2.And(i => i.RegisterDate != null);
            if (dto.StartRegisterDate != null && dto.StartRegisterDate.Value != default(DateTime))
            {
                where2=where2.And(i => i.RegisterDate >= dto.StartRegisterDate);
            }
            if (dto.EndRegisterDate != null && dto.EndRegisterDate.Value != default(DateTime))
            {
                where2=where2.And(i => i.RegisterDate <= dto.EndRegisterDate);
            }
            var table2 = _dbContext.Set<ServiceApplication>().Where(where2);



            var query =
                await (from item1 in table1
                       join app in table2 on item1.Id equals app.ServiceId
                       join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id
                       join item4 in _dbContext.Set<Station>() on item1.PostStationId equals item4.Id

                       group new { item4, app } by new { item4.Id } into grouped
                       orderby grouped.Sum(i => i.app.Satisfaction) descending
                       select new StatisticstDto<Guid?>
                       {
                           Key = grouped.Key.Id,
                           KeyDescription = grouped.Select(i => i.item4.StationName).FirstOrDefault(),
                           Count = grouped.Count(),
                           Sum = grouped.Sum(i => i.app.Satisfaction)
                       }).ToListAsync();


            return query;
        }


        public async Task<List<StatisticstDto<int>>> GetHoursCount(ReceptionServiceCountDto dto)
        {
            Expression<Func<ReceptionService, bool>> where = PredicateExtensions.True<ReceptionService>();
            if (dto.StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.PostStationId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.PostStationId.ToString())))
            {
                where = where.And(i => i.PostStationId == dto.PostStationId);
            }
            where = where.And(i => i.StreetId != null);
            where = where.And(i => i.PostStationId != null);


            var table1 = _dbContext.Set<ReceptionService>().Where(where);


            Expression<Func<ServiceApplication, bool>> where2 = PredicateExtensions.True<ServiceApplication>();
            where2.And(i=>i.RegisterDate!=null);
            if (dto.StartRegisterDate != null && dto.StartRegisterDate.Value != default(DateTime))
            {
                where2=where2.And(i => i.RegisterDate >= dto.StartRegisterDate);
            }
            if (dto.EndRegisterDate != null && dto.EndRegisterDate.Value != default(DateTime))
            {
                where2=where2.And(i => i.RegisterDate <= dto.EndRegisterDate);
            }
            var apptable = _dbContext.Set<ServiceApplication>().Where(where2);

            var Top5Category =
                 (from item1 in table1
                       join app in apptable on item1.Id equals app.ServiceId
                       group new { item1} by new { item1.Category } into grouped
                       where grouped.Count() > 0
                       orderby grouped.Count() descending
                       select new StatisticstDto<int>
                       {
                           Key = grouped.Key.Category,
                          
                       }).Take(5).Select(i => i.Key).ToList(); 
           
            var table2 = table1.Where(i => Top5Category.Contains(i.Category)).Select(i => i.Id);
            var table3 = table2.Join(_dbContext.Set<ServiceApplication>(), lelft => lelft, right => right.ServiceId, (lelft, right) => new { right.RegisterDate });
            var talbe4 = table3.GroupBy(i => i.RegisterDate.Hour).Select(i => new StatisticstDto<int> { Key = i.Key, Count = i.Count() });
            var result = await talbe4.ToListAsync();
            return result;
        }


        public async Task<List<dynamic>> GetMonthsCount(ReceptionServiceCountDto dto)
        {
            Expression<Func<ReceptionService, bool>> where = PredicateExtensions.True<ReceptionService>();
            if (dto.StreetId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.StreetId.ToString())))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.PostStationId != (default(Guid)) && (!string.IsNullOrWhiteSpace(dto.PostStationId.ToString())))
            {
                where = where.And(i => i.PostStationId == dto.PostStationId);
            }
            where = where.And(i => i.StreetId != null);
            where = where.And(i => i.PostStationId != null);
            var table1 = _dbContext.Set<ReceptionService>().Where(where);
            var apptable = _dbContext.Set<ServiceApplication>().Where(i => i.RegisterDate != null&& i.RegisterDate.Year==DateTime.Now.Year);
            var Top5Category =
                 (from item1 in table1
                  join app in apptable on item1.Id equals app.ServiceId
                  group new { item1 } by new { item1.Category } into grouped
                  where grouped.Count() > 0
                  orderby grouped.Count() descending
                  select new StatisticstDto<int>
                  {
                      Key = grouped.Key.Category,

                  }).Take(5).Select(i => i.Key).ToList();
            var table2 = table1.Where(i => Top5Category.Contains(i.Category)).Select(i => new { i.Id, i.Category });
            var table3 = table2.Join(_dbContext.Set<ServiceApplication>(), lelft => lelft.Id, right => right.ServiceId, (lelft, right) => new { lelft.Category, right.RegisterDate });
            var result = new List<dynamic>();
            foreach (var item in Top5Category)
            {
                var months = await table3.Where(i => i.Category == item).GroupBy(i => i.RegisterDate.Month).Select(i => new StatisticstDto<int> { Key = i.Key, Count = i.Count() }).ToListAsync();
                var resultMoths = new List<StatisticstDto<int>>();
                for (int i = 1; i <= 12; i++)
                {
                    var temp = months.Where(k => k.Key == i).FirstOrDefault();
                    if (temp == null)
                    {
                        temp = new StatisticstDto<int> { Key = i, Count = 0 };
                        resultMoths.Add(temp);
                    }
                    else
                    {
                        resultMoths.Add(temp);
                    }
                }
                result.Add(new { Category = ((ReceptionServiceCatagory)item).GetDescription(), resultMoths });
            }
            return result;
        }


    }
}
