using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Dto.Base;

namespace CoreSolution.Dto
{
    public class StationDto : EntityBaseFullDto
    {
        //驿站名称
        public string StationName { get; set; }
        //简写驿站名称
        public string StationNameJX { get; set; }
        //驿站地址
        public string StationAddress { get; set; }
        //联系人
        public string StationPeople { get; set; }
        //联系电话
        public string StationTell { get; set; }
        //服务时间
        public string StationTime { get; set; }
        //服务介绍
        public string StationInfo { get; set; }
        //驿站上传文件名
        public string StationImg { get; set; }
        //上传文件地址
        public string StationSrc { get; set; }
        //所属街道ID
        public Guid StreetID { get; set; }
        //所属街道名称
        public string StreetName { get; set; }

        //驿站类型
        public string StationType { get; set; }

        //排序
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
