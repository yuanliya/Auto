using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace NJUST.AUTO06.Modbus.Comm
{
    public class ModbusUDPEthernet : ICommunicationProxy
    {
        /// <summary>
        /// 以太网读写异步操作的状态对象
        /// </summary>
        class StateObject
        {
            // 客户端Socket对象 
            public Socket workSocket = null;
            // 接收缓冲区大小  
            public const int BufferSize = 2048;
            // 接收缓冲区 
            public byte[] buffer = new byte[BufferSize];
            // 保存接收的字符串  
            public StringBuilder sb = new StringBuilder();
            // 远端节点
            public EndPoint remotePoint = null;
        }

        // 本机网络通信的套接字
        private Socket serverSocket = null;
        // 远端节点
        private IPEndPoint _remotePoint;

        public IPEndPoint RemotePoint
        {
            set { _remotePoint = value; }
            get { return _remotePoint; }
        }

        private static ManualResetEvent connectDone = new ManualResetEvent(false);   // 服务器连接完成，默认阻塞当前线程     
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);   // 数据接收完成，默认阻塞当前线程  

        private static String response = String.Empty;                               // 接收字符串  

        /// <summary>
        /// 读取本地IP地址
        /// </summary>
        /// <returns></returns>
        private string GetIPAddress()
        {
            IPHostEntry hostEntry = Dns.GetHostEntry(Dns.GetHostName());
               
            if(hostEntry.AddressList.Length < 1)
            {
                return "";
            }

            return hostEntry.AddressList[0].ToString();
        }

        private int GetValidPort(string port)
        {
            int lport;

            try 
            {
                if(port == "")
                {
                    throw new ArgumentException("端口号为空，不能启动UDP");
                }

                lport = Convert.ToInt32(port);
            }
            catch(Exception ex)
            {
                throw new Exception("无效的端口号:" + ex.Message);
            }

            return lport;
        }

        private IPAddress GetValidIP(string ip)
        {
            IPAddress lip = null;

            try
            {
                if(!IPAddress.TryParse(ip, out lip))
                {
                    throw new ArgumentException("IP无效，不能启动UDP");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("无效的IP:" + ex.Message);
            }

            return lip;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ipAddr"></param>
        /// <param name="port"></param>
        public ModbusUDPEthernet(string ipAddr, string port)
        {
            IPAddress localIPAddr = GetValidIP(ipAddr);
            int       localIPPort = GetValidPort(port);

            // 处理由于网络异常引起的连接超时现象
            try
            {
                // 创建本机IP节点
                IPEndPoint ipLocalPoint = new IPEndPoint(localIPAddr, localIPPort);
                // 定义网络类型、数据连接类型和网络协议UDP
                serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Udp);
                // 绑定网络地址
                serverSocket.Bind(ipLocalPoint);
            }
            catch (SocketException)
            {
                throw new Exception("尝试创建套接字时发生错误!");
            }
            catch (ObjectDisposedException)
            {
                throw new Exception("Socket 已关闭!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 连接--UDP通信不需要
        /// </summary>
        public void Connect()
        {
            return;
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (serverSocket == null) return;

            // 完成当前缓存中剩余的数据读写
            serverSocket.Shutdown(SocketShutdown.Both);
            // 关闭Socket通道
            serverSocket.Close();
            // 注销Socket对象
            serverSocket.Dispose();
        }

        /// <summary>
        /// 判断Socket是否处于连接状态
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return false;
        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        public void Clear()
        {
            return;
        }

        /// <summary>
        /// 堵塞读取数据
        /// </summary>
        /// <param name="buffer">存放读取到的数据</param>
        /// <param name="len">欲读取的数据</param>
        /// <returns>实际读取的数据</returns>
        public int Receive(byte[] buffer, int len)
        {
            EndPoint senderRemote = (EndPoint)_remotePoint;

            // 读取远端节点发送数据
            return serverSocket.ReceiveFrom(buffer, len, SocketFlags.None, ref senderRemote);
        }

        /// <summary>
        /// 异步接收服务器发送数据的回调函数 
        /// </summary>
        /// <param name="ar">异步状态量</param>
        private void ReadCallback(IAsyncResult ar)
        {
            try
            {
                // 获得与服务器关联的Socket对象
                StateObject state = (StateObject)ar.AsyncState;
                Socket client = state.workSocket;

                // 堵塞接收远端节点数据   
                int bytesRead = client.EndReceiveFrom(ar, ref state.remotePoint);
                if (bytesRead > 0)
                {
                    // 存储收到的数据     
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
                    response = state.sb.ToString();
                    // 接收信号量置位    
                    receiveDone.Set();
                }
            }
            catch (SocketException)
            {
                throw new Exception("尝试访问套接字时发生错误!");
            }
            catch (ObjectDisposedException)
            {
                throw new Exception("Socket 已关闭!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 以异步回调方式读取服务器端发送的数据帧
        /// </summary>
        /// <param name="buffer">存放读到的数据</param>
        /// <returns>实际读取字节数</returns>
        public int Receive(byte[] buffer)
        {
            bool isReadOK = false;
            int byteCount = 0;

            EndPoint senderRemote = (EndPoint)_remotePoint;

            // 定义处理客户端接收接收数据的状态对象类   
            StateObject state = new StateObject();
            state.workSocket = serverSocket;
            state.remotePoint = senderRemote;

            // 利用回调函数开始接收服务器发送数据
            serverSocket.BeginReceiveFrom(state.buffer, 0, StateObject.BufferSize, 0, ref senderRemote, new AsyncCallback(ReadCallback), state);

            // 等待数据接收完成，1000ms超时
            isReadOK = receiveDone.WaitOne(1000, false);

            // 信号量复位，准备下一次异步读取操作
            receiveDone.Reset();

            if (isReadOK)  // 正常返回
            {
                // 正常读取到数据，转换成字节存储                   
                Encoding.Default.GetBytes(response.ToString()).CopyTo(buffer, 0);
                byteCount = buffer.Length;
            }
            else          // 超时返回
            {
                throw new Exception("接收超时，未收到数据!");
            }

            return byteCount;
        }

        /// <summary>
        /// 堵塞发送数据
        /// </summary>
        /// <param name="buf">待发送数据缓冲区</param>
        /// <param name="byteLength">发送数据长度</param>
        public void Send(byte[] buf, int len)
        {
            EndPoint senderRemote = (EndPoint)_remotePoint;
            serverSocket.SendTo(buf, len,  SocketFlags.None, senderRemote);

            serverSocket.Close();
        }
    }
}
