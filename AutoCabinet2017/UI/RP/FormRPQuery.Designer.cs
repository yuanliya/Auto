namespace AutoCabinet2017.UI.RP
{
    partial class FormRPQuery
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormRPQuery));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.toolBar = new DevExpress.XtraBars.Bar();
            this.toolMutilEdit = new DevExpress.XtraBars.BarButtonItem();
            this.toolDelete = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.toolExport = new DevExpress.XtraBars.BarButtonItem();
            this.toolPrint = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem2 = new DevExpress.XtraBars.BarStaticItem();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.toolQuery = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barStaticItem3 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItem4 = new DevExpress.XtraBars.BarStaticItem();
            this.toolQueryKey = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemComboBox1 = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.toolQueryValue = new DevExpress.XtraBars.BarEditItem();
            this.repositoryItemTextEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.toolLendInfo = new DevExpress.XtraBars.BarCheckItem();
            this.panelCondition = new System.Windows.Forms.Panel();
            this.radioLendType = new DevExpress.XtraEditors.RadioGroup();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.dtTo = new DevExpress.XtraEditors.DateEdit();
            this.dtFrom = new DevExpress.XtraEditors.DateEdit();
            this.radioTimeSpan = new DevExpress.XtraEditors.RadioGroup();
            this.gcArvInfo = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.panelCondition.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioLendType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioTimeSpan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcArvInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.toolBar});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.toolMutilEdit,
            this.toolDelete,
            this.barStaticItem1,
            this.toolExport,
            this.toolPrint,
            this.barStaticItem2,
            this.barStaticItem3,
            this.toolQuery,
            this.barStaticItem4,
            this.toolQueryKey,
            this.toolQueryValue,
            this.toolLendInfo});
            this.barManager1.MaxItemId = 16;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemTextEdit1,
            this.repositoryItemComboBox1,
            this.repositoryItemTextEdit2,
            this.repositoryItemCheckEdit1});
            // 
            // toolBar
            // 
            this.toolBar.BarName = "Tools";
            this.toolBar.DockCol = 0;
            this.toolBar.DockRow = 0;
            this.toolBar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.toolBar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolMutilEdit, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolDelete, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem1),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.toolExport, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItem2),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.toolQueryKey, "", false, true, true, 95),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.Width, this.toolQueryValue, "", false, true, true, 107),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolLendInfo),
            new DevExpress.XtraBars.LinkPersistInfo(this.toolQuery)});
            this.toolBar.Text = "Tools";
            // 
            // toolMutilEdit
            // 
            this.toolMutilEdit.Caption = "批量修改";
            this.toolMutilEdit.Glyph = ((System.Drawing.Image)(resources.GetObject("toolMutilEdit.Glyph")));
            this.toolMutilEdit.Id = 0;
            this.toolMutilEdit.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolMutilEdit.LargeGlyph")));
            this.toolMutilEdit.Name = "toolMutilEdit";
            // 
            // toolDelete
            // 
            this.toolDelete.Caption = "删除";
            this.toolDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("toolDelete.Glyph")));
            this.toolDelete.Id = 1;
            this.toolDelete.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolDelete.LargeGlyph")));
            this.toolDelete.Name = "toolDelete";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Id = 2;
            this.barStaticItem1.Name = "barStaticItem1";
            this.barStaticItem1.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // toolExport
            // 
            this.toolExport.Caption = "导出";
            this.toolExport.Glyph = ((System.Drawing.Image)(resources.GetObject("toolExport.Glyph")));
            this.toolExport.Id = 3;
            this.toolExport.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolExport.LargeGlyph")));
            this.toolExport.Name = "toolExport";
            // 
            // toolPrint
            // 
            this.toolPrint.Caption = "打印";
            this.toolPrint.Glyph = ((System.Drawing.Image)(resources.GetObject("toolPrint.Glyph")));
            this.toolPrint.Id = 4;
            this.toolPrint.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("toolPrint.LargeGlyph")));
            this.toolPrint.Name = "toolPrint";
            // 
            // barStaticItem2
            // 
            this.barStaticItem2.Id = 5;
            this.barStaticItem2.Name = "barStaticItem2";
            this.barStaticItem2.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // toolQuery
            // 
            this.toolQuery.Caption = "检索";
            this.toolQuery.Id = 8;
            this.toolQuery.Name = "toolQuery";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(983, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 473);
            this.barDockControlBottom.Size = new System.Drawing.Size(983, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 442);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(983, 31);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 442);
            // 
            // barStaticItem3
            // 
            this.barStaticItem3.Caption = "barStaticItem3";
            this.barStaticItem3.Id = 6;
            this.barStaticItem3.Name = "barStaticItem3";
            this.barStaticItem3.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barStaticItem4
            // 
            this.barStaticItem4.Id = 10;
            this.barStaticItem4.Name = "barStaticItem4";
            this.barStaticItem4.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // toolQueryKey
            // 
            this.toolQueryKey.Caption = "barEditItem1";
            this.toolQueryKey.Edit = this.repositoryItemComboBox1;
            this.toolQueryKey.Id = 11;
            this.toolQueryKey.Name = "toolQueryKey";
            // 
            // repositoryItemComboBox1
            // 
            this.repositoryItemComboBox1.AutoHeight = false;
            this.repositoryItemComboBox1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBox1.Name = "repositoryItemComboBox1";
            // 
            // toolQueryValue
            // 
            this.toolQueryValue.Caption = "barEditItem2";
            this.toolQueryValue.Edit = this.repositoryItemTextEdit2;
            this.toolQueryValue.Id = 12;
            this.toolQueryValue.Name = "toolQueryValue";
            // 
            // repositoryItemTextEdit2
            // 
            this.repositoryItemTextEdit2.AutoHeight = false;
            this.repositoryItemTextEdit2.Name = "repositoryItemTextEdit2";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // toolLendInfo
            // 
            this.toolLendInfo.Caption = "    借阅信息";
            this.toolLendInfo.CheckBoxVisibility = DevExpress.XtraBars.CheckBoxVisibility.BeforeText;
            this.toolLendInfo.Id = 15;
            this.toolLendInfo.Name = "toolLendInfo";
            // 
            // panelCondition
            // 
            this.panelCondition.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.panelCondition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelCondition.Controls.Add(this.radioLendType);
            this.panelCondition.Controls.Add(this.labelControl2);
            this.panelCondition.Controls.Add(this.labelControl1);
            this.panelCondition.Controls.Add(this.dtTo);
            this.panelCondition.Controls.Add(this.dtFrom);
            this.panelCondition.Controls.Add(this.radioTimeSpan);
            this.panelCondition.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCondition.Location = new System.Drawing.Point(0, 31);
            this.panelCondition.Margin = new System.Windows.Forms.Padding(26, 4, 26, 4);
            this.panelCondition.Name = "panelCondition";
            this.panelCondition.Size = new System.Drawing.Size(983, 61);
            this.panelCondition.TabIndex = 76;
            // 
            // radioLendType
            // 
            this.radioLendType.Location = new System.Drawing.Point(804, 14);
            this.radioLendType.MenuManager = this.barManager1;
            this.radioLendType.Name = "radioLendType";
            this.radioLendType.Properties.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radioLendType.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioLendType.Properties.Appearance.Options.UseBackColor = true;
            this.radioLendType.Properties.Appearance.Options.UseFont = true;
            this.radioLendType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioLendType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("out", "借出未还"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("history", "借阅历史")});
            this.radioLendType.Size = new System.Drawing.Size(185, 33);
            this.radioLendType.TabIndex = 5;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(629, 23);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(16, 14);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "----";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelControl1.Location = new System.Drawing.Point(440, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 3;
            this.labelControl1.Text = "时间区间";
            // 
            // dtTo
            // 
            this.dtTo.EditValue = null;
            this.dtTo.Location = new System.Drawing.Point(660, 20);
            this.dtTo.MenuManager = this.barManager1;
            this.dtTo.Name = "dtTo";
            this.dtTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTo.Size = new System.Drawing.Size(110, 20);
            this.dtTo.TabIndex = 2;
            // 
            // dtFrom
            // 
            this.dtFrom.EditValue = null;
            this.dtFrom.Location = new System.Drawing.Point(504, 20);
            this.dtFrom.MenuManager = this.barManager1;
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtFrom.Size = new System.Drawing.Size(110, 20);
            this.dtFrom.TabIndex = 1;
            // 
            // radioTimeSpan
            // 
            this.radioTimeSpan.Location = new System.Drawing.Point(11, 14);
            this.radioTimeSpan.MenuManager = this.barManager1;
            this.radioTimeSpan.Name = "radioTimeSpan";
            this.radioTimeSpan.Properties.Appearance.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.radioTimeSpan.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radioTimeSpan.Properties.Appearance.Options.UseBackColor = true;
            this.radioTimeSpan.Properties.Appearance.Options.UseFont = true;
            this.radioTimeSpan.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioTimeSpan.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(30)), "近30天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(60)), "近60天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(90)), "近90天"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((short)(-1)), "自定义时间")});
            this.radioTimeSpan.Size = new System.Drawing.Size(394, 33);
            this.radioTimeSpan.TabIndex = 0;
            // 
            // gcArvInfo
            // 
            this.gcArvInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcArvInfo.Location = new System.Drawing.Point(0, 92);
            this.gcArvInfo.MainView = this.gridView1;
            this.gcArvInfo.MenuManager = this.barManager1;
            this.gcArvInfo.Name = "gcArvInfo";
            this.gcArvInfo.Size = new System.Drawing.Size(983, 381);
            this.gcArvInfo.TabIndex = 77;
            this.gcArvInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gcArvInfo;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // FormRPQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 473);
            this.Controls.Add(this.gcArvInfo);
            this.Controls.Add(this.panelCondition);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "FormRPQuery";
            this.Text = "FormRPArvQuery";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.panelCondition.ResumeLayout(false);
            this.panelCondition.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.radioLendType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioTimeSpan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcArvInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar toolBar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem toolMutilEdit;
        private DevExpress.XtraBars.BarButtonItem toolDelete;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarButtonItem toolExport;
        private DevExpress.XtraBars.BarButtonItem toolPrint;
        private DevExpress.XtraBars.BarStaticItem barStaticItem2;
        private DevExpress.XtraBars.BarStaticItem barStaticItem3;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraBars.BarButtonItem toolQuery;
        private DevExpress.XtraBars.BarEditItem toolQueryKey;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBox1;
        private DevExpress.XtraBars.BarEditItem toolQueryValue;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit2;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem4;
        private DevExpress.XtraBars.BarCheckItem toolLendInfo;
        private System.Windows.Forms.Panel panelCondition;
        private DevExpress.XtraEditors.RadioGroup radioLendType;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.DateEdit dtTo;
        private DevExpress.XtraEditors.DateEdit dtFrom;
        private DevExpress.XtraEditors.RadioGroup radioTimeSpan;
        private DevExpress.XtraGrid.GridControl gcArvInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
    }
}