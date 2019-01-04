using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.IService
{
    /// <summary>
    ///数据字典
    /// </summary>   
    public interface IDataDictionaryService : IEfCoreRepository<DataDictionary, DataDictionaryDto>, IServiceSupport
    {
        

        Task<DataDictionaryDto> GetDataDictionaryById(Guid Id);

        Task<List<DataDictionaryDto>> GetDataDictionaryByName(string name);
        Task<List<DataDictionaryDto>> GetDataDictionaryByName(string parentName, string childName);
        Task<List<DataDictionaryDto>> GetDataDictionaryByExpression(string expression);
        DataSet GetAllDataDictionarysForTree();

        Task<bool> CheckIfExist(string name, Guid? parentId);

        Task<Tuple<int, List<DataDictionaryDto>>> GetDataDictionaryPage(Guid pId, string name, int pageIndex, int pageSize);
        
        /// <summary>
        /// 通过Guid拿到字典名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string GetDataName(Guid Id);
        

        string GetItemNameById(Guid id);



        }
}
