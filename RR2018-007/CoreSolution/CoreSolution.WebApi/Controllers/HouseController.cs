using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using AutoMapper;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Service;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Helper;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models;
using CoreSolution.WebApi.Models.House;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/House")]
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;
        private readonly string _websertviceurl;
        //string filePath = @"logs/mylogs.txt";
        public HouseController(IHouseService houseService)
        {
            _houseService = houseService;
            _websertviceurl = ConfigHelper.GetSectionValue("WebServiceUrl");
        }

        /// <summary>       
        /// 新增       
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("create")]
        [HttpPost]
        public async Task<JsonResult> Create(InputHouseModel model)
        {
            if (model.HouseNumber == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "室号不能为空");
            }
            if (model.BuildArea == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "建筑面积不能为空");
            }
            if (model.DoorCardId == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "所属门牌不能为空");
            }
            if (model.OrientationId == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "朝向不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //数据映射
            var infoDto = Mapper.Map<HouseDto>(model);
            var id = await _houseService.InsertAndGetIdAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
        }


        [Route("create2")]
        [HttpPost]
        public async Task<JsonResult> Create2(Guid doorCardId, string houseJson)
        {
            if (doorCardId == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "门牌号不能为空");
            }
            string token = HttpContext.Request.Headers["token"];
            var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
            //反序列化成房屋对象
            List<HouseDto> housemodel = JsonConvert.DeserializeObject<List<HouseDto>>(houseJson);
            if (housemodel != null && housemodel.Count > 0)
            {
                //新增前先删除旧的记录
                _houseService.DeleteByDoorCard(doorCardId);
                foreach (var houseitem in housemodel)
                {
                    HouseDto housedto = new HouseDto();
                    housedto.BuildArea = houseitem.BuildArea;
                    housedto.OrientationId = houseitem.OrientationId;
                    housedto.HouseNumber = houseitem.HouseNumber;
                    housedto.DoorCardId = doorCardId;
                    await _houseService.InsertAndGetIdAsync(housedto);
                }
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加成功");
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "添加失败");
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns></returns>
        [Route("delete")]
        [HttpPost]
        public async Task<JsonResult> Delete(Guid id)
        {
            var infoDto = await _houseService.GetAsync(id);
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该门牌不存在");
            }
            infoDto.DeletionTime = DateTime.Now;
            await _houseService.DeleteAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "删除成功");
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model">输入参数model</param>
        /// <returns></returns>
        [Route("modify")]
        [HttpPost]
        public async Task<JsonResult> Modify(InputHouseModel model)
        {
            var infoDto = await _houseService.GetAsync(Guid.Parse(model.Id.ToString()));
            if (infoDto == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该房屋不存在");
            }
            model.HouseNumber = infoDto.HouseNumber;
            infoDto = Mapper.Map<HouseDto>(model);
            //调用人口库（同步本市、外来人口数量）
            var houses = GetHouseUsersByn(model.Address);
            if (houses.Count > 0)
            {
                infoDto.BenShiUserCount = houses.Count(p => p.PersonType == "001");
                infoDto.WaiLaiUserCount = houses.Count(p => p.PersonType == "002");
            }

            await _houseService.UpdateAsync(infoDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 根据Id获取房屋。200获取成功;404未找到
        /// </summary>
        /// <param name="id">小区Id</param>
        /// <returns></returns>
        [Route("getInfoById")]
        [HttpGet]
        public async Task<JsonResult> GetInfoById(Guid id)
        {
            var result = await _houseService.GetAsync(id);
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该房屋不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        [Route("getMyHouse")]
        [HttpGet]
        public JsonResult GetMyHouse(Guid id)
        {

            var result = _houseService.GetHouseInfo(id);
            if (result != null && result.house.OrientationId != null)
            {
                DataDictionaryService service = new DataDictionaryService();
                result.OrientationName = service.GetItemNameById(Guid.Parse(result.house.OrientationId.ToString()));
            }
            if (result == null)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "该房屋不存在", result);
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        /// <summary>
        /// 根据过滤条件查询分页
        /// </summary>
        /// <param name="doorCardId">门牌</param>
        /// <param name="houseaddress">地址</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页数据条数</param>
        /// <returns></returns>
        [Route("getHouses")]
        [HttpGet]
        public async Task<JsonResult> GetHouses(Guid? doorCardId, string houseaddress, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                //拼接过滤条件
                Expression<Func<House, bool>> where = p =>
                   (doorCardId == null || p.DoorCardId == doorCardId);
                var result = await _houseService.GetPagedAsync(where, i => i.CreationTime, pageIndex, pageSize, true);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", new ListModel<OutputHouseModel>
                { Total = result.Item1, List = Mapper.Map<IList<OutputHouseModel>>(result.Item2) });

            }
            catch (Exception)
            {

                return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "失败");
            }
        }



        ///// <summary>
        ///// 调用人口库，返回房屋人员列表
        ///// </summary>
        ///// <param name="houseAddress">居住地址</param>
        ///// <returns></returns>
        //[Route("getHouseUsers3")]
        //[HttpGet]
        //public JsonResult GetHouseUsers3(string houseAddress)
        //{
        //    try
        //    {
        //        using (FileStream stream = new FileStream(filePath, FileMode.Append))
        //        using (StreamWriter writer = new StreamWriter(stream))
        //        {
        //            writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "开始执行");
        //        }
        //        using (var client = new WebClient())
        //        {
        //            client.Encoding = Encoding.UTF8;
        //            string serviceAddress = "http://192.168.1.62:8201/WebService/PersonBasicAndStateService.asmx/GetPersonListByAddress?address=" + houseAddress;
        //            var xmldata = client.DownloadString(serviceAddress);
        //            xmldata = HttpUtility.HtmlDecode(xmldata);

        //            xmldata = xmldata.Replace("<string xmlns=\"http://tempuri.org/\">", "").Replace("</string>", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");

        //            //获取Data节点下的内容
        //            XmlDocument doc = new XmlDocument();
        //            doc.LoadXml(xmldata.Trim());
        //            var channelXml = doc.SelectSingleNode("Data").InnerXml;
        //            List<Person> persons = new List<Person>();

        //            if (channelXml != "<Rows></Rows>")
        //            {
        //                //将xml转化为json
        //                var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
        //                var jObject = JObject.Parse(json);
        //                var jsonnew = jObject["Rows"]["Person"].ToString();
        //                //将json数据反序列化成对象
        //                persons = JsonConvert.DeserializeObject<List<Person>>(jsonnew);
        //            }
        //            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", persons);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        using (FileStream stream = new FileStream(filePath, FileMode.Append))
        //        using (StreamWriter writer = new StreamWriter(stream))
        //        {
        //            writer.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + ex.Message);
        //        }
        //        return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "失败", new List<Person>());
        //    }
        //}


        /// <summary>
        /// 调用人口库,获取房屋居住人员列表 
        /// Create by LiuCheng 
        /// </summary>
        /// <param name="houseAddress">居住地址</param>
        /// <returns></returns>
        [Route("getHouseUsers")]
        [HttpGet]
        public JsonResult GetHouseUsers(string houseAddress)
        {
            using (var client = new WebClient())
            {
                string serviceAddress = $"{_websertviceurl}GetPersonListByAddress?address={houseAddress}";
                //调用webservice
                client.Encoding = Encoding.UTF8;
                var xmldata = client.DownloadString(serviceAddress);
                xmldata = HttpUtility.HtmlDecode(xmldata);
                //处理返回的结果（根据具体的返回数据做处理）
                xmldata = xmldata.Replace("<string xmlns=\"http://tempuri.org/\">", "").Replace("</string>", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");
                //获取Data节点下的内容
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmldata.Trim());
                var channelXml = doc.SelectSingleNode("Data").InnerXml;
                List<Person> persons = new List<Person>();
                if (channelXml != "<Rows></Rows>")
                {
                    //将xml转化为json
                    var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                    var jObject = JObject.Parse(json);
                    var jsonnew = jObject["Rows"]["Person"].ToString();
                    if (!jsonnew.Contains("["))
                    {
                        jsonnew = "[" + jsonnew + "]";
                    }
                    //将json数据反序列化成对象
                    persons = JsonConvert.DeserializeObject<List<Person>>(jsonnew);
                }
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", persons);
            }
        }


        /// <summary>
        /// 调用人口库，返回人员详细信息
        /// </summary>
        /// <param name="idcard">身份证号码</param>
        /// <param name="persontype">用户类型</param>
        /// <returns></returns>
        [Route("getHouseUser")]
        [HttpGet]
        public JsonResult GetHouseUser(string idcard, string persontype)
        {
            using (var client = new WebClient())
            {
                string serviceAddress = $"{_websertviceurl}GetBasicPersonInfoByCondition?strIDCard={idcard}&personType={persontype}";
                //调用webservice
                client.Encoding = Encoding.UTF8;
                var xmldata = client.DownloadString(serviceAddress);
                xmldata = HttpUtility.HtmlDecode(xmldata);
                xmldata = xmldata.Replace("<string xmlns=\"http://tempuri.org/\">", "").Replace("</string>", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");

                //获取Data节点下的内容
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmldata.Trim());
                var channelXml = doc.SelectSingleNode("Data").InnerXml;
                Person person = new Person();

                if (channelXml != "<Rows></Rows>")
                {
                    //将xml转化为json
                    var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                    var jObject = JObject.Parse(json);
                    var jsonnew = jObject["Rows"]["Row"].ToString();
                    //将json数据反序列化成对象
                    person = JsonConvert.DeserializeObject<Person>(jsonnew);
                }
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", person);
            }
        }


        [Route("getHouseUsersByn")]
        [HttpGet]
        public List<Person> GetHouseUsersByn(string houseAddress)
        {
            using (var client = new WebClient())
            {
                string serviceAddress = $"{_websertviceurl}GetPersonListByAddress?address={houseAddress}";
                //调用webservice
                client.Encoding = Encoding.UTF8;
                var xmldata = client.DownloadString(serviceAddress);
                xmldata = HttpUtility.HtmlDecode(xmldata);
                xmldata = xmldata.Replace("<string xmlns=\"http://tempuri.org/\">", "").Replace("</string>", "").Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "");

                //获取Data节点下的内容
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmldata.Trim());
                var channelXml = doc.SelectSingleNode("Data").InnerXml;
                List<Person> persons = new List<Person>();

                if (channelXml != "<Rows></Rows>")
                {
                    //将xml转化为json
                    var json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.None, true);
                    var jObject = JObject.Parse(json);
                    var jsonnew = jObject["Rows"]["Person"].ToString();
                    if (!jsonnew.Contains("["))
                    {
                        jsonnew = "[" + jsonnew + "]";
                    }
                    //将json数据反序列化成对象
                    persons = JsonConvert.DeserializeObject<List<Person>>(jsonnew);
                }
                return persons;
            }
        }

        /// <summary>
        /// 同步人口库数据（本市人口数量、外来人口数量）
        /// </summary>
        /// <returns></returns>
        [Route("syncHousePerson")]
        [HttpGet]
        public async Task<JsonResult> SyncHousePerson()
        {
            //获取所有房屋list
            var houselist = await _houseService.GetAllListAsync();
            if (houselist != null)
            {
                foreach (var house in houselist)
                {
                    //获取房屋地址
                    var house_address = _houseService.GetHouseAddress(house.Id);
                    if (house_address != null)
                    {
                        //调用人口库，返回房屋内人员信息
                        var _houses = GetHouseUsersByn(house_address);
                        if (_houses.Count > 0)
                        {
                            house.BenShiUserCount = _houses.Count(p => p.PersonType == "001");
                            house.WaiLaiUserCount = _houses.Count(p => p.PersonType == "002");
                            //更新数据库
                            await _houseService.UpdateAsync(house);
                        }
                    }
                }
            }
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功");
        }
    }
}