using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.Dto.MyModel;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.IService
{
    public interface IResidentWorkService : IEfCoreRepository<ResidentWork, ResidentWorkDto>, IServiceSupport
    {
        //Task<ResidentWorkPagedDto> GetResidentWorkPagedAsync(int pageIndex, int pageSize);
        //Task<ResidentWorkDto> GetResidents(string name, int? type);
        List<MyResidentWork> GetResidentWorkApp(Guid streetid, Guid? stationid, int? residentWorktype, string residentWorkname, out int total, int pageIndex, int pageSize);
    }
}
