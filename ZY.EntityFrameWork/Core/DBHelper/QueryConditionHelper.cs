using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.DBHelper
{
    /// <summary>
    /// 构建查询条件
    /// </summary>
    public class QueryCondition
    {
        public QueryCondition()
        {
        }

        public string FieldName { get; set; }
        public CompareType Type { get; set; }
        public CompareDataType DataType { get; set; }
        public object Value { get; set; }

        /// <summary>
        /// 查询属性
        /// </summary>
        /// <param name="fieldName">字段名</param>
        /// <param name="type">关系符</param>
        /// <param name="value">字段值</param>
        public QueryCondition(string fieldName, CompareType type, string value)
        {
            this.FieldName = fieldName;
            this.Type      = type;
            this.Value     = value;
        }

        /// <summary>
        /// 查询属性
        /// </summary>
        /// <param name="fieldName">字段名</param>
        public QueryCondition(string fieldName)
        {
            this.FieldName = fieldName;
        }
    }

    /// <summary>
    /// 关系类型枚举
    /// </summary>
    public enum CompareType
    {
        Equal = 1,                // ==
        GreaterThan = 2,          // >
        GreaterThanOrEqual = 3,   // >=
        LessThan = 4,             // <
        LessThanOrEqual = 5,      // <=
        Include = 6,              // 包含
    }

    /// <summary>
    /// 数据类型
    /// </summary>
    public enum CompareDataType
    {
        Int = 1,
        String = 2,
        Double = 3,
        Decimal = 4,
        Float = 5,
        DateTime = 6
    }
}
