using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.House
{
    public class OutputHouseModel
    {
        public Guid Id { get; set; }
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
        public double? BuildArea { get; set; }
        /// <summary>
        /// 朝向
        /// </summary>
        public Guid? OrientationId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 本市人口数目
        /// </summary>
        public int? BenShiUserCount { get; set; } = 0;

        /// <summary>
        /// 外来人口数目
        /// </summary>
        public int? WaiLaiUserCount { get; set; } = 0;
    }
}
