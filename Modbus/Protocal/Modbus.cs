namespace NJUST.AUTO06.Modbus.Protocal
{	
	/// <summary>
	///  定义MODBUS协议的功能码及其他参数
	/// </summary>
	public static class ModbusCommand
	{			
		// Modbus协议的功能码定义
        public const byte ReadCoils                  = 0x01; // 读线圈状态    
        public const byte ReadInputs                 = 0x02; // 读开关量输入状态  
        public const byte ReadHoldingRegisters       = 0x03; // 读保持寄存器的值  
		public const byte ReadInputRegisters         = 0x04; // 读取输入寄存器
        public const byte WriteSingleCoil            = 0x05; // 写单线圈状态
        public const byte WriteSingleRegister        = 0x06; // 写单寄存器
        public const byte WriteMultipleCoils         = 0x0F; // 写多线圈状态  
        public const byte WriteMultipleRegisters     = 0x10; // 写多寄存器

        public const byte RTUErrorCodeSize           = 0x05; // Modbus RTU协议的错误码帧长度
        public const byte TCPErrorCodeSize           = 0x09; // Modbus TCP协议的错误码帧长度

		// 错误帧功能码的偏移量（=常规功能码+0x80）
		public const byte ExceptionOffset = 0x80;

		// IO操作的缺省重传次数
		public const int DefaultRetries = 2;

		// 写操作缺省等待时间（ms）
		public const int DefaultWaitToRetryMilliseconds = 250;

		// 读操作缺省等待时间（ms）
		public const int DefaultTimeout = 500;

        // 线圈操作的1和0
		public const ushort CoilOn = 0xFF00;
		public const ushort CoilOff = 0x0000;
	
	}
}
