using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoCabinet2017.Controller
{
    public class Monitor : IDisposable
    {
        public class DeviceToMonitor
        {
            public int devNo { get; set; }       // 设备编号
            public int errorCount { get; set; }  // 错误计数
            public int noRunCount { get; set; }  // 非运行状态计数

            public DeviceToMonitor(int devNo)
            {
                this.devNo = devNo;
            }
        }

        // 系统状态反馈
        private const int STATUS_MMOTOR_RUNNING     = 0x01;      // 主电机运行
        private const int STATUS_DMOTOR_RUNNING     = 0x02;      // 门电机运行
        private const int STATUS_MMOTOR_DOWNRUNNING = 0x4000;    // 主电机下行
        private const int STATUS_MMOTOR_UPRUNNING   = 0x8000;    // 主电机上行

        // 控制器对象
        private HZKController Controller = null;
        // 监控线程
        private Thread mThread = null;

        // 监控设备列表
        private List<DeviceToMonitor> monitorDevList = new List<DeviceToMonitor>();

        // 发布设备运行状态信息的事件
        public delegate void DeviceMonitorMessageEventHandler(string info);
        public event DeviceMonitorMessageEventHandler OnDeviceMonitorMessage;

        // 发布设备运行当前层的事件
        public delegate void UpdateDeviceLayerInfoEventHandler(int curLayer);
        public event UpdateDeviceLayerInfoEventHandler OnDeviceLayerInfo;

        public Monitor(HZKController hzkController)
        {
            // 设备控制类
            this.Controller = hzkController;
        }

        public void AddDeviceToMonitor(int devNo)
        {
            if (devNo < 1) return;

            monitorDevList.Add(new DeviceToMonitor(devNo) {});
        }

        #region 设备监控中的事件

        /// <summary>
        /// 系统运行时的事件处理
        /// </summary>
        /// <param name="curlayer"></param>
        /// <param name="stat"></param>
        public void RaiseOnDeviceMonitorMessage(string info) 
        {
            if (OnDeviceMonitorMessage != null)
            {
                OnDeviceMonitorMessage(info);
            }
        }

        /// <summary>
        /// 设备当前层信息
        /// </summary>
        /// <param name="curLayer"></param>
        public void RaiseOnDeviceLayerInfo(int curLayer)
        {
            if (OnDeviceLayerInfo != null)
            {
                OnDeviceLayerInfo(curLayer);
            }
        }

        #endregion

        #region 设备监控操作

        /// <summary>
        /// 停止监控线程
        /// </summary>
        public void StopMonitor()
        {
            if (mThread != null)
            {
                mThread.Abort();
            }
        }

        /// <summary>
        /// 启动监控
        /// </summary>
        public void StartMonitor()
        {
            if (mThread == null)
            {
                mThread = new Thread(new ParameterizedThreadStart(MonitorWorkingThread));

                mThread.IsBackground = true;
                mThread.Start(this.Controller);
            }
        }

        /// <summary>
        /// 设备监控线程
        /// </summary>
        /// <param name="obj"></param>
        public void MonitorWorkingThread(object obj)
        {
            int idx = 0;
            int curlayer = 0, curstat = 0;

            HZKController controller = obj as HZKController; 

            while(true)
            {
                // 轮询监控列表里的各个设备
                try
                {
                    // 查询当前状态
                    controller.Query(monitorDevList[idx].devNo, ref curlayer, ref curstat);

                    // 节点错误计数清0
                    monitorDevList[idx].errorCount = 0;

                    // 当前设备门电机和主电机都没有动作
                    if ((curstat & 0x03) == 0x00)
                    {
                        monitorDevList[idx].noRunCount++;
                        return;
                    }
                    else
                    {
                        // 系统非运行状态清0
                        monitorDevList[idx].noRunCount = 0;
                    }

                    // 显示设备柜门状态信息
                    if ((curstat & STATUS_DMOTOR_RUNNING) == STATUS_DMOTOR_RUNNING)     // 门电机运行
                    {
                        RaiseOnDeviceMonitorMessage(string.Format("提示：{0}号回转库开关门运行中!", monitorDevList[idx].devNo));
                    }

                    // 显示设备走层状态信息
                    if ((curstat & STATUS_MMOTOR_RUNNING) == STATUS_MMOTOR_RUNNING)     // 主电机运行
                    {
                        RaiseOnDeviceMonitorMessage(string.Format("提示：{0}号回转库走层运行中，当前层为第{1}层!", monitorDevList[idx].devNo, curlayer));
                        RaiseOnDeviceLayerInfo(curlayer);
                    }                       
                }
                catch (TimeoutException)
                {
                    // 与节点通信发生超时错误
                    RaiseOnDeviceMonitorMessage(string.Format("{0}号设备通信超时无响应！", monitorDevList[idx].devNo));
                    monitorDevList[idx].errorCount++;
                }
                catch (Exception)
                {
                    // 与节点通信发生无法收到正确回帧错误
                    RaiseOnDeviceMonitorMessage(string.Format("{0}号设备通信收到错误回帧！", monitorDevList[idx].devNo));
                    monitorDevList[idx].errorCount++;
                }
                finally
                {
                    // 本设备移除出监控队列时，检索号不能自加；
                    if (monitorDevList[idx].errorCount == 3)
                    {
                        // 与节点通信发生无法收到正确回帧错误
                        RaiseOnDeviceMonitorMessage(string.Format("{0}号设备通信错误，停止监控！", monitorDevList[idx].devNo));
                        // 从监控队列中移除设备
                        monitorDevList.Remove(monitorDevList[idx]);
                    }
                    else if (monitorDevList[idx].noRunCount == 3)
                    {
                        // 设备停止运行
                        RaiseOnDeviceMonitorMessage(string.Format("{0}号设备不在运行，停止监控！", monitorDevList[idx].devNo));
                        // 从监控队列中移除设备
                        monitorDevList.Remove(monitorDevList[idx]);
                    }
                    else
                    {
                        // 本设备没有移除操作时，检索号才能自加，查询下一个设备
                        idx++;
                    }

                    // 回到第一个设备进行监控
                    if(idx >= monitorDevList.Count)
                    {
                        idx = 0;
                    }
                }

                // 休眠400ms
                Thread.Sleep(400);
            }
        }

        #endregion

        /// <summary>
        /// 对象被销毁时调用
        /// </summary>
        public void Dispose()
        {
            if (mThread.IsAlive)
            {
                StopMonitor();
            }
        }
    }
}
