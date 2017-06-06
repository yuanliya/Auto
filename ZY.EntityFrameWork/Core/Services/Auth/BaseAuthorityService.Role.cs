using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.DBHelper;
using System.Linq.Expressions;

namespace ZY.EntityFrameWork.Core.Services
{
    public partial class BaseAuthorityService
    {
        /// <summary>
        /// 查询所有角色信息
        /// </summary>
        /// <returns></returns>
        public List<Role> GetAllRoles()
        {
            return roleRepository.GetQueryable().ToList();
        }

        public int AddRole(Role role)
        {
            if (roleRepository.CheckExists(q => q.RoleName == role.RoleName))
            {
                throw new Exception("已存在的角色名！");
            }
            return roleRepository.Insert(role);
        }

        public int DeleteRole(Role role)
        {
            roleModuleRepository.Delete(q => q.RoleId == role.ID, false);
            userRepository.Delete(q => q.RoleId == role.ID);
            roleRepository.Delete(role.ID, false);
            return Context.Commit();
        }

        public int UpdateRole(Role role)
        {
            if (roleRepository.CheckExists(q => (q.RoleName == role.RoleName)&&q.ID!=role.ID))
            {
                throw new Exception("已存在的角色名！");
            }
            return roleRepository.Update(role);
        }

        /// <summary>
        /// 查找所有拥有该模块权限的角色
        /// </summary>
        /// <param name="moduleTag"></param>
        /// <returns></returns>
        public List<Role> GetRoleByPermission(string moduleTag)
        {
            return roleModuleRepository.Find(q => q.Module.ModuleTag == moduleTag).Select(q => q.Role).ToList();
        }

        /// <summary>
        /// 查找角色
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Role GetRoleByName(string name)
        {
            return roleRepository.FindSingle(q => q.RoleName == name);
        }

        /// <summary>
        /// 查找角色拥有权限的模块信息
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public List<RoleModule> GetRolePermissions(string roleName)
        {
            Role role = roleRepository.FindSingle(q => q.RoleName == roleName);
            return role == null ? null : role.RoleModules.ToList();
        }

        /// <summary>
        /// 更新用户权限
        /// </summary>
        /// <param name="roleModules"></param>
        /// <returns></returns>
        public int UpdateRole(List<RoleModule> roleModules)
        {
            foreach (RoleModule roleModule in roleModules)
            {
                RoleModule old = roleModuleRepository.FindSingle(q => q.ModuleId == roleModule.ModuleId && q.RoleId == roleModule.RoleId);
                if (old == null )
                {
                    if(roleModule.Enabled)
                    roleModuleRepository.Insert(roleModule, false); // 新增权限
                }
                else if (!roleModule.Enabled)
                {
                    roleModuleRepository.Delete(old.ID, false);//删除未授权的条目
                }
                else
                {
                    old.Enabled = roleModule.Enabled;
                    old.Permissions = roleModule.Permissions;
                    roleModuleRepository.Update(old, false);        // 修改已存在权限
                }
            }

            return Context.Commit();
        }

        //按条件删除
        public int DeleteRoleModules(List<QueryCondition> searchItems)
        {
            // 构建Lamda表达式
            Expression<Func<RoleModuleDto, bool>> ep = LamdaExpressionHelper.BuildExpression<RoleModuleDto>(searchItems);
            return roleModuleRepository.Delete(searchItems);
        }

    }
}
