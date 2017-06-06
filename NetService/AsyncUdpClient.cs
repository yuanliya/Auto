using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.ComponentModel;

namespace NJUST.AUTO06.NetService
{
    public class AsyncUdpClient
    {
        [DefaultValue(0)]
        public int ReadTimeout { get; set; }   // 同步方式下的读超时，默认超时期限无限大

        [DefaultValue(0)]
        public int WriteTimeout { get; set; }  // 同步方式下的写超时，默认超时期限无限大

        public  EndPoint remoteEP { get; set; } // 远端连接点
        private EndPoint localEP;               // 远端连接点

        private Socket workSocket;              // 套接字

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ipAddr"></param>
        /// <param name="port"></param>
        public AsyncUdpClient(IPAddress localAddress, int localPort)
        {
            // 处理由于网络异常引起的连接超时现象
            try
            {
                // 创建本机IP节点
                localEP = new IPEndPoint(localAddress, localPort);
                // 定义网络类型、数据连接类型和网络协议UDP
                workSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                // 绑定网络地址
                workSocket.Bind(localEP);
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
        /// 同步发送数据
        /// </summary>
        /// <param name="buf">待发送数据缓冲区</param>
        /// <param name="byteLength">发送数据长度</param>
        public bool Send(byte[] buf, int len)
        {
            bool b = false;

            EndPoint senderRemote = (EndPoint)remoteEP;

            try
            {
                workSocket.SendTo(buf, len, SocketFlags.None, senderRemote);
                b = true;
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "AsyncUdpClient.Send");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "AsyncUdpClient.Send");
            }
            catch (Exception e)
            {
                RaiseOnError(e, "AsyncUdpClient.Send");
            }

            return b;
        }

        /// <summary>
        /// 同步接收数据
        /// </summary>
        /// <param name="buffer">存放读取到的数据</param>
        /// <param name="len">欲读取的数据</param>
        /// <returns>实际读取的数据</returns>
        public int Receive(byte[] buffer, int len)
        {
            EndPoint senderRemote = (EndPoint)remoteEP;

            // 读取远端节点发送数据
            try
            {
                return workSocket.ReceiveFrom(buffer, len, SocketFlags.None, ref senderRemote);
            }
            catch (Exception ex)
            {
                RaiseOnError(new Exception(senderRemote.ToString() + ex.Message), "AsyncUdpClient.BeginReceive");               
            }

            return -1;
        }

        /// <summary>
        /// 异步接收服务器发送数据的回调函数 
        /// </summary>
        /// <param name="ar">异步状态量</param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            int bytesRead = 0;

            // 获得与服务器关联的Socket对象
            StateObject state = (StateObject)ar.AsyncState;
            Socket client     = state.workSocket;

            try
            {
                // 完成在BeginReceive方法中启动的异步接收远程主机数据的请求   
                bytesRead = client.EndReceiveFrom(ar, ref state.remoteEP);
                if (bytesRead > 0)
                {
                    client.BeginReceive(state.Buffer, 0, state.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception(state.remoteEP.ToString() + "访问错误！"), "AsyncUdpClient.ReceiveCallback");
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception(state.remoteEP.ToString() + "Socket 已关闭!"), "AsyncUdpClient.ReceiveCallback");
            }
            catch (Exception ex)
            {
                bytesRead = 0;
                RaiseOnError(new Exception(ex.Message), "AsyncUdpClient.ReceiveCallback");

                return;
            }

            // 继续接收数据
            BeginReceive();

            // 获得本次接收的数据
            byte[] dataframe = new byte[bytesRead];
            Array.Copy(state.Buffer, dataframe, bytesRead);

            // 数据接收完成事件
            RaiseOnReceived(dataframe);
        }

        /// <summary>
        /// 以异步回调方式读取服务器端发送的数据帧
        /// </summary>
        /// <param name="buffer">存放读到的数据</param>
        /// <returns>实际读取字节数</returns>
        public void BeginReceive()
        {
            EndPoint senderRemote = (EndPoint)remoteEP;

            // 定义处理客户端接收接收数据的状态对象类   
            StateObject state = new StateObject(workSocket, new byte[1024 * 4]);
            state.remoteEP    = senderRemote;

            // 利用回调函数开始接收服务器发送数据
            try
            {
                workSocket.BeginReceiveFrom(state.Buffer, 0, state.BufferSize, 0, ref senderRemote, new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception ex)
            {
                RaiseOnError(new Exception(senderRemote.ToString() + ex.Message), "AsyncUdpClient.BeginReceive");
            }
        }

        #region 客户端发布的事件

        /// <summary>
        /// 当TCP客户端请求连接或TCP服务器端进入侦听时发生
        /// </summary>
        public event EventHandler<ConnectEventArgs> OnConnecting;

        /// <summary>
        /// 当TCP客户端连接成功或TCP服务器端接受连接成功时发生
        /// </summary>
        public event EventHandler<ConnectEventArgs> OnConnected;

        /// <summary>
        /// 当TCP客户端连接断开或TCP服务器端连接断开时发生
        /// </summary>
        public event EventHandler<ConnectEventArgs> OnDisconnected;

        /// <summary>
        /// 当TCP客户端或TCP服务器端接收到数据时发生
        /// </summary>
        public event EventHandler<IOEventArgs> OnReceived;

        /// <summary>
        /// 当TCP客户端或TCP服务器端数据发送完毕时发生
        /// </summary>
        public event EventHandler<IOEventArgs> OnSent;

        /// <summary>
        /// 当TCP客户端或TCP服务器端异常时发生
        /// </summary>
        public event EventHandler<ExceptEventArgs> OnError;

        #endregion

        #region 封装了事件的函数

        /// <summary>
        /// 请求连接时发布的事件
        /// </summary>
        private void RaiseOnConnecting()
        {
            if (OnConnecting == null) return;

            OnConnecting(this, new ConnectEventArgs(remoteEP, localEP));
        }

        /// <summary>
        /// 连接成功时发布的事件
        /// </summary>
        private void RaiseOnConnected()
        {
            if (OnConnected == null) return;

            OnConnected(this, new ConnectEventArgs(remoteEP, localEP));
        }

        /// <summary>
        /// 断开连接时发布的事件
        /// </summary>
        private void RaiseOnDisconnected()
        {
            if (OnDisconnected == null) return;

            OnDisconnected(this, new ConnectEventArgs(remoteEP, localEP));
        }

        /// <summary>
        /// 数据接收完成发布的事件
        /// </summary>
        /// <param name="dataframe"></param>
        private void RaiseOnReceived(byte[] dataframe)
        {
            if (OnReceived == null) return;

            OnReceived(this, new IOEventArgs(dataframe, remoteEP));
        }

        /// <summary>
        /// 发送数据时发布的事件
        /// </summary>
        /// <param name="dataframe"></param>
        private void RaiseOnSent(byte[] dataframe)
        {
            if (OnSent == null) return;

            OnSent(this, new IOEventArgs(dataframe, remoteEP));
        }

        /// <summary>
        /// 异常时发布的事件
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="functionName"></param>
        private void RaiseOnError(Exception exception, string functionName)
        {
            if (OnError == null) return;

            OnError(this, new ExceptEventArgs(exception, functionName));
        }

        #endregion
    }
}
