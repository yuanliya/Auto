using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace NJUST.AUTO06.Modbus.Comm
{
    public interface ICommunicationProxy
    {
        /// <summary>
        /// 连接通讯设备
        /// </summary>
        void Connect();

        /// <summary>
        /// 断开通讯设备连接
        /// </summary>
        void DisConnect();

        /// <summary>
        /// 通信设备是否连接
        /// </summary>
        /// <returns></returns>
        bool IsConnected();

        /// <summary>
        /// 清通信设备的数据缓冲区
        /// </summary>
        void Clear();

        /// <summary>
        /// 通信端口发送数据
        /// </summary>
        /// <param name="buf">待发送数据的数组</param>
        /// <param name="len">数据长度</param>
        void Send(byte[] buf, int len);

        /// <summary>
        /// 通信端口读指定数量的数据
        /// </summary>
        /// <param name="buf">存放读取数据的数组</param>
        /// <param name="len">希望读取数据长度</param>
        /// <returns>实际读取的数据长度</returns>
        int Receive(byte[] buf, int len);

        /// <summary>
        /// 通信端口读数据
        /// </summary>
        /// <param name="buffer">存放读取数据的数组</param>
        /// <returns>实际读取的数据长度</returns>
        int Receive(byte[] buffer);

        /// <summary>
        /// 以太网通信的远程节点
        /// </summary>
        IPEndPoint RemotePoint { set; get; }
    }
}
