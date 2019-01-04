using CoreSolution.Dto.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Dto
{
    public class PropertyDto : EntityBaseFullDto
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 所属街道
        /// </summary>
        public Guid StreetId { set; get; }
        public string StreetName { set; get; }

        /// <summary>
        /// 所属居委
        /// </summary>
        public Guid JuWeiId { set; get; }
        public string JuWeiName { set; get; }

        /// <summary>
        /// 所属小区
        /// </summary>
        public Guid QuartersId { set; get; }
        public string QuartersName { set; get; }


        /// <summary>
        /// 管理处地址
        /// </summary>
        public string AdminAddress { set; get; }
        /// <summary>
        /// 公司地址
        /// </summary>
        public string CompanyAddress { set; get; }
        /// <summary>
        /// 小区经理姓名
        /// </summary>
        public string ManagerName { set; get; }
        /// <summary>
        /// 小区经理电话
        /// </summary>
        public string ManagerTel { set; get; }
        /// <summary>
        /// 保修电话
        /// </summary>
        public string GuaranteeTel { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { set; get; }
        /// <summary>
        /// 附件名
        /// </summary>
        public string EnclosureName { set; get; }
        /// <summary>
        /// 附件路径
        /// </summary>
        public string EnclosureUrl { set; get; }

        public Guid StationId { get; set; }
        public string StationName { get; set; }

    }
}
