namespace AutoCabinet2017.UI.SY
{
    partial class FormSYLog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSYLog));
            this.panelCtrl = new System.Windows.Forms.Panel();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblArvBoxTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblArvBoxID = new DevExpress.XtraEditors.LabelControl();
            this.lblArvTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.txtArvBoxID = new DevExpress.XtraEditors.TextEdit();
            this.cbxLogType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.dtFrom = new DevExpress.XtraEditors.DateEdit();
            this.dtTo = new DevExpress.XtraEditors.DateEdit();
            this.gcLog = new DevExpress.XtraGrid.GridControl();
            this.gvLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelCtrl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArvBoxID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxLogType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLog)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelCtrl
            // 
            this.panelCtrl.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelCtrl.Controls.Add(this.labelControl1);
            this.panelCtrl.Controls.Add(this.lblArvBoxTitle);
            this.panelCtrl.Controls.Add(this.lblArvBoxID);
            this.panelCtrl.Controls.Add(this.lblArvTitle);
            this.panelCtrl.Controls.Add(this.btnSearch);
            this.panelCtrl.Controls.Add(this.btnDelete);
            this.panelCtrl.Controls.Add(this.txtArvBoxID);
            this.panelCtrl.Controls.Add(this.cbxLogType);
            this.panelCtrl.Controls.Add(this.dtFrom);
            this.panelCtrl.Controls.Add(this.dtTo);
            this.panelCtrl.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCtrl.Location = new System.Drawing.Point(0, 0);
            this.panelCtrl.Margin = new System.Windows.Forms.Padding(26, 4, 26, 4);
            this.panelCtrl.Name = "panelCtrl";
            this.panelCtrl.Size = new System.Drawing.Size(895, 61);
            this.panelCtrl.TabIndex = 68;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(450, 21);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(23, 14);
            this.labelControl1.TabIndex = 97;
            this.labelControl1.Tag = "ArvBoxTitle";
            this.labelControl1.Text = "——";
            // 
            // lblArvBoxTitle
            // 
            this.lblArvBoxTitle.Location = new System.Drawing.Point(332, 20);
            this.lblArvBoxTitle.Name = "lblArvBoxTitle";
            this.lblArvBoxTitle.Size = new System.Drawing.Size(24, 14);
            this.lblArvBoxTitle.TabIndex = 96;
            this.lblArvBoxTitle.Tag = "ArvBoxTitle";
            this.lblArvBoxTitle.Text = "时间";
            // 
            // lblArvBoxID
            // 
            this.lblArvBoxID.Location = new System.Drawing.Point(179, 21);
            this.lblArvBoxID.Name = "lblArvBoxID";
            this.lblArvBoxID.Size = new System.Drawing.Size(48, 14);
            this.lblArvBoxID.TabIndex = 95;
            this.lblArvBoxID.Tag = "ArvBoxID";
            this.lblArvBoxID.Text = "用户编号";
            // 
            // lblArvTitle
            // 
            this.lblArvTitle.Location = new System.Drawing.Point(15, 22);
            this.lblArvTitle.Name = "lblArvTitle";
            this.lblArvTitle.Size = new System.Drawing.Size(48, 14);
            this.lblArvTitle.TabIndex = 94;
            this.lblArvTitle.Tag = "ArvTitle";
            this.lblArvTitle.Text = "日志类型";
            // 
            // btnSearch
            // 
            this.btnSearch.Image = global::AutoCabinet2017.Properties.Resources.find32x32;
            this.btnSearch.Location = new System.Drawing.Point(643, 10);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 38);
            this.btnSearch.TabIndex = 93;
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
            this.btnDelete.Location = new System.Drawing.Point(749, 10);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(136, 38);
            this.btnDelete.TabIndex = 20;
            this.btnDelete.Tag = "Delete";
            this.btnDelete.Text = "删除30天前的日志";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // txtArvBoxID
            // 
            this.txtArvBoxID.EnterMoveNextControl = true;
            this.txtArvBoxID.Location = new System.Drawing.Point(231, 18);
            this.txtArvBoxID.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtArvBoxID.Name = "txtArvBoxID";
            this.txtArvBoxID.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtArvBoxID.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtArvBoxID.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtArvBoxID.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.txtArvBoxID.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtArvBoxID.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtArvBoxID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtArvBoxID.Size = new System.Drawing.Size(73, 20);
            this.txtArvBoxID.TabIndex = 90;
            this.txtArvBoxID.Tag = "UserCode";
            // 
            // cbxLogType
            // 
            this.cbxLogType.Location = new System.Drawing.Point(79, 19);
            this.cbxLogType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbxLogType.Name = "cbxLogType";
            this.cbxLogType.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.cbxLogType.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.cbxLogType.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxLogType.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.cbxLogType.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.cbxLogType.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.cbxLogType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cbxLogType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxLogType.Properties.Items.AddRange(new object[] {
            "",
            "登录日志",
            "操作日志",
            "错误日志"});
            this.cbxLogType.Size = new System.Drawing.Size(74, 20);
            this.cbxLogType.TabIndex = 91;
            this.cbxLogType.Tag = "Logger";
            this.cbxLogType.SelectedIndexChanged += new System.EventHandler(this.cbxLogType_SelectedIndexChanged);
            // 
            // dtFrom
            // 
            this.dtFrom.EditValue = null;
            this.dtFrom.Location = new System.Drawing.Point(361, 18);
            this.dtFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.dtFrom.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dtFrom.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dtFrom.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.dtFrom.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.dtFrom.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.dtFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.DisplayFormat.FormatString = "";
            this.dtFrom.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtFrom.Properties.EditFormat.FormatString = "";
            this.dtFrom.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtFrom.Properties.Mask.EditMask = "";
            this.dtFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtFrom.Size = new System.Drawing.Size(82, 20);
            this.dtFrom.TabIndex = 92;
            this.dtFrom.Tag = "DateTime";
            this.dtFrom.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.dtFrom_EditValueChanging);
            // 
            // dtTo
            // 
            this.dtTo.EditValue = null;
            this.dtTo.Location = new System.Drawing.Point(478, 18);
            this.dtTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtTo.Name = "dtTo";
            this.dtTo.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.dtTo.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.dtTo.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dtTo.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.dtTo.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.dtTo.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.dtTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.DisplayFormat.FormatString = "";
            this.dtTo.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtTo.Properties.EditFormat.FormatString = "";
            this.dtTo.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtTo.Properties.Mask.EditMask = "";
            this.dtTo.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.None;
            this.dtTo.Size = new System.Drawing.Size(82, 20);
            this.dtTo.TabIndex = 98;
            this.dtTo.Tag = "DateEdit";
            this.dtTo.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.dtTo_EditValueChanging);
            // 
            // gcLog
            // 
            this.gcLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLog.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.gcLog.Location = new System.Drawing.Point(4, 18);
            this.gcLog.MainView = this.gvLog;
            this.gcLog.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gcLog.Name = "gcLog";
            this.gcLog.Size = new System.Drawing.Size(887, 294);
            this.gcLog.TabIndex = 0;
            this.gcLog.Tag = "1";
            this.gcLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLog});
            // 
            // gvLog
            // 
            this.gvLog.Appearance.ColumnFilterButton.Options.UseTextOptions = true;
            this.gvLog.Appearance.ColumnFilterButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvLog.Appearance.ColumnFilterButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvLog.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvLog.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvLog.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvLog.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvLog.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvLog.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvLog.GridControl = this.gcLog;
            this.gvLog.Name = "gvLog";
            this.gvLog.OptionsMenu.EnableFooterMenu = false;
            this.gvLog.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvLog.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvLog.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gvLog.OptionsView.ColumnAutoWidth = false;
            this.gvLog.OptionsView.ShowGroupPanel = false;
            this.gvLog.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvLog_RowCellStyle);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gcLog);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 61);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox1.Size = new System.Drawing.Size(895, 316);
            this.groupBox1.TabIndex = 69;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "日志明细";
            // 
            // FormSYLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 377);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelCtrl);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "FormSYLog";
            this.Text = "FormSYLog";
            this.Load += new System.EventHandler(this.FormSYLog_Load);
            this.panelCtrl.ResumeLayout(false);
            this.panelCtrl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArvBoxID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxLogType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLog)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelCtrl;
        private DevExpress.XtraEditors.LabelControl lblArvBoxTitle;
        private DevExpress.XtraEditors.LabelControl lblArvBoxID;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.TextEdit txtArvBoxID;
        private DevExpress.XtraGrid.GridControl gcLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLog;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtFrom;
        private DevExpress.XtraEditors.DateEdit dtTo;
        private DevExpress.XtraEditors.LabelControl lblArvTitle;
        private DevExpress.XtraEditors.ComboBoxEdit cbxLogType;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}