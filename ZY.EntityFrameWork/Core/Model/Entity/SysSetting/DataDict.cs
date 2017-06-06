using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库中的数据字典表
    /// </summary>
    public class DataDict : BaseEntity
    {
        /// <summary>
        /// 字段类型名
        /// </summary>
        public string FieldTypeName { get; set; }

        /// <summary>
        /// 字段值
        /// </summary>
        public string FieldTypeValue { get; set; }

        /// <summary>
        /// 字段排序号
        /// </summary>
        public int FieldTypeSerialNo { get; set; }
    }
}
