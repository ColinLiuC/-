using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.Dto.Base;

namespace CoreSolution.WebApi.Models.People
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class OutputPeopleModel : EntityBaseFullDto
    {
        /// <summary>
        /// 用户账号
        /// </summary>
        public string PeopleNum { get; set; }
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string PassWord { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string PeopleName { get; set; }
        /// <summary>
        /// 用户性别
        /// </summary>
        public string PeopleSex { get; set; }
        /// <summary>
        /// 用户年龄
        /// </summary>
        public int PeopleAge { get; set; }
        /// <summary>
        /// 用户手机号
        /// </summary>
        public string PeopleTell { get; set; }
        /// <summary>
        /// 绑定邮箱
        /// </summary>
        public string PeopleMail { get; set; }
        /// <summary>
        /// 二维码名片
        /// </summary>
        public string PeopleCard { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string PeoplePicture { get; set; }
        /// <summary>
        /// 用户积分
        /// </summary>
        public int PeopleIntegration { get; set; }
    }
}
