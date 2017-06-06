using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的出库信息实体
    /// </summary>
    public class OutCabInfoDto : BaseDto
    {
        public OutCabInfoDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [Display(Name = "档案编号")]
        // <summary>
        // 档案编号
        // </summary>
        public string ArvID { get; set; }

        [Display(Name = "备注")]
        // <summary>
        // 出柜备注
        // </summary>
        public string OutRemark { get; set; }

        [Display(Name = "出库缘由")]
        // <summary>
        // 出柜缘由
        // </summary>
        public string OutReason { get; set; }

        [Display(Name = "出库日期")]
        // <summary>
        // 出柜日期
        // </summary>
        public string OutDate { get; set; }

        [Display(Name = "经办人")]
        // <summary>
        // 经办人
        // </summary>
        public string Adminer { get; set; }
    }
}
