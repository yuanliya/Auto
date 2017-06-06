using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Ports;
using System.Windows.Forms;

using NJUST.AUTO06.Modbus.Protocal;
using NJUST.AUTO06.Utility;

namespace AutoCabinet2017.Helper
{
    public sealed class CommonHelper
    {
        // 私有构造函数
        private CommonHelper()
        { }

        // 单例模式的类对象
        public static readonly CommonHelper Instance = new CommonHelper();

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
        public SerialPort CreateSerialPortObject(string devTag)
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

                // 生成串口
                return new SerialPort(item.Name, item.Baud, parity, item.DataBits, stopBits);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
