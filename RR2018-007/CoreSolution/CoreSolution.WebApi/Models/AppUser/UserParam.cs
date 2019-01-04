using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.AppUser
{
    public class UserParam
    {
        public Guid UserId { get; set; }

        public string Token { get; set; }
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
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

    }


    public struct JsonData2
    {
        public string success { get; set; }

        public data1 data;
    }

    public struct data1
    {
        //public string nickName { get; set; }

        public string idcard { get; set; }
        public string status { get; set; }
        public string name { get; set; }
        public string userId { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }
        public string sscard { get; set; }
        public string userName { get; set; }
        public string head_pic { get; set; }

    }
}
