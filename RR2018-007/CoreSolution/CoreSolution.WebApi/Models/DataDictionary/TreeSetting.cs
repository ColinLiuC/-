using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.DataDictionary
{
    public class TreeSetting
    {

        public TreeSetting()
        {
            ShowAdd = true;
            ShowEdit = true;
            ShowDelete = true;
            ShowExpandAll = true;
            ShowCloseAll = true;
            ShowAddRoot = false;
            ShowCheck = true;
            ShowSearch = true;
            ShowTreeData = true;
            IsAsync = true;
            ShowParentTreeData = false;
        }




        /// <summary>
        /// 树名称：功能简短描述
        /// </summary>
        public string TreeName { get; set; }




        /// <summary>
        /// 是否异步加载数据:false的时候 树数据需要一次获取 ，true:数据通过点击展开节点按钮分批加载,异步模式下无法使用:展开，折叠，搜索
        /// </summary>
        public bool IsAsync { get; set; }

        /// <summary>
        /// 是否显示新曾结点
        /// </summary>
        public bool ShowAdd { get; set; }

        /// <summary>
        /// 是否显示修改借点
        /// </summary>
        public bool ShowEdit { get; set; }

        /// <summary>
        /// 是否显示删除按钮
        /// </summary>
        public bool ShowDelete { get; set; }

        /// <summary>
        /// 是否显示展开全部,此按钮只在IsAsync=false 时有效
        /// </summary>
        public bool ShowExpandAll { get; set; }

        /// <summary>
        /// 是否显示折叠全部
        /// </summary>
        public bool ShowCloseAll { get; set; }


        /// <summary>
        /// 是否显示添加跟结点
        /// </summary>
        public bool ShowAddRoot { get; set; }

        /// <summary>
        /// 是否显示checkbox
        /// </summary>
        public bool ShowCheck { get; set; }

        /// <summary>
        /// 是否显示搜索按钮
        /// </summary>
        public bool ShowSearch { get; set; }

        /// <summary>
        /// 点击tree节点是否允许跳转到Controller: TreeData
        /// </summary>
        public bool ShowTreeData { get; set; }




        /// <summary>
        /// 点击根节点是否允许跳转到Controller: TreeData
        /// </summary>
        public bool ShowRootTreeData { get; set; }


        /// <summary>
        /// 点击父节点是否允许跳转到Controller: TreeData
        /// </summary>
        public bool ShowParentTreeData { get; set; }


    }
}
