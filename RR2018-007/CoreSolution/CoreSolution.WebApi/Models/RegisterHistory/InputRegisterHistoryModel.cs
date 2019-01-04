using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.RegisterHistory
{
    public class InputRegisterHistoryModel
    {

        public Guid Id { get; set; }
        //服务对象Id
        public Guid OldPeopleId { get; set; }
        //服务人员姓名
        public string Name { get; set; }
        //服务类别
        public Guid Type { get; set; }
        //服务分类
        public Guid Category { get; set; }
        //服务周期
        public int Cycle { get; set; }
        //服务时间
        public DateTime Time { get; set; }
        //服务内容
        public string Info { get; set; }
        //服务预警级别
        public int level { get; set; }
        //登记人
        public string User { get; set; }
        //登记时间
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
