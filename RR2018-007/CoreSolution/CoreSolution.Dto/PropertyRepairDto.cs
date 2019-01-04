using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
    public class PropertyRepairDto : EntityBaseFullDto
    {
        /// <summary>
        /// 报修时间
        /// </summary>
        public DateTime RepairTime { get; set; }
        /// <summary>
        /// 报修事项
        /// </summary>
        public string RepairMatter { get; set; }
        /// <summary>
        /// 报修地点
        /// </summary>
        public string RepairAddress { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactUser { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactTel { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }

        /// <summary>
        /// 所属居委
        /// </summary>
        public Guid JuWeiId { get; set; }
        public string JuWeiName { get; set; }

        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid StationId { get; set; }
        public string StationName { get; set; }

        /// <summary>
        /// 所属小区
        /// </summary>
        public Guid QuartersId { get; set; }
        public string QuartersName { get; set; }

        /// <summary>
        /// 所属物业
        /// </summary>
        public Guid PropertyId { get; set; }

        public string PropertyName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string RegisterUser { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime RegisterTime { get; set; }

        /// <summary>
        /// 当前状态
        /// </summary>
        public ProStatusCode StatusCode { get; set; }
        public string StatusCodeStr { get; set; }


        /// <summary>
        /// 处理结果
        /// </summary>
        public string DisposeResult { get; set; }
        /// <summary>
        /// 处理人
        /// </summary>
        public string DisposeUser { get; set; }
        /// <summary>
        /// 处理日期
        /// </summary>
        public DateTime? DisposeTime { get; set; }
    }
}
