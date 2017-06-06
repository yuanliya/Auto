using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的档案信息实体，是持久层的档案Model的扁平化
    /// 需要Dto和Model之间的映射
    /// </summary>
    public class ArchiveInfoDto : BaseDto
    {
        public ArchiveInfoDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 档案编号
        /// </summary>
        [Display(Name = "档案编号")]
        public string ArvID { get; set; }

        /// <summary>
        /// 档案名称
        /// </summary>
        [Display(Name = "档案名称")]
        public string ArvTitle { get; set; }

        /// <summary>
        /// 档案盒编号
        /// </summary>
        [Display(Name = "档案盒编号")]
        public string ArvBoxID { get; set; }

        /// <summary>
        /// 档案盒名称
        /// </summary>
        [Display(Name = "档案盒名称")]
        public string ArvBoxTitle { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        [Display(Name = "所属部门")]
        public string ArvUnit { get; set; }

        /// <summary>
        /// 档案类型
        /// </summary>
        [Display(Name = "档案类型")]
        public string ArvType { get; set; }

        /// <summary>
        /// 所属年度
        /// </summary>
        [Display(Name = "所属年度")]
        public string ArvYear { get; set; }

        /// <summary>
        /// 电子标签ID
        /// </summary>
        [Display(Name = "电子标签ID")]
        public string LabelID { get; set; }

        /// <summary>
        /// 档案状态
        /// </summary>
        [Display(Name = "档案状态")]
        public string ArvStatus { get; set; }

        /// <summary>
        /// 预览
        /// </summary>
        [Display(Name = "预览")]
        public string Edoc { get; set; }

        /// <summary>
        /// 备用1
        /// </summary>
        [Display(Name = "备用1")]
        public string Rsv1 { get; set; }

        /// <summary>
        /// 备用2
        /// </summary>
        [Display(Name = "备用2")]
        public string Rsv2 { get; set; }

        /// <summary>
        /// 备用3
        /// </summary>
        [Display(Name = "备用3")]
        public string Rsv3 { get; set; }

        /// <summary>
        /// 备用4
        /// </summary>
        [Display(Name = "备用4")]
        public string Rsv4 { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        [Display(Name = "柜号")]
        public int GroupNo { get; set; }

        /// <summary>
        /// 层号
        /// </summary>
        [Display(Name = "层号")]
        public int LayerNo { get; set; }

        /// <summary>
        /// 格号
        /// </summary>
        [Display(Name = "格号")]
        public int CellNo { get; set; }

        /// <summary>
        /// 立档时间
        /// </summary>
        [Display(Name = "立档时间")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 立档人
        /// </summary>
        [Display(Name = "立档人")]
        public String CreatePerson { get; set; }
    }
}
