using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZY.EntityFrameWork.Core.Model.Attibutes
{
    /// <summary>
    /// 定义权限.
    /// </summary>
    [Flags]
    public enum PermissionValue
    {
        /// <summary>
        /// The add.
        /// </summary>
        [Description("新增")]
        Add = 1,

        /// <summary>
        /// The edit.
        /// </summary>
        [Description("编辑")]
        Edit = 2,

        /// <summary>
        /// The delete.
        /// </summary>
        [Description("删除")]
        Delete = 4,

        /// <summary>
        /// The detail.
        /// </summary>
        [Description("控制")]
        Control = 8,

        /// <summary>
        /// The all.
        /// </summary>
        [Description("全部")]
        All = Add | Edit | Delete | Control ,

        /// <summary>
        /// The none.
        /// </summary>
        [Description(" ")]
        None = 0
    }
}
