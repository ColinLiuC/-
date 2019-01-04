using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;

namespace CoreSolution.AutoMapper.MapProfile
{
    class GroupMemberProfile : Profile, IProfile
    {

        public GroupMemberProfile()
        {
            CreateMap<GroupMember, GroupMemberDto>();

            CreateMap<GroupMemberDto, GroupMember>();
        }
    }
}
