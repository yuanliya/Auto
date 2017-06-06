using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZY.EntityFrameWork.Core.Model.Entity
{
    /// <summary>
    /// 映射数据库中的用户表
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UserCode { get; set; }

        /// <summary>
        ///  用户姓名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        ///  登录密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        ///  用户部门
        /// </summary>
        public string UserDept { get; set; }

        /// <summary>
        ///  用户角色ID
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 用户和角色是1：1关系
        /// </summary>
        public virtual Role UserRole { get; set; }
    }
}
