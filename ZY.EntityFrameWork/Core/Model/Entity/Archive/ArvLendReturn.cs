using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库的档案信息表
    /// </summary>
    public class ArvLendReturn : BaseEntity
    {

        /// <summary>
        /// 档案编号
        /// </summary>
        public string ArvID { get; set; }

        /// <summary>
        /// 档案名称
        /// </summary>
        public string LendID { get; set; }

        /// <summary>
        /// 归还表的ID
        /// </summary>
        public string ReturnID { get; set; }

        /// <summary>
        /// 档案类型
        /// </summary>
        public ArchiveInfo ArchiveInfo { get; set; }

        /// <summary>
        /// 所属年度
        /// </summary>
        public ArvLendInfo ArvLend { get; set; }

        public ArvReturnInfo ArvReturn { get; set; }

        // 属性要设为IsConcurrencyToken().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
        // 时间戳作为乐观锁
        public DateTime RowVersion { get; set; }
    }
}
