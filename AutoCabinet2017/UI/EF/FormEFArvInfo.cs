using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

using DevExpress.XtraLayout;	
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors;

using AutoCabinet2017.Helper;
using NJUST.AUTO06.Utility;

using ZY.EntityFrameWork.Caller;
using ZY.EntityFrameWork.Caller.Facade;
using ZY.EntityFrameWork.Core.Model.Dto;
using ZY.EntityFrameWork.Core.Model.Entity;
using ZY.EntityFrameWork.Core.DBHelper;


namespace AutoCabinet2017.UI.EF
{
    public partial class FormArvInfo : XtraForm
    {
        public FormArvInfo()
        {
            InitializeComponent();
        }

        // 表格加载的数据集
        private BindingList<ArchiveInfoDto> arvInfoList = null;
        public BindingList<ArchiveInfoDto> ArvInfoList
        {
            get { return arvInfoList; }
            set { arvInfoList = value; }
        }

        // 当前档案在表格中的编号
        private int rowIndex = 0;
        // 当前选中的档案
        private ArchiveInfoDto arvCurrent = null;

        private List<DeviceDto> devices;

        // 有记录变化时更新主表格的显示
        public delegate void UpdateGViewDataListEventHandler(ArchiveInfoDto arch);
        public event UpdateGViewDataListEventHandler UpdateGViewDataListEvent;

        #region 配置界面属性

        public void GetArvBoxs()
        {
            List<ArvBoxDto> dtos = CallerFactory.Instance.GetService<IArvOpService>().FindArvBoxes(null);

            // txtArvBoxID为SearchLookUpEdit控件，绑定数据源
            //txtArvBoxID.Properties.DataSource    = dtos;
            //txtArvBoxID.Properties.DisplayMember = "ArvBoxID";
            //txtArvBoxID.Properties.ValueMember   = "ArvBoxID";
        }

        /// <summary>
        /// 配置界面标签的属性
        /// </summary>
        private void ConfigLabelProperty(LayoutControlItem cn)
        {
            // 查找标签的显示名称
            FieldCfgDto arvField = PropertyHelper.FieldCfgItems.Find(item => item.FieldName == cn.Text);

            if (arvField!=null)
            {
                cn.Text = arvField.FieldShowName;     // 显示名称
                cn.Enabled = arvField.IsFieldUsable;  // 是否可用
            }
            //cn.Enabled = true;
        }

        /// <summary>
        /// 配置文本框的属性
        /// </summary>
        /// <param name="cn"></param>
        private void ConfigTextBoxProperty(TextEdit cn,string field)
        {
            // 查找文本框的显示名称
            FieldCfgDto arvField = PropertyHelper.FieldCfgItems.Find(item => item.FieldName == field);

            if (arvField!=null)
            {
                cn.Enabled = arvField.IsFieldUsable;  // 字段是否可编辑
            }
            //cn.Enabled = true;
        }

