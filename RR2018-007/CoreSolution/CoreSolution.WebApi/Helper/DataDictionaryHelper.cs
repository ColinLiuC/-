using CoreSolution.Domain.Entities;
using CoreSolution.EntityFrameworkCore;
using log4net.Core;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace CoreSolution.WebApi.Helper
{
    public class DataDictionaryHelper
    {
        public static string GetName(string expression)
        {
            return Get(expression).Name;
        }
        public static string GetName(Guid id)
        {
            return GetById(id).Name;
        }

        public static string GetValue(string expression)
        {
            return Get(expression).Value;
        }
        public static string GetValue(Guid id)
        {
            return GetById(id).Value;
        }
        public static Guid GetId(string expression)
        {
            return Get(expression).Id;
        }
        private static DataDictionary GetDataDictionaryByName(string name)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                //var query = new Repository<DataDictionary>().TableNoTracking;
                var query = db.DataDictionarys;
                var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDeleted == false);
                if (root == null)
                {
                    throw new LogException("没有找到Root数据字典");
                }
                var dataDictionary = query.FirstOrDefault(p => p.Name == name && p.ParentId == root.Id && p.IsDeleted == false);
                if (dataDictionary != null)
                {
                    return dataDictionary;
                }
                else
                {
                    throw new COMException("没有配置数据字典:" + name);
                }
            }
        }
        public static IList<SelectListItem> GetNullSelectList(string expression, Guid? selectedValue = null, string excludedValues = "")
        {

            IList<SelectListItem> selectList = new List<SelectListItem>();
            selectList.Add(new SelectListItem { Text = "请选择", Value = "" });
            return selectList;
        }
        private static DataDictionary GetDataDictionaryByName(string parentName, string childName)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var query = db.DataDictionarys;
                var root = query.FirstOrDefault(p => p.Name == "Root" && p.ParentId == null && p.IsDeleted == false);
                if (root == null)
                {
                    throw new IOException("没有找到Root数据字典");
                }
                var dataDictionary = query.FirstOrDefault(p => p.Name == parentName && p.ParentId == root.Id && p.IsDeleted == false);
                if (dataDictionary != null)
                {
                    var childDataDictionary = query.FirstOrDefault(p => p.Name == childName && p.ParentId == dataDictionary.Id && p.IsDeleted == false);
                    if (childDataDictionary != null)
                    {
                        return childDataDictionary;
                    }
                    else
                    {
                        throw new IOException("没有该子数据字典:" + childName);
                    }
                }
                else
                {
                    throw new IOException("没有配置数据字典:" + parentName);
                }
            }

        }
        public static DataDictionary Get(string expression)
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
                        throw new IOException("表达式不正确");
                }
            }
            else
            {
                return GetDataDictionaryByName(expression);
            }
        }
        public static DataDictionary GetById(Guid id)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var query = db.DataDictionarys;
                var dataDictionary = query.FirstOrDefault(p => p.Id == id && p.IsDeleted == false);
                if (dataDictionary != null)
                {
                    return dataDictionary;
                }
                else
                {
                    throw new IOException("没有该数据字典:" + id);
                }
            }
        }
        public static IList<SelectListItem> GetSelectList(string expression, Guid? selectedValue = null, string excludedValues = "")
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var dataDictionary = Get(expression);
                var query = db.DataDictionarys;
                var dataDictionarys = query.Where(p => p.ParentId == dataDictionary.Id && p.IsDeleted == false).ToList();

                IList<SelectListItem> selectList = new List<SelectListItem>();
                selectList.Add(new SelectListItem { Text = "请选择", Value = "" });

                foreach (var item in dataDictionarys)
                {
                    if (excludedValues.Contains(item.Id.ToString()))
                    {
                        continue;
                    }
                    if (item.Id == selectedValue)
                    {
                        selectList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString(), Selected = true });
                    }
                    else
                    {
                        selectList.Add(new SelectListItem { Text = item.Name, Value = item.Id.ToString() });
                    }
                }
                return selectList;
            }

        }
        public static List<DataDictionary> GetChildren(string expression)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var query = db.DataDictionarys;
                var parentDataDictionary = new DataDictionary();

                if (expression.Contains(">"))
                {
                    var names = expression.Split(new[] { '>' }, StringSplitOptions.RemoveEmptyEntries);
                    switch (names.Length)
                    {
                        case 1:
                            parentDataDictionary = GetDataDictionaryByName(names[0]);
                            return query.Where(p => p.ParentId == parentDataDictionary.Id && p.IsDeleted == false).ToList();
                        case 2:
                            parentDataDictionary = GetDataDictionaryByName(names[0], names[1]);
                            return query.Where(p => p.ParentId == parentDataDictionary.Id && p.IsDeleted == false).ToList();
                        //case 3:
                        //    break;
                        default:
                            throw new IOException("表达式不正确");
                    }
                }
                else
                {
                    parentDataDictionary = GetDataDictionaryByName(expression);
                    return query.Where(p => p.ParentId == parentDataDictionary.Id && p.IsDeleted == false).ToList();
                }
            }
        }
    }
}
