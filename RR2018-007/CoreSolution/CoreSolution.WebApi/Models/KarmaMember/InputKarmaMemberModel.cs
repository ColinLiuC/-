using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.KarmaMember
{
    public class InputKarmaMemberModel
    {
        public Guid? Id { get; set; }

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
    }

    public class SearchKarmaMemberModel
    {
        public Guid? KarmaId { get; set; }

        public string Name { get; set; }

        public string IDCard { get; set; }

        public Guid? Duties { get; set; }
        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 10;
    }

}
