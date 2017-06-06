namespace AutoCabinet2017.UI.OP
{
    partial class FormOPLocate
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
            this.lblLayerNo = new System.Windows.Forms.Label();
            this.lblGroupNo = new System.Windows.Forms.Label();
            this.cbxLayerNo = new System.Windows.Forms.ComboBox();
            this.cbxGroupNo = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLayerNo
            // 
            this.lblLayerNo.AutoSize = true;
            this.lblLayerNo.Location = new System.Drawing.Point(175, 24);
            this.lblLayerNo.Name = "lblLayerNo";
            this.lblLayerNo.Size = new System.Drawing.Size(29, 12);
            this.lblLayerNo.TabIndex = 58;
            this.lblLayerNo.Tag = "LayerNo";
            this.lblLayerNo.Text = "层号";
            // 
            // lblGroupNo
            // 
            this.lblGroupNo.AutoSize = true;
            this.lblGroupNo.Location = new System.Drawing.Point(5, 24);
            this.lblGroupNo.Name = "lblGroupNo";
            this.lblGroupNo.Size = new System.Drawing.Size(29, 12);
            this.lblGroupNo.TabIndex = 57;
            this.lblGroupNo.Tag = "GroupNo";
            this.lblGroupNo.Text = "编号";
            // 
            // cbxLayerNo
            // 
            this.cbxLayerNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxLayerNo.Enabled = false;
            this.cbxLayerNo.FormattingEnabled = true;
            this.cbxLayerNo.Location = new System.Drawing.Point(208, 19);
            this.cbxLayerNo.Name = "cbxLayerNo";
            this.cbxLayerNo.Size = new System.Drawing.Size(100, 20);
            this.cbxLayerNo.TabIndex = 55;
            this.cbxLayerNo.Tag = "LayerNo";
            // 
            // cbxGroupNo
            // 
            this.cbxGroupNo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxGroupNo.FormattingEnabled = true;
            this.cbxGroupNo.Location = new System.Drawing.Point(39, 20);
            this.cbxGroupNo.Name = "cbxGroupNo";
            this.cbxGroupNo.Size = new System.Drawing.Size(101, 20);
            this.cbxGroupNo.TabIndex = 54;
            this.cbxGroupNo.Tag = "GroupNo";
            this.cbxGroupNo.SelectedIndexChanged += new System.EventHandler(this.cbxGroupNo_SelectedIndexChanged);
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(154, 71);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 61;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(248, 70);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 62;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblGroupNo);
            this.groupBox1.Controls.Add(this.cbxLayerNo);
            this.groupBox1.Controls.Add(this.cbxGroupNo);
            this.groupBox1.Controls.Add(this.lblLayerNo);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(314, 54);
            this.groupBox1.TabIndex = 63;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "请选择存放位置";
            // 
            // FormOPLocate
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(329, 105);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Name = "FormOPLocate";
            this.Text = "位置选择";
            this.Load += new System.EventHandler(this.FormOPLocate_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblLayerNo;
        private System.Windows.Forms.Label lblGroupNo;
        private System.Windows.Forms.ComboBox cbxLayerNo;
        private System.Windows.Forms.ComboBox cbxGroupNo;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}