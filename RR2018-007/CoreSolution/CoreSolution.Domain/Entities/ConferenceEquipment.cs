using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 会议设备
    /// </summary>
   public  class ConferenceEquipment : EntityBaseFull
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
