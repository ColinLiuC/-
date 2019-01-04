using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.WorkDispose
{
    public class InputWorkDisposeByAdminModel
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


    }
}
