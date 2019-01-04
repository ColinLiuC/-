using CoreSolution.Domain.Entities;
using CoreSolution.Tools.Extensions;

namespace CoreSolution.EntityFrameworkCore.Test
{
    /// <summary>
    /// 种子数据
    /// </summary>
    public static class SeedData
    {
        public static void Initialize(EfCoreDbContext context)
        {
            var user = new User
            {
                UserName = "admin",
                RealName = "admin",
                Salt = "123456",
                Password = ("123456".ToMd5() + "123456").ToMd5()
            };
            var role = new Role
            {
                Name = "Admin",
                Description = "管理员"
            };
            var userRole = new UserRole
            {
                User = user,
                Role = role
            };
            var permission = new Permission
            {
                DisplayName = "All",
                TargetName ="All",
                Description = "All"
               
            };
            var people = new People
            {
                PeopleNum = "123456",
                PassWord = "123456",
                PeopleName = "张三",
                PeopleTell = "123456789",
                PeopleMail = "123@qq.com",
                PeopleCard = "PeopleCard",
                PeoplePicture = "PeoplePicture"
            };
            //context.Users.Add(user);
            //context.Roles.Add(role);
            //context.UserRoles.Add(userRole);
            //context.Permissions.Add(permission);
            context.SaveChanges();
        }
    }
}
