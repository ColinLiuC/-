using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CoreSolution.Domain.Enum.EnumCode;

namespace CoreSolution.WebApi.Models.ReceptionService
{
    /// <summary>
    /// 新增服务信息model
    /// </summary>
    public class InputReceptionServiceModel
    {
        public Guid? Id { get; set; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 所属分类
        /// </summary>
        public ServiceCateGory Category { get; set; }
        /// <summary>
        /// 服务描述
        /// </summary>
        public string ServiceDescription { get; set; }
        /// <summary>
        /// 服务流程
        /// </summary>
        public string ServiceFlow { get; set; }
        /// <summary>
        /// 服务依据
        /// </summary>
        public string ServiceBasis { get; set; }
        /// <summary>
        /// 服务申请条件
        /// </summary>
        public string ApplicationConditions { get; set; }
        /// <summary>
        /// 附件
        /// </summary>
        public string Attachments { get; set; }
        /// <summary>
        /// 服务地址
        /// </summary>
        public string ServiceAddress { get; set; }
        /// <summary>
        /// 服务时间描述
        /// </summary>
        public string TimeDescription { get; set; }
        /// <summary>
        /// 收费情况
        /// </summary>
        public ServiceCharge CaChargeSituationtegory { get; set; }
        /// <summary>
        /// 服务费用
        /// </summary>
        public decimal ServiceCost { get; set; }
        /// <summary>
        /// 注意事项
        /// </summary>
        public string MattersAttention { get; set; }

        /// <summary>
        /// 是否归档
        /// </summary>
        public int? IsGuiDang { get; set; }
        /// <summary>
        /// 归档备注
        /// </summary>
        public string ArchivalRemark { get; set; }
        /// <summary>
        /// 归档人
        /// </summary>
        public string Archiving { get; set; }
        /// <summary>
        /// 归档日期
        /// </summary>
        public DateTime? FilingDate { get; set; }
        /// <summary>
        /// 活动图片名称
        /// </summary>
        public string ActivityImg { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string AttachmentPath { get; set; }
        /// <summary>
        /// 所属街道Id
        /// </summary>
        public Guid? StreetId { get; set; }
        /// <summary>
        /// 所属驿站Id
        /// </summary>
        public Guid? PostStationId { get; set; }
        /// <summary>
        /// 街道名称
        /// </summary>
        public string StreetName { get; set; }
        /// <summary>
        /// 驿站名称
        /// </summary>
        public string PostStationName { get; set; }
        /// <summary>
        /// 所属机构Id
        /// </summary>
        public Guid? ServiceAgencyId { get; set; }
        /// <summary>
        /// 所属机构名称
        /// </summary>
        public string ServiceAgencyName { get; set; }
        public string Phone { get; set; }
    }
}
