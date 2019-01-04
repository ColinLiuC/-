using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.IService
{
   public interface IServiceApplication: IEfCoreRepository<Domain.Entities.ServiceApplication, Dto.ServiceApplicationDto>, IServiceSupport
    {
        ApplicationInformationDto GetApplicationInformationDataById(Guid id);
        IList<ApplicationInformationDto> GetServiceResult(ServiceResultDto srt,int pageIndex,int pageSize,out int total);
        int GetCount(Guid? streetId, Guid? postStationId, int applicationSource);
        int GetApplicationCount(Guid? streetId, Guid? postStationId);
        int GetServiceResultCount(Guid? streetId, Guid? postStationId);
        Guid GetServiceType(Guid? streetId, Guid? postStationId, int applicationCource);
    }
}
