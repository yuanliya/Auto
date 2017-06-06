using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

using NJUST.AUTO06.Utility;

using NJUST.AUTO06.Modbus.IO;
using NJUST.AUTO06.Modbus.Protocal;

using AutoCabinet2017.Helper;
using AutoCabinet2017.Controller;

using ZY.EntityFrameWork.Core.Model.Dto;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;

namespace AutoCabinet2017.UI.DV
{
    public partial class FormDVControlPanel : Form
    {
        /// <summary>
        /// 设备控制模型
        /// </summary>
        public class DeviceModel
        {
            public int  DeviceNo { get; set; }           // 设备编号
            public int TotalLayers { get; set; }         // 总层数
            public int  CurLayerNo { get; set; }         // 当前层
            public int  DstLayerNo { get; set; }         // 目的层
            public bool DoorOperateFlag { get; set; }    // 开门OR关门
            public bool IsGoLayer { get; set; }          // 走层操作
            public bool IsDoorOperate { get; set; }      // 门操作  
            public Image IsConnected { get; set; }       // 连接状态
        }

        // 通信设备
        private SerialPort     spComm         = null;
        // 控制器对象
        private HZKController  Controller     = null;
        // 命令处理类
        private CommandInvoker commandInvoker = null;
        // 监控类
        //private Monitor        deviceMonitor  = null;

        // 保存系统管理的所有回转库信息
        private List<DeviceDto> listCabinetInfo = null;

        public FormDVControlPanel()
        {
            InitializeComponent();
            // 为BandGridView增加CheckBox列
            //new GridCheckMarksSelection(bgDeviceInfo);
        }

        /// <summary>
        /// 更新设备状态
        /// </summary>
        /// <param name="model"></param>
        private void UpdateDeviceStatus(ref DeviceModel model)
        {
            int curLayerNo = 1, curStat = 0;

            // 尝试连接查询设备状态
            try
            {
                // 查询设备状态
                Controller.Query(model.DeviceNo, ref curLayerNo, ref curStat);

                model.CurLayerNo  = curLayerNo;
                model.IsConnected = imgConnStatus.Images[0];
            }
            catch (Exception ex)
            {
                string msg = string.Format("{0}号设备通信错误：{1}", model.DeviceNo, ex.Message);
                // 在信息栏显示错误信息
                UpdateClipBoardInfo(msg);
            }
        }

        /// <summary>
        /// 初始化设备信息面板
        /// </summary>
        private void InitGridView()
        {
            // 构建表格对应设备模型
            BindingList<DeviceModel> lstDeviceInfo = new BindingList<DeviceModel>();
         
            // 查询设备信息
            listCabinetInfo = new List<DeviceDto>(CallerFactory.Instance.GetService<ISystemConfigService>().GetAllDevices());

            // 构建表格对应的设备模型数据源
            foreach (DeviceDto item in listCabinetInfo)
            {
                // 设备模型
                DeviceModel model = new DeviceModel
                {
                    DeviceNo    = item.CabinetNo,
                    TotalLayers = item.CabinetLayers,
                    CurLayerNo  = 1,
                    IsDoorOperate = false,
                    IsGoLayer     = false,
                    DoorOperateFlag = false,
                    DstLayerNo      = 1,
                    IsConnected     = imgConnStatus.Images[1]
                };

                // 查询设备状态
                UpdateDeviceStatus(ref model);

                // 加入设备模型
                lstDeviceInfo.Add(model);
            }

            // 表格关联数据源
            gcDevicePanel.DataSource = lstDeviceInfo;
        }

        private void FormDVControlPanel_Load(object sender, EventArgs e)
        {
            try
            {
                // 初始化控制设备
                if (Controller == null)
                {
                    // 从配置文件中读取设备配置信息并创建对象
                    spComm = CommonHelper.Instance.CreateSerialPortObject("主控串口");
                    // 打开串口
                    spComm.Open();
                    // 设置读超时
                    spComm.ReadTimeout = ModbusCommand.DefaultTimeout;

                    // 创建设备控制类:采用串口通信和Modbus RTU协议
                    Controller = new HZKController(new SerialPortAdapter(spComm), ModbusFactory.Instance.GetProtocalProxy());
                }

                // 初始化命令处理类
                commandInvoker = new CommandInvoker();
                // 订阅命令处理的事件
                commandInvoker.OnCommandError   += UpdateCommandErrorInfo;           // 命令执行错误的事件
                commandInvoker.OnCommandExecute += UpdateCommandExecuteInfo;         // 命令执行正确的事件

                // 初始化监控类
                //deviceMonitor = new Monitor(Controller);                             
                //deviceMonitor.OnDeviceMonitorMessage += UpdateClipBoardInfo;         // 更新信息栏的事件
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
                return;
            }

            // 初始化表格
            InitGridView();
            // 信息栏取消焦点
            memoClipboard.Select(0, -1);
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
                memoClipboard.MaskBox.AppendText(info + "\r\n");

                return;
            }

            if (args.Exception is TimeoutException)
            {
                // 与节点通信发生超时错误
                info = string.Format("{0}号设备通信超时无响应！", args.DevNo);
                memoClipboard.MaskBox.AppendText(info + "\r\n");

                return;
            }

            // 操作未完成：通信错误、节点无响应、
            info = string.Format("{0}号设备通信收到错误回帧！", args.DevNo);
            memoClipboard.MaskBox.AppendText(info + "\r\n");
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
            memoClipboard.MaskBox.AppendText(info + "\r\n");
        }

