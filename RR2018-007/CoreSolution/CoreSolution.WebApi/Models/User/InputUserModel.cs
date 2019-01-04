using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.User
{
    /// <summary>
    /// 用户参数model
    /// </summary>
    public class InputUserModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户真实姓名
        /// </summary>
        public string RealName { get; set; }
        /// <summary>
        /// 角色Id数组
        /// </summary>
        public Guid[] Roles { get; set; }

        public Guid? PostStationId { get; set; }
        public string PostStationName { get; set; }
        public Guid? StreetId { get; set; }
        public string StreetName { get; set; }
    }
}
