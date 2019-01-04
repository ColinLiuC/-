using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.Permission
{
    /// <summary>
    /// 权限参数model
    /// </summary>
    public class InputPermissionModel
    {
        /// <summary>
        /// 权限Id，新增是不用传，修改时必传
        /// </summary>
        public Guid? PermissionId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
     
        public string DisplayName { get; set; }
        /// <summary>
        /// 对应名称
        /// </summary>
       
        public string TargetName { get; set; }
        /// <summary>
        /// 权限描述
        /// </summary>
        public string Description { get; set; }
  

    }
}
