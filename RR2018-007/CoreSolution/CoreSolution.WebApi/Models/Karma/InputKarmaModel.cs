using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.Karma
{
    public class InputKarmaModel
    {
       // public Guid? Id { get; set; }
        public string Name { get; set; }
        public string ContactUser { get; set; }
        public string ContactTel { get; set; }
        public string Address { get; set; }
        public Guid StationId { get; set; }
        public Guid StreetId { get; set; }
        public Guid JuWeiId { get; set; }
        public Guid QuartersId { get; set; }
        public Guid RenQiId { get; set; }
        public string Remarks { get; set; }
    }

    public class SearchKarmaModel
    {
        public string Name { get; set; }
        public Guid? StreetId { get; set; }
        public Guid? StationId { get; set; }

        public Guid? JuWeiId { get; set; }
        public Guid? QuartersId { get; set; }
        public Guid? RenQiId { get; set; }
        public int pageIndex { get; set; } = 1;

        public int pageSize { get; set; } = 10;

    }
}
