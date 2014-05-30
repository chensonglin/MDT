namespace MDT.Console
{
    partial class UCTaskAllocation
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.gridClient = new DevExpress.XtraGrid.GridControl();
            this.gvClient = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ClientName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ServerIP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit2 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.gridTask = new DevExpress.XtraGrid.GridControl();
            this.gvTask = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.IsAllocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.TaskName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Note = new DevExpress.XtraGrid.Columns.GridColumn();
            this.Category = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl3 = new DevExpress.XtraEditors.GroupControl();
            this.sbtnSave = new DevExpress.XtraEditors.SimpleButton();
            this.checkEditAll = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).BeginInit();
            this.groupControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAll.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(576, 528);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(113, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "保存";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(856, 522);
            this.splitContainerControl1.SplitterPosition = 198;
            this.splitContainerControl1.TabIndex = 7;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.gridClient);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(198, 522);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "客户端列表";
            // 
            // gridClient
            // 
            this.gridClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridClient.Location = new System.Drawing.Point(2, 23);
            this.gridClient.MainView = this.gvClient;
            this.gridClient.Name = "gridClient";
            this.gridClient.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit2});
            this.gridClient.Size = new System.Drawing.Size(194, 497);
            this.gridClient.TabIndex = 8;
            this.gridClient.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvClient});
            // 
            // gvClient
            // 
            this.gvClient.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ID,
            this.ClientName,
            this.ServerIP});
            this.gvClient.GridControl = this.gridClient;
            this.gvClient.Name = "gvClient";
            this.gvClient.OptionsView.ShowGroupPanel = false;
            this.gvClient.OptionsView.ShowIndicator = false;
            this.gvClient.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvClient_FocusedRowChanged);
            // 
            // ID
            // 
            this.ID.Caption = "ID";
            this.ID.FieldName = "ID";
            this.ID.Name = "ID";
            this.ID.OptionsColumn.AllowEdit = false;
            // 
            // ClientName
            // 
            this.ClientName.Caption = "客户端名称";
            this.ClientName.FieldName = "Name";
            this.ClientName.Name = "ClientName";
            this.ClientName.OptionsColumn.AllowEdit = false;
            this.ClientName.Visible = true;
            this.ClientName.VisibleIndex = 0;
            this.ClientName.Width = 81;
            // 
            // ServerIP
            // 
            this.ServerIP.Caption = "客户端地址";
            this.ServerIP.FieldName = "ServerIP";
            this.ServerIP.Name = "ServerIP";
            this.ServerIP.OptionsColumn.AllowEdit = false;
            this.ServerIP.Visible = true;
            this.ServerIP.VisibleIndex = 1;
            this.ServerIP.Width = 105;
            // 
            // repositoryItemCheckEdit2
            // 
            this.repositoryItemCheckEdit2.AutoHeight = false;
            this.repositoryItemCheckEdit2.Name = "repositoryItemCheckEdit2";
            this.repositoryItemCheckEdit2.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.gridTask);
            this.groupControl2.Controls.Add(this.groupControl3);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(652, 522);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "任务列表";
            // 
            // gridTask
            // 
            this.gridTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTask.Location = new System.Drawing.Point(2, 23);
            this.gridTask.MainView = this.gvTask;
            this.gridTask.Name = "gridTask";
            this.gridTask.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemCheckEdit1});
            this.gridTask.Size = new System.Drawing.Size(648, 458);
            this.gridTask.TabIndex = 6;
            this.gridTask.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTask});
            // 
            // gvTask
            // 
            this.gvTask.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvTask.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvTask.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gvTask.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.OddRow.Options.UseBackColor = true;
            this.gvTask.Appearance.OddRow.Options.UseForeColor = true;
            this.gvTask.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.IsAllocation,
            this.TaskName,
            this.Note,
            this.Category});
            this.gvTask.GridControl = this.gridTask;
            this.gvTask.Name = "gvTask";
            this.gvTask.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTask.OptionsView.EnableAppearanceOddRow = true;
            this.gvTask.OptionsView.ShowGroupPanel = false;
            this.gvTask.OptionsView.ShowIndicator = false;
            // 
            // IsAllocation
            // 
            this.IsAllocation.Caption = "选项";
            this.IsAllocation.ColumnEdit = this.repositoryItemCheckEdit1;
            this.IsAllocation.FieldName = "IsAllocation";
            this.IsAllocation.Name = "IsAllocation";
            this.IsAllocation.Visible = true;
            this.IsAllocation.VisibleIndex = 0;
            this.IsAllocation.Width = 42;
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            this.repositoryItemCheckEdit1.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            // 
            // TaskName
            // 
            this.TaskName.Caption = "任务名称";
            this.TaskName.FieldName = "TaskName";
            this.TaskName.Name = "TaskName";
            this.TaskName.OptionsColumn.AllowEdit = false;
            this.TaskName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.TaskName.Visible = true;
            this.TaskName.VisibleIndex = 2;
            this.TaskName.Width = 266;
            // 
            // Note
            // 
            this.Note.Caption = "任务描述";
            this.Note.FieldName = "Note";
            this.Note.Name = "Note";
            this.Note.OptionsColumn.AllowEdit = false;
            this.Note.Visible = true;
            this.Note.VisibleIndex = 3;
            this.Note.Width = 239;
            // 
            // Category
            // 
            this.Category.Caption = "任务分类";
            this.Category.FieldName = "Category";
            this.Category.Name = "Category";
            this.Category.OptionsColumn.AllowEdit = false;
            this.Category.Visible = true;
            this.Category.VisibleIndex = 1;
            this.Category.Width = 109;
            // 
            // groupControl3
            // 
            this.groupControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.groupControl3.Controls.Add(this.sbtnSave);
            this.groupControl3.Controls.Add(this.checkEditAll);
            this.groupControl3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupControl3.Location = new System.Drawing.Point(2, 481);
            this.groupControl3.Name = "groupControl3";
            this.groupControl3.Size = new System.Drawing.Size(648, 39);
            this.groupControl3.TabIndex = 7;
            this.groupControl3.Text = "groupControl3";
            // 
            // sbtnSave
            // 
            this.sbtnSave.Location = new System.Drawing.Point(95, 9);
            this.sbtnSave.Name = "sbtnSave";
            this.sbtnSave.Size = new System.Drawing.Size(75, 23);
            this.sbtnSave.TabIndex = 6;
            this.sbtnSave.Text = "保存";
            this.sbtnSave.Click += new System.EventHandler(this.sbtnSave_Click);
            // 
            // checkEditAll
            // 
            this.checkEditAll.Location = new System.Drawing.Point(21, 11);
            this.checkEditAll.Name = "checkEditAll";
            this.checkEditAll.Properties.Caption = "全选";
            this.checkEditAll.Size = new System.Drawing.Size(75, 19);
            this.checkEditAll.TabIndex = 7;
            this.checkEditAll.CheckedChanged += new System.EventHandler(this.checkEdit1_CheckedChanged);
            // 
            // UCTaskAllocation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.button1);
            this.Name = "UCTaskAllocation";
            this.Size = new System.Drawing.Size(856, 522);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl3)).EndInit();
            this.groupControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditAll.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.GroupControl groupControl3;
        private DevExpress.XtraEditors.SimpleButton sbtnSave;
        private DevExpress.XtraGrid.GridControl gridTask;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTask;
        private DevExpress.XtraGrid.Columns.GridColumn TaskName;
        private DevExpress.XtraGrid.Columns.GridColumn Note;
        private DevExpress.XtraGrid.Columns.GridColumn IsAllocation;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn Category;
        private DevExpress.XtraGrid.GridControl gridClient;
        private DevExpress.XtraGrid.Views.Grid.GridView gvClient;
        private DevExpress.XtraGrid.Columns.GridColumn ClientName;
        private DevExpress.XtraGrid.Columns.GridColumn ServerIP;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit2;
        private DevExpress.XtraGrid.Columns.GridColumn ID;
        private DevExpress.XtraEditors.CheckEdit checkEditAll;
    }
}
