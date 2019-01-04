using CoreSolution.Domain.Entities;
using CoreSolution.Domain.Enum;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService.Convention;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CoreSolution.IService
{
    public interface IAppUserService : IEfCoreRepository<AppUser, AppUserDto>, IServiceSupport
    {

        bool CheckUserName(string username);
        Task<bool> CheckUserNameDupAsync(string userName);
        Task<bool> CheckPhoneDupAsync(string phone);
        Task<bool> CheckEmailDupAsync(string email);

        Task<LoginResults> CheckUserPasswordAsync(string phoneOrName, string password);

        Task<AppUserDto> GetUserByUserNameOrPhoneAsync(string phoneorname);

        Task<AppUserDto> GetUserByUserNameOrIdCartAsync(string userNameOrIdCart);

        Task<bool> CheckUserRealName(string realname);

        Task<AppUserDto> LoginByUserName(string realname);
        Task<AppUserDto> GetAppUserDtoByIdCard(string idcart);
        AppUser GetUserInfo(string username);

    }
}
