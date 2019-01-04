using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities.Base;

namespace CoreSolution.Domain.Entities
{
    public class User : EntityBaseFull
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string Email { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public string PhoneNum { get; set; }
        public bool IsPhoneNumConfirmed { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public Guid? CreatorUserId { get; set; }
        public virtual User CreatorUser { get; set; }
        public Guid? DeleterUserId { get; set; }
        public virtual User DeleterUser { get; set; }
        public bool IsLocked { get; set; } = false;
        public Guid? PostStationId { get; set; }
        public string PostStationName { get; set; }
        public Guid? StreetId { get; set; }
        public string StreetName { get; set; }
        public int? UserType { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();



        //身份证号
        public string IdCard { get; set; }
        //用户性别
        public string PeopleSex { get; set; }
        //用户年龄
        public int? PeopleAge { get; set; }
        //用户手机号
        public string PeopleTell { get; set; }
        //二维码名片
        public string PeopleCard { get; set; }
        //用户头像
        public string PeoplePicture { get; set; }
        //用户积分
        public int? PeopleIntegration { get; set; }


    }
}
