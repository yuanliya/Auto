using System;
using System.Collections.Generic;
using System.Linq;

using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Services;

namespace ZY.EntityFrameWork.Caller.WinformCaller
{
    /// <summary>
    /// 权限管理的服务接口--角色管理
    /// </summary>
    public partial class WinAuthorityService
    {
        /// <summary>
        /// 根据角色名查找角色信息
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns>角色DTO对象</returns>
        public RoleDto GetRoleByName(string roleName)
        {
            return baseAuthorityService.GetRoleByName(roleName).MapTo<RoleDto>();
        }

        /// <summary>
        /// 查找所有角色信息
        /// </summary>
        /// <returns>角色DTO对象List</returns>
        public List<RoleDto> GetAllRoles()
        {
            return baseAuthorityService.GetAllRoles().MapTo<List<RoleDto>>();
        }

        /// <summary>
        /// 添加新角色
        /// </summary>
        /// <param name="role">角色DTO对象</param>
        /// <returns>操作是否成功</returns>
        public int AddRole(RoleDto role)
        {
            return baseAuthorityService.AddRole(role.MapTo<Role>());
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="role">角色DTO对象</param>
        /// <returns>操作是否成功</returns>
        public int DeleteRole(RoleDto role)
        {
            return baseAuthorityService.DeleteRole(role.MapTo<Role>());
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">角色DTO对象</param>
        /// <returns>操作是否成功</returns>
        public int UpdateRole(RoleDto role)
        {
            return baseAuthorityService.UpdateRole(role.MapTo<Role>());
        }

        /// <summary>
        /// 更新角色拥有的模块
        /// </summary>
        /// <param name="role">角色模块DTO对象</param>
        /// <returns>操作是否成功</returns>
        public int UpdateRole(List<RoleModuleDto> dtos)
        {
            return baseAuthorityService.UpdateRole(dtos.MapTo<List<RoleModule>>());
        }

        /// <summary>
        /// 查找角色拥有的权限
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns>角色模块DTO对象</returns>
        public List<RoleModuleDto> GetRolePermissions(string roleName)
        {
            return baseAuthorityService.GetRolePermissions(roleName).MapTo<List<RoleModuleDto>>();
        }

        public int DeleteRoleModules(List<QueryCondition> searchItems)
        {
            return baseAuthorityService.DeleteRoleModules(searchItems);
        }
    }
}
