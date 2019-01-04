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
    public sealed class FixedAssetsService : EfCoreRepositoryBase<FixedAssets, FixedAssetsDto, Guid>, IFixedAssetsService
    {
        public async Task<FixedAssetsDto> GetFixedAssetsById(Guid id)
        {
         
            var query = from item1 in _dbContext.Set<FixedAssets>()
                        join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                        from item3 in Stre.DefaultIfEmpty()
                        join item4 in _dbContext.Set<Station>() on item1.StationId equals item4.Id into statin
                        from item5 in statin.DefaultIfEmpty()
                        where item1.Id==id
                        select new FixedAssetsDto()
                        {
                            Id = item1.Id,
                            Number = item1.Number,
                            Name = item1.Name,
                            Version = item1.Version,
                            StreetId = item1.StreetId,
                            StationId = item1.StationId,
                            PurchaseDate = item1.PurchaseDate,
                            CurrentState = item1.CurrentState,
                            StreetName = item3.StreetName,
                            StationName = item5.StationName,
                            DutyPeople = item1.DutyPeople,
                            Telephone = item1.Telephone,
                            Description = item1.Description,
                            Photo = item1.Photo,
                            PhotoUrl = item1.PhotoUrl,
                            RegisterPeople = item1.RegisterPeople,
                            RegisterDate = item1.RegisterDate
                        };
            var dto = await query.FirstOrDefaultAsync();
            if (dto!=null)
            {
                dto.CurrentStateDescription = dto.CurrentState.GetDescription();
            }
            
            return dto;
        }

        public async Task<Tuple<int, List<FixedAssetsDto>>> GetFixedAssetsPagedAsync(FixedAssetsDto dto, int pageIndex, int pageSize)
        {
            Expression<Func<FixedAssetsDto, bool>> where = PredicateExtensions.True<FixedAssetsDto>();
            if (dto.StreetId != null && dto.StreetId != (default(Guid)))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.StationId != null && dto.StationId != (default(Guid)))
            {
                where = where.And(i => i.StationId == dto.StationId);
            }
            if (!string.IsNullOrWhiteSpace(dto.Name))
            {
                where = where.And(i => i.Name.Contains(dto.Name));
            }
            if (!string.IsNullOrWhiteSpace(dto.Number))
            {
                where = where.And(i => i.Number == dto.Number);
            }
            if (dto.CurrentState != 0)
            {
                where = where.And(i => i.CurrentState == dto.CurrentState);
            }
            if (dto.StratPurchaseDate != null)
            {
                where = where.And(i => i.PurchaseDate >= dto.StratPurchaseDate);
            }
            if (dto.EndPurchaseDate != null)
            {
                where = where.And(i => i.PurchaseDate <= dto.EndPurchaseDate);
            }

           
            var query = (from item1 in _dbContext.Set<FixedAssets>()
                        join item2 in _dbContext.Set<Street>() on item1.StreetId equals item2.Id into Stre
                        from item3 in Stre.DefaultIfEmpty()
                        join item4 in _dbContext.Set<Station>() on item1.StationId equals item4.Id into statin
                        from item5 in statin.DefaultIfEmpty()
                        select new FixedAssetsDto() {
                            Id = item1.Id, Number = item1.Number,
                            Name = item1.Name, StreetId = item1.StreetId,
                            StationId = item1.StationId, PurchaseDate = item1.PurchaseDate,
                            CurrentState = item1.CurrentState,
                            StreetName = item3.StreetName, StationName = item5.StationName ,
                            CreationTime = item1.CreationTime
                        }).Where(where);

            var count = await query.Distinct().CountAsync();
            var data = await query.Distinct().OrderByDescending(i => i.CreationTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            data.ForEach(i => i.CurrentStateDescription = i.CurrentState.GetDescription());
            return new Tuple<int, List<FixedAssetsDto>>(count, data);


        }
    }
}
