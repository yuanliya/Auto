using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Services
{
    public partial class BaseAuthorityService
    {
        /// <summary>
        /// 查找所有模块信息
        /// </summary>
        /// <returns>结果集</returns>
        public List<Module> GetAllModules()
        {
            return moduleRepository.GetQueryable().ToList();
        }

        public List<RoleModule> GetAuthorizedRoles(string tag)
        {
            return moduleRepository.FindSingle(q => q.ModuleTag == tag).RoleModules.ToList();
        }

        public int UpdateModules(List<Module> modules)
        {
            moduleRepository.Update(modules, false);
            IEnumerable<string> disabled = modules.Where(q => !q.Enabled).Select(q => q.ID);
            roleModuleRepository.Delete(q => disabled.Contains(q.ModuleId));//删除角色中曾被授权但现在被禁用的模块
            return Context.Commit();
        }
       
    }
}
