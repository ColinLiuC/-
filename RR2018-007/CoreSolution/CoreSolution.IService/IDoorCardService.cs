using CoreSolution.Domain;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
    public interface IDoorCardService : IEfCoreRepository<DoorCard, DoorCardDto>, IServiceSupport
    {
        MyDoorCard GetDoorCardInfo(Guid doorCardId);
    }
}
