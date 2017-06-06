namespace AutoCabinet2017.UI.EF
{
    partial class FormEFDeviceInfo
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
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.txtDevCells = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtDevLayers = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtDevNo = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDevCells.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDevLayers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDevNo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(201, 163);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.txtDevCells);
            this.groupControl1.Controls.Add(this.labelControl3);
            this.groupControl1.Controls.Add(this.txtDevLayers);
            this.groupControl1.Controls.Add(this.labelControl2);
            this.groupControl1.Controls.Add(this.txtDevNo);
            this.groupControl1.Controls.Add(this.labelControl1);
            this.groupControl1.Location = new System.Drawing.Point(12, 12);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(264, 136);
            this.groupControl1.TabIndex = 7;
            this.groupControl1.Text = "设备信息";
            // 
            // txtDevCells
            // 
            this.txtDevCells.Location = new System.Drawing.Point(87, 103);
            this.txtDevCells.Name = "txtDevCells";
            this.txtDevCells.Size = new System.Drawing.Size(146, 20);
            this.txtDevCells.TabIndex = 5;
            this.txtDevCells.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDevCells_KeyPress);
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(16, 106);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(48, 14);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "设备格数";
            // 
            // txtDevLayers
            // 
            this.txtDevLayers.Location = new System.Drawing.Point(87, 67);
            this.txtDevLayers.Name = "txtDevLayers";
            this.txtDevLayers.Size = new System.Drawing.Size(146, 20);
            this.txtDevLayers.TabIndex = 3;
            this.txtDevLayers.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDevLayers_KeyPress);
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(16, 70);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(48, 14);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "设备层数";
            // 
            // txtDevNo
            // 
            this.txtDevNo.Location = new System.Drawing.Point(87, 32);
            this.txtDevNo.Name = "txtDevNo";
            this.txtDevNo.Size = new System.Drawing.Size(146, 20);
            this.txtDevNo.TabIndex = 1;
            this.txtDevNo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDevNo_KeyPress);
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(16, 35);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(48, 14);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "设备编号";
            // 
            // FormEFDeviceInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(291, 198);
            this.Controls.Add(this.groupControl1);
            this.Controls.Add(this.btnSave);
            this.Name = "FormEFDeviceInfo";
            this.Text = "设备信息编辑";
            this.Load += new System.EventHandler(this.FormEFDeviceInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            this.groupControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtDevCells.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDevLayers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDevNo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.TextEdit txtDevCells;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtDevLayers;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtDevNo;
        private DevExpress.XtraEditors.LabelControl labelControl1;

    }
}