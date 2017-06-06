using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace NJUST.AUTO06.Utility.XmlUtil
{
    public class ModuleItem
    {
        public ModuleItem()
        {
        }

        #region XML文件的属性字段

        /// <summary>
        /// 标记
        /// </summary>
        [XmlAttribute("ModuleTag")]
        public string ModuleTag { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        [XmlAttribute("ParentTag")]
        public string ParentTag { get; set; }

        /// <summary>
        /// 模块名
        /// </summary>
        [XmlAttribute("ModuleName")]
        public string ModuleName { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [XmlAttribute("Enabled")]
        public bool Enabled { get; set; }


        /// <summary>
        /// 权限
        /// </summary>
        [XmlAttribute("Permissions")]
        public int Permissions { get; set; }

        #endregion
    }

    /// <summary>
    /// 基于序列化的XML操作类
    /// </summary>
    [XmlRoot("ModuleInfo")]    // Xml文件的根节点名
    public class ModuleXml
    {
        public ModuleXml()
        {
            Items = new List<ModuleItem>();
        }

        /// <summary>
        /// SCommItem列表集合
        /// “Modules”为子节点名，“Item”对应节里面的元素
        /// </summary>
        [XmlArray("Modules"), XmlArrayItem("Item")]
        public List<ModuleItem> Items { get; set; }

        /// <summary>
        /// 索引器
        /// </summary>
        /// <param name="Tag">索引标记</param>
        /// <returns>标记项</returns>
        public ModuleItem this[string tag]
        {
            get { return Items.Find(item => item.ModuleTag == tag); }
        }

    }
}

