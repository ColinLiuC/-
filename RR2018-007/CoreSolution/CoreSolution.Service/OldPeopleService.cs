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
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;
using Microsoft.EntityFrameworkCore;


namespace CoreSolution.Service
{
    public sealed class OldPeopleService : EfCoreRepositoryBase<OldPeople, OldPeopleDto, Guid>, IOldPeopleService
    {
        public List<ServiceInfoDto> GetOldPeopleInfo(OldPeopleDto oldpeopleDto, int? sAge, int? eAge, out int total, int pageIndex = 1, int pageSize = 10, int level1 = 0, int level2 = 0, int level3 = 0)
        {
            int level = level1 + level2 + level3;
            Expression<Func<ServiceInfoDto, bool>> where = PredicateExtensions.True<ServiceInfoDto>();
            where = where.And(p => p.RegisterType == 1);
            if (oldpeopleDto.JuWei != null && oldpeopleDto.JuWei != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.JuWei == oldpeopleDto.JuWei);
            }
            if (oldpeopleDto.Street != null && oldpeopleDto.Street != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.Street == oldpeopleDto.Street);
            }
            if (oldpeopleDto.Station != null && oldpeopleDto.Station != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.Station == oldpeopleDto.Station);
            }
            if (oldpeopleDto.OldType != null && oldpeopleDto.OldType != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.OldType == oldpeopleDto.OldType);
            }
            if (sAge != null)
            {
                where = where.And(p => p.Age >= sAge);
            }
            if (eAge != null)
            {
                where = where.And(p => p.Age <= eAge);
            }
            if (oldpeopleDto.keji != null)
            {
                where = where.And(p => p.keji == oldpeopleDto.keji);
            }
            if (oldpeopleDto.yiyang != null)
            {
                where = where.And(p => p.yiyang == oldpeopleDto.yiyang);
            }
            if (oldpeopleDto.niunai != null)
            {
                where = where.And(p => p.niunai == oldpeopleDto.niunai);
            }
            if (!string.IsNullOrEmpty(oldpeopleDto.Name))
            {
                where = where.And(p => p.Name.Contains(oldpeopleDto.Name));
            }
            if (!string.IsNullOrEmpty(oldpeopleDto.Card))
            {
                where = where.And(p => p.Card.Contains(oldpeopleDto.Card));
            }
            if (oldpeopleDto.Category != null && oldpeopleDto.Category != Guid.Parse("00000000-0000-0000-0000-000000000000"))
            {
                where = where.And(p => p.Category == oldpeopleDto.Category);
            }
            if (oldpeopleDto.Sex != Sex.all)
            {
                where = where.And(p => p.Sex == oldpeopleDto.Sex);
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
            var list = _dbContext.Registers.Join(_dbContext.OldPeoples, p => p.OldPeopleId, p => p.Id, (Register, OldPeople) => new ServiceInfoDto()
            {
                Id = OldPeople.Id,
                Card = OldPeople.Card,
                Name = OldPeople.Name,
                JuWei = OldPeople.JuWei,
                Street = OldPeople.Street,
                Station = OldPeople.Station,
                Category = Register.Category,
                Age = OldPeople.Age,
                OldType = OldPeople.OldType,
                Sex = OldPeople.Sex,
                keji = OldPeople.keji,
                yiyang = OldPeople.yiyang,
                niunai = OldPeople.niunai,
                Phone = OldPeople.Phone,
                Address = OldPeople.Address,
                CreationTime = OldPeople.CreationTime,
                Time = Register.Time,
                Cycle = Register.Cycle,
                level = Register.level,
                RegisterType = Register.RegisterType
            }).Where(where);

            total = list.Count();
            var result = list.OrderByDescending(p => p.CreationTime).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }



        #region 统计分析  create by 刘城 2018-06-14

        /// <summary>
        /// 根据老人类别统计
        /// </summary>
        /// <returns></returns>

        public async Task<List<MyKeyAndValue>> StatisticsByType(List<Guid> juweiids)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                DataDictionaryService d_service = new DataDictionaryService();
                var list =await db.OldPeoples.Where(p => juweiids.Contains(p.JuWei)).GroupBy(p => p.OldType).Select(g => new MyKeyAndValue
                {
                    Key = g.Key,
                    Count = g.Count()
                }).ToListAsync();
                foreach (var item in list)
                {
                    item.KeyName = d_service.GetItemNameById(Guid.Parse(item.Key.ToString()));
                }
                return list;
            }
        }



        /// <summary>
        /// 根据老人年龄统计
        /// </summary>
        /// <returns></returns>
        public async Task<List<MyKeyAndValue>> StatisticsByAge(List<Guid> juweiids)
        {
            List<MyKeyAndValue> list = new List<MyKeyAndValue>();
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var _list = await db.OldPeoples.Where(p => juweiids.Contains(p.JuWei)).OrderBy(p => p.Age).ToListAsync();
                MyKeyAndValue item1 = new MyKeyAndValue()
                {
                    KeyName = "55-60",
                    Count = _list.Where(p => (p.Age >= 55 && p.Age <= 60)).Count()
                };
                MyKeyAndValue item2 = new MyKeyAndValue()
                {
                    KeyName = "61-70",
                    Count = _list.Where(p => (p.Age >= 61 && p.Age <= 70)).Count()
                };
                MyKeyAndValue item3 = new MyKeyAndValue()
                {
                    KeyName = "71-80",
                    Count = _list.Where(p => (p.Age >= 71 && p.Age <= 80)).Count()
                };
                MyKeyAndValue item4 = new MyKeyAndValue()
                {
                    KeyName = "81-90",
                    Count = _list.Where(p => (p.Age >= 81 && p.Age <= 90)).Count()
                };
                MyKeyAndValue item5 = new MyKeyAndValue()
                {
                    KeyName = "91-100",
                    Count = _list.Where(p => (p.Age >= 91 && p.Age <= 100)).Count()
                };
                list.Add(item1);
                list.Add(item2);
                list.Add(item3);
                list.Add(item4);
                list.Add(item5);
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
                var _list = db.RegisterHistory.Join(db.OldPeoples, r => r.OldPeopleId, o => o.Id, (r, o) => new
                {
                    registerHistory = r,
                    oldPeoples = o
                }).Where(p => p.registerHistory.RegisterType == 1 && juweiids.Contains(p.oldPeoples.JuWei)).ToList();

                if (_list != null)
                {
                    for (int i = 1; i <= 12; i++)
                    {
                        var a = new MyKeyAndValue();
                        List<DateTime> d = GetTime(i);
                        a.KeyName = i.ToString();
                        a.Count = _list.Where(p => p.registerHistory.Time > d[0] && p.registerHistory.Time < d[1]).Count();
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
                var _list = db.RegisterHistory.Join(db.OldPeoples, r => r.OldPeopleId, o => o.Id, (r, o) => new
                {
                    registerHistory = r,
                    oldPeoples = o
                }).Where(p => p.registerHistory.RegisterType == 1 && juweiids.Contains(p.oldPeoples.JuWei)).GroupBy(p => p.registerHistory.Category).Select(g => new MyKeyAndValue()
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
                    var _list = db.OldPeoples.Where(p => juweiids.Contains(p.JuWei)).GroupBy(p => p.JuWei).Select(g => new
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




        //统计分析首页用户数据
        public List<MyKeyAndValue> GetObjectUser(List<Guid> juweiids)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var oldusercount = db.OldPeoples.Count(p => juweiids.Contains(p.JuWei));
                var disusercount = db.Disabilities.Count(p => juweiids.Contains(p.JuWei));
                var spusercount = db.SpecialCares.Count(p => juweiids.Contains(p.JuWei));
                var otherusercount = db.OtherPeoples.Count(p => juweiids.Contains(p.JuWei));
                List<MyKeyAndValue> list = new List<MyKeyAndValue>()
                {
                     new MyKeyAndValue(){
                        KeyName="老人",
                        Count=oldusercount
                    },
                      new MyKeyAndValue(){
                        KeyName="残疾人",
                        Count=disusercount
                    },
                        new MyKeyAndValue(){
                        KeyName="优抚对象",
                        Count=spusercount
                    },
                          new MyKeyAndValue(){
                        KeyName="其他重点人员",
                        Count=otherusercount
                    },
                };
                return list;
            }

        }


        //统计分析首页地图数据
        public List<MyJuWeiDituModel> StatisticsByJuWeiAll(List<Guid> juweiids)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                List<MyJuWeiDituModel> list = new List<MyJuWeiDituModel>();
                if (juweiids != null)
                {
                    JuWeiService js = new JuWeiService();
                    var _list1 = db.OldPeoples.Where(p => juweiids.Contains(p.JuWei)).GroupBy(p => p.JuWei).Select(g => new
                    {
                        g.Key,
                        Count = g.Count()
                    }).ToList();
                    var _list2 = db.Disabilities.Where(p => juweiids.Contains(p.JuWei)).GroupBy(p => p.JuWei).Select(g => new
                    {
                        g.Key,
                        Count = g.Count()
                    }).ToList();

                    var _list3 = db.SpecialCares.Where(p => juweiids.Contains(p.JuWei)).GroupBy(p => p.JuWei).Select(g => new
                    {
                        g.Key,
                        Count = g.Count()
                    }).ToList();
                    var _list4 = db.OtherPeoples.Where(p => juweiids.Contains(p.JuWei)).GroupBy(p => p.JuWei).Select(g => new
                    {
                        g.Key,
                        Count = g.Count()
                    }).ToList();
                    //连接多个集合，取并集
                    var listall = _list1.Union(_list2).Union(_list3).Union(_list4);
                    foreach (var item in listall)
                    {
                        JuWei info = js.GetJuWeiById(item.Key);
                        MyJuWeiDituModel aa = new MyJuWeiDituModel();
                        aa.Count = item.Count;
                        aa.Lat = info.Lat;
                        aa.Lng = info.Lng;
                        list.Add(aa);
                    }
                    //分组过滤重复数据+求和
                    list = list.GroupBy(p => new { p.Lat, p.Lng }).Select(g => new MyJuWeiDituModel()
                    {
                        Count = g.Sum(k => k.Count),
                        Lng = g.First().Lng,
                        Lat = g.First().Lat,
                    }).ToList();

                    return list;
                }
                return null;
            }
        }

        #endregion

    }
}
