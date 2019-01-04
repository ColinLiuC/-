using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Models.OldPeople
{
    public class ServiceInfo
    {
        public Guid Id { get; set; }
        //身份证号码
        public string Card { get; set; }
        //姓名
        public string Name { get; set; }
        //性别
        public Sex Sex { get; set; }
        //年龄
        public int Age { get; set; }

        //出生日期
        public DateTime BirthDay { get; set; }
        //婚姻状态
        public Marriage Marriage { get; set; }
        //联系电话
        public string Phone { get; set; }
        //居住地址
        public string Address { get; set; }
        //所属居委
        public Guid JuWei { get; set; }
        //联系人
        public string Contacts { get; set; }
        //联系人电话
        public string ContactsPhone { get; set; }
        //老人类别
        public Guid OldType { get; set; }
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



        //服务对象Id
        public Guid OldPeopleId { get; set; }

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



    }
}
