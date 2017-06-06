using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

using NJUST.AUTO06.Modbus.Device;
using NJUST.AUTO06.Modbus.IO;
using NJUST.AUTO06.Modbus.Protocal;

namespace AutoCabinet2017.Device
{
    /*-------------------MODBUS RTU协议----------------------------------------------------
    // 功 能 码: 01
    // 描    述：读1路或多路开关量输出状态
    // 数据类型：位
    // 请求帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      线圈地址       ---- 2个字节    高位在前，低位在后（起始的位地址）
    //      数据长度       ---- 2个字节    高位在前，低位在后（读多少位数据）
    //      CRC校验        ---- 2个字节    高位在前，低位在后
    
    // 反馈帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      数据长度       ---- 1个字节    （1个字节，8Bit）
    //      状态值         ---- 1个字节   
    //      CRC校验        ---- 2个字节    高位在前，低位在后
    
    // 示例：
    //      主机要读取地址为1，开关量DO1和DO2的输出状态；从机相应线圈地址为0，DO寄存器（16bit）值为2（DO1=0, DO2=1）
    //   发送：01 01 00 00 00 02 BD CB
    //   反馈：01 01 01 02 D0 49
    --------------------------------------------------------------------------------------*/

    /*--------------------------------------------------------------------------
    // 功 能 码: 02
    // 描    述：读1路或多路开关量状态输入
    // 数据类型：位
    // 请求帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      线圈地址       ---- 2个字节    高位在前，低位在后（起始的位地址）
    //      数据长度       ---- 2个字节    高位在前，低位在后（读多少位数据）
    //      CRC校验        ---- 2个字节    高位在前，低位在后
    
    // 反馈帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      数据长度       ---- 1个字节    （1个字节，8Bit）
    //      状态值         ---- 1个字节   
    //      CRC校验        ---- 2个字节    高位在前，低位在后
    
    // 示例：
    //      主机要读取地址为1，开关量DI1-DI4的输入状态；从机相应线圈地址为0，DI寄存器（16bit）值为0B（DI1/2/4=1, DI3=0）
    //   发送：01 02 00 00 00 04 79 C9
    //   反馈：01 02 01 0B E0 4F
    --------------------------------------------------------------------------------------*/

    /*------------------------------------------------------------------------------------ 
    // 功 能 码：0x03
    // 描    述：读多路寄存器输入
    // 数据类型：整形、字符型、状态字、浮点型
    // 请求帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      寄存器起始地址 ---- 2个字节    高位在前，低位在后
    //      寄存器数量     ---- 2个字节    高位在前，低位在后
    //      CRC校验        ---- 2个字节    高位在前，低位在后

    // 反馈帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      读字节数       ---- 1个字节
    //      数据           ---- N*2个字节  高位在前，低位在后 N表示需要读的寄存器个数
    //      CRC校验        ---- 2个字节    高位在前，低位在后
    
    // 从机接收错误时反馈帧格式:
    //      从机地址       ---- 1个字节
    //      0x83           ---- 1个字节
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 
    
    // 示例：
    //      主机要读取地址为1，起始地址为0116的从机3个寄存器数据；从机寄存器对应数据为，0116-1784；0117-1780；0118-178A；
    //   发送：01 03 01 16 00 03 E5 F3
    //   反馈：01 03 06 17 84 17 80 17 8A 58 47
    -----------------------------------------------------------------------------------*/

    /*--------------------------------------------------------------------------
    // 功 能 码: 0x05
    // 描    述：写单线圈（1路开关量输出）
    // 请求帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      输出BIT位      ---- 2个字节    高位在前，低位在后
    //      控制命令       ---- 2个字节    高位在前，低位在后（FF00为继电器合，0000为继电器分）
    //      CRC校验        ---- 2个字节    高位在前，低位在后
    
    // 反馈帧格式：与主机发送格式完全相同
     
    // 示例1：
    //      开关量输出点DO1，当前状态为“分”，要控制该路继电器为“合”
    //   发送：01 05 00 00 FF 00 8C 3A
    //   反馈：01 05 00 00 FF 00 8C 3A
    // 示例2：
    //      开关量输出点DO2，当前状态为“合”，要控制该路继电器为“分”
    //   发送：01 05 00 01 00 00 9C 0A
    //   反馈：01 05 00 01 00 00 9C 0A
    --------------------------------------------------------------------------------------*/

