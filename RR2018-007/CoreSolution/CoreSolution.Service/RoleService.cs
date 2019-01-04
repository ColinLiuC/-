using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreSolution.Domain.Entities;
using CoreSolution.Dto;
using CoreSolution.EntityFrameworkCore.Repositories;
using CoreSolution.IService;
using CoreSolution.Tools;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace CoreSolution.Service
{
    public sealed class RoleService : EfCoreRepositoryBase<Role, RoleDto, Guid>, IRoleService
    {
        public override IQueryable<Role> GetAllIncluding()
        {
            return GetAll()
                .Include(i => i.CreatorUser)
                .Include(i => i.DeleterUser)
                .Include(i => i.UserRoles);
        }

        public override async Task<RoleDto> InsertAsync(RoleDto entityDto)
        {
            if (entityDto != null)
            {
                bool r = Any(i => i.Name == entityDto.Name);
                if (!r)
                {
                    await _dbContext.Roles.AddAsync(Mapper.Map<Role>(entityDto));
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception($"已存在name={entityDto.Name}的角色");
                }
            }
            return entityDto;
        }

        public Task<bool> CheckRoleNameDupAsync(string roleName)
        {
            return AnyAsync(i => i.Name == roleName);
        }

        public async Task<IList<RoleDto>> GetRolesByUserIdAsync(Guid userId)
        {
            var roleIds = await _dbContext.UserRoles
                .Where(i => i.UserId == userId)
                .Select(i => i.RoleId)
                .ToListAsync();
            return await GetAll()
                .Where(i => roleIds.Contains(i.Id))
                .ProjectTo<RoleDto>()
                .ToListAsync();
        }

        public async Task<IList<Guid>> GetPermissionIdsAsync(Guid roleId)
        {
            var permissionIds = await _dbContext.RolePermissions
                .Where(i => i.RoleId == roleId)
                .Select(i => i.PermissionId)
                .ToListAsync();
            return permissionIds;
         
        }

        public DataSet GetAllPermissionsForTree()
        {
            string sql = "select AA.*,BB.Id as PermissionId  from(  SELECT d.ChildrenNumber,c.* FROM T_Menus c LEFT JOIN " +
                         "(SELECT a.Id, COUNT(b.ParentId) ChildrenNumber FROM T_Menus a LEFT JOIN T_Menus b on a.Id = b.ParentId GROUP BY a.Id) as d " +
                         "ON c.Id = d.Id WHERE c.[IsDeleted] = 0) AA  left join T_Permissions BB on AA.PermissionTarget=BB.TargetName ";
            var cfg = new ConfigurationBuilder().Add(new JsonConfigurationSource { Path = "configuration.json", ReloadOnChange = true }).Build();
            var connStr = cfg.GetSection("connStr");

            return DBHelper.ExecuteQuery(sql, connStr.Value); ;


        }

        public async void SetRoleHasMenuPermissions(Guid roleId, List<Guid> permissionIds)
        {
            var oldpermissions = await _dbContext.Permissions.Where(i=>i.permissionType==1).Select(i => i.Id)
                .ToListAsync();

            var oldpermissionRoles = await _dbContext.RolePermissions.Where(i => oldpermissions.Contains(i.PermissionId) && i.RoleId == roleId).ToListAsync();

            oldpermissionRoles.ForEach(oldpermission => { _dbContext.RolePermissions.Remove(oldpermission); });
             permissionIds.ForEach(permissionid => {
                _dbContext.RolePermissions.AddAsync(new RolePermission { RoleId = roleId, PermissionId = permissionid });
               
            });

            _dbContext.SaveChanges();
           
        }

        public async void SetRolePermissions(Guid roleId, List<Guid> permissionIds)
        {
            var oldpermissions = await _dbContext.Permissions.Where(i => i.permissionType != 1).Select(i => i.Id)
                .ToListAsync();

            var oldpermissionRoles = await _dbContext.RolePermissions.Where(i => oldpermissions.Contains(i.PermissionId) && i.RoleId == roleId).ToListAsync();

            oldpermissionRoles.ForEach(oldpermission => { _dbContext.RolePermissions.Remove(oldpermission); });
            permissionIds.ForEach(permissionid => {
                _dbContext.RolePermissions.AddAsync(new RolePermission { RoleId = roleId, PermissionId = permissionid });

            });

            _dbContext.SaveChanges();

        }

    }
}
