using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.Property
{
    public class InputPropertyModel
    {
        //public Guid? Id { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public string Name { set; get; }
        /// <summary>
        /// 所属街道
        /// </summary>
        [Required]
        public Guid StreetId { set; get; }
        /// <summary>
        /// 所属居委
        /// </summary>
        public Guid JuWeiId { set; get; }
        /// <summary>
        /// 所属小区
        /// </summary>
        public Guid QuartersId { set; get; }

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
    }


    public class SearchPropertyModel
    {
        public string Name { set; get; }
        public Guid? StreetId { set; get; }

        public Guid? StationId { get; set; }

        public Guid? JuWeiId { set; get; }
        public Guid? QuartersId { set; get; }
        public int pageIndex { set; get; } = 1;
        public int pageSize { set; get; } = 10;
    }
}