    /*-------------------------------------------------------------------------------------
    // 功 能 码：0x06
    // 描    述：写单个寄存器
    // 请求帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      寄存器地址     ---- 2个字节    高位在前，低位在后
    //      寄存器值       ---- 2个字节    高位在前，低位在后
    //      CRC校验        ---- 2个字节    高位在前，低位在后
    
    // 反馈帧格式：与主机发送格式完全相同
     
    // 示例：
    //      主机要写数据07D0到地址为1，寄存器地址为002C的从机寄存器中；
    //   发送：01 06 00 2C 07 D0 4B AF
    //   反馈：01 06 00 2C 07 D0 4B AF
    
    // 从机接收错误时反馈帧格式:
    //      从机地址       ---- 1个字节
    //      0x83           ---- 1个字节
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 
    --------------------------------------------------------------------------------------*/

    /*-------------------------------------------------------------------------------------
    // 动作类型：写连续多个寄存器
    // 功能码  ：0x10
    // 请求帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      起始寄存器地址 ---- 2个字节    高位在前，低位在后
    //      寄存器数量     ---- 2个字节    高位在前，低位在后
    //      数据字节总数   ---- 1个字节
    //      数据           ---- N*2个字节  高位在前，低位在后 N表示需要写的寄存器个数
    //      CRC校验        ---- 2个字节    高位在前，低位在后
    
    // 反馈帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      寄存器地址     ---- 2个字节    高位在前，低位在后
    //      寄存器数量     ---- 2个字节    高位在前，低位在后
    //      CRC校验        ---- 2个字节    高位在前，低位在后
    
    // 从机接收错误时反馈帧格式:
    //      从机地址       ---- 1个字节
    //      0x90           ---- 1个字节
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 
    --------------------------------------------------------------------------------------*/

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

    /***************************************************************
     * 类 名：档案柜控制设备类
     * 功 能：档案柜操作相关函数
     * 作 者：邹卫军
     * 时 间：2013.12.13
     ***************************************************************/
    public class MJJController : ModbusDevice
    {
        //--------永宏PLC的地址设定------------------------//
        // 控制寄存器地址
        public const UInt16 CONTROL_REG_ADDR = 2200;  // M200起，起始地址02000
        // 控制寄存器对应动作
        public const UInt16 CLOSE    = 1 << 0;   // bit0--关闭
        public const UInt16 STOP     = 1 << 1;   // bit1--停止
        public const UInt16 UNLOCK   = 1 << 2;   // bit2--解锁
        public const UInt16 MANUMODE = 1 << 3;   // bit3=1--手动工作模式
        public const UInt16 AUTOMODE = 0 << 3;   // bit3=0--自动工作模式
        public const UInt16 AIRFLOW  = 1 << 4;   // bit4--通风
        public const UInt16 OPEN     = 1 << 5;   // bit5--打开

        //public const UInt16 Wind_Coil_ADDR  = 2147; // 通风信号
        //public const UInt16 LOGIN_Coil_ADDR = 2149; // 登陆信号

        // 列移动状态寄存器地址（左移、右移、停止）
        //public const UInt16 COLUMNRUN_REG_ADDR  = 0x0911 - 1;  // (M320起--,起始10进制地址02321)
        // 通道操作状态寄存器地址(通道打开操作是否完成)
        //public const UInt16 CHANNELOPE_REG_ADDR = 0x09D1 - 1;  // (M512起--,起始10进制地址02513)
        // 移动列传感器状态寄存器起始地址
        // 移动列1-M21~M23, 移动列2-M37~M39......以此类推，偏移量为16
        //public const UInt16 SENSOR_REG_ADDR     = 0x07E6 - 1;  // (M21起--,起始10进制地址02021)

        // 系统状态寄存器地址（是否保护、是否运行）
        public const UInt16 SYSSTAT_REG_ADDR = 2148;     // M148--是否保护,M149--是否登录

        public const UInt16 RUNSTAT_REG_ADDR = 2206;//M206 是否运行

        // 存储打开通道地址
        public const UInt16 DSTLAYER_REG_ADDR   = 10;        // R10   
        // 存储温度信息地址
        public const UInt16 TEMPRATURE_REG_ADDR = 11;        // R11 
        // 存储湿度信息地址
        public const UInt16 HUMIDITY_REG_ADDR   = 12;        // R12

