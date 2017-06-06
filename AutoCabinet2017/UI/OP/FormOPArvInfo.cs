using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using DevExpress.XtraEditors.Repository;
using ZY.EntityFrameWork.Core.Model.Entity;
using DevExpress.XtraEditors;
using AutoCabinet2017.Helper;
using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.Model.Dto;
using NJUST.AUTO06.Utility;	

namespace AutoCabinet2017.UI.OP
{
    public partial class FormOPArvInfo : XtraForm
    {
        // 表格加载的数据集
        private BindingList<ArchiveInfoDto> arvInfoList = null;
        public BindingList<ArchiveInfoDto> ArvInfoList
        {
            set { arvInfoList = value; }
        }

        // 当前档案在表格中的编号
        private int rowIndex = 0;

        // 当前选中的档案
        private ArchiveInfoDto arvCurrent = null;

        // 有记录变化时更新主表格的显示
        public delegate void UpdateGViewDataListEventHandler(ArchiveInfoDto arch);
        public event UpdateGViewDataListEventHandler UpdateGViewDataListEvent;

        public FormOPArvInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 配置界面标签的属性
        /// </summary>
        private void ConfigLabelProperty(Control cn)
        {
            //// 查找标签的显示名称
            //List<ArvFieldCfg> arvField = PropertyHelper.ArvFieldCfgItems
            //                             .Where<ArvFieldCfg>(item => item.FieldName == cn.Tag.ToString())
            //                             .ToList<ArvFieldCfg>();

            //if (arvField.Count == 1)
            //{
            //    cn.Text    = arvField[0].FieldShowName;  // 显示名称
            //    cn.Enabled = arvField[0].IsFieldUsable;  // 是否可用
            //}
            cn.Enabled = true;
        }

        /// <summary>
        /// 配置文本框的属性
        /// </summary>
        /// <param name="cn"></param>
        private void ConfigTextBoxProperty(Control cn)
        {
            //// 查找文本框的显示名称
            //List<ArvFieldCfg> arvField = PropertyHelper.ArvFieldCfgItems
            //                             .Where<ArvFieldCfg>(item => item.FieldName == cn.Tag.ToString())
            //                             .ToList<ArvFieldCfg>();

            //if (arvField.Count == 1)
            //{
            //    cn.Enabled = arvField[0].IsFieldUsable;  // 字段是否可编辑
            //}
            cn.Enabled = true;
        }

        /// <summary>
        /// 配置下拉列表框的属性
        /// </summary>
        /// <param name="cn"></param>
        private void ConfigComboBoxProperty(Control cn)
        {
            // 查找ComboBox控件匹配的字段信息
            ArvFieldCfg arvField = PropertyHelper.ArvFieldCfgItems.Find(item => item.FieldName == cn.Tag.ToString());

            if (arvField != null)
            {
                // ComboBox控件是否可用
                cn.Enabled = arvField.IsFieldUsable;

                if (cn.Enabled)
                {
                    // 查找ComboBox是否对应可配置字段
                    List<FieldType> Items = PropertyHelper.FieldTypeItems.Where<FieldType>(item => item.FieldTypeName == arvField.FieldName)
                                                                        .OrderBy(item => item.FieldTypeSerialNo)
                                                                        .ToList<FieldType>();

                    // 加入配置信息到ComboBox下拉框
                    if (Items.Count != 0)
                    {
                        (cn as DevExpress.XtraEditors.ComboBoxEdit).Properties.Items.AddRange(Items);
                    }
                }
            }
        }

        /// <summary>
        /// 根据当前操作属性设置按钮属性
        /// </summary>
        /// <param name="tag"></param>
        private void ConfigButtonProperty(string tag)
        {
            switch(tag)
            {
                case "EDIT":
                    this.btnCopy.Enabled    = false;
                    this.btnSaveAdd.Enabled = false;

                    // 档案编号为只读属性，不能修改
                    txtArvID.Properties.ReadOnly = true;
                    
                    break;

                case "ADD":
                    this.btnPrev.Enabled = false;
                    this.btnNext.Enabled = false;
                    
                    break;

                default:
                    break;
            }
        }

