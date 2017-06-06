using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO.Ports;

using NJUST.AUTO06.Utility;

using NJUST.AUTO06.Modbus.IO;
using NJUST.AUTO06.Modbus.Protocal;

using AutoCabinet2017.Helper;
using AutoCabinet2017.Controller;

using ZY.EntityFrameWork.Core.Model.Dto;

namespace AutoCabinet2017.UI.DV
{
    public partial class FormDVDeviceControl : Form
    {
        private const int MONITOR_DELAY_TIME  = 200;   // 监控启动延时
        private const int MONITOR_PERIOD_TIME = 500;   // 监控周期

        private const int EVENT_RUN    = 0x01;  // 电机运行事件
        private const int EVENT_UNRUN  = 0x02;  // 电机未运行事件

        // 数字按键集合
        private List<Button> lstbtns = new List<Button>();

        // 通信设备
        private SerialPort         spComm     = null;
        // 控制器对象
        private HZKController      Controller = null;
        // 命令处理类
        private CommandInvoker commandInvoker = null;

        // 保存系统管理的所有回转库信息
        private List<DeviceDto> listCabinetInfo = null;
        // 目标层
        private int DST_Layer = 1;

        #region 系统状态显示图标定义

        // “层号按钮”背景
        private Image BtnPressed = Properties.Resources.BtnPressed; // 数字键按下状态
        private Image BtnOrigin  = Properties.Resources.BtnOrigin;  // 数字键抬起状态
        private Image BtnUnused  = Properties.Resources.BtnUnused;  // 数字键禁用状态

        #endregion 

        public FormDVDeviceControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化数字按键设置
        /// </summary>
        private void InitButtons()
        {
            // 默认每个回转库最大层号为16层
            lstbtns.Clear();

            // 遍历所有控件
            for (int i = 0; i < 16; i++)
            {
                foreach (Button control in panel1.Controls)
                {
                    // 将数字按键Button按照顺序加入控件列表
                    int tag = Convert.ToInt32(control.Tag);
                    if (tag == (i + 1))
                    {
                        lstbtns.Add(control);                 // 控件加入list
                        control.BackgroundImage = BtnOrigin;  // 设置默认图片
                    }
                }
            }
        }

        /// <summary>
        /// 窗体加载初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDVDeviceControl_Load(object sender, EventArgs e)
        {
            // 初始化数字按键设置
            InitButtons();
            // 设置数字键状态
            SetDigitalButton(Convert.ToInt32(listCabinetInfo[0].CabinetLayers));

            try
            {
                // 初始化控制设备，采用ModbusRTU协议支持的串口
                if (Controller == null)
                {
                    // 从配置文件中读取设备配置信息并创建对象
                    spComm = CommonHelper.Instance.CreateSerialPortObject("主控串口");
                    // 打开串口
                    spComm.Open();
                    // 设置读超时
                    spComm.ReadTimeout = ModbusCommand.DefaultTimeout;

                    // 创建设备控制类:采用以太网的TCP客户端通信和ModbusTCP协议
                    Controller = new HZKController(new SerialPortAdapter(spComm), ModbusFactory.Instance.GetProtocalProxy());
                }

                // 初始化命令处理类
                commandInvoker = new CommandInvoker();
                // 订阅命令处理的事件
                commandInvoker.OnCommandError         += UpdateCommandErrorInfo;           // 命令执行错误的事件
                commandInvoker.OnCommandExecute       += UpdateCommandExecuteInfo;         // 命令执行正确的事件

                // 初始化监控类

//                commandInvoker.OnDeviceMonitorMessage += UpdateClipBoardInfo;              // 更新信息栏时间
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
                return;
            }
        }

        #region 事件处理

        /// <summary>
        /// 控制命令无法执行的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void UpdateCommandErrorInfo(Object sender, CommandExceptionEventArgs args)
        {
            string info = string.Empty;

            if (args.Exception is ControlCheckException)
            {
                // 系统无法进入控制引发的异常
                info = string.Format("无法控制{0}号设备：{1}", args.DevNo, args.Exception.Message);
                lsbInfoBoard.Items.Add(info);

                return;
            }

            if (args.Exception is TimeoutException)
            {
                // 与节点通信发生超时错误
                info = string.Format("{0}号设备通信超时无响应！", args.DevNo);
                lsbInfoBoard.Items.Add(info);

                return;
            }

            // 操作未完成：通信错误、节点无响应、
            info = string.Format("{0}号设备通信收到错误回帧！", args.DevNo);
            lsbInfoBoard.Items.Add(info);
        }

