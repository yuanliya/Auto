using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.Model.Dto;

using AutoCabinet2017.Helper;

using NJUST.AUTO06.Utility;

namespace AutoCabinet2017.UI.OP
{
    public partial class FormOPArvReturn : Form
    {
        // 查询表格关联的数据集
        private BindingList<ArchiveInfoDto> arvList = new BindingList<ArchiveInfoDto>();
        // 归还表格关联的数据集
        private BindingList<ArchiveInfoDto> arvToReturnList = new BindingList<ArchiveInfoDto>();

        public FormOPArvReturn()
        {
            InitializeComponent();
        }

        private void FormOPArvReturn_Load(object sender, EventArgs e)
        {
            if (PropertyHelper.CurrentUser.RoleName != "超级管理员")
            {
                RoleModuleDto dto = PropertyHelper.RoleModules.Find(q => q.ModuleTag.ToString() == "106");
                if (dto != null)
                {
                    bar1.Authorize(dto.Permissions);
                }
            }

            // 默认隐藏“高级搜索”
            panelSearch.Visible = false;

            try
            {
                gcArvInfo.DataSource = CallerFactory.Instance.GetService<IArvOpService>().GetLendInfo();
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 工具栏操作--高级检索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolAdvancedQuery_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (panelSearch.Visible)
            {
                panelSearch.Visible = false;
            }
            else
            {
                panelSearch.Visible = true;
            }              
        }

        private ArvReturnInfoDto InitReturnInfo()
        {
            ArvReturnInfoDto dto = new ArvReturnInfoDto
            {
                ReturnDate = dtReturnDate.DateTime,
                Returner = txtReturner.Text,
                ReturnerDept = cbxUnit.Text,
                ReturnExecuter = txtAdmin.Text
            };
            
            return dto;
        }

        private void toolReturn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CallerFactory.Instance.GetService<IArvOpService>().ArvReturn(InitReturnInfo(), gcArvInfo.DataSource as List<ArvLendInfoDto>);
        }
    }
}
