using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NJUST.AUTO06.Modbus.Protocal
{
    #region MODBUS RTU协议数据帧格式

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

    // 从机接收错误时反馈帧格式:
    //      从机地址       ---- 1个字节
    //      错误           ---- 1个字节    差错码：0x81；异常码：01-04
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 

    // 示例：
    //      主机要读取地址为1，开关量DO1和DO2的输出状态；从机相应线圈地址为0，DO寄存器（16bit）值为2（DO1=0, DO2=1）
    //   发送：01 01 00 00 00 02 BD CB
    //   反馈：01 01 01 02 D0 49


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

    // 从机接收错误时反馈帧格式:
    //      从机地址       ---- 1个字节
    //      错误           ---- 1个字节    差错码：0x82；异常码：01-04
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 

    // 示例：
    //      主机要读取地址为1，开关量DI1-DI4的输入状态；从机相应线圈地址为0，DI寄存器（16bit）值为0B（DI1/2/4=1, DI3=0）
    //   发送：01 02 00 00 00 04 79 C9
    //   反馈：01 02 01 0B E0 4F

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
    //      错误           ---- 1个字节    差错码：0x83；异常码：01-04
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 

    // 示例：
    //      主机要读取地址为1，起始地址为0116的从机3个寄存器数据；从机寄存器对应数据为，0116-1784；0117-1780；0118-178A；
    //   发送：01 03 01 16 00 03 E5 F3
    //   反馈：01 03 06 17 84 17 80 17 8A 58 47

    // 功 能 码: 0x05
    // 描    述：写单线圈（1路开关量输出）
    // 请求帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      输出BIT位      ---- 2个字节    高位在前，低位在后
    //      控制命令       ---- 2个字节    高位在前，低位在后（FF00为继电器合，0000为继电器分）
    //      CRC校验        ---- 2个字节    高位在前，低位在后

    // 反馈帧格式：与主机发送格式完全相同

    // 从机接收错误时反馈帧格式:
    //      从机地址       ---- 1个字节
    //      错误           ---- 1个字节    差错码：0x85；异常码：01-04
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 

    // 示例1：
    //      开关量输出点DO1，当前状态为“分”，要控制该路继电器为“合”
    //   发送：01 05 00 00 FF 00 8C 3A
    //   反馈：01 05 00 00 FF 00 8C 3A
    // 示例2：
    //      开关量输出点DO2，当前状态为“合”，要控制该路继电器为“分”
    //   发送：01 05 00 01 00 00 9C 0A
    //   反馈：01 05 00 01 00 00 9C 0A

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
    //      错误           ---- 1个字节    差错码：0x86；异常码：01-04
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 

    // 动作类型：写连续多个线圈
    // 功能码  ：0x0F
    // 请求帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      起始线圈地址   ---- 2个字节    高位在前，低位在后
    //      线圈数量       ---- 2个字节    高位在前，低位在后
    //      数据字节总数   ---- 1个字节    需要写的线圈位，转化为字节数；如要写8位线圈，数据则为1字节
    //      数据           ---- N*1个字节  高位在前，低位在后（N＝线圈数量/8，如果余数不等于 0，那么N = N+1）
    //      CRC校验        ---- 2个字节    高位在前，低位在后

    // 反馈帧格式：
    //      从机地址       ---- 1个字节
    //      功能码         ---- 1个字节
    //      线圈地址       ---- 2个字节    高位在前，低位在后
    //      线圈数量       ---- 2个字节    高位在前，低位在后
    //      CRC校验        ---- 2个字节    高位在前，低位在后

    // 从机接收错误时反馈帧格式:
    //      从机地址       ---- 1个字节
    //      错误           ---- 1个字节    差错码：0x8F；异常码：01-04
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 

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
    //      错误           ---- 1个字节    差错码：0x90；异常码：01-04
    //      信息码         ---- 1个字节
    //      CRC校验        ---- 2个字节    高位在前，低位在后 

    #endregion

    /// <summary>
    /// MODBUS协议实现的接口规范
    /// </summary>
    public interface IModbusProtocal
    {
        #region 构建不带CRC校验的MODBUS协议帧内容

        // 生成功能码0x06的基本协议内容
        List<byte> CreateWriteRegContent(byte no, UInt16 addr, int value);
        // 生成功能码0x10的基本协议内容
        List<byte> CreateWriteMulRegContent(byte no, UInt16 addr, int[] value, int dataLen);
        // 生成功能码0x05的基本协议内容
        List<byte> CreateWriteCoilContent(byte no, UInt16 addr, int value);
        // 生成功能码0x03的基本协议内容
        List<byte> CreateReadRegContent(byte no, UInt16 addr, int regCount);
        // 生成功能码0x01的基本协议内容
        List<byte> CreateReadCoilContent(byte no, UInt16 addr, int len);

        #endregion

        #region 构建完整的MODBUS协议帧

        // 根据读功能码生成完整的协议帧
        List<byte> GetModbusProtocalFrame(int cabNo, byte funcCode, UInt16 addr, int len);
        // 根据写功能码生成完整的协议帧
        List<byte> GetModbusProtocalFrame(int cabNo, byte funcCode, UInt16 addr, int num, int[] value);

        // 写功能码的反馈帧校验
        bool ValidateResponse(List<byte> wData, List<byte> fbData);
        // 读功能码的反馈帧校验
       // bool ValidateReadResponse(List<byte> rData, List<byte> fbData);

        #endregion

        // Modbus数据帧第一次读取的字节数
        int FrameHeaderSize { get;}

        int ResponseBytesToRead(byte[] frameStart);
    }

    /// <summary>
    /// 封装MODBUS协议的类
    /// </summary>
    public abstract class ModbusProtocal : IModbusProtocal
    {
        public virtual int FrameHeaderSize
        {
            get { return 0; }
        }

        #region MODBUS协议的基本实现

        /// <summary>
        /// 功能码06-写单寄存器的协议内容
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="value">寄存器值</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧</returns>
        public virtual List<byte> CreateWriteRegContent(byte no, UInt16 addr, int value)
        {
            List<byte> data = new List<byte>();

            // 配置待发送数据
            data.Add(no);
            data.Add(ModbusCommand.WriteSingleRegister);
            data.Add((byte)((addr >> 8) & 0xFF));      // 寄存器地址高位 
            data.Add((byte)(addr & 0xFF));             // 寄存器地址低位
            // 写单寄存器
            data.Add((byte)((value >> 8) & 0xFF));     // 写入值高位
            data.Add((byte)(value & 0xFF));            // 写入值低位

            return data;
        }

        /// <summary>
        /// 功能码10-写多寄存器的协议内容
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧</returns>
        public virtual List<byte> CreateWriteMulRegContent(byte no, UInt16 addr, int[] value, int dataLen)
        {
            List<byte> data = new List<byte>(); 

            // 配置待发送数据
            data.Add(no);
            data.Add(ModbusCommand.WriteMultipleRegisters);
            data.Add((byte)((addr >> 8) & 0xFF));      // 寄存器地址高位
            data.Add((byte)(addr & 0xFF));             // 寄存器地址低位
            // 写多寄存器
            data.Add((byte)((dataLen >> 8) & 0xFF));   // 写入寄存器数量高位
            data.Add((byte)(dataLen & 0xFF));          // 写入寄存器数量低位
            data.Add(Convert.ToByte(dataLen * 2));     // 写入字节数

            // 写入数据
            for (int i = 0; i < dataLen; i++)
            {
                data.Add((byte)((value[i] >> 8) & 0xFF)); // 数值高位
                data.Add((byte)(value[i] & 0xFF));        // 数值低位 
            }

            return data;
        }

        /// <summary>
        /// 功能码05-写单线圈的协议内容
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public virtual List<byte> CreateWriteCoilContent(byte no, UInt16 addr, int value)
        {
            List<byte> data = new List<byte>();

            // 配置待发送数据
            data.Add(no);
            data.Add(ModbusCommand.WriteSingleCoil);
            data.Add((byte)((addr >> 8) & 0xFF));     // 寄存器地址高位
            data.Add((byte)(addr & 0xFF));            // 寄存器地址低位
            // 写单线圈
            if (value == 1)
            {
                data.Add(0xFF); // 写入值高位
                data.Add(0x00); // 写入值低位 
            }
            else
            {
                data.Add(0x00); // 写入值高位
                data.Add(0x00); // 写入值低位 
            }

            return data;
        }

        /// <summary>
        /// 功能码0F-写多线圈的协议内容
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="addr">线圈数</param>
        /// <param name="value">写入值</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public virtual List<byte> CreateWriteMulCoilContent(byte no, UInt16 addr, int coilNum, int[] value)
        {
            List<byte> data = new List<byte>();
            int byteLen = 1 + (coilNum - 1) / 8;

            // 配置待发送数据
            data.Add(no);
            data.Add(ModbusCommand.WriteMultipleCoils);
            data.Add((byte)((addr >> 8) & 0xFF));            // 寄存器地址高位 
            data.Add((byte)(addr & 0xFF));                   // 寄存器地址低位
            // 写多线圈
            data.Add((byte)((coilNum >> 8) & 0xFF));         // 写入线圈数量高位
            data.Add((byte)(coilNum & 0xFF));                // 写入线圈数量低位 
            data.Add(Convert.ToByte(byteLen));               // 写入线圈值占用字节数

            for (int i = 0; i < data[6]; i++)
            {
                data.Add(Convert.ToByte(value[i] & 0xFF)); // 写入值-高位
            }

            return data;;
        }

        /// <summary>
        /// 功能码03-读寄存器的协议内容
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public virtual List<byte> CreateReadRegContent(byte no, UInt16 addr, int regCount)
        {
            List<byte> data = new List<byte>();

            // 配置待发送数据
            data.Add(no);
            data.Add(ModbusCommand.ReadHoldingRegisters);
            data.Add((byte)((addr & 0xFF00) >> 8));  //高起始地址
            data.Add((byte)(addr & 0x00FF));         //低起始地址
            data.Add((byte)((regCount >> 8) & 0xFF));//高字节在前
            data.Add((byte)(regCount & 0xFF));

            return data;
        }

        /// <summary>
        /// 功能码01-读线圈的协议内容
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">线圈地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public virtual List<byte> CreateReadCoilContent(byte no, UInt16 addr, int len)
        {
            List<byte> data = new List<byte>();

            // 配置待发送数据
            data.Add(no);
            data.Add(ModbusCommand.ReadCoils);
            data.Add((byte)((addr & 0xFF00) >> 8)); //高起始地址
            data.Add((byte)(addr & 0x00FF));        //低起始地址
            data.Add((byte)((len >> 8) & 0xFF));    //高字节在前
            data.Add((byte)(len & 0xFF));

            return data;
        }

        #endregion

        #region MODBUS协议内容，不带效验字

        /// <summary>
        /// 获取MODBUS读操作的协议内容
        /// </summary>
        /// <param name="cabNo">节点号</param>
        /// <param name="funcCode">地址</param>
        /// <param name="addr">寄存器/线圈地址</param>
        /// <param name="len">读取数据数量</param>
        /// <returns>协议帧</returns>
        public List<byte> GetModbusProtocalFrame(int cabNo, byte funcCode, UInt16 addr, int len)
        {
            List<byte> data = null;

            switch (funcCode)
            {
                case ModbusCommand.ReadCoils:            // 读线圈
                    data = CreateReadCoilContent((byte)cabNo, addr, len);
                    break;
                case ModbusCommand.ReadHoldingRegisters: // 读单存器
                    data = CreateReadRegContent((byte)cabNo, addr, len);
                    break;
                default:
                    break;
            }

            return data;
        }

        /// <summary>
        /// 获取MODBUS 的写操作的协议内容
        /// </summary>
        /// <param name="cabNo">节点号</param>
        /// <param name="funcCode">地址</param>
        /// <param name="addr">寄存器/线圈地址</param>
        /// <param name="num">寄存器/线圈数量</param>
        /// <param name="value">写入的数值</param>
        /// <returns>协议帧</returns>
        public List<byte> GetModbusProtocalFrame(int cabNo, byte funcCode, UInt16 addr, int num, int[] value)
        {
            List<byte> data = null;

            switch (funcCode)
            {
                case ModbusCommand.WriteSingleCoil:        // 写单线圈--0x05
                    data = CreateWriteCoilContent((byte)cabNo, addr, value[0]);
                    break;

                case ModbusCommand.WriteMultipleCoils:     // 写多线圈--0x0F
                    data = CreateWriteMulCoilContent((byte)cabNo, addr, num, value);
                    break;

                case ModbusCommand.WriteSingleRegister:    // 写单寄存器--0x06
                    data = CreateWriteRegContent((byte)cabNo, addr, value[0]);
                    break;

                case ModbusCommand.WriteMultipleRegisters: // 写多寄存器--0x10
                    data = CreateWriteMulRegContent((byte)cabNo, addr, value, num);
                    break;

                default:
                    break;
            }

            return data;
        }

        #endregion

        #region Modbus协议帧的帧校验，在继承的子类中实现

        // 写功能码的帧校验,留到具体协议帧类再实现
        public virtual bool ValidateResponse(List<byte> wData, List<byte> fbData)
        {
            return false;
        }

        // 读功能码的帧校验,留到具体协议帧类再实现
        //public virtual bool ValidateReadResponse(List<byte> rData, List<byte> fbData)
        //{
        //    return false;
        //}

        // 根据反馈帧的帧头确定本帧的长度，留到具体协议帧类再实现
        public virtual int ResponseBytesToRead(byte[] frameStart)
        {
            return 0;
        }

        #endregion
    }

    #region 基于MODBUS RTU的协议类

    /// <summary>
    /// MODBUS RTU 协议类
    /// </summary>
    public class ModbusRTUProtocal : ModbusProtocal
    {
        /// <summary>
        /// ModbusRTU的错误帧长度
        /// </summary>
        public override int FrameHeaderSize
        {
            get { return ModbusCommand.RTUErrorCodeSize; }
        }

        /// <summary>
        /// 16位CRC校验
        /// </summary>
        /// <param name="buf">存放校验的字节</param>
        /// <param name="len">参与校验的字节数</param>
        /// <returns>16位CRC校验值</returns>
        private uint CRC_16(byte[] buf, int len)
        {
            uint CRC = 0xFFFF;

            int i;
            uint j, tmp = 0;

            for (i = 0; i < len; i++)
            {
                CRC ^= buf[i];
                for (j = 0; j < 8; j++)
                {
                    tmp = CRC & 0x0001;
                    CRC = CRC >> 1;
                    if (tmp != 0)
                    {
                        CRC = (CRC ^ 0xA001);
                    }
                }
            }

            return ((CRC >> 8) + (CRC << 8));   // 低位在前
        }

        /// <summary>
        /// 为MODBUS RTU帧加入CRC校验
        /// </summary>
        /// <param name="data">MODBUS RTU的基本协议帧</param>
        private void Add_CRC(List<byte> data)
        {
            // MODBUS RTU协议，计算校验和
            int len = data.Count;
            uint CRC = CRC_16(data.ToArray(), len);

            data.Add((byte)((CRC >> 8) & 0xFF));   // CRC校验和高位
            data.Add((byte)(CRC & 0xFF));          // CRC校验和低位  
        }

        #region MODBUS RTU的协议帧

        /// <summary>
        /// 功能码06-写单寄存器的ModbusRTU协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public override List<byte> CreateWriteRegContent(byte no, UInt16 addr, int value)
        {
            // 获取MODBUS的功能码06的协议帧内容
            List<byte> data = base.CreateWriteRegContent(no, addr, value);

            // 加入帧效验
            Add_CRC(data);

            return data;
        }

        /// <summary>
        /// 组建功能码10-写多寄存器的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public override List<byte> CreateWriteMulRegContent(byte no, UInt16 addr, int[] value, int dataLen)
        {
            // 获取MODBUS的功能码10的协议帧内容
            List<byte> data = base.CreateWriteMulRegContent(no, addr, value, dataLen);

            // 加入帧效验
            Add_CRC(data);

            return data;
        }

        /// <summary>
        /// 组建功能码05-写单线圈的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧</returns>
        public override List<byte> CreateWriteCoilContent(byte no, UInt16 addr, int value)
        {
            // 获取MODBUS的功能码10的协议帧内容
            List<byte> data = base.CreateWriteCoilContent(no, addr, value);

            // 加入帧效验
            Add_CRC(data);

            return data;
        }

        /// <summary>
        /// 组建功能码0F-写多线圈的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧</returns>
        public override List<byte> CreateWriteMulCoilContent(byte no, UInt16 addr, int coilNum, int[] value)
        {
            // 获取MODBUS的功能码0F的协议帧内容
            List<byte> data = base.CreateWriteMulCoilContent(no, addr, coilNum, value);

            // 加入帧效验
            Add_CRC(data);

            return data;
        }

        /// <summary>
        /// 组建功能码03-读寄存器的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧</returns>
        public override List<byte> CreateReadRegContent(byte no, UInt16 addr, int regCount)
        {
            // 获取MODBUS的功能码03的协议帧内容
            List<byte> data = base.CreateReadRegContent(no, addr, regCount);

            // 加入帧效验
            Add_CRC(data);

            return data;
        }

        /// <summary>
        /// 组建功能码01-读线圈的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">线圈地址</param>
        /// <param name="coilNum">线圈数量</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧</returns>
        public override List<byte> CreateReadCoilContent(byte no, UInt16 addr, int coilNum)
        {
            // 获取MODBUS的功能码01的协议帧内容
            List<byte> data = base.CreateReadCoilContent(no, addr, coilNum);

            // 加入帧效验
            Add_CRC(data);

            return data;
        }

        #endregion

        #region MODBUS RTU帧的反馈校验

        /// <summary>
        /// 校验反馈帧
        /// </summary>
        /// <param name="wData">写指令的数据帧</param>
        /// <param name="fbData">反馈帧</param>
        /// <returns></returns>
        public override bool ValidateResponse(List<byte> wData, List<byte> fbData)
        {
            // 前6字节相符，则检验反馈帧的CRC
            uint CRC = CRC_16(fbData.ToArray(), fbData.Count-2);
            if ((fbData[fbData.Count-2] != (byte)((CRC >> 8) & 0xFF))    // CRC校验和高位
                || (fbData[fbData.Count-1] != (byte)(CRC&0xFF)))         // CRC校验和低位 
            {
                // 校验和不相符则为错误反馈
                return false;
            }

            return true;
        }

        #endregion

        /// <summary>
        /// 根据功能码计算ModbusUDP的帧长
        /// </summary>
        /// <param name="frameStart"></param>
        /// <returns></returns>
        public override int ResponseBytesToRead(byte[] frameStart)
        {
            // 功能码
            byte functionCode = frameStart[1];

            // 功能码大于0x80为错误帧，固定为5字节
            if (functionCode > ModbusCommand.ExceptionOffset)
                return -1;

            int numBytes = 0;

            switch (functionCode)
            {
                // 功能码01,02,03,04为读操作，反馈帧长度为：地址、功能码、字节长度、校验（2字节） + 数据字节
                case ModbusCommand.ReadCoils:
                case ModbusCommand.ReadInputs:
                case ModbusCommand.ReadHoldingRegisters:
                case ModbusCommand.ReadInputRegisters:
                    numBytes = frameStart[2] + 5;
                    break;

                // 功能码05,06,0F,10为写操作，反馈帧长度为8字节
                case ModbusCommand.WriteSingleCoil:
                case ModbusCommand.WriteSingleRegister:
                case ModbusCommand.WriteMultipleCoils:
                case ModbusCommand.WriteMultipleRegisters:
                    numBytes = 8;
                    break;

                default:
                    numBytes = -1;
                    break;
            }

            return numBytes;
        }
    }

    #endregion

    #region 基于MODBUS TCP的协议类

    /// <summary>
    /// MODBUS TCP协议类
    /// </summary>
    public class ModbusTCPProtocal : ModbusProtocal
    {
        /// <summary>
        /// ModbusRTU的错误帧长度
        /// </summary>
        public override int FrameHeaderSize
        {
            get { return ModbusCommand.TCPErrorCodeSize; }
        }

        // 拼接两个字节型数组
        private byte[] ByteCat(byte[] header, byte[] body)
        {
            int length = header.Length + body.Length;
            byte[] message = new byte[length];

            for (int i = 0; i < length; i++)
            {
                if (i < header.Length)
                {
                    message[i] = header[i];
                }
                else
                {
                    message[i] = body[i - header.Length];
                }
            }

            return message;
        }

        // 设置MODBUS TCP的报文头
        private byte[] CreateHeader(int identifier, int length)
        {
            // 为数组赋值
            byte[] header = new byte[6];

            header[0] = 00;               // 事务元标识符高位
            header[1] = (byte)identifier; // 事务元标识符低位
            header[2] = 0x00;
            header[3] = 0x00;
            header[4] = 0x00;             // 长度高位
            header[5] = (byte)length;     // 长度低位

            return header;
        }

        #region MODBUS TCP的协议帧

        /// <summary>
        /// 功能码06-写单寄存器的ModbusRTU协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public override List<byte> CreateWriteRegContent(byte no, UInt16 addr, int value)
        {
            // 获取MODBUS的基本帧
            List<byte> body = base.CreateWriteRegContent(no, addr, value);

            // 生成TCP报文头
            byte[] header = CreateHeader(1, 6);

            // 生成MODBUS TCP协议帧 = TCP报文头+基本帧
            byte[] buf = ByteCat(header, body.ToArray());

            List<byte> data = new List<byte>();

            for (int i = 0; i < 6 + body.Count; i++)
            {
                data.Add(buf[i]);
            }

            return data;
        }

        /// <summary>
        /// 组建功能码10-写多寄存器的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public override List<byte> CreateWriteMulRegContent(byte no, UInt16 addr, int[] value, int dataLen)
        {
            // 获取MODBUS的基本帧
            List<byte> body = base.CreateWriteMulRegContent(no, addr, value, dataLen);

            // 生成TCP报文头
            byte[] header = CreateHeader(1, 6);

            // 生成MODBUS TCP协议帧 = TCP报文头+基本帧
            byte[] buf = ByteCat(header, body.ToArray());

            List<byte> data = new List<byte>();

            for (int i = 0; i < 6 + body.Count; i++)
            {
                data.Add(buf[i]);
            }

            return data;
        }

        /// <summary>
        /// 组建功能码05-写单线圈的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public override List<byte> CreateWriteCoilContent(byte no, UInt16 addr, int value)
        {
            // 获取MODBUS的基本帧
            List<byte> body = base.CreateWriteCoilContent(no, addr, value);

            // 生成TCP报文头
            byte[] header = CreateHeader(1, 6);

            // 生成MODBUS TCP协议帧 = TCP报文头+基本帧
            byte[] buf = ByteCat(header, body.ToArray());

            List<byte> data = new List<byte>();

            for (int i = 0; i < 6 + body.Count; i++)
            {
                data.Add(buf[i]);
            }

            return data;
        }

        /// <summary>
        /// 组建功能码0F-写多线圈的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public override List<byte> CreateWriteMulCoilContent(byte no, UInt16 addr, int coilNum, int[] value)
        {
            // 获取MODBUS的基本帧
            List<byte> body = base.CreateWriteMulCoilContent(no, addr, coilNum, value);

            // 生成TCP报文头
            byte[] header = CreateHeader(1, 6);

            // 生成MODBUS TCP协议帧 = TCP报文头+基本帧
            byte[] buf = ByteCat(header, body.ToArray());

            List<byte> data = new List<byte>();

            for (int i = 0; i < 6 + body.Count; i++)
            {
                data.Add(buf[i]);
            }

            return data;
        }

        /// <summary>
        /// 组建功能码03-读寄存器的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">寄存器地址</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public override List<byte> CreateReadRegContent(byte no, UInt16 addr, int regCount)
        {
            // 获取MODBUS的基本帧
            List<byte> body = base.CreateReadRegContent(no, addr, regCount);

            // 生成TCP报文头
            byte[] header = CreateHeader(1, 6);

            // 生成MODBUS TCP协议帧 = TCP报文头+基本帧
            byte[] buf = ByteCat(header, body.ToArray());

            List<byte> data = new List<byte>();

            for (int i = 0; i < 6 + body.Count; i++)
            {
                data.Add(buf[i]);
            }

            return data;
        }

        /// <summary>
        /// 组建功能码01-读线圈的协议帧
        /// </summary>
        /// <param name="no">节点号</param>
        /// <param name="addr">线圈地址</param>
        /// <param name="coilNum">线圈数量</param>
        /// <param name="data">协议帧字节数组</param>
        /// <returns>协议帧长度</returns>
        public override List<byte> CreateReadCoilContent(byte no, UInt16 addr, int coilNum)
        {
            // 获取MODBUS的基本帧
            List<byte> body = base.CreateReadCoilContent(no, addr, coilNum);

            // 生成TCP报文头
            byte[] header = CreateHeader(1, 6);

            // 生成MODBUS TCP协议帧 = TCP报文头+基本帧
            byte[] buf = ByteCat(header, body.ToArray());

            List<byte> data = new List<byte>();

            for (int i = 0; i < 6 + body.Count; i++)
            {
                data.Add(buf[i]);
            }

            return data;
        }

        #endregion

        #region MODBUS TCP的帧校验

        /// <summary>
        /// 校验反馈帧
        /// </summary>
        /// <param name="wData">写指令的数据帧</param>
        /// <param name="fbData">反馈帧</param>
        /// <returns>校验是否正确</returns>
        public override bool ValidateResponse(List<byte> rData, List<byte> fbData)
        {
            return true;
        }

        #endregion

        /// <summary>
        /// 根据功能码计算ModbusTCP的帧长
        /// </summary>
        /// <param name="frameStart"></param>
        /// <returns></returns>
        public override int ResponseBytesToRead(byte[] frameStart)
        {
            // 功能码
            byte functionCode = frameStart[7];

            // 功能码大于0x80为错误帧，固定为9字节
            if (functionCode > ModbusCommand.ExceptionOffset)
                return -1; 

            int numBytes = 0;

            switch (functionCode)
            {
                // 功能码01,02,03,04为读操作，反馈帧长度为：地址、功能码、字节长度、校验（2字节） + 数据字节
                case ModbusCommand.ReadCoils:
                case ModbusCommand.ReadInputs:
                case ModbusCommand.ReadHoldingRegisters:
                case ModbusCommand.ReadInputRegisters:
                    numBytes = frameStart[8] + 9;
                    break;

                // 功能码05,06,0F,10为写操作，反馈帧长度为12字节
                case ModbusCommand.WriteSingleCoil:
                case ModbusCommand.WriteSingleRegister:
                case ModbusCommand.WriteMultipleCoils:
                case ModbusCommand.WriteMultipleRegisters:
                    numBytes = 12;
                    break;

                default:
                    numBytes = -1;
                    break;
            }

            return numBytes;
        }
    }

    #endregion
}
