using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class DoorCardDto : EntityBaseFullDto
    {
        /// <summary>
        /// 门牌号
        /// </summary>
        public int DoorCardNumber { get; set; }
        /// <summary>
        /// 所属楼栋
        /// </summary>
        public Guid BuildingId { get; set; }
        /// <summary>
        /// 负责人
        /// </summary>
        public string ChargeUser { get; set; }
        /// <summary>
        /// 党代表
        /// </summary>
        public string DangDaiBiaoUser { get; set; }
        /// <summary>
        /// 楼组长
        /// </summary>
        public string BuildLeaderUser { get; set; }

        /// <summary>
        /// 居民小组长
        /// </summary>
        public string JuMingLeaderUser { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
    }
}
