using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库的出库信息表 
    /// </summary>
    public class OutCabInfo : BaseEntity
    {
        // <summary>
        // 档案编号
        // </summary>
        public string ArvID { get; set; }

        // <summary>
        // 出柜备注
        // </summary>
        public string OutRemark { get; set; }

        // <summary>
        // 出柜缘由
        // </summary>
        public string OutReason { get; set; }

        // <summary>
        // 出柜日期
        // </summary>
        public string OutDate { get; set; }

        // <summary>
        // 经办人
        // </summary>
        public string Adminer { get; set; }
    }
}
