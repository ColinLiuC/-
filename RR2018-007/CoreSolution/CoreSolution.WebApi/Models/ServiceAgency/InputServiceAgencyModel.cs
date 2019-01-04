using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.ServiceAgency
{
    public class InputServiceAgencyModel
    {
        /// <summary>
        /// ID值
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string AgencyName { get; set; }
        /// <summary>
        /// 机构类别
        /// </summary>
        public Guid AgencyCategory { get; set; }
        /// <summary>
        /// 机构负责人
        /// </summary>
        public string AgencyLeader { get; set; }
        /// <summary>
        /// 机构联系电话
        /// </summary>
        public string ContactPhone { get; set; }
        /// <summary>
        /// 机构联系地址
        /// </summary>
        public string ContactAddress { get; set; }
        /// <summary>
        /// 机构描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 服务机构图片名称
        /// </summary>
        public string SaImgName { get; set; }
        /// <summary>
        /// 服务机构图片路径
        /// </summary>
        public string SaImgPath { get; set; }
        /// <summary>
        /// 服务机构资质信息
        /// </summary>
        public string QualificationsName { get; set; }
        /// <summary>
        /// 服务机构资质图片路径
        /// </summary>
        public string QualificationsPath { get; set; }
        /// <summary>
        /// 所属街道Id
        /// </summary>
        public Guid? StreetId { get; set; }
        /// <summary>
        /// 所属街道名称
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 所属驿站Id
        /// </summary>
        public Guid? PostStationId { get; set; }
        /// <summary>
        /// 所属驿站名称
        /// </summary>
        public string PostStationName { get; set; }
    }
}
