using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using NJUST.AUTO06.DBORM.DBEntity.MODEL;
using NJUST.AUTO06.DBORM.DBEntity.BLL;

using NJUST.AUTO06.Utility;

using HZK.Helper;
using HZK.Device;

using NJUST.AUTO06.Modbus.IO;
using NJUST.AUTO06.Modbus.Protocal;

using NJUST.AUTO06.NetService;

namespace HZK.UI.DV
{
    public partial class FormDVDevControl : Form
    {
        // 控制器对象
        private MJJController Controller = null;
        // 通信类对象
        private AsyncTcpClient tcpClient = new AsyncTcpClient();
        // 连接服务器完成，默认阻塞当前线程  
        private static ManualResetEvent connectDone = new ManualResetEvent(false);

        // 命令处理类
        private CommandInvoker commandInvoker = null;
        // 设备监控类
        private DeviceMonitor devMonitor = null;
        // 监控周期
        private const int MONITOR_PERIOD_TIME = 1000;

        // 保存选中的密集架信息
        private List<Rack> lstRack = new List<Rack>();

        // 密集架温湿度上下限信息
        private int temperMax = 0, temperMin = 0;
        private int humMax = 0, humMin = 0;

        // 定时器计数信息
        private int timerCount = 0;

        public FormDVDevControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 读取相应区号密集架信息
        /// </summary>
        /// <param name="groupNo"></param>
        /// <returns></returns>
        private Rack GetRackInfo(int groupNo)
        {
            int i;

            List<Rack> rack = lstRack.Where(q => q.RackNo == groupNo).ToList();

            if (rack.Count == 0)
            {
                throw new Exception("没有该区号的密集架信息！");
            }

            // 读取数据库保存的温湿度上下限信息
            temperMax = rack[0].TemperatureMax;
            temperMin = rack[0].TemperatureMin;

            humMax = rack[0].HumidityMax;
            humMin = rack[0].HumidityMin;

            // 添加列号及列方向（A/B面）
            string item;
            for (i = 0; i < rack[0].RackColumns + 1; i++)
            {
                item = (i + 1).ToString() + "A";
                cbxColumnNo.Items.Add(item);

                item = (i + 1).ToString() + "B";
                cbxColumnNo.Items.Add(item);
            }

            // 默认1列A面
            cbxColumnNo.SelectedIndex = 0;

            return rack[0];
        }

        /// <summary>
        /// Form加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FormDVDevControl_Load(object sender, EventArgs e)
        {
            //Control.CheckForIllegalCrossThreadCalls = false;
            // 默认区号为1
            cbxGroupNo.SelectedIndex = -1;

            // 查找所有的设备
            lstRack = RackBLL.Instance.Find("*", "");

            cbxGroupNo.Items.Clear();
            // 添加设备区号
            if (lstRack.Count > 0)
            {
                // 排序
                //lstRack.Sort();
                lstRack = lstRack.OrderBy(q => q.RackNo).ToList();
                // 添加进下拉框
                foreach (var item in lstRack)
                {
                    cbxGroupNo.Items.Add(item.RackNo);
                }
            }

            // 订阅连接服务器时间
            tcpClient.OnConnected += OnTcpServerConnected;
            tcpClient.OnConnecting += OnTcpServerConnecting;
            tcpClient.OnError += OnTcpClientCommError;

            // 设置TCPClient的读写超时时间
            tcpClient.ReadTimeout = 500;
            tcpClient.WriteTimeout = 500;

            // 创建设备控制类:采用串口通信和Modbus协议
            Controller = new MJJController(new TcpClientAdapter(tcpClient), ModbusFactory.Instance.GetProtocalProxy());

            // 初始化设备监控类
            devMonitor = new DeviceMonitor(Controller, MONITOR_PERIOD_TIME);
            // 订阅设备运行状态信息事件
            devMonitor.OnDeviceMonitorMessage += UpdateClipBoardInfo;

            // 初始化命令处理类
            commandInvoker = new CommandInvoker(devMonitor);
            // 订阅命令处理的事件
            commandInvoker.OnCommandError += UpdateCommandErrorInfo;
            commandInvoker.OnCommandExecute += UpdateCommandExecuteInfo;
        }

        #region 事件处理

        /// <summary>
        /// 成功连接服务器的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTcpServerConnected(Object sender, ConnectEventArgs args)
        {
            // 信号量置位，成功连接服务器
            connectDone.Set();

            string info = string.Empty;

            // 控件的跨线程调用处理
            if (cbxGroupNo.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                //Action<string> actionDelegate = (x) =>
                //{
                //    info = string.Format("成功连接{0}区服务器：IP地址{1}  端口号{2}", x.ToString(),
                //        (args.RemoteEndPoint as IPEndPoint).Address.ToString(), (args.RemoteEndPoint as IPEndPoint).Port);
                //};

                Action actionDelegate = () =>
                {
                    info = string.Format("成功连接{0}区服务器：IP地址{1}  端口号{2}", cbxGroupNo.Text,
                        (args.RemoteEndPoint as IPEndPoint).Address.ToString(), (args.RemoteEndPoint as IPEndPoint).Port);
                };

                this.cbxGroupNo.Invoke(actionDelegate);
            }
            else
            {
                info = string.Format("成功连接{0}区服务器：IP地址{1}  端口号{2}", cbxGroupNo.Text,
                       (args.RemoteEndPoint as IPEndPoint).Address.ToString(), (args.RemoteEndPoint as IPEndPoint).Port);
            }

            // 更新信息栏
            UpdateClipBoardInfo(info);
        }

