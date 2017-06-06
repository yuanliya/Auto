using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

using ZY.EntityFrameWork.Caller.SvcInvokeHelper;
using ZY.EntityFrameWork.Core.Context;
using ZY.EntityFrameWork.Core.DBHelper;

namespace ZY.EntityFrameWork.Core.Services
{
    public abstract class BaseRootService
    {
        // 数据库访问的上下文
        private readonly IUnitOfWorkContext context;

        public IUnitOfWorkContext Context
        {
            get { return this.context; }
        }

        public BaseRootService()
        {
            // 从容器中提取注册好的上下文实例
            context = UnityHelper.Instance.Resolve<IUnitOfWorkContext>();
        }

        public BaseRootService(IUnitOfWorkContext unitContext)
        {
            // 直接设置上下文实例
            context = unitContext;
        }
    }
}
