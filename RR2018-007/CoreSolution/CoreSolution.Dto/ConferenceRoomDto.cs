using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class ConferenceRoomDto: EntityBaseFullDto
    {
        /// <summary>
        /// 会议活动室名称
        /// </summary>
        public string ConferenceRoomName { get; set; }
        /// <summary>
        /// 会议活动室类型
        /// </summary>
        public Guid ConferenceRoomType { get; set; }
        /// <summary>
        /// 会议活动室类型名称
        /// </summary>
        public string RoomTypeName { get; set; }
        /// <summary>
        /// 主管单位
        /// </summary>
        public string CompetentUnit { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 所属街道名称
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 所属驿站名称
        /// </summary>
        public string PostStationName { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid PostStation { get; set; }     
        /// <summary>
        /// 负责人
        /// </summary>
        public string PersonInCharge { get; set; }
        /// <summary>
        /// 负责人联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 座位数
        /// </summary>
        public int Pedestal { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 详细描述
        /// </summary>
        public string DetailedDescr { get; set; }
        /// <summary>
        /// 内部布局图片名称
        /// </summary>
        public string ImgName { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgPath { get; set; }
        /// <summary>
        /// 状态 1-开放使用 2-暂不开放 3-需特殊申请
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 会议/活动室状态 
        /// </summary>
        public Guid ConferenceRoomState { get; set; }
        /// <summary>
        /// 是否收费
        /// </summary>
        public bool IsCharge { get; set; }
    }
}
