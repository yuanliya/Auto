using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NJUST.AUTO06.Utility
{
    public static class LogHelper
    {
        static log4net.ILog loginLogger = log4net.LogManager.GetLogger("登录日志");//登录日志
        static log4net.ILog infoLogger = log4net.LogManager.GetLogger("操作日志");//操作日志
        static log4net.ILog errorLogger = log4net.LogManager.GetLogger("错误日志");//错误日志

        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="userCode"></param>
        public static  void SetUserProperty(string userCode)
        {
            log4net.GlobalContext.Properties["UserCode"] = userCode;//自定义属性
        }

        public static void LogUserInfo(string message)
        {
            loginLogger.Info(message);
        }

        public static void LogOpInfo(string message)
        {
            infoLogger.Info(message);
        }


        public static void LogSysError(string mesage, Exception ex)
        {
            if (!string.IsNullOrEmpty(mesage) && ex == null)
            {
                errorLogger.ErrorFormat("【附加信息】 : {0}<br>", new object[] { mesage });
            }
            else if (!string.IsNullOrEmpty(mesage) && ex != null)
            {
                string errorMsg = BeautyErrorMsg(ex);
                errorLogger.ErrorFormat("【附加信息】 : {0}<br>{1}", new object[] { mesage, errorMsg });
            }
            else if (string.IsNullOrEmpty(mesage) && ex != null)
            {
                string errorMsg = BeautyErrorMsg(ex);
                errorLogger.Error(errorMsg);
            }
        }

        /// <summary>
        /// 美化错误信息
        /// </summary>
        /// <param name="ex">异常</param>
        /// <returns>错误信息</returns>
        private static string BeautyErrorMsg(Exception ex)
        {
            string errorMsg = string.Format("【异常类型】：{0} <br>【异常信息】：{1} <br>【堆栈调用】：{2}", new object[] { ex.GetType().Name, ex.Message, ex.StackTrace });
            errorMsg = errorMsg.Replace("\r\n", "<br>");
            errorMsg = errorMsg.Replace("位置", "<strong style=\"color:red\">位置</strong>");
            return errorMsg;
        }
    }
}
