
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
    ///菜单
    /// </summary>   
    public interface IMenuService : IEfCoreRepository<Menu, MenuDto>, IServiceSupport
    {


        Task<MenuDto> GetMenuById(Guid Id);

        Task<List<MenuDto>> GetDataMenuByName(string name);


        DataSet GetAllMenusForTree();

        Task<bool> CheckIfExist(string name, Guid? parentId);

        Task<Tuple<int, List<MenuDto>>> GetMenusPage(Guid pId, string name, int pageIndex, int pageSize);

        /// <summary>
        /// 通过Guid拿到字典名称
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        string GetDataName(Guid Id);



    }
}

