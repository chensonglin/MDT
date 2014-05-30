namespace MDT.Console
{
    partial class FormTaskEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaskEdit));
            this.rbtnTaskInfo = new DevExpress.XtraEditors.RadioGroup();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnTaskInfo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // rbtnTaskInfo
            // 
            this.rbtnTaskInfo.Location = new System.Drawing.Point(14, 10);
            this.rbtnTaskInfo.Name = "rbtnTaskInfo";
            this.rbtnTaskInfo.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.rbtnTaskInfo.Properties.Appearance.Options.UseBackColor = true;
            this.rbtnTaskInfo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rbtnTaskInfo.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("Mapping", "Mapping"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("XSLTInfo", "XSLTInfo"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "源配置文件"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(null, "目标配置文件")});
            this.rbtnTaskInfo.Size = new System.Drawing.Size(469, 23);
            this.rbtnTaskInfo.TabIndex = 16;
            this.rbtnTaskInfo.SelectedIndexChanged += new System.EventHandler(this.rbtnTaskInfo_SelectedIndexChanged);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(534, 10);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 17;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Location = new System.Drawing.Point(12, 43);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 505);
            this.panel1.TabIndex = 6;
            // 
            // FormTaskEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(890, 560);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.rbtnTaskInfo);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTaskEdit";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "修改任务配置";
            this.Load += new System.EventHandler(this.FormTaskSet2_Load);
            ((System.ComponentModel.ISupportInitialize)(this.rbtnTaskInfo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.RadioGroup rbtnTaskInfo;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private System.Windows.Forms.Panel panel1;
    }
}