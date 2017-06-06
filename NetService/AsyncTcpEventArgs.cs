using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace NJUST.AUTO06.NetService
{
    /// <summary>
    /// 网络连接的事件参数
    /// </summary>
    public class ConnectEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="remoteEP">远程站点</param>
        /// <param name="localEP">本地站点</param>
        public ConnectEventArgs(EndPoint remoteEP, EndPoint localEP)
        {
            RemoteEndPoint = remoteEP;
            LocalEndPoint  = localEP;
        }

        // 远程站点
        public EndPoint RemoteEndPoint { get; private set; }
        // 本地站点
        public EndPoint LocalEndPoint { get; private set; }
    }

    /// <summary>
    /// 网络传输IO的事件参数 
    /// </summary>
    public class IOEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataframe">数据帧</param>
        /// <param name="remoteEP">远程站点</param>
        public IOEventArgs(byte[] dataframe,EndPoint remoteEP)
        {
            Dataframe = dataframe;
            RemoteEP = remoteEP;
        }

        // 传输的数据帧
        public byte[] Dataframe { get; private set; }
        // 远程站点
        public EndPoint RemoteEP { get; private set; }
    }

    /// <summary>
    /// 异常处理的事件参数
    /// </summary>
    public class ExceptEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception">异常内容</param>
        /// <param name="functionName">引发异常的函数</param>
        public ExceptEventArgs(Exception exception, string functionName)
        {
            Exception    = exception;
            FunctionName = functionName;
        }

        // 异常内容
        public Exception Exception { get; private set; }
        // 引发异常的函数
        public string FunctionName { get; private set; }
    }
}
