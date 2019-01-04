using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;

namespace CoreSolution.IService
{
    public interface IGroupMemberService : IEfCoreRepository<GroupMember, GroupMemberDto>, IServiceSupport
    {
    }
}
