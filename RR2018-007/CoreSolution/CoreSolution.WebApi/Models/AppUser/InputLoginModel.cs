using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreSolution.WebApi.Models.AppUser
{
    public class InputLoginModel
    {
        public string PhoneOrName { get; set; }
        public string PassWord { get; set; }
    }
}
