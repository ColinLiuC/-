using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class StreetDto : EntityBaseFullDto
    {

        //街道名称
        public string StreetName { get; set; }
        //街道地址
        public string StreetAddress { get; set; }
        //联系人
        public string StreetPeople { get; set; }
        //联系电话
        public string StreetTell { get; set; }
        //介绍
        public string StreetInfo { get; set; }

        //街道图片
        public string StreetImg { get; set; }

        //街道图片路径
        public string StreetPaths { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public double? Lat { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public double? Lng { get; set; }
        //预留字段
        public string StreetAttr1 { get; set; }
        public string StreetAttr2 { get; set; }
        public string StreetAttr3 { get; set; }
    }
}
