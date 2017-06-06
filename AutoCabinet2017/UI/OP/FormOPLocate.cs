using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using AutoCabinet2017.Helper;
using ZY.EntityFrameWork.Core.Model.Dto;
using NJUST.AUTO06.Utility;


namespace AutoCabinet2017.UI.OP
{
    public partial class FormOPLocate : Form
    {
        public FormOPLocate()
        {
            InitializeComponent();
        }

        ArvLocationDto location = null;
        public ArvLocationDto ArchiveLocation
        {
            get { return location; }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbxGroupNo.Text) | string.IsNullOrEmpty(cbxLayerNo.Text))
            {
                MessageUtil.ShowWarning("回转库编号和层号不能为空！");
                return;
            }
            //else
            //{
            //    this.btnOK.DialogResult = DialogResult.OK;
            //}

            location = new ArvLocationDto();
            location.GroupNo = cbxGroupNo.Text;
            location.LayerNo = cbxLayerNo.Text;
            //location.CellNo = cbxCellNo.Text;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FormOPLocate_Load(object sender, EventArgs e)
        {
            this.btnCancel.DialogResult = DialogResult.Cancel;

            // 查询档案柜分类
            //List<CabinetDto> listCabinet = CabinetBLL.Instance.Find("*", "");
            //for (int i = 0; i < listCabinet.Count; i++)
            //{
            //    cbxGroupNo.Items.Add(listCabinet[i].CabinetNo);
            //}

        }

        private void cbxGroupNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = 0;

            if (!string.IsNullOrEmpty(cbxGroupNo.Text))
            {
                cbxLayerNo.Enabled = true;

                // 查询该编号回转库的层数
                string condition = string.Format("CabinetNo= {0}", cbxGroupNo.Text);
                //List<CabinetDto> cab = CabinetBLL.Instance.Find("*", condition);

                // 添加回转库层数
                cbxLayerNo.Items.Clear();
                //for (i = 1; i <= cab[0].CabinetLayers; i++)
                //{
                //    cbxLayerNo.Items.Add(i);
                //}
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