        /// <summary>
        /// 更新信息栏事件
        /// </summary>
        /// <param name="info"></param>
        public void UpdateClipBoardInfo(string info)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Monitor.DeviceMonitorMessageEventHandler(UpdateClipBoardInfo), new object[] { info });
            }
            else
            {
                memoClipboard.MaskBox.AppendText(info + "\r\n");
            }
        }

        #endregion

        /// <summary>
        /// 根据表格的行信息执行控制
        /// </summary>
        /// <param name="model"></param>
        private void GetControlCommand(DeviceModel model, ref CommandInvoker cmdInvoker)
        {
            if (model == null) return;

            if (model.IsDoorOperate)
            {
                if (model.DoorOperateFlag == true)
                {
                    // 开门操作
                    OpenDoorCommand cmd = new OpenDoorCommand(Controller, model.DeviceNo);
                    cmdInvoker.AddCommand(cmd);
                }
                else
                {
                    // 关门操作
                    CloseDoorCommand cmd = new CloseDoorCommand(Controller, model.DeviceNo);
                    cmdInvoker.AddCommand(cmd);
                }
            }

            if (model.IsGoLayer)
            {
                // 走层操作
                RunCommand cmd = new RunCommand(Controller, model.DeviceNo);
                cmd.dstLayers = new int[1] { model.DstLayerNo };
                cmdInvoker.AddCommand(cmd);
            }
        }

        #region 控制面板

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // 获得选中的行
            int[] selectedIdx = gvDeviceInfo.GetSelectedRows();

            foreach (int item in selectedIdx) 
            {
                // 将选中行的信息转换为设备模型对象
                DeviceModel model = (DeviceModel)gvDeviceInfo.GetRow(item);

                // 查询设备状态
                UpdateDeviceStatus(ref model);
            }
        }

        /// <summary>
        /// 全部开门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenDoor_Click(object sender, EventArgs e)
        {
            // 获得选中的行
            int[] selectedIdx = gvDeviceInfo.GetSelectedRows();

            foreach (int item in selectedIdx) 
            {
                // 将选中行的信息转换为设备模型对象
                DeviceModel model = (DeviceModel)gvDeviceInfo.GetRow(item);

                // 生成开门命令
                OpenDoorCommand cmd = new OpenDoorCommand(Controller, model.DeviceNo);
                commandInvoker.AddCommand(cmd);
            }

            // 执行命令队列里的所有命令
            commandInvoker.ExecuteCommand();
        }

        /// <summary>
        /// 全部关门
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCloseDoor_Click(object sender, EventArgs e)
        {
            // 获得选中的行
            int[] selectedIdx = gvDeviceInfo.GetSelectedRows();

            foreach (int item in selectedIdx) 
            {
                // 将选中行的信息转换为设备模型对象
                DeviceModel model = (DeviceModel)gvDeviceInfo.GetRow(item);

                // 生成关门命令
                CloseDoorCommand cmd = new CloseDoorCommand(Controller, model.DeviceNo);
                commandInvoker.AddCommand(cmd);
            }

            // 执行命令队列里的所有命令
            commandInvoker.ExecuteCommand();
        }

        /// <summary>
        /// 运行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRun_Click(object sender, EventArgs e)
        {
            // 获得选中的行
            int[] selectedIdx = gvDeviceInfo.GetSelectedRows();

            foreach (int item in selectedIdx) 
            {
                // 将选中行的信息转换为设备模型对象
                DeviceModel model = (DeviceModel)gvDeviceInfo.GetRow(item);

                // 根据行的设备模型信息提取控制命令
                GetControlCommand(model, ref commandInvoker);
            }

            // 执行命令队列里的所有命令
            commandInvoker.ExecuteCommand();
        }

        /// <summary>
        /// 全部停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            // 获得选中的行
            int[] selectedIdx = gvDeviceInfo.GetSelectedRows();

            foreach (int item in selectedIdx) 
            {
                // 将选中行的信息转换为设备模型对象
                DeviceModel model = (DeviceModel)gvDeviceInfo.GetRow(item);

                // 生成停止命令
                StopCommand cmd = new StopCommand(Controller, model.DeviceNo);
                commandInvoker.AddCommand(cmd);
            }

            // 执行命令队列里的所有命令
            commandInvoker.ExecuteCommand();
        }

        #endregion

        #region 表格嵌入的控制按钮

        /// <summary>
        /// 表格嵌入的“执行”按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExecute_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            // 将选中行的信息转换为设备模型对象
            DeviceModel model = (DeviceModel)gvDeviceInfo.GetFocusedRow();

            // 根据行的设备模型信息提取控制命令
            GetControlCommand(model, ref commandInvoker);

            // 执行命令
            commandInvoker.ExecuteCommand();
        }

        /// <summary>
        /// 表格嵌入的“刷新”按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateStatus_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            // 将选中行的信息转换为设备模型对象
            DeviceModel model = (DeviceModel)gvDeviceInfo.GetFocusedRow();

            // 查询设备状态
            UpdateDeviceStatus(ref model);
        }

        #endregion

        /// <summary>
        /// 根据设备信息更新下拉框的总层数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgDeviceInfo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            // 读取本行设备总层数Cell的值
            int totalLayers = (int)gvDeviceInfo.GetFocusedRowCellValue("TotalLayers");

            // 更新下拉框
            cbxDstLayerNo.Items.Clear();
            for(int i = 0; i < totalLayers; i++)
            {
                cbxDstLayerNo.Items.Add(i+1);
            }
        }
    }
}
