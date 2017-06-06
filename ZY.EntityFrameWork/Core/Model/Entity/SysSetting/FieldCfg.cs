using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库中的字段配置表
    /// </summary>
    public class FieldCfg : BaseEntity
    {
        /// <summary>
        /// 字段变量名
        /// </summary>
        public string FieldName { get; set; }

        /// <summary>
        /// 字段显示名
        /// </summary>
        public string FieldShowName { get; set; }

        /// <summary>
        /// 字段是否可用
        /// </summary>
        public Boolean IsFieldUsable { get; set; }

        /// <summary>
        /// 字段是否可见
        /// </summary>
        public Boolean IsFieldVisible { get; set; }

        /// <summary>
        /// 字段是否为查询关键字
        /// </summary>
        public Boolean IsKeyWord { get; set; }

        /// <summary>
        /// 字段类型（文本或者复选）
        /// </summary>
        public string FieldType { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 字段排序号
        /// </summary>
        public int SerialNo { get; set; }
    }
}
