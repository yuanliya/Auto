using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Caller.Facade
{
    public interface IAuthorityService
    {
        #region User
        int Add(UserDto userDto);

        int Delete(UserDto user);

        int Delete(List<UserDto> users);

        UserDto CheckUser(string userCode, string passWord);

        List<UserDto> GetAllUsers();

        List<UserDto> GetRoleUsers(string roleName);

        List<UserDto> FindUsers(List<QueryCondition> searchItems);

        int Update(UserDto user);

        int Update(List<UserDto> users);
        #endregion

        #region Module
        List<ModuleDto> GetAllModules();

        int UpdateModules(List<ModuleDto> modules);
        #endregion

        #region Role
        RoleDto GetRoleByName(string name);
        List<RoleDto> GetAllRoles();

        int AddRole(RoleDto role);

        int DeleteRole(RoleDto role);

        int UpdateRole(RoleDto role);

        List<RoleModuleDto> GetRolePermissions(string roleName);

        /// <summary>
        /// 更新用户权限
        /// </summary>
        /// <param name="roleModules"></param>
        /// <returns></returns>
        int UpdateRole(List<RoleModuleDto> dtos);

        int DeleteRoleModules(List<QueryCondition> searchItems);
        #endregion

        #region Log
        List<LogDto> GetAllLog();

        List<LogDto> GetLog(IList<QueryCondition> condition);

        //int DeleteLogItems(Expression<Func<LogDto, bool>> exp);

        int DeleteRecentLog();
        #endregion
    }
}