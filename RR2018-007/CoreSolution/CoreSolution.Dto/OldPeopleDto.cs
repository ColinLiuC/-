using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Dto
{
    public class OldPeopleDto : EntityBaseFullDto
    {
        //身份证号码
        public string Card { get; set; }
        //姓名
        public string Name { get; set; }
        //性别
        public Sex? Sex { get; set; }
        //年龄
        public int Age { get; set; }
        // 所属分类
        public Guid? Category { get; set; }
        //出生日期
        public DateTime BirthDay { get; set; }
        //婚姻状态
        public Marriage Marriage { get; set; }
        //联系电话
        public string Phone { get; set; }
        //居住地址
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
        //所属居委
        public Guid JuWei { get; set; }
        public string JuWeiName { get; set; }
        //联系人
        public string Contacts { get; set; }
        //联系人电话
        public string ContactsPhone { get; set; }
        //老人类别
        public Guid OldType { get; set; }
        public string OldTypeName { get; set; }
        //是否享有百岁每月营养补贴
        public bool? yiyang { get; set; }
        //是否安装科技助老设备
        public bool? keji { get; set; }
        //是否享有爱心送牛奶
        public bool? niunai { get; set; }
        //备注
        public string Remarks { get; set; }
        //登记人
        public string User { get; set; }
        //登记时间
        public DateTime CreateDate { get; set; }
    }
}