        private const int DELAY_TIME = 200;                  // 指令间延时200ms

        // 定义Modbus协议帧和反馈帧的事件（一般由应用界面订阅用于显示帧内容）
        public delegate void ModbusFrameShowEventHandle(List<byte> inData, List<byte> fbData);
        public event ModbusFrameShowEventHandle ModbusFrameShow;
 
        public MJJController(IStreamResource commProxy, IModbusProtocal protocalProxy)
            : base(commProxy, protocalProxy)
        {
        }

        /// <summary>
        /// 按键的点动操作（按1松0）--控制器要求
        /// </summary>
        /// <param name="cabNo">设备节点号</param>
        /// <param name="cmd">控制字</param>
        /// <param name="len">数据长度</param>
        private void Key_Press(int cabNo, int[] cmd, int len)
        {
            List<byte> inData = new List<byte>();  // 发送帧
            List<byte> fbData = new List<byte>();  // 反馈帧

            // 向设备发送协议帧
            WriteCommand((byte)cabNo, ModbusCommand.WriteMultipleCoils, CONTROL_REG_ADDR, cmd, len, inData, fbData);
            //AsyncWriteCommand((byte)cabNo, ModbusCommand.WriteMultipleCoils, CONTROL_REG_ADDR, cmd, len, inData, fbData);

            // 显示Modbus帧信息
            if (ModbusFrameShow != null)
            {
                ModbusFrameShow(inData, fbData);
            }
        }

        /***************************************************************
         * 函数名： SetDstLayer（）
         * 功  能： 设定档案柜目的层号
         * 输  入：
         *          int  cabNo：     档案柜节点号
         *          int dstLayerNo ：目的层编号
         * 输  出： true--操作成功 false--从机没有正确回应 
         * 作  者： 邹卫军
         * 时  间： 2013.12.13
         * ----------------------------------------------------------------
         * 修  改：2016.11.3
         * 说  明：重新定义封装，继承ModbusDevice类
         ***************************************************************/
        private void SetDstChannel(int cabNo, int dstLayerNo)
        {
            List<byte> inData = new List<byte>();  // 发送帧
            List<byte> fbData = new List<byte>();  // 反馈帧

            int[] value = new int[32];

            // 目的层写入保持寄存器
            value[0] = dstLayerNo;

            WriteCommand(cabNo, ModbusCommand.WriteSingleRegister, DSTLAYER_REG_ADDR, value, 1, inData, fbData);
            //AsyncWriteCommand(cabNo, ModbusCommand.WriteSingleRegister, DSTLAYER_REG_ADDR, value, 1, inData, fbData);
        }

        /***************************************************************
         * 函数名： Open（）
         * 功  能： 开通道(点动，按1松0)
         * 输  入：
         *          int  cabNo：档案柜节点号
         * 输  出： 无
         * 作  者： 邹卫军
         * 时  间： 2013.12.13
         * 说  明：执行Open（）前应先执行SetDstChannel(),设置通道号
         * ----------------------------------------------------------------
         * 修  改：2016.11.3
         * 说  明：重新定义封装，继承ModbusDevice类
         ***************************************************************/
        private void Open(int cabNo)
        {
            int[] value = new int[1];

            // 设置自动模式+打开
            value[0] = AUTOMODE | OPEN;
            Key_Press(cabNo, value, 6);

            //Thread.Sleep(500);

            // 打开复位
            value[0] = AUTOMODE;
            Key_Press(cabNo, value, 6);
        }

        /***************************************************************
        * 函数名： Close（）
        * 功  能： 密集架关闭(点动，按1松0)
        * 输  入：
        *          int  cabNo：档案柜节点号
        * 输  出： 无
        * 作  者： 邹卫军
        * 时  间： 2011.12.1
        * ----------------------------------------------------------------
        * 修  改：2016.11.3
        * 说  明：重新定义封装，继承ModbusDevice类
        ***************************************************************/
        private void Close(int cabNo)
        {
            int[] value = new int[1];

            // 设置自动模式+关闭
            value[0] = AUTOMODE | CLOSE;
            Key_Press(cabNo, value, 6);

            // 关闭复位
            value[0] = AUTOMODE;
            Key_Press(cabNo, value, 6);
        }

