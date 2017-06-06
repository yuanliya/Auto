using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Reflection;

namespace ZY.EntityFrameWork.Core.DBHelper
{
    /// <summary>
    /// 利用反射机制动态生成命名空间的类实例
    /// </summary>
    /// <typeparam name="T">类名</typeparam>
    public class Reflect<T> where T : class 
    {
        // 类的哈希表属性
        private static Hashtable m_objCache = null;
        public static Hashtable ObjCache
        {
            get
            {
                if (m_objCache == null)
                {
                    m_objCache = new Hashtable();
                }

                return m_objCache;
            }
        }

        /// <summary>
        /// 利用反射机制，通过程序集生成命令空间的类对象
        /// </summary>
        /// <param name="nameSpace">命名空间</param>
        /// <param name="assemblyName">程序集</param>
        /// <returns>类实例</returns>
        public static T Create(string nameSpace, string assemblyName)
        {
            T objType  = null;

            // 利用反射机制，调用程序集的CreateInstance()方法生成类实例
            //没有默认构造函数的情况下无法使用
            objType = (T)CreateAssembly(assemblyName).CreateInstance(nameSpace);

            return objType;
        }

        /// <summary>
        /// 生成程序集的实例
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>程序集实例</returns>
        public static Assembly CreateAssembly(string assemblyName)
        {
            // 从缓存哈希表中读取程序集的键值，判断是否已有程序集实例
            Assembly assObj = (Assembly)ObjCache[assemblyName];
            if (assObj == null)
            {
                // 生成程序集对象
                assObj = Assembly.Load(assemblyName);
                // 加入缓存中的哈希表
                ObjCache.Add(assemblyName, assObj);
            }

            return assObj;
        }
    }
}
