﻿using CoreSolution.Domain.Entities;
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
    public interface IDisabilityService : IEfCoreRepository<Disability, DisabilityDto>, IServiceSupport
    {
        List<DisabilityRegisterDto> GetDisabilityInfo(DisabilityDto disabilityDto, int? sAge, int? eAge, out int total, int pageIndex = 1, int pageSize = 10, int level1 = 0, int level2 = 0, int level3 = 0);

        Task<List<MyKeyAndValue>> StatisticsByType(List<Guid> juweiids);
        Task<List<MyKeyAndValue>> StatisticsByAge(List<Guid> juweiids);

        List<MyKeyAndValue> StatisticsByMonth(List<Guid> juweiids);

        List<MyKeyAndValue> StatisticsByCategory(List<Guid> juweiids);

        List<MyJuWeiDituModel> StatisticsByJuWei(List<Guid> juweiids);
        List<Guid> GetJuWeiIds(Guid? streetid, Guid? poststationid);
    }
}