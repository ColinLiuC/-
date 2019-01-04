using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
    public interface IResidentWork_AttachService: IEfCoreRepository<ResidentWork_Attach, ResidentWork_AttachDto>, IServiceSupport
    {
        bool DeleteByResidentWorkId(Guid residentWorkId, Guid? streetid = null);

        List<Guid> GetResidentWorkByStreet(Guid residentWorkId, Guid? streetid);

        List<MyStationDto> GetMyStation(Guid residentWorkId, Guid streetid);
    }
}
