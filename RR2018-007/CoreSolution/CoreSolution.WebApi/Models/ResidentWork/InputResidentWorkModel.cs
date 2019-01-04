using CoreSolution.Domain.Enum;
using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Models.ResidentWork
{

    public class InputResidentWorkModel
    {
        /// <summary>
        /// 事项名称
        /// </summary>
        public string ResidentWorkName { get; set; }
        /// <summary>
        /// 事项分类
        /// </summary>
        public int ResidentWorkType { get; set; }
        /// <summary>
        /// 办事流程
        /// </summary>
        public string ResidentWorkFlow { get; set; }


        /// <summary>
        /// 办事流程图-名称
        /// </summary>
        public string ResidentWorkFlowImg { get; set; }

        /// <summary>
        /// 办事流程图-路径
        /// </summary>
        public string ResidentWorkFlowImgPaths { get; set; }

        /// <summary>
        /// 相关政策
        /// </summary>
        public string RelevantPolicies { get; set; }
        /// <summary>
        /// 行政依据
        /// </summary>
        public string AdministrativeBasis { get; set; }
        /// <summary>
        /// 申请条件
        /// </summary>
        public string Requirement { get; set; }
        /// <summary>
        /// 提交材料
        /// </summary>
        public string Material { get; set; }
        /// <summary>
        /// 收费标准
        /// </summary>
        public float Charge { get; set; }
        /// <summary>
        /// 办理期限
        /// </summary>
        public string Deadline { get; set; }
        /// <summary>
        /// 是否发布（1 是 2 否）
        /// </summary>
        public bool IsPublish { get; set; } = false;
        
        //街道Id
        public Guid StreetId { get; set; }

        //驿站Id
        public string StationIds { get; set; }

        public string StationNames { get; set; }

    }
}
