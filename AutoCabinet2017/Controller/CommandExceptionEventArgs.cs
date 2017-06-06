using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace AutoCabinet2017.Controller
{
    /// <summary>
    /// 命令异常处理的事件参数
    /// </summary>
    public class CommandExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception">异常内容</param>
        /// <param name="functionName">引发异常的函数</param>
        public CommandExceptionEventArgs(Exception exception, int devNo, string cmdType)
        {
            Exception = exception;
            DevNo     = devNo;   
            CmdType   = cmdType;
        }

        // 异常内容
        public Exception Exception { get; private set; }
        // 引发异常的设备编号
        public int DevNo { get; private set; }
        // 引发异常的命令
        public string CmdType { get; private set; }
    }

    /// <summary>
    /// 命令正常处理的事件参数
    /// </summary>
    public class CommandExecuteEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="exception">异常内容</param>
        /// <param name="functionName">引发异常的函数</param>
        public CommandExecuteEventArgs(int devNo, string cmdType)
        {
            DevNo   = devNo;   
            CmdType = cmdType;
        }

        // 设备编号
        public int DevNo { get; private set; }
        // 命令
        public string CmdType { get; private set; }
    }
}