        /***************************************************************
        * 函数名： Stop（）
        * 功  能： 停止(点动，按1松0)
        * 输  入：
        *          int  cabNo：档案柜节点号
        * 输  出： 无
        * 作  者： 邹卫军
        * 时  间： 2011.12.1
        * ----------------------------------------------------------------
        * 修  改：2016.11.3
        * 说  明：重新定义封装，继承ModbusDevice类
        ***************************************************************/
        private void Stop(int cabNo)
        {
            int[] value = new int[1];

            // 设置自动模式+停止
            value[0] = AUTOMODE | STOP;
            Key_Press(cabNo, value, 6);

            // 停止复位
            value[0] = AUTOMODE;
            Key_Press(cabNo, value, 6);
        }

        /***************************************************************
       * 函数名： UnLock（）
       * 功  能： 解锁(点动，按1松0)
       * 输  入：
       *          int  cabNo：档案柜节点号
       * 输  出： 无
       * 作  者： 邹卫军
       * 时  间： 2011.12.1
       * ----------------------------------------------------------------
       * 修  改：2016.11.3
       * 说  明：重新定义封装，继承ModbusDevice类
       ***************************************************************/
        private void UnLock(int cabNo)
        {
            int[] value = new int[1];

            // 设置自动模式+解锁
            value[0] = AUTOMODE | UNLOCK;
            Key_Press(cabNo, value, 6);

            // 解锁复位
            value[0] = AUTOMODE;
            Key_Press(cabNo, value, 6);
        }

        /***************************************************************
        * 函数名： Lock（）
        * 功  能： 锁定密集架
        * 输  入：
        *          int  cabNo：档案柜节点号
        * 输  出： 无
        * 作  者： 邹卫军
        * 时  间： 2014.9.16
        * ----------------------------------------------------------------
        * 修  改：2016.11.3
        * 说  明：重新定义封装，继承ModbusDevice类
       ***************************************************************/
        //private void Lock(int cabNo)
        //{
        //    int[] value = new int[1];

        //    // 设置保护
        //    value[0] = 1;
        //    Key_Press(cabNo, value, 6);
        //}

        /***************************************************************
       * 函数名： AirFlow（）
       * 功  能： 通风(点动，按1松0)
       * 输  入：
       *          int  cabNo：档案柜节点号
       * 输  出： 无
       * 作  者： 邹卫军
       * 时  间： 2011.12.1
       ***************************************************************/
        private void AirFlow(int cabNo)
        {
            int[] value = new int[1];

            // 设置自动模式+通风
            value[0] = AUTOMODE | AIRFLOW;
            Key_Press(cabNo, value, 6);

            // 通风复位
            value[0] = AUTOMODE;
            Key_Press(cabNo, value, 6);
        }

        /***************************************************************
        * 函数名： GetColumnRunStat（）
        * 功  能： 检测移动列运行状态
        * 输  入：
        *          int cabNo   : 节点号
        *          int colCount: 移动列数
        * 输  出： 包含列运动信息的字节LIST，其中， 
        *         bit0--M1列左移; bit1--M1列右移; bit2--M2列左移; bit3--M2列右移;......
        * 作  者： 邹卫军
        * 时  间： 2014.8.11
        ***************************************************************/
        //public List<byte> GetColumnRunStat(int cabNo, int colCount)
        //{
        //    List<byte> inData = new List<byte>();  // 发送帧
        //    List<byte> fbData = new List<byte>();  // 反馈帧

        //    List<byte> colRunStat = new List<byte>(); 

        //    // 读存储系统状态信息的寄存器值
        //    ReadCommand(cabNo, ModbusCommand.ReadCoils, COLUMNRUN_REG_ADDR, colCount * 2, inData, fbData);
            
        //    // 显示Modbus帧信息
        //    if (ModbusFrameShow != null)
        //    {
        //        ModbusFrameShow(inData, fbData);
        //    }  

        //    // 提取反馈的数据内容
        //    for(int i = 0; i < colCount * 2; i++)
        //    {
        //        colRunStat.Add(fbData[3+i]);
        //    }

        //    return colRunStat;
        //}

