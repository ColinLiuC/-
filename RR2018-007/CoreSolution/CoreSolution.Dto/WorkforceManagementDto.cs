using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
    /// <summary>
    /// 排班管理 1
    /// </summary>
    public class WorkforceManagementDto : EntityBaseFullDto
    {
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        /// <summary>
        /// 所属驿站
        /// </summary>
        public Guid StationId { get; set; }
        /// <summary>
        /// 年
        /// </summary>
        public string WorkforceYear { get; set; }
        /// <summary>
        /// 月
        /// </summary>
        public string WorkforceMonth { get; set; }
        /// <summary>
        /// 日
        /// </summary>
        public string WorkforceDay { get; set; }
        /// <summary>
        /// 周
        /// </summary>
        public string WorkforceWeek { get; set; }
        /// <summary>
        /// 上午1，中午2，下午3
        /// </summary>
        public int DayState { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string BeginTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string EndTime { get; set; }
        /// <summary>
        /// 值班人员/组(第一组 张三)
        /// </summary>
        public Guid PeopleGroupId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 操作人
        /// </summary>
        public string RegisterPeople { get; set; }
        /// <summary>
        ///  操作日期
        /// </summary>
        public DateTime RegisterDate { get; set; }

        public string GroupName { get; set; }

        public string DutyPeople { get; set; }

        public GroupCateGory GroupCateGory { get; set; }
        public string GroupCateGoryName { get; set; }
        

        public int ObjectIsNull { get; set; }
    }
}
