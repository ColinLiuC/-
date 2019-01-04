using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.JuWei
{
    public class InputJuWeiModel
    {
        //public Guid? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }

        [Required]
        public Guid PostStationId { get; set; }
        public string PostStationName { get; set; }

        public string JuWeiPeople { get; set; }

        //联系电话
        public string Phone { get; set; }

        //介绍
        public string Introduce { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public double? Lat { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public double? Lng { get; set; }
    }
}
