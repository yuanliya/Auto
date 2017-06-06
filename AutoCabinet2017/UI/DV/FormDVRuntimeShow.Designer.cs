namespace HZK.UI.DV
{
    partial class FormDVRuntimeShow
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
            this.pbLeds = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDstLayer = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDevNo = new System.Windows.Forms.Label();
            this.lsbClipboard = new System.Windows.Forms.ListBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbLeds)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLeds
            // 
            this.pbLeds.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pbLeds.Location = new System.Drawing.Point(12, 31);
            this.pbLeds.Name = "pbLeds";
            this.pbLeds.Size = new System.Drawing.Size(836, 74);
            this.pbLeds.TabIndex = 0;
            this.pbLeds.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "当前层指示";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(9, 220);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(165, 32);
            this.label2.TabIndex = 2;
            this.label2.Text = "设定目标层：";
            // 
            // lblDstLayer
            // 
            this.lblDstLayer.AutoSize = true;
            this.lblDstLayer.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDstLayer.Location = new System.Drawing.Point(180, 220);
            this.lblDstLayer.Name = "lblDstLayer";
            this.lblDstLayer.Size = new System.Drawing.Size(31, 36);
            this.lblDstLayer.TabIndex = 3;
            this.lblDstLayer.Text = "1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(12, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(165, 32);
            this.label4.TabIndex = 4;
            this.label4.Text = "回转库编号：";
            // 
            // lblDevNo
            // 
            this.lblDevNo.AutoSize = true;
            this.lblDevNo.Font = new System.Drawing.Font("微软雅黑", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblDevNo.Location = new System.Drawing.Point(183, 153);
            this.lblDevNo.Name = "lblDevNo";
            this.lblDevNo.Size = new System.Drawing.Size(31, 36);
            this.lblDevNo.TabIndex = 5;
            this.lblDevNo.Text = "1";
            // 
            // lsbClipboard
            // 
            this.lsbClipboard.FormattingEnabled = true;
            this.lsbClipboard.ItemHeight = 15;
            this.lsbClipboard.Location = new System.Drawing.Point(251, 139);
            this.lsbClipboard.Name = "lsbClipboard";
            this.lsbClipboard.Size = new System.Drawing.Size(597, 139);
            this.lsbClipboard.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(248, 117);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(82, 15);
            this.label7.TabIndex = 8;
            this.label7.Text = "系统信息栏";
            // 
            // FormDVRuntimeShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 282);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lsbClipboard);
            this.Controls.Add(this.lblDevNo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblDstLayer);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pbLeds);
            this.Name = "FormDVRuntimeShow";
            this.Text = "回转库运行实时状态";
            this.Load += new System.EventHandler(this.FormDVRuntimeShow_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbLeds)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLeds;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDstLayer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDevNo;
        private System.Windows.Forms.ListBox lsbClipboard;
        private System.Windows.Forms.Label label7;
    }
}