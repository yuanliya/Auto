using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using ZY.EntityFrameWork.Caller.SvcInvokeHelper;
using ZY.EntityFrameWork.Core.DBHelper;

namespace ZY.EntityFrameWork.Caller.WcfCaller
{
    /// <summary>
    /// 服务代理基类
    /// </summary>
    /// <typeparam name="TChannel">泛型类，代表服务接口</typeparam>
    public class ServiceProxyBase<TChannel>
    {
        public virtual SvcInvoker<TChannel> Invoker
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="endpointConfigurationName">服务终结点</param>
        public ServiceProxyBase(string endpointConfigurationName)
        {
            // 参数有效性检验
            Guard.ArgumentNotNullOrEmpty(endpointConfigurationName, "endpointConfigurationName");
            // 根据服务终结点生成服务调用对象
            this.Invoker = new SvcInvoker<TChannel>(endpointConfigurationName);
        }

        /// <summary>
        /// 默认无参构造函数
        /// </summary>
        public ServiceProxyBase()
        {
        }


        
    }
}
