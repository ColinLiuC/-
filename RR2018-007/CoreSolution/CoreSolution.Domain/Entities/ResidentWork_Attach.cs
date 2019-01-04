using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 事项信息（附表）
    /// </summary>
    public class ResidentWork_Attach : EntityBaseFull
    {
        //事项Id
        public Guid ResidentWorkId { get; set; }
        //街道Id
        public Guid StreetId { get; set; }
        //驿站Id
        public Guid StationId { get; set; }

    }
}
