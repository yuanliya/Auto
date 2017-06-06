using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NJUST.AUTO06.Utility;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.Model.Dto;

namespace AutoCabinet2017.UI.EF
{
    public partial class FormEFRole : Form
    {
        public FormEFRole()
        {
            InitializeComponent();
            dto = new RoleDto();
        }

        public RoleDto dto { get; set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRole.Text) || string.IsNullOrEmpty(cbxRoleLevel.Text))
            {
                MessageUtil.ShowWarning("角色名和角色等级不能为空！");
                return;
            }

           dto.Description = txtDescription.Text;
           dto.RoleName    = txtRole.Text;
           dto.Level       = Convert.ToInt32(cbxRoleLevel.Text);

            if (this.Tag != null)
            {
                try
                {
                    if (this.Tag.ToString() == "Add")
                        CallerFactory.Instance.GetService<IAuthorityService>().AddRole(dto);
                    else if (this.Tag.ToString() == "Edit")
                        CallerFactory.Instance.GetService<IAuthorityService>().UpdateRole(dto);
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageUtil.ShowError(ex.Message);
                }              
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void FormEFRole_Load(object sender, EventArgs e)
        {
            if(this.Tag!=null&&this.Tag.ToString()=="Edit"&&this.dto!=null)
            {
                txtDescription.Text = dto.Description;
                txtRole.Text        = dto.RoleName;
                cbxRoleLevel.Text   = dto.Level.ToString();
            }
        }
    }
}
;