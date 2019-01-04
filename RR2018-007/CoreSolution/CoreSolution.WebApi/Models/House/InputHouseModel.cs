using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.House
{
    public class InputHouseModel
    {
        public Guid? Id { get; set; }

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
        /// 查询地址
        /// </summary>
        public string Address { get; set; }

    }
}
