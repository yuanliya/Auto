using System;
using System.Diagnostics;
using System.IO.Ports;

namespace NJUST.AUTO06.Modbus.IO
{
	/// <summary>
	/// 基于桥接设计模式的串口适配器类
	/// </summary>
	public class SerialPortAdapter : IStreamResource
	{
		private SerialPort _serialPort;

		public SerialPortAdapter(SerialPort serialPort)
		{
			Debug.Assert(serialPort != null, "SerialPort对象参数不能为空！");

			_serialPort = serialPort;
		}

        /// <summary>
        /// 指示不发生超时
        /// </summary>
		public int InfiniteTimeout
		{
			get { return SerialPort.InfiniteTimeout; }
		}

        /// <summary>
        /// 读操作超时时间
        /// </summary>
		public int ReadTimeout
		{
			get { return _serialPort.ReadTimeout; }
			set { _serialPort.ReadTimeout = value; }
		}

        /// <summary>
        /// 写操作超时时间
        /// </summary>
		public int WriteTimeout
		{
			get { return _serialPort.WriteTimeout; }
			set { _serialPort.WriteTimeout = value; }
		}

        /// <summary>
        /// 清空输入缓冲区
        /// </summary>
		public void DiscardInBuffer()
		{
			_serialPort.DiscardInBuffer();
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
			return _serialPort.Read(buffer, offset, count);
		}

        /// <summary>
        /// 发送缓冲区数据
        /// </summary>
        /// <param name="buffer">存储发送数据的字节数组</param>
        /// <param name="offset">发送字节在数组中的偏移量</param>
        /// <param name="count">发送字节数</param>
		public void Write(byte[] buffer, int offset, int count)
		{
			_serialPort.Write(buffer, offset, count);
		}
	}
}
