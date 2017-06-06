using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.IO.Ports;

namespace NJUST.AUTO06.Utility
{
    /// <summary>
    /// 串行通信参数明细,映射XML文件的属性字段
    /// </summary>
    public class SCommItem
    {
        public SCommItem()
        {
        }

        #region XML文件的属性字段

        /// <summary>
        /// 标记
        /// </summary>
        [XmlAttribute("Tag")]
        public string Tag { get; set; }

        /// <summary>
        /// 串口名
        /// </summary>
        [XmlAttribute("Name")]
        public string Name { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        [XmlAttribute("Baud")]
        public int Baud { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        [XmlAttribute("DataBits")]
        public int DataBits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        [XmlAttribute("StopBits")]
        public string StopBits { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        [XmlAttribute("Parity")]
        public string Parity { get; set; }

        #endregion
    }

    /// <summary>
    /// 基于序列化的XML操作类
    /// </summary>
    [XmlRoot("MySettings")]    // Xml文件的根节点名
    public class SCommXml
    {
        public SCommXml()
        {
            Items = new List<SCommItem>();
        }

        /// <summary>
        /// SCommItem列表集合
        /// “SComm”为子节点名，“Item”对应节里面的元素
        /// </summary>
        [XmlArray("SComm"), XmlArrayItem("Item")]
        public List<SCommItem> Items { get; set; }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="Tag">索引标记</param>
        /// <returns>标记项</returns>
        public SCommItem this[string tag]
        {
            get { return Items.Find(item => item.Tag == tag); }
        }
    }
}
