namespace AutoCabinet2017.UI.Report
{
    partial class FormArvCount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormArvCount));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gcCount = new DevExpress.XtraGrid.GridControl();
            this.gvCount = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colArgument = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chart = new DevExpress.XtraCharts.ChartControl();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.cbxCondition = new System.Windows.Forms.ToolStripComboBox();
            this.cbxTime = new System.Windows.Forms.ToolStripComboBox();
            this.toolBtnAllQuery = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolBtnExit = new System.Windows.Forms.ToolStripButton();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gcCount);
            this.groupBox1.Controls.Add(this.chart);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 28);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(1095, 630);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "统计情况";
            // 
            // gcCount
            // 
            this.gcCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcCount.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.gcCount.Location = new System.Drawing.Point(815, 32);
            this.gcCount.MainView = this.gvCount;
            this.gcCount.Margin = new System.Windows.Forms.Padding(4);
            this.gcCount.Name = "gcCount";
            this.gcCount.Size = new System.Drawing.Size(259, 534);
            this.gcCount.TabIndex = 5;
            this.gcCount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCount});
            // 
            // gvCount
            // 
            this.gvCount.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvCount.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvCount.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvCount.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colArgument,
            this.colValue});
            this.gvCount.GridControl = this.gcCount;
            this.gvCount.Name = "gvCount";
            this.gvCount.OptionsView.ShowFooter = true;
            this.gvCount.OptionsView.ShowGroupPanel = false;
            // 
            // colArgument
            // 
            this.colArgument.AppearanceCell.Options.UseTextOptions = true;
            this.colArgument.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colArgument.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colArgument.Caption = "种类";
            this.colArgument.FieldName = "Argument";
            this.colArgument.Name = "colArgument";
            this.colArgument.Visible = true;
            this.colArgument.VisibleIndex = 0;
            // 
            // colValue
            // 
            this.colValue.AppearanceCell.Options.UseTextOptions = true;
            this.colValue.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colValue.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.colValue.Caption = "数量";
            this.colValue.FieldName = "Value";
            this.colValue.Name = "colValue";
            this.colValue.Visible = true;
            this.colValue.VisibleIndex = 1;
            // 
            // chart
            // 
            this.chart.Location = new System.Drawing.Point(8, 32);
            this.chart.Margin = new System.Windows.Forms.Padding(4);
            this.chart.Name = "chart";
            this.chart.SeriesSerializable = new DevExpress.XtraCharts.Series[0];
            this.chart.Size = new System.Drawing.Size(788, 536);
            this.chart.TabIndex = 4;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripLabel2,
            this.cbxCondition,
            this.cbxTime,
            this.toolBtnAllQuery,
            this.toolStripSeparator2,
            this.toolBtnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1095, 28);
            this.toolStrip1.TabIndex = 11;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 28);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(69, 25);
            this.toolStripLabel2.Text = "统计方式";
            // 
            // cbxCondition
            // 
            this.cbxCondition.BackColor = System.Drawing.Color.Gold;
            this.cbxCondition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCondition.DropDownWidth = 120;
            this.cbxCondition.Items.AddRange(new object[] {
            "按档案状态统计",
            "按档案类别统计",
            "按部门类别",
            "按年限统计",
            "按存储位置统计",
            "按入库时间统计"});
            this.cbxCondition.Name = "cbxCondition";
            this.cbxCondition.Size = new System.Drawing.Size(159, 28);
            this.cbxCondition.Click += new System.EventHandler(this.cbxCondition_Click);
            // 
            // cbxTime
            // 
            this.cbxTime.Items.AddRange(new object[] {
            "近3个月内",
            "近6个月内",
            "近一年内",
            "所有时间"});
            this.cbxTime.Name = "cbxTime";
            this.cbxTime.Size = new System.Drawing.Size(121, 28);
            this.cbxTime.Visible = false;
            // 
            // toolBtnAllQuery
            // 
            this.toolBtnAllQuery.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnAllQuery.Image")));
            this.toolBtnAllQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnAllQuery.Name = "toolBtnAllQuery";
            this.toolBtnAllQuery.Size = new System.Drawing.Size(93, 25);
            this.toolBtnAllQuery.Text = "开始统计";
            this.toolBtnAllQuery.Click += new System.EventHandler(this.toolBtnAllQuery_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 28);
            // 
            // toolBtnExit
            // 
            this.toolBtnExit.Image = ((System.Drawing.Image)(resources.GetObject("toolBtnExit.Image")));
            this.toolBtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolBtnExit.Name = "toolBtnExit";
            this.toolBtnExit.Size = new System.Drawing.Size(63, 25);
            this.toolBtnExit.Text = "退出";
            // 
            // FormArvCount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1095, 658);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "FormArvCount";
            this.Text = "档案统计";
            this.Load += new System.EventHandler(this.FormAMArvCount_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripComboBox cbxCondition;
        private System.Windows.Forms.ToolStripButton toolBtnAllQuery;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton toolBtnExit;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private DevExpress.XtraCharts.ChartControl chart;
        private DevExpress.XtraGrid.GridControl gcCount;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCount;
        private DevExpress.XtraGrid.Columns.GridColumn colArgument;
        private DevExpress.XtraGrid.Columns.GridColumn colValue;
        private System.Windows.Forms.ToolStripComboBox cbxTime;
    }
}