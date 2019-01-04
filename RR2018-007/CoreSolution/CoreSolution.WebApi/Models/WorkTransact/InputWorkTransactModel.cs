using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.WorkTransact
{
    /// <summary>
    /// 事项办理
    /// </summary>
    public class InputWorkTransactModel
    {     
        public Guid ResidentWorkId { get; set; }
        public string ResidentWorkName { get; set; }
        public string UserName { get; set; }
        public string IdCard { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Remarks { get; set; }

        public string ShouliUser { get; set; }
        public string ShouliContent { get; set; }
        public string ShouliAddress { get; set; }
        public DateTime? ShouliTime { get; set; }

        public Guid? StreetId { get; set; }
        public Guid? StationId { get; set; }

    }
}
