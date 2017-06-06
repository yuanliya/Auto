using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;
using System.Xml.Linq;

using NJUST.AUTO06.Utility;
using AutoCabinet2017.Helper;

using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using NJUST.AUTO06.Utility.XmlUtil;

namespace AutoCabinet2017.UI.APP
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        // 主窗口
        private FormAppMain appMain = new FormAppMain();

        /// <summary>
        /// 显示主界面
        /// </summary>
        private void ShowMainForm()
        {
            // 如果主窗体已打开，则重新打开
            if (CheckFormIsOpen("FormAppMain"))
            {
                this.DialogResult = DialogResult.OK;
                // 关闭登录窗口
                this.Hide();

                return;
            }

            // 设置窗体的返回值
            this.DialogResult = DialogResult.OK;
            // 关闭登录窗口
            this.Hide();

            // 显示主界面
            appMain.Show();
        }

        /// <summary>
        /// 取消”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        /// <summary>
        /// “确定”
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            txtUser.Text     = "NJUST";
            txtPassword.Text = "NJUST123";

            // 登录验证
            try
            {
                // 验证用户名和密码
                DoCheckUser(txtUser.Text, txtPassword.Text);
                PropertyHelper.Modules = CallerFactory.Instance.GetService<IAuthorityService>().GetAllModules().OrderBy(q=>q.ModuleTag).ToList();
                // 更新当前用户的权限信息到属性类
                UpdateUserRightInfo();

                // 从数据库中读取档案字段配置信息
                PropertyHelper.FieldCfgItems = CallerFactory.Instance.GetService<ISystemConfigService>().GetAllFieldCfgs().OrderBy(q => q.SerialNo).ToList();
                // 数据库错误或没有字段配置信息，则从配置文件中读取缺省值
                if (PropertyHelper.FieldCfgItems.Count == 0)
                {
                    // 设置字段配置的缺省值
                    SetDefaultFieldInfoCfg();
                }

                // 更新分类信息到属性类
                PropertyHelper.DataDictItems = CallerFactory.Instance.GetService<ISystemConfigService>().GetAllDataDicts();

                // 显示主界面
                ShowMainForm();
            }
            catch (FormatException ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }            
        }

        /// <summary>
        /// 用户名和密码校验
        /// </summary>
        /// <param name="userLogin">用户名</param>
        /// <param name="pwd">密码</param>
        private void DoCheckUser(string userLogin, string pwd)
        {
            // 检验输入有效性
            ValidateInput(userLogin, pwd);

            // 验证用户名和密码
            UserDto user = CheckUser(userLogin, pwd);

            // 记录日志
            LogHelper.SetUserProperty(user.UserCode);
            LogHelper.LogUserInfo("登录");

            // 保存当前登录用户信息到属性类
            PropertyHelper.CurrentUser = user;
         }

        #region 用户登录的验证和操作

        /// <summary>
        /// 判断窗口是否已经打开
        /// </summary>
        /// <param name="asFormName"></param>
        /// <returns></returns>
        private bool CheckFormIsOpen(string asFormName)
        {
            bool bResult = false;

            // 遍历当前打开的窗体Form
            foreach (Form frm in Application.OpenForms)
            {
                if (frm.Name == asFormName)
                {
                    bResult = true;
                    break;
                }
            }

            return bResult;
        }

        /// <summary>
        /// 数据输入有效性验证
        /// </summary>
        /// <param name="userCode">用户名</param>
        /// <param name="pwd">密码</param>
        private void ValidateInput(string userCode, string pwd)
        {
            // 数据录入有效性检验
            if (string.IsNullOrEmpty(userCode) || string.IsNullOrEmpty(pwd))
            {
                // 焦点设置到输入文本框
                txtUser.Focus();

                throw new Exception("用户名和密码必须录入！");
            }

            // 字符有效性验证
            if (!KeyboardInputHelper.Instance.CheckPasswordFormat(pwd))
            {
                // 焦点设置到输入文本框
                txtUser.Focus();

                throw new Exception("密码含有非法字符！");
            }
        }

        /// <summary>
        /// 用户登录验证
        /// </summary>
        /// <param name="user">用户登录名</param>
        /// <param name="pwd">密码</param>
        private UserDto CheckUser(string userCode, string pwd)
        {
            // 记录用户信息
            UserDto user = null;

            // 超级管理员
            if ((userCode == "NJUST") && (pwd == "NJUST123"))
            {
                // 超级用户
                user = new UserDto();

                user.UserCode = "NJUST";
                user.UserName = "南京理工大学";
                user.UserDept = "自动化学院";
                user.Password = "NJUST123";
                user.RoleName = "超级管理员";
                user.RoleLevel = 0;

                return user;
            }

            // 用户名和密码效验   
            user = CallerFactory.Instance.GetService<IAuthorityService>().CheckUser(userCode, pwd);
            if(user == null)
            {
                txtPassword.Text = "";
                txtUser.Text = "";

                txtUser.Focus();

                throw new Exception("登录失败, 用户名与密码不符合！");
            }

            return user;
        }

        #endregion

        #region 更新系统的相关配置信息

        /// <summary>
        /// 更新用户权限信息
        /// </summary>
        private void UpdateUserRightInfo()
        {
            // 超级管理员拥有全部权限
            if (PropertyHelper.CurrentUser.RoleName == "超级管理员") return;
            
            // 查询当前用户的权限信息，保存进属性
            PropertyHelper.RoleModules = CallerFactory.Instance.GetService<IAuthorityService>().GetRolePermissions(PropertyHelper.CurrentUser.RoleName);
        }

        /// <summary>
        /// 设置缺省的字段配置信息
        /// </summary>
        private void SetDefaultFieldInfoCfg()
        {
            // 利用Linq to XML从XNL文件中读取字段配置信息
            string path = Application.StartupPath + "\\XML\\FieldToShowConfig.xml";
            XElement root = XElement.Load(path);

            // 读取XML树中所有的“Field”属性及其子元素的值
            var fieldList = from ele in root.Elements("Field") select ele;

            List<FieldCfgDto> items=new List<FieldCfgDto>();
            // 写入数据库
            foreach (var item in fieldList)
            {
                // 提取字段信息
                string[] arr = item.Value.Split(',');
                FieldCfgDto dictItem = new FieldCfgDto();

                dictItem.FieldName      = item.Attribute("Name").Value.ToString(); // 字段名（对应数据库）
                dictItem.FieldShowName  = arr[0];                                  // 字段UI显示名
                dictItem.IsFieldVisible = arr[2] == "1" ? true : false;            // 是否显示  
                dictItem.IsFieldUsable  = arr[1] == "1" ? true : false;            // 是否能用
                dictItem.IsKeyWord      = arr[3] == "1" ? true : false;            // 是否主键

                // 将档案字段映射信息保存进属性类
                //PropertyHelper.DataDictItems.Add(arvItem);
                // 同步写进数据库
                //ArvFieldCfgBLL.Instance.Insert(arvItem);
                items.Add(dictItem);
            }
            try
            {
                CallerFactory.Instance.GetService<ISystemConfigService>().Add(items);
                PropertyHelper.FieldCfgItems.AddRange(items);
            }
            catch (Exception)
            {
                MessageUtil.ShowError("添加字典项目失败！");
            }
        }

        #endregion

        #region 键盘输入处理

        /// <summary>
        /// 密码输入框的回车键操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        /// <summary>
        /// 用户输入框的回车键操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        /// <summary>
        /// 用户名输入效验--只能输入数字和字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUser_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardInputHelper.Instance.InputNumericAndChar(e, sender as Control);
        }

        /// <summary>
        /// 密码输入效验--只能输入数字和字母
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            KeyboardInputHelper.Instance.InputNumericAndChar(e, sender as Control);
        }

        #endregion

    }
}
