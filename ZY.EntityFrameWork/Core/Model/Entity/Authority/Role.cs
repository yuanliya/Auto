using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库中的角色表
    /// </summary>
    public class Role : BaseEntity
    {
        public Role()
        {
            Users       = new Collection<User>();
            RoleModules = new Collection<RoleModule>();
        }

        /// <summary>
        /// 角色名
        /// </summary>
        public String RoleName { get; set; }

        /// <summary>
        /// 角色描述
        /// </summary>
        public String Description { get; set; }

        /// <summary>
        /// 角色级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 角色包括的用户，是1：N的关系
        /// </summary>
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// 角色可以使用的模块，是1：N的关系
        /// </summary>
        public virtual ICollection<RoleModule> RoleModules { get; set; }
    }
}
