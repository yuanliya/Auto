using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AutoCabinet2017.Controller
{
    /// <summary>
    /// 执行命令并监控设备
    /// </summary>
    public class CommandInvoker
    {
        // 命令队列
        private Queue<ICommand> commandQE = new Queue<ICommand>();

        #region 命令执行和设备监控中的事件

        // 发布命令执行错误的事件
        public event EventHandler<CommandExceptionEventArgs> OnCommandError;
        // 发布命令执行正常的事件
        public event EventHandler<CommandExecuteEventArgs> OnCommandExecute;

        /// <summary>
        /// 处理命令异常事件
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="e"></param>
        public void RaiseOnCommandError(string cmdType, int devNo, Exception e)
        {
            if (OnCommandError != null)
            {
                OnCommandError(this, new CommandExceptionEventArgs(e, devNo, cmdType));
            }
        }

        /// <summary>
        /// 处理命令正常处理事件
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="e"></param>
        public void RaiseOnCommandExecute(string cmdType, int devNo)
        {
            if (OnCommandExecute != null)
            {
                OnCommandExecute(this, new CommandExecuteEventArgs(devNo, cmdType));
            }
        }

        #endregion

        /// <summary>
        /// 添加新命令
        /// </summary>
        /// <param name="cmd">控制指令</param>
        public void AddCommand(ICommand cmd)
        {
            commandQE.Enqueue(cmd);
        }

        /// <summary>
        /// 执行所有命令
        /// </summary>
        /// <returns>成功执行的命令List</returns>
        public List<ICommand> ExecuteCommand()
        {
            ICommand cmd = null;
            List<ICommand> commandToExecute = new List<ICommand>();

            // 命令队列为空，直接返回
            if (commandQE.Count == 0) return commandToExecute;

            // 执行队列里所有命令
            while (commandQE.Count > 0)
            {
                try
                {
                    // 从队列中取出命令
                    cmd = commandQE.Dequeue();
                    // 执行命令
                    cmd.Execute();

                    // 命令正确执行
                    RaiseOnCommandExecute(cmd.CommandType, cmd.DevNo);
                    // 保存正确执行的命令
                    commandToExecute.Add(cmd);
                }
                catch (Exception ex)
                {
                    // 命令执行错误
                    RaiseOnCommandError(cmd.CommandType, cmd.DevNo, ex);
                }                 
            }

            return commandToExecute;
        }
    }
}
