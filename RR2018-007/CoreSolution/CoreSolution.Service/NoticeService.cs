using System;
using System.Collections.Generic;
using System.Text;
using System.Linq.Expressions;
using System.Security;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

namespace CoreSolution.Service
{
    public sealed class NoticeService : EfCoreRepositoryBase<Notice, NoticeDto, Guid>, INoticeService
    {
        public async Task<IList<NoticeDto>> GetNoticePagedAsync(NoticeDto noticeDto, int pageIndex, int pageSize)
        {
            //int count = await CountAsync();
            //var list = await GetAll()
            //    .OrderBy(i => i.CreationTime)
            //    .Skip((pageIndex - 1) * pageSize)
            //    .Take(pageSize)
            //    .ToListAsync();
            //var data = list.Select(i => new NoticePagedDto.NoticeListDto
            //{
            //    Id = i.Id,
            //    NoticeTitle = i.NoticeTitle,
            //    NoticeInfo = i.NoticeInfo,
            //    NoticeChannel = i.NoticeChannel,
            //    NoticePeople = i.NoticePeople,
            //    NoticeTime = i.NoticeTime,
            //    NoticeState=i.NoticeState
            //}).ToList();
            //return new NoticePagedDto { Count = count, Data = data };
            int count = await CountAsync();
            string sql = " select * from [T_Notices] where 1=1 ";
            List<SqlParameter> param = new List<SqlParameter>();
            //if (noticeDto.NoticeTitle != "")
            //{
            //    param.Add(new SqlParameter("@NoticeTitle", noticeDto.NoticeTitle));
            //}
            if (!string.IsNullOrEmpty( noticeDto.NoticeTitle))
            {
                sql += " and [NoticeTitle] like '%" + noticeDto.NoticeTitle + "%' ";
            }
            //if (noticeDto.NoticeState !=null)
            //{
            //    sql += " and [NoticeState] = " + noticeDto.NoticeState;
            //}
            var info =  GetPaged(out count, sql, "Id", pageIndex, pageSize, param.ToArray());
            return info;
        }








    }
}
