namespace MDT.Console
{
    partial class UCTraceLog
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTraceLog));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiRepeatData = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiTask = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiRuninfo = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiData = new System.Windows.Forms.ToolStripMenuItem();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsTxtPageIndex = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.tslblInfo = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnQuery = new System.Windows.Forms.ToolStripButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpEndTime = new System.Windows.Forms.DateTimePicker();
            this.dtpStartTime = new System.Windows.Forms.DateTimePicker();
            this.cboTaskNames = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rbtnFailed = new System.Windows.Forms.RadioButton();
            this.rbtnSuccess = new System.Windows.Forms.RadioButton();
            this.chkTaskNames = new System.Windows.Forms.CheckBox();
            this.chkState = new System.Windows.Forms.CheckBox();
            this.chkDateTime = new System.Windows.Forms.CheckBox();
            this.customerId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Etask_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.taskname = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.processln = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.state = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.data = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.runInfo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.customerId,
            this.id,
            this.Etask_id,
            this.taskname,
            this.processln,
            this.stage,
            this.state,
            this.data,
            this.dataCount,
            this.startTime,
            this.EndTime,
            this.runInfo});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(932, 450);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellDoubleClick);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridView1.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_RowEnter);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiRepeatData,
            this.tsmiTask,
            this.tsmiRuninfo,
            this.tsmiData});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(119, 92);
            // 
            // tsmiRepeatData
            // 
            this.tsmiRepeatData.Name = "tsmiRepeatData";
            this.tsmiRepeatData.Size = new System.Drawing.Size(118, 22);
            this.tsmiRepeatData.Text = "重发数据";
            this.tsmiRepeatData.Click += new System.EventHandler(this.tsmiRepeatData_Click);
            // 
            // tsmiTask
            // 
            this.tsmiTask.Name = "tsmiTask";
            this.tsmiTask.Size = new System.Drawing.Size(118, 22);
            this.tsmiTask.Text = "任务信息";
            this.tsmiTask.Click += new System.EventHandler(this.tsmiTask_Click);
            // 
            // tsmiRuninfo
            // 
            this.tsmiRuninfo.Name = "tsmiRuninfo";
            this.tsmiRuninfo.Size = new System.Drawing.Size(118, 22);
            this.tsmiRuninfo.Text = "运行信息";
            this.tsmiRuninfo.Click += new System.EventHandler(this.tsmiRuninfo_Click);
            // 
            // tsmiData
            // 
            this.tsmiData.Name = "tsmiData";
            this.tsmiData.Size = new System.Drawing.Size(118, 22);
            this.tsmiData.Text = "数据信息";
            this.tsmiData.Click += new System.EventHandler(this.tsmiData_Click);
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = this.bindingNavigatorAddNewItem;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.bindingNavigatorDeleteItem;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator,
            this.toolStripButton1,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.tsTxtPageIndex,
            this.toolStripLabel1,
            this.tslblInfo,
            this.toolStripSeparator3,
            this.toolStripButton2,
            this.toolStripSeparator1,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem,
            this.tsbtnRefresh,
            this.tsbtnQuery});
            this.bindingNavigator1.Location = new System.Drawing.Point(5, 3);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(932, 25);
            this.bindingNavigator1.TabIndex = 1;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "添加";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(41, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "删除";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Margin = new System.Windows.Forms.Padding(1, 1, 0, 2);
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "上一页";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(17, 22);
            this.toolStripLabel2.Text = "第";
            // 
            // tsTxtPageIndex
            // 
            this.tsTxtPageIndex.AccessibleName = "Position";
            this.tsTxtPageIndex.AutoSize = false;
            this.tsTxtPageIndex.Name = "tsTxtPageIndex";
            this.tsTxtPageIndex.Size = new System.Drawing.Size(50, 21);
            this.tsTxtPageIndex.Text = "0";
            this.tsTxtPageIndex.ToolTipText = "Current position";
            this.tsTxtPageIndex.Leave += new System.EventHandler(this.tsTxtPageIndex_Leave);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(17, 22);
            this.toolStripLabel1.Text = "页";
            // 
            // tslblInfo
            // 
            this.tslblInfo.Margin = new System.Windows.Forms.Padding(3, 1, 0, 2);
            this.tslblInfo.Name = "tslblInfo";
            this.tslblInfo.Size = new System.Drawing.Size(47, 22);
            this.tslblInfo.Text = "共{0}页";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Margin = new System.Windows.Forms.Padding(1, 1, 0, 2);
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "下一页";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefresh.Image")));
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnRefresh.Margin = new System.Windows.Forms.Padding(1, 1, 0, 2);
            this.tsbtnRefresh.Name = "tsbtnRefresh";
            this.tsbtnRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsbtnRefresh.Text = "刷新";
            this.tsbtnRefresh.Click += new System.EventHandler(this.tsbtnRefresh_Click);
            // 
            // tsbtnQuery
            // 
            this.tsbtnQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnQuery.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnQuery.Image")));
            this.tsbtnQuery.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.tsbtnQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnQuery.Margin = new System.Windows.Forms.Padding(1, 1, 0, 2);
            this.tsbtnQuery.Name = "tsbtnQuery";
            this.tsbtnQuery.Size = new System.Drawing.Size(23, 22);
            this.tsbtnQuery.Text = "高级查询";
            this.tsbtnQuery.Click += new System.EventHandler(this.tsbtnQuery_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(745, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(17, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "→";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Location = new System.Drawing.Point(768, 9);
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(111, 21);
            this.dtpEndTime.TabIndex = 6;
            this.dtpEndTime.ValueChanged += new System.EventHandler(this.dtpEndTime_ValueChanged);
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Location = new System.Drawing.Point(628, 9);
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(111, 21);
            this.dtpStartTime.TabIndex = 5;
            this.dtpStartTime.ValueChanged += new System.EventHandler(this.dtpStartTime_ValueChanged);
            // 
            // cboTaskNames
            // 
            this.cboTaskNames.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTaskNames.FormattingEnabled = true;
            this.cboTaskNames.Location = new System.Drawing.Point(83, 10);
            this.cboTaskNames.Name = "cboTaskNames";
            this.cboTaskNames.Size = new System.Drawing.Size(226, 20);
            this.cboTaskNames.TabIndex = 1;
            this.cboTaskNames.SelectedValueChanged += new System.EventHandler(this.cboTaskNames_SelectedValueChanged);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(5, 28);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rbtnFailed);
            this.splitContainer1.Panel1.Controls.Add(this.rbtnSuccess);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.chkTaskNames);
            this.splitContainer1.Panel1.Controls.Add(this.dtpEndTime);
            this.splitContainer1.Panel1.Controls.Add(this.cboTaskNames);
            this.splitContainer1.Panel1.Controls.Add(this.dtpStartTime);
            this.splitContainer1.Panel1.Controls.Add(this.chkState);
            this.splitContainer1.Panel1.Controls.Add(this.chkDateTime);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dataGridView1);
            this.splitContainer1.Size = new System.Drawing.Size(932, 490);
            this.splitContainer1.SplitterDistance = 36;
            this.splitContainer1.TabIndex = 71;
            // 
            // rbtnFailed
            // 
            this.rbtnFailed.Location = new System.Drawing.Point(474, 12);
            this.rbtnFailed.Name = "rbtnFailed";
            this.rbtnFailed.Size = new System.Drawing.Size(47, 16);
            this.rbtnFailed.TabIndex = 9;
            this.rbtnFailed.Tag = "Failed";
            this.rbtnFailed.Text = "失败";
            this.rbtnFailed.UseVisualStyleBackColor = true;
            // 
            // rbtnSuccess
            // 
            this.rbtnSuccess.Checked = true;
            this.rbtnSuccess.Location = new System.Drawing.Point(420, 12);
            this.rbtnSuccess.Name = "rbtnSuccess";
            this.rbtnSuccess.Size = new System.Drawing.Size(47, 16);
            this.rbtnSuccess.TabIndex = 8;
            this.rbtnSuccess.TabStop = true;
            this.rbtnSuccess.Tag = "Success";
            this.rbtnSuccess.Text = "成功";
            this.rbtnSuccess.UseVisualStyleBackColor = true;
            this.rbtnSuccess.CheckedChanged += new System.EventHandler(this.rbtnSuccess_CheckedChanged);
            // 
            // chkTaskNames
            // 
            this.chkTaskNames.AutoSize = true;
            this.chkTaskNames.Location = new System.Drawing.Point(5, 12);
            this.chkTaskNames.Name = "chkTaskNames";
            this.chkTaskNames.Size = new System.Drawing.Size(72, 16);
            this.chkTaskNames.TabIndex = 0;
            this.chkTaskNames.Text = "任务名称";
            this.chkTaskNames.UseVisualStyleBackColor = true;
            this.chkTaskNames.Click += new System.EventHandler(this.chkSearch_Click);
            // 
            // chkState
            // 
            this.chkState.AutoSize = true;
            this.chkState.Location = new System.Drawing.Point(339, 12);
            this.chkState.Name = "chkState";
            this.chkState.Size = new System.Drawing.Size(72, 16);
            this.chkState.TabIndex = 2;
            this.chkState.Text = "执行状态";
            this.chkState.UseVisualStyleBackColor = true;
            this.chkState.Click += new System.EventHandler(this.chkSearch_Click);
            // 
            // chkDateTime
            // 
            this.chkDateTime.AutoSize = true;
            this.chkDateTime.Location = new System.Drawing.Point(549, 12);
            this.chkDateTime.Name = "chkDateTime";
            this.chkDateTime.Size = new System.Drawing.Size(72, 16);
            this.chkDateTime.TabIndex = 4;
            this.chkDateTime.Text = "操作日期";
            this.chkDateTime.UseVisualStyleBackColor = true;
            this.chkDateTime.Click += new System.EventHandler(this.chkSearch_Click);
            // 
            // customerId
            // 
            this.customerId.HeaderText = "客户ID";
            this.customerId.Name = "customerId";
            this.customerId.ReadOnly = true;
            this.customerId.Visible = false;
            // 
            // id
            // 
            this.id.DataPropertyName = "id";
            this.id.FillWeight = 31.05037F;
            this.id.HeaderText = "ID";
            this.id.Name = "id";
            this.id.ReadOnly = true;
            // 
            // Etask_id
            // 
            this.Etask_id.DataPropertyName = "Etask_id";
            this.Etask_id.HeaderText = "任务ID";
            this.Etask_id.Name = "Etask_id";
            this.Etask_id.ReadOnly = true;
            this.Etask_id.Visible = false;
            // 
            // taskname
            // 
            this.taskname.DataPropertyName = "taskname";
            this.taskname.FillWeight = 108.6763F;
            this.taskname.HeaderText = "任务名称";
            this.taskname.Name = "taskname";
            this.taskname.ReadOnly = true;
            // 
            // processln
            // 
            this.processln.DataPropertyName = "processln";
            this.processln.FillWeight = 120.3202F;
            this.processln.HeaderText = "处理批次";
            this.processln.Name = "processln";
            this.processln.ReadOnly = true;
            // 
            // stage
            // 
            this.stage.DataPropertyName = "stage";
            this.stage.FillWeight = 50.45685F;
            this.stage.HeaderText = "处理阶段";
            this.stage.Name = "stage";
            this.stage.ReadOnly = true;
            // 
            // state
            // 
            this.state.DataPropertyName = "state";
            this.state.FillWeight = 34.93167F;
            this.state.HeaderText = "执行状态";
            this.state.Name = "state";
            this.state.ReadOnly = true;
            // 
            // data
            // 
            this.data.DataPropertyName = "data";
            this.data.FillWeight = 55F;
            this.data.HeaderText = "数据信息";
            this.data.Name = "data";
            this.data.ReadOnly = true;
            // 
            // dataCount
            // 
            this.dataCount.DataPropertyName = "dataCount";
            this.dataCount.FillWeight = 31.05037F;
            this.dataCount.HeaderText = "总数量";
            this.dataCount.Name = "dataCount";
            this.dataCount.ReadOnly = true;
            // 
            // startTime
            // 
            this.startTime.DataPropertyName = "startTime";
            this.startTime.FillWeight = 50.45685F;
            this.startTime.HeaderText = "开始时间";
            this.startTime.Name = "startTime";
            this.startTime.ReadOnly = true;
            // 
            // EndTime
            // 
            this.EndTime.DataPropertyName = "EndTime";
            this.EndTime.FillWeight = 50.45685F;
            this.EndTime.HeaderText = "结束时间";
            this.EndTime.Name = "EndTime";
            this.EndTime.ReadOnly = true;
            // 
            // runInfo
            // 
            this.runInfo.DataPropertyName = "runInfo";
            this.runInfo.FillWeight = 77.62592F;
            this.runInfo.HeaderText = "运行信息";
            this.runInfo.Name = "runInfo";
            this.runInfo.ReadOnly = true;
            // 
            // UCTraceLogs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bindingNavigator1);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "UCTraceLogs";
            this.Padding = new System.Windows.Forms.Padding(5, 3, 5, 10);
            this.Size = new System.Drawing.Size(942, 528);
            this.Load += new System.EventHandler(this.UCTraceLogs_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorAddNewItem;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorDeleteItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel tslblInfo;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripTextBox tsTxtPageIndex;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiTask;
        private System.Windows.Forms.ToolStripMenuItem tsmiRuninfo;
        private System.Windows.Forms.ToolStripMenuItem tsmiData;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tsbtnQuery;
        private System.Windows.Forms.ToolStripMenuItem tsmiRepeatData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpEndTime;
        private System.Windows.Forms.DateTimePicker dtpStartTime;
        private System.Windows.Forms.ComboBox cboTaskNames;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RadioButton rbtnFailed;
        private System.Windows.Forms.RadioButton rbtnSuccess;
        private System.Windows.Forms.CheckBox chkTaskNames;
        private System.Windows.Forms.CheckBox chkState;
        private System.Windows.Forms.CheckBox chkDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn customerId;
        private System.Windows.Forms.DataGridViewTextBoxColumn id;
        private System.Windows.Forms.DataGridViewTextBoxColumn Etask_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn taskname;
        private System.Windows.Forms.DataGridViewTextBoxColumn processln;
        private System.Windows.Forms.DataGridViewTextBoxColumn stage;
        private System.Windows.Forms.DataGridViewTextBoxColumn state;
        private System.Windows.Forms.DataGridViewTextBoxColumn data;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataCount;
        private System.Windows.Forms.DataGridViewTextBoxColumn startTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn EndTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn runInfo;
    }
}
