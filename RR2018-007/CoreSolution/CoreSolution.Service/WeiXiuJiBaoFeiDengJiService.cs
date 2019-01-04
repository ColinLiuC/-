using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
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
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Service
{
    public sealed class WeiXiuJiBaoFeiDengJiService : EfCoreRepositoryBase<WeiXiuJiBaoFeiDengJi, WeiXiuJiBaoFeiDengJiDto, Guid>, IWeiXiuJiBaoFeiDengJiService
    {
        public async Task<WeiXiuJiBaoFeiDengJiDto> GetWeiXiuJiBaoFeiDengJiByFixedAssetsId(Guid id)
        {
            var query = from item1 in _dbContext.Set<WeiXiuJiBaoFeiDengJi>()
                         join item2 in _dbContext.Set<FixedAssets>() on item1.FixedAssetsId equals item2.Id into FixedAssetsList
                         from item3 in FixedAssetsList.DefaultIfEmpty()
                        where (item1.CurrentState == WeiXiuCurrentState.YiBaoFei || item1.CurrentState == WeiXiuCurrentState.ZaiWeiXiu) && item3.Id == id 
                         select new WeiXiuJiBaoFeiDengJiDto
                         {
                             Id = item1.Id,
                             FixedAssetsId = item1.FixedAssetsId,
                             Category = item1.Category,
                             HappenDate = item1.HappenDate,
                             CurrentState = item1.CurrentState,
                             FinishDate = item1.FinishDate,
                             RegisterPeople = item1.RegisterPeople,
                             RegisterDate = item1.RegisterDate,
                             Remark = item1.Remark,
                             FixedAssetsNumber = item3.Number,
                             FixedAssetsName = item3.Name
                         };
            var list = await query.ToListAsync();
            var data= list.Where(i => i.CurrentState == WeiXiuCurrentState.YiBaoFei).FirstOrDefault();
            if (data==null)
            {
                data = list.Where(i => i.CurrentState == WeiXiuCurrentState.ZaiWeiXiu).FirstOrDefault();
            }
            if (data!=null)
            {
                data.CurrentStateDescription = data.CurrentState.GetDescription();
                data.CategoryDescription = data.Category.GetDescription();
            }
            
            return data;
        }

        public async Task<WeiXiuJiBaoFeiDengJiDto> GetWeiXiuJiBaoFeiDengJiById(Guid id)
        {
            var query3 = from item1 in _dbContext.Set<WeiXiuJiBaoFeiDengJi>()
                         join item2 in _dbContext.Set<FixedAssets>() on item1.FixedAssetsId equals item2.Id into Table1
                         from item3 in Table1.DefaultIfEmpty()
                         where item1.Id == id
                         select new WeiXiuJiBaoFeiDengJiDto
                         {
                             Id = item1.Id,
                             FixedAssetsId = item1.FixedAssetsId,
                             Category = item1.Category,
                             HappenDate = item1.HappenDate,
                             CurrentState = item1.CurrentState,
                             FinishDate = item1.FinishDate,
                             RegisterPeople = item1.RegisterPeople,
                             RegisterDate = item1.RegisterDate,
                             Remark = item1.Remark,
                             FixedAssetsNumber = item3.Number,
                             FixedAssetsName = item3.Name
                         };
            var data = await query3.FirstOrDefaultAsync();
            if (data != null)
            {
                data.CurrentStateDescription = data.CurrentState.GetDescription();
                data.CategoryDescription = data.Category.GetDescription();
            }

            return data;
        }

        public async Task<List<WeiXiuJiBaoFeiDengJiDto>> GetWeiXiuJiBaoFeiDengJiPaged(Guid id)
        {
            var query = await _dbContext.Set<WeiXiuJiBaoFeiDengJi>().Where(i => i.IsDeleted == false && i.FixedAssetsId == id).OrderByDescending(i => i.CreationTime).Take(4).ToListAsync();
            var result = Mapper.Map<List<WeiXiuJiBaoFeiDengJiDto>>(query);
            result.ForEach(i => {
                i.CurrentStateDescription = i.CurrentState.GetDescription();
                i.CategoryDescription = i.Category.GetDescription();
            });
            return result;
        }
    }
}
