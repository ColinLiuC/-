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
    public sealed class SpecialCareService : EfCoreRepositoryBase<SpecialCare, SpecialCareDto, Guid>, ISpecialCareService
    {
        public List<SpecialCareRegisterDto> GetSpecialCareInfo(SpecialCareDto specialcareDto, int? sAge, int? eAge, out int total, int pageIndex = 1, int pageSize = 10, int level1 = 0, int level2 = 0, int level3 = 0)
        {
            int level = level1 + level2 + level3;
            Expression<Func<SpecialCareRegisterDto, bool>> where = PredicateExtensions.True<SpecialCareRegisterDto>();
            where = where.And(p => p.RegisterType == 3);
            if (specialcareDto.JuWei != null && specialcareDto.JuWei != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.JuWei == specialcareDto.JuWei);
            }
            if (specialcareDto.Street != null && specialcareDto.Street != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.Street == specialcareDto.Street);
            }
            if (specialcareDto.Station != null && specialcareDto.Station != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.Station == specialcareDto.Station);
            }
            if (sAge != null)
            {
                where = where.And(p => p.Age >= sAge);
            }
            if (eAge != null)
            {
                where = where.And(p => p.Age <= eAge);
            }
            if (!string.IsNullOrEmpty(specialcareDto.Name))
            {
                where = where.And(p => p.Name.Contains(specialcareDto.Name));
            }
            if (!string.IsNullOrEmpty(specialcareDto.Card))
            {
                where = where.And(p => p.Card.Contains(specialcareDto.Card));
            }
            if (specialcareDto.SpecialCareType != null && specialcareDto.SpecialCareType != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.SpecialCareType == specialcareDto.SpecialCareType);
            }
            if (specialcareDto.Category != null && specialcareDto.Category != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.Category == specialcareDto.Category);
            }
            switch (level)
            {
                case 7:
                    break;
                case 6:
                    where = where.And(p => p.Cycle != 0);
                    where = where.And(p => ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 5 && ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays > 0 || ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays > 5);
                    break;
                case 5:
                    where = where.And(p => p.Cycle != 0);
                    where = where.And(p => ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 0 || ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays > 5);
                    break;
                case 4:
                    where = where.And(p => p.Cycle != 0);
                    where = where.And(p => ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays > 5);
                    break;
                case 3:
                    where = where.And(p => p.Cycle != 0);
                    where = where.And(p => ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 0 || ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 5 && ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays > 0);
                    break;
                case 2:
                    where = where.And(p => p.Cycle != 0);
                    where = where.And(p => ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 5 && ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays > 0);
                    break;
                case 1:
                    where = where.And(p => p.Cycle != 0);
                    where = where.And(p => ((p.Time.AddDays(int.Parse(p.Cycle.ToString())) - DateTime.Now)).TotalDays <= 0);
                    break;
                case 0:
                    where = where.And(p => p.level == 0);
                    break;
            }
            var list = _dbContext.Registers.Join(_dbContext.SpecialCares, p => p.OldPeopleId, p => p.Id, (Register, SpecialCares) => new SpecialCareRegisterDto()
            {
                Id = SpecialCares.Id,
                Card = SpecialCares.Card,
                Name = SpecialCares.Name,
                JuWei = SpecialCares.JuWei,
                Street=SpecialCares.Street,
                Station=SpecialCares.Station,
                SpecialCareType = SpecialCares.SpecialCareType,
                Age = SpecialCares.Age,
                Sex = SpecialCares.Sex,
                Phone = SpecialCares.Phone,
                Address = SpecialCares.Address,
                CreationTime = SpecialCares.CreationTime,
                Time = Register.Time,
                Cycle = Register.Cycle,
                level = Register.level,
                Type = Register.Type,
                Category = Register.Category,
                RegisterType = Register.RegisterType

            }).Where(where);

            total = list.Count();
            var result = list.OrderByDescending(p => p.CreationTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }




        #region 统计分析  create by 刘城 2018-06-15

        /// <summary>
        /// 根据类别统计
        /// </summary>
        /// <returns></returns>

        public List<MyKeyAndValue> StatisticsByType(List<Guid> juweiids)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                DataDictionaryService d_service = new DataDictionaryService();
                var list = db.SpecialCares.Where(p => juweiids.Contains(p.JuWei)).GroupBy(p => p.SpecialCareType).Select(g => new MyKeyAndValue
                {
                    Key = g.Key,
                    Count = g.Count()
                }).ToList();
                foreach (var item in list)
                {
                    item.KeyName = d_service.GetItemNameById(Guid.Parse(item.Key.ToString()));
                }
                return list;
            }
        }



        /// <summary>
        /// 根据年龄统计
        /// </summary>
        /// <returns></returns>
        public List<MyKeyAndValue> StatisticsByAge(List<Guid> juweiids)
        {
            List<MyKeyAndValue> list = new List<MyKeyAndValue>();
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var _list = db.SpecialCares.Where(p => juweiids.Contains(p.JuWei)).OrderBy(p => p.Age).ToList();
                MyKeyAndValue item1 = new MyKeyAndValue()
                {
                    KeyName = "0-10",
                    Count = _list.Where(p => (p.Age >= 0 && p.Age <= 10)).Count()
                };
                MyKeyAndValue item2 = new MyKeyAndValue()
                {
                    KeyName = "11-20",
                    Count = _list.Where(p => (p.Age >= 11 && p.Age <= 20)).Count()
                };
                MyKeyAndValue item3 = new MyKeyAndValue()
                {
                    KeyName = "21-30",
                    Count = _list.Where(p => (p.Age >= 21 && p.Age <= 30)).Count()
                };
                MyKeyAndValue item4 = new MyKeyAndValue()
                {
                    KeyName = "31-40",
                    Count = _list.Where(p => (p.Age >= 31 && p.Age <= 40)).Count()
                };
                MyKeyAndValue item5 = new MyKeyAndValue()
                {
                    KeyName = "41-50",
                    Count = _list.Where(p => (p.Age >= 41 && p.Age <= 50)).Count()
                };
                MyKeyAndValue item6 = new MyKeyAndValue()
                {
                    KeyName = "51-60",
                    Count = _list.Where(p => (p.Age >= 51 && p.Age <= 60)).Count()
                };
                MyKeyAndValue item7 = new MyKeyAndValue()
                {
                    KeyName = "60岁以上",
                    Count = _list.Where(p => (p.Age >= 60)).Count()
                };
                list.Add(item1);
                list.Add(item2);
                list.Add(item3);
                list.Add(item4);
                list.Add(item5);
                list.Add(item6);
                list.Add(item7);
            }
            return list;
        }


        /// <summary>
        /// 按月分组统计服务情况
        /// </summary>
        /// <returns></returns>
        public List<MyKeyAndValue> StatisticsByMonth(List<Guid> juweiids)
        {
            List<MyKeyAndValue> list = new List<MyKeyAndValue>();
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var _list = db.RegisterHistory.Join(db.SpecialCares, r => r.OldPeopleId, o => o.Id, (r, o) => new
                {
                    registerHistory = r,
                    oldPeoples = o
                }).Where(p => p.registerHistory.RegisterType == 3 && juweiids.Contains(p.oldPeoples.JuWei)).ToList();

                if (_list != null)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var a = new MyKeyAndValue();
                        List<DateTime> d = GetTime(i);
                        a.KeyName = i.ToString();
                        a.Count = _list.Where(p => p.registerHistory.Time >= d[0] && p.registerHistory.Time <= d[1]).Count();
                        list.Add(a);
                    };
                }
            }
            return list;
        }


        /// <summary>
        /// 按服务分类统计服务情况
        /// </summary>
        /// <returns></returns>
        public List<MyKeyAndValue> StatisticsByCategory(List<Guid> juweiids)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var _list = db.RegisterHistory.Join(db.SpecialCares, r => r.OldPeopleId, o => o.Id, (r, o) => new
                {
                    registerHistory = r,
                    oldPeoples = o
                }).Where(p => p.registerHistory.RegisterType == 3 && juweiids.Contains(p.oldPeoples.JuWei)).GroupBy(p => p.registerHistory.Category).Select(g => new MyKeyAndValue()
                {
                    Key = g.Key,
                    Count = g.Count()
                }).ToList();

                if (_list != null)
                {
                    DataDictionaryService d_service = new DataDictionaryService();
                    foreach (var item in _list)
                    {
                        item.KeyName = d_service.GetItemNameById(Guid.Parse(item.Key.ToString()));
                    }
                }
                return _list;
            }
        }

        /// <summary>
        /// 根据月份获取本月数据
        /// </summary>
        /// <param name="month"></param>
        /// <returns></returns>
        public List<DateTime> GetTime(int month)
        {
            List<DateTime> result = new List<DateTime>();
            int Year = DateTime.Now.Year;
            DateTime d1 = new DateTime(Year, month, 1); //本月第一天
            DateTime d2 = d1.AddMonths(1).AddDays(-1); //本月最后一天
            result.Add(d1);
            result.Add(d2);
            return result;
        }


        /// <summary>
        /// 统计所在居委下的老人数量
        /// </summary>
        /// <param name="streetid">街道id</param>
        /// <param name="poststationid">驿站id</param>
        /// <returns></returns>
        public List<MyJuWeiDituModel> StatisticsByJuWei(List<Guid> juweiids)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                List<MyJuWeiDituModel> list = new List<MyJuWeiDituModel>();
                if (juweiids != null)
                {
                    JuWeiService js = new JuWeiService();
                    var _list = db.SpecialCares.Where(p => juweiids.Contains(p.JuWei)).GroupBy(p => p.JuWei).Select(g => new
                    {
                        g.Key,
                        Count = g.Count()
                    }).ToList();

                    foreach (var item in _list)
                    {
                        JuWei info = js.GetJuWeiById(item.Key);
                        MyJuWeiDituModel aa = new MyJuWeiDituModel();
                        aa.Count = item.Count;
                        aa.Lat = info.Lat;
                        aa.Lng = info.Lng;
                        list.Add(aa);
                    }
                    return list;
                }
                return null;
            }
        }
        public List<Guid> GetJuWeiIds(Guid? streetid, Guid? poststationid)
        {
            List<Guid> juweiids = new List<Guid>();
            JuWeiService js = new JuWeiService();
            if (streetid != null && poststationid != null)
            {
                //获取驿站下所有居委
                juweiids = js.GetJuWeisByStation(Guid.Parse(poststationid.ToString()));
            }
            if (streetid != null && poststationid == null)
            {
                //获取街道下所有居委
                juweiids = js.GetJuWeisByStreet(Guid.Parse(streetid.ToString()));
            }
            if (streetid == null && poststationid == null)
            {
                //获取所有居委
                juweiids = js.GetJuWeiAll();
            }
            return juweiids;
        }
        #endregion




    }




}
