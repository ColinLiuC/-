using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class EventBurstDto : EntityBaseFullDto
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

        public Guid StreetId { get; set; }
        public string StreetName { get; set; }

        public Guid StationId { get; set; }
        public string StationName { get; set; }

        public Guid JuWeiId { get; set; }
        public string JuWeiName { get; set; }
        public Guid QuartersId { get; set; }
        public string QuartersName { get; set; }
    }
}
