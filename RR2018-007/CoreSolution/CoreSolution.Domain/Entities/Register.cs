using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Domain.Entities
{
    /// <summary>
    ///重点服务信息表
    /// </summary>
    public class Register : EntityBaseFull
    {
        /// <summary>
        /// 服务对象Id
        /// </summary>
        public Guid OldPeopleId { get; set; }
        /// <summary>
        /// 服务人员姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 服务类别
        /// </summary>
        public Guid Type { get; set; }
        /// <summary>
        /// 服务分类
        /// </summary>
        public Guid Category { get; set; }
        /// <summary>
        /// 服务周期
        /// </summary>
        public int Cycle { get; set; }
        /// <summary>
        /// 服务时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 服务预警级别
        /// </summary>
        public int level { get; set; }
        /// <summary>
        /// 登记人
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 登记时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 服务对象类别
        /// 1:老人 2:残疾人 3:优抚对象 4:就业困难人员 5:其他重点人员
        /// </summary>
        public int RegisterType { get; set; }
        ///<summary>
        ///所属街道
        /// </summary>
        public Guid Street { get; set; }
        ///<summary>
        ///所属驿站
        /// </summary>
        public Guid Station { get; set; }
        ///<summary>
        ///所属居委
        /// </summary>
        public Guid JuWei { get; set; }
    }
}
