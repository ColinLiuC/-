using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ResourceOrg
{
    /// <summary>
    /// 场所资源信息
    /// </summary>
    public class OutputResourceOrgModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public Guid ResourceCategory { get; set; }
        public string ResourceCategoryName { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid Street { get; set; }
        public string StreetName { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid Station { get; set; }
        public string StationName { get; set; }
        /// <summary>
        /// 地址描述
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 坐标:X轴
        /// </summary>
        public double Xaxis { get; set; }
        /// <summary>
        /// 坐标:Y轴
        /// </summary>
        public double Yaxis { get; set; }
        /// <summary>
        ///备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        ///登记人
        /// </summary>
        public string User { get; set; }
        /// <summary>
        ///登记时间
        /// </summary>
        public DateTime CreateDate { get; set; }

    }
}
