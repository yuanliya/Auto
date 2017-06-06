using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.IO.Ports;
using System.Threading;

namespace NJUST.AUTO06.Modbus.Comm
{
    public class ModbusRTUSerialPort : ICommunicationProxy
    {
        /// <summary>
        /// 用于以太网，本类不用
        /// </summary>
        public IPEndPoint RemotePoint { set; get; }

        protected SerialPort sp = null;           // 串口对象  

        /// <summary>
        /// 串口初始化
        /// </summary>
        /// <param name="commPort">串口号</param>
        /// <param name="baudRate">波特率</param>
        /// <param name="parity">校验</param>
        /// <param name="dataBits">数据位</param>
        /// <param name="stopBits">停止位</param>
        public ModbusRTUSerialPort(string commPort, int baudRate, Parity parity, int dataBits, StopBits stopBits) 
        {
            if(sp == null)
            {
                // 生成串口类并初始化通信参数
                sp = new SerialPort(commPort, baudRate, parity, dataBits, stopBits);
                // 设置读写超时时间
                sp.ReadTimeout  = 500;
                sp.WriteTimeout = 500;
            }
        }

        ///  <summary>   
        ///  打开串口并初始化
        ///  </summary>   
        ///  <param name="com">串口号 </param>   
        ///  <returns> 无 </returns>   
        public virtual void Connect()
        {
            try
            {
                // 如果串口已打开，则先关闭再重新打开
                if (sp.IsOpen)
                {
                    // 关闭串口
                    sp.Close();
                    // 延时等待彻底关闭
                    Thread.Sleep(100);
                    // 重新打开串口
                    sp.Open();
                }
                else
                {
                    sp.Open();
                }
            }
            catch (UnauthorizedAccessException)
            {
                throw new Exception("对端口的访问被拒绝!");
            }
            catch (IOException)
            {
                throw new Exception("未能找到或打开指定端口，端口处于无效状态!");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ///  <summary>   
        ///  关闭串口
        ///  </summary>   
        ///  <returns> 无 </returns>   
        public virtual void DisConnect()
        {
            if (sp.IsOpen == false) return;

            try
            {
                sp.Close();
            }            
            catch (UnauthorizedAccessException)
            {
                throw new Exception("对端口的访问被拒绝!");
            }
            catch (IOException)
            {
                throw new Exception("未能找到或打开指定端口，端口处于无效状态!");
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        ///  <summary>   
        ///  串口是否连接
        ///  </summary>   
        ///  <returns> 无 </returns>   
        public virtual bool IsConnected()
        {
            return sp.IsOpen;
        }

        ///  <summary>   
        ///  清串口缓冲区
        ///  </summary>   
        ///  <returns>-1：串口没打开；0：操作完成</returns>   
        public virtual void Clear()
        {
            if (sp.IsOpen == false) return;

            // 清空接收和发送缓冲区
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
        }

        /// <summary>
        /// 从串口缓冲区读取收到的字节
        /// </summary>
        /// <param name="buffer">存放读到的数据</param>
        /// <param name="len">欲读取的字节数</param>
        /// <returns>实际读取字节数</returns>
        public virtual int Receive(byte[] buffer)
        {
            int bytesToRead = sp.BytesToRead;

            return sp.Read(buffer, 0, bytesToRead);

            //// 读取收到的数据
            //try
            //{
            //    return sp.Read(buffer, 0, bytesToRead);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }

        /// <summary>
        /// 从串口读指定长度的数据
        /// </summary>
        /// <param name="buffer">存放读取数据的缓冲区</param>
        /// <param name="len">欲读取的字节数</param>
        /// <returns>实际读取的字节数</returns>
        public virtual int Receive(byte[] buffer, int len)
        {
            int numBytesRead = 0;

            while (numBytesRead != len)
            {
                numBytesRead += sp.Read(buffer, numBytesRead, len - numBytesRead);
            }

            return numBytesRead;
              

            //// 能读取的最多数据
            //if (len > sp.BytesToRead)
            //{
            //    len = sp.BytesToRead;
            //}

            //try
            //{
            //    return sp.Read(buffer, 0, len);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}
        }

        /// <summary>
        /// 串口发送指定数量的数据
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="len"></param>
        public virtual void Send(byte[] buffer, int len)
        {
            if (len <= 0) return;

            sp.Write(buffer, 0, len);

            //try
            //{
            //    sp.Write(buffer, 0, len);
            //}
            //catch(Exception ex)
            //{
            //    throw new Exception(ex.Message);
            //}           
        }
    }
}
