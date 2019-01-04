using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ReceptionService
{
    public class SearchReceptionServiceModel
    {
        public Guid? streetId { get; set; }
        public Guid? PostStationId { get; set; }
        public string serviceName { get; set; }
        public int? category { get; set; }
    }
}
