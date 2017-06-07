using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        /// 档案借阅记录编号
        /// </summary>
        public string LendID { get; set; }

        /// <summary>
        /// 档案归还记录编号
        /// </summary>
        public string ReturnID { get; set; }

        /// <summary>
        /// 档案信息
        /// </summary>
        public ArchiveInfo ArchiveInfo { get; set; }

        /// <summary>
        /// 档案借阅信息
        /// </summary>
        public ArvLendInfo ArvLend { get; set; }

        /// <summary>
        /// 档案归还信息
        /// </summary>
        public ArvReturnInfo ArvReturn { get; set; }

        // 属性要设为IsConcurrencyToken().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
        // 时间戳作为乐观锁
        public DateTime RowVersion { get; set; }
    }
}
