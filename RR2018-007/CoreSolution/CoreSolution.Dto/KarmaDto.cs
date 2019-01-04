using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class KarmaDto : EntityBaseFullDto
    {
        public string Name { get; set; }
        public string ContactUser { get; set; }
        public string ContactTel { get; set; }
        public string Address { get; set; }
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }
        
        public Guid JuWeiId { get; set; }
        public string JuWeiName { get; set; }


        public Guid StationId { get; set; }
        public string StationName { get; set; }

        public Guid QuartersId { get; set; }
        public string QuartersName { get; set; }

        public Guid RenQiId { get; set; }
        public string RenQiName { get; set; }

        public string Remarks { get; set; }
    }
}