        private void FormArvInfo_Load(object sender, EventArgs e)
        {
            // 设置按键属性
            ConfigButtonProperty(this.Tag.ToString());

            #region 设置标签、文本框和下拉框属性

            // 遍历档案字段控件
            foreach (Control cn in this.gbxArvInfo.Controls)
            {
                // 控件的Tag属性为空，表示不需要配置
                if (cn.Tag == null)
                {
                    continue;
                }

                // 设置Label控件的显示名称和是否可用
                if (cn.GetType() == typeof(Label))
                {
                    // 配置标签属性
                    ConfigLabelProperty(cn);
                }

                // 设置TextBox控件是否可用
                if (cn.GetType() == typeof(DevExpress.XtraEditors.TextEdit))
                {
                    // 配置文本框属性
                    ConfigTextBoxProperty(cn);
                }

                // 设置ComboBox控件是否可用，并设置可配置ComboBox控件的下拉明细
                if (cn.GetType() == typeof(DevExpress.XtraEditors.ComboBoxEdit))
                {
                    // 配置下拉列表框属性
                    ConfigComboBoxProperty(cn); 
                }

                // 年度信息
                if(cbxArvType.Enabled)
                {
                    int curYear = DateTime.Now.Year;
                    // 年度信息加入列表框
                    cbxArvYear.Properties.Items.Clear();
                    for (int i = 0; i < (curYear - 1949); i++)
                    {
                        cbxArvYear.Properties.Items.Add(Convert.ToString(curYear - i));
                    }
                }
            }
           
            #endregion

            if (this.Tag.ToString() == "EDIT")
            {
                // 当前选中的档案信息
                arvCurrent = arvInfoList[0];
                // 加载档案信息
                LoadArvInfo(arvCurrent);
                // 档案编号为只读属性，不能修改
                txtArvID.Properties.ReadOnly = true;
            }
        }

       
        #region 按键操作

        /// <summary>
        /// 检验输入
        /// </summary>
        private void ValidateInput()
        {
            if (string.IsNullOrEmpty(txtArvID.Text))
            {
                throw new Exception("档案编号不能为空！");
            }
        }

