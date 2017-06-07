using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的档案盒实体
    /// </summary>
    public class ArvBoxDto : BaseDto
    {
        public ArvBoxDto()
        {
            // 每个实体生成独一无二的ID
            //ID = Guid.NewGuid().ToString("N");
        }

        [Display(Name = "档案盒编号")]
        public override string ID { get; set; }
       // public string ArvBoxID { get; set; }

        [Display(Name = "档案盒名称")]
        public string ArvBoxTitle { get; set; }
        
        [Display(Name = "柜号")]
        public int GroupNo { get; set; }
        
        [Display(Name = "层号")]
        public int LayerNo { get; set; }
        
        [Display(Name = "格号")]
        public int CellNo { get; set; }

    }
}
