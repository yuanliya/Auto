using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCabinet2017.Controller
{
    public class SignalController
    {
        // 控制485总线数据收发的同步事件,初始化为有信号状态
        private AutoResetEvent commDone = new AutoResetEvent(true);

        private SignalController()
        {}

        public static readonly SignalController Instance = new SignalController();

        /// <summary>
        /// 等候有效的信号量
        /// </summary>
        /// <param name="millisecondsTimeout">等待时间，默认500ms</param>
        public void WaitForSignal(int millisecondsTimeout = 500)
        {
            if (commDone.WaitOne(millisecondsTimeout) == false)
            {
                throw new Exception("无法获取485总线通信信号量！");
            }
        }

        /// <summary>
        /// 释放信号量
        /// </summary>
        public void ReleaseSignal()
        {
            commDone.Set();
        }
    }
}
