using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的字段配置DTO实体
    /// </summary>
    public class FieldCfgDto : BaseDto
    {
        public FieldCfgDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [DisplayName("变量名")]
        /// <summary>
        /// 字段变量名
        /// </summary>
        public string FieldName { get; set; }

        [DisplayName("显示名")]
        /// <summary>
        /// 字段显示名
        /// </summary>
        public string FieldShowName { get; set; }

        [DisplayName("是否可用")]
        /// <summary>
        /// 字段是否可用
        /// </summary>
        public Boolean IsFieldUsable { get; set; }

        [DisplayName("是否可见")]
        /// <summary>
        /// 字段是否可见
        /// </summary>
        public Boolean IsFieldVisible { get; set; }

        [DisplayName("是否关键字")]
        /// <summary>
        /// 字段是否为查询关键字
        /// </summary>
        public Boolean IsKeyWord { get; set; }

        [DisplayName("使用类型")]
        /// <summary>
        /// 字段类型（文本或者复选）
        /// </summary>
        public string FieldType { get; set; }

        [DisplayName("字段描述")]
        /// <summary>
        /// 字段描述
        /// </summary>
        public string Remark { get; set; }

        [DisplayName("字段序号")]
        /// <summary>
        /// 字段排序号
        /// </summary>
        public int SerialNo { get; set; }
    }
}
