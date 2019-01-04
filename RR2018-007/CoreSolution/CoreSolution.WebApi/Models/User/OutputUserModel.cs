using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.Dto.Base;

namespace CoreSolution.WebApi.Models.User
{
    public class OutputUserModel : EntityBaseFullDto
    {
        public string UserName { get; set; }
        public string RealName { get; set; }
        public string[] RoleName { get; set; }
        public string Email { get; set; }
        public string PhoneNum { get; set; }
        public string PeopleSex { get; set; }
        public int PeopleAge { get; set; }
        public int PeopleIntegration { get; set; }
        public string PeopleCard { get; set; }
        public string IdCard { get; set; }
        public string PeoplePicture { get; set; }

        public Guid? PostStationId { get; set; }
        public string PostStationName { get; set; }
        public Guid? StreetId { get; set; }
        public string StreetName { get; set; }


    }
}
