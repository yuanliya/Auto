using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading; 

using NJUST.AUTO06.Modbus.IO;
using NJUST.AUTO06.Modbus.Protocal;

namespace NJUST.AUTO06.Modbus.Device
{
    /*--------------------------------------------------------------
     * 类 名：ModbusDevice
     * 功 能：基于MODBUS协议通信的抽象类，实现MODBUS协议的读写操作
     * 作 者：邹卫军
     * 时 间：2015.08.09
     * -------------------------------------------------------------*/
    public abstract class ModbusDevice
    {
        // 同步对象
        private object _syncLock = new object();

        // 通信组件
        protected IStreamResource commProxy = null;
        // Modbus通信协议
        protected IModbusProtocal protocalProxy = null;

        /// <summary>
        /// 带参数构造函数
        /// </summary>
        /// <param name="commProxy">通信设备代理</param>
        /// <param name="protocalProxy">具体MODBUS通信协议</param>
        public ModbusDevice(IStreamResource commProxy, IModbusProtocal protocalProxy)
        {
            this.commProxy     = commProxy;
            this.protocalProxy = protocalProxy;
        }

        /// <summary>
        /// 读取指定数量的字节数据
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        private byte[] Read(int count)
        {
            byte[] frameBytes = new byte[count];
            int numBytesRead = 0;

            while (numBytesRead != count)
                numBytesRead += commProxy.Read(frameBytes, numBytesRead, count - numBytesRead);

            return frameBytes;
        }

        /// <summary>
        /// 发送指定数量的字节数据
        /// </summary>
        /// <param name="buf"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        private void Write(byte[] buf, int offset, int count)
        {
            // 清接收缓冲区
            commProxy.DiscardInBuffer();
            // 发送数据
            commProxy.Write(buf, offset, count);
        }

        /// <summary>
        /// 完成Modbus通信的一次读写操作
        /// </summary>
        /// <param name="inData">发送数据帧</param>
        /// <param name="fbData">反馈数据帧</param>
        /// <returns></returns>
        private bool UnicastMessage(List<byte> inData, List<byte> fbData)
        {
            int i = 0, bytes = 0;

            // 锁定操作
            lock (_syncLock)
            {
                Thread.Sleep(50);                      
                // 超时或错误重发
                for (i = 0; i < ModbusCommand.DefaultRetries; i++)
                {
                    fbData.Clear();

                    // 发送数据
                    Write(inData.ToArray(), 0, inData.Count);

                    // 读取反馈帧帧头信息
                    byte[] frameStart = Read(protocalProxy.FrameHeaderSize);

                    // 根据帧头判断反馈帧的长度
                    bytes = protocalProxy.ResponseBytesToRead(frameStart);
                    if (bytes == -1)
                    {
                        // 错误帧，进行下一次发送
                        continue;
                    }

                    // 继续读取反馈帧剩余字节
                    byte[] frameEnd = Read(bytes - protocalProxy.FrameHeaderSize);
                    // 组成完整反馈帧
                    byte[] frame = frameStart.Concat(frameEnd).ToArray();

                    // 填充反馈帧list
                    fbData.AddRange(frame);

                    // 进行反馈帧校验
                    if (protocalProxy.ValidateResponse(inData, fbData) == true)
                    {
                        // Thread.Sleep(50);                     
                        // 校验正确，停止发送                 
                        return true;
                    }

                }
            }

            return false;
        }

        /// <summary>
        /// 发送MODBUS协议的写命令
        /// </summary>
        /// <param name="devNo">节点号</param>
        /// <param name="funcCode">功能码</param>
        /// <param name="addr">寄存器起始地址</param>
        /// <param name="value">发送数据数组</param>
        /// <param name="inData">发送帧</param>
        /// <param name="fbData">反馈帧</param>
        public void WriteCommand(int devNo, byte funcCode, UInt16 addr, int[] value, List<byte> inData, List<byte> fbData)
        {
            // 获得写功能的MODBUS RTU协议帧
            inData = protocalProxy.GetModbusProtocalFrame(devNo, funcCode, addr, value.Length, value);

            // 发送数据帧并读取回帧
            if (UnicastMessage(inData, fbData) == false)
            {
                throw new Exception("写指令无法收到系统的正确回帧!");
            }
        }

        /// <summary>
        /// 发送MODBUS协议的读命令
        /// </summary>
        /// <param name="devNo">节点号</param>
        /// <param name="funcCode">功能码</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="len">读字节数</param>
        /// <param name="inData">发送帧</param>
        /// <param name="fbData">反馈帧</param>
        public void ReadCommand(int devNo, byte funcCode, UInt16 addr, int len, List<byte> inData, List<byte> fbData)
        {
            // 获得读功能的MODBUS RTU协议帧
            inData = protocalProxy.GetModbusProtocalFrame(devNo, funcCode, addr, len);

            // 发送数据帧并读取回帧
            if (UnicastMessage(inData, fbData) == false) 
            {
                throw new Exception("写指令无法收到系统的正确回帧!");
            }
        }
    }
}
