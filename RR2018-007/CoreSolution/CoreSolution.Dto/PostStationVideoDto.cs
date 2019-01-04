using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class PostStationVideoDto: EntityBaseFullDto
    {
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }
        public Guid PostStationId { get; set; }
        public string PostStationName { get; set; }
        public string ViedoImgPath { get; set; }
        public string ViedoPath { get; set; }
        public string ViedoName { get; set; }
    }
}