        /// <summary>
        /// 启动连接服务器的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTcpServerConnecting(Object sender, ConnectEventArgs args)
        {
            // 信号量复位，堵塞主流程
            connectDone.Reset();

            string info = string.Empty;

            // 控件的跨线程调用处理
            if (cbxGroupNo.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                Action actionDelegate = () =>
                {
                    info = string.Format("开始连接{0}区服务器：IP地址{1}  端口号{2}", cbxGroupNo.Text,
                        (args.RemoteEndPoint as IPEndPoint).Address.ToString(), (args.RemoteEndPoint as IPEndPoint).Port);
                };

                this.cbxGroupNo.Invoke(actionDelegate);
            }
            else
            {
                info = string.Format("开始连接{0}区服务器：IP地址{1}  端口号{2}", cbxGroupNo.Text,
                       (args.RemoteEndPoint as IPEndPoint).Address.ToString(), (args.RemoteEndPoint as IPEndPoint).Port);
            }

            // 更新信息栏
            UpdateClipBoardInfo(info);
        }

        /// <summary>
        /// 通信错误事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void OnTcpClientCommError(Object sender, ExceptEventArgs args)
        {
            string info = string.Empty;

            // 控件的跨线程调用处理
            if (cbxGroupNo.InvokeRequired)
            {
                // 当一个控件的InvokeRequired属性值为真时，说明有一个创建它以外的线程想访问它
                //Action<string> actionDelegate = (x) => { info = string.Format("与{0}区服务器通信错误：{1}", x.ToString(), args.Exception.Message); };
                Action actionDelegate = () => { info = string.Format("与{0}区服务器通信错误：{1}", cbxGroupNo.Text, args.Exception.Message); };
                this.cbxGroupNo.Invoke(actionDelegate);
            }
            else
            {
                info = string.Format("与{0}区服务器通信错误：{1}", cbxGroupNo.Text, args.Exception.Message);
            }

            // 更新信息栏
            UpdateClipBoardInfo(info);
        }

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
                info = string.Format("无法控制{0}区系统：{1}", args.DevNo, args.Exception.Message);

                UpdateClipBoardInfo(info);

                return;
            }

            if (args.Exception is TimeoutException)
            {
                // 与节点通信发生超时错误
                info = string.Format("{0}区系统：通信超时无响应！", args.DevNo);

                UpdateClipBoardInfo(info);

                return;
            }

            // 操作未完成：通信错误、节点无响应、
            info = string.Format("{0}区系统：通信收到错误回帧！", args.DevNo);