        /// <summary>
        /// 控制命令执行的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void UpdateCommandExecuteInfo(Object sender, CommandExecuteEventArgs args)
        {
            string info = string.Empty;

            info = string.Format("{0}号设备：{1}操作命令已执行!", args.DevNo, args.CmdType);
            lsbInfoBoard.Items.Add(info);
        }

        /// <summary>
        /// 更新信息栏事件
        /// </summary>
        /// <param name="info"></param>
        public void UpdateClipBoardInfo(string info)
        {
            if (this.InvokeRequired)
            {
               // this.Invoke(new DeviceMonitor.DeviceMonitorMessageEventHandler(UpdateClipBoardInfo), new object[] { info });
            }
            else
            {
                lsbInfoBoard.Items.Add(info);
            }
        }

        /// <summary>
        /// 显示上位机和PLC之间的MODBUS协议帧
        /// </summary>
        /// <param name="inData">发送帧</param>
        /// <param name="fbData">反馈帧</param>
        public void OnModbusFrameShow(List<byte> inData, List<byte> fbData)
        {
            string instr = "";
            string fbstr = "";

            if (this.InvokeRequired)
            {
                // 从不是创建该控件的线程以外的地方调用本函数（跨线程调用）
                this.Invoke(new HZKController.ModbusFrameShowEventHandle(OnModbusFrameShow), new Object[] { inData, fbData });
            }
            else
            {
                foreach (byte a in inData)
                {
                    instr += a.ToString();
                    instr += " ";
                }
                foreach (byte b in fbData)
                {
                    fbstr += b.ToString();
                    fbstr += " ";
                }

                lsbInfoBoard.Items.Add("提示：发送MODBUS RTU协议帧内容为：" + instr);

                lsbInfoBoard.Items.Add("提示：反馈MODBUS RTU协议帧内容为：" + fbstr);
            }
        }

        #endregion

        /// <summary>
        /// 显示指示灯状态
        /// </summary>
        /// <param name="img">指示灯图片</param>
        /// <param name="status">设备状态</param>
        /// 新修改picture
        private void SetLEDStatus(PictureBox pb, int status)
        {
            //if (status == EVENT_UNRUN) 
            //{
            //    // 指示灯显示停止状态
            //    pb.Image = imgLedStop;
            //}

            //if (status == EVENT_RUN)
            //{
            //    // 指示灯显示运行闪烁状态
            //    if ((pb.Image == imgLedStop) || (pb.Image == imgLedRun2))
            //    {
            //        pb.Image = imgLedRun1;
            //    }
            //        //新修改
            //    else if (pb.Image == imgLedRun1)
            //    {
            //        pb.Image = imgLedRun2;
            //    }
            //}
        }

        #region 回转库操作

        /// <summary>
        /// 开门 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            // 检查回转库编号的合理性
            if(string.IsNullOrEmpty(lblGroupNo.Text) || (Convert.ToInt32(lblGroupNo.Text) == 0))
            {
                MessageUtil.ShowWarning("回转库编号没有正确配置，请检查设置！");
                return;
            }

            // 执行开门指令
            try
            {
                // 开门命令
                OpenDoorCommand cmd = new OpenDoorCommand(Controller, Convert.ToInt32(lblGroupNo.Text));
                commandInvoker.AddCommand(cmd);

                // 执行命令
                commandInvoker.ExecuteCommand();
            }
            catch(Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
                return;
            }
        }

        /// <summary>
        /// 关闭柜门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseDoor_Click(object sender, EventArgs e)
        {
            // 检查回转库编号的合理性
            if (string.IsNullOrEmpty(lblGroupNo.Text) || (Convert.ToInt32(lblGroupNo.Text) == 0))
            {
                MessageUtil.ShowWarning("回转库编号没有正确配置，请检查设置！");
                return;
            }

            // 执行关门指令
            try
            {
                //Controller.CloseDoor_Execute(Convert.ToInt32(lblGroupNo.Text));
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
                return;
            }

            // 在信息窗口显示信息
            lsbInfoBoard.Items.Add("提示:开始执行关门操作!");

            // 延时500ms启动监控
            Thread.Sleep(MONITOR_DELAY_TIME);

            //// 关门指令
            //devMonitor.CtrlCommand = HZK_COMMAND.CMD_CLOSEDOOR;
            //// 节点号
            //devMonitor.GroupNo = Convert.ToInt32(lblGroupNo.Text);
            //// 启动监控
            //devMonitor.StartDevMonitor();
        }

        /// <summary>
        /// 走层操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            // 检查回转库编号的合理性
            if(string.IsNullOrEmpty(lblGroupNo.Text) || (Convert.ToInt32(lblGroupNo.Text) == 0))
            {
                MessageUtil.ShowWarning("回转库编号没有正确配置，请检查设置！");
                return;
            }

