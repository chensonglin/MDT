namespace MDT.Console
{
    partial class UCTask
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCTask));
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.grdTask = new DevExpress.XtraGrid.GridControl();
            this.gvTask = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolTaskName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDiscription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolOperator = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolOperateDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDuration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolIsEnable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkIsEnable = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcolCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbtnQuery = new System.Windows.Forms.ToolStripButton();
            this.tsbtnModify = new System.Windows.Forms.ToolStripButton();
            this.tsbtnAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDelete = new System.Windows.Forms.ToolStripButton();
            this.appointmentResourcesEdit1 = new DevExpress.XtraScheduler.UI.AppointmentResourcesEdit();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.cboTaskName = new DevExpress.XtraEditors.ComboBoxEdit();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.chkStatus = new DevExpress.XtraEditors.CheckEdit();
            this.chkTaskName = new DevExpress.XtraEditors.CheckEdit();
            this.chkEnableTask = new DevExpress.XtraEditors.CheckEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTask)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsEnable)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentResourcesEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboTaskName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStatus.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTaskName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableTask.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridView1.GridControl = this.grdTask;
            this.gridView1.Name = "gridView1";
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            // 
            // grdTask
            // 
            this.grdTask.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTask.Location = new System.Drawing.Point(0, 0);
            this.grdTask.MainView = this.gvTask;
            this.grdTask.Name = "grdTask";
            this.grdTask.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkIsEnable});
            this.grdTask.Size = new System.Drawing.Size(856, 460);
            this.grdTask.TabIndex = 68;
            this.grdTask.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTask,
            this.gridView1});
            // 
            // gvTask
            // 
            this.gvTask.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvTask.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvTask.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvTask.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvTask.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvTask.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gvTask.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.gvTask.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gvTask.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvTask.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvTask.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvTask.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvTask.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvTask.Appearance.Empty.Options.UseBackColor = true;
            this.gvTask.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvTask.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvTask.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvTask.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvTask.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvTask.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvTask.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvTask.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gvTask.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gvTask.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvTask.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvTask.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gvTask.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvTask.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvTask.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.gvTask.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvTask.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvTask.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvTask.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvTask.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvTask.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvTask.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvTask.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvTask.Appearance.GroupButton.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gvTask.Appearance.GroupButton.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gvTask.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvTask.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvTask.Appearance.GroupButton.Options.UseForeColor = true;
            this.gvTask.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvTask.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvTask.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvTask.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvTask.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvTask.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gvTask.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvTask.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvTask.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvTask.Appearance.GroupRow.BorderColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.gvTask.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gvTask.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.GroupRow.Options.UseBackColor = true;
            this.gvTask.Appearance.GroupRow.Options.UseBorderColor = true;
            this.gvTask.Appearance.GroupRow.Options.UseFont = true;
            this.gvTask.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvTask.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvTask.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvTask.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvTask.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvTask.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvTask.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvTask.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.gvTask.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.gvTask.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvTask.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvTask.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gvTask.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.OddRow.Options.UseBackColor = true;
            this.gvTask.Appearance.OddRow.Options.UseForeColor = true;
            this.gvTask.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gvTask.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.gvTask.Appearance.Preview.Options.UseBackColor = true;
            this.gvTask.Appearance.Preview.Options.UseForeColor = true;
            this.gvTask.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gvTask.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gvTask.Appearance.Row.Options.UseBackColor = true;
            this.gvTask.Appearance.Row.Options.UseForeColor = true;
            this.gvTask.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gvTask.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvTask.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.gvTask.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvTask.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvTask.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvTask.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolID,
            this.gcolTaskName,
            this.gcolDiscription,
            this.gcolType,
            this.gcolOperator,
            this.gcolOperateDate,
            this.gcolDuration,
            this.gcolIsEnable,
            this.gcolCategory});
            this.gvTask.GridControl = this.grdTask;
            this.gvTask.GroupCount = 1;
            this.gvTask.Name = "gvTask";
            this.gvTask.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvTask.OptionsView.EnableAppearanceEvenRow = true;
            this.gvTask.OptionsView.EnableAppearanceOddRow = true;
            this.gvTask.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.gvTask.OptionsView.ShowGroupedColumns = true;
            this.gvTask.OptionsView.ShowGroupPanel = false;
            this.gvTask.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.gcolCategory, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gvTask.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvTask_RowCellClick);
            this.gvTask.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvTask_CellValueChanged);
            // 
            // gcolID
            // 
            this.gcolID.AppearanceCell.Options.UseTextOptions = true;
            this.gcolID.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcolID.AppearanceHeader.Options.UseTextOptions = true;
            this.gcolID.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcolID.Caption = "ID";
            this.gcolID.FieldName = "ID";
            this.gcolID.MinWidth = 50;
            this.gcolID.Name = "gcolID";
            this.gcolID.OptionsColumn.AllowEdit = false;
            this.gcolID.Visible = true;
            this.gcolID.VisibleIndex = 0;
            this.gcolID.Width = 69;
            // 
            // gcolTaskName
            // 
            this.gcolTaskName.Caption = "任务名称";
            this.gcolTaskName.FieldName = "TaskName";
            this.gcolTaskName.Name = "gcolTaskName";
            this.gcolTaskName.Visible = true;
            this.gcolTaskName.VisibleIndex = 1;
            this.gcolTaskName.Width = 198;
            // 
            // gcolDiscription
            // 
            this.gcolDiscription.Caption = "任务描述";
            this.gcolDiscription.FieldName = "Note";
            this.gcolDiscription.Name = "gcolDiscription";
            this.gcolDiscription.Visible = true;
            this.gcolDiscription.VisibleIndex = 2;
            this.gcolDiscription.Width = 133;
            // 
            // gcolType
            // 
            this.gcolType.Caption = "任务类型";
            this.gcolType.FieldName = "Type";
            this.gcolType.MaxWidth = 100;
            this.gcolType.MinWidth = 10;
            this.gcolType.Name = "gcolType";
            this.gcolType.OptionsColumn.AllowEdit = false;
            this.gcolType.Visible = true;
            this.gcolType.VisibleIndex = 3;
            this.gcolType.Width = 55;
            // 
            // gcolOperator
            // 
            this.gcolOperator.AppearanceCell.Options.UseTextOptions = true;
            this.gcolOperator.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcolOperator.AppearanceHeader.Options.UseTextOptions = true;
            this.gcolOperator.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcolOperator.Caption = "操作人";
            this.gcolOperator.FieldName = "OperationID";
            this.gcolOperator.MaxWidth = 100;
            this.gcolOperator.MinWidth = 10;
            this.gcolOperator.Name = "gcolOperator";
            this.gcolOperator.OptionsColumn.AllowEdit = false;
            this.gcolOperator.Visible = true;
            this.gcolOperator.VisibleIndex = 4;
            // 
            // gcolOperateDate
            // 
            this.gcolOperateDate.Caption = "操作日期";
            this.gcolOperateDate.DisplayFormat.FormatString = "yyyy-MM-dd HH:mm:dd";
            this.gcolOperateDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gcolOperateDate.FieldName = "OperationDate";
            this.gcolOperateDate.MaxWidth = 150;
            this.gcolOperateDate.MinWidth = 150;
            this.gcolOperateDate.Name = "gcolOperateDate";
            this.gcolOperateDate.OptionsColumn.AllowEdit = false;
            this.gcolOperateDate.Visible = true;
            this.gcolOperateDate.VisibleIndex = 5;
            this.gcolOperateDate.Width = 150;
            // 
            // gcolDuration
            // 
            this.gcolDuration.Caption = "时间间隔";
            this.gcolDuration.FieldName = "Interval";
            this.gcolDuration.MaxWidth = 75;
            this.gcolDuration.MinWidth = 75;
            this.gcolDuration.Name = "gcolDuration";
            this.gcolDuration.Visible = true;
            this.gcolDuration.VisibleIndex = 6;
            // 
            // gcolIsEnable
            // 
            this.gcolIsEnable.AppearanceCell.Options.UseTextOptions = true;
            this.gcolIsEnable.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gcolIsEnable.Caption = "是否启用";
            this.gcolIsEnable.ColumnEdit = this.chkIsEnable;
            this.gcolIsEnable.FieldName = "Enable";
            this.gcolIsEnable.MaxWidth = 70;
            this.gcolIsEnable.MinWidth = 70;
            this.gcolIsEnable.Name = "gcolIsEnable";
            this.gcolIsEnable.Visible = true;
            this.gcolIsEnable.VisibleIndex = 7;
            this.gcolIsEnable.Width = 70;
            // 
            // chkIsEnable
            // 
            this.chkIsEnable.AutoHeight = false;
            this.chkIsEnable.Name = "chkIsEnable";
            this.chkIsEnable.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkIsEnable.ValueGrayed = false;
            // 
            // gcolCategory
            // 
            this.gcolCategory.Caption = "平台";
            this.gcolCategory.FieldName = "Category";
            this.gcolCategory.Name = "gcolCategory";
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AddNewItem = null;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = null;
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.tsbtnRefresh,
            this.tsbtnQuery,
            this.tsbtnModify,
            this.tsbtnAdd,
            this.tsbtnDelete});
            this.bindingNavigator1.Location = new System.Drawing.Point(0, 0);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(856, 25);
            this.bindingNavigator1.TabIndex = 67;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(32, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
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
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
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
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
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
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tsbtnRefresh
            // 
            this.tsbtnRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnRefresh.Image")));
            this.tsbtnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
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
            this.tsbtnQuery.Visible = false;
            this.tsbtnQuery.Click += new System.EventHandler(this.tsbtnQuery_Click);
            // 
            // tsbtnModify
            // 
            this.tsbtnModify.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnModify.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnModify.Image")));
            this.tsbtnModify.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnModify.Name = "tsbtnModify";
            this.tsbtnModify.Size = new System.Drawing.Size(23, 22);
            this.tsbtnModify.Text = "编辑";
            this.tsbtnModify.Click += new System.EventHandler(this.tsbtnModify_Click);
            // 
            // tsbtnAdd
            // 
            this.tsbtnAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnAdd.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAdd.Image")));
            this.tsbtnAdd.Name = "tsbtnAdd";
            this.tsbtnAdd.RightToLeftAutoMirrorImage = true;
            this.tsbtnAdd.Size = new System.Drawing.Size(23, 22);
            this.tsbtnAdd.Text = "添加";
            this.tsbtnAdd.Click += new System.EventHandler(this.tsbtnAdd_Click);
            // 
            // tsbtnDelete
            // 
            this.tsbtnDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbtnDelete.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDelete.Image")));
            this.tsbtnDelete.Name = "tsbtnDelete";
            this.tsbtnDelete.RightToLeftAutoMirrorImage = true;
            this.tsbtnDelete.Size = new System.Drawing.Size(23, 22);
            this.tsbtnDelete.Text = "删除";
            this.tsbtnDelete.Click += new System.EventHandler(this.tsbtnDelete_Click);
            // 
            // appointmentResourcesEdit1
            // 
            this.appointmentResourcesEdit1.EditValue = "(None)";
            this.appointmentResourcesEdit1.Location = new System.Drawing.Point(0, 0);
            this.appointmentResourcesEdit1.Name = "appointmentResourcesEdit1";
            this.appointmentResourcesEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.appointmentResourcesEdit1.Size = new System.Drawing.Size(100, 21);
            this.appointmentResourcesEdit1.TabIndex = 0;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.cboTaskName);
            this.splitContainer1.Panel1.Controls.Add(this.textEdit1);
            this.splitContainer1.Panel1.Controls.Add(this.chkStatus);
            this.splitContainer1.Panel1.Controls.Add(this.chkTaskName);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdTask);
            this.splitContainer1.Size = new System.Drawing.Size(856, 497);
            this.splitContainer1.SplitterDistance = 33;
            this.splitContainer1.TabIndex = 70;
            // 
            // cboTaskName
            // 
            this.cboTaskName.Location = new System.Drawing.Point(84, 9);
            this.cboTaskName.Name = "cboTaskName";
            this.cboTaskName.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTaskName.Size = new System.Drawing.Size(311, 21);
            this.cboTaskName.TabIndex = 17;
            this.cboTaskName.EditValueChanged += new System.EventHandler(this.cboTaskName_EditValueChanged);
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(540, 9);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Size = new System.Drawing.Size(255, 21);
            this.textEdit1.TabIndex = 15;
            // 
            // chkStatus
            // 
            this.chkStatus.Location = new System.Drawing.Point(448, 10);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Properties.Caption = "数据源名称";
            this.chkStatus.Size = new System.Drawing.Size(89, 19);
            this.chkStatus.TabIndex = 14;
            // 
            // chkTaskName
            // 
            this.chkTaskName.Location = new System.Drawing.Point(6, 10);
            this.chkTaskName.Name = "chkTaskName";
            this.chkTaskName.Properties.Caption = "任务名称";
            this.chkTaskName.Size = new System.Drawing.Size(75, 19);
            this.chkTaskName.TabIndex = 12;
            // 
            // chkEnableTask
            // 
            this.chkEnableTask.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.chkEnableTask.EditValue = true;
            this.chkEnableTask.Location = new System.Drawing.Point(745, 2);
            this.chkEnableTask.Name = "chkEnableTask";
            this.chkEnableTask.Properties.Caption = "仅显示启用任务";
            this.chkEnableTask.Size = new System.Drawing.Size(106, 19);
            this.chkEnableTask.TabIndex = 18;
            this.chkEnableTask.CheckedChanged += new System.EventHandler(this.chkCheckEnable_CheckedChanged);
            // 
            // UCTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkEnableTask);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.bindingNavigator1);
            this.Name = "UCTask";
            this.Size = new System.Drawing.Size(856, 522);
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTask)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsEnable)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appointmentResourcesEdit1.Properties)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboTaskName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkStatus.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTaskName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkEnableTask.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton tsbtnAdd;
        private System.Windows.Forms.ToolStripButton tsbtnDelete;
        private System.Windows.Forms.ToolStripButton tsbtnModify;
        private System.Windows.Forms.ToolStripButton tsbtnRefresh;
        private DevExpress.XtraScheduler.UI.AppointmentResourcesEdit appointmentResourcesEdit1;
        private DevExpress.XtraGrid.GridControl grdTask;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTask;
        private DevExpress.XtraGrid.Columns.GridColumn gcolID;
        private DevExpress.XtraGrid.Columns.GridColumn gcolTaskName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDiscription;
        private DevExpress.XtraGrid.Columns.GridColumn gcolOperator;
        private DevExpress.XtraGrid.Columns.GridColumn gcolOperateDate;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDuration;
        private DevExpress.XtraGrid.Columns.GridColumn gcolIsEnable;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkIsEnable;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gcolCategory;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraEditors.CheckEdit chkTaskName;
        private DevExpress.XtraEditors.CheckEdit chkStatus;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private System.Windows.Forms.ToolStripButton tsbtnQuery;
        private DevExpress.XtraEditors.ComboBoxEdit cboTaskName;
        private DevExpress.XtraEditors.CheckEdit chkEnableTask;
        private DevExpress.XtraGrid.Columns.GridColumn gcolType;
    }
}
