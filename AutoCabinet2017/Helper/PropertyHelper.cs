using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZY.EntityFrameWork.Core.Model.Attibutes;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace AutoCabinet2017.Helper
{
    // 用于Combobox控件条目的中英文对照
    public class ItemEx
    {
        public object Tag;   // 别称
        public string Text;  // 显示名

        public ItemEx(object tag, string text)
        {
            this.Tag  = tag;
            this.Text = text;
        }

        public override string ToString()
        {
            return this.Text.ToString();
        }
    }

    /// <summary>
    /// 记录串口信息
    /// </summary>
    public class SCommConfig
    {
        /// <summary>
        /// 标记
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// 串口号
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 波特率
        /// </summary>
        public int Baud { get; set; }

        /// <summary>
        /// 数据位
        /// </summary>
        public int DataBits { get; set; }

        /// <summary>
        /// 停止位
        /// </summary>
        public string StopBits { get; set; }

        /// <summary>
        /// 校验位
        /// </summary>
        public string Parity { get; set; }
    }

    /// <summary>
    /// 表示软件相关属性的类
    /// </summary>
    public class PropertyHelper
    {
        /// <summary>
        /// 登录用户信息
        /// </summary>
        private static UserDto _currentUser = new UserDto();
        public static UserDto CurrentUser
        {
            get { return _currentUser; }
            set { _currentUser = value; }
        }

        /// <summary>
        /// 设备使用的串口
        /// </summary>
        private static SCommConfig _scommConfig = new SCommConfig();
        public static SCommConfig SCommInfo
        {
            get { return _scommConfig; }
            set { _scommConfig = value; }
        }

        /// <summary>
        /// 字段分类的明细
        /// </summary>
        private static List<DataDictDto> _dataDictItems = new List<DataDictDto>();
        public static List<DataDictDto> DataDictItems
        {
            get { return _dataDictItems; }
            set { _dataDictItems = value; }
        }

        /// <summary>
        /// 档案字段名称映射及是否显示
        /// </summary>
        private static List<FieldCfgDto> _fieldCfgItems = new List<FieldCfgDto>();
        public static List<FieldCfgDto> FieldCfgItems
        {
            get { return _fieldCfgItems; }
            set { _fieldCfgItems = value; }
        }

        /// <summary>
        /// 用户权限
        /// </summary>
        private static List<RoleModuleDto> _roleModuleDto = new List<RoleModuleDto>();
        public static List<RoleModuleDto> RoleModules
        {
            get { return _roleModuleDto; }
            set { _roleModuleDto = value; }
        }

        /// <summary>
        /// 所有模块
        /// </summary>
        public static List<ModuleDto> Modules { get; set; }
    }
}
