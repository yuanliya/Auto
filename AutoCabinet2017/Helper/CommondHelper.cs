using System;

using System.IO;
using System.Diagnostics;

namespace AutoCabinet2017.Helper
{
    /// <summary>
    /// 在DOS窗口下执行指令的辅助类
    /// </summary>
    public class CommandHelper
    {
        // 私有构造函数
        private CommandHelper()
        { }

        // 单例模式的类对象
        public static readonly CommandHelper Instance = new CommandHelper();

        /// <summary>  
        /// 执行DOS命令，返回DOS命令的输出  
        /// </summary>  
        /// <param name="dosCommand">dos命令</param>  
        /// <param name="milliseconds">等待命令执行的时间（单位：毫秒），如果设定为0，则无限等待</param>  
        /// <returns>返回DOS命令的输出</returns>  
        public string Execute(string command, int seconds, string workingDirectoty=null)
        {
            if (string.IsNullOrEmpty(command)) return string.Empty;

            string output = ""; // 输出字符串  

            // 进程启动信息
            ProcessStartInfo startInfo = new ProcessStartInfo();
            if (workingDirectoty != null)
            {
                startInfo.WorkingDirectory = workingDirectoty;
            }
                
            startInfo.FileName               = "cmd.exe";        // 设定需要执行的命令  
            startInfo.Arguments              = "/C " + command;  //“/C”表示执行完命令后马上退出  
            startInfo.UseShellExecute        = false;            // 不使用系统外壳程序启动  
            startInfo.RedirectStandardInput  = false;            // 不重定向输入  
            startInfo.RedirectStandardOutput = true;             // 重定向输出  
            startInfo.CreateNoWindow         = true;             // 不创建窗口  

            Process process = new Process();  // 创建进程对象  
            process.StartInfo = startInfo;
         
            try
            {
                if (process.Start()) // 开始进程  
                {
                    if (seconds == 0)
                    {
                        process.WaitForExit();        // 这里无限等待进程结束  
                    }
                    else
                    {
                        process.WaitForExit(seconds); // 等待进程结束，等待时间为指定的毫秒  
                    }

                    output = process.StandardOutput.ReadToEnd(); // 读取进程的输出  
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (process != null)
                { 
                    process.Close(); 
                }                 
            }

            return output;
        }

        /// <summary>  
        /// 执行DOS命令，返回DOS命令的输出  
        /// </summary>  
        /// <param name="dosCommand">dos命令</param>  
        /// <param name="milliseconds">等待命令执行的时间（单位：毫秒），  
        /// 如果设定为0，则无限等待</param>  
        /// <returns>返回DOS命令的输出</returns>  
        public string Execute(string fileName, string command, int seconds)
        {
            if (string.IsNullOrEmpty(command)) return string.Empty;

            string output = ""; // 输出字符串  

            ProcessStartInfo startInfo = new ProcessStartInfo();

            startInfo.FileName               = fileName; // 设定需要执行的命令  
            startInfo.Arguments              = command;  //“/C”表示执行完命令后马上退出  
            startInfo.UseShellExecute        = false;    // 不使用系统外壳程序启动  
            startInfo.RedirectStandardInput  = false;    // 不重定向输入  
            startInfo.RedirectStandardOutput = true;     // 重定向输出  
            startInfo.CreateNoWindow         = true;     // 不创建窗口

            Process process = new Process(); // 创建进程对象  
            process.StartInfo = startInfo;

            try
            {
                if (process.Start())//开始进程  
                {
                    if (seconds == 0)
                    {
                        process.WaitForExit();        // 这里无限等待进程结束  
                    }
                    else
                    {
                        process.WaitForExit(seconds); // 等待进程结束，等待时间为指定的毫秒  
                    }

                    output = process.StandardOutput.ReadToEnd(); // 读取进程的输出  
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (process != null)
                {
                    process.Close();
                }                  
            }

            return output;
        }
    }
}

