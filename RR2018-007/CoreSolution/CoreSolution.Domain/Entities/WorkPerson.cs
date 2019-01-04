using System;
using System.Collections.Generic;
using System.Text;
using CoreSolution.Domain.Entities.Base;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.Domain.Entities
{
    public class WorkPerson : EntityBaseFull
    {
        //工作人员ID
        public string PerId { get; set; }
        //人员照片名称
        public string PerImg { get; set; }
        //人员照片地址
        public string PerImgSrc { get; set; }
        //人员姓名
        public string PerName { get; set; }
        //身份证号
        public string PerCard { get; set; }
        //性别
        public Sex Sex { get; set; }
        //年龄
        public int Age { get; set; }
        //出生日期
        public DateTime BirthDay { get; set; }
        //民族
        public Guid Ethnic { get; set; }
        //居住地址
        public string Address { get; set; }
        //联系电话
        public int Phone { get; set; }
        //所属街道
        public Guid Street { get; set; }
        //所属驿站
        public Guid Station { get; set; }
        //职务
        public string Post { get; set; }
        //人员类型
        public Guid PerType { get; set; }
        //学历
        public Guid Degree { get; set; }
        //政治面貌
        public Guid PoliticalAspects { get; set; }
    }
}
