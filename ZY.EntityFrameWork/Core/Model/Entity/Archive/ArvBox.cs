using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库的档案盒信息表
    /// </summary>
    public class ArvBox : BaseEntity
    {
        public ArvBox()
        {
            Arvs = new Collection<ArchiveInfo>();
        }

        // 直接以档案盒ID作为主键，但RegisterModify中的catch里是按主键找到实体的，所以会导致无法更新
        /// <summary>
        /// 档案盒ID
        /// </summary>
      //  public string ArvBoxID { get; set; }

        /// <summary>
        /// 档案盒题名
        /// </summary>
        public string ArvBoxTitle { get; set; }

        /// <summary>
        /// 存放柜号
        /// </summary>
        public int GroupNo { get; set; }

        /// <summary>
        /// 存放层号
        /// </summary>
        public int LayerNo { get; set; }

        /// <summary>
        /// 存放格号
        /// </summary>
        public int CellNo { get; set; }

        /// <summary>
        /// 档案盒中的档案，1：N关系
        /// </summary>
        public virtual Collection<ArchiveInfo> Arvs { get; set; }
    }
}
