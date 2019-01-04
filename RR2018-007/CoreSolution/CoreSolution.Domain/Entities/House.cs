using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 房屋表
    /// </summary>
    public class House : EntityBaseFull
    {
        /// <summary>
        /// 室号
        /// </summary>
        public string HouseNumber { get; set; }
        /// <summary>
        /// 所属门牌
        /// </summary>
        public Guid DoorCardId { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public double BuildArea { get; set; }
        /// <summary>
        /// 朝向
        /// </summary>
        public Guid OrientationId { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 本市人口数量
        /// </summary>
        public int? BenShiUserCount { get; set; }
        /// <summary>
        /// 外来人口数量
        /// </summary>
        public int? WaiLaiUserCount { get; set; }
    }
}
