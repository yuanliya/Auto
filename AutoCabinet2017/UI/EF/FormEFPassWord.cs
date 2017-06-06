using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using DevExpress.XtraEditors.Controls;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.Model.Dto;

using NJUST.AUTO06.Utility;

namespace AutoCabinet2017.UI.EF
{
    public partial class FormEFPassWord : Form
    {
        // 待编辑的设备实体
        // 必须采用表格数据源中既有实体对象来赋值，因为EF对该实体有跟踪；
        // 否则，EF会认为是新的实体，更新会失败！
        public UserDto EditedUserDto { set; private get; }

        // 更新UI界面的表格
        public delegate void UpdateViewEventHandler(UserDto deviceDto);
        public event UpdateViewEventHandler OnUpdateView;

        public FormEFPassWord()
        {
            InitializeComponent();
        }

        private void FormSYPassWord_Load(object sender, EventArgs e)
        {
            // 用户名不能编辑
            txtUserCode.Enabled = false;
            // 显示用户登录名
            txtUserCode.Text = EditedUserDto.UserCode;
        }

        /// <summary>
        /// 通知UI界面更新表格
        /// </summary>
        /// <param name="deviceDto">UI界面表格显示的实体信息</param>
        private void RaiseOnUpdateView(UserDto userDto)
        {
            if (OnUpdateView == null) return;

            OnUpdateView(userDto);
        }

        /// <summary>
        /// 保存按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtOldPwd.Text))
            {
                MessageUtil.ShowTips("原密码不许为空！");
                txtOldPwd.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtNewPwd.Text))
            {
                MessageUtil.ShowTips("新密码不许为空！");
                txtNewPwd.Focus();
                return;
            }

            if (String.IsNullOrEmpty(txtConfirmPwd.Text))
            {
                MessageUtil.ShowTips("确认密码不许为空！");
                txtConfirmPwd.Focus();
                return;
            }

            if (txtNewPwd.Text != txtConfirmPwd.Text)
            {
                MessageUtil.ShowTips("两次输入的密码不相同！");
                txtNewPwd.Focus();
                return;
            }

            // 保存新密码
            EditedUserDto.Password = txtConfirmPwd.Text;

            try 
            {
                int succeed = CallerFactory.Instance.GetService<IAuthorityService>().Update(EditedUserDto);
                if (succeed != 0)
                {
                    MessageUtil.ShowTips("更新用户信息成功！");

                    // 更新主界面的表格
                    RaiseOnUpdateView(EditedUserDto);
                    // 关闭对话框
                    this.Close();
                }
                else
                {
                    MessageUtil.ShowTips("更新用户信息失败！");
                }
            }
            catch(Exception ex) 
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        #region 键盘输入文本的校验

        /// <summary>
        /// 输入效验--只能输入数字和字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtOldPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardInputHelper.Instance.InputNumericAndChar(e, sender as Control);
        }

        /// <summary>
        /// 输入效验--只能输入数字和字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNewPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardInputHelper.Instance.InputNumericAndChar(e, sender as Control);
        }

        /// <summary>
        /// 输入效验--只能输入数字和字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtConfirmPwd_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardInputHelper.Instance.InputNumericAndChar(e, sender as Control);
        }

        #endregion

        /// <summary>
        /// 是否显示密码内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxDisplayPwd_CheckStateChanged(object sender, EventArgs e)
        {
            if(cbxDisplayPwd.Checked == true)
            {
                txtOldPwd.Properties.PasswordChar     = '\0';
                txtNewPwd.Properties.PasswordChar     = '\0';
                txtConfirmPwd.Properties.PasswordChar = '\0';
            }
            else
            {
                txtOldPwd.Properties.PasswordChar     = '*';
                txtNewPwd.Properties.PasswordChar     = '*';
                txtConfirmPwd.Properties.PasswordChar = '*';
            }
        }
    }
}
