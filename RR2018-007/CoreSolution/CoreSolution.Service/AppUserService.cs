using CoreSolution.Domain.Entities;
using CoreSolution.Domain.Enum;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.Service
{
    public class AppUserService : EfCoreRepositoryBase<AppUser, AppUserDto, Guid>, IAppUserService
    {

        public Task<bool> CheckUserNameDupAsync(string username)
        {
            return AnyAsync(i => i.UserName == username);
        }

        public Task<bool> CheckPhoneDupAsync(string phone)
        {
            return AnyAsync(i => i.Phone == phone);
        }

        public Task<bool> CheckEmailDupAsync(string email)
        {
            return AnyAsync(i => i.Email == email);
        }


        public async Task<LoginResults> CheckUserPasswordAsync(string phoneorname, string password)
        {
            var appUserDto = GetUserByUserNameOrPhone(phoneorname);
            if (appUserDto != null)
            {
                if (appUserDto.PassWord == (password.ToMd5() + appUserDto.Salt).ToMd5())
                {
                    return LoginResults.Success;
                }
                return LoginResults.PassWordError;
            }
            else
            {
                return LoginResults.NotExist;
            }
        }

        public AppUser GetUserByUserNameOrPhone(string phoneOrName)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                return db.Set<AppUser>().FirstOrDefault(p=>p.UserName==phoneOrName||p.Phone==phoneOrName);
            }
        }


        public async Task<AppUserDto> GetUserByUserNameOrPhoneAsync(string phoneOrName)
        {
            return await SingleOrDefaultAsync(i => (i.Phone == phoneOrName || i.UserName == phoneOrName));
        }

        public Task<bool> CheckUserRealName(string realname)
        {
            return AnyAsync(i => i.RealName == realname);
        }

        public async Task<AppUserDto> GetUserByUserNameOrIdCartAsync(string userNameOrIdCart)
        {
            return await SingleOrDefaultAsync(i => (i.IdCard == userNameOrIdCart || i.UserName == userNameOrIdCart));
        }

        public async Task<AppUserDto> LoginByUserName(string username)
        {
            return await SingleOrDefaultAsync(i => i.UserName == username);
        }
        public async Task<AppUserDto> GetAppUserDtoByIdCard(string idcart)
        {
            return await SingleOrDefaultAsync(i => i.IdCard == idcart);
        }

        public bool CheckUserName(string username)
        {
            var user = GetAll().Where(i => i.UserName == username).FirstOrDefault();
            if (user != null)
            {
                return true;
            }
            return false;
        }


        public AppUser GetUserInfo(string username)
        {
            using (EfCoreDbContext db = new EfCoreDbContext())
            {
                var info = db.Set<AppUser>().FirstOrDefault(p => p.UserName == username);
                if (info != null)
                {
                    return info;
                }
                return null;
            }
        }
    }
}
