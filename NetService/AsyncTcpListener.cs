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
    /// <summary>
    /// 以异步方式实现的TCP Listener
    /// </summary>
    public class AsyncTcpListener
    {
        [DefaultValue(0)]
        public int ReadTimeout { get; set; }        // 同步方式下的读超时，默认超时期限无限大

        [DefaultValue(0)]
        public int WriteTimeout { get; set; }       // 同步方式下的写超时，默认超时期限无限大

        private Socket   listener;    // 服务器Socket
        private Socket   client;      // 远程连接的Socket
        private EndPoint remoteEP;    // 远程终结点 
        private EndPoint localEP;     // 本地终结点

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="localAddress">本地IP地址</param>
        /// <param name="localPort">本地监听端口</param>
        public AsyncTcpListener(IPAddress localAddress, int localPort)
        {
            localEP = new IPEndPoint(localAddress, localPort);
        }

        /// <summary>
        /// 建立服务器端套接字并启动侦听
        /// </summary>
        public void CreateListener()
        {
            // 建立服务器端套接字对象
            listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // 绑定服务器地址
            listener.Bind(localEP);
            // 启动对远端连接的侦听
            listener.Listen(10);
        }

        #region 异步接收客户端连接

        /// <summary>
        /// 指示是否建立连接
        /// </summary>
        public bool Connected
        {
            get
            {
                if (client == null) return false;
                /**
                 * MSDN：Socket.Connected 属性的值反映最近I/O 操作时的连接状态，不代表当前状态
                 * https://msdn.microsoft.com/library/system.net.sockets.socket.connected%28v=vs.100%29.aspx
                * */
                bool connected = false;
                // 保存当前socket的阻塞模式
                bool blockingState = client.Blocking;

                try
                {
                    // 设置socket为非阻塞状态
                    client.Blocking = false;

                    // 发送空字节测试Socket的连接状态
                    // 调用成功返回或引发 WAEWOULDBLOCK 错误代码 (10035)，则Socket仍然处于连接状态
                    byte[] temp = new byte[1];
                    client.Send(temp, 0, 0);

                    connected = true;
                }
                catch (SocketException se)
                {
                    if (se.NativeErrorCode.Equals(10035))
                    {
                        connected = true;
                    }
                }
                finally
                {
                    client.Blocking = blockingState;
                }
                return connected;
            }
        }

        /// <summary>
        /// 异步接受客户端的连接
        /// </summary>
        private void BeginAccept()
        {
            try
            {
                if(listener == null)
                {
                    CreateListener();
                }

                // 启动异步连接
                listener.BeginAccept(new AsyncCallback(EndAcceptCallback), listener);
                // 正在连接事件
                RaiseOnAccepting();
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "BeginAccept");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "BeginAccept");
            }
            catch (Exception e)
            {
                RaiseOnError(e, "AsyncTcpListener.BeginAccept");
            }
        }

        /// <summary>
        /// 异步连接的回调函数
        /// 这里只接受一个客户端的连接
        /// </summary>
        /// <param name="ar">异步操作状态对象</param>
        private void EndAcceptCallback(IAsyncResult ar)
        {
            Socket workSocket = (Socket)ar.AsyncState;

            try
            {
                // 获取远程的客户端
                Socket newSocket = workSocket.EndAccept(ar);

                // 关闭原来的客户端连接
                if (client != null) CloseConnection();
                // 保存新的连接
                client = newSocket;
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "EndAcceptCallback");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "EndAcceptCallback");
                return;
            }
            catch (Exception e)
            {
                RaiseOnError(e, "AsyncTcpListener.EndAcceptCallback");
                return;
            }

            // 获取连接后的远端地址
            remoteEP = client.RemoteEndPoint;
            localEP  = client.LocalEndPoint;

            // 产生连接完成事件
            RaiseOnConnected();
            // 再次在客户端连接上启动异步接收
            BegingReceive(client);
            // 继续接受连接请求
            BeginAccept();
        }

        #endregion

        /// <summary>
        /// 启动异步模式的数据接收
        /// </summary>
        /// <param name="workSocket"></param>
        private void BegingReceive(Socket workSocket)
        {
            StateObject state = new StateObject(workSocket, new byte[1024 * 4]);

            try
            {
                // 异步接收客户端数据
                workSocket.BeginReceive(state.Buffer, 0, state.BufferSize, SocketFlags.None, new AsyncCallback(EndReceiveCallback), state);
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "BegingReceive");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "BegingReceive");
            }
            catch (Exception e)
            {
                RaiseOnError(e, "BegingReceive");
            }
        }

        /// <summary>
        /// 异步接收的回调函数
        /// </summary>
        /// <param name="ar"></param>
        private void EndReceiveCallback(IAsyncResult ar)
        {
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;

            int byteRead = 0;

            try
            {
                // 读取所有可用的数据，直到达到 BeginReceive 方法的 size 参数所指定的字节数为止
                // 如果远程主机使用 Shutdown 方法关闭Socket 连接，并且所有可用数据均已收到，则EndReceive 方法将立即完成并返回零字节。
                byteRead = handler.EndReceive(ar);
                // 下面这个判断必须有，如果没有下次就无法接收数据
                if (byteRead > 0)
                {
                    // 本次数据没有接收完，继续异步接收
                    handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0, new AsyncCallback(EndReceiveCallback), state);
                }
            }
            catch
            { 
                byteRead = 0; 
            }

            // 客户端不再发送数据，关闭连接
            if (byteRead == 0)
            {
                CloseConnection();
                return;
            }

            // 保存本次接收数据
            byte[] dataframe = new byte[byteRead];
            Array.Copy(state.Buffer, dataframe, byteRead);

            // 数据接收完成事件
            RaiseOnReceived(dataframe,remoteEP);
        }

        /// <summary>
        /// 关闭客户端连接
        /// </summary>
        public void CloseConnection()
        {
            try
            {
                if (client != null)
                {
                    client.Shutdown(SocketShutdown.Both);
                    client.Close();

                    // 断开连接事件
                    RaiseOnDisconnected();
                }
            }
            catch { }

            client = null;
        }

        /// <summary>
        /// 关闭服务器和客户端连接
        /// </summary>
        public void Close()
        {
            CloseConnection();

            try
            {
                // 关闭服务器Socket侦听
                listener.Close();
            }
            catch { }

            listener = null;

            // 服务器Socket侦听关闭事件
            RaiseOnListernClosed();
        }

        /// <summary>
        /// 堵塞发送字节数组
        /// </summary>
        /// <param name="dataframe">字节数组</param>
        /// <returns>发送成功返回true；否则返回false</returns>
        public bool Send(byte[] dataframe)
        {
            bool b = false;

            if (!Connected) return b;

            try
            {
                client.Send(dataframe);
                b = true;
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "AsyncTcpListener.Send");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "AsyncTcpListener.Send");
            }
            catch (Exception e)
            {
                RaiseOnError(e, "AsyncTcpListener.Send");
            }

            return b;
        }

        #region 服务器端以太网操作定义的事件

        /// <summary>
        /// TCP服务器端进入侦听时发生
        /// </summary>
        public event EventHandler<ConnectEventArgs> OnAccepting;

        /// <summary>
        /// TCP服务器端接受连接成功时发生
        /// </summary>
        public event EventHandler<ConnectEventArgs> OnConnected;

        /// <summary>
        /// TCP服务器端连接断开时发生
        /// </summary>
        public event EventHandler<ConnectEventArgs> OnDisconnected;

        /// <summary>
        /// TCP服务器端结束侦听时发生
        /// </summary>
        public event EventHandler<ConnectEventArgs> OnListenerClosed;

        /// <summary>
        /// TCP服务器端接收到数据时发生
        /// </summary>
        public event EventHandler<IOEventArgs> OnReceived;

        /// <summary>
        /// TCP服务器端数据发送完毕时发生
        /// </summary>
        public event EventHandler<IOEventArgs> OnSent;

        /// <summary>
        /// TCP服务器端异常时发生
        /// </summary>
        public event EventHandler<ExceptEventArgs> OnError;

        #endregion

        #region 封装了事件的函数

        private void RaiseOnListernClosed()
        {
            if (OnListenerClosed == null) return;
            OnListenerClosed(this, new ConnectEventArgs(remoteEP, localEP));
        }

        /// <summary>
        /// 正在侦听时发布的事件
        /// </summary>
        private void RaiseOnAccepting()
        {
            if (OnAccepting == null) return;
            OnAccepting(this, new ConnectEventArgs(remoteEP, localEP));
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
        private void RaiseOnReceived(byte[] dataframe, EndPoint remoteEP)
        {
            if (OnReceived == null) return;
            OnReceived(this, new IOEventArgs(dataframe,remoteEP));
        }

        /// <summary>
        /// 发送数据时发布的事件
        /// </summary>
        /// <param name="dataframe"></param>
        private void RaiseOnSent(byte[] dataframe,EndPoint remoteEP)
        {
            if (OnSent == null) return;
            OnSent(this, new IOEventArgs(dataframe,remoteEP));
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
