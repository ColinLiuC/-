using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.WorkTransact
{
    public class OutputWorkTransactModel
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
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
        /// 当前状态
        /// </summary>
        public string StatusCode { get; set; }

        public string Remarks { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime CreationTime { get; set; }
    }
}
