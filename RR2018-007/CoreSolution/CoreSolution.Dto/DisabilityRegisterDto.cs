using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
    /// <summary>
    /// 残疾人服务信息
    /// </summary>
    public class DisabilityRegisterDto
    {
        public Guid Id { get; set; }
        /// <summary>
        ///身份证号码
        /// </summary>
        public string Card { get; set; }
        /// <summary>
        ///姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///性别
        /// </summary>
        public Sex Sex { get; set; }
        /// <summary>
        ///年龄
        /// </summary>
        public int Age { get; set; }
        /// <summary>
        ///出生日期
        /// </summary>
        public DateTime BirthDay { get; set; }
        /// <summary>
        ///婚姻状态
        /// </summary>
        public Marriage Marriage { get; set; }
        /// <summary>
        ///联系电话
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        ///居住地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        ///所属街道
        /// </summary>
        public Guid Street { get; set; }
        public string StreetName { get; set; }
        /// <summary>
        ///所属驿站
        /// </summary>
        public Guid Station { get; set; }
        public string StationName { get; set; }
        /// <summary>
        ///所属居委
        /// </summary>
        public Guid JuWei { get; set; }
        public string JuWeiName { get; set; }
        /// <summary>
        ///联系人
        /// </summary>
        public string Contacts { get; set; }
        /// <summary>
        ///联系人电话
        /// </summary>
        public string ContactsPhone { get; set; }
        /// <summary>
        ///残疾类型
        /// </summary>
        public Guid DisabilityType { get; set; }
        public string DisabilityTypeName { get; set; }

        /// <summary>
        ///残疾级别
        /// </summary>
        public Level DisabilityLevel { get; set; }
        /// <summary>
        ///就业情况
        /// </summary>
        public Employment Employment { get; set; }
        /// <summary>
        ///备注
        /// </summary>
        public string Remarks { get; set; }
        /// <summary>
        ///登记人
        /// </summary>
        public string User { get; set; }
        /// <summary>
        ///登记时间
        /// </summary>
        public DateTime CreateDate { get; set; }


        /// <summary>
        ///服务对象Id
        /// </summary>
        public Guid DisabilityId { get; set; }
        /// <summary>
        /// 服务类别
        /// </summary>
        public Guid Type { get; set; }
        public string TypeName { get; set; }
        /// <summary>
        /// 服务分类
        /// </summary>
        public Guid Category { get; set; }
        public string CategoryName { get; set; }
        /// <summary>
        /// 服务周期
        /// </summary>
        public int? Cycle { get; set; }
        /// <summary>
        /// 服务时间
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 服务内容
        /// </summary>
        public string Info { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }
        /// <summary>
        /// 服务预警级别
        /// </summary>
        public int level { get; set; }


        /// <summary>
        /// 服务对象类别
        /// 1:老人 2:残疾人 3:优抚对象 4:就业困难人员 5:其他重点人员
        /// </summary>
        public int RegisterType { get; set; }

    }
}
