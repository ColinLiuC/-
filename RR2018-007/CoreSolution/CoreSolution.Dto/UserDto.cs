using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Dto.Base;

namespace CoreSolution.Dto
{
    public class UserDto : EntityBaseFullDto
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
        public UserDto CreatorUser { get; set; }
        public Guid? DeleterUserId { get; set; }
        public UserDto DeleterUser { get; set; }
        public bool IsLocked { get; set; } = false;
        public Guid? PostStationId { get; set; }
        public string PostStationName { get; set; }
        public Guid? StreetId { get; set; }
        public string StreetName { get; set; }
        public int? UserType { get; set; }
        public IList<UserRoleDto> UserRoles { get; set; }



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
