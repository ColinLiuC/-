using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    public class WorkforceManagementService : EfCoreRepositoryBase<WorkforceManagement, WorkforceManagementDto, Guid>, IWorkforceManagementService
    {
        public async Task<Tuple<dynamic, List<WorkforceManagementDto>>> GetWorkforceManagementByWeekAsync(WorkforceManagementDto dto)
        {
            DateTime dateTime = DateTime.Now;
            int year = dateTime.Year;
            int month = dateTime.Month;
            int week = ((int)dateTime.DayOfWeek) == 0 ? 7 : (int)dateTime.DayOfWeek;

            Expression<Func<WorkforceManagement, bool>> where = PredicateExtensions.True<WorkforceManagement>();
            if (dto.StreetId != null && dto.StreetId != (default(Guid)))
            {
                where = where.And(i => i.StreetId == dto.StreetId);
            }

            if (dto.StationId != null && dto.StationId != (default(Guid)))
            {
                where = where.And(i => i.StationId == dto.StationId);
            }

            if (!string.IsNullOrWhiteSpace(dto.WorkforceYear))
            {
                year = Convert.ToInt32(dto.WorkforceYear);
                where = where.And(i => i.WorkforceYear == dto.WorkforceYear);
            }
            else
            {
                where = where.And(i => i.WorkforceYear == year.ToString());
            }

            if (!string.IsNullOrWhiteSpace(dto.WorkforceMonth))
            {
                month = Convert.ToInt32(dto.WorkforceMonth);
                where = where.And(i => i.WorkforceMonth == dto.WorkforceMonth);
            }
            else
            {
                where = where.And(i => i.WorkforceMonth == month.ToString());
            }

            if (!string.IsNullOrWhiteSpace(dto.WorkforceWeek))
            {
                week = Convert.ToInt32(dto.WorkforceWeek);
                where = where.And(i => i.WorkforceWeek == dto.WorkforceWeek);
            }
            else
            {
                where = where.And(i => i.WorkforceWeek == week.ToString());
            }

            DateTime weekBeginDate = WeekBeginDate(year, month, week);
            string day1 = weekBeginDate.Month + "." + weekBeginDate.Day;
            string day2 = weekBeginDate.AddDays(1).Month + "." + weekBeginDate.AddDays(1).Day;
            string day3 = weekBeginDate.AddDays(2).Month + "." + weekBeginDate.AddDays(2).Day;
            string day4 = weekBeginDate.AddDays(3).Month + "." + weekBeginDate.AddDays(3).Day;
            string day5 = weekBeginDate.AddDays(4).Month + "." + weekBeginDate.AddDays(4).Day;
            string day6 = weekBeginDate.AddDays(5).Month + "." + weekBeginDate.AddDays(5).Day;
            string day7 = weekBeginDate.AddDays(6).Month + "." + weekBeginDate.AddDays(6).Day;

            var Title = new List<dynamic> {
                new {Zou=day1 },
                new {Zou=day2 },
                new {Zou=day3 },
                new {Zou=day4 },
                new {Zou=day5 },
                new {Zou=day6 },
                new {Zou=day7 },
            };

            where = where.And(i => i.WorkforceDay == day1 || i.WorkforceDay == day2 || i.WorkforceDay == day3 || i.WorkforceDay == day4 || i.WorkforceDay == day5 || i.WorkforceDay == day6 || i.WorkforceDay == day7);

            var query1 = _dbContext.Set<WorkforceManagement>().AsQueryable().Where(where);
            var query = from item1 in query1
                        join item2 in _dbContext.Set<PeopleGroup>() on item1.PeopleGroupId equals item2.Id into Stre
                        from item3 in Stre.DefaultIfEmpty()
                        select new WorkforceManagementDto()
                        {
                            Id = item1.Id,
                            StreetId = item1.StreetId,
                            StationId = item1.StationId,
                            WorkforceYear = item1.WorkforceYear,
                            WorkforceMonth = item1.WorkforceMonth,
                            WorkforceDay = item1.WorkforceDay,
                            WorkforceWeek = item1.WorkforceWeek,
                            DayState = item1.DayState,
                            BeginTime = item1.BeginTime,
                            EndTime = item1.EndTime,
                            PeopleGroupId = item1.PeopleGroupId,
                            Remark = item1.Remark,
                            RegisterPeople = item1.RegisterPeople,
                            RegisterDate = item1.RegisterDate,
                            GroupName=item3.GroupName,
                            DutyPeople= item3.DutyPeople,
                           GroupCateGory=item3.GroupCateGory
                        };

            var list = await query.ToListAsync();
            list.ForEach(i=>i.GroupCateGoryName= i.GroupCateGory.GetDescription());

            var newList = new List<WorkforceManagementDto>();
            var newDto = new WorkforceManagementDto { ObjectIsNull = 2 };
            for (int j = 1; j < 4; j++)
            {
                newList.Add(list.Where(i => i.WorkforceDay == day1 && i.DayState == j).FirstOrDefault() ?? newDto);
                newList.Add(list.Where(i => i.WorkforceDay == day2 && i.DayState == j).FirstOrDefault() ?? newDto);
                newList.Add(list.Where(i => i.WorkforceDay == day3 && i.DayState == j).FirstOrDefault() ?? newDto);
                newList.Add(list.Where(i => i.WorkforceDay == day4 && i.DayState == j).FirstOrDefault() ?? newDto);
                newList.Add(list.Where(i => i.WorkforceDay == day5 && i.DayState == j).FirstOrDefault() ?? newDto);
                newList.Add(list.Where(i => i.WorkforceDay == day6 && i.DayState == j).FirstOrDefault() ?? newDto);
                newList.Add(list.Where(i => i.WorkforceDay == day7 && i.DayState == j).FirstOrDefault() ?? newDto);
            }

            return new Tuple<dynamic, List<WorkforceManagementDto>>(Title, newList);

        }
        private DateTime WeekBeginDate(int year, int month, int week)
        {
            int weekCount = GetWeekCountByMonth(year, month);
            int[] temp1 = new int[weekCount];
            for (int i = 0; i < temp1.Length; i++)
            {
                temp1[i] = weekCount--;
            }
            int tempWeek = temp1[week - 1];
            year = month == 12 ? year + 1 : year;
            month = month == 12 ? 1 : month + 1;
            string format = year + "-" + month + "-1";
            DateTime dateTime = Convert.ToDateTime(format).AddDays(-1);
            int dayiw = ((int)dateTime.DayOfWeek) == 0 ? 7 : (int)dateTime.DayOfWeek;//当前 星期
            //星期  开始日期
            DateTime myWeekBeginDate = dateTime.AddDays(1 - dayiw);
            int temp = (tempWeek - 1) * 7; //week=5
            return myWeekBeginDate.AddDays(-temp);
        }
        private int GetWeekCountByMonth(int year, int month)
        {
            year = month == 12 ? year + 1 : year;
            month = month == 12 ? 1 : month + 1;
            string format = year + "-" + month + "-1";
            DateTime dateTime = Convert.ToDateTime(format).AddDays(-1);
            int dayiw = ((int)dateTime.DayOfWeek) == 0 ? 7 : (int)dateTime.DayOfWeek;//当前 星期
            double day = dateTime.AddDays(1 - dayiw).Day;//4
            int temp = Convert.ToInt32(Math.Round((day + 6) / 7, 0));
            return temp;
        }

        public async Task<WorkforceManagementDto> GetWorkforceManagementById(Guid id)
        {
            var query1 = _dbContext.Set<WorkforceManagement>().AsQueryable().Where(i=>i.Id== id);
            var query = from item1 in query1
                        join item2 in _dbContext.Set<PeopleGroup>() on item1.PeopleGroupId equals item2.Id into Stre
                        from item3 in Stre.DefaultIfEmpty()
                        select new WorkforceManagementDto()
                        {
                            Id = item1.Id,
                            StreetId = item1.StreetId,
                            StationId = item1.StationId,
                            WorkforceYear = item1.WorkforceYear,
                            WorkforceMonth = item1.WorkforceMonth,
                            WorkforceDay = item1.WorkforceDay,
                            WorkforceWeek = item1.WorkforceWeek,
                            DayState = item1.DayState,
                            BeginTime = item1.BeginTime,
                            EndTime = item1.EndTime,
                            PeopleGroupId = item1.PeopleGroupId,
                            Remark = item1.Remark,
                            RegisterPeople = item1.RegisterPeople,
                            RegisterDate = item1.RegisterDate,
                            GroupName = item3.GroupName,
                            DutyPeople = item3.DutyPeople,
                            GroupCateGory = item3.GroupCateGory
                        };

            var dto = await query.FirstOrDefaultAsync();
            dto.GroupCateGoryName = dto.GroupCateGory.GetDescription();
            return dto;
        }
    }
}
