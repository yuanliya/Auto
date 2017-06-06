using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的用户DTO实体
    /// </summary>
    public class UserDto : BaseDto
    {
        public UserDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [DisplayName("登录名")]
        /// <summary>
        /// 用户登录名
        /// </summary>
        public string UserCode { get; set; }

        [DisplayName("用户名")]
        /// <summary>
        ///  用户姓名
        /// </summary>
        public string UserName { get; set; }

        [DisplayName("登录密码")]
        /// <summary>
        ///  登录密码
        /// </summary>
        public string Password { get; set; }

        [DisplayName("用户部门")]
        /// <summary>
        ///  用户部门
        /// </summary>
        public string UserDept { get; set; }

        [DisplayName("角色")]
        /// <summary>
        ///  用户角色
        /// </summary>
        public string RoleName { get; set; }

        [DisplayName("权限等级")]
        /// <summary>
        ///  用户角色
        /// </summary>
        public int RoleLevel { get; set; }

        /// <summary>
        /// 用户所属角色ID
        /// </summary>
        public string RoleId { get; set; }

        //[DisplayName("角色")]
        ///// <summary>
        /////  用户角色
        ///// </summary>
        //public RoleDto UserRole { get; set; }
    }
}
