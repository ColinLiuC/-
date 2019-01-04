using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using CoreSolution.Tools.Extensions;
using Microsoft.EntityFrameworkCore;

namespace CoreSolution.Service
{
    public class PeopleGroupService : EfCoreRepositoryBase<PeopleGroup, PeopleGroupDto, Guid>, IPeopleGroupService
    {
        public async Task<PeopleGroupDto> GetPeopleGroupById(Guid id)
        {
            var result = await GetAsync(id);
            if (result!=null)
            {
                result.GroupCateGoryDescription = result.GroupCateGory.GetDescription();
                string workPersonIds = result.WorkPersonIds;
                if (workPersonIds!=null)
                {
                   var ids= workPersonIds.Split(',').ToList().ConvertAll<Guid>(i => Guid.Parse(i));
                    var perNames= _dbContext.Set<WorkPerson>()
                        .Where(i => ids.Contains(i.Id))
                        .Select(i=>i.PerName)
                        .ToArray();
                    result.WorkPersonNames = string.Join(",", perNames);
                }
                return result;
            }
             return null;
          
        }

        public async Task<Tuple<int, List<PeopleGroupDto>>> GetPeopleGroupPagedAsync(PeopleGroupDto dto, int pageIndex, int pageSize)
        {
            Expression<Func<PeopleGroup, bool>> where = PredicateExtensions.True<PeopleGroup>();
            if (dto.StreetId != null && dto.StreetId != (default(Guid)))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }
            if (dto.StationId != null && dto.StationId != (default(Guid)))
            {
                where = where.And(i => i.StationId == dto.StationId);
            }
            if (!string.IsNullOrWhiteSpace(dto.GroupName))
            {
                where = where.And(i => i.GroupName.Contains(dto.GroupName));
            }
            if (dto.GroupCateGory != 0)
            {
                where = where.And(i => i.GroupCateGory == dto.GroupCateGory);
            }
             if (!string.IsNullOrWhiteSpace(dto.DutyPeople))
            {
                where = where.And(i => i.DutyPeople.Contains(dto.DutyPeople));
            }

            var query = _dbContext.Set<PeopleGroup>().AsQueryable().Where(where)
                .Select(i=>new PeopleGroupDto {
                    Id=i.Id,
                    GroupName=i.GroupName,
                    GroupCateGory=i.GroupCateGory,
                    DutyPeople=i.DutyPeople,
                    DutyPeopleTelPhone=i.DutyPeopleTelPhone,
                    CreationTime = i.CreationTime
                });
                       
            var count = await query.Distinct().CountAsync();
            var data = await query.Distinct().OrderByDescending(i => i.CreationTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            data.ForEach(i => i.GroupCateGoryDescription = i.GroupCateGory.GetDescription());
            return new Tuple<int, List<PeopleGroupDto>>(count, data);
        }
    }
}
