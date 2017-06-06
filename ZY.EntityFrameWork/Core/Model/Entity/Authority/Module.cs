using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库中的模块表
    /// </summary>
    public class Module : BaseEntity
    {
        public Module()
        {
            // 模块包括多个子模块
            ChildModule = new Collection<Module>();
        }

        /// <summary>
        /// 模块标记
        /// </summary>
        public string ModuleTag { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }    
       
        /// <summary>
        /// 使用权限
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 模块的实际权限
        /// </summary>
        public int Permissions { get; set; }

        /// <summary>
        /// 上级模块ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 模块包含的子模块，是1：N关系
        /// </summary>
        public virtual ICollection<Module> ChildModule { get; set; }

        /// <summary>
        /// 使用该模块的角色集合，是1：N的关系
        /// </summary>
        public virtual ICollection<RoleModule> RoleModules { get; set; }
    }
}
