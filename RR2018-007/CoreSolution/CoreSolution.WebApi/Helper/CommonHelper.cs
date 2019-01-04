using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Xml;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace CoreSolution.WebApi.Helper
{
    /// <summary>
    /// 通用方法类
    /// </summary>
    public class CommonHelper
    {

        //百度API地址
        private static string url = @"http://api.map.baidu.com/geocoder/v2/?location={0}&output=json&ak=WEc8RlPXzSifaq9RHxE1WW7lRKgbid6Y";
        /// <summary>
        /// 根据经纬度获取地理位置
        /// </summary>
        /// <param name="lat">纬度</param>
        /// <param name="lng">经度</param>
        /// <returns>具体的地埋位置</returns>
        public static string GetLocation(string lat, string lng)
        {
            HttpClient client = new HttpClient();
            string location = string.Format("{0},{1}", lat, lng);
            string bdUrl = string.Format(url, location);
            string result = client.GetStringAsync(bdUrl).Result;
            var locationResult = (JObject)JsonConvert.DeserializeObject(result);

            if (locationResult == null || locationResult["result"] == null || locationResult["result"]["formatted_address"] == null)
                return string.Empty;

            var address = Convert.ToString(locationResult["result"]["formatted_address"]);
            if (locationResult["result"]["sematic_description"] != null)
                address += " " + Convert.ToString(locationResult["result"]["sematic_description"]);
            return address;
        }



        /// <summary>
        /// 获取服务状态
        /// </summary>
        /// <param name="servertime">服务时间</param>
        /// <param name="zq">服务周期</param>
        /// <returns></returns>
        public static int SetStatusCode(DateTime servertime, int cycle)
        {
            //下次服务时间
            var mytime = servertime.AddDays(cycle);
            var nowtime = DateTime.Now;
            //时间差
            var _ctime = (mytime - nowtime).TotalDays;
            if (_ctime <= 0)
            {
                return 1;
            }
            else if (_ctime > 0 && _ctime <= 5)
            {
                return 2;
            }
            else
            {
                return 4;
            }
        }


        /// <summary>  
        /// 反序列化xml字符为对象  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="xml"></param>  
        /// <param name="encoding"></param>  
        /// <returns></returns>  
        public static T DeSerialize<T>(string xml, Encoding encoding)
            where T : new()
        {
            try
            {
                var mySerializer = new XmlSerializer(typeof(T));
                using (var ms = new MemoryStream(encoding.GetBytes(xml)))
                {
                    using (var sr = new StreamReader(ms, encoding))
                    {
                        return (T)mySerializer.Deserialize(sr);
                    }
                }
            }
            catch (Exception e)
            {
                return default(T);
            }

        }

        /// <summary>  
        /// 反序列化xml字符为对象，默认为Utf-8编码  
        /// </summary>  
        /// <typeparam name="T"></typeparam>  
        /// <param name="xml"></param>  
        /// <returns></returns>  
        public static T DeSerialize<T>(string xml)
            where T : new()
        {
            return DeSerialize<T>(xml, Encoding.UTF8);
        }

        public static T DeserializeToObject<T>(string xml)
        {
            T myObject;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringReader reader = new StringReader(xml); myObject = (T)serializer.Deserialize(reader); reader.Close();
            return myObject;
        }



        /// <summary>
        /// 调用http Get请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="Timeout"></param>
        /// <returns></returns>
        public static string GetHttpResponse(string url, int Timeout)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            request.UserAgent = null;
            request.Timeout = Timeout;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            retString = HttpUtility.HtmlDecode(retString);
            return retString;
        }



    }

}
