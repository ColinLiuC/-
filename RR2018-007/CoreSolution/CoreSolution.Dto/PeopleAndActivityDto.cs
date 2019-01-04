using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Dto.Base;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
    public class PeopleAndActivityDto : EntityDto<Guid>
    {
        //用户ID
        public Guid PeopleID { get; set; }
        //活动ID
        public Guid ActivityID { get; set; }
        //是否评论
        public Boolean IsComment { get; set; }
        /// <summary>
        /// 评价
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// 满意度
        /// </summary>
        public int Satisfaction { get; set; }

    }
}
