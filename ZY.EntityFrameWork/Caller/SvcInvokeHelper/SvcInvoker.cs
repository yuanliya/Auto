using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Caller.SvcInvokeHelper
{
    public class SvcInvoker<TChannel> : ServiceInvoker
    {
        public string EndpointConfigurationName
        {
            get; 
            private set;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="endpointConfigurationName">服务终结点</param>
        public SvcInvoker(string endpointConfigurationName)
        {
            // 参数有效性检验
            Guard.ArgumentNotNullOrEmpty(endpointConfigurationName, "endpointConfigurationName");
            // 初始化服务终结点信息
            this.EndpointConfigurationName = endpointConfigurationName;
        }

        /// <summary>
        /// 执行服务调用
        /// </summary>
        /// <param name="action">服务调用的泛型委托</param>
        public void Invoke(Action<TChannel> action)
        {
            Invoke<TChannel>(action, this.EndpointConfigurationName);
        }

        /// <summary>
        /// 执行带返回值的服务调用
        /// </summary>
        /// <typeparam name="TResult">返回值</typeparam>
        /// <param name="function">服务调用的泛型委托</param>
        /// <returns></returns>
        public TResult Invoke<TResult>(Func<TChannel, TResult> function)
        {
            return Invoke<TChannel, TResult>(function, this.EndpointConfigurationName);
        }
    }
}
