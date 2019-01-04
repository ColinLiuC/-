using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.EventBurst
{
    public class OutputEventBurstModel
    {
        public Guid Id { set; get; }

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
        public DateTime HappenTime { set; get; }
        /// <summary>
        /// 事件详情
        /// </summary>
        public string EventDetails { set; get; }
        /// <summary>
        /// 处置详情
        /// </summary>
        public string DisposeDetails { set; get; }

        /// <summary>
        /// 登记人
        /// </summary>
        public string RegisterUser { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime RegisterTime { get; set; }

     
    }
}