        /// <summary>
        /// 配置下拉列表框的属性
        /// </summary>
        /// <param name="cn"></param>
        private void ConfigComboBoxProperty(DevExpress.XtraEditors.ComboBoxEdit cn, string field)
        {
            // 查找ComboBox控件匹配的字段信息
            FieldCfgDto arvField = PropertyHelper.FieldCfgItems.Find(item => item.FieldName == field);

            if (arvField != null)
            {
                // ComboBox控件是否可用
                cn.Enabled = arvField.IsFieldUsable;

                if (cn.Enabled)
                {
                    // 查找ComboBox是否对应可配置字段
            //        List<DataDictDto> Items = PropertyHelper.DataDictItems.Where<DataDictDto>(item => item.FieldTypeName == field).OrderBy(item => item.FieldTypeSerialNo) .ToList<DataDictDto>();

                    // 加入配置信息到ComboBox下拉框
               //     if (Items.Count != 0)
                    {
                       // cn.Properties.Items.AddRange(Items.Select(q=>q.FieldTypeValue).ToList());
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
            cbxLayerNo.Enabled = false;
            cbxCellNo.Enabled = false;
            switch(tag)
            {
                case "EDIT":   // 编辑界面
                    this.btnCopy.Enabled    = false;     // 禁用“保存复制”和“保存增加”
                    this.btnSaveAdd.Enabled = false;

                    // 编辑单条记录，禁用“上一条”和“下一条”
                    if (arvInfoList.Count == 1)
                    {                       
                        btnPrev.Enabled = false;
                        btnNext.Enabled = false;
                    }

                    // 档案编号为只读属性，不能修改
                    txtArvID.Properties.ReadOnly = true;
                    
                    break;

                case "ADD":    // 新增界面
                    this.btnPrev.Enabled = false;       // 禁用“上一条”和“下一条”
                    this.btnNext.Enabled = false;
                    
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 配置UI界面的属性
        /// </summary>
        /// <param name="tag">UI界面的tag类型</param>
        private void ConfigControlProperty(string tag)
        {
            // 设置按钮属性
            ConfigButtonProperty(this.Tag.ToString());

            List<int> cabNos = devices.Select(q => q.CabinetNo).ToList();
            cbxGroupNo.Properties.Items.AddRange(cabNos);
            cbxGroupNo.Enabled = true;
            

            // 设置标签、文本框和下拉框属性

            // 遍历UI控件
            foreach (var cn in layoutControl1.Items)
            {
                // 设置Label控件的显示名称和是否可用
                if (cn.GetType() == typeof(LayoutControlItem))
                {
                    // 设置TextBox控件是否可用
                    if ((cn as LayoutControlItem).Control.GetType() == typeof(TextEdit))
                    {
                        // 配置文本框属性
                        ConfigTextBoxProperty((cn as LayoutControlItem).Control as TextEdit, (cn as LayoutControlItem).Text);
                    }

                    // 设置ComboBox控件是否可用，并设置可配置ComboBox控件的下拉明细
                    if ((cn as LayoutControlItem).Control.GetType() == typeof(ComboBoxEdit))
                    {
                        // 配置下拉列表框属性
                        ConfigComboBoxProperty((cn as LayoutControlItem).Control as ComboBoxEdit, (cn as LayoutControlItem).Text);
                    }

                    // 配置标签属性
                    ConfigLabelProperty(cn as LayoutControlItem);
                }
            }
        }

        #endregion

        #region 初始化

        /// <summary>
        /// 根据界面显示初始化记录对象
        /// </summary>
        /// <param name="arch"></param>
        private void InitArchive(ArchiveInfoDto arch)
        {
            // 档案信息
            //arch.ArvID    = txtArvID.Text.Trim();
            arch.ID = txtArvID.Text.Trim();
            arch.ArvTitle = txtArvTitle.Text.Trim();
            arch.ArvType  = cbxArvType.Text.Trim();
            arch.ArvUnit  = cbxArvUnit.Text.Trim();
            arch.ArvYear  = deArvYear.Text.Trim();
            arch.LabelID  = txtLabelID.Text.Trim();
            arch.Edoc = txtEDoc.Text.Trim();
            arch.Rsv1 = cbxRsv2.Text.Trim();
            arch.Rsv2 = txtRsv3.Text.Trim();
            arch.Rsv3 = cbxRsv4.Text.Trim();
            arch.Rsv4 = txtRsv5.Text.Trim();

            arch.CreatePerson = txtCreatePerson.Text.Trim();
            arch.CreateTime   = deCreateTime.DateTime;

            // 档案盒
            arch.ArvBoxID    = txtArvBoxID.Text.Trim();
            arch.ArvBoxTitle = txtArvBoxTitle.Text.Trim();

            // 位置
            string groupNo = cbxGroupNo.Text.Trim();
            arch.GroupNo   = string.IsNullOrEmpty(groupNo) == true ? 0 : Convert.ToInt16(groupNo);
            string layerNo = cbxLayerNo.Text.Trim();
            arch.LayerNo   = string.IsNullOrEmpty(layerNo) == true ? 0 : Convert.ToInt16(layerNo);
            string cellNo  = cbxCellNo.Text.Trim();
            arch.CellNo    = string.IsNullOrEmpty(cellNo) == true ? 0 : Convert.ToInt16(cellNo);

            //// 如果档案盒信息和存储位置信息不为空，则档案状态从“在档”变为“在库”
            //if(!string.IsNullOrEmpty(arch.ArvBoxID) && (arch.GroupNo != 0) && (arch.LayerNo != 0))
            //{
            //    arch.ArvStatus = "在库";
            //}
        }

        /// <summary>
        /// 清除窗口中预设信息
        /// </summary>
        void ClearForm()
        {
            txtArvID.Text       = "";
            txtArvTitle.Text    = "";
            txtArvBoxID.Text    = "";
            txtArvBoxTitle.Text = "";
            txtLabelID.Text     = "";

            cbxArvType.SelectedIndex = -1;
            cbxArvUnit.SelectedIndex = -1;

            cbxGroupNo.SelectedIndex = -1;
            cbxLayerNo.SelectedIndex = -1;
            cbxCellNo.SelectedIndex  = -1;
        }

        /// <summary>
        /// “编辑”状态下UI显示档案信息
        /// </summary>
        /// <param name="arvCurrent"></param>
        void LoadArvInfo(ArchiveInfoDto arvCurrent)
        {
            // 档案信息
            txtArvID.Text       = arvCurrent.ID;// ArvID;
            txtArvTitle.Text    = arvCurrent.ArvTitle;          
            cbxArvType.Text     = arvCurrent.ArvType;
            cbxArvUnit.Text     = arvCurrent.ArvUnit;
            txtLabelID.Text     = arvCurrent.LabelID;
            txtEDoc.Text        = arvCurrent.Edoc;
            deArvYear.Text      = arvCurrent.ArvYear;

            txtCreatePerson.Text  = arvCurrent.CreatePerson;
            deCreateTime.DateTime = arvCurrent.CreateTime;

            // 档案盒信息
            txtArvBoxID.Text    = arvCurrent.ArvBoxID;
            txtArvBoxTitle.Text = arvCurrent.ArvBoxTitle;

            // 档案盒存储位置
            cbxGroupNo.Text = arvCurrent.GroupNo.ToString();
            cbxLayerNo.Text = arvCurrent.LayerNo.ToString();
            cbxCellNo.Text  = arvCurrent.CellNo.ToString();
        }

        /// <summary>
        /// 加载档案盒信息
        /// </summary>
        /// <param name="dto"></param>
        private void LoadArvBox(ArvBoxDto arvBoxCurrent)
        {
            // 档案盒信息
            txtArvBoxID.Text = arvBoxCurrent.ID;// ArvBoxID;
            txtArvBoxTitle.Text = arvBoxCurrent.ArvBoxTitle;

            // 存储位置
            cbxGroupNo.Text = arvBoxCurrent.GroupNo.ToString();
            cbxLayerNo.Text = arvBoxCurrent.LayerNo.ToString();
            cbxCellNo.Text  = arvBoxCurrent.CellNo.ToString();

            // 如果是已有档案盒，则位置设置为只读
           // txtArvBoxID.Properties.ReadOnly = true;
           // txtArvBoxTitle.ReadOnly=true;
            cbxGroupNo.ReadOnly = true;
            cbxLayerNo.ReadOnly = true;
            cbxCellNo.ReadOnly  = true;
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

        #endregion

        #region 档案基本信息界面

        private void FormArvInfo_Load(object sender, EventArgs e)
        {
            devices = CallerFactory.Instance.GetService<ISystemConfigService>().GetAllDevices();//所有设备

            // 设置文本框输入提示
            FieldCfgDto t   = PropertyHelper.FieldCfgItems.Find(q => q.FieldName == "ArvBoxID");
            string nullText = string.Format("请输入或双击选择{0}", t == null ? "ArvBoxID" : t.FieldShowName);
            txtArvBoxID.SetWatermark(nullText);

            // 设置UI控件的属性
            ConfigControlProperty(this.Tag.ToString());

            // 获得所有档案盒信息
            GetArvBoxs();

            // “编辑”界面下显示档案信息
            if (this.Tag.ToString() == "EDIT")
            {
                // 当前选中的档案信息
                arvCurrent = arvInfoList[0];
                // 加载档案信息
                LoadArvInfo(arvCurrent);
            }
        }

        /// <summary>
        /// 双击“档案盒编号”文本框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void txtArvBoxID_DoubleClick(object sender,EventArgs e)
        {
            // 在弹出对话框中选择在档的档案盒
            FormArvBox frmArvBox = new FormArvBox();
            if (frmArvBox.ShowDialog() == DialogResult.OK)
            {
                // 加载选中档案盒信息
                LoadArvBox(frmArvBox.ArvBoxDto);
            }
        }

        /// <summary>
        /// 控件失去焦点时调用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtArvBoxID_Validating(object sender, CancelEventArgs e)
        {
            // 根据录入档案盒ID查找是否存在于数据库中
            List<ArvBoxDto> arvBox = CallerFactory.Instance.GetService<IArvOpService>().FindArvBoxes(QueryHelper.GetConditionList("ArvBoxID", CompareType.Equal, txtArvBoxID.Text));
          
            if (arvBox != null && arvBox.Count > 0)
            {
                // 已经存在的档案盒，自动填写对应信息
                LoadArvBox(arvBox[0]);
            } 
            else
            {
                // 新的档案盒，允许编辑题名和存储位置
                txtArvBoxTitle.ReadOnly = false;
                cbxGroupNo.ReadOnly = false;
                cbxLayerNo.ReadOnly = false;
                cbxCellNo.ReadOnly  = false;
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
            // 建立档案实体类对象
            ArchiveInfoDto arch = new ArchiveInfoDto();
            // 利用界面显示信息初始化实体类
            InitArchive(arch);

            // 利用反射获取类对象的成员
            foreach (PropertyInfo info in arch.GetType().GetProperties())
            {
                if (info.Name != "ArvStatus" && info.Name.ToLower() != "id")
                {
                    if ((info.GetValue(arch, null) != null) && (string.IsNullOrEmpty(info.GetValue(arch, null).ToString())))
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
        ///  电子文档文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEDoc_DoubleClick(object sender, EventArgs e)
        {
            txtEDoc.Text = FileDialogHelper.Instance.OpenFile();
        }
       
        #endregion

        #region 数据库操作
       
        /// <summary>
        /// 新增操作
        /// </summary>
        /// <param name="arch"></param>
        private void AddNew(ArchiveInfoDto arch)
        {
            try
            {
                int succeed = CallerFactory.Instance.GetService<IArvOpService>().InToStorage(arch);
                if(succeed != 0)
                {
                    // 档案信息加入表格关联的数据集
                    UpdateGridViewShow(arch);

                    MessageUtil.ShowTips("添加成功！"); 
                }
                else
                {
                    MessageUtil.ShowTips("添加失败！"); 
                }                   
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
                // 界面信息初始化实体类
                InitArchive(arch);
                // 档案初始状态为“在档”，执行“入库”后转为“在库”
                arch.ArvStatus = "在档";
                // 写数据库
                AddNew(arch);
            }

            // 修改
            if (this.Tag.ToString() == "EDIT")
            {
                InitArchive(arvCurrent);
                Update(arvCurrent);
            }
        }
        #endregion

        #endregion

        #region 菜单

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
           catch (Exception ex)
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
               txtArvID.Text = "";
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

       private void cbxGroupNo_SelectedIndexChanged(object sender, EventArgs e)
       {
           DeviceDto dev = devices.Find(s => s.CabinetNo == (int)cbxGroupNo.EditValue);
           if(dev!=null)
           {
               if(!cbxCellNo.Enabled||!cbxLayerNo.Enabled)
               {
                   cbxLayerNo.Enabled = true;
                   cbxCellNo.Enabled = true;
               }
               for (int i = 1; i <= dev.CabinetLayers; i++)
                   cbxLayerNo.Properties.Items.Add(i);
               for (int i = 1; i <= dev.CabinetCells; i++)
                   cbxCellNo.Properties.Items.Add(i);
           }
       }

       private void cbxLayerNo_SelectedIndexChanged(object sender, EventArgs e)
       {

       }

       private void cbxCellNo_SelectedIndexChanged(object sender, EventArgs e)
       {

       }
    }
}
