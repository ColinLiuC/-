using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ServiceAgency
{
    public class SearchServiceAgencyModel
    {
        /// <summary>
        /// 机构名称
        /// </summary>
        public string AgencyName { get; set; }
        /// <summary>
        /// 机构类别
        /// </summary>
        public Guid? AgencyCategory { get; set; }
        /// <summary>
        /// 所属街道Id
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 所属驿站Id
        /// </summary>
        public Guid PostStationId { get; set; }
    }
}
