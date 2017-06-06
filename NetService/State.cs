using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NJUST.AUTO06.NetService
{
    /// <summary>
    /// 以太网异步操作的状态类
    /// </summary>
    public class StateObject
    {
        // Socket对象
        public Socket workSocket { get; private set; }
        // 存储数据的数组
        public byte[] Buffer { get; private set; }
        // 接收缓冲区大小  
        public int BufferSize {get; private set;}
        // 保存接收的字符串  
        public StringBuilder sb = new StringBuilder();
        // 远端节点
        public EndPoint remoteEP = null;

        /// <summary>
        /// 带参数的构造函数
        /// </summary>
        /// <param name="socket">以太网的Socket对象</param>
        /// <param name="buffer">保存数据的数组</param>
        public StateObject(Socket socket, byte[] buffer)
        {
            workSocket = socket;
            Buffer     = buffer;
            BufferSize = buffer.Length;
        }     
    }
}
