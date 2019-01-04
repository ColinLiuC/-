using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CoreSolution.WebApi.Models.House
{

    /// <summary>
    /// 房屋人员模型类
    /// </summary>

    [XmlType("Person")]
    public class Person
    {
        /// <summary>
        /// 身份证
        /// </summary>
        public string ID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        public string BirthDay { get; set; }
        /// <summary>
        /// 户籍地址
        /// </summary>
        public string RegAddr { get; set; }
        /// <summary>
        /// 居住地址
        /// </summary>
        public string InAddr { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }
        /// <summary>
        /// 人员类型
        /// </summary>
        public string PersonType { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string Nation { get; set; }
        /// <summary>
        /// 户籍居委会
        /// </summary>
        public string RegJuweiName { get; set; }
        /// <summary>
        /// 居住居委会
        /// </summary>
        public string InAddressJuwei { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        public string NativePlace { get; set; }

    }
}
