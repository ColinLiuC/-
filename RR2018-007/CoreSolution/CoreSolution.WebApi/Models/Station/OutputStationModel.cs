using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.Dto.Base;

namespace CoreSolution.WebApi.Models.Station
{
    public class OutputStationModel : EntityBaseFullDto
    {
        /// <summary>
        /// 驿站名称
        /// </summary>
        public string StationName { get; set; }
        //简写驿站名称
        public string StationNameJX { get; set; }
        /// <summary>
        /// 驿站地址
        /// </summary>
        public string StationAddress { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string StationPeople { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string StationTell { get; set; }
        /// <summary>
        /// 服务时间
        /// </summary>
        public string StationTime { get; set; }
        /// <summary>
        /// 服务介绍
        /// </summary>
        public string StationInfo { get; set; }
        /// <summary>
        /// 驿站图片
        /// </summary>
        public string StationImg { get; set; }
        ///<summary>
        ///上传文件地址
        /// </summary>
        public string StationSrc { get; set; }
        ///<summary>
        ///所属街道ID
        /// </summary>
        public Guid StreetID { get; set; }
        ///<summary>
        ///所属街道名称
        /// </summary>
        public string StreetName { get; set; }
        ///<summary>
        ///驿站类型
        /// </summary>
        public string StationType { get; set; }
        ///<summary>
        ///排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 坐标:经度
        /// </summary>
        public double? Lat { get; set; }
        /// <summary>
        /// 坐标:纬度
        /// </summary>
        public double? Lng { get; set; }
    }
}
