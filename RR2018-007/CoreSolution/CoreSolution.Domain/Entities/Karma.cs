using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 业务会
    /// </summary>
    public class Karma : EntityBaseFull
    {
        public string Name { get; set; }
        public string ContactUser { get; set; }
        public string ContactTel { get; set; }
        public string Address { get; set; }
        public Guid StreetId { get; set; }

        public Guid StationId { get; set; }

        public Guid JuWeiId { get; set; }
        public Guid QuartersId { get; set; }
        public Guid RenQiId { get; set; }
        public string Remarks { get; set; }
        public string Test { get; set; }

    }
}
