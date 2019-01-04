using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Script.Serialization;

namespace CoreSolution.WebApi.Helper
{
    public class Oauth2
    {
        //JavaScriptSerializer Jss = new JavaScriptSerializer();
        public Oauth2() { }

        /// <summary>
        /// 获取用户code
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="redirect_uri">重定向url</param>
        public void GetCodeUrl(string clientId, string redirect_uri)
        {
            string url = string.Format(" http://api.eshimin.com/api/oauth/authorize?client_id={0}&response_type=code&redirect_uri={1}&scope=read", clientId, redirect_uri);
            CommonMethod.WebRequestPostOrGet(url, "");
            using (System.IO.StreamWriter sw = new System.IO.StreamWriter("E:\\website\\xyaerror.txt", true))
            {
                sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ") + url);
            }

        }


        /// <summary>
        /// 用code 获取accesstoken，获取用户信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="clientsecret"></param>
        /// <param name="redirecturl">重定向地址，</param>
        /// <param name="Code">code</param>
        /// <returns>返回用户信息</returns>
        public string GetUserInfo(string clientId, string clientsecret, string redirecturl, string Code)
        {
            LogHelper.WriteLog(redirecturl + "," + clientsecret + "," + Code);
            string url = string.Format("http://api.eshimin.com/api/oauth/token?client_id={0}&client_secret={1}&grant_type=authorization_code&redirect_uri={2}&code={3}", clientId, clientsecret, redirecturl, Code);
            string ReText = CommonMethod.GetHttpResponse(url, 1200);//post/get方法获取信息
            LogHelper.WriteLog(ReText);

            //   Dictionary<string, object> DicText = (Dictionary<string, object>)JsonConvert.DeserializeObject(ReText);
            Dictionary<string, object> DicText = JsonConvert.DeserializeObject<Dictionary<string, object>>(ReText);

            if (!DicText.ContainsKey("access_token"))
            {
                return "获取access_token失败";
            }
            else
            {
                LogHelper.WriteLog(DicText["user"] +","+ DicText["access_token"]);
                return CommonMethod.GetHttpResponse(" http://api.eshimin.com/api/v2/user/identity/" + DicText["user"] + "?access_token=" + DicText["access_token"], 1200);
            }
        }


    }

}