            UpdateClipBoardInfo(info);
        }

        /// <summary>
        /// 控制命令执行的事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        public void UpdateCommandExecuteInfo(Object sender, CommandExecuteEventArgs args)
        {
            string info = string.Empty;

            info = string.Format("{0}区系统：{1}操作命令已执行!", args.DevNo, args.CmdType);

            UpdateClipBoardInfo(info);
        }

        /// <summary>
        /// 更新信息栏事件
        /// </summary>
        /// <param name="info"></param>
        public void UpdateClipBoardInfo(string info)
        {
            if (this.Disposing || this.IsDisposed)
            {
                return;
            }
            if (this.InvokeRequired)
            {
                try
                {
                    this.Invoke(new DeviceMonitor.DeviceMonitorMessageEventHandler(UpdateClipBoardInfo), new object[] { info });
                }
                catch (Exception)
                {
                    return;
                }

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
                this.Invoke(new MJJController.ModbusFrameShowEventHandle(OnModbusFrameShow), new Object[] { inData, fbData });
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

        // 更新温湿度环境信息
        private void UpdateEnvironmentInfo()
        {
            List<byte> temp = new List<byte>();

            // 检测当前温湿度
            temp = Controller.GetCurTemperHum(1);
            if (temp == null)
            {
                MessageBox.Show("检测当前温湿度值失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 显示温度
            //txtTemp.Text = Convert.ToString((temp[2] * 256 + temp[3]) / 10);
            //txtTemp.Text = Convert.ToString((temp[2] * 256 + temp[3]) * 80 / 1650-20);
            double temp1 = (temp[0] * 256 + temp[1]) * 80.0 / 1650 - 20;
            txtTemp.Text = temp1.ToString("f1");

            //判断温度是否超标
            if ((temp1 > temperMax) || temp1 < temperMin)
            {
                labTempUp.ForeColor = Color.Red;   // 超标显示红色
            }
            else
            {
                labTempUp.ForeColor = Color.Lime;  // 正常显示绿色
            }

            // 显示湿度
            //txtHum.Text = Convert.ToString((temp[0] * 256 + temp[1]) / 10);
            temp1 = (temp[2] * 256 + temp[3]) / 10;
            txtHum.Text = temp1.ToString("f1");


            // 判断湿度是否超标
            if ((temp1 > humMax) || temp1 < humMin)
            {
                labHumUp.ForeColor = Color.Red;
            }
            else
            {
                labHumUp.ForeColor = Color.Lime;
            }
        }

        /// <summary>
        /// 开架
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxGroupNo.Text) || string.IsNullOrEmpty(cbxColumnNo.Text))
            {
                MessageBox.Show("请指定区号和列号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 列号
            int columnNo = Convert.ToInt32(cbxColumnNo.Text.Substring(0, cbxColumnNo.Text.ToString().Length - 1));
            // 列方向
            string columnDir = cbxColumnNo.Text.Substring(cbxColumnNo.Text.ToString().Length - 1, 1);

            // 开架命令(所有的站号都是1)
            OpenCommand cmd = new OpenCommand(Controller, Convert.ToInt32(cbxGroupNo.Text), columnNo, columnDir);
            commandInvoker.AddCommand(cmd);

            // 执行命令
            //commandInvoker.ExecuteCommand();
            commandInvoker.AsyncExecuteCommand();
        }

        /// <summary>
        /// 通风
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVentilation_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxGroupNo.Text) || string.IsNullOrEmpty(cbxColumnNo.Text))
            {
                MessageBox.Show("请指定区号和列号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 通风命令
            AirFlowCommand cmd = new AirFlowCommand(Controller, Convert.ToInt32(cbxGroupNo.Text));
            commandInvoker.AddCommand(cmd);

            // 执行命令
            //commandInvoker.ExecuteCommand();
            commandInvoker.AsyncExecuteCommand();
        }

        /// <summary>
        /// 闭架
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxGroupNo.Text) || string.IsNullOrEmpty(cbxColumnNo.Text))
            {
                MessageBox.Show("请指定区号和列号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 闭架操作
            CloseCommand cmd = new CloseCommand(Controller, Convert.ToInt32(cbxGroupNo.Text));
            commandInvoker.AddCommand(cmd);

            // 执行命令
            //commandInvoker.ExecuteCommand();
            commandInvoker.AsyncExecuteCommand();
        }

        /// <summary>
        /// 停止运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxGroupNo.Text) || string.IsNullOrEmpty(cbxColumnNo.Text))
            {
                MessageBox.Show("请指定区号和列号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 停止操作
            StopCommand cmd = new StopCommand(Controller, Convert.ToInt32(cbxGroupNo.Text));
            commandInvoker.AddCommand(cmd);

            // 执行命令
            //commandInvoker.ExecuteCommand();
            commandInvoker.AsyncExecuteCommand();
        }

        /// <summary>
        /// 解锁操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUnLock_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxGroupNo.Text) || string.IsNullOrEmpty(cbxColumnNo.Text))
            {
                MessageBox.Show("请指定区号和列号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // 执行解锁操作
            UnLockCommand cmd = new UnLockCommand(Controller, Convert.ToInt32(cbxGroupNo.Text));
            commandInvoker.AddCommand(cmd);

            // 执行命令
            //commandInvoker.ExecuteCommand();
            commandInvoker.AsyncExecuteCommand();
        }

        // 界面关闭时，关闭相应未断开的连接
        private void ControlFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (tcpClient.Connected)
            {
                devMonitor.StopMonitor();
                // 关闭当前连接
                tcpClient.Close();
                
            }
        }

        /// <summary>
        /// 选中相应区号的密集架
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Rack rack = null;
            int groupNo = Convert.ToInt16(cbxGroupNo.Text);

            try
            {
                // 获取本区列信息
                rack = GetRackInfo(groupNo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //devMonitor.MonitorDevList.Clear();//清除之前的监控列表
            devMonitor.MonitorDevDic.Clear();

            if (tcpClient.Connected)
            {
                // 关闭当前连接
                tcpClient.Close();
            }

            // 当前区的IP地址和端口号
            IPAddress remoteIP = IPAddress.Parse(rack.DevIP);
            int remotePort = rack.DevPort;

            // 异步连接
            tcpClient.Connect(remoteIP, remotePort);
            // 堵塞等待3秒
            if (connectDone.WaitOne(3000, false) == false)
            {
                // 超时，连接服务器失败
                tcpClient.Close();

                string info = string.Format("无法连接{0}区服务器：IP地址{1}  端口号{2}", cbxGroupNo.Text, remoteIP.ToString(), remotePort);
                lsbInfoBoard.Items.Add(info);
                return;
            }

            // 读取环境信息
            UpdateEnvironmentInfo();
        }

        private void cbxColumnNo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}


