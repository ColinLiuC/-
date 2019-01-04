using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models
{
    public class InputWorkDisposeModel
    {
        /// <summary>
        /// 事项ID
        /// </summary>
        public Guid ResidentWorkId { get; set; }

        /// <summary>
        /// 事项名称
        /// </summary>
        public string ResidentWorkName { get; set; }
        /// <summary>
        /// 预约用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 居住地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        /// 当前状态
        /// </summary>
        public string StatusCode { get; set; }
        /// <summary>
        /// 所属街道Id
        /// </summary>
        public Guid? StreetId { get; set; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 所属驿站Id
        /// </summary>
        public Guid? PostStationId { get; set; }
        /// <summary>
        /// 所属驿站名称
        /// </summary>
        public string PostStationName { get; set; }

        /// <summary>
        /// 提交日期
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        ///预约时间
        /// </summary>
        public DateTime? YuYueTime { get; set; }
        public string ShiMinYunId { get; set; }
    }
}
