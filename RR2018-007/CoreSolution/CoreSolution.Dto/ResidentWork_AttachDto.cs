using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
   public class ResidentWork_AttachDto : EntityBaseFullDto
    {
        //事项Id
        public Guid ResidentWorkId { get; set; }
        //街道Id
        public Guid StreetId { get; set; }
        //驿站Id
        public Guid StationId { get; set; }
    }
}
