using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.Quarters
{
    public class OutputQuartersModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }


        /// <summary>
        /// 所属居委
        /// </summary>
        public Guid JuWeiId { get; set; }
        public string JuWeiName { get; set; }
        /// <summary>
        /// 竣工年份
        /// </summary>
        public string CompletedYear { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildArea { get; set; }

        /// <summary>
        /// 总人口数
        /// </summary>
        public int UserCount { get; set; }

        /// <summary>
        /// 户籍人数
        /// </summary>
        public int? HujiCount { get; set; }

        /// <summary>
        /// 居住人数
        /// </summary>
        public int? JuZhuCount { get; set; }

        /// <summary>
        /// 本市人口数量
        /// </summary>
        public int? CityUserCount { get; set; }

        /// <summary>
        /// 本市人口男性数量
        /// </summary>
        public int? CityManUserCount { get; set; }
        /// <summary>
        /// 本市人口男性数量
        /// </summary>
        public int? CityGirlUserCount { get; set; }

        /// <summary>
        /// 外来人口数量
        /// </summary>
        public int? ForeignUserCount { get; set; }


        /// <summary>
        /// 外来人口男性数量
        /// </summary>
        public int? ForeignManUserCount { get; set; }

        /// <summary>
        /// 外来人口女性数量
        /// </summary>
        public int? ForeignGirlUserCount { get; set; }

        public Guid StationId { get; set; }
        public string StationName { get; set; }
    }
}
