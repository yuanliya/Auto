using AutoCabinet2017.Helper;
using NJUST.AUTO06.Utility;
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

namespace AutoCabinet2017.UI.EF
{
    public partial class FrmDataDictInfo : Form
    {
        public FrmDataDictInfo()
        {
            InitializeComponent();
        }

        public DataDictDto dataDictDto { get; set; }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (GetInfo())
            {
                bool succeed = false;
                if (this.Tag.ToString() == "ADD")
                {
                    succeed = Add(dataDictDto);
                }
                else
                {
                    succeed = Update(dataDictDto);
                }
                  
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmDataDictInfo_Load(object sender, EventArgs e)
        {
            if (dataDictDto == null) return;

            // 查找字段名对应的中文显示名
            //txtTypeName.Text   = PropertyHelper.FieldCfgItems.Find(q => q.FieldName == dataDictDto.FieldTypeName).FieldShowName;  //dataDictDto.FieldTypeName;
            //txtTypeItem.Text   = dataDictDto.FieldTypeValue;
            //txtTypeItemNo.Text = dataDictDto.FieldTypeSerialNo.ToString();
        }

        private bool GetInfo()
        {
            if (string.IsNullOrEmpty(txtTypeName.Text) || string.IsNullOrEmpty(txtTypeItem.Text) || string.IsNullOrEmpty(txtTypeItemNo.Text))
            {
                MessageUtil.ShowWarning("信息不能为空！");
                return false;
            }

            //dataDictDto.FieldTypeName     = txtTypeName.Text;
            //dataDictDto.FieldTypeValue    = txtTypeItem.Text ;
            //dataDictDto.FieldTypeSerialNo = Convert.ToInt16(txtTypeItemNo.Text);

            return true;
        }

        /// <summary>
        /// 添加纪录
        /// </summary>
        /// <param name="dto"></param>
        private bool Add(DataDictDto dto)
        {
            //int recordCnt = PropertyHelper.DataDictItems.Count<DataDictDto>(item => item.FieldTypeName == dto.FieldTypeName
            //                                              && item.FieldTypeValue == dto.FieldTypeValue);
            //if (recordCnt > 0)
            //{
            //    MessageUtil.ShowWarning("重复的条目值，请重新输入!");

            //    return false;
            //}

            //try
            //{
            //    CallerFactory.Instance.GetService<ISystemConfigService>().Add(dto);

            //    MessageUtil.ShowTips("添加新条目成功!");
            //}
            //catch (Exception ex)
            //{
            //    MessageUtil.ShowWarning(ex.Message);
            //    return false;
            //}

            return true;
        }

        /// <summary>
        /// 更新纪录
        /// </summary>
        /// <param name="fieldInfo"></param>
        private bool Update(DataDictDto fieldInfo)
        {
            try
            {
                CallerFactory.Instance.GetService<ISystemConfigService>().UpdateDataDict(fieldInfo);

                MessageUtil.ShowTips("更新条目成功!");
                return true;
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
                return false;
            }
        }
    }
}
