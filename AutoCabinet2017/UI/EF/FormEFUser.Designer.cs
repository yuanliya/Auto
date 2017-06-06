namespace AutoCabinet2017.UI.EF
{
    partial class FormEFUser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormEFUser));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.gcUser = new DevExpress.XtraGrid.GridControl();
            this.gvUser = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemButtonEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.panelSearch = new System.Windows.Forms.Panel();
            this.lblLayerNo = new System.Windows.Forms.Label();
            this.lblGroupNo = new System.Windows.Forms.Label();
            this.btnAdvanced = new DevExpress.XtraEditors.SimpleButton();
            this.cbxUserDept = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cbxUserRole = new DevExpress.XtraEditors.ComboBoxEdit();
            this.ctrlPanel = new System.Windows.Forms.Panel();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.txtArvBoxID = new DevExpress.XtraEditors.TextEdit();
            this.lblArvBoxID = new System.Windows.Forms.Label();
            this.btnExpand = new DevExpress.XtraEditors.SimpleButton();
            this.btnSearch = new DevExpress.XtraEditors.SimpleButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gcSelect = new DevExpress.XtraGrid.GridControl();
            this.gvSelected = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).BeginInit();
            this.panelSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUserDept.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUserRole.Properties)).BeginInit();
            this.ctrlPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArvBoxID.Properties)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcSelect)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelected)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.gcUser);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 100);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(776, 191);
            this.groupBox1.TabIndex = 96;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "用户明细";
            // 
            // gcUser
            // 
            this.gcUser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcUser.Location = new System.Drawing.Point(3, 17);
            this.gcUser.MainView = this.gvUser;
            this.gcUser.Name = "gcUser";
            this.gcUser.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit2});
            this.gcUser.Size = new System.Drawing.Size(770, 171);
            this.gcUser.TabIndex = 1;
            this.gcUser.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUser});
            // 
            // gvUser
            // 
            this.gvUser.GridControl = this.gcUser;
            this.gvUser.Name = "gvUser";
            this.gvUser.OptionsSelection.MultiSelect = true;
            this.gvUser.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvUser.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gvUser.OptionsView.ShowGroupPanel = false;
            this.gvUser.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gvUser_SelectionChanged);
            // 
            // repositoryItemButtonEdit2
            // 
            this.repositoryItemButtonEdit2.AutoHeight = false;
            this.repositoryItemButtonEdit2.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph)});
            this.repositoryItemButtonEdit2.Name = "repositoryItemButtonEdit2";
            this.repositoryItemButtonEdit2.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // panelSearch
            // 
            this.panelSearch.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.panelSearch.Controls.Add(this.lblLayerNo);
            this.panelSearch.Controls.Add(this.lblGroupNo);
            this.panelSearch.Controls.Add(this.btnAdvanced);
            this.panelSearch.Controls.Add(this.cbxUserDept);
            this.panelSearch.Controls.Add(this.cbxUserRole);
            this.panelSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSearch.Location = new System.Drawing.Point(0, 53);
            this.panelSearch.Margin = new System.Windows.Forms.Padding(26, 4, 26, 4);
            this.panelSearch.Name = "panelSearch";
            this.panelSearch.Size = new System.Drawing.Size(776, 47);
            this.panelSearch.TabIndex = 94;
            // 
            // lblLayerNo
            // 
            this.lblLayerNo.AutoSize = true;
            this.lblLayerNo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblLayerNo.Location = new System.Drawing.Point(187, 20);
            this.lblLayerNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLayerNo.Name = "lblLayerNo";
            this.lblLayerNo.Size = new System.Drawing.Size(31, 14);
            this.lblLayerNo.TabIndex = 96;
            this.lblLayerNo.Tag = "UserRoleName";
            this.lblLayerNo.Text = "角色";
            // 
            // lblGroupNo
            // 
            this.lblGroupNo.AutoSize = true;
            this.lblGroupNo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGroupNo.Location = new System.Drawing.Point(23, 20);
            this.lblGroupNo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblGroupNo.Name = "lblGroupNo";
            this.lblGroupNo.Size = new System.Drawing.Size(55, 14);
            this.lblGroupNo.TabIndex = 94;
            this.lblGroupNo.Tag = "UserDept";
            this.lblGroupNo.Text = "用户部门";
            // 
            // btnAdvanced
            // 
            this.btnAdvanced.Image = ((System.Drawing.Image)(resources.GetObject("btnAdvanced.Image")));
            this.btnAdvanced.Location = new System.Drawing.Point(605, 6);
            this.btnAdvanced.Margin = new System.Windows.Forms.Padding(4);
            this.btnAdvanced.Name = "btnAdvanced";
            this.btnAdvanced.Size = new System.Drawing.Size(76, 38);
            this.btnAdvanced.TabIndex = 93;
            this.btnAdvanced.Text = "搜索";
            this.btnAdvanced.Click += new System.EventHandler(this.Button_Click);
            // 
            // cbxUserDept
            // 
            this.cbxUserDept.Location = new System.Drawing.Point(79, 12);
            this.cbxUserDept.Margin = new System.Windows.Forms.Padding(4);
            this.cbxUserDept.Name = "cbxUserDept";
            this.cbxUserDept.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxUserDept.Properties.Appearance.Options.UseFont = true;
            this.cbxUserDept.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.cbxUserDept.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.cbxUserDept.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxUserDept.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.cbxUserDept.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.cbxUserDept.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.cbxUserDept.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cbxUserDept.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxUserDept.Size = new System.Drawing.Size(81, 26);
            this.cbxUserDept.TabIndex = 95;
            this.cbxUserDept.Tag = "ArvBoxTitle";
            // 
            // cbxUserRole
            // 
            this.cbxUserRole.Location = new System.Drawing.Point(242, 11);
            this.cbxUserRole.Margin = new System.Windows.Forms.Padding(4);
            this.cbxUserRole.Name = "cbxUserRole";
            this.cbxUserRole.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.cbxUserRole.Properties.Appearance.Options.UseFont = true;
            this.cbxUserRole.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.cbxUserRole.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.cbxUserRole.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.cbxUserRole.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.cbxUserRole.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.cbxUserRole.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.cbxUserRole.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cbxUserRole.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbxUserRole.Size = new System.Drawing.Size(79, 26);
            this.cbxUserRole.TabIndex = 97;
            this.cbxUserRole.Tag = "ArvBoxTitle";
            // 
            // ctrlPanel
            // 
            this.ctrlPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(236)))), ((int)(((byte)(239)))));
            this.ctrlPanel.Controls.Add(this.btnCancel);
            this.ctrlPanel.Controls.Add(this.btnOK);
            this.ctrlPanel.Controls.Add(this.txtArvBoxID);
            this.ctrlPanel.Controls.Add(this.lblArvBoxID);
            this.ctrlPanel.Controls.Add(this.btnExpand);
            this.ctrlPanel.Controls.Add(this.btnSearch);
            this.ctrlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ctrlPanel.Location = new System.Drawing.Point(0, 0);
            this.ctrlPanel.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlPanel.Name = "ctrlPanel";
            this.ctrlPanel.Size = new System.Drawing.Size(776, 53);
            this.ctrlPanel.TabIndex = 95;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Image = ((System.Drawing.Image)(resources.GetObject("btnCancel.Image")));
            this.btnCancel.Location = new System.Drawing.Point(687, 9);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(76, 38);
            this.btnCancel.TabIndex = 87;
            this.btnCancel.Text = "取消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
            this.btnOK.Location = new System.Drawing.Point(605, 9);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(76, 38);
            this.btnOK.TabIndex = 86;
            this.btnOK.Text = "确定";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtArvBoxID
            // 
            this.txtArvBoxID.EnterMoveNextControl = true;
            this.txtArvBoxID.Location = new System.Drawing.Point(95, 14);
            this.txtArvBoxID.Margin = new System.Windows.Forms.Padding(4);
            this.txtArvBoxID.Name = "txtArvBoxID";
            this.txtArvBoxID.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F);
            this.txtArvBoxID.Properties.Appearance.Options.UseFont = true;
            this.txtArvBoxID.Properties.AppearanceFocused.BackColor = System.Drawing.Color.Yellow;
            this.txtArvBoxID.Properties.AppearanceFocused.Options.UseBackColor = true;
            this.txtArvBoxID.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txtArvBoxID.Properties.AppearanceReadOnly.BorderColor = System.Drawing.Color.Red;
            this.txtArvBoxID.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtArvBoxID.Properties.AppearanceReadOnly.Options.UseBorderColor = true;
            this.txtArvBoxID.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtArvBoxID.Size = new System.Drawing.Size(129, 26);
            this.txtArvBoxID.TabIndex = 85;
            this.txtArvBoxID.Tag = "UserCode";
            // 
            // lblArvBoxID
            // 
            this.lblArvBoxID.AutoSize = true;
            this.lblArvBoxID.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblArvBoxID.Location = new System.Drawing.Point(16, 19);
            this.lblArvBoxID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblArvBoxID.Name = "lblArvBoxID";
            this.lblArvBoxID.Size = new System.Drawing.Size(43, 14);
            this.lblArvBoxID.TabIndex = 84;
            this.lblArvBoxID.Tag = "UserCode";
            this.lblArvBoxID.Text = "登录名";
            // 
            // btnExpand
            // 
            this.btnExpand.Image = ((System.Drawing.Image)(resources.GetObject("btnExpand.Image")));
            this.btnExpand.ImageLocation = DevExpress.XtraEditors.ImageLocation.MiddleRight;
            this.btnExpand.Location = new System.Drawing.Point(351, 7);
            this.btnExpand.Margin = new System.Windows.Forms.Padding(4);
            this.btnExpand.Name = "btnExpand";
            this.btnExpand.Size = new System.Drawing.Size(100, 38);
            this.btnExpand.TabIndex = 15;
            this.btnExpand.Text = "高级搜索";
            this.btnExpand.Click += new System.EventHandler(this.Button_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Image = ((System.Drawing.Image)(resources.GetObject("btnSearch.Image")));
            this.btnSearch.Location = new System.Drawing.Point(243, 7);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(100, 38);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.Button_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gcSelect);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 291);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(776, 131);
            this.groupBox2.TabIndex = 97;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "已选择用户";
            // 
            // gcSelect
            // 
            this.gcSelect.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcSelect.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2, 1, 2, 1);
            this.gcSelect.Location = new System.Drawing.Point(4, 18);
            this.gcSelect.MainView = this.gvSelected;
            this.gcSelect.Margin = new System.Windows.Forms.Padding(4);
            this.gcSelect.Name = "gcSelect";
            this.gcSelect.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit1});
            this.gcSelect.Size = new System.Drawing.Size(768, 109);
            this.gcSelect.TabIndex = 0;
            this.gcSelect.Tag = "1";
            this.gcSelect.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSelected});
            // 
            // gvSelected
            // 
            this.gvSelected.Appearance.ColumnFilterButton.Options.UseTextOptions = true;
            this.gvSelected.Appearance.ColumnFilterButton.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSelected.Appearance.ColumnFilterButton.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvSelected.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.gvSelected.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSelected.Appearance.HeaderPanel.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvSelected.Appearance.ViewCaption.Options.UseTextOptions = true;
            this.gvSelected.Appearance.ViewCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gvSelected.Appearance.ViewCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.gvSelected.GridControl = this.gcSelect;
            this.gvSelected.Name = "gvSelected";
            this.gvSelected.OptionsMenu.EnableFooterMenu = false;
            this.gvSelected.OptionsMenu.EnableGroupPanelMenu = false;
            this.gvSelected.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gvSelected.OptionsSelection.ShowCheckBoxSelectorInColumnHeader = DevExpress.Utils.DefaultBoolean.True;
            this.gvSelected.OptionsView.ColumnAutoWidth = false;
            this.gvSelected.OptionsView.ShowGroupPanel = false;
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit1.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "预览", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit1.Buttons1"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "修改", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("repositoryItemButtonEdit1.Buttons2"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "删除", null, null, true)});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            this.repositoryItemButtonEdit1.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            // 
            // FormEFUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 434);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panelSearch);
            this.Controls.Add(this.ctrlPanel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormEFUser";
            this.Text = "选择人员";
            this.Load += new System.EventHandler(this.FormEFUser_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUser)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit2)).EndInit();
            this.panelSearch.ResumeLayout(false);
            this.panelSearch.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUserDept.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbxUserRole.Properties)).EndInit();
            this.ctrlPanel.ResumeLayout(false);
            this.ctrlPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtArvBoxID.Properties)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSelect)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSelected)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraGrid.GridControl gcUser;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUser;
        private System.Windows.Forms.Panel panelSearch;
        private System.Windows.Forms.Label lblLayerNo;
        private System.Windows.Forms.Label lblGroupNo;
        private DevExpress.XtraEditors.SimpleButton btnAdvanced;
        private DevExpress.XtraEditors.ComboBoxEdit cbxUserDept;
        private DevExpress.XtraEditors.ComboBoxEdit cbxUserRole;
        private System.Windows.Forms.Panel ctrlPanel;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraEditors.TextEdit txtArvBoxID;
        private System.Windows.Forms.Label lblArvBoxID;
        private DevExpress.XtraEditors.SimpleButton btnExpand;
        private DevExpress.XtraEditors.SimpleButton btnSearch;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevExpress.XtraGrid.GridControl gcSelect;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSelected;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit2;
    }
}