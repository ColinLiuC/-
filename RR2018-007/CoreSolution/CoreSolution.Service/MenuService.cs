
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.Service
{
    public sealed class MenuService : EfCoreRepositoryBase<Menu, MenuDto, Guid>, IMenuService
    {

     

        public Task<List<MenuDto>> GetDataMenuByName(string name)
        {

            if (!String.IsNullOrEmpty(name))
            {
                var pinfo = GetAll().Where(p => p.Name == name).FirstOrDefault();
                if (pinfo != null)
                {
                    return GetAllListAsync(p => p.ParentId == pinfo.Id);
                }
            }
            return null;
        }

        public Task<MenuDto> GetMenuById(Guid Id)
        {

            return LoadAsync(Id);

        }

        public async Task<bool> CheckIfExist(string name, Guid? parentId)
        {
            return await AnyAsync(p => p.Name == name && p.ParentId == parentId);


        }
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllMenusForTree()
        {
            string sql = "SELECT d.ChildrenNumber,c.* FROM T_Menus c LEFT JOIN " +
                         "(SELECT a.Id, COUNT(b.ParentId) ChildrenNumber FROM T_Menus a LEFT JOIN T_Menus b on a.Id = b.ParentId GROUP BY a.Id) as d " +
                         "ON c.Id = d.Id WHERE c.[IsDeleted] = 0 ";
            var cfg = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "configuration.json", ReloadOnChange = true }).Build();
            var connStr = cfg.GetSection("connStr");

            return DBHelper.ExecuteQuery(sql, connStr.Value); ;


        }

        public async Task<Tuple<int, List<MenuDto>>> GetMenusPage(Guid pId, string name, int pageIndex, int pageSize)
        {
            Expression<Func<Menu, bool>> where = PredicateExtensions.True<Menu>();

            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(p => p.Name.Contains(name));
            }
            where = where.And(p => p.ParentId == pId);
            return await GetPagedAsync(where, p => p.CreationTime, pageIndex, pageSize);

        }
        /// <summary>
        /// 通过Guid拿菜单名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetDataName(Guid Id)
        {
            var list = FirstOrDefault(Id);
            return list.Name;
        }

        public string GetItemNameById(Guid id)
        {
            var info = GetAll().Where(p => p.Id == id).FirstOrDefault();
            if (info != null)
            {
                return info.Name;
            }
            return "";
        }
    }
}

