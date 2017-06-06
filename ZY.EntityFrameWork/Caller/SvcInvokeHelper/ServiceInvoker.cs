using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using TecX.Expressions;

namespace ZY.EntityFrameWork.Caller.SvcInvokeHelper
{
    public class ServiceInvoker
    {
        // 保存客户端到服务终结点通道的字典变量
        private static Dictionary<string, ChannelFactory> channelFactories = new Dictionary<string, ChannelFactory>();
        // 处理同步操作的变量
        private static object syncHelper = new object();

        /// <summary>
        /// 根据服务终结点信息获得客户端到服务端的通道工厂
        /// </summary>
        /// <typeparam name="TChannel">服务端的服务接口</typeparam>
        /// <param name="endpointConfigurationName">服务终结点</param>
        /// <returns></returns>
        public static ChannelFactory<TChannel> GetChannelFactory<TChannel>(string endpointConfigurationName)
        {
            ChannelFactory<TChannel> channelFactory = null;

            // 如果存在该服务终结点的服务通道
            if (channelFactories.ContainsKey(endpointConfigurationName))
            {
                // 直接从字典取出服务通道对象
                channelFactory = channelFactories[endpointConfigurationName] as ChannelFactory<TChannel>;
            }

            // 如果不存在该服务终结点的服务通道
            if (channelFactory == null)
            {
                // 生成对应该服务终结点的服务通道
                channelFactory = new ChannelFactory<TChannel>(endpointConfigurationName);

                //https://outlawtrail.wordpress.com/2013/06/10/using-expressions-in-wcf/
                //有这个就可以自由地用Linq作为WCF的方法参数了
                channelFactory.Endpoint.Behaviors.Add(new ClientSideSerializeExpressionsBehavior());
                
                // 锁定以下操作
                lock (syncHelper)
                {
                    // 保存服务通道到字典
                    channelFactories[endpointConfigurationName] = channelFactory;
                }
            }

            return channelFactory;
        }

        /// <summary>
        /// 执行没有返回值的服务调用
        /// </summary>
        /// <typeparam name="TChannel">泛型表示的服务接口</typeparam>
        /// <param name="action">服务接口为参数的泛型委托</param>
        /// <param name="proxy">服务接口</param>
        public static void Invoke<TChannel>(Action<TChannel> action, TChannel proxy)
        {
            // 定义基本状态机协定的通信对象
            ICommunicationObject channel = proxy as ICommunicationObject;
            
            if (channel == null)
            {
                throw new ArgumentException("The proxy is not a valid channel implementing the ICommunicationObject interface", "proxy");
            }

            // 打开客户端到服务端的连接通道
            channel.Open();
            
            try
            {
                // 调用服务端的服务
                action(proxy);
                // 关闭通道
                channel.Close();
            }
            catch (TimeoutException)
            {
                // 操作超时
                channel.Abort();
                throw;
            }
            catch (CommunicationException)
            {
                // 通信故障
                channel.Abort();
                throw;
            }
            finally
            {
                // 关闭通道
                channel.Close();
            }
        }

        /// <summary>
        /// 执行带返回值的服务调用
        /// </summary>
        /// <typeparam name="TChannel">泛型表示的服务接口</typeparam>
        /// <typeparam name="TResult">泛型表示的返回值</typeparam>
        /// <param name="function">服务接口为参数的、带返回值的泛型委托</param>
        /// <param name="proxy">服务接口</param>
        /// <returns></returns>
        public static TResult Invoke<TChannel, TResult>(Func<TChannel, TResult> function, TChannel proxy)
        {
            ICommunicationObject channel = proxy as ICommunicationObject;

            if (channel == null)
            {
                throw new ArgumentException("The proxy is not a valid channel implementing the ICommunicationObject interface", "proxy");
            }

            // 打开服务通道
            channel.Open();

            try
            {
                // 执行服务操作
                return function(proxy);
            }
            catch (TimeoutException)
            {
                // 操作超时
                channel.Abort();
                throw;
            }
            catch (CommunicationException)
            {
                // 通信错误
                channel.Abort();
                throw;
            }
            finally
            {
                // 关闭服务通道
                channel.Close();
            }
        }

        /// <summary>
        /// 无返回参数的服务调用
        /// </summary>
        /// <typeparam name="TChannel">服务接口</typeparam>
        /// <param name="action">服务接口为参数的泛型委托</param>
        /// <param name="endpointConfigurationName">服务终结点</param>
        public static void Invoke<TChannel>(Action<TChannel> action, string endpointConfigurationName)
        {
            // 参数有效性判断
            Guard.ArgumentNotNullOrEmpty(endpointConfigurationName, "endpointConfigurationName");
            // 根据服务终结点生成服务通道和服务接口
            Invoke<TChannel>(action, GetChannelFactory<TChannel>(endpointConfigurationName).CreateChannel());
        }

        /// <summary>
        /// 带返回参数的服务调用
        /// </summary>
        /// <typeparam name="TChannel">服务接口</typeparam>
        /// <param name="function">服务接口为参数、带返回值的泛型委托</param>
        /// <param name="endpointConfigurationName">服务终结点</param>
        public static TResult Invoke<TChannel, TResult>(Func<TChannel, TResult> function, string endpointConfigurationName)
        {
            // 参数有效性判断
            Guard.ArgumentNotNullOrEmpty(endpointConfigurationName, "endpointConfigurationName");
            // 根据服务终结点生成服务通道，并调用服务接口，有返回值
            return Invoke<TChannel, TResult>(function, GetChannelFactory<TChannel>(endpointConfigurationName).CreateChannel());
        }        
    }
}