        /// <summary>
        /// 判断当前档案是否改变
        /// </summary>
        /// <returns></returns>
        private bool CheckChanged()
        {
            // 建立档案信息类对象
            ArchiveInfoDto arch = new ArchiveInfoDto();
            // 初始化对象
            InitObject(arch);
            foreach (PropertyInfo info in arch.GetType().GetProperties())
            {
                if (info.Name != "ArvStatus")
                {
                    if (info.GetValue(arch, null)!=null&&String.IsNullOrEmpty(info.GetValue(arch, null).ToString()))
                    {
                        if (info.GetValue(arvCurrent, null) == null || info.GetValue(arvCurrent, null).ToString() == "")
                        {
                            continue;
                        }
                        else
                            return true;
                    }
                    else if (info.GetValue(arvCurrent, null) == null)
                    {
                        return true;
                    }
                    else
                    {
                        if (info.GetValue(arch, null).ToString() != info.GetValue(arvCurrent, null).ToString())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 初始化记录对象
        /// </summary>
        /// <param name="arch"></param>
        private void InitObject(ArchiveInfoDto arch)
        {
            arch.ArvID    = txtArvID.Text.Trim();
            arch.ArvTitle = txtArvTitle.Text.Trim();
            arch.ArvBoxID = txtArvBoxID.Text.Trim();
            arch.ArvBoxTitle = txtArvBoxTitle.Text.Trim();
            arch.ArvType = cbxArvType.Text.Trim();
            arch.ArvUnit = cbxArvUnit.Text.Trim();
            arch.ArvYear = cbxArvYear.Text.Trim();
            arch.LabelID = txtLabelID.Text.Trim();
            arch.Edoc = txtEDoc.Text.Trim();
            arch.Rsv2 = cbxRsv2.Text.Trim();
            arch.Rsv3 = txtRsv3.Text.Trim();
            arch.Rsv4 = cbxRsv4.Text.Trim();
            arch.Rsv5=txtRsv5.Text.Trim();
        }

        /// <summary>
        /// 更新录入界面的表格显示
        /// </summary>
        /// <param name="arch"></param>
        private void UpdateGridViewShow(ArchiveInfoDto arch)
        {
            if (UpdateGViewDataListEvent != null)
            {
                UpdateGViewDataListEvent(arch);
            }
        }

        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="arch"></param>
        private void AddNew(ArchiveInfoDto arch)
        {
            try
            {
                int succeed=CallerFactory.Instance.GetService<IArvOpService>().InputToLib(arch);
                if(succeed==1)
                {
                    // 档案信息加入表格关联的数据集
                    UpdateGridViewShow(arch);

                    MessageUtil.ShowTips("添加成功！"); 
                }
                else
                    MessageUtil.ShowTips("添加失败！"); 
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="arch"></param>
        private void Update(ArchiveInfoDto arch)
        {
            try
            {
                // 修改档案
                CallerFactory.Instance.GetService<IArvOpService>().UpdateArvInfo(arch);
                // 更新档案信息到表格关联的数据集 
                UpdateGridViewShow(arch);

                MessageUtil.ShowTips("修改成功！");
            }
            catch (Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);
            }
        }

        /// <summary>
        /// 保存档案信息
        /// </summary>
        private void Save()
        {
            // 新增
            if (this.Tag.ToString() == "ADD")
            {
                // 建立档案信息类对象
                ArchiveInfoDto arch = new ArchiveInfoDto();
                // 初始化对象
                InitObject(arch);
                arch.CreateTime = System.DateTime.Now;
                arch.ArvStatus = "在库";
                AddNew(arch);
            }

            // 修改
            if (this.Tag.ToString() == "EDIT")
            {
                InitObject(arvCurrent);
                //arvCurrent.AuditTime = System.DateTime.Now;
                Update(arvCurrent);
            }
        }

        /// <summary>
        /// 保存关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            // 新增或编辑成功，关闭对话框
            try
            {
                // 输入效验
                ValidateInput();
                // 保存记录
                Save();
                // 关闭窗口
                this.Close();
            }
            catch(Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 保存新增
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveAdd_Click(object sender, EventArgs e)
        {
            // 新增或编辑成功
            try
            {
                // 输入效验
                ValidateInput();
                // 保存记录
                Save();
                // 清除当前信息，继续编辑
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 保存复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCopy_Click(object sender, EventArgs e)
        {
            // 新增或编辑成功
            try
            {
                // 输入效验
                ValidateInput();
                // 保存记录
                Save();
                // 清空档案编号和名称，继续编辑
                txtArvID.Text    = "";
                txtArvTitle.Text = "";
            }
            catch (Exception ex)
            {
                MessageUtil.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 上一记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CheckChanged()) 
            {
                DialogResult result = MessageUtil.ShowYesNoAndTips("是否保存对当前档案的修改？");
                if (result == DialogResult.Yes)
                {
                    Save();
                }
            }
            if (rowIndex == 0)
            {
                MessageUtil.ShowWarning("已经到达第一条记录！");
                return;
            }

            rowIndex--;

            // 得到当前档案信息
            arvCurrent = arvInfoList[rowIndex];
            // 加载档案信息
            LoadArvInfo(arvCurrent);    
        }

        /// <summary>
        /// 下一记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CheckChanged())
            {
                DialogResult result = MessageUtil.ShowYesNoAndTips("是否保存对当前档案的修改？");
                if (result == DialogResult.Yes)
                {
                    Save();
                }
            }
            if (rowIndex == arvInfoList.Count() - 1)
            {
                MessageUtil.ShowWarning("已经到达最后一条记录！");
                return;
            }

            rowIndex++;

            // 得到当前档案信息
            arvCurrent = arvInfoList[rowIndex];
            // 加载档案信息
            LoadArvInfo(arvCurrent);        
        }

        #endregion

        /// <summary>
        /// 清除窗口中预设信息
        /// </summary>
        void ClearForm()
        {
            txtArvID.Text    = "";
            txtArvTitle.Text = "";
            txtArvBoxID.Text = "";
            txtArvBoxTitle.Text = "";
            //cbxArvType.Text = "";
            cbxArvType.SelectedIndex = -1;
            //cbxArvUnit.Text = "";
            cbxArvUnit.SelectedIndex = -1;
            txtLabelID.Text = "";
            //cbxArvYear.Text = "";
            cbxArvYear.SelectedIndex = -1;
        }

        /// <summary>
        /// 显示选中的档案信息
        /// </summary>
        /// <param name="arvCurrent"></param>
        void LoadArvInfo(ArchiveInfoDto arvCurrent)
        {
            txtArvID.Text       = arvCurrent.ArvID;
            txtArvTitle.Text    = arvCurrent.ArvTitle;
            txtArvBoxID.Text    = arvCurrent.ArvBoxID;
            txtArvBoxTitle.Text = arvCurrent.ArvBoxTitle;
            cbxArvType.Text     = arvCurrent.ArvType;
            cbxArvUnit.Text     = arvCurrent.ArvUnit;
            txtLabelID.Text     = arvCurrent.LabelID;
            txtEDoc.Text = arvCurrent.Edoc;
            cbxArvYear.Text = arvCurrent.ArvYear;
        }

        /// <summary>
        /// 电子文档文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrower_Click(object sender, EventArgs e)
        {
            //txtEDoc.Text=FileDialogHelper.Instance.OpenFile();
        }
    }
}
