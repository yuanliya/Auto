using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的角色-模块DTO实体
    /// </summary>
    public class RoleModuleDto : BaseDto
    {
        public RoleModuleDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [DisplayName("角色ID")]
        /// <summary>
        /// 角色ID
        /// </summary>    
        public string RoleId { get; set; }

        [DisplayName("角色名")] 
        /// <summary>
        /// 角色名
        /// </summary>       
        public string RoleName { get; set; }

        [DisplayName("模块ID")]
        /// <summary>
        /// 模块ID
        /// </summary>    
        public string ModuleId { get; set; }

        [DisplayName("模块ID")]
        /// <summary>
        /// 模块ID
        /// </summary>    
        public string ModuleTag { get; set; }

        [DisplayName("模块名")]
        /// <summary>
        /// 模块名称
        /// </summary>     
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块是否对该角色开放
        /// </summary>
        public bool Enabled { get; set; }

        [DisplayName("权限")]
        /// <summary>
        /// 权限
        /// </summary>
        public int Permissions { get; set; }
    }
}
