using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NJUST.AUTO06.NetService;

namespace NJUST.AUTO06.Modbus.IO
{
    class UdpClientAdapter : IStreamResource
    {
        private AsyncUdpClient udpClient;

        public UdpClientAdapter(AsyncUdpClient udpClient)
		{
            if (udpClient != null)
            {
                this.udpClient = udpClient;
            }
		}

        /// <summary>
        /// 指示不发生超时
        /// </summary>
		public int InfiniteTimeout
		{
            get { return -1; }
		}

        /// <summary>
        /// 读操作超时时间
        /// </summary>
		public int ReadTimeout
		{
			get { return udpClient.ReadTimeout; }
			set { udpClient.ReadTimeout = value; }
		}

        /// <summary>
        /// 写操作超时时间
        /// </summary>
		public int WriteTimeout
		{
            get { return udpClient.WriteTimeout; }
            set { udpClient.WriteTimeout = value; }
		}

        /// <summary>
        /// 清空输入缓冲区
        /// </summary>
		public void DiscardInBuffer()
		{
            return;
		}

        /// <summary>
        /// 从接收缓冲区读取收到数据
        /// </summary>
        /// <param name="buffer">存储接收数据的字节数组</param>
        /// <param name="offset">接收字节存储在数组中的偏移量</param>
        /// <param name="count">接收字节数</param>
        /// <returns></returns>
		public int Read(byte[] buffer, int offset, int count)
		{
            byte[] readBuf = new byte[count];

            // 同步接收
            int bytes = udpClient.Receive(readBuf, count);
            Array.Copy(readBuf, 0, buffer, offset, count);
            
            return bytes;
		}

        /// <summary>
        /// 发送缓冲区数据
        /// </summary>
        /// <param name="buffer">存储发送数据的字节数组</param>
        /// <param name="offset">发送字节在数组中的偏移量</param>
        /// <param name="count">发送字节数</param>
		public void Write(byte[] buffer, int offset, int count)
		{
            byte[] writeBuf = new byte[count];

            Array.Copy(buffer, offset, writeBuf, 0, count);
            // 同步发送
            udpClient.Send(writeBuf, count);
		}
    }
}
