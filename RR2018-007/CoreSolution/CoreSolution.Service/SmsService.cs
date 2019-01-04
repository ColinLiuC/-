using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CoreSolution.Service
{
   public class SmsService
    {
        private const string CodeModel = "您的验证码是{0},有效时间5分钟。如非本人操作，请忽略本短信";

        public bool SendCode(string mobile, string code)
        {
            StringBuilder sms = new StringBuilder();
            sms.AppendFormat("name={0}", "dxwreload");
            sms.AppendFormat("&pwd={0}", "466A7DA6ABD3608208A68DE51631");
            sms.AppendFormat("&content={0}", string.Format(CodeModel, code));
            sms.AppendFormat("&mobile={0}", mobile);
            sms.AppendFormat("&sign={0}", "【虹口市民驿站】");
            sms.Append("&type=pt");
            string resp = PushToWeb("http://web.duanxinwang.cc/asmx/smsservice.aspx", sms.ToString(), Encoding.UTF8);
            string[] msg = resp.Split(',');
            if (msg[0] == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SendPassword(string mobile, string code)
        {
            string CodeModelP = "您的新密码是{0}。为了您的账号安全，请登录成功后修改该密码。";
            StringBuilder sms = new StringBuilder();
            sms.AppendFormat("name={0}", "dxwreload");
            sms.AppendFormat("&pwd={0}", "466A7DA6ABD3608208A68DE51631");
            sms.AppendFormat("&content={0}", string.Format(CodeModelP, code));
            sms.AppendFormat("&mobile={0}", mobile);
            sms.AppendFormat("&sign={0}", "洋泾生活服务网");
            sms.Append("&type=pt");
            string resp = PushToWeb("http://web.duanxinwang.cc/asmx/smsservice.aspx", sms.ToString(), Encoding.UTF8);
            string[] msg = resp.Split(',');
            if (msg[0] == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SendCode(string[] mobiles, string code)
        {
            string mobile = string.Join(",", mobiles);
            return SendCode(mobile, code);
        }

        private string PushToWeb(string weburl, string data, Encoding encode)
        {
            byte[] byteArray = encode.GetBytes(data);

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(new Uri(weburl));
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = byteArray.Length;
            Stream newStream = webRequest.GetRequestStream();
            newStream.Write(byteArray, 0, byteArray.Length);
            newStream.Close();

            //接收返回信息：
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            StreamReader aspx = new StreamReader(response.GetResponseStream(), encode);
            return aspx.ReadToEnd();
        }


        public bool SendMessage(string mobile, string content)
        {
            StringBuilder sms = new StringBuilder();
            sms.AppendFormat("name={0}", "dxwreload");
            sms.AppendFormat("&pwd={0}", "466A7DA6ABD3608208A68DE51631");
            sms.AppendFormat("&content={0}",content);
            sms.AppendFormat("&mobile={0}", mobile);
            sms.AppendFormat("&sign={0}", "【虹口市民驿站】");
            sms.Append("&type=pt");
            string resp = PushToWeb("http://web.duanxinwang.cc/asmx/smsservice.aspx", sms.ToString(), Encoding.UTF8);
            string[] msg = resp.Split(',');
            if (msg[0] == "0")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool SendMessage(string[] mobiles, string content)
        {
            string mobile = string.Join(",", mobiles);
            return SendMessage(mobile, content);
        }
    }
}