        /***************************************************************
        * 函数名： GetSensorStat（）
        * 功  能： 检测移动列传感器状态
        * 输  入：
        *          int cabNo：     节点号
        *          int colNo：     移动列编号 
        * 输  出： 包含传感器信息的List表  null--查询失败
        * 作  者： 邹卫军
        * 时  间： 2014.8.29
        ***************************************************************/
        //public byte GetSensorStat(int cabNo, int colNo)
        //{
        //    List<byte> inData = new List<byte>();  // 发送帧
        //    List<byte> fbData = new List<byte>();  // 反馈帧

        //    // 计算移动列传感器寄存器地址
        //    UInt16 colRegAddr = Convert.ToUInt16((colNo - 1) * 16 + SENSOR_REG_ADDR);

        //    // 读存储系统状态信息的寄存器值
        //    ReadCommand(cabNo, ModbusCommand.ReadCoils, colRegAddr, 3, inData, fbData);

        //    // 显示Modbus帧信息
        //    if (ModbusFrameShow != null)
        //    {
        //        ModbusFrameShow(inData, fbData);
        //    }  

        //    // bit0--左开闭检测  bit1--右开闭检测  bit2--尾门检测
        //    return fbData[3];
        //}

        /***************************************************************
        * 函数名： ChannelOpeStat（）
        * 功  能： 检测通道操作是否完成的状态
        * 输  入：
        *          int cabNo：     节点号
        *          int chanCount： 通道数 
        * 输  出： 包含通道信息的List表  null--查询失败
        * 作  者： 邹卫军
        * 时  间： 2014.8.11
        ***************************************************************/
        //public byte GetChannelOpeStat(int cabNo, int chanCount)
        //{
        //    List<byte> inData = new List<byte>();  // 发送帧
        //    List<byte> fbData = new List<byte>();  // 反馈帧

        //    // 读存储通道状态信息的寄存器值
        //    ReadCommand(cabNo, ModbusCommand.ReadCoils, (ushort)(CHANNELOPE_REG_ADDR + (chanCount - 1)), 1, inData, fbData);

        //    // 显示Modbus帧信息
        //    if (ModbusFrameShow != null)
        //    {
        //        ModbusFrameShow(inData, fbData);
        //    }

        //    return fbData[3];
        //}

        /***************************************************************
         * 函数名： GetCurTemperHum（）
         * 功  能： 读取当前温湿度
         * 输  入：
         *          int cabNo：     节点号
         * 输  出： 包含温湿度信息的List表  null--查询失败
         * 作  者： 邹卫军
         * 时  间： 2014.8.29
         ***************************************************************/
        public List<byte> GetCurTemperHum(int cabNo)
        {
            List<byte> inData = new List<byte>();  // 发送帧
            List<byte> fbData = new List<byte>();  // 反馈帧

            // 读存储温湿度信息的寄存器值
            ReadCommand(cabNo, ModbusCommand.ReadHoldingRegisters, TEMPRATURE_REG_ADDR, 2, inData, fbData);

            // 显示Modbus帧信息
            if (ModbusFrameShow != null)
            {
                ModbusFrameShow(inData, fbData);
            }

            List<byte> envirStat = new List<byte>();
            envirStat.Add(fbData[9]);
            envirStat.Add(fbData[10]);
            envirStat.Add(fbData[11]);
            envirStat.Add(fbData[12]);

            return envirStat;
        }

        /***************************************************************
        * 函数名： GetSystemStat（）
        * 功  能： 检测系统当前状态
        * 输  入：
        *          int cabNo：     节点号
        * 输  出： 状态信息  0xFF--查询失败
        * 作  者： 邹卫军
        * 时  间： 2014.8.29
        ***************************************************************/
        public byte GetSystemStat(int cabNo)
        {
            List<byte> inData = new List<byte>();  // 发送帧
            List<byte> fbData = new List<byte>();  // 反馈帧

            // 读存储通道状态信息的寄存器值
            ReadCommand(cabNo, ModbusCommand.ReadCoils, SYSSTAT_REG_ADDR, 2, inData, fbData);

            // 显示Modbus帧信息
            if (ModbusFrameShow != null)
            {
                ModbusFrameShow(inData, fbData);
            }

            // bit0--是否保护,bit1--是否登录
            //return fbData[3];
            return fbData[9];
        }

