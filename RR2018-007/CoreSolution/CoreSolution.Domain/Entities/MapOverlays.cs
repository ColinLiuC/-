
using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 数据字典
    /// </summary>
    public class MapOverlays : EntityBaseFull
    {
        public string MapCenter { get; set; }
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int Type { get; set; }
       
    }
   
}
