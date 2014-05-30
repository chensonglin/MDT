namespace MDT.Console
{
    partial class FormTaskSetting
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
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPostTaskName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboTTableNames = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cboTPrimaryKeys = new System.Windows.Forms.ComboBox();
            this.txtTaskName = new DevExpress.XtraEditors.TextEdit();
            this.txtTaskDescription = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cmbTaskType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cmbPlateForm = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit2 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.comboBoxEdit3 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.comboBoxEdit4 = new DevExpress.XtraEditors.ComboBoxEdit();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskDescription.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTaskType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlateForm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit4.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // simpleButton1
            // 
            this.simpleButton1.Location = new System.Drawing.Point(912, 32);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(75, 23);
            this.simpleButton1.TabIndex = 119;
            this.simpleButton1.Text = "添加任务";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.labelControl6);
            this.groupControl2.Controls.Add(this.comboBoxEdit4);
            this.groupControl2.Controls.Add(this.comboBoxEdit3);
            this.groupControl2.Controls.Add(this.labelControl5);
            this.groupControl2.Controls.Add(this.comboBoxEdit2);
            this.groupControl2.Controls.Add(this.comboBoxEdit1);
            this.groupControl2.Controls.Add(this.textBox2);
            this.groupControl2.Controls.Add(this.label8);
            this.groupControl2.Controls.Add(this.txtPostTaskName);
            this.groupControl2.Controls.Add(this.label4);
            this.groupControl2.Controls.Add(this.cboTTableNames);
            this.groupControl2.Controls.Add(this.label10);
            this.groupControl2.Controls.Add(this.label7);
            this.groupControl2.Controls.Add(this.cboTPrimaryKeys);
            this.groupControl2.Location = new System.Drawing.Point(10, 125);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(753, 348);
            this.groupControl2.TabIndex = 121;
            this.groupControl2.Text = "目标数据信息";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(41, 239);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(238, 21);
            this.textBox2.TabIndex = 121;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(41, 223);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 12);
            this.label8.TabIndex = 122;
            this.label8.Text = "执行条件（WHERE）：";
            // 
            // txtPostTaskName
            // 
            this.txtPostTaskName.Location = new System.Drawing.Point(41, 296);
            this.txtPostTaskName.Name = "txtPostTaskName";
            this.txtPostTaskName.Size = new System.Drawing.Size(238, 21);
            this.txtPostTaskName.TabIndex = 107;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(39, 280);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 112;
            this.label4.Text = "事后任务名称：";
            // 
            // cboTTableNames
            // 
            this.cboTTableNames.FormattingEnabled = true;
            this.cboTTableNames.Location = new System.Drawing.Point(42, 122);
            this.cboTTableNames.Name = "cboTTableNames";
            this.cboTTableNames.Size = new System.Drawing.Size(238, 20);
            this.cboTTableNames.TabIndex = 104;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(40, 164);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(59, 12);
            this.label10.TabIndex = 109;
            this.label10.Text = "主/外键：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(40, 105);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 12);
            this.label7.TabIndex = 108;
            this.label7.Text = "表名：";
            // 
            // cboTPrimaryKeys
            // 
            this.cboTPrimaryKeys.FormattingEnabled = true;
            this.cboTPrimaryKeys.Location = new System.Drawing.Point(42, 180);
            this.cboTPrimaryKeys.Name = "cboTPrimaryKeys";
            this.cboTPrimaryKeys.Size = new System.Drawing.Size(238, 20);
            this.cboTPrimaryKeys.TabIndex = 105;
            // 
            // txtTaskName
            // 
            this.txtTaskName.Location = new System.Drawing.Point(113, 39);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(243, 21);
            this.txtTaskName.TabIndex = 122;
            // 
            // txtTaskDescription
            // 
            this.txtTaskDescription.Location = new System.Drawing.Point(445, 40);
            this.txtTaskDescription.Name = "txtTaskDescription";
            this.txtTaskDescription.Size = new System.Drawing.Size(243, 21);
            this.txtTaskDescription.TabIndex = 123;
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(32, 42);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 14);
            this.labelControl1.TabIndex = 124;
            this.labelControl1.Text = "任务名称：";
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(379, 42);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(60, 14);
            this.labelControl2.TabIndex = 125;
            this.labelControl2.Text = "任务描述：";
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(32, 76);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 14);
            this.labelControl3.TabIndex = 126;
            this.labelControl3.Text = "任务类型：";
            // 
            // cmbTaskType
            // 
            this.cmbTaskType.Location = new System.Drawing.Point(113, 74);
            this.cmbTaskType.Name = "cmbTaskType";
            this.cmbTaskType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbTaskType.Size = new System.Drawing.Size(243, 21);
            this.cmbTaskType.TabIndex = 127;
            // 
            // cmbPlateForm
            // 
            this.cmbPlateForm.Location = new System.Drawing.Point(445, 75);
            this.cmbPlateForm.Name = "cmbPlateForm";
            this.cmbPlateForm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbPlateForm.Size = new System.Drawing.Size(243, 21);
            this.cmbPlateForm.TabIndex = 128;
            // 
            // labelControl4
            // 
            this.labelControl4.Location = new System.Drawing.Point(379, 77);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(60, 14);
            this.labelControl4.TabIndex = 129;
            this.labelControl4.Text = "任务分类：";
            // 
            // groupControl3
            // 
            this.groupControl3.CaptionLocation = DevExpress.Utils.Locations.Top;
            this.groupControl3.Controls.Add(this.labelControl1);
            this.groupControl3.Controls.Add(this.txtTaskName);
            this.groupControl3.Controls.Add(this.labelControl4);
            this.groupControl3.Controls.Add(this.txtTaskDescription);
            this.groupControl3.Controls.Add(this.cmbPlateForm);
            this.groupControl3.Controls.Add(this.labelControl2);
            this.groupControl3.Controls.Add(this.cmbTaskType);
            this.groupControl3.Controls.Add(this.labelControl3);
            this.groupControl3.Location = new System.Drawing.Point(11, 12);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(753, 112);
            this.groupControl3.TabIndex = 131;
            this.groupControl3.Text = "基本信息";
            // 
            // comboBoxEdit1
            // 
            this.comboBoxEdit1.Location = new System.Drawing.Point(113, 38);
            this.comboBoxEdit1.Name = "comboBoxEdit1";
            this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit1.Size = new System.Drawing.Size(243, 21);
            this.comboBoxEdit1.TabIndex = 130;
            // 
            // comboBoxEdit2
            // 
            this.comboBoxEdit2.Location = new System.Drawing.Point(445, 38);
            this.comboBoxEdit2.Name = "comboBoxEdit2";
            this.comboBoxEdit2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit2.Size = new System.Drawing.Size(243, 21);
            this.comboBoxEdit2.TabIndex = 131;
            // 
            // labelControl5
            // 
            this.labelControl5.Location = new System.Drawing.Point(369, 41);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(72, 14);
            this.labelControl5.TabIndex = 130;
            this.labelControl5.Text = "目标数据源：";
            // 
            // comboBoxEdit3
            // 
            this.comboBoxEdit3.Location = new System.Drawing.Point(114, 79);
            this.comboBoxEdit3.Name = "comboBoxEdit3";
            this.comboBoxEdit3.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit3.Size = new System.Drawing.Size(243, 21);
            this.comboBoxEdit3.TabIndex = 132;
            // 
            // comboBoxEdit4
            // 
            this.comboBoxEdit4.Location = new System.Drawing.Point(445, 79);
            this.comboBoxEdit4.Name = "comboBoxEdit4";
            this.comboBoxEdit4.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEdit4.Size = new System.Drawing.Size(243, 21);
            this.comboBoxEdit4.TabIndex = 133;
            // 
            // labelControl6
            // 
            this.labelControl6.Location = new System.Drawing.Point(37, 68);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(72, 14);
            this.labelControl6.TabIndex = 134;
            this.labelControl6.Text = "目标数据源：";
            // 
            // FormTaskSetting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1179, 496);
            this.Controls.Add(this.groupControl3);
            this.Controls.Add(this.groupControl2);
            this.Controls.Add(this.simpleButton1);
            this.Name = "FormTaskSetting";
            this.Text = "FormTaskSetting";
            this.Load += new System.EventHandler(this.FormTaskSetting_Load);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.groupControl2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTaskDescription.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbTaskType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbPlateForm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            this.groupControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit4.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPostTaskName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboTTableNames;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboTPrimaryKeys;
        private DevExpress.XtraEditors.TextEdit txtTaskName;
        private DevExpress.XtraEditors.TextEdit txtTaskDescription;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cmbTaskType;
        private DevExpress.XtraEditors.ComboBoxEdit cmbPlateForm;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit4;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit2;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
    }
}