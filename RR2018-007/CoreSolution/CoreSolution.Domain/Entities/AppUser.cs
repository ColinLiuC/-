using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
    public class AppUser : EntityBaseFull
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }    
        public string PassWord { get; set; }              
        //身份证号
        public string IdCard { get; set; }
        //用户性别
        public string Gender { get; set; }
        //用户年龄
        public int? Age { get; set; }
        //二维码名片
        public string UserQRCode { get; set; }
        //用户头像
        public string Picture { get; set; }
        //用户积分
        public int? Integration { get; set; }
        
        public bool IsLocked { get; set; } = false;

        public bool IsPhoneNumConfirmed { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string Salt { get; set; }


    }
}
