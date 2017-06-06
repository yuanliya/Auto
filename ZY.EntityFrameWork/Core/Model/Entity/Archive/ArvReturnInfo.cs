using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库的归还信息表 
    /// </summary>
    public class ArvReturnInfo : BaseEntity
    {
        public ArvReturnInfo()
        {
            ArvLendReturns = new Collection<ArvLendReturn>();
        }
        // <summary>
        // 归还人
        // </summary>
        public string Returner { get; set; }

        // <summary>
        // 归还人部门
        // </summary>
        public string ReturnerDept { get; set; }

        // <summary>
        // 归还时间
        // </summary>
        public DateTime ReturnDate { get; set; }

        // <summary>
        // 经办人
        // </summary>
        public string ReturnExecuter { get; set; }

        /// <summary>
        /// 档案归还信息与档案借阅信息是1：1关系
        /// </summary>
        public virtual ICollection<ArvLendReturn> ArvLendReturns { get; set; }
    }
}
