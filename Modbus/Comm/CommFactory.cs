using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.IO.Ports;
using System.Xml;
using System.Windows.Forms;

using NJUST.AUTO06.Utility;

namespace NJUST.AUTO06.Modbus.Comm
{
    public sealed class CommFactory
    {
        #region 单例模式

        CommFactory()
        { }

        public static readonly CommFactory Instance = new CommFactory();

        #endregion

        /// <summary>
        /// 串口校验位格式转换
        /// </summary>
        /// <param name="parity"></param>
        /// <returns></returns>
        private Parity ConvertToParity(string parity)
        {
            Parity parityBit = Parity.None;

            switch (parity)
            {
                case "None":
                    parityBit = Parity.None;
                    break;
                case "Odd":
                    parityBit = Parity.Odd;
                    break;
                case "Even":
                    parityBit = Parity.Even;
                    break;
                default:
                    break;
            }

            return parityBit;
        }

        /// <summary>
        /// 串口停止位格式转换
        /// </summary>
        /// <param name="stopBit"></param>
        /// <returns></returns>
        private StopBits ConvertToStopBits(string stopBit)
        {
            StopBits stopBits = StopBits.One;

            switch (stopBit)
            {
                case "1":
                    stopBits = StopBits.One;
                    break;
                case "1.5":
                    stopBits = StopBits.OnePointFive;
                    break;
                case "2":
                    stopBits = StopBits.Two;
                    break;
                case "0":
                    stopBits = StopBits.None;
                    break;
                default:
                    break;
            }

            return stopBits;
        }

        /// <summary>
        /// 生成串口对象
        /// </summary>
        /// <returns></returns>
        private ICommunicationProxy CreateSerialPortObject(string devTag)
        {
            // 从配置文件读取串口信息
            string filePath = Application.StartupPath + "\\XML\\MySettings.xml";

            try 
            {
                // 打开XML文件
                XmlSerializeHelper<SCommItem, SCommXml>.OpenXmlFile(filePath);
                // 读取XML文件
                SCommXml xml = XmlSerializeHelper<SCommItem, SCommXml>.ReadXML();
                // 获得指定名称的串口信息
                SCommItem item = (SCommItem)xml[devTag];

                // 校验位转换
                Parity parity = ConvertToParity(item.Parity);
                // 停止位
                StopBits stopBits = ConvertToStopBits(item.StopBits);

                // 生成串口类对象
                return new ModbusRTUSerialPort(item.Name, item.Baud, parity, item.DataBits, stopBits);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }         
        }

        /// <summary>
        /// 生成以太网对象
        /// </summary>
        /// <param name="DevAddr"></param>
        /// <returns></returns>
        private ICommunicationProxy CreateEthernetObject(string devTag)
        {
            return null;
        }

        /// <summary>
        /// 从配置文件中读取通信模式信息
        /// </summary>
        /// <returns></returns>
        private string GetCommunicationMode()
        {
            string commMode = null;

            try
            {
                commMode = ConfigurationManager.AppSettings["CommunicationMode"].ToString();
            }
            catch (ConfigurationErrorsException)
            {
                throw new Exception("未能从配置文件中读取通信模式信息!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return commMode;
        }

        /// <summary>
        /// 工厂模式实现通信类对象
        /// </summary>
        /// <param name="devTag">标识设备标记：串口--配置文件定义的串口标识名
        ///                                    网口--网络所在设备的节点号
        ///                                    </param>
        /// <returns></returns>
        public ICommunicationProxy GetCommunicationProxy(string devTag) 
        {
            ICommunicationProxy proxy = null;

            // 从配置文件中读取当前设备的通信模式
            string commMode = GetCommunicationMode();
            
            switch(commMode)
            {
                case "COM":       // 设备采用串口通信
                    // 生成串口类对象
                    proxy = CreateSerialPortObject(devTag); 

                    break;

                case "ETHERENT":  // 设备采用以太网通信
                    // 生成以太网对象
                    proxy = CreateEthernetObject(devTag);

                    break;

                default:
                    break;
            }

            return proxy;
        }
    }
}
