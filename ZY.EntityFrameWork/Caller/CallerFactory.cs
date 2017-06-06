using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using ZY.EntityFrameWork.Core.DBHelper;

namespace ZY.EntityFrameWork.Caller
{
    public class CallerFactory
    {
        private static CallerFactory instance = new CallerFactory();
        public static CallerFactory Instance
        {
            get { return instance; }
        }

        // 数据库服务模式：Local/WCF
        private bool   isWcf       = false;
        private string serviceMode = null; 

        private CallerFactory()
        {
            // 从配置文件读取服务调用模式
            serviceMode = ConfigurationManager.AppSettings["ServiceMode"].ToString();
            if(serviceMode == "WCF")
            {
                isWcf = true;
            }
        }

        public T GetService<T>()
        {
            if (isWcf) 
            {
                // WCF服务调用
                return UnityHelper.Instance.Resolve<T>("Wcf");
            }
            else
            {
                // 本地服务调用
                return UnityHelper.Instance.Resolve<T>("Local");
            }
        }
    }
}
