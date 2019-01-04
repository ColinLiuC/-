using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.FixedAssets
{
    /// <summary>
    /// 固定资产 1  
    /// </summary>
    public class OutputFixedAssetsModel
    {
        public Guid Id { get; set; }
        /// <summary>
        /// 固定资产编号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        ///  固定资产名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///  型号
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        ///  目前情况 （空闲，正在使用，正在维修，已报废）
        /// </summary>
        public int CurrentState { get; set; }

        /// <summary>
        ///  负责人
        /// </summary>
        public string DutyPeople { get; set; }

        /// <summary>
        ///  联系电话
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        ///  采购日期
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        ///  固定资产描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 固定资产照片
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// 登记人
        /// </summary>
        public string RegisterPeople { get; set; }

        /// <summary>
        ///  登记日期
        /// </summary>
        public DateTime? RegisterDate { get; set; }

        /// <summary>
        ///  所属街道
        /// </summary>
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }

        /// <summary>
        ///  所属驿站
        /// </summary>
        public Guid StationId { get; set; }
        public string StationName { get; set; }

    }
}
