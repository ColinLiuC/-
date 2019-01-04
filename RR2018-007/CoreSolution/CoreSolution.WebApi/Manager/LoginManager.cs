using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreSolution.Redis.Helper;

namespace CoreSolution.WebApi.Manager
{
    public class LoginManager
    {
        private static string TOKEN_PREFIX = "Api.User.Token.";
        private static string USERID_PREFIX = "Api.User.UserId.";
        private static string USERPERMISSIONS_PREFIX = "Api.User.Permissions.";
        private static string USERROLES_PREFIX = "Api.User.Roles.";

        private static string USERSTREETID_PREFIX = "Api.User.UserStreetId.";
        private static string USERSTATIONID_PREFIX = "Api.User.UserStationId.";



        public static async Task LoginAsync(string token, Guid userId)
        {
            await RedisHelper.StringSetAsync(TOKEN_PREFIX + token, userId, TimeSpan.FromHours(3));
            await RedisHelper.StringSetAsync(USERID_PREFIX + userId + "." + token, token, TimeSpan.FromHours(3));//正向、反向关系都保存，这样保证一个token只能登陆一次
        }

        /// <summary>
        /// 缓存当前用户所具有的权限
        /// </summary>
        /// <param name="permissions">权限数组</param>
        /// <param name="userId">用户id</param>
        /// <returns></returns>
        public static async Task SaveCurrentUserPermissionsAsync(string[] permissions, Guid userId)
        {
            await RedisHelper.StringSetAsync(USERPERMISSIONS_PREFIX + userId, permissions, TimeSpan.FromHours(3));
        }

        public static async Task SaveCurrentUserRolesAsync(string[] roles, Guid userId)
        {
            await RedisHelper.StringSetAsync(USERROLES_PREFIX + userId, roles, TimeSpan.FromHours(3));

        }

        /// <summary>
        /// 缓存用户街道ID
        /// </summary>
        /// <param name="streetId"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task SaveCurrentUserStreetId(Guid? streetId, Guid userId,string token
            )
        {
            await RedisHelper.StringSetAsync(USERSTREETID_PREFIX + userId+"."+ token, streetId, TimeSpan.FromHours(24));
        }

        /// <summary>
        /// 缓存用户驿站ID
        /// </summary>
        /// <param name="stationId"></param>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task SaveCurrentUserStationId(Guid? stationId, Guid userId,string token)
        {
            await RedisHelper.StringSetAsync(USERSTATIONID_PREFIX + userId + "." + token, stationId, TimeSpan.FromHours(24));
        }

        public static async Task<string[]> GetCurrentUserPermissionsAsync(Guid userId)
        {
            return await RedisHelper.StringGetAsync<string[]>(USERPERMISSIONS_PREFIX + userId);
        }

        public static async Task<string[]> GetCurrentUserRolesAsync(Guid userId)
        {
            return await RedisHelper.StringGetAsync<string[]>(USERROLES_PREFIX + userId);
        }

        public static async Task<Guid?> GetUserIdAsync(string token)
        {
            Guid? userId = await RedisHelper.StringGetAsync<Guid?>(TOKEN_PREFIX + token);
            if (userId == null)
            {
                return null;
            }
            string revertToken = await RedisHelper.StringGetAsync(USERID_PREFIX + userId + "." + token);
            if (revertToken != token)//如果反向查的token不一样，说明这个token已经过期了
            {
                return null;
            }
            return userId;
        }


        public static async Task<Guid?> GetCurrentUserStreetIdAsync(Guid userId,string token)
        {
            Guid? result = await RedisHelper.StringGetAsync<Guid?>(USERSTREETID_PREFIX + userId+"."+token);
            if (result != null && result.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                return null;
            }
            return result;
        }

        public static async Task<Guid?> GetCurrentUserStationIdAsync(Guid userId, string token)
        {
            Guid? result = await RedisHelper.StringGetAsync<Guid?>(USERSTATIONID_PREFIX + userId + "." + token);
            if (result != null && result.ToString() == "00000000-0000-0000-0000-000000000000")
            {
                return null;
            }
            return result;
        }

        /// <summary>
        /// 得到错误登录次数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        public static async Task<int> GetErrorLoginTimesAsync(string redisKey, string email)
        {
            string key = redisKey + email;
            int? count = await RedisHelper.StringGetAsync<int?>(key);
            if (count == null)
            {
                count = 0;
            }
            return (int)count;
        }

        /// <summary>
        /// 重置登录错误次数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="email"></param>
        public static void ResetErrorLogin(string redisKey, string email)
        {
            string key = redisKey + email;
            RedisHelper.KeyRemove(key);
        }

        /// <summary>
        /// 递增登录错误次数
        /// </summary>
        /// <param name="redisKey"></param>
        /// <param name="email"></param>
        public static async Task IncreaseErrorLoginAsync(string redisKey, string email)
        {
            string key = redisKey + email;
            int? count = await RedisHelper.StringGetAsync<int?>(key) ?? 0;
            count++;
            await RedisHelper.StringSetAsync(key, count, TimeSpan.FromMinutes(15));//超时时间15分钟
        }
    }
}
