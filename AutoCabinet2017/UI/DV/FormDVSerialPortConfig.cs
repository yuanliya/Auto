using System;
using System.Collections.Generic;
using System.Windows.Forms;

using NJUST.AUTO06.Utility;

using AutoCabinet2017.Helper;

namespace AutoCabinet2017.UI.DV
{
    public partial class FormDVSerialPortConfig : Form
    {
        public FormDVSerialPortConfig()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载系统当前的串口
        /// </summary>
        private void LoadSerialPortItems()
        {
            try
            {
                // 利用C#的SerialPort类读取系统当前的串口
                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    cbxSerialPortNo.Items.Add(port);
                }
            }       
            catch(Exception)
            {
                throw new Exception("没有找到可供使用的串口！");
            }
        }

        /// <summary>
        /// 更新界面串口参数显示
        /// </summary>
        /// <param name="item"></param>
        private void UpdateUISerialPortInfo(SCommItem item)
        {
            // 界面显示
            cbxSerialPortNo.Text = item.Name;
            cbxBaud.Text         = item.Baud.ToString();
            cbxDataBits.Text     = item.DataBits.ToString();
            cbxStopBits.Text     = item.StopBits.ToString();
            cbxParity.Text       = item.Parity;
        }

        /// <summary>
        /// 串口信息更新到XML文件
        /// </summary>
        /// <param name="item"></param>
        private void UpdateXmlSerialPortInfo(SCommItem item)
        {
            // 界面显示
            item.Name     = cbxSerialPortNo.Text;
            item.Baud     = Convert.ToInt32(cbxBaud.Text);
            item.DataBits = Convert.ToInt32(cbxDataBits.Text);
            item.StopBits = cbxStopBits.Text;
            item.Parity   = cbxParity.Text;
        }

        /// <summary>
        /// 更新属性类串口信息
        /// </summary>
        /// <param name="item"></param>
        private void UpdatePropertySerialPortInfo(SCommItem item)
        {
            // 属性类
            PropertyHelper.SCommInfo.Tag  = item.Tag;
            PropertyHelper.SCommInfo.Name = item.Name;
            PropertyHelper.SCommInfo.Baud = item.Baud;
            PropertyHelper.SCommInfo.DataBits = item.DataBits;
            PropertyHelper.SCommInfo.StopBits = item.StopBits;
            PropertyHelper.SCommInfo.Parity   = item.Parity;
        }

        /// <summary>
        /// 打开Xml文件
        /// </summary>
        /// <returns>包含配置信息的类实例</returns>
        private SCommXml OpenSCommXml()
        {
            string fileName = Application.StartupPath + "\\XML\\MySettings.xml";

            // 打开XML文件
            XmlSerializeHelper<SCommItem, SCommXml>.OpenXmlFile(fileName);
            // 读取XML文件
            return XmlSerializeHelper<SCommItem, SCommXml>.ReadXML();
        }

        private void FormDVSerialPortConfig_Load(object sender, EventArgs e)
        {
            SCommXml xml = null;

            try
            {
                // 加载系统串口信息
                LoadSerialPortItems();

                // 读XML文件
                xml = OpenSCommXml();
            }
            catch(Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
                   
                return;
            }

            SCommItem item = (SCommItem)xml["主控串口"];
            if (item == null)
            {
                MessageUtil.ShowTips("无法从配置文件中找到串口信息，使用默认串口COM1");

                lblComm.Text = "COM1";
            }
            else
            {
                lblComm.Text = item.Name;
            }

            // 更新界面显示
            UpdateUISerialPortInfo(item);
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOk_Click(object sender, EventArgs e)
        {
            if (cbxSerialPortNo.Text == "")
            {
                MessageUtil.ShowTips("请先选择串口号！");
                return;
            }

            // 打开系统配置文件，写入串口配置信息
            try
            {
                // 读XML文件
                SCommXml xml = OpenSCommXml();
                // 读取当前串口配置
                SCommItem item = (SCommItem)xml["主控串口"];

                // 更新XML中的串口信息
                UpdateXmlSerialPortInfo(item);
                XmlSerializeHelper<SCommItem, SCommXml>.WriteXML(xml);
                // 更新属性类中的串口信息
                UpdatePropertySerialPortInfo(item);

                if (MessageUtil.ShowTips("串口设置成功！") == DialogResult.OK)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }
        }        
    }
}
