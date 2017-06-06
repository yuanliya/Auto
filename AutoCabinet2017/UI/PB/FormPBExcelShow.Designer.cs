namespace AutoCabinet2017.UI.PB
{
    partial class FormPBExcelShow
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuccessLabel = new System.Windows.Forms.Label();
            this.FalseLabel = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.btnDetail = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.gcImportFailure = new DevExpress.XtraGrid.GridControl();
            this.gvImportFailure = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.Table = new System.Windows.Forms.TabControl();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcImportFailure)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvImportFailure)).BeginInit();
            this.Table.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(137, 28);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "成功导入数据";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(271, 28);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "条";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(497, 29);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(82, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "导入失败：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(612, 29);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "条";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(193, 66);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 29);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // SuccessLabel
            // 
            this.SuccessLabel.AutoSize = true;
            this.SuccessLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.SuccessLabel.Location = new System.Drawing.Point(243, 24);
            this.SuccessLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.SuccessLabel.Name = "SuccessLabel";
            this.SuccessLabel.Size = new System.Drawing.Size(20, 20);
            this.SuccessLabel.TabIndex = 14;
            this.SuccessLabel.Text = "0";
            // 
            // FalseLabel
            // 
            this.FalseLabel.AutoSize = true;
            this.FalseLabel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.FalseLabel.Location = new System.Drawing.Point(583, 25);
            this.FalseLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.FalseLabel.Name = "FalseLabel";
            this.FalseLabel.Size = new System.Drawing.Size(20, 20);
            this.FalseLabel.TabIndex = 17;
            this.FalseLabel.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(75, 28);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(52, 15);
            this.label13.TabIndex = 18;
            this.label13.Text = "提示：";
            // 
            // btnDetail
            // 
            this.btnDetail.Location = new System.Drawing.Point(407, 66);
            this.btnDetail.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(100, 29);
            this.btnDetail.TabIndex = 19;
            this.btnDetail.Text = "详情>>";
            this.btnDetail.UseVisualStyleBackColor = true;
            this.btnDetail.Click += new System.EventHandler(this.btnDetail_Click);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.gcImportFailure);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.tabPage1.Size = new System.Drawing.Size(685, 291);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "档案导入失败明细";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // gcImportFailure
            // 
            this.gcImportFailure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcImportFailure.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcImportFailure.Location = new System.Drawing.Point(4, 4);
            this.gcImportFailure.MainView = this.gvImportFailure;
            this.gcImportFailure.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcImportFailure.Name = "gcImportFailure";
            this.gcImportFailure.Size = new System.Drawing.Size(677, 283);
            this.gcImportFailure.TabIndex = 0;
            this.gcImportFailure.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvImportFailure});
            // 
            // gvImportFailure
            // 
            this.gvImportFailure.GridControl = this.gcImportFailure;
            this.gvImportFailure.Name = "gvImportFailure";
            this.gvImportFailure.OptionsView.ColumnAutoWidth = false;
            this.gvImportFailure.OptionsView.ShowGroupPanel = false;
            // 
            // Table
            // 
            this.Table.Controls.Add(this.tabPage1);
            this.Table.Location = new System.Drawing.Point(19, 121);
            this.Table.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Table.Name = "Table";
            this.Table.SelectedIndex = 0;
            this.Table.Size = new System.Drawing.Size(693, 320);
            this.Table.TabIndex = 21;
            // 
            // FormPBExcelShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 446);
            this.Controls.Add(this.Table);
            this.Controls.Add(this.btnDetail);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.FalseLabel);
            this.Controls.Add(this.SuccessLabel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(730, 551);
            this.Name = "FormPBExcelShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Excel导入明细";
            this.Load += new System.EventHandler(this.FormPBExcelShow_Load);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcImportFailure)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvImportFailure)).EndInit();
            this.Table.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label SuccessLabel;
        private System.Windows.Forms.Label FalseLabel;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btnDetail;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabControl Table;
        private DevExpress.XtraGrid.GridControl gcImportFailure;
        private DevExpress.XtraGrid.Views.Grid.GridView gvImportFailure;
    }
}