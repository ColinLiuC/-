using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ResourceOrg
{
    /// <summary>
    /// 组织资源信息
    /// </summary>
    public class InputResourceOrgModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        [Required]
        public Guid ResourceCategory { get; set; }
        public string ResourceCategoryName { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        [Required]
        public Guid Street { get; set; }
        public string StreetName { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        [Required]
        public Guid Station { get; set; }
        public string StationName { get; set; }
        /// <summary>
        /// 地址描述
        /// </summary>
        [Required]
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
        [Required]
        public string User { get; set; }
        /// <summary>
        ///登记时间
        /// </summary>
        [Required]
        public DateTime CreateDate { get; set; }

    }






}
