using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库的借阅信息表 
    /// </summary>
    public class ArvLendInfo : BaseEntity
    {
        public ArvLendInfo()
        {
            this.ArvLendReturns = new Collection<ArvLendReturn>();
        }

        // <summary>
        // 借阅人
        // </summary>
        public string Lender { get; set; }

        // <summary>
        // 借阅人部门
        // </summary>
        public string LenderDept { get; set; }

        // <summary>
        // 借出日期
        // </summary>
        public DateTime LendDate { get; set; }

        // <summary>
        // 应还日期
        // </summary>
        public DateTime ReturnDeadline { get; set; }

        // <summary>
        // 借阅经办人
        // </summary>
        public string LendExecuter { get; set; }

        // <summary>
        // 档案编号做为外键关联档案信息实体
        // </summary>
        //public string ArvID { get; set; }

        /// <summary>
        /// 档案借阅与档案信息是1：1关系
        /// </summary>
        //public virtual ArchiveInfo ArchiveInfo { get; set; }
        public virtual ICollection<ArvLendReturn> ArvLendReturns { get; set; }

        /// <summary>
        /// 档案归还信息与档案借阅信息是1：1关系
        /// </summary>
        public virtual ArvReturnInfo ArvReturnInfo { get; set; }
    }
}
