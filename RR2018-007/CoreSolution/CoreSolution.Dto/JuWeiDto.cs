using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class JuWeiDto : EntityBaseFullDto
    {

        public string Name { get; set; }
        public string Address { get; set; }

        public Guid StreetId { get; set; }
        public string StreetName { get; set; }

        public Guid PostStationId { get; set; }
        public string PostStationName { get; set; }

        public string JuWeiPeople { get; set; }

        //联系电话
        public string Phone { get; set; }

        //介绍
        public string Introduce { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double? Lat { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double? Lng { get; set; }
    }
}
