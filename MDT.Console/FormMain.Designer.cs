namespace MDT.Console
{
    partial class FormMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.fileSystemWatcher1 = new System.IO.FileSystemWatcher();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.navBarControl1 = new DevExpress.XtraNavBar.NavBarControl();
            this.navBarGroup1 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiTraceLog = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup2 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiDatabase = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiTaskAllocation = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiTask = new DevExpress.XtraNavBar.NavBarItem();
            this.navBarGroup3 = new DevExpress.XtraNavBar.NavBarGroup();
            this.nbiUser = new DevExpress.XtraNavBar.NavBarItem();
            this.nbiSecure = new DevExpress.XtraNavBar.NavBarItem();
            this.pnlRight = new DevExpress.XtraEditors.PanelControl();
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.lblTip = new DevExpress.XtraEditors.LabelControl();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.人 = new DevExpress.XtraNavBar.NavBarItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLblUserName = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusLblLastVistTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "配置管理-tb.png");
            this.imageList1.Images.SetKeyName(1, "任务列表-tb.png");
            // 
            // fileSystemWatcher1
            // 
            this.fileSystemWatcher1.EnableRaisingEvents = true;
            this.fileSystemWatcher1.SynchronizingObject = this;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(2, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.navBarControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.pnlRight);
            this.splitContainerControl1.Panel2.Controls.Add(this.pnlTop);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(848, 460);
            this.splitContainerControl1.SplitterPosition = 179;
            this.splitContainerControl1.TabIndex = 3;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // navBarControl1
            // 
            this.navBarControl1.ActiveGroup = this.navBarGroup1;
            this.navBarControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.navBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.navBarControl1.Groups.AddRange(new DevExpress.XtraNavBar.NavBarGroup[] {
            this.navBarGroup1,
            this.navBarGroup2,
            this.navBarGroup3});
            this.navBarControl1.Items.AddRange(new DevExpress.XtraNavBar.NavBarItem[] {
            this.nbiDatabase,
            this.nbiTask,
            this.nbiTaskAllocation,
            this.nbiTraceLog,
            this.nbiUser,
            this.nbiSecure});
            this.navBarControl1.Location = new System.Drawing.Point(0, 0);
            this.navBarControl1.Name = "navBarControl1";
            this.navBarControl1.OptionsNavPane.ExpandedWidth = 179;
            this.navBarControl1.Size = new System.Drawing.Size(179, 460);
            this.navBarControl1.TabIndex = 0;
            this.navBarControl1.Text = "navBarControl1";
            // 
            // navBarGroup1
            // 
            this.navBarGroup1.Caption = "日志管理";
            this.navBarGroup1.Expanded = true;
            this.navBarGroup1.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTraceLog)});
            this.navBarGroup1.Name = "navBarGroup1";
            // 
            // nbiTraceLog
            // 
            this.nbiTraceLog.Caption = "日志信息";
            this.nbiTraceLog.Name = "nbiTraceLog";
            this.nbiTraceLog.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiTraceLog.SmallImage")));
            this.nbiTraceLog.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbi_LinkClicked);
            // 
            // navBarGroup2
            // 
            this.navBarGroup2.Caption = "任务管理";
            this.navBarGroup2.Expanded = true;
            this.navBarGroup2.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiDatabase),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTaskAllocation),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiTask)});
            this.navBarGroup2.Name = "navBarGroup2";
            // 
            // nbiDatabase
            // 
            this.nbiDatabase.Caption = "数据源配置";
            this.nbiDatabase.Name = "nbiDatabase";
            this.nbiDatabase.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiDatabase.SmallImage")));
            this.nbiDatabase.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbi_LinkClicked);
            // 
            // nbiTaskAllocation
            // 
            this.nbiTaskAllocation.Caption = "任务分配";
            this.nbiTaskAllocation.Name = "nbiTaskAllocation";
            this.nbiTaskAllocation.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiTaskAllocation.SmallImage")));
            this.nbiTaskAllocation.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbi_LinkClicked);
            // 
            // nbiTask
            // 
            this.nbiTask.Caption = "任务配置";
            this.nbiTask.Name = "nbiTask";
            this.nbiTask.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiTask.SmallImage")));
            this.nbiTask.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbi_LinkClicked);
            // 
            // navBarGroup3
            // 
            this.navBarGroup3.Caption = "用户管理";
            this.navBarGroup3.Expanded = true;
            this.navBarGroup3.ItemLinks.AddRange(new DevExpress.XtraNavBar.NavBarItemLink[] {
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiUser),
            new DevExpress.XtraNavBar.NavBarItemLink(this.nbiSecure)});
            this.navBarGroup3.Name = "navBarGroup3";
            // 
            // nbiUser
            // 
            this.nbiUser.Caption = "用户中心";
            this.nbiUser.Name = "nbiUser";
            this.nbiUser.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiUser.SmallImage")));
            this.nbiUser.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbi_LinkClicked);
            // 
            // nbiSecure
            // 
            this.nbiSecure.Caption = "安全管理";
            this.nbiSecure.Name = "nbiSecure";
            this.nbiSecure.SmallImage = ((System.Drawing.Image)(resources.GetObject("nbiSecure.SmallImage")));
            this.nbiSecure.LinkClicked += new DevExpress.XtraNavBar.NavBarLinkEventHandler(this.nbi_LinkClicked);
            // 
            // pnlRight
            // 
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 84);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(664, 376);
            this.pnlRight.TabIndex = 5;
            // 
            // pnlTop
            // 
            this.pnlTop.Appearance.BackColor = System.Drawing.SystemColors.Control;
            this.pnlTop.Appearance.Options.UseBackColor = true;
            this.pnlTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTop.ContentImage = ((System.Drawing.Image)(resources.GetObject("pnlTop.ContentImage")));
            this.pnlTop.ContentImageAlignment = System.Drawing.ContentAlignment.MiddleRight;
            this.pnlTop.Controls.Add(this.lblTip);
            this.pnlTop.Controls.Add(this.pictureBox1);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(664, 84);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTip
            // 
            this.lblTip.Appearance.Font = new System.Drawing.Font("YouYuan", 22F);
            this.lblTip.Location = new System.Drawing.Point(8, 24);
            this.lblTip.Name = "lblTip";
            this.lblTip.Size = new System.Drawing.Size(120, 30);
            this.lblTip.TabIndex = 3;
            this.lblTip.Text = "日志信息";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(4822, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(280, 52);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // 人
            // 
            this.人.Caption = "配置管理";
            this.人.Name = "人";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLblUserName,
            this.toolStripStatusLabel1,
            this.statusLblLastVistTime,
            this.toolStripStatusLabel2});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.statusStrip1.Location = new System.Drawing.Point(2, 460);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(848, 22);
            this.statusStrip1.TabIndex = 4;
            // 
            // statusLblUserName
            // 
            this.statusLblUserName.BorderStyle = System.Windows.Forms.Border3DStyle.RaisedOuter;
            this.statusLblUserName.Image = global::MDT.Console.Properties.Resources.user;
            this.statusLblUserName.Name = "statusLblUserName";
            this.statusLblUserName.Size = new System.Drawing.Size(60, 17);
            this.statusLblUserName.Spring = true;
            this.statusLblUserName.Text = "admin";
            this.statusLblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // statusLblLastVistTime
            // 
            this.statusLblLastVistTime.Image = global::MDT.Console.Properties.Resources.time;
            this.statusLblLastVistTime.Name = "statusLblLastVistTime";
            this.statusLblLastVistTime.Size = new System.Drawing.Size(84, 17);
            this.statusLblLastVistTime.Spring = true;
            this.statusLblLastVistTime.Text = "第一次登陆";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.ClientSize = new System.Drawing.Size(852, 484);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.Name = "FormMain";
            this.Padding = new System.Windows.Forms.Padding(2, 0, 2, 2);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据交换平台MDT";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.fileSystemWatcher1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.navBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ImageList imageList1;
        private System.IO.FileSystemWatcher fileSystemWatcher1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraNavBar.NavBarItem 人;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusLblUserName;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel statusLblLastVistTime;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private DevExpress.XtraNavBar.NavBarControl navBarControl1;
        private DevExpress.XtraEditors.PanelControl pnlTop;
        private System.Windows.Forms.PictureBox pictureBox1;
        private DevExpress.XtraEditors.LabelControl lblTip;
        private DevExpress.XtraEditors.PanelControl pnlRight;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup1;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup2;
        private DevExpress.XtraNavBar.NavBarGroup navBarGroup3;
        private DevExpress.XtraNavBar.NavBarItem nbiDatabase;
        private DevExpress.XtraNavBar.NavBarItem nbiTask;
        private DevExpress.XtraNavBar.NavBarItem nbiTaskAllocation;
        private DevExpress.XtraNavBar.NavBarItem nbiTraceLog;
        private DevExpress.XtraNavBar.NavBarItem nbiUser;
        private DevExpress.XtraNavBar.NavBarItem nbiSecure;
    }
}

