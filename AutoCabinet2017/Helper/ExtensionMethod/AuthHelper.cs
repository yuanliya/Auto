using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZY.EntityFrameWork.Core.Model.Attibutes;

namespace AutoCabinet2017.Helper
{
    /// <summary>
    /// 用户界面操作权限的扩展方法
    /// </summary>
    public static  class AuthHelper
    {
        /// <summary>
        /// 权限验证
        /// </summary>
        /// <param name="toVerification">需要验证的权限</param>
        /// <param name="functionInRole">已经存在的权限</param>
        /// <returns></returns>
        public static bool VerifyPermission(int toVerification,  PermissionValue functionInRole)
        {
            return ((PermissionValue)toVerification & functionInRole) != 0;
        }

        /// <summary>
        /// 工具栏的权限认证扩展方法
        /// </summary>
        /// <param name="bar">工具栏实体</param>
        /// <param name="value">位表示的权限值</param>
        public static void Authorize(this Bar bar, int value)
        {
            // 拥有全部权限
            if ((PermissionValue)value == PermissionValue.All)
            {
                return;
            }

            // 遍历工具栏，根据权限确定Button是否可见
            foreach(BarItemLink ctr in bar.ItemLinks)
            {
                ctr.Item.Visibility = IsVisible(ctr.Item.Tag, value) ? BarItemVisibility.Always : BarItemVisibility.Never;
            }
        }

        /// <summary>
        /// Panel面板的权限认证扩展方法
        /// </summary>
        /// <param name="panel">面板实体</param>
        /// <param name="value">位表示的权限值</param>
        public static void Authorize(this Panel panel, int value)
        {
            // 拥有全部权限
            if ((PermissionValue)value == PermissionValue.All)
            {
                return;
            }

            // 遍历面板，根据权限确定Button是否可见
            foreach (Control ctr in panel.Controls)
            {
                ctr.Visible = IsVisible(ctr.Tag, value);
            }
        }

        /// <summary>
        /// 表格中嵌入控件的权限认证扩展方法
        /// </summary>
        /// <param name="panel">嵌入控件实体</param>
        /// <param name="value">位表示的权限值</param>
        public static void Authorize(this RepositoryItemButtonEdit item, int value)
        {
            // 拥有全部权限
            if ((PermissionValue)value == PermissionValue.All)
            {
                return;
            }

            // 遍历嵌入式控件集，根据权限确定Button是否可见
            foreach (EditorButton ctr in item.Buttons)
            {
                ctr.Visible = IsVisible(ctr.Tag, value);
            }
        }

        /// <summary>
        /// 根据控件的Tag属性确定使用权限（是否可见）
        /// </summary>
        /// <param name="tag">控件的Tag属性</param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool IsVisible(object tag, int value)
        {
            if (tag == null) return true;

            bool result = true;
            
            switch (tag.ToString())
            {
                case "Add":     //“Add”类型的控件
                    result = VerifyPermission(value, PermissionValue.Add);
                    break;
                case "Delete":  //“Delete”类型的控件
                    result = VerifyPermission(value, PermissionValue.Delete);
                    break;
                case "Control": //“Control”类型的控件
                    result = VerifyPermission(value, PermissionValue.Control);
                    break;
                case "Edit":    //“Edit”类型的控件
                    result = VerifyPermission(value, PermissionValue.Edit);
                    break;
                default:        // 其他类型的控件默认可见
                    result= true;
                    break;
            }

            return result;
        }
    }
}
