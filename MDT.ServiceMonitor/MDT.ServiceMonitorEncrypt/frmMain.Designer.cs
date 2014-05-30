namespace MDT.ServiceMonitorEncrypt
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.label1 = new System.Windows.Forms.Label();
            this.txtPlaintext = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCiphertext = new System.Windows.Forms.TextBox();
            this.btnTrans = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnCopy = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnDecodeClear = new System.Windows.Forms.Button();
            this.btnDecodeCopy = new System.Windows.Forms.Button();
            this.btnDecodeTrans = new System.Windows.Forms.Button();
            this.btnDecodeClose = new System.Windows.Forms.Button();
            this.txtDecodeSecurtyText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDecodePlainText = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "明 文：";
            // 
            // txtPlaintext
            // 
            this.txtPlaintext.Location = new System.Drawing.Point(90, 20);
            this.txtPlaintext.Name = "txtPlaintext";
            this.txtPlaintext.Size = new System.Drawing.Size(675, 21);
            this.txtPlaintext.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 51);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(47, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "密 串：";
            // 
            // txtCiphertext
            // 
            this.txtCiphertext.Location = new System.Drawing.Point(90, 47);
            this.txtCiphertext.Name = "txtCiphertext";
            this.txtCiphertext.ReadOnly = true;
            this.txtCiphertext.Size = new System.Drawing.Size(675, 21);
            this.txtCiphertext.TabIndex = 1;
            this.txtCiphertext.TextChanged += new System.EventHandler(this.txtCiphertext_TextChanged);
            // 
            // btnTrans
            // 
            this.btnTrans.Location = new System.Drawing.Point(529, 97);
            this.btnTrans.Name = "btnTrans";
            this.btnTrans.Size = new System.Drawing.Size(75, 23);
            this.btnTrans.TabIndex = 1;
            this.btnTrans.Text = "转换(&T)";
            this.btnTrans.UseVisualStyleBackColor = true;
            this.btnTrans.Click += new System.EventHandler(this.btnTrans_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(691, 97);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "退出(&E)";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnCopy
            // 
            this.btnCopy.Location = new System.Drawing.Point(610, 97);
            this.btnCopy.Name = "btnCopy";
            this.btnCopy.Size = new System.Drawing.Size(75, 23);
            this.btnCopy.TabIndex = 2;
            this.btnCopy.Text = "复制(&C)";
            this.btnCopy.UseVisualStyleBackColor = true;
            this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(448, 97);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(75, 23);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "清空(&K)";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtPlaintext);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCiphertext);
            this.groupBox1.Controls.Add(this.btnClose);
            this.groupBox1.Controls.Add(this.btnTrans);
            this.groupBox1.Controls.Add(this.btnCopy);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Location = new System.Drawing.Point(21, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(812, 137);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "加密";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDecodePlainText);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.btnDecodeClear);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.btnDecodeCopy);
            this.groupBox2.Controls.Add(this.txtDecodeSecurtyText);
            this.groupBox2.Controls.Add(this.btnDecodeTrans);
            this.groupBox2.Controls.Add(this.btnDecodeClose);
            this.groupBox2.Location = new System.Drawing.Point(21, 170);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(812, 156);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "解密";
            // 
            // btnDecodeClear
            // 
            this.btnDecodeClear.Location = new System.Drawing.Point(451, 109);
            this.btnDecodeClear.Name = "btnDecodeClear";
            this.btnDecodeClear.Size = new System.Drawing.Size(75, 23);
            this.btnDecodeClear.TabIndex = 1;
            this.btnDecodeClear.Text = "清空(&K)";
            this.btnDecodeClear.UseVisualStyleBackColor = true;
            this.btnDecodeClear.Click += new System.EventHandler(this.btnDecodeClear_Click);
            // 
            // btnDecodeCopy
            // 
            this.btnDecodeCopy.Location = new System.Drawing.Point(613, 109);
            this.btnDecodeCopy.Name = "btnDecodeCopy";
            this.btnDecodeCopy.Size = new System.Drawing.Size(75, 23);
            this.btnDecodeCopy.TabIndex = 2;
            this.btnDecodeCopy.Text = "复制(&C)";
            this.btnDecodeCopy.UseVisualStyleBackColor = true;
            this.btnDecodeCopy.Click += new System.EventHandler(this.btnDecodeCopy_Click);
            // 
            // btnDecodeTrans
            // 
            this.btnDecodeTrans.Location = new System.Drawing.Point(532, 109);
            this.btnDecodeTrans.Name = "btnDecodeTrans";
            this.btnDecodeTrans.Size = new System.Drawing.Size(75, 23);
            this.btnDecodeTrans.TabIndex = 1;
            this.btnDecodeTrans.Text = "转换(&T)";
            this.btnDecodeTrans.UseVisualStyleBackColor = true;
            this.btnDecodeTrans.Click += new System.EventHandler(this.btnDecodeTrans_Click);
            // 
            // btnDecodeClose
            // 
            this.btnDecodeClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDecodeClose.Location = new System.Drawing.Point(694, 109);
            this.btnDecodeClose.Name = "btnDecodeClose";
            this.btnDecodeClose.Size = new System.Drawing.Size(75, 23);
            this.btnDecodeClose.TabIndex = 3;
            this.btnDecodeClose.Text = "退出(&E)";
            this.btnDecodeClose.UseVisualStyleBackColor = true;
            this.btnDecodeClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtDecodeSecurtyText
            // 
            this.txtDecodeSecurtyText.Location = new System.Drawing.Point(93, 59);
            this.txtDecodeSecurtyText.Name = "txtDecodeSecurtyText";
            this.txtDecodeSecurtyText.ReadOnly = true;
            this.txtDecodeSecurtyText.Size = new System.Drawing.Size(675, 21);
            this.txtDecodeSecurtyText.TabIndex = 1;
            this.txtDecodeSecurtyText.TextChanged += new System.EventHandler(this.txtCiphertext_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(34, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "明 文：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(34, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "密 文：";
            // 
            // txtDecodePlainText
            // 
            this.txtDecodePlainText.Location = new System.Drawing.Point(93, 32);
            this.txtDecodePlainText.Name = "txtDecodePlainText";
            this.txtDecodePlainText.Size = new System.Drawing.Size(675, 21);
            this.txtDecodePlainText.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AcceptButton = this.btnTrans;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(882, 365);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "字串加密解密工具";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtPlaintext;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCiphertext;
        private System.Windows.Forms.Button btnTrans;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnCopy;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDecodePlainText;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnDecodeClear;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDecodeCopy;
        private System.Windows.Forms.TextBox txtDecodeSecurtyText;
        private System.Windows.Forms.Button btnDecodeTrans;
        private System.Windows.Forms.Button btnDecodeClose;
    }
}