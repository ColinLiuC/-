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
    public sealed class RegistrationOfUseService : EfCoreRepositoryBase<RegistrationOfUse, RegistrationOfUseDto, Guid>, IRegistrationOfUseService
    {
        public async Task<RegistrationOfUseDto> GetRegistrationOfUseByFixedAssetsId(Guid id)
        {
           
            var query3 = from item1 in _dbContext.Set<RegistrationOfUse>()
                         join item2 in _dbContext.Set<FixedAssets>() on item1.FixedAssetsId equals item2.Id into Table1
                         from item3 in Table1.DefaultIfEmpty()
                         where item1.CurrentState== UseCurrentState.WeiGuiHuan &&
                         item3.Id==id
                         select new RegistrationOfUseDto
                         {
                             Id = item1.Id,
                             FixedAssetsId = item1.FixedAssetsId,
                             ReceivePeople = item1.ReceivePeople,
                             ReceiveDate = item1.ReceiveDate,
                             PredictReturnDate = item1.PredictReturnDate,
                             CurrentState = item1.CurrentState,
                             ReturnDate = item1.ReturnDate,
                             RegisterPeople = item1.RegisterPeople,
                             RegisterDate = item1.RegisterDate,
                             Remark = item1.Remark,
                             FixedAssetsNumber = item3.Number,
                             FixedAssetsName = item3.Name
                         };
            var data = await query3.FirstOrDefaultAsync();
            if (data!=null)
            {
                data.CurrentStateDescription = data.CurrentState.GetDescription();
            }
            return data;
        }

        public async Task<RegistrationOfUseDto> GetRegistrationOfUseById(Guid id)
        {
            var query3 = from item1 in _dbContext.Set<RegistrationOfUse>()
                         join item2 in _dbContext.Set<FixedAssets>() on item1.FixedAssetsId equals item2.Id into Table1
                         from item3 in Table1.DefaultIfEmpty()
                         where item1.Id == id
                         select new RegistrationOfUseDto
                         {
                             Id = item1.Id,
                             FixedAssetsId = item1.FixedAssetsId,
                             ReceivePeople = item1.ReceivePeople,
                             ReceiveDate = item1.ReceiveDate,
                             PredictReturnDate = item1.PredictReturnDate,
                             CurrentState = item1.CurrentState,
                             ReturnDate = item1.ReturnDate,
                             RegisterPeople = item1.RegisterPeople,
                             RegisterDate = item1.RegisterDate,
                             Remark = item1.Remark,
                             FixedAssetsNumber = item3.Number,
                             FixedAssetsName = item3.Name
                         };
            var data= await query3.FirstOrDefaultAsync();
            data.CurrentStateDescription = data.CurrentState.GetDescription();
            return data;
        }

        public async Task<List<RegistrationOfUseDto>> GetRegistrationOfUsePaged(Guid id)
        {
            var query = await _dbContext.Set<RegistrationOfUse>().Where(i => i.IsDeleted == false && i.FixedAssetsId == id).OrderByDescending(i => i.CreationTime).Take(4).ToListAsync();
            var result = Mapper.Map<List<RegistrationOfUseDto>>(query);
            result.ForEach(i => i.CurrentStateDescription = i.CurrentState.GetDescription());
            return result;
        }
    }
}
