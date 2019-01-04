using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.DataDictionary
{
    public class TreeNode
    {
        public TreeNode()
        {
            showAdd = true;
            showEdit = true;
            showRemove = true;
        }
        public string id { get; set; }
        public string name { get; set; }
        public string parentid { get; set; }
        public string check { get; set; }
        public string open { get; set; }
        /// <summary>
        /// 提供对节点的新增权限的控制
        /// </summary>
        public bool showAdd { get; set; }
        /// <summary>
        /// 提供对节点的修改权限的控制
        /// </summary>
        public bool showEdit { get; set; }
        /// <summary>
        /// 提供对节点的删除权限的控制
        /// </summary>
        public bool showRemove { get; set; }
        public string isParent { get; set; }
        /// <summary>
        /// 字体 
        /// font="{'color':'#ccc'}"
        /// </summary>
        public string font { get; set; }
        /// <summary>
        /// 自定义参数，可以在TreeData方法中通过Request[""]获取 
        /// nodeParam ="type=1&key=value";
        /// </summary>
        public string nodeParam { get; set; }
        public string iconOpen { get; set; }
        public string iconClose { get; set; }
        public string icon { get; set; }

        public string PermissionId { get; set; }
    }
}
