using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.User
{
    public class UserParam
    {
        public Guid userId { get; set; }
        public Guid PostStationId { get; set; }
        public string PostStationName { get; set; }
        public Guid StreetId { get; set; }
        public string StreetName { get; set; }

        public int? UserType { get; set; }

        public string Token { get; set; }
        public string UserName { get; set; }
        public double? centerStreetLat { get; set; }
        public double? centerStreetLng { get; set; }
        public string RealName { get; internal set; }
    }

}
