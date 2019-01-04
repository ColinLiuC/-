using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.Redis.Helper;
using CoreSolution.WebApi.Manager;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreSolution.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/Default")]
    public class BaseController : Controller
    {
        private static string PERMISSIONS_PREFIX = "Api.User.Permissions.";
        /// <summary>
        /// 访问任意action前
        /// </summary>
        /// <param name="context"></param>
        public async override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
            //当前action
            string action_path = Request.Path;
            //根据token获取userid
            //string token = HttpContext.Request.Headers["token"];
            string token = "9a77ba30-c3a4-4568-adaf-8706dcb9db76";
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //根据userid获取权限
            var permissions = await RedisHelper.StringGetAsync(PERMISSIONS_PREFIX + userId);
            permissions = permissions.Replace("\"", "");

            if (!permissions.Contains(action_path))
            {
                throw new Exception("您无权访问");
            }
        }

    }
}