namespace MDT.Console
{
    partial class FormTraceLog
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
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gridTraceLog = new DevExpress.XtraGrid.GridControl();
            this.gvTraceLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.stage = new DevExpress.XtraGrid.Columns.GridColumn();
            this.status = new DevExpress.XtraGrid.Columns.GridColumn();
            this.data = new DevExpress.XtraGrid.Columns.GridColumn();
            this.data2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.starttime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.endtime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.runinfo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucTextEditor1 = new MDT.Console.UCTextEditor();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTraceLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTraceLog)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(5, 3);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gridTraceLog);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ucTextEditor1);
            this.splitContainer1.Size = new System.Drawing.Size(752, 601);
            this.splitContainer1.SplitterDistance = 201;
            this.splitContainer1.TabIndex = 3;
            // 
            // gridTraceLog
            // 
            this.gridTraceLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTraceLog.Location = new System.Drawing.Point(0, 0);
            this.gridTraceLog.MainView = this.gvTraceLog;
            this.gridTraceLog.Name = "gridTraceLog";
            this.gridTraceLog.Size = new System.Drawing.Size(752, 201);
            this.gridTraceLog.TabIndex = 2;
            this.gridTraceLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTraceLog});
            // 
            // gvTraceLog
            // 
            this.gvTraceLog.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gvTraceLog.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvTraceLog.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvTraceLog.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvTraceLog.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gvTraceLog.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvTraceLog.Appearance.OddRow.Options.UseBackColor = true;
            this.gvTraceLog.Appearance.OddRow.Options.UseForeColor = true;
            this.gvTraceLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.id,
            this.stage,
            this.status,
            this.data,
            this.data2,
            this.starttime,
            this.endtime,
            this.runinfo});
            this.gvTraceLog.GridControl = this.gridTraceLog;
            this.gvTraceLog.Name = "gvTraceLog";
            this.gvTraceLog.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTraceLog.OptionsView.EnableAppearanceOddRow = true;
            this.gvTraceLog.OptionsView.ShowGroupPanel = false;
            this.gvTraceLog.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gvTraceLog_FocusedRowChanged);
            // 
            // id
            // 
            this.id.Caption = "ID";
            this.id.FieldName = "ID";
            this.id.Name = "id";
            this.id.OptionsColumn.AllowEdit = false;
            this.id.Visible = true;
            this.id.VisibleIndex = 0;
            this.id.Width = 65;
            // 
            // stage
            // 
            this.stage.Caption = "处理阶段";
            this.stage.FieldName = "Stage";
            this.stage.Name = "stage";
            this.stage.OptionsColumn.AllowEdit = false;
            this.stage.Visible = true;
            this.stage.VisibleIndex = 1;
            this.stage.Width = 101;
            // 
            // status
            // 
            this.status.Caption = "执行状态";
            this.status.FieldName = "Status";
            this.status.Name = "status";
            this.status.OptionsColumn.AllowEdit = false;
            this.status.Visible = true;
            this.status.VisibleIndex = 2;
            this.status.Width = 77;
            // 
            // data
            // 
            this.data.Caption = "数据信息";
            this.data.FieldName = "Data";
            this.data.Name = "data";
            this.data.OptionsColumn.AllowEdit = false;
            // 
            // data2
            // 
            this.data2.Caption = "数据信息";
            this.data2.FieldName = "Data2";
            this.data2.Name = "data2";
            this.data2.Visible = true;
            this.data2.VisibleIndex = 3;
            this.data2.Width = 110;
            // 
            // starttime
            // 
            this.starttime.Caption = "开始时间";
            this.starttime.DisplayFormat.FormatString = "DateTime \"yyyy-MM-dd HH:mm:dd\"";
            this.starttime.FieldName = "StartTime";
            this.starttime.Name = "starttime";
            this.starttime.OptionsColumn.AllowEdit = false;
            this.starttime.Visible = true;
            this.starttime.VisibleIndex = 4;
            this.starttime.Width = 130;
            // 
            // endtime
            // 
            this.endtime.Caption = "结束时间";
            this.endtime.DisplayFormat.FormatString = "DateTime \"yyyy-MM-dd HH:mm:dd\"";
            this.endtime.FieldName = "EndTime";
            this.endtime.Name = "endtime";
            this.endtime.OptionsColumn.AllowEdit = false;
            this.endtime.Visible = true;
            this.endtime.VisibleIndex = 5;
            this.endtime.Width = 130;
            // 
            // runinfo
            // 
            this.runinfo.Caption = "运行信息";
            this.runinfo.FieldName = "RunInfo";
            this.runinfo.Name = "runinfo";
            this.runinfo.Visible = true;
            this.runinfo.VisibleIndex = 6;
            // 
            // ucTextEditor1
            // 
            this.ucTextEditor1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTextEditor1.Location = new System.Drawing.Point(0, 0);
            this.ucTextEditor1.Message = "textEditorControl1";
            this.ucTextEditor1.Name = "ucTextEditor1";
            this.ucTextEditor1.Padding = new System.Windows.Forms.Padding(3);
            this.ucTextEditor1.Size = new System.Drawing.Size(752, 396);
            this.ucTextEditor1.TabIndex = 2;
            // 
            // FormTraceLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 609);
            this.Controls.Add(this.splitContainer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "FormTraceLog";
            this.Padding = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "数据交换日志信息";
            this.Load += new System.EventHandler(this.FormTraceLogs_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTraceLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTraceLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private UCTextEditor ucTextEditor1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl gridTraceLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTraceLog;
        private DevExpress.XtraGrid.Columns.GridColumn id;
        private DevExpress.XtraGrid.Columns.GridColumn stage;
        private DevExpress.XtraGrid.Columns.GridColumn status;
        private DevExpress.XtraGrid.Columns.GridColumn data;
        private DevExpress.XtraGrid.Columns.GridColumn starttime;
        private DevExpress.XtraGrid.Columns.GridColumn endtime;
        private DevExpress.XtraGrid.Columns.GridColumn runinfo;
        private DevExpress.XtraGrid.Columns.GridColumn data2;
    }
}