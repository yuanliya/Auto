using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的借阅信息实体
    /// </summary>
    public class ArvLendInfoDto : BaseDto
    {
        public ArvLendInfoDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [Display(Name = "档案编号")]
        // <summary>
        // 档案编号
        // </summary>
        public string ArvID { get; set; }

        public string ArchiveID { get; set; }

        [Display(Name = "借阅人")]
        // <summary>
        // 借阅人
        // </summary>
        public string Lender { get; set; }

        [Display(Name = "借阅部门")]
        // <summary>
        // 借阅人部门
        // </summary>
        public string LenderDept { get; set; }

        [Display(Name = "借阅日期")]
        // <summary>
        // 借出日期
        // </summary>
        public DateTime LendDate { get; set; }

        [Display(Name = "应还日期")]
        // <summary>
        // 应还日期
        // </summary>
        public DateTime ReturnDeadline { get; set; }

        [Display(Name = "借阅经办人")]
        // <summary>
        // 借阅经办人
        // </summary>
        public string LendExecuter { get; set; }

        
    }
}
