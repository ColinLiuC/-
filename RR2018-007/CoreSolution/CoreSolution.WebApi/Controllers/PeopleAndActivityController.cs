using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/PeopleAndActivity")]
    public class PeopleAndActivityController : Controller
    {
        private readonly IPeopleAndActivityService _peopleAndActivityService;
        public PeopleAndActivityController(IPeopleAndActivityService peopleAndActivityService)
        {
            _peopleAndActivityService = peopleAndActivityService;
        }


    }
}