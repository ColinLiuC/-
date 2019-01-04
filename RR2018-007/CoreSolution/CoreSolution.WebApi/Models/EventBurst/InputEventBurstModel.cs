using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.EventBurst
{
    public class InputEventBurstModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { set; get; }
        /// <summary>
        /// 发生地址
        /// </summary>
        [Required]
        public string Address { set; get; }
        /// <summary>
        /// 发生时间
        /// </summary>
        [Required]
        public DateTime HappenTime { set; get; }
        /// <summary>
        /// 事件详情
        /// </summary>
        [Required]
        public string EventDetails { set; get; }
        /// <summary>
        /// 处置详情
        /// </summary>
        [Required]
        public string DisposeDetails { set; get; }

        /// <summary>
        /// 登记人
        /// </summary>
        [Required]
        public string RegisterUser { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        [Required]
        public DateTime RegisterTime { get; set; }

        [Required]
        public Guid StreetId { get; set; }
        [Required]
        public Guid StationId { get; set; }
        [Required]
        public Guid JuWeiId { get; set; }
        [Required]
        public Guid QuartersId { get; set; }
    }

    public class SearchEventBurstModel
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 发生地址
        /// </summary>
        public string Address { set; get; }
        /// <summary>
        /// 发生时间
        /// </summary>
        public DateTime? HappenTime_Start { set; get; }
        public DateTime? HappenTime_End { set; get; }

        public Guid? StreetId { get; set; }
        public Guid? StationId { get; set; }
        public Guid? JuWeiId { get; set; }
        public Guid? QuartersId { get; set; }

        public int pageIndex { set; get; } = 1;
        public int pageSize { set; get; } = 10;
    }
}
