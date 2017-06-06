using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Dto
{
    /// <summary>
    /// 面向应用层的模块DTO实体
    /// </summary>
    public class ModuleDto : BaseDto
    {
        public ModuleDto()
        {
            // 每个实体生成独一无二的ID
            ID = Guid.NewGuid().ToString("N");
        }

        [DisplayName("模块名")]
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 上级模块ID
        /// </summary>
        public string ParentID { get; set; }

        /// <summary>
        /// 使用权限
        /// </summary>
        public bool Enabled { get; set; }

        [DisplayName("权限")]
        /// <summary>
        /// 权限
        /// </summary>
        public int Permissions { get; set; }

        [DisplayName("标识")]
        /// <summary>
        /// 模块标记
        /// </summary>
        public string ModuleTag { get; set; }
    }
}
