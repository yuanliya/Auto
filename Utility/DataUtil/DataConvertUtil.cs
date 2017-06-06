using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.ComponentModel;

namespace NJUST.AUTO06.Utility
{
    public class DataConvertUtil
    {
        #region 构造函数和实例

        // 私有构造函数
        DataConvertUtil()
        {
        }

        // 利用单件模式构造线程安全的独立类实例
        public static readonly DataConvertUtil Instance = new DataConvertUtil();
        
        #endregion

        /// <summary>
        /// 利用反射机制将BindingList转换为DataTable
        /// </summary>
        /// <typeparam name="T">泛型，List的数据类型</typeparam>
        /// <param name="entitys">BindingList对象</param>
        /// <returns>数据表</returns>
        public DataTable BindingListToDataTable<T>(BindingList<T> entity)
        {
            // 检查实体集合不能为空
            if (entity == null || entity.Count < 1)
            {
                throw new Exception("需转换的集合为空");
            }

            // 取出BindingList包含实体的所有Properties
            Type entityType = entity[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            // 生成DataTable的structure
            // 生产代码中，应将生成的DataTable结构Cache起来，此处略
            DataTable dt = new DataTable();
            for (int i = 0; i < entityProperties.Length; i++)
            {
                // 创建具有指定名称的DataTable的列
                dt.Columns.Add(entityProperties[i].Name);
            }

            // 将所有entity添加到DataTable中
            foreach (object obj in entity)
            {
                // 检查所有的的实体都为同一类型
                if (obj.GetType() != entityType)
                {
                    throw new Exception("要转换的集合元素类型不一致");
                }

                // 获取BindingList包含对象所有属性的值
                object[] entityValues = new object[entityProperties.Length];
                for (int i = 0; i < entityProperties.Length; i++)
                {
                    entityValues[i] = entityProperties[i].GetValue(obj, null);
                }

                // 加入DataTable
                dt.Rows.Add(entityValues);
            }

            return dt;
        }
    }
}
