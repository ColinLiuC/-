using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ParkingLot
{
    public class InputParkingLotModel
    {
        public Guid? Id { get; set; }

        public Guid StreetId { get; set; }
        public string StreetName { get; set; }
        public Guid JuWeiId { get; set; }
        public string JuWeiName { get; set; }
        public Guid QuartersId { get; set; }
        public string QuartersName { get; set; }

        public Guid StationId { get; set; }
        public string StationName { get; set; }
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


    public class SearchParkingLotModel
    {
        public Guid? StreetId { get; set; }
        public Guid? StationId { get; set; }
        public Guid? JuWeiId { get; set; }
        public Guid? QuartersId { get; set; }
        public int? ParkingCount_Start { get; set; }
        public int? ParkingCount_End { get; set; }
        public int? ChanQuanCount_Start { get; set; }
        public int? ChanQuanCount_End { get; set; }
        public int? PublicCount_Start { get; set; }
        public int? PublicCount_End { get; set; }

        public int? PublicChargeCount_Start { get; set; }
        public int? PublicChargeCount_End { get; set; }

        public int pageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
    }

}
