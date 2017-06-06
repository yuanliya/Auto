using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using ZY.EntityFrameWork.Core.Model.Entity;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的数据字典DTO实体
    /// </summary>
    public class DataDictDto : BaseDto
    {
        public DataDictDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [DisplayName("字典类别")]
        /// <summary>
        /// 字段类型名
        /// </summary>
        public string FieldTypeName { get; set; }

        [DisplayName("字典条目")]
        /// <summary>
        /// 字段值
        /// </summary>
        public string FieldTypeValue { get; set; }

        [DisplayName("字典条目序号")]
        /// <summary>
        /// 字段排序号
        /// </summary>
        public int FieldTypeSerialNo { get; set; }
    }
}
