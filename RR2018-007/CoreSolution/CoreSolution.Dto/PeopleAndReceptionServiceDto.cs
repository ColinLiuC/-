using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class PeopleAndReceptionServiceDto: EntityDto<Guid>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid PeopleID { get; set; }
        /// <summary>
        /// 服务ID
        /// </summary>
        public Guid ReceptinServiceID { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public int Type { get; set; }
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
