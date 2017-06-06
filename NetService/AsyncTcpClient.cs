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
    /// 具有同步和异步方式通信的TCP Client
    /// </summary>
    public class AsyncTcpClient
    {
        [DefaultValue(0)]
        public int ReadTimeout { get; set; }        // 同步方式下的读超时，默认超时期限无限大

        [DefaultValue(0)]
        public int WriteTimeout { get; set; }       // 同步方式下的写超时，默认超时期限无限大

        private Socket socket;                // 套接字
         
        private EndPoint remoteEP;            // 远端连接点
        private EndPoint localEP;             // 本地连接点
        
        private volatile bool isConnecting;   // 正在连接
        private volatile bool hasConnected;   // 已经连接
        private volatile bool isClosing;      // 正在关闭

        /// <summary>
        /// 实例化<see cref="AsyncTcpClient"/>
        /// </summary>
        /// <param name="remoteAddress">远端主机IP地址</param>
        /// <param name="remotePort">远端主机端口</param>
        /// <param name="localAddress">本地IP地址</param>
        /// <param name="localPort">本地端口</param>
        public AsyncTcpClient()
        {
            isConnecting = false;   // 正在连接标志
            hasConnected = false;   // 已经连接标志
            isClosing    = false;   // 正在关闭标志
        }

        #region 客户端连接和关闭主机

        /// <summary>
        /// 指示是否连接
        /// </summary>
        public bool Connected
        {
            get
            {
                if (socket == null) return false;
                /**
                 * MSDN：Socket.Connected 属性的值反映最近一次I/O 操作时的连接状态，不代表当前状态
                 * https://msdn.microsoft.com/library/system.net.sockets.socket.connected%28v=vs.100%29.aspx
                 * */
                bool connected = false;
                // 保存当前socket的阻塞模式
                bool blockingState = socket.Blocking;

                try
                {
                    // 设置socket为非阻塞状态
                    socket.Blocking = false;

                    // 发送空字节测试Socket的连接状态
                    // 调用成功返回或引发 WAEWOULDBLOCK 错误代码 (10035)，则Socket仍然处于连接状态
                    byte[] temp = new byte[1];
                    socket.Send(temp, 0, 0);

                    connected = true;
                }
                catch (SocketException se)
                {
                    if (se.NativeErrorCode.Equals(10035)) connected = true;
                }
                finally
                {
                    // 恢复当前socket的阻塞模式
                    socket.Blocking = blockingState;
                }

                return connected;
            }
        }

        /// <summary>
        /// 连接远端主机
        /// </summary>
        public void Connect(IPAddress remoteAddress, int remotePort)
        {
            if (isConnecting) return;

            try
            {
                // 关闭已经建立的连接
                if (hasConnected) Close();

                // 连接终结点
                remoteEP = new IPEndPoint(remoteAddress, remotePort);

                // 创建基于TCP协议和流传输的Socket套接字
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // 设置LingerState 属性修改 Winsock 可重置的连接的条件
                // 基于 IP 协议行为会出现连接重置。当仍然要发送数据时，此属性控制面向连接的连接在对 Close 调用后仍保持打开的时间长度。
                // 在调用方法以向对等方发送数据时，此数据放置在输出网络缓冲区中。 此属性可用来确保在 Close 方法结束该连接之前，将这些数据发送到远程主机。
                // 当前设置为：在Socket.Close()被调用后，立即关闭连接，丢弃所有数据
                socket.LingerState = new LingerOption(true, 0);
                // 绑定本地IP
                //socket.Bind(localEP);

                // 启动异步连接
                BeginConnect();
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "Connect");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "Connect");
            }
            catch (Exception e)
            {
                RaiseOnError(e, "Connect");
            }           
        }

        /// <summary>
        /// 异步连接远端主机
        /// </summary>
        private void BeginConnect()
        {
            isConnecting = true;

            try
            {
                // 启动异步连接并直接返回，由线程池的线程监控连接状态
                socket.BeginConnect(remoteEP, new AsyncCallback(EndConnectCallback), socket);
                // 处理连接事件
                RaiseOnConnecting();
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "BeginConnect");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "BeginConnect");
            }
            catch (Exception e)
            {
                RaiseOnError(e, "BeginConnect");
            }           
        }

        /// <summary>
        /// 异步连接的回调函数
        /// 线程池的线程监控到与远端主机连接后，进入回调函数
        /// </summary>
        /// <param name="ar">异步操作的状态量</param>
        private void EndConnectCallback(IAsyncResult ar)
        {
            isConnecting = false;

            if (isClosing) return;

            // 获得当前连接的套接字
            Socket workSocket = (Socket)ar.AsyncState;

            try
            {
                // 完成在BeginConnect方法中启动的异步远程主机连接请求
                workSocket.EndConnect(ar);

                // 连接已经建立标志
                hasConnected = true;

                // 获取连接建立后真实IP地址
                remoteEP = workSocket.RemoteEndPoint;
                localEP  = workSocket.LocalEndPoint;

                // 启动异步接收(试情况而定)
                //BeginReceive(workSocket);

                // 处理连接完成事件
                RaiseOnConnected();
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "EndConnectCallback");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "EndConnectCallback");
                // 再此请求连接
                BeginConnect();
            }
            catch(Exception e)
            {
                RaiseOnError(e, "EndConnectCallback");
            }
        }

        /// <summary>
        /// 关闭Socket连接
        /// </summary>
        public void Close()
        {
            isClosing = true;

            try
            {
                if (socket != null)
                {
                    socket.Dispose();
                    socket.Close();
                    socket = null;

                    isConnecting = false;
                    hasConnected = false;

                    // 等待Socket释放资源
                    System.Threading.Thread.Sleep(100);

                    isClosing = false;

                    // 关闭完成事件
                    RaiseOnDisconnected();
                }
            }
            catch (Exception e)
            {
                RaiseOnError(e, "Close");
            }
        }

        #endregion

        #region 同步和异步接收数据

        /// <summary>
        /// 同步发送数据
        /// </summary>
        /// <param name="dataframe">数据字节数组</param>
        /// <returns>发送成功返回true；否则返回false</returns>
        public bool Send(byte[] dataframe)
        {
            bool b = false;
            if (!Connected) return b;

            try
            {
                // 堵塞发送数据
                socket.Send(dataframe);

                b = true;
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "AsyncTcpClient.Send");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "AsyncTcpClient.Send");
            }
            catch (Exception e)
            {
                RaiseOnError(e, "AsyncTcpClient.Send");
            }

            return b;
        }

        /// <summary>
        /// 异步接收
        /// </summary>
        /// <param name="workSocket">socket事件</param>
        public void BeginReceive()
        {
            if (isClosing) return;

            // 定义缓冲区大小为4096字节的异步状态量
            StateObject state = new StateObject(socket, new byte[1024 * 4]);

            try
            {
                // 启动异步数据接收并直接返回，由线程池的线程监控数据接收状态
                socket.BeginReceive(state.Buffer, 0, state.BufferSize, SocketFlags.None, new AsyncCallback(ReceiveCallback), state);
            }
            catch (ObjectDisposedException)
            {
                RaiseOnError(new Exception("对已释放的对象执行操作!"), "BeginReceive");
            }
            catch (SocketException)
            {
                RaiseOnError(new Exception("发生套接字错误!"), "BeginReceive");
            }
            catch (Exception e)
            {
                RaiseOnError(e, "BeginReceive");
            }
        }

        /// <summary>
        /// 异步接收的回调函数
        /// 线程池的线程监控到接收远端主机的数据后，进入回调函数
        /// </summary>
        /// <param name="ar">异步操作状态量</param>
        private void ReceiveCallback(IAsyncResult ar)
        {
            if (isClosing) return;

            // 获得与服务器关联的Socket对象
            StateObject state = (StateObject)ar.AsyncState;
            Socket client     = state.workSocket;

            int bytesRead = 0;

            try
            {
                // 完成在BeginReceive方法中启动的异步接收远程主机数据的请求   
                bytesRead = client.EndReceive(ar);
                if (bytesRead > 0)
                {
                    client.BeginReceive(state.Buffer, 0, state.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
                }
            }
            catch
            {
                bytesRead = 0;
            }

            // 可以视情况屏蔽
            if (bytesRead == 0)
            {
                // 重新启动Socket连接
                Close();
                Connect((remoteEP as IPEndPoint).Address, (remoteEP as IPEndPoint).Port);

                return;
            }

            // 继续启动线程池的线程监控异步接收
            BeginReceive();

            // 获得本次接收的数据
            byte[] dataframe = new byte[bytesRead];
            Array.Copy(state.Buffer, dataframe, bytesRead);

            // 引发数据接收完成事件
            RaiseOnReceived(dataframe); 
        }

        /// <summary>
        /// 同步堵塞方式读取数据
        /// </summary>
        /// <param name="buffer">存放读取到的数据</param>
        /// <param name="len">欲读取的数据</param>
        /// <returns>实际读取的数据</returns>
        public int Receive(byte[] buffer, int len)
        {
            return socket.Receive(buffer, len, SocketFlags.None);
        }

        #endregion

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
        public event EventHandler<IOEventArgs>  OnSent;             

        /// <summary>
        /// 当TCP客户端或TCP服务器端异常时发生
        /// </summary>
        public event EventHandler<ExceptEventArgs>  OnError;

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

            OnReceived(this, new IOEventArgs(dataframe,remoteEP));
        }

        /// <summary>
        /// 发送数据时发布的事件
        /// </summary>
        /// <param name="dataframe"></param>
        private void RaiseOnSent(byte[] dataframe)
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
