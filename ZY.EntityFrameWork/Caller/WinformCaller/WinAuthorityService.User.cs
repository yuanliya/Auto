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
    /// 权限管理的服务接口--用户管理
    /// </summary>
    public partial class WinAuthorityService : IAuthorityService
    {
        private BaseAuthorityService baseAuthorityService = new BaseAuthorityService();

        /// <summary>
        /// 添加新用户
        /// </summary>
        /// <param name="userDto">用户DTO对象</param>
        /// <returns></returns>
        public int Add(UserDto userDto)
        {
            if (userDto.RoleId == null && string.IsNullOrEmpty(userDto.RoleName))
            {
                // 用户的角色
                Role role = baseAuthorityService.GetUserRole(userDto.RoleName);
                if (role == null)
                {
                    throw new Exception("角色不存在！");
                }

                // 添加用户的角色
                userDto.RoleId = role.ID;
            }
           
            // 执行DTO到Entity的映射，并添加新用户
            return baseAuthorityService.Add(userDto.MapTo<User>());
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="user">用户的DTO对象</param>
        /// <returns></returns>
        public int Delete(UserDto user)
        {
            return baseAuthorityService.Delete(user.MapTo<User>());
        }

        /// <summary>
        /// 批量删除用户
        /// </summary>
        /// <param name="users">用户DTOList</param>
        /// <returns></returns>
        public int Delete(List<UserDto> users)
        {
            return baseAuthorityService.Delete(users.MapTo<List<User>>());
        }

        /// <summary>
        /// 用户登录效验
        /// </summary>
        /// <param name="userCode">登录名</param>
        /// <param name="passWord">密码</param>
        /// <returns>用户DTO对象</returns>
        public UserDto CheckUser(string userCode, string passWord)
        {
            return baseAuthorityService.CheckUser(userCode, passWord).MapTo<UserDto>();
        }

        /// <summary>
        /// 查询所有用户
        /// </summary>
        /// <returns>用户DTO对象List</returns>
        public List<UserDto> GetAllUsers()
        {
            return baseAuthorityService.GetAllUsers().MapTo<List<UserDto>>();
        }

        /// <summary>
        /// 查找角色包含的所有用户
        /// </summary>
        /// <param name="roleName">角色名</param>
        /// <returns>用户DTO对象List</returns>
        public List<UserDto> GetRoleUsers(string roleName)
        {
            return baseAuthorityService.GetRoleUsers(roleName).MapTo<List<UserDto>>();
        }

        /// <summary>
        /// 查找用户
        /// </summary>
        /// <param name="searchItems">查询条件</param>
        /// <returns>用户DTO对象List</returns>
        public List<UserDto> FindUsers(List<QueryCondition> searchItems)
        {
            return baseAuthorityService.FindUsers(searchItems).MapTo<List<UserDto>>();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="user">用户DTO对象</param>
        /// <returns>操作是否成功</returns>
        public int Update(UserDto user)
        {
            return baseAuthorityService.Update(user.MapTo<User>());
        }

        /// <summary>
        /// 批量更新用户信息
        /// </summary>
        /// <param name="user">用户DTO对象List</param>
        /// <returns>操作是否成功</returns>
        public int Update(List<UserDto> users)
        {
            return baseAuthorityService.Update(users.MapTo<List<User>>());
        }
    }
}