            // 执行走层指令
            try
            {
                //Controller.Run_Execute(Convert.ToInt32(lblGroupNo.Text), DST_Layer);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
                return;
            }

            // 走层时不允许更改回转库编号
            btnLeft.Enabled  = false;
            btnRight.Enabled = false;

            // 在信息窗口显示信息
            lsbInfoBoard.Items.Add(String.Format("提示:{0}号回转库开始执行走层操作，目标层:第{1}层!", lblGroupNo.Text, DST_Layer));

            // 延时500ms启动监控
            Thread.Sleep(MONITOR_DELAY_TIME);

            // 走层指令
            //devMonitor.CtrlCommand = HZK_COMMAND.CMD_EXECUTE;
            //// 节点号
            //devMonitor.GroupNo = Convert.ToInt32(lblGroupNo.Text);
            //// 目的层  
            //devMonitor.DstLayerNo = DST_Layer;
            //// 启动监控
            //devMonitor.StartDevMonitor();         
        }

        /// <summary>
        /// 停止系统运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            // 检查回转库编号的合理性
            if (string.IsNullOrEmpty(lblGroupNo.Text) || (Convert.ToInt32(lblGroupNo.Text) == 0))
            {
                MessageUtil.ShowWarning("回转库编号没有正确配置，请检查设置！");
                return;
            }
           
            //try
            //{
            //    // 停止监控
            //    devMonitor.StopDevMonitor();
            //    // 执行停止指令
            //    Controller.Stop_Execute(Convert.ToInt32(lblGroupNo.Text));
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowWarning(ex.Message);
            //    return;
            //}

            // 在信息窗口显示信息
            lsbInfoBoard.Items.Add(String.Format("提示:{0}号回转库停止运行!", lblGroupNo.Text));

            // 延时500ms启动监控
            Thread.Sleep(MONITOR_DELAY_TIME);

            // 停止指令
            //devMonitor.CtrlCommand = HZK_COMMAND.CMD_STOP;
            //// 节点号
            //devMonitor.GroupNo = Convert.ToInt32(lblGroupNo.Text);
            //// 启动监控
            //devMonitor.StartDevMonitor();      
        }

        /// <summary>
        /// 检测通信状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheck_Click(object sender, EventArgs e)
        {
            int i = 0;

            //if(!Controller.IsConnected())
            //{
            //    lsbInfoBoard.Items.Add("警告:串口不存在或没有正确连接!");
            //    return;
            //}

            //try
            //{
            //    Controller.Connect();
            //}
            //catch (Exception ex)
            //{
            //    lsbInfoBoard.Items.Add("警告: " + ex.Message);
            //    return;
            //}

            lsbInfoBoard.Items.Add("开始检测回转库的通信状态!");

            // 检测系统内所有回转库的通信状态
            int curLayerNo = 0;
            int sysStat    = 0;
            string strTemp = null;
            for(i = 0; i < listCabinetInfo.Count; i++)
            {
                try
                {
                    // 查询当前层和系统状态
                  //  Controller.GetSystemStat(i + 1, ref curLayerNo, ref sysStat);
                    // 正常状态
                    strTemp = String.Format("{0}号回转库: 通讯正常，当前层: 第{1}层！", i + 1, curLayerNo);
                }
                catch(Exception)
                {
                    // 异常状态
                    strTemp = String.Format("警告:{0}号回转库:通讯不正常，请检查设备是否上电及通讯口是否连接！", i + 1);
                }
                finally
                {
                    // 显示状态信息
                    lsbInfoBoard.Items.Add(strTemp);
                }                          
            }
        }

        #endregion

        #region 处理数字按键和回转库编号

        /// <summary>
        /// 设置数字按键状态
        /// </summary>
        /// <param name="maxLayers"></param>
        private void SetDigitalButton(int maxLayers)
        {
            int i = 0;

            // 设置数字键盘按键状态，超出最大层的按键禁用
            for (i = 0; i < maxLayers; i++)
            {
                lstbtns[i].BackgroundImage = BtnOrigin;
                lstbtns[i].Enabled = true;
            }

            for (i = maxLayers; i < 16; i++)
            {
                lstbtns[i].BackgroundImage = BtnUnused;
                lstbtns[i].Enabled = false;
            }
        }

        /// <summary>
        /// 更改回转库编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lblGroupNo_TextChanged(object sender, EventArgs e)
        {
            // 回转库编号
            //int curGroup = Convert.ToInt32(lblGroupNo.Text);
            //// 设置数字按键使能状态
            //SetDigitalButton(Convert.ToInt32(listCabinetInfo[curGroup - 1].CabinetLayers));

            //if (Controller == null) return;

            //if(!Controller.IsConnected())
            //{
            //    MessageUtil.ShowWarning("PC机串口未连接或正确打开，请检查设置！");
            //    return;
            //}

            //// 查询系统状态
            //int curLayerNo = 10, sysStat = 0;

            //try
            //{
            //    // 查询当前层和系统状态
            //    Controller.GetSystemStat(Convert.ToInt32(lblGroupNo.Text), ref curLayerNo, ref sysStat);
            //    // 显示连接图标
            //    pbConnStatus.Image = imgConnect;
            //    // 显示当前层
            //    lblCurLayer.Text = curLayerNo.ToString();
            //}
            //catch (Exception)
            //{
            //  //  MessageUtil.ShowWarning(String.Format("与{0}号回转库通讯不正常，请检查设备是否上电及通讯口是否连接！", curGroup));
            //    // 显示未连接图标
            //    pbConnStatus.Image = imgUnConnect;

            //    return;
            //}
        }

        /// <summary>
        /// 减小回转库编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLeft_Click(object sender, EventArgs e)
        {
            int curGroup = Convert.ToInt32(lblGroupNo.Text);

            if (curGroup == 1)
            {
                lblGroupNo.Text = listCabinetInfo.Count.ToString();
            }
            else
            {
                lblGroupNo.Text = (--curGroup).ToString();
            }
        }

        /// <summary>
        /// 增大回转库编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRight_Click(object sender, EventArgs e)
        {
            int curGroup = Convert.ToInt32(lblGroupNo.Text);

            if (curGroup == listCabinetInfo.Count)
            {
                // 回转库编号到上限
                lblGroupNo.Text = "1";
            }
            else
            {
                lblGroupNo.Text = (++curGroup).ToString();
            }
        }

        /// <summary>
        /// 界面上按下数字键的处理
        /// </summary>
        /// <param name="sender">产生事件的控件</param>
        /// <param name="e">相关参数</param>
        private void DigitalButton_Click(object sender, EventArgs e)
        {
            DigitalButton_Dispose((Button)sender);
        }

        /// <summary>
        /// 处理数字键点击动作
        /// </summary>
        /// <param name="btnItem">数字按键</param>
        private void DigitalButton_Dispose(Button btnItem)
        {
            if (btnItem.BackgroundImage == BtnOrigin)
            {
                // 回转库编号
                int curGroup = Convert.ToInt32(lblGroupNo.Text);
                int maxLayer = Convert.ToInt32(listCabinetInfo[curGroup - 1].CabinetLayers);
                // 设置数字键盘按键状态，超出最大层的按键禁用
                for (int i = 0; i < maxLayer; i++)
                {
                    if (lstbtns[i] != btnItem)
                    {
                        lstbtns[i].BackgroundImage = BtnOrigin;
                    }
                }

                // 设定目标层
                DST_Layer = Convert.ToInt32(btnItem.Tag.ToString());
               
                // 数字键处于按下状态
                btnItem.BackgroundImage = BtnPressed;
            }
            else
            {
                
                // 数字键处于未按下状态
                btnItem.BackgroundImage = BtnOrigin;
                
            }
        }

        #endregion

        #region ListBox处理

        /// <summary>
        /// 清空ListBox显示内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void muClear_Click(object sender, EventArgs e)
        {
            lsbInfoBoard.Items.Clear();
        }

        /// <summary>
        /// 动态修改ListBox中文字的颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsbInfoBoard_DrawItem(object sender, DrawItemEventArgs e)
        {
            Brush FontBrush = Brushes.Black;
            ListBox listBox = sender as ListBox;

            if (e.Index > -1)
            {
                e.DrawBackground();

                if (listBox.Items[e.Index].ToString().IndexOf("警告") == 0)
                {
                    FontBrush = Brushes.Red;
                }
                if (listBox.Items[e.Index].ToString().IndexOf("提示") == 0)
                {
                    FontBrush = Brushes.Blue;
                }

                e.Graphics.DrawString(listBox.Items[e.Index].ToString(), e.Font, FontBrush, e.Bounds, StringFormat.GenericDefault);
                e.DrawFocusRectangle();
            }
        }

        #endregion

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDVDeviceControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Controller == null) return;

            //if (devMonitor != null)
            //{
            //    // 停止监控
            //    devMonitor.StopDevMonitor();
            //}

            //if (Controller.IsConnected())
            //{
            //    // 关闭串口
            //    Controller.DisConnect();
            //}
        }
    }
}
