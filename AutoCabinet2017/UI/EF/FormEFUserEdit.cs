using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using DevExpress.XtraEditors.Controls;

using NJUST.AUTO06.Utility;

using AutoCabinet2017.Helper;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.DBHelper;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;

namespace AutoCabinet2017.UI.EF
{
    public partial class FormEFUserEdit : Form
    {
        // 待编辑的设备实体
        // 必须采用表格数据源中既有实体对象来赋值，因为EF对该实体有跟踪；
        // 否则，EF会认为是新的实体，更新会失败！
        public UserDto EditedUserDto { set; private get; }

        public FormEFUserEdit()
        {
            InitializeComponent();
        }

        // 更新UI界面的表格
        public delegate void UpdateViewEventHandler(UserDto deviceDto);
        public event UpdateViewEventHandler OnUpdateView;

        /// <summary>
        /// 通知UI界面更新表格
        /// </summary>
        /// <param name="deviceDto">UI界面表格显示的实体信息</param>
        private void RaiseOnUpdateView(UserDto userDto)
        {
            if (OnUpdateView == null) return;

            OnUpdateView(userDto);
        }

        private void FormEFUserEdit_Load(object sender, EventArgs e)
        {
            #region 设置复选框

            // 设置选项只能选择不能编辑
            cbxUserDept.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;

            // 加入复选框明细
            cbxUserDept.Properties.Items.Clear();

            try
            {
                // 部门分类信息-控件扩展方法
                cbxUserDept.AddItems("ArvUnit");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }

            // 设置角色下拉框

            // 设置选项只能选择不能编辑
            cbxUserRole.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
            // 清空下拉框
            cbxUserRole.Properties.Items.Clear();

            // 根据登录用户的权限加载角色信息
            switch (PropertyHelper.CurrentUser.RoleName)
            {
                case "超级管理员":   // 可以添加"普通用户", "管理员", "系统管理员"
                    // 设置角色复选框
                    cbxUserRole.Properties.Items.AddRange(new string[] { "普通用户", "管理员", "系统管理员" });

                    break;

                case "系统管理员":   // 可以添加"普通用户", "管理员"
                    // 设置角色复选框
                    cbxUserRole.Properties.Items.AddRange(new string[] { "普通用户", "管理员" });

                    break;

                case "管理员":       // 可以添加"普通用户"
                    // 设置角色复选框
                    cbxUserRole.Properties.Items.Add("普通用户");

                    break;

                default:
                    break;
            }

            #endregion

            if(this.Tag.ToString() == "EDIT")
            {
                txtUserCode.Text = EditedUserDto.UserCode;
                txtUserName.Text = EditedUserDto.UserName;
                txtPassword.Text = EditedUserDto.Password;
                cbxUserDept.Text = EditedUserDto.UserDept;
                cbxUserRole.Text = EditedUserDto.RoleName;

                txtUserCode.Enabled   = false;
                txtPassword.Enabled   = false;
                txtConfirmPwd.Enabled = false;

                cbxDisplayPwd.Visible = false;
            }
        }

        /// <summary>
        /// 新增记录
        /// </summary>
        private void Add()
        {
            if(txtPassword.Text != txtConfirmPwd.Text)
            {
                MessageUtil.ShowWarning("两次输入的登录密码不一致!");
                return;
            }

            // 设备类实体信息
            UserDto userDto = new UserDto();

            userDto.UserCode = txtUserCode.Text;
            userDto.UserName = txtUserName.Text;
            userDto.Password = txtPassword.Text;
            userDto.UserDept = cbxUserDept.Text;
            userDto.RoleName = cbxUserRole.Text;

            try
            {
                // 添加用户
                int succeed = CallerFactory.Instance.GetService<IAuthorityService>().Add(userDto);
                if (succeed != 0)
                {
                    MessageUtil.ShowTips("添加用户信息成功！");

                    // 更新主界面的表格
                    RaiseOnUpdateView(userDto);
                }
                else
                {
                    MessageUtil.ShowTips("添加用户信息失败！");
                }
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        private void Edit()
        {
            // 用户实体信息
            EditedUserDto.UserName = txtUserName.Text;
            EditedUserDto.UserDept = cbxUserDept.Text;
            EditedUserDto.RoleName = cbxUserRole.Text;

            try
            {
                // 编辑用户
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
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 保存操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserCode.Text) || string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(cbxUserRole.Text))
            {
                MessageUtil.ShowWarning("登录名、用户名和角色信息不能为空！");
                return;
            }

            switch(this.Tag.ToString())
            {
                case "ADD":
                    Add();

                    break;

                case "EDIT":
                    Edit();

                    break;

                default:
                    break;
            }
        }

        #region 键盘输入文本的校验

        /// <summary>
        /// 输入效验--只能输入数字和字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardInputHelper.Instance.InputNumericAndChar(e, sender as Control);
        }

        /// <summary>
        /// 输入效验--只能输入数字和字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardInputHelper.Instance.InputNumericAndChar(e, sender as Control);
        }

        /// <summary>
        /// 输入效验--只能输入数字和字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
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
        private void cbxDisplayPwd_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxDisplayPwd.Checked == true)
            {
                txtPassword.Properties.PasswordChar   = '\0';
                txtConfirmPwd.Properties.PasswordChar = '\0';
            }
            else
            {
                txtPassword.Properties.PasswordChar   = '*';
                txtConfirmPwd.Properties.PasswordChar = '*';
            }
        }
    }
}
