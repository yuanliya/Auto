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
    public class ArchiveInfo : BaseEntity
    {
        public ArchiveInfo()
        {
            ArvLendReturns = new Collection<ArvLendReturn>();
        }

        /// <summary>
        /// 档案编号
        /// </summary>
        public string ArvID { get; set; }

        /// <summary>
        /// 档案名称
        /// </summary>
        public string ArvTitle { get; set; }

        /// <summary>
        /// 所属部门
        /// </summary>
        public string ArvUnit { get; set; }

        /// <summary>
        /// 档案类型
        /// </summary>
        public string ArvType { get; set; }

        /// <summary>
        /// 所属年度
        /// </summary>
        public string ArvYear { get; set; }

        /// <summary>
        /// 电子标签ID
        /// </summary>
        public string LabelID { get; set; }

        /// <summary>
        /// 档案状态
        /// </summary>
        public string ArvStatus { get; set; }

        /// <summary>
        /// 预览
        /// </summary>
        public string Edoc { get; set; }

        /// <summary>
        /// 立档时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 立档人
        /// </summary>
        public String CreatePerson { get; set; }

        /// <summary>
        /// 备用1
        /// </summary>
        public string Rsv1 { get; set; }

        /// <summary>
        /// 备用2
        /// </summary>
        public string Rsv2 { get; set; }

        /// <summary>
        /// 备用3
        /// </summary>
        public string Rsv3 { get; set; }

        /// <summary>
        /// 备用4
        /// </summary>
        public string Rsv4 { get; set; }

        /// <summary>
        /// 档案盒主键ID
        /// 指定外键，比如更新档案盒信息时要用到
        /// </summary>
        public string ArvBoxID { get; set; }

        /// <summary>
        /// 档案与档案盒是1：1关系
        /// </summary>
        public virtual ArvBox ArvBox{ get; set; }

        /// <summary>
        /// 档案包含借阅信息，1：N关系
        /// </summary>
        public virtual ICollection<ArvLendReturn> ArvLendReturns { get; set; }

        // 属性要设为IsConcurrencyToken().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity); 
        // 时间戳作为乐观锁
        public DateTime RowVersion { get; set; }
    }
}
