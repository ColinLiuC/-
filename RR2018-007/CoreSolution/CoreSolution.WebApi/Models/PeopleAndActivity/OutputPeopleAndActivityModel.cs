using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.Dto;
using CoreSolution.Dto.Base;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Models.PeopleAndActivity
{
    /// <summary>
    /// 用户-活动表
    /// </summary>
    public class OutputPeopleAndActivityModel : EntityDto<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid PeopleID { get; set; }
        /// <summary>
        /// 活动ID
        /// </summary>
        public Guid ActivityID { get; set; }
        /// <summary>
        /// 是否评论
        /// </summary>
        public Boolean IsComment { get; set; }

    }
}
