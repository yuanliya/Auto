using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace AutoCabinet2017.Controller
{
    /// <summary>
    /// 基于命令模式的设备控制模式设计
    /// 声明Command接口
    /// </summary>
    public interface ICommand
    {
        // 设备编号
        int DevNo { get; }

        // 命令类型
        string CommandType { get; }

        // 执行命令
        void Execute();
    }

    #region 具体命令类
    
    /// <summary>
    /// 具体命令类:开门命令
    /// </summary>
    public class OpenDoorCommand : ICommand
    {
        private HZKController controller;
        
        private int    devNo = 1;
        private string commandType = "开门"; 

        public int DevNo
        {
            get { return devNo; }
        }

        public string CommandType 
        {
            get { return commandType; } 
        }

        public OpenDoorCommand(HZKController hzkController, int devNo)
        {
            controller = hzkController;
            this.devNo = devNo;
        }

        /// <summary>
        /// 命令执行
        /// </summary>
        public void Execute()
        {
            int curLayerNo = 0, curStat = 0;

            // 查询设备状态
            controller.Query(devNo, ref curLayerNo, ref curStat);
            // 执行开门操作
            controller.OpenDoor_Execute(devNo, curStat);
        }
    }

    /// <summary>
    /// 具体命令类:关门命令
    /// </summary>
    public class CloseDoorCommand : ICommand
    {
        private HZKController controller;

        private int devNo = 1;
        private string commandType = "关门";

        public int DevNo
        {
            get { return devNo; }
        }

        public string CommandType
        {
            get { return commandType; }
        }

        public CloseDoorCommand(HZKController hzkController, int devNo)
        {
            controller = hzkController;
            this.devNo = devNo;
        }

        /// <summary>
        /// 命令执行
        /// </summary>
        public void Execute()
        {
            int curLayerNo = 0, curStat = 0;

            // 查询设备状态
            controller.Query(devNo, ref curLayerNo, ref curStat);
            // 执行关门操作
            controller.CloseDoor_Execute(devNo, curStat);
        }
    }

    /// <summary>
    /// 具体命令类:停止命令
    /// </summary>
    public class StopCommand : ICommand
    {
        private HZKController controller;

        private int    devNo = 1;
        private string commandType = "停止";

        public int DevNo
        {
            get { return devNo; }
        }

        public string CommandType
        {
            get { return commandType; }
        }

        public StopCommand(HZKController hzkController, int devNo)
        {
            controller = hzkController;
            this.devNo = devNo;
        }

        /// <summary>
        /// 命令执行
        /// </summary>
        public void Execute()
        {
            controller.Stop_Execute(devNo);
        }
    }

    /// <summary>
    /// 具体命令类:走层命令
    /// </summary>
    public class RunCommand : ICommand
    {
        private HZKController controller;

        private int    devNo = 1;
        private string commandType = "走层运行";

        public int DevNo
        {
            get { return devNo; }
        }

        public string CommandType
        {
            get { return commandType; }
        }

        // 设定层集合
        public int[] dstLayers { get; set; }

        public RunCommand(HZKController hzkController, int devNo)
        {
            controller = hzkController;
            this.devNo = devNo;
        }

        /// <summary>
        /// 命令执行
        /// </summary>
        public void Execute()
        {
            int curLayerNo = 0, curStat = 0;

            // 查询设备状态
            controller.Query(devNo, ref curLayerNo, ref curStat);
            // 执行命令
            controller.Run_Execute(devNo, dstLayers, curLayerNo, curStat);
        }
    }

    #endregion

    ///// <summary>
    ///// 命令模式--命令执行者
    ///// </summary>
    //public class CommandInvoker
    //{
    //    // 发布命令执行错误的事件
    //    public event EventHandler<CommandExceptionEventArgs> OnCommandError;
    //    // 发布命令执行正常的事件
    //    public event EventHandler<CommandExecuteEventArgs> OnCommandExecute;

    //    // 监控类
    //    private DeviceMonitor devMonitor;
    //    // 命令集合
    //    //private List<ICommand> commandList = new List<ICommand>();
    //    private Queue<ICommand> commandQE = new Queue<ICommand>();

    //    /// <summary>
    //    /// 构造函数
    //    /// </summary>
    //    /// <param name="devMonitor"></param>
    //    public CommandInvoker(DeviceMonitor devMonitor)
    //    {
    //        this.devMonitor = devMonitor;
    //    }

    //    /// <summary>
    //    /// 添加新命令
    //    /// </summary>
    //    /// <param name="cmd"></param>
    //    public void AddCommand(ICommand cmd)
    //    {
    //        //commandList.Add(cmd);
    //        commandQE.Enqueue(cmd);
    //    }

    //    /// <summary>
    //    /// 执行所有命令
    //    /// </summary>
    //    public void ExecuteCommand()
    //    {
    //        // 命令队列为空，直接返回
    //        if (commandQE.Count == 0) return;

    //        // 启动监控线程，在线程内完成命令执行和设备监控
    //        if(devMonitor.IsMonitorThreadRunning() == false)
    //        {
    //            devMonitor.StartMonitor();
    //        }

    //        //// 遍历命令集合，执行命令
    //        //foreach(ICommand cmd in commandList)
    //        //{
    //        //    try
    //        //    {
    //        //        // 执行命令
    //        //        cmd.Execute();
    //        //        // 添加监控对象
    //        //        devMonitor.MonitorDevList.Add(cmd.DevNo);

    //        //        // 发布命令正常执行的事件
    //        //        RaiseOnCommandExecute(cmd.CommandType, cmd.DevNo);
    //        //    }
    //        //    catch(Exception e)
    //        //    {
    //        //        // 发布命令异常的事件
    //        //        RaiseOnCommandError(cmd.CommandType, cmd.DevNo, e);
    //        //    }               
    //        //}

    //        //// 启动监控(监控设备数量不为0，且监控线程未启动)
    //        //if((devMonitor.MonitorDevList.Count() > 0) && (devMonitor.IsMonitorThreadRunning() == false))
    //        //{
    //        //    devMonitor.StartMonitor();
    //        //}
    //    }

    //    /// <summary>
    //    /// 处理命令异常事件
    //    /// </summary>
    //    /// <param name="cmd"></param>
    //    /// <param name="e"></param>
    //    public void RaiseOnCommandError(string cmdType, int devNo, Exception e)
    //    {
    //        if(OnCommandError != null)
    //        {
    //            OnCommandError(this, new CommandExceptionEventArgs(e, devNo, cmdType));
    //        }
    //    }

    //    /// <summary>
    //    /// 处理命令正常处理事件
    //    /// </summary>
    //    /// <param name="cmd"></param>
    //    /// <param name="e"></param>
    //    public void RaiseOnCommandExecute(string cmdType, int devNo)
    //    {
    //        if (OnCommandExecute != null)
    //        {
    //            OnCommandExecute(this, new CommandExecuteEventArgs(devNo, cmdType));
    //        }
    //    }
    //}
}
