using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace NJUST.AUTO06.Modbus.Comm
{
    /***************************************************************
     * 类 名：基于MODBUS TCP协议的以太网通信代理类
     * 功 能：封装了符合MODBUS TCP协议特点的、主从式的以太网读写操作
     * 作 者：邹卫军
     * 时 间：2016.3.26
     ***************************************************************/
    public class ModbusTCPEthernet : ICommunicationProxy
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
        }

        // 服务器端IP地址
    //    private string serveIPAddr = "192.168.0.1";
        // 服务器端IP端口号
    //    private int    serveIPPort = 8888;
        // Socket对象
        protected Socket clientSocket = null;

        private static ManualResetEvent connectDone = new ManualResetEvent(false);   // 服务器连接完成，默认阻塞当前线程     
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);   // 数据接收完成，默认阻塞当前线程  

        private static String response = String.Empty;                               // 接收字符串  

        // 远端节点
        private IPEndPoint _remotePoint;

        public IPEndPoint RemotePoint
        {
            set { _remotePoint = value; }
            get { return _remotePoint; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ipAddr">服务器端IP地址</param>
        /// <param name="port">服务器端端口号</param>
        public ModbusTCPEthernet()//(string ipAddr, int port)
        {
            //this.serveIPAddr = ipAddr;
            //this.serveIPPort = port;
        }

        #region 实现接口定义的函数

        /// <summary>
        /// 异步连接回调函数 
        /// </summary>
        /// <param name="ar">异步操作状态量</param>
        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                // 等待连接服务器成功
                Socket client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
            }
            catch(SocketException)
            {
                throw new Exception("尝试访问套接字时发生错误!");
            }
            catch (ObjectDisposedException)
            {
                throw new Exception("套接字已关闭!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                // 信号量置位
                connectDone.Set();
            }
        }

        /// <summary>
        /// 以异步方式连接服务器
        /// </summary>
        /// <returns></returns>
        public void Connect()
        {
            // 处理由于网络异常引起的连接超时现象
            try
            {
                // 创建远程IP节点
                //IPEndPoint remoteIPPoint = new IPEndPoint(IPAddress.Parse(serveIPAddr), serveIPPort);
                // 建立TCP连接
                clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // 开始异步连接
                connectDone.Reset();
                clientSocket.BeginConnect(_remotePoint, new AsyncCallback(ConnectCallback), clientSocket);
                // 堵塞等待3秒
                connectDone.WaitOne(3000, false);

                // 连接成功
                //if (clientSocket.Connected)
                //{
                //    hSocket = clientSocket;
                //}
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
        /// 断开连接
        /// </summary>
        public void DisConnect()
        {
            if (clientSocket == null) return;

            if (clientSocket.Connected)
            {
                // 完成当前缓存中剩余的数据读写
                clientSocket.Shutdown(SocketShutdown.Both);
                // 关闭Socket通道
                clientSocket.Close();
                // 注销Socket对象
                clientSocket.Dispose();
            }
        }

        /// <summary>
        /// 判断Socket是否处于连接状态
        /// </summary>
        /// <returns></returns>
        public bool IsConnected()
        {
            return clientSocket.Connected;
        }

        /// <summary>
        /// 清空缓冲区
        /// </summary>
        public void Clear()
        {
            return;
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

                // 堵塞接收服务器数据   
                int bytesRead = client.EndReceive(ar);
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

            // 定义处理客户端接收接收数据的状态对象类   
            StateObject state = new StateObject();
            state.workSocket = clientSocket;

            // 利用回调函数开始接收服务器发送数据
            clientSocket.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);

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
        /// 堵塞读取数据
        /// </summary>
        /// <param name="buffer">存放读取到的数据</param>
        /// <param name="len">欲读取的数据</param>
        /// <returns>实际读取的数据</returns>
        public int Receive(byte[] buffer, int len)
        {
            return clientSocket.Receive(buffer, len, SocketFlags.None);
        }

        /// <summary>
        /// 堵塞发送数据
        /// </summary>
        /// <param name="buf">待发送数据缓冲区</param>
        /// <param name="byteLength">发送数据长度</param>
        public void Send(byte[] buf, int len)
        {
            clientSocket.Send(buf, len, SocketFlags.None);
        }

        #endregion
    }
}
