
using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class MapOverlaysDto : EntityBaseFullDto
    {
        public string MapCenter { get; set; }
        public string Name { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        public int Type { get; set; }

    }
}
