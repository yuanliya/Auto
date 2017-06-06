using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.IO.Ports;

using NJUST.AUTO06.Modbus.Device;
using NJUST.AUTO06.Modbus.IO;
using NJUST.AUTO06.Modbus.Protocal;

namespace AutoCabinet2017.Controller
{
    /***************************************************************
     * 类 名：回转库控制设备类
     * 功 能：回转库操作相关函数
     * 作 者：邹卫军
     * 时 间：2013.12.13
     * -------------------------------------------------------------
     * 修 改：2015.1.18
     * 说 明：修改类的架构，分离协议和设备控制
     ***************************************************************/

    /// <summary>
    /// 由用户程序引发，用于派生自定义的异常类型
    /// 控制设备前进行设备状态校验引发的异常
    /// </summary>
    public class ControlCheckException : ApplicationException  
    {
        /// <summary>  
        /// 默认构造函数  
        /// </summary>  
        public ControlCheckException() { }
        public ControlCheckException(string message) : base(message) { }
    }  

    public class HZKController : ModbusDevice
    {
        // 回转库的状态
        public const int STATUS_MMOTORRUNING   = 0x01;    // 主电机运行
        public const int STATUS_DMOTORRUNING   = 0x02;    // 门电机运行
        public const int STATUS_ISPROTECTED    = 0x04;    // 系统保护
        public const int STATUS_LAYERSIGNAL    = 0x08;    // 层信号
        public const int STATUS_DOOROPENED     = 0x10;    // 门打开
        public const int STATUS_DOORCLOSED     = 0x20;    // 门关闭
        public const int STATUS_LOGIN          = 0x40;    // 登录信号

        //--------永宏PLC的地址设定------------------------//
        // 控制寄存器地址
        private const UInt16 CONTROL_REG_ADDR = 0x07D0;    // M0-M15,起始10进制地址02000
        // 控制寄存器对应动作
        private const UInt16 WORKMODE   = 0 << 0;   // bit0--工作模式 1--手动 0--自动；上位机采用自动模式
        private const UInt16 OPEN_DOOR  = 1 << 1;   // bit1--开门
        private const UInt16 CLOSE_DOOR = 1 << 2;   // bit2--关门
        private const UInt16 EXCUTE     = 1 << 3;   // bit3--执行
        private const UInt16 STOP       = 1 << 4;   // bit4--停止
        private const UInt16 GO_UP      = 1 << 5;   // bit5--上行
        private const UInt16 GO_DOWN    = 1 << 6;   // bit6--下行
        private const UInt16 PANKU      = 1 << 7;   // bit7--盘库

        private const UInt16 CURLAYER_REG_ADDR = 0x06; // R6--存储当前层地址 
        private const UInt16 STATUS_REG_ADR    = 0x07; // R7--系统状态 
                                                       // bit0--主电机运行; 
                                                       // bit1--门电机运行; 
                                                       // bit2--保护信号; 
                                                       // bit3--层信号; 
                                                       // bit4--开门限位;   
                                                       // bit5--关门限位;
                                                       // bit6--登录状态
                                                       // bit14--主电机下行
                                                       // bit15--主电机上行
        private const UInt16 DSTLAYER_REG_ADDR = 0x0A; // R10--存储目标层地址
        private const UInt16 UICHANGE_REG_ADDR = 0x1773; // D3--UI界面切换
        
        // 定义Modbus协议帧和反馈帧的事件（一般由应用界面订阅用于显示帧内容）
        public delegate void ModbusFrameShowEventHandle(List<byte> inData, List<byte> fbData);
        public event ModbusFrameShowEventHandle ModbusFrameShow;

        /// <summary>
        /// 构造函数
        /// </summary>
        public HZKController(IStreamResource commProxy, IModbusProtocal protocalProxy)
            : base(commProxy, protocalProxy)
        {
        }

        /// <summary>
        /// 按键的点动操作（按1松0）--控制器要求
        /// </summary>
        /// <param name="cabNo">设备节点号</param>
        /// <param name="cmd">控制字</param>
        private void Key_Press(int cabNo, int[] cmd)
        {
            List<byte> inData = new List<byte>();  // 发送帧
            List<byte> fbData = new List<byte>();  // 反馈帧

            // 向设备发送协议帧
            WriteCommand((byte)cabNo, ModbusCommand.WriteMultipleCoils, CONTROL_REG_ADDR, cmd, inData, fbData);

            // 显示Modbus帧信息
            if (ModbusFrameShow != null)
            {
                ModbusFrameShow(inData, fbData);
            }
        }

