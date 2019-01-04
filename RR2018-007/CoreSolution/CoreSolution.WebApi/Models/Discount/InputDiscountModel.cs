using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.Discount
{
    public class InputDiscountModel
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
