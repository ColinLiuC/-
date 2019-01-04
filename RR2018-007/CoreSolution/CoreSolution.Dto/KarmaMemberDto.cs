using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class KarmaMemberDto : EntityBaseFullDto
    {
        /// <summary>
        /// 所属业委会
        /// </summary>
        public Guid KarmaId { get; set; }

        public string Name { get; set; }

        public string IDCard { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 居住地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 职务
        /// </summary>
        public Guid Duties { get; set; }
        public string DutiesName { get; set; }

    }
}
