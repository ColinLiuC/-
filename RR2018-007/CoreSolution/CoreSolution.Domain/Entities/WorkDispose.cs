using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    /// 事项预约
    /// </summary>
    public class WorkDispose : EntityBaseFull
    {
        public Guid ResidentWorkId { get; set; }
        public string ResidentWorkName { get; set; }

        public string UserName { get; set; }
        public string IdCard { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Remarks { get; set; }
        public string StatusCode { get; set; }
        public Guid? StreetId { get; set; }
        public string StreetName { get; set; }
        public Guid? PostStationId { get; set; }
        public string PostStationName { get; set; }
        public string DisposeUser { get; set; }
        public string DisposeResult { get; set; }
        public DateTime? DisposeTime { get; set; }
        //新增字段
        public DateTime? YuYueTime { get; set; }

        public string ShiMinYunId { get; set; }
    }
}
