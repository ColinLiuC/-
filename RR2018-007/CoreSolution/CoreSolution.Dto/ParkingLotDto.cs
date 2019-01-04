using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class ParkingLotDto : EntityBaseFullDto
    {
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }
        public Guid JuWeiId { get; set; }
        public string JuWeiName { get; set; }
        public Guid StationId { get; set; }
        public string StationName { get; set; }
        public Guid QuartersId { get; set; }
        public string QuartersName { get; set; }
        /// <summary>
        /// 车位总数
        /// </summary>
        public int ParkingCount { get; set; }
        /// <summary>
        /// 产权车位数
        /// </summary>
        public int? ChanQuanCount { get; set; }
        /// <summary>
        /// 公共车位数
        /// </summary>
        public int? PublicCount { get; set; }
        /// <summary>
        /// 公共充电车位数
        /// </summary>
        public int? PublicChargeCount { get; set; }
    }
}
