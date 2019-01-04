using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore;
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

namespace CoreSolution.Service
{
    public sealed class ExpenditureService : EfCoreRepositoryBase<Expenditure, ExpenditureDto, Guid>, IExpenditureService
    {
        public async Task<ExpenditureDto> GetExpenditureById(Guid id)
        {
            var queryFixedAssets = _dbContext.Set<Expenditure>().AsQueryable().Where(i=>i.Id== id);
            var query = from item1 in queryFixedAssets
                        join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                        from item3 in Stre.DefaultIfEmpty()
                        join item4 in _dbContext.Set<Station>() on item1.StationId equals item4.Id into statin
                        from item5 in statin.DefaultIfEmpty()
                        select new ExpenditureDto() {
                            Id = item1.Id,
                            ExpenditureName = item1.ExpenditureName,
                            Category = item1.Category,
                            StreetId = item1.StreetId,
                            StationId = item1.StationId,
                            UseMoney = item1.UseMoney,
                            UseDate = item1.UseDate,
                            DutyPeople = item1.DutyPeople,
                            StreetName = item3.StreetName,
                            StationName = item5.StationName,
                            Purpose = item1.Purpose,
                            Accessory = item1.Accessory,
                            AccessoryUrl = item1.AccessoryUrl,
                            Remark = item1.Remark,
                            RegisterPeople = item1.RegisterPeople,
                            RegisterDate = item1.RegisterDate
                        };

            var dto=await query.FirstOrDefaultAsync();
            dto.CategoryDescription = dto.Category.GetDescription();
            return dto;
        }

        public async Task<(int, List<ExpenditureDto>)> GetExpenditurePagedAsync(ExpenditureDto dto, int pageIndex, int pageSize)
        {
            Expression<Func<Expenditure, bool>> where = PredicateExtensions.True<Expenditure>();
            if (dto.StreetId != null && dto.StreetId != (default(Guid)))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.StationId != null && dto.StationId != (default(Guid)))
            {
                where = where.And(i => i.StationId == dto.StationId);
            }
            if (!string.IsNullOrWhiteSpace(dto.ExpenditureName))
            {
                where = where.And(i => i.ExpenditureName.Contains(dto.ExpenditureName));
            }
            if (dto.Category != 0)
            {
                where = where.And(i => i.Category == dto.Category);
            }

            if (dto.StartUseDate != null)
            {
                where = where.And(i => i.UseDate >= dto.StartUseDate);
            }
            if (dto.EndUseDate != null)
            {
                where = where.And(i => i.UseDate <= dto.EndUseDate);
            }

           

            var queryFixedAssets = _dbContext.Set<Expenditure>().Where(where);
           

            var query = from item1 in queryFixedAssets
                        join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                        from item3 in Stre.DefaultIfEmpty()
                        join item4 in _dbContext.Set<Station>() on item1.StationId equals item4.Id into statin
                        from item5 in statin.DefaultIfEmpty()
                        select new ExpenditureDto() { Id = item1.Id, ExpenditureName = item1.ExpenditureName, Category = item1.Category, StreetId = item1.StreetId, StationId = item1.StationId, UseMoney = item1.UseMoney, UseDate = item1.UseDate, DutyPeople=item1.DutyPeople, StreetName = item3.StreetName, StationName = item5.StationName, AccessoryUrl = item1.AccessoryUrl, CreationTime=item1.CreationTime};

            var count = await query.Distinct().CountAsync();
            var data = await query.Distinct().OrderByDescending(i => i.CreationTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            data.ForEach(i => i.CategoryDescription=i.Category.GetDescription());
            return (count, data);
        }

    }
}
