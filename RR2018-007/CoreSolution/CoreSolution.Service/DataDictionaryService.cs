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
    public sealed class DataDictionaryService : EfCoreRepositoryBase<DataDictionary, DataDictionaryDto, Guid>, IDataDictionaryService
    {
        public Task<List<DataDictionaryDto>> GetDataDictionaryByExpression(string expression)
        {
            if (expression.Contains(">"))
            {
                var names = expression.Split(new[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                switch (names.Length)
                {
                    case 1:
                        return GetDataDictionaryByName(names[0]);
                    case 2:
                        return GetDataDictionaryByName(names[0], names[1]);
                    //case 3:
                    //    break;
                    default:
                        throw new Exception("表达式不正确");
                }
            }
            else
            {
                return GetDataDictionaryByName(expression);
            }
        }

        public Task<List<DataDictionaryDto>> GetDataDictionaryByName(string parentName, string childName)
        {

            var root = SingleOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDeleted == false);
            if (root == null)
            {
                throw new Exception("没有找到Root数据字典");
            }
            var dataDictionary = SingleOrDefault(p => p.Name == parentName && p.ParentId == root.Id && p.IsDeleted == false);
            if (dataDictionary != null)
            {
                var childDataDictionary = GetAllListAsync(p => p.Name == childName && p.ParentId == dataDictionary.Id && p.IsDeleted == false);
                if (childDataDictionary != null)
                {
                    return childDataDictionary;
                }
                else
                {
                    throw new Exception("没有该子数据字典:" + childName);
                }
            }
            else
            {
                throw new Exception("没有配置数据字典:" + parentName);
            }
        }




        public Task<List<DataDictionaryDto>> GetDataDictionaryByName(string name)
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

        public Task<DataDictionaryDto> GetDataDictionaryById(Guid Id)
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
        public DataSet GetAllDataDictionarysForTree()
        {
            string sql = "SELECT d.ChildrenNumber,c.* FROM T_DataDictionaries c LEFT JOIN " +
                         "(SELECT a.Id, COUNT(b.ParentId) ChildrenNumber FROM T_DataDictionaries a LEFT JOIN T_DataDictionaries b on a.Id = b.ParentId GROUP BY a.Id) as d " +
                         "ON c.Id = d.Id WHERE c.[IsDeleted] = 0";
            var cfg = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "configuration.json", ReloadOnChange = true }).Build();
            var connStr = cfg.GetSection("connStr");

            return DBHelper.ExecuteQuery(sql, connStr.Value); ;
           

        }

        public async Task<Tuple<int, List<DataDictionaryDto>>> GetDataDictionaryPage(Guid pId, string name, int pageIndex, int pageSize)
        {
            Expression<Func<DataDictionary, bool>> where = PredicateExtensions.True<DataDictionary>();

            if (!string.IsNullOrEmpty(name))
            {
                where = where.And(p => p.Name.Contains(name));
            }
            where = where.And(p => p.ParentId == pId);
            return await GetPagedAsync(where, p => p.CreationTime, pageIndex, pageSize);

        }
        /// <summary>
        /// 通过Guid拿到字典名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public string GetDataName(Guid Id) {
            var list = FirstOrDefault(Id);
            return list.Name;
        }

        public string GetItemNameById(Guid id)
        {
            var info = GetAll().Where(p => p.Id == id).FirstOrDefault();
            if (info!=null)
            {
                return info.Name;
            }
            return "";
        }
    }
}