        /// <summary>
        /// 设定目的层号
        /// </summary>
        /// <param name="cabNo">节点号</param>
        /// <param name="dstLayerNo">目的层号</param>
        private void SetDstLayer(int cabNo, int[] dstLayers)
        {
            List<byte> inData = new List<byte>();  // 发送帧
            List<byte> fbData = new List<byte>();  // 反馈帧

            if(dstLayers.Length == 1)
            {
                // 向设备发送协议帧-一个目的层
                WriteCommand(cabNo, ModbusCommand.WriteSingleRegister, DSTLAYER_REG_ADDR, dstLayers, inData, fbData);
            }
            else
            {
                // 切换触摸屏到多目标层界面
                SetUI(cabNo, 12);

                // 向设备发送协议帧-多个目的层
                WriteCommand(cabNo, ModbusCommand.WriteMultipleRegisters, DSTLAYER_REG_ADDR, dstLayers, inData, fbData);
            }
            
            // 显示Modbus帧信息
            if (ModbusFrameShow != null)
            {
                ModbusFrameShow(inData, fbData);
            } 
        }

        /// <summary>
        /// 切换触摸屏的UI界面
        /// </summary>
        /// <param name="cabNo"></param>
        /// <param name="uiNo"></param>
        private void SetUI(int cabNo, int uiNo)
        {
            List<byte> inData = new List<byte>();  // 发送帧
            List<byte> fbData = new List<byte>();  // 反馈帧

            int[] data = new int[1] { uiNo};
            // 向设备发送协议帧              
            WriteCommand(cabNo, ModbusCommand.WriteSingleRegister, UICHANGE_REG_ADDR, data, inData, fbData);
     
            // 显示Modbus帧信息
            if (ModbusFrameShow != null)
            {
                ModbusFrameShow(inData, fbData);
            }
        }

        /// <summary>
        /// 检测回转库状态:
        /// bit0--主电机运行; bit1--门电机运行; bit2--保护信号; bit3--层信号; bit4--开门限位; bit5--关门限位; bit6--登录状态;
        /// bit14--主电机下行; bit15--主电机上行; 
        /// </summary>
        /// <param name="cabNo">节点号</param>
        /// <param name="curLayer">当前层</param>
        /// <param name="sysStat">当前状态</param>
        public void Query(int cabNo, ref int curLayer, ref int sysStat)
        {
            List<byte> inData = new List<byte>();  // 发送帧
            List<byte> fbData = new List<byte>();  // 反馈帧

            // 读指定寄存器内容
            ReadCommand(cabNo, ModbusCommand.ReadHoldingRegisters, CURLAYER_REG_ADDR, 2, inData, fbData);

            // 当前层
            curLayer = fbData[4];
            // 当前状态
            sysStat = fbData[5] << 8 | fbData[6];

            // 显示Modbus帧信息
            if (ModbusFrameShow != null)
            {
                ModbusFrameShow(inData, fbData);
            }  
        }

        #region 用于UI界面的回转库控制接口

        /// <summary>
        /// 执行开门操作
        /// </summary>
        /// <param name="cabNo">节点号</param>
        public void OpenDoor_Execute(int cabNo, int sysStat)
        {
            // 如果系统触摸屏处于未登录状态，不执行命令
            if ((sysStat & STATUS_LOGIN) != STATUS_LOGIN)
            {
                throw new ControlCheckException("系统没有登录!");
            }

            // 如果系统处于保护状态，不执行命令
            if ((sysStat & STATUS_ISPROTECTED) == STATUS_ISPROTECTED)
            {
                throw new ControlCheckException("系统处于保护状态!");
            }

            // 如果门已打开，不执行命令
            if ((sysStat & STATUS_DOOROPENED) == STATUS_DOOROPENED)
            {
                throw new ControlCheckException("柜门当前处于打开状态!");
            }

            // 如果门电机正在运行，不执行命令
            if ((sysStat & STATUS_DMOTORRUNING) == STATUS_DMOTORRUNING)
            {
                throw new ControlCheckException("门电机正在运行!");
            }

            // 执行开门操作
            OpenDoor(cabNo);
        }

        /// <summary>
        /// 执行关门操作
        /// </summary>
        /// <param name="cabNo">节点号</param>
        public void CloseDoor_Execute(int cabNo, int sysStat)
        {
            // 如果系统触摸屏处于未登录状态，不执行命令
            if ((sysStat & STATUS_LOGIN) != STATUS_LOGIN)
            {
                throw new ControlCheckException("系统没有登录!");
            }

            // 如果系统处于保护状态，不执行命令
            if ((sysStat & STATUS_ISPROTECTED) == STATUS_ISPROTECTED)
            {
                throw new ControlCheckException("系统处于保护状态!");
            }

            // 如果门已关闭，不执行命令
            if ((sysStat & STATUS_DOORCLOSED) == STATUS_DOOROPENED)
            {
                throw new ControlCheckException("柜门当前处于关闭状态!");
            }

            // 如果门电机正在运行，不执行命令
            if ((sysStat & STATUS_DMOTORRUNING) == STATUS_DMOTORRUNING)
            {
                throw new ControlCheckException("门电机正在运行!");
            }

            // 执行关门操作
            CloseDoor(cabNo);   
        }

