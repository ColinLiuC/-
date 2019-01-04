using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Models.Disability
{
    public class InputDisabilityModel
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

        /// <summary>
        ///残疾级别
        /// </summary>
        public Level? DisabilityLevel { get; set; }
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
    }
}
