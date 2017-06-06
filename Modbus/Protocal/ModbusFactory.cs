using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace NJUST.AUTO06.Modbus.Protocal
{
    public sealed class ModbusFactory
    {
         #region 单例模式

        ModbusFactory()
        { }

        public static readonly ModbusFactory Instance = new ModbusFactory();

        #endregion

        /// <summary>
        /// 从配置文件中读取通信模式信息
        /// </summary>
        /// <returns></returns>
        private string GetCommunicationProtocal()
        {
            string commProtocal = null;

            try
            {
                commProtocal = ConfigurationManager.AppSettings["CommunicationProtocal"].ToString();
            }
            catch (ConfigurationErrorsException)
            {
                throw new Exception("未能从配置文件中读取通信模式信息!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return commProtocal;
        }

        /// <summary>
        /// 工厂模式实现通信类对象
        /// </summary>
        /// <param name="DevAddr"></param>
        /// <returns></returns>
        public IModbusProtocal GetProtocalProxy()
        {
            IModbusProtocal proxy = null;

            // 从配置文件中读取当前设备的通信模式
            string commMode = GetCommunicationProtocal();

            switch (commMode)
            {
                case "ModbusRTU":       // 设备采用串口通信
                    // 生成串口类对象
                    proxy = new ModbusRTUProtocal();

                    break;

                case "ModbusTCP":  // 设备采用以太网通信
                    // 生成以太网对象
                    proxy = new ModbusTCPProtocal();

                    break;

                default:
                    break;
            }

            return proxy;
        }
    }
}
