using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;

namespace CoreSolution.IService
{
    public interface INoticeService : IEfCoreRepository<Notice, NoticeDto>, IServiceSupport
    {
        Task<IList<NoticeDto>> GetNoticePagedAsync(NoticeDto noticeDto, int pageIndex, int pageSize);

    }
}
