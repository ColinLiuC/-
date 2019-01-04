using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ConferenceEquipment
{
    public class OutputModel: EntityBaseFullDto
    {
        /// <summary>
        /// 设备名称
        /// </summary>
        public string EquipmentName { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 会议室Id
        /// </summary>
        public Guid ConferenceRoomId { get; set; }
    }
}