        /// <summary>
        /// 执行停止操作
        /// </summary>
        /// <param name="cabNo">节点号</param>
        public void Stop_Execute(int cabNo)
        {
            // 执行停止操作
            Stop(cabNo);
        }

        /// <summary>
        /// 执行走层命令
        /// </summary>
        /// <param name="cabNo">节点号</param>
        /// <param name="dstLayerNo">目的层</param>
        public void Run_Execute(int cabNo, int[] dstLayers, int curLayer, int sysStat)
        {
            // 如果系统触摸屏处于未登录状态，不执行命令
            if ((sysStat & STATUS_LOGIN) != STATUS_LOGIN)
            {
                throw new ControlCheckException("系统没有登录!");
            }

            // 如果系统处于保护状态，不执行命令
            if ((sysStat & STATUS_ISPROTECTED) == STATUS_ISPROTECTED)
            {
                throw new ControlCheckException("系统处于保护状态!");
            }

            // 如果当前层即为设定层，不执行命令
            if ((dstLayers.Length == 1) && (curLayer == dstLayers[0]))
            {
                throw new ControlCheckException("系统当前层即为设定层!");
            }

            // 如果主电机或门电机正在运行，不执行命令
            if (((sysStat & STATUS_MMOTORRUNING) == STATUS_MMOTORRUNING)
                || ((sysStat & STATUS_DMOTORRUNING) == STATUS_DMOTORRUNING))
            {
                throw new ControlCheckException("系统正在运行!");
            }

            // 执行走层指令
            Run(cabNo, dstLayers);
        }

        #endregion

        #region 回转库典型控制动作

        /// <summary>
        /// 开门(点动，按1松0)
        /// </summary>
        /// <param name="cabNo">节点号</param>
        private void OpenDoor(int cabNo)
        {
            int[] value = new int[1];

            // 开门操作-按下置1
            value[0] = WORKMODE | OPEN_DOOR;
            Key_Press(cabNo, value);

            Thread.Sleep(100);

            // 开门操作-抬起置0
            value[0] = WORKMODE;
            Key_Press(cabNo, value);
        }

        /// <summary>
        /// 关门(点动，按1松0)
        /// </summary>
        /// <param name="cabNo">节点号</param>
        private void CloseDoor(int cabNo)
        {
            int[] value = new int[1];

            // 关门操作-按下置1
            value[0] = WORKMODE | CLOSE_DOOR;
            Key_Press(cabNo, value);

            Thread.Sleep(100);

            // 关门操作-抬起置0
            value[0] = WORKMODE;
            Key_Press(cabNo, value);
        }

        /// <summary>
        /// 停止(点动，按1松0)
        /// </summary>
        /// <param name="cabNo">节点号</param>
        private void Stop(int cabNo)
        {
            int[] value = new int[1];

            // 停止操作-按下置1
            value[0] = WORKMODE | STOP;
            Key_Press(cabNo, value);

            Thread.Sleep(100);

            // 停止操作-抬起置0
            value[0] = WORKMODE;
            Key_Press(cabNo, value);
        }

        /// <summary>
        /// 运行(点动，按1松0)
        /// </summary>
        /// <param name="cabNo">节点号</param>
        /// <param name="dstLayerNo">目的层</param>
        private void Run(int cabNo, int[] dstLayers)
        {
            int[] value = new int[1];

            // 设置设定层
            SetDstLayer(cabNo, dstLayers);
           
            // 执行操作-按下置1
            value[0] = WORKMODE | EXCUTE;
            Key_Press(cabNo, value);

            Thread.Sleep(100);

            // 执行操作-抬起置0
            value[0] = WORKMODE;
            Key_Press(cabNo, value);
        }

        /// <summary>
        /// 盘库(点动，按1松0)
        /// </summary>
        /// <param name="cabNo">节点号</param>
        /// <param name="dstLayerNo">目的层</param>
        private void InventoryBase(int cabNo, int[] dstLayers)
        {
            // 设置目的层
            SetDstLayer(cabNo, dstLayers);

            int[] value = new int[1];

            // 停止操作-按下置1
            value[0] = WORKMODE | PANKU | EXCUTE;
            Key_Press(cabNo, value);

            Thread.Sleep(100);

            // 停止操作-抬起置0
            value[0] = WORKMODE | PANKU;
            Key_Press(cabNo, value);
        }

        #endregion
    }
}
