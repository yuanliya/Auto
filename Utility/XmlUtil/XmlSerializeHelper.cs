using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace NJUST.AUTO06.Utility
{
    /// <summary>
    /// 利用序列化和面向对象方式处理XML文件的类
    /// </summary>
    /// <typeparam name="TItem">与XML节中元素对应的类对象</typeparam>
    /// <typeparam name="TEntity">与XML节对应的类对象，是TItem的集合</typeparam>
    public class XmlSerializeHelper<TItem, TEntity> where TItem:   class
                                                    where TEntity: class 
    {
        // Xml序列化类
        private static XmlSerializer serializer = null;
        // 读写流
        private static Stream writer = null;
        // XML文件名
        private static string fileName;

        public XmlSerializeHelper()
        {
        }

        /// <summary>
        /// 打开XML文件
        /// </summary>
        /// <typeparam name="T">泛型，XML文件中Item对应的实体类</typeparam>
        /// <param name="fileName"></param>
        public static void OpenXmlFile(string filePath)
        {
            // 定义XML文档的数据类型
            Type[] types = new Type[] 
            { 
                typeof(TItem), 
            };

            // 创建XmlSerializer，可以将指定类型的对象序列化为XML文档，也能将XML文档反序列化为指定类型的对象
            serializer = new XmlSerializer(typeof(TEntity), types);

            // 指定XML文件
            if (!File.Exists(filePath))
            {
                string error = string.Format("文件 {0} 不存在！", filePath);

                throw new Exception(error);
            }

            // 保存文件名
            fileName = filePath;
        }

        /// <summary>
        /// 读XML文件
        /// </summary>
        /// <typeparam name="T">泛型，XML文件中自定义节对应的实体类</typeparam>
        /// <returns></returns>
        public static TEntity ReadXML()
        {
            try
            {
                using (Stream reader = new FileStream(fileName, FileMode.Open))
                {
                    return (TEntity)serializer.Deserialize(reader);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        /// <summary>
        /// 写XML文件
        /// </summary>
        /// <typeparam name="T">泛型，XML文件中自定义节对应的实体类</typeparam>
        /// <param name="xmlObj"></param>
        public static void WriteXML(TEntity xmlObj)
        {
            try
            {
                // 序列化数据块并保存文件
                using (writer = new FileStream(fileName, FileMode.Create))
                {
                    serializer.Serialize(writer, xmlObj);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
