using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的归还信息实体
    /// </summary>
    public class ArvReturnInfoDto : BaseDto
    {
        public ArvReturnInfoDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [Display(Name = "归还人")]
        // <summary>
        // 归还人
        // </summary>
        public string Returner { get; set; }

        [Display(Name = "归还人部门")]
        // <summary>
        // 归还人部门
        // </summary>
        public string ReturnerDept { get; set; }

        [Display(Name = "归还时间")]
        // <summary>
        // 归还时间
        // </summary>
        public DateTime ReturnDate { get; set; }

        [Display(Name = "经办人")]
        // <summary>
        // 经办人
        // </summary>
        public string ReturnExecuter { get; set; }

        //[Display(Name = "档案编号")]
        ///// <summary>
        ///// 档案编号
        ///// </summary>
        //public string ArvID { get; set; }
    }
}
