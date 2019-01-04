using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.AppUser
{
    public class InputRegisterModel
    {
        public string Phone { get; set; }
        public string PassWord { get; set; }
        public string ValidateCode { get; set; }
    }

    public class InputAppUserModel
    {
        public string UserName { get; set; }
        public string Phone { get; set; }
        public string PassWord { get; set; }

    }
}
