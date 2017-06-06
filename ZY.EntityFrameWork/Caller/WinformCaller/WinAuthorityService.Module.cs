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
    /// 权限管理的服务接口--模块管理
    /// </summary>
    public partial class WinAuthorityService
    {
        /// <summary>
        /// 查询所有Module信息
        /// </summary>
        /// <returns>模块DTO对象List</returns>     
        public List<ModuleDto> GetAllModules()
        {
            return baseAuthorityService.GetAllModules().MapTo<List<ModuleDto>>();
        }

        public int UpdateModules(List<ModuleDto> modules)
        {
            return baseAuthorityService.UpdateModules(modules.MapTo<List<Module>>());
        }
    }
}
