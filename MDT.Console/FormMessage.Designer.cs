namespace MDT.Console
{
    partial class FormMessage
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
            this.ucTextEditor1 = new MDT.Console.UCTextEditor();
            this.SuspendLayout();
            // 
            // ucTextEditor1
            // 
            this.ucTextEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTextEditor1.Location = new System.Drawing.Point(0, 0);
            this.ucTextEditor1.Message = "textEditorControl1";
            this.ucTextEditor1.Name = "ucTextEditor1";
            this.ucTextEditor1.Padding = new System.Windows.Forms.Padding(3);
            this.ucTextEditor1.Size = new System.Drawing.Size(648, 463);
            this.ucTextEditor1.TabIndex = 0;
            // 
            // FormMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(648, 463);
            this.Controls.Add(this.ucTextEditor1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormMessage";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "信息内容";
            this.ResumeLayout(false);

        }

        #endregion

        private UCTextEditor ucTextEditor1;


    }
}