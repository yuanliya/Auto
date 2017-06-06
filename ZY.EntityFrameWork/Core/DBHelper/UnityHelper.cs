using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZY.EntityFrameWork.Core.Context;

namespace ZY.EntityFrameWork.Core.DBHelper
{
    public class UnityHelper
    {
        // 单例模式
        private static UnityHelper instance = new UnityHelper();
        public static UnityHelper Instance
        {
            get { return instance; }
        }

        /// <summary>
        /// IOC的容器，可调用来获取对应接口实例。
        /// </summary>
        private IUnityContainer container;

        private UnityHelper()
        {
            container = new UnityContainer();

            // 读取配置文件中的接口和实现类
            UnityConfigurationSection section = (UnityConfigurationSection)ConfigurationManager.GetSection("unity");
            if(section != null)
            {
                // 根据配置文件实现接口和类实例的注册
                section.Configure(container);
            }

            // 注册单例模式的容器类
            container.RegisterType(typeof(UnityHelper), new ContainerControlledLifetimeManager());
            // 注册实体框架上下文
            container.RegisterType(typeof(DbContext), typeof(HZKContext));

            // 注册单例模式的单元操作上下文类
            container.RegisterType<IUnitOfWorkContext, UnitOfWorkContextBase>(new ContainerControlledLifetimeManager()); ;
            
            // 注册接口和实现类
            Register(container);
        }

        /// <summary>
        /// 使用Unity根据IRepository加载对应的Repository实体
        /// </summary>
        /// <param name="container"></param>
        private static void Register(IUnityContainer container)
        {
            Dictionary<string, Type> dictIRespority = new Dictionary<string, Type>();
            Dictionary<string, Type> dictRespority = new Dictionary<string, Type>();

            // 程序集及命名空间后缀名
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            string resporitySuffix  = ".Repositories.Impl";
            string iresporitySuffix = ".Repositories.Interface";

            // 对比程序集里面的接口和具体的接口实现类，把它们分别放到不同的字典集合里
            foreach (Type objType in currentAssembly.GetTypes())
            {
                string defaultNamespace = objType.Namespace;
                if (defaultNamespace == null)
                {
                    continue;
                }

                if (objType.IsInterface )
                {
                    // 接口
                    if (!dictIRespority.ContainsKey(objType.FullName) 
                        && defaultNamespace.EndsWith(iresporitySuffix))
                    {
                        dictIRespority.Add(objType.FullName, objType);
                    }
                }
                else if (defaultNamespace.EndsWith(resporitySuffix))
                {
                    // 接口对应的实体
                    if (!dictRespority.ContainsKey(objType.FullName))
                    {
                        dictRespority.Add(objType.FullName, objType);
                    }
                }
            }

            // 根据注册的接口和接口实现集合，使用IOC容器进行注册
            foreach (string key in dictIRespority.Keys)
            {
                Type interfaceType = dictIRespority[key];

                foreach (string resporityKey in dictRespority.Keys)
                {
                    Type resporityType = dictRespority[resporityKey];
                    if (interfaceType.IsAssignableFrom(resporityType))
                    {
                        // 该Respority是否实现了对应接口，实现则注册
                        container.RegisterType(interfaceType, resporityType);
                    }
                }
            }
        }

        /// <summary>
        /// 从容器中提取接口对应的类实例
        /// </summary>
        /// <typeparam name="T">接口</typeparam>
        /// <returns></returns>
        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        /// <summary>
        /// 依据命名从容器中提取接口对应的类实例
        /// </summary>
        /// <typeparam name="T">接口</typeparam>
        /// <param name="name">命名</param>
        /// <returns></returns>
        public T Resolve<T>(string name)
        {
            return container.Resolve<T>(name);
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="t"></param>
        public void RegisterType(Type t)
        {
            container.RegisterType(t);
        }

        /// <summary>
        /// 注册映射
        /// </summary>
        /// <typeparam name="from"></typeparam>
        /// <typeparam name="to"></typeparam>
        /// <param name="name"></param>
        public void RegisterType<from, to>(string name) where to : from
        {
            container.RegisterType<from, to>(name);
        }
    }
}
