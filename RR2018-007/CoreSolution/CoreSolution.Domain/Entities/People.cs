using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities.Base;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Domain.Entities
{
    //用户表
    public class People : EntityBaseFull
    {
        //用户账号
        public string PeopleNum { get; set; }
        //身份证号
        public string IdCard { get; set; }
        //用户密码
        public string PassWord { get; set; }
        //用户昵称
        public string PeopleName { get; set; }
        //用户性别
        public string PeopleSex { get; set; }
        //用户年龄
        public int PeopleAge { get; set; }
        //用户手机号
        public string PeopleTell { get; set; }
        //绑定邮箱
        public string PeopleMail { get; set; }
        //二维码名片
        public string PeopleCard { get; set; }
        //用户头像
        public string PeoplePicture { get; set; }
        //用户积分
        public int PeopleIntegration { get; set; }
    }
}
