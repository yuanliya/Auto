using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库中的角色-模块关系表
    /// </summary>
    public class RoleModule : BaseEntity
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public string ModuleId { get; set; }

        /// <summary>
        /// 模块是否对该角色开放
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 角色权限
        /// </summary>
        public int Permissions { get; set; }

        /// <summary>
        /// 角色对应的模块实体
        /// </summary>
        public virtual Module Module { get; set; }

        /// <summary>
        /// 角色实体
        /// </summary>
        public virtual Role Role { get; set; }
    }
}
