using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class WorkTransactDto : EntityBaseFullDto
    {
        public Guid ResidentWorkId { get; set; }
        public string ResidentWorkName { get; set; }
        public string UserName { get; set; }
        public string IdCard { get; set; }
        public Guid PeopleID { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Remarks { get; set; }

        public string StatusCode { get; set; }

        public string ShouliUser { get; set; }
        public string ShouliContent { get; set; }
        public string ShouliAddress { get; set; }
        public DateTime? ShouliTime { get; set; }

        public string DisposeUser { get; set; }
        public string DisposeResult { get; set; }
        public DateTime? DisposeTime { get; set; }

        public Guid? StreetId { get; set; }
        public Guid? StationId { get; set; }
    }
}