        public byte GetRunStat(int cabNo)
        {
            List<byte> inData = new List<byte>();  // 发送帧
            List<byte> fbData = new List<byte>();  // 反馈帧

            // 读存储通道状态信息的寄存器值
            ReadCommand(cabNo, ModbusCommand.ReadCoils, RUNSTAT_REG_ADDR, 1, inData, fbData);//读取系统运行状态
            // 显示Modbus帧信息
            if (ModbusFrameShow != null)
            {
                ModbusFrameShow(inData, fbData);
            }
            // bit0--是否保护,bit1--是否登录
            return fbData[9];
        }

        #region 用于UI界面的控制接口

        /// <summary>
        /// 执行开架操作
        /// </summary>
        /// <param name="devNo">区号</param>
        /// <param name="chanNo">通道号</param>
        public void Open_Execute(int devNo, int chanNo)
        {
            byte sysStat = GetSystemStat(devNo);

            if((sysStat & 0x01) == 0x01)
            {
                throw new ControlCheckException("系统处于保护状态!");
            }

            if ((sysStat & 0x02) == 0x00)
            {
                throw new ControlCheckException("系统主控屏没有登录!");
            }

            byte runStat = GetRunStat(devNo);
            if ((runStat & 0x01) == 0x01)
            {
                throw new ControlCheckException("系统正在运行!");
            }

            // 设置通道号
            SetDstChannel(devNo, chanNo);
            // 开架操作
            Open(devNo);
        }

        /// <summary>
        /// 执行闭架操作
        /// </summary>
        /// <param name="devNo">区号</param>
        public void Close_Execute(int devNo)
        {
            byte sysStat = GetSystemStat(devNo);

            if ((sysStat & 0x01) == 0x01)
            {
                throw new ControlCheckException("系统处于保护状态!");
            }

            if ((sysStat & 0x02) == 0x00)
            {
                throw new ControlCheckException("系统主控屏没有登录!");
            }

            byte runStat = GetRunStat(devNo);
            if ((runStat & 0x01) == 0x01)
            {
                throw new ControlCheckException("系统正在运行!");
            }

            // 闭架操作
            Close(devNo);
        }

        /// <summary>
        /// 执行停止操作
        /// </summary>
        /// <param name="devNo">区号</param>
        public void Stop_Execute(int devNo)
        {
            byte sysStat = GetSystemStat(devNo);

            if ((sysStat & 0x01) == 0x01)
            {
                throw new ControlCheckException("系统处于保护状态!");
            }

            if ((sysStat & 0x02) == 0x00)
            {
                throw new ControlCheckException("系统主控屏没有登录!");
            }

            // 停止操作
            Stop(devNo);
        }

        /// <summary>
        /// 执行锁定操作
        /// </summary>
        /// <param name="devNo">区号</param>
        //public void Lock_Execute(int devNo)
        //{
        //    byte sysStat = GetSystemStat(devNo);

        //    if ((sysStat & 0x01) == 0x01)
        //    {
        //        throw new ControlCheckException("系统处于保护状态!");
        //    }

        //    if ((sysStat & 0x02) == 0x02)
        //    {
        //        throw new ControlCheckException("系统主控屏没有登录!");
        //    }

        //    // 锁定操作
        //    Lock(devNo);
        //}

        /// <summary>
        /// 执行解锁操作
        /// </summary>
        /// <param name="devNo">区号</param>
        public void UnLock_Execute(int devNo)
        {
            byte sysStat = GetSystemStat(devNo);

            if ((sysStat & 0x01) == 0x00)
            {
                throw new ControlCheckException("系统未处于保护状态!");
            }

            if ((sysStat & 0x02) == 0x00)
            {
                throw new ControlCheckException("系统主控屏没有登录!");
            }

            // 解锁操作
            UnLock(devNo);
        }

        /// <summary>
        /// 执行通风操作
        /// </summary>
        /// <param name="devNo">区号</param>
        public void AirFlow_Execute(int devNo)
        {
            byte sysStat = GetSystemStat(devNo);

            if ((sysStat & 0x01) == 0x01)
            {
                throw new ControlCheckException("系统处于保护状态!");
            }

            if ((sysStat & 0x02) == 0x00)
            {
                throw new ControlCheckException("系统主控屏没有登录!");
            }

            byte runStat = GetRunStat(devNo);
            if ((runStat & 0x01) == 0x01)
            {
                throw new ControlCheckException("系统正在运行!");
            }

            // 通风操作
            AirFlow(devNo);
        }

        #endregion
    }
}
