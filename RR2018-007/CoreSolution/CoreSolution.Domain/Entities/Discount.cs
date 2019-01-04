using CoreSolution.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreSolution.Domain.Entities
{
   public class Discount: EntityBaseFull
    {
        /// <summary>
        /// 优惠名称
        /// </summary>
        public string PreferentialName { get; set; }

        /// <summary>
        /// 优惠条件
        /// </summary>
        public string FavourableConditions { get; set; }

        /// <summary>
        /// 价格说明
        /// </summary>
        public string PriceDescription { get; set; }

        /// <summary>
        /// 对应的服务Guid
        /// </summary>
        public Guid ServiceGuid { get; set; }
    }
}
