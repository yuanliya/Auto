namespace HZK.UI.DV
{
    partial class FormDVBarCodePrint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormDVBarCodePrint));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnBarCodeCfg = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rdbArvFree = new System.Windows.Forms.RadioButton();
            this.rdbArvType = new System.Windows.Forms.RadioButton();
            this.cbxComm = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnBarcodePrint = new System.Windows.Forms.Button();
            this.txtArvID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBarCode = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.labCondation = new System.Windows.Forms.ToolStripLabel();
            this.cbxCondition = new System.Windows.Forms.ToolStripComboBox();
            this.txtKeyWord = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnQuery = new System.Windows.Forms.ToolStripButton();
            this.toolBtnExit = new System.Windows.Forms.ToolStripButton();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnBarCodeCfg);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.cbxComm);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(9, 177);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(396, 120);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "打印设置";
            // 
            // btnBarCodeCfg
            // 
            this.btnBarCodeCfg.Location = new System.Drawing.Point(277, 28);
            this.btnBarCodeCfg.Name = "btnBarCodeCfg";
            this.btnBarCodeCfg.Size = new System.Drawing.Size(110, 23);
            this.btnBarCodeCfg.TabIndex = 10;
            this.btnBarCodeCfg.Text = "条码配置";
            this.btnBarCodeCfg.UseVisualStyleBackColor = true;
            this.btnBarCodeCfg.Click += new System.EventHandler(this.btnBarCodeCfg_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 32);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 12);
            this.label4.TabIndex = 2;
            this.label4.Text = "打印机连接串口：";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rdbArvFree);
            this.panel1.Controls.Add(this.rdbArvType);
            this.panel1.Location = new System.Drawing.Point(116, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(272, 43);
            this.panel1.TabIndex = 5;
            // 
            // rdbArvFree
            // 
            this.rdbArvFree.AutoSize = true;
            this.rdbArvFree.Location = new System.Drawing.Point(161, 14);
            this.rdbArvFree.Name = "rdbArvFree";
            this.rdbArvFree.Size = new System.Drawing.Size(83, 16);
            this.rdbArvFree.TabIndex = 1;
            this.rdbArvFree.Text = "用户自定义";
            this.rdbArvFree.UseVisualStyleBackColor = true;
            // 
            // rdbArvType
            // 
            this.rdbArvType.AutoSize = true;
            this.rdbArvType.Checked = true;
            this.rdbArvType.Location = new System.Drawing.Point(3, 13);
            this.rdbArvType.Name = "rdbArvType";
            this.rdbArvType.Size = new System.Drawing.Size(125, 16);
            this.rdbArvType.TabIndex = 0;
            this.rdbArvType.TabStop = true;
            this.rdbArvType.Text = "档案编号+生成时间";
            this.rdbArvType.UseVisualStyleBackColor = true;
            // 
            // cbxComm
            // 
            this.cbxComm.FormattingEnabled = true;
            this.cbxComm.Location = new System.Drawing.Point(121, 28);
            this.cbxComm.Name = "cbxComm";
            this.cbxComm.Size = new System.Drawing.Size(121, 20);
            this.cbxComm.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "条码生成规则：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnBarcodePrint);
            this.groupBox2.Controls.Add(this.txtArvID);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtBarCode);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(10, 303);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(395, 110);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "条码打印";
            // 
            // btnBarcodePrint
            // 
            this.btnBarcodePrint.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnBarcodePrint.Location = new System.Drawing.Point(286, 21);
            this.btnBarcodePrint.Name = "btnBarcodePrint";
            this.btnBarcodePrint.Size = new System.Drawing.Size(100, 71);
            this.btnBarcodePrint.TabIndex = 55;
            this.btnBarcodePrint.Text = "条码打印";
            this.btnBarcodePrint.UseVisualStyleBackColor = true;
            this.btnBarcodePrint.Click += new System.EventHandler(this.btnBarcodePrint_Click);
            // 
            // txtArvID
            // 
            this.txtArvID.Enabled = false;
            this.txtArvID.Location = new System.Drawing.Point(74, 27);
            this.txtArvID.Name = "txtArvID";
            this.txtArvID.Size = new System.Drawing.Size(187, 21);
            this.txtArvID.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "档案编号";
            // 
            // txtBarCode
            // 
            this.txtBarCode.Location = new System.Drawing.Point(74, 70);
            this.txtBarCode.Name = "txtBarCode";
            this.txtBarCode.Size = new System.Drawing.Size(187, 21);
            this.txtBarCode.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "条码ID";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.labCondation,
            this.cbxCondition,
            this.txtKeyWord,
            this.toolStripSeparator2,
            this.toolBtnQuery,
            this.toolBtnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(413, 27);
            this.toolStrip1.TabIndex = 91;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // labCondation
            // 
            this.labCondation.Name = "labCondation";
            this.labCondation.Size = new System.Drawing.Size(59, 24);
            this.labCondation.Text = "查询条件:";
            // 
            // cbxCondition
            // 
            this.cbxCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCondition.Name = "cbxCondition";
            this.cbxCondition.Size = new System.Drawing.Size(90, 27);
            // 
            // txtKeyWord
            // 
            this.txtKeyWord.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtKeyWord.Name = "txtKeyWord";
            this.txtKeyWord.Size = new System.Drawing.Size(100, 27);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // toolBtnQuery
            // 
            this.toolBtnQuery.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnQuery.Image")));
            this.toolBtnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnQuery.Name = "toolBtnQuery";
            this.toolBtnQuery.Size = new System.Drawing.Size(56, 24);
            this.toolBtnQuery.Tag = "6";
            this.toolBtnQuery.Text = "查找";
            // 
            // toolBtnExit
            // 
            this.toolBtnExit.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnExit.Image")));
            this.toolBtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnExit.Name = "toolBtnExit";
            this.toolBtnExit.Size = new System.Drawing.Size(56, 24);
            this.toolBtnExit.Tag = "7";
            this.toolBtnExit.Text = "退出";
            this.toolBtnExit.Click += new System.EventHandler(this.toolBtnExit_Click);
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(6, 30);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(400, 169);
            this.gridControl1.TabIndex = 92;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // FormDVBarCodePrint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(413, 428);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "FormDVBarCodePrint";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "条码打印";
            this.Load += new System.EventHandler(this.BarCodePrintFrm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnBarCodeCfg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rdbArvFree;
        private System.Windows.Forms.RadioButton rdbArvType;
        private System.Windows.Forms.ComboBox cbxComm;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtBarCode;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtArvID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBarcodePrint;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel labCondation;
        private System.Windows.Forms.ToolStripComboBox cbxCondition;
        private System.Windows.Forms.ToolStripTextBox txtKeyWord;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolBtnQuery;
        private System.Windows.Forms.ToolStripButton toolBtnExit;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}