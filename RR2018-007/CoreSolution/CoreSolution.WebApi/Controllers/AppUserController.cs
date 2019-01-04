using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Autofac.Configuration.Util;
using CoreSolution.Domain.Enum;
using CoreSolution.Dto;
using CoreSolution.IService;
using CoreSolution.Tools.WebResult;
using CoreSolution.WebApi.Manager;
using CoreSolution.WebApi.Models.AppUser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CoreSolution.Tools.Extensions;
using CoreSolution.Service;
using CoreSolution.Redis.Helper;
using Microsoft.AspNetCore.Cors;
using CoreSolution.WebApi.Models.User;
using Newtonsoft.Json;
using CoreSolution.WebApi.Helper;
using AutoMapper;
using System.IO;

namespace CoreSolution.WebApi.Controllers
{
    [EnableCors("AllowAllOrigin")]
    [Produces("application/json")]
    [Route("api/AppUser")]
    public class AppUserController : Controller
    {
        private readonly IAppUserService _iAppUserService;
        string filePath = @"logs/shiminyun.txt";
        public AppUserController(IAppUserService iAppUserService)
        {
            _iAppUserService = iAppUserService;
        }

        /// <summary>
        /// 用户注册 （200注册成功，400用户名、密码不能为空，302用户名已存在,406验证码不匹配）
        /// </summary>
        /// <param name="inputAppUserModel"></param>
        /// <returns></returns>
        [Route("register")]
        [HttpPost]
        public async Task<JsonResult> Register(Models.AppUser.InputRegisterModel inputAppUserModel)
        {
            if (string.IsNullOrEmpty(inputAppUserModel.Phone))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "手机号不能为空");
            }
            if (string.IsNullOrEmpty(inputAppUserModel.PassWord))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "密码不能为空");
            }
            if (inputAppUserModel.PassWord.Length < 6)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "密码长度不能小于6位数");
            }
            //检查手机号是否重复
            bool isExist = await _iAppUserService.CheckPhoneDupAsync(inputAppUserModel.Phone);
            if (isExist)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.MovedPermanently, "手机号已存在");
            }
            //检测验证码是否匹配
            string code = await RedisHelper.StringGetAsync("uservalidcode"); ;
            string salt = new Random().Next(100000, 999999).ToString();
            if (inputAppUserModel.ValidateCode != code)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotAcceptable, "验证码不正确");
            }
            var appUserDto = new AppUserDto
            {
                Phone = inputAppUserModel.Phone,
                UserName = inputAppUserModel.Phone,
                PassWord = (inputAppUserModel.PassWord.ToMd5() + salt).ToMd5(),
                Salt = salt
            };
            var id = await _iAppUserService.InsertAndGetIdAsync(appUserDto);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "注册成功", id);
        }


        /// <summary>
        /// 用户登录（200 成功，返回token，400参数不能为空，404 用户名不存在，406 用户名或密码错误，502 未知的result）
        /// </summary>
        /// <param name="inputLoginModel"></param>
        /// <returns></returns>
        [Route("login")]
        [HttpPost]
        public async Task<JsonResult> Login(Models.AppUser.InputLoginModel inputLoginModel)
        {
            if (string.IsNullOrWhiteSpace(inputLoginModel.PhoneOrName))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "登录账号不能为空");//400
            }
            if (string.IsNullOrWhiteSpace(inputLoginModel.PassWord))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "密码不能为空");
            }
            LoginResults result = await _iAppUserService.CheckUserPasswordAsync(inputLoginModel.PhoneOrName, inputLoginModel.PassWord);
            if (result == LoginResults.Success)
            {
                var user = await _iAppUserService.GetUserByUserNameOrPhoneAsync(inputLoginModel.PhoneOrName);
                string token = Guid.NewGuid().ToString();
                await LoginManager.LoginAsync(token, user.Id);
                user.Token = token;
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", user);//200
            }
            if (result == LoginResults.NotExist)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotFound, "用户不存在");//404
            }
            if (result == LoginResults.PassWordError)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.NotAcceptable, "用户名或密码错误");//406
            }
            return AjaxHelper.JsonResult(HttpStatusCode.BadGateway, "未知的result:" + result);//502
        }



        /// <summary>
        /// 处理市民云登录用户 （200 登录成功，201注册成功， 400携带信息为空）
        /// </summary>
        /// <param name="userNameOrIdCart"></param>
        /// <returns></returns>
        [Route("checkCloudUser")]
        [HttpPost]
        public async Task<JsonResult> CheckCloudUser(string userNameOrIdCart)
        {
            //备注：根据市民云携带的信息去数据库匹配
            //若匹配成功则直接登录，若匹配不到则新增一条用户信息
            if (string.IsNullOrWhiteSpace(userNameOrIdCart))
            {
                return AjaxHelper.JsonResult(HttpStatusCode.BadRequest, "市民云携带信息为空");//400
            }
            var user = await _iAppUserService.GetUserByUserNameOrIdCartAsync(userNameOrIdCart);
            if (user != null)
            {
                string token = Guid.NewGuid().ToString();
                await LoginManager.LoginAsync(token, user.Id);
                var _user = new Models.AppUser.UserParam()
                {
                    Token = token,
                    UserId = user.Id,
                    UserName = user.UserName,
                    IdCard = user.IdCard,
                    Phone = user.Phone,
                    RealName = user.RealName,
                    Email = user.Email,
                    Picture = user.Picture,
                    Age = user.Age,
                    Gender = user.Gender,
                    UserQRCode = user.UserQRCode
                };
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", _user);//200
            }
            else
            {
                var appUserDto = new AppUserDto();
                appUserDto.PassWord = "123456";
                if (Tools.Extensions.StringExtensions.IsNumeric(userNameOrIdCart))
                {
                    appUserDto.IdCard = userNameOrIdCart;
                }
                else
                {
                    appUserDto.UserName = userNameOrIdCart;
                }
                var id = await _iAppUserService.InsertAndGetIdAsync(appUserDto);
                return AjaxHelper.JsonResult(HttpStatusCode.Created, "注册成功", id);//201
            }
        }



        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="mobile">手机号</param>
        /// <returns></returns>
        [Route("sendCode")]
        [HttpPost]
        public async Task<JsonResult> SendCode(string mobile)
        {
            bool isExist = await _iAppUserService.CheckPhoneDupAsync(mobile);
            if (isExist)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "该手机号已注册", "该手机号已注册");
            }
            ValidCodeHelper vCode = new ValidCodeHelper();
            string uservalidcode = vCode.CreateValidateCode(6);
            //存入redis
            await RedisHelper.StringSetAsync("uservalidcode", uservalidcode, TimeSpan.FromMinutes(5));
            //发送短信
            new SmsService().SendCode(mobile, uservalidcode);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "发送成功");//201
        }


        /// <summary>
        /// 市民云授权登录 
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        /// 
        [Route("getUserInfoAuth")]
        [HttpPost]
        public async Task<JsonResult> GetUserInfoAuth(string code)
        {
            try
            {
                LogHelper.WriteLog("获取code：" + code);
                string clientid = ConfigHelper.GetSectionValue("clientid");
                string clientsecret = ConfigHelper.GetSectionValue("clientsecret");
                string redirecturl = ConfigHelper.GetSectionValue("redirecturl");
                Oauth2 auth = new Oauth2();
                //获取用户信息
                string userinfo = auth.GetUserInfo(clientid, clientsecret, redirecturl, code);

                LogHelper.WriteLog("获取用户信息：" + userinfo);

                if (!string.IsNullOrEmpty(userinfo))
                {
                    //JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    JsonData2 jd = JsonConvert.DeserializeObject<JsonData2>(userinfo);

                    string token = Guid.NewGuid().ToString();
                    string salt = new Random().Next(100000, 999999).ToString();
                    string password = "123456";
                    LogHelper.WriteLog("用户名：" + jd.data.userName);

                    //检测用户是否已注册
                    //bool isExist = _iAppUserService.CheckUserName(jd.data.userName);
                    bool isExist = false;
                    if (isExist)
                    {
                        LogHelper.WriteLog("用户存在，执行登录");
                        //已注册则直接登录
                        var user = await _iAppUserService.LoginByUserName(jd.data.userName);
                        await LoginManager.LoginAsync(token, user.Id);
                        user.Token = token;
                        LogHelper.WriteLog("用户ID：" + user.Id);
                        return AjaxHelper.JsonResult(HttpStatusCode.OK, "登录成功", user);
                    }
                    else
                    {
                        //未注册则先插入数据库，随后登录
                        var user = new AppUserDto
                        {
                            RealName = jd.data.name,
                            UserName = jd.data.userName,
                            Picture = jd.data.head_pic,
                            Phone = jd.data.mobile,
                            Email = jd.data.email,
                            IdCard = jd.data.idcard,
                            PassWord = (password.ToMd5() + salt).ToMd5(),
                            Salt = salt
                        };
                        var id = _iAppUserService.InsertAndGetIdAsync(user);
                        user.Id = await id;
                        user.Token = token;
                        await LoginManager.LoginAsync(token, user.Id);
                        return AjaxHelper.JsonResult(HttpStatusCode.OK, "登录成功", user);
                    }

                }
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "请登录");
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog("报错信息：" + ex.Message);
                return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "授权失败", ex.Message);
            }
        }

        /// <summary>
        /// 根据username获取用户。200获取成功
        /// </summary>
        /// <param name="username">用户username</param>
        /// <returns></returns>
        //[Route("getUser")]
        //[HttpGet]
        //public async Task<JsonResult> GetUserById(string username)
        //{
        //    var result = await _iAppUserService.SingleAsync(n => n.UserName == username);
        //    return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        //}

        [Route("getUser")]
        [HttpGet]
        public JsonResult GetUserById(string username)
        {
            var result = _iAppUserService.GetUserInfo(username);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", result);
        }


        [Route("EditRealName")]
        [HttpPost]
        public async Task<JsonResult> EditRealName(string username, string realName)
        {
            var result = await _iAppUserService.SingleAsync(n => n.UserName == username);
            result.RealName = realName;
            await _iAppUserService.UpdateAsync(result);
            return AjaxHelper.JsonResult(HttpStatusCode.OK, "修改成功");
        }

        /// <summary>
        /// 客户端根据token获取用户信息
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [Route("getUserInfoByRedis")]
        [HttpGet]
        public async Task<JsonResult> GetUserInfoByRedis(string token)
        {
            try
            {
                //根据token获取userid
                var userId = (await LoginManager.GetUserIdAsync(token)).GetValueOrDefault();
                //根据userid获取用户信息
                var user = await _iAppUserService.GetAsync(userId);
                return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", user);
            }
            catch (Exception)
            {
                return AjaxHelper.JsonResult(HttpStatusCode.InternalServerError, "失败", null);
            }
        }


        //[Route("GetUserById2")]
        //[HttpGet]
        //public async Task<JsonResult> GetUserById2(string username)
        //{
        //    bool isExist = _iAppUserService.CheckUserName(username);
        //    string token = Guid.NewGuid().ToString();
        //    if (true)
        //    {
        //        var user = await _iAppUserService.LoginByRealName(username);
        //        await LoginManager.LoginAsync(token, user.Id);
        //        user.Token = token;
        //    }
        //    return AjaxHelper.JsonResult(HttpStatusCode.OK, "成功", isExist);
        //}
    }
}