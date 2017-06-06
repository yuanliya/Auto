using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 定义基类
    /// </summary>
    public class BaseEntity
    {
        // 独一无二的ID
        public string ID { get; set; }

        //MySQL用这行做并发处理不可行
        //MySqlMigrationSqlGenerator不允许byte[]类型上标记TimeStamp/RowVersion，这里使用DateTime类型
        //[Timestamp]
        //public byte[] RowVersion { get; set; }       
    }
}