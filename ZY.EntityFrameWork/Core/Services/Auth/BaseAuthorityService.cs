using System;
using System.Collections.Generic;
using System.Linq;
using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.Repositories.Interface;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.DBHelper;

namespace ZY.EntityFrameWork.Core.Services
{
    /// <summary>
    /// 权限相关的服务
    /// </summary>
    public partial class BaseAuthorityService : BaseRootService
    {
        public BaseAuthorityService(IUnitOfWorkContext unitContext, IUserRepository UserRepository, IRoleRepository RoleRepository,
            IModuleRepository ModuleRepository, IRoleModuleRepository RoleModuleRepository, ILogRepository LogRepository)
            : base(unitContext)
        {
            this.userRepository = UserRepository;
            this.roleRepository = RoleRepository;
            this.moduleRepository = ModuleRepository;
            this.roleModuleRepository = RoleModuleRepository;
        }

        public BaseAuthorityService()
        {
            userRepository = UnityHelper.Instance.Resolve<IUserRepository>();
            roleRepository = UnityHelper.Instance.Resolve<IRoleRepository>();
            moduleRepository = UnityHelper.Instance.Resolve<IModuleRepository>();
            roleModuleRepository = UnityHelper.Instance.Resolve<IRoleModuleRepository>();
            logRepository = UnityHelper.Instance.Resolve<ILogRepository>();
        }

        private IUserRepository userRepository;
        private IRoleRepository roleRepository;
        private IModuleRepository moduleRepository;
        private IRoleModuleRepository roleModuleRepository;
        private ILogRepository logRepository;
    }
}
