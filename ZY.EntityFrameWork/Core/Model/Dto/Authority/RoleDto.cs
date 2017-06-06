using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的角色DTO实体
    /// </summary>
    public class RoleDto : BaseDto
    {
        public RoleDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [DisplayName("角色名")]
        public String RoleName { get; set; }

        [DisplayName("角色级别")]
        public int Level { get; set; }

        [DisplayName("描述")]
        public String Description { get; set; }
    }
}
