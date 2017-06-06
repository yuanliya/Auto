using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

using HZK.Helper;

using NJUST.AUTO06.Utility;
using NJUST.AUTO06.Peripheral;

namespace HZK.UI.DV
{
    public partial class FormDVBarCodePrint : Form
    {
        public FormDVBarCodePrint()
        {
            InitializeComponent();
        }

        private void BarCodePrintFrm_Load(object sender, EventArgs e)
        {
            // 读取系统中的串口号
            try
            {
                // 从注册表获取系统串口列表
                //RegistryKey keyCom = Registry.LocalMachine.OpenSubKey("Hardware\\DeviceMap\\SerialComm");

                //string[] sSubKeys = keyCom.GetValueNames();
                //for (int i = 0; i < sSubKeys.Length; i++)
                //{
                //    cbxComm.Items.Add(keyCom.GetValue(sSubKeys[i]));
                //}

                // 利用C#的SerialPort类
                string[] ports = System.IO.Ports.SerialPort.GetPortNames();
                foreach (string port in ports)
                {
                    cbxComm.Items.Add(port);
                }
            }
            catch
            {
                MessageUtil.ShowTips("没有找到可供使用的串口，请检查串口接线是否连好！");
                throw;
            }

            cbxCondition.Items.Add("档案编号");
            cbxCondition.Items.Add("档案名称");
        }

        // 条码打印位置设置
        private void btnBarCodeCfg_Click(object sender, EventArgs e)
        {
            FormDVBarCodeCfg frmBarCodeCfg = new FormDVBarCodeCfg();

            frmBarCodeCfg.ShowDialog();
        }

        private void toolBtnQuery_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cbxCondition.Text) || String.IsNullOrEmpty(txtKeyWord.Text))
            {
                MessageUtil.ShowTips("请填写完整的查询信息！");
                return;
            }

            // 建立查询条件
            StringBuilder sb = new StringBuilder();
            sb.Append("Select ArvID, ArvTitle, ArvLabel from ArchiveInfo where ");     
            sb.Append(cbxCondition.Text + " like '%%" + txtKeyWord.Text + "%%'");

            //dgv.DataSource = arvManager.QueryArchiveInfo(sb.ToString());

            //if (schCount == 0)
            //{
            //    DataGridHelper DataGridHelper = new DataGridHelper();
            //    // 插入“序号”列
            //    DataGridHelper.InsertTextBoxColumn(dgv, 0, "序号");
            //    // 设置表格格式
            //    DataGridHelper.ConfigDataGridView(dgv);
            //    schCount = 1;
            //}

            //// 编排表格中的序号
            //for (int i = 0; i < dgv.Rows.Count; i++)
            //{
            //    dgv.Rows[i].Cells[0].Value = (i + 1).ToString();
            //}
        }

        private void dgv_DoubleClick(object sender, EventArgs e)
        {
            //// 获得当前选中的行     
            //int rowindex = dgv.CurrentCell.RowIndex;
            //txtArvID.Text = dgv.Rows[rowindex].Cells[1].Value.ToString();
            //string labelID= dgv.Rows[rowindex].Cells[3].Value.ToString();

            //if (labelID != "")
            //{
            //    // 当前档案已存在条形码ID
            //    txtBarCode.Text = labelID;
            //    txtBarCode.Enabled = false;  
            //    isBarCodeExisted = true;

            //    return;
            //}

            //// 按照“档案编号+生成时间”来生成条形码
            //if (rdbArvType.Checked)
            //{
            //    // 当前档案还没有条形码ID，按"档案编号+生成时间"规则生成
            //    txtBarCode.Text = txtArvID.Text.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();
            //    txtBarCode.Enabled = true;  
            //}

            //isBarCodeExisted = false;

        }

        private void btnBarcodePrint_Click(object sender, EventArgs e)
        {
            if (cbxComm.Text == "")
            {
                MessageBox.Show("请先选择端口号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (txtBarCode.Text == "")
            {
                MessageBox.Show("没有条码可以打印！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string barCode = txtBarCode.Text;

            // 打开条形码配置文件，获取条码配置信息
            try
            {
                APPConfigHelper.Instance.OpenConfigFile("barCode.config");
            }
            catch(Exception ex)
            {
                MessageUtil.ShowWarning(ex.Message);

                // 关闭当前窗口
                this.Close();

                return;
            }

            // 加载条码配置信息
            Hashtable htBarCode = APPConfigHelper.Instance.GetAllKeyAndValue();

            string barLeftMargin   = htBarCode["条码左偏移"].ToString();
            string barTopMargin    = htBarCode["条码上偏移"].ToString();
            string barHeight       = htBarCode["条码高度"].ToString();
            string moduleWidth     = htBarCode["模块宽度"].ToString();
            string strWidth        = htBarCode["字符宽度"].ToString();
            string strHeight       = htBarCode["字符高度"].ToString();
            string verticalSpacing = htBarCode["行间距"].ToString();

            string secondHeght = (Convert.ToDouble(barTopMargin) + Convert.ToDouble(barHeight) + Convert.ToDouble(verticalSpacing)).ToString();
            string str = "^XA^FO" + barLeftMargin + "," + barTopMargin + "^BY" + moduleWidth + "^BCN," + barHeight + ",N,N^FD>;" +

            //C#打印条码操作的实例  
            barCode + "^FS^FO" + barLeftMargin + "," + secondHeght + "^CI1^ABN," + strWidth + "," + strHeight + "^FD" + barCode + "^FS^PQ1,0,1,Y^XZ";

            bool bol = BarCodePrinterHelper.Instance.Open(cbxComm.Text.Trim());
            if (bol == false)
            {
                MessageUtil.ShowTips("端口未打开！");
                return;
            }

            bol = BarCodePrinterHelper.Instance.Write(str);
            if (bol == false)
            {
                MessageUtil.ShowTips("端口写入失败！");
                return;
            }

            // 关闭打印机
            BarCodePrinterHelper.Instance.Close();
        }

        //enter键查询
        private void txtQueryValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                toolBtnQuery_Click(sender, e);
            }
        }

        private void toolBtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
