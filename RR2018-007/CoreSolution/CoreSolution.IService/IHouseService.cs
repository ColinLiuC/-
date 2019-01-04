using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
    public interface IHouseService : IEfCoreRepository<House, HouseDto>, IServiceSupport
    {
        MyHouse GetHouseInfo(Guid houseid);

        void DeleteByDoorCard(Guid doorcardId);

        string GetHouseAddress(Guid houseid);
    }
}
