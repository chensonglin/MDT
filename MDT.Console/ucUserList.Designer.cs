namespace MDT.Console
{
    partial class UCUserList
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UCUserList));
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdUserList = new DevExpress.XtraGrid.GridControl();
            this.gvUserList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolUserID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolUserName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolUserMail = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolUserType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolHostName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolMacAddr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolIPAddr = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolIsLocked = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkIsLocked = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.gcolCreateBy = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolCreateTime = new DevExpress.XtraGrid.Columns.GridColumn();
            this.bingdingUser = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorDeleteItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorAddNewItem = new System.Windows.Forms.ToolStripButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsLocked)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bingdingUser)).BeginInit();
            this.bingdingUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdUserList);
            this.panel1.Controls.Add(this.bingdingUser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(876, 387);
            this.panel1.TabIndex = 0;
            // 
            // grdUserList
            // 
            this.grdUserList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUserList.Location = new System.Drawing.Point(0, 25);
            this.grdUserList.MainView = this.gvUserList;
            this.grdUserList.Name = "grdUserList";
            this.grdUserList.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.chkIsLocked});
            this.grdUserList.Size = new System.Drawing.Size(876, 362);
            this.grdUserList.TabIndex = 0;
            this.grdUserList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvUserList});
            // 
            // gvUserList
            // 
            this.gvUserList.Appearance.ColumnFilterButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.ColumnFilterButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvUserList.Appearance.ColumnFilterButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.ColumnFilterButton.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.ColumnFilterButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvUserList.Appearance.ColumnFilterButton.Options.UseBackColor = true;
            this.gvUserList.Appearance.ColumnFilterButton.Options.UseBorderColor = true;
            this.gvUserList.Appearance.ColumnFilterButton.Options.UseForeColor = true;
            this.gvUserList.Appearance.ColumnFilterButtonActive.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gvUserList.Appearance.ColumnFilterButtonActive.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(154)))), ((int)(((byte)(190)))), ((int)(((byte)(243)))));
            this.gvUserList.Appearance.ColumnFilterButtonActive.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(251)))), ((int)(((byte)(255)))));
            this.gvUserList.Appearance.ColumnFilterButtonActive.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.ColumnFilterButtonActive.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvUserList.Appearance.ColumnFilterButtonActive.Options.UseBackColor = true;
            this.gvUserList.Appearance.ColumnFilterButtonActive.Options.UseBorderColor = true;
            this.gvUserList.Appearance.ColumnFilterButtonActive.Options.UseForeColor = true;
            this.gvUserList.Appearance.Empty.BackColor = System.Drawing.Color.White;
            this.gvUserList.Appearance.Empty.Options.UseBackColor = true;
            this.gvUserList.Appearance.EvenRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(242)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.EvenRow.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.EvenRow.Options.UseBackColor = true;
            this.gvUserList.Appearance.EvenRow.Options.UseForeColor = true;
            this.gvUserList.Appearance.FilterCloseButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.FilterCloseButton.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvUserList.Appearance.FilterCloseButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.FilterCloseButton.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.FilterCloseButton.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvUserList.Appearance.FilterCloseButton.Options.UseBackColor = true;
            this.gvUserList.Appearance.FilterCloseButton.Options.UseBorderColor = true;
            this.gvUserList.Appearance.FilterCloseButton.Options.UseForeColor = true;
            this.gvUserList.Appearance.FilterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gvUserList.Appearance.FilterPanel.ForeColor = System.Drawing.Color.White;
            this.gvUserList.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvUserList.Appearance.FilterPanel.Options.UseForeColor = true;
            this.gvUserList.Appearance.FixedLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(97)))), ((int)(((byte)(156)))));
            this.gvUserList.Appearance.FixedLine.Options.UseBackColor = true;
            this.gvUserList.Appearance.FocusedCell.BackColor = System.Drawing.Color.White;
            this.gvUserList.Appearance.FocusedCell.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvUserList.Appearance.FocusedCell.Options.UseForeColor = true;
            this.gvUserList.Appearance.FocusedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(106)))), ((int)(((byte)(197)))));
            this.gvUserList.Appearance.FocusedRow.ForeColor = System.Drawing.Color.White;
            this.gvUserList.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvUserList.Appearance.FocusedRow.Options.UseForeColor = true;
            this.gvUserList.Appearance.FooterPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.FooterPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvUserList.Appearance.FooterPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.FooterPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvUserList.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvUserList.Appearance.FooterPanel.Options.UseBorderColor = true;
            this.gvUserList.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvUserList.Appearance.GroupButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvUserList.Appearance.GroupButton.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvUserList.Appearance.GroupButton.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.GroupButton.Options.UseBackColor = true;
            this.gvUserList.Appearance.GroupButton.Options.UseBorderColor = true;
            this.gvUserList.Appearance.GroupButton.Options.UseForeColor = true;
            this.gvUserList.Appearance.GroupFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvUserList.Appearance.GroupFooter.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvUserList.Appearance.GroupFooter.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvUserList.Appearance.GroupFooter.Options.UseBorderColor = true;
            this.gvUserList.Appearance.GroupFooter.Options.UseForeColor = true;
            this.gvUserList.Appearance.GroupPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(109)))), ((int)(((byte)(185)))));
            this.gvUserList.Appearance.GroupPanel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.GroupPanel.Options.UseBackColor = true;
            this.gvUserList.Appearance.GroupPanel.Options.UseForeColor = true;
            this.gvUserList.Appearance.GroupRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvUserList.Appearance.GroupRow.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(216)))), ((int)(((byte)(247)))));
            this.gvUserList.Appearance.GroupRow.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.gvUserList.Appearance.GroupRow.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.GroupRow.Options.UseBackColor = true;
            this.gvUserList.Appearance.GroupRow.Options.UseBorderColor = true;
            this.gvUserList.Appearance.GroupRow.Options.UseFont = true;
            this.gvUserList.Appearance.GroupRow.Options.UseForeColor = true;
            this.gvUserList.Appearance.HeaderPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.HeaderPanel.BackColor2 = System.Drawing.Color.FromArgb(((int)(((byte)(132)))), ((int)(((byte)(171)))), ((int)(((byte)(228)))));
            this.gvUserList.Appearance.HeaderPanel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(221)))), ((int)(((byte)(236)))), ((int)(((byte)(254)))));
            this.gvUserList.Appearance.HeaderPanel.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.HeaderPanel.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.gvUserList.Appearance.HeaderPanel.Options.UseBackColor = true;
            this.gvUserList.Appearance.HeaderPanel.Options.UseBorderColor = true;
            this.gvUserList.Appearance.HeaderPanel.Options.UseForeColor = true;
            this.gvUserList.Appearance.HideSelectionRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(106)))), ((int)(((byte)(153)))), ((int)(((byte)(228)))));
            this.gvUserList.Appearance.HideSelectionRow.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(224)))), ((int)(((byte)(251)))));
            this.gvUserList.Appearance.HideSelectionRow.Options.UseBackColor = true;
            this.gvUserList.Appearance.HideSelectionRow.Options.UseForeColor = true;
            this.gvUserList.Appearance.HorzLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gvUserList.Appearance.HorzLine.Options.UseBackColor = true;
            this.gvUserList.Appearance.OddRow.BackColor = System.Drawing.Color.White;
            this.gvUserList.Appearance.OddRow.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.OddRow.Options.UseBackColor = true;
            this.gvUserList.Appearance.OddRow.Options.UseForeColor = true;
            this.gvUserList.Appearance.Preview.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(252)))), ((int)(((byte)(255)))));
            this.gvUserList.Appearance.Preview.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(88)))), ((int)(((byte)(129)))), ((int)(((byte)(185)))));
            this.gvUserList.Appearance.Preview.Options.UseBackColor = true;
            this.gvUserList.Appearance.Preview.Options.UseForeColor = true;
            this.gvUserList.Appearance.Row.BackColor = System.Drawing.Color.White;
            this.gvUserList.Appearance.Row.ForeColor = System.Drawing.Color.Black;
            this.gvUserList.Appearance.Row.Options.UseBackColor = true;
            this.gvUserList.Appearance.Row.Options.UseForeColor = true;
            this.gvUserList.Appearance.RowSeparator.BackColor = System.Drawing.Color.White;
            this.gvUserList.Appearance.RowSeparator.Options.UseBackColor = true;
            this.gvUserList.Appearance.SelectedRow.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(126)))), ((int)(((byte)(217)))));
            this.gvUserList.Appearance.SelectedRow.ForeColor = System.Drawing.Color.White;
            this.gvUserList.Appearance.SelectedRow.Options.UseBackColor = true;
            this.gvUserList.Appearance.SelectedRow.Options.UseForeColor = true;
            this.gvUserList.Appearance.VertLine.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(99)))), ((int)(((byte)(127)))), ((int)(((byte)(196)))));
            this.gvUserList.Appearance.VertLine.Options.UseBackColor = true;
            this.gvUserList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolUserID,
            this.gcolUserName,
            this.gcolUserMail,
            this.gcolUserType,
            this.gcolHostName,
            this.gcolMacAddr,
            this.gcolIPAddr,
            this.gcolIsLocked,
            this.gcolCreateBy,
            this.gcolCreateTime});
            this.gvUserList.GridControl = this.grdUserList;
            this.gvUserList.Name = "gvUserList";
            this.gvUserList.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvUserList.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvUserList.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.gvUserList.OptionsView.EnableAppearanceEvenRow = true;
            this.gvUserList.OptionsView.EnableAppearanceOddRow = true;
            this.gvUserList.OptionsView.ShowGroupPanel = false;
            this.gvUserList.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gvUserList_RowCellClick);
            this.gvUserList.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvUserList_CellValueChanged);
            // 
            // gcolUserID
            // 
            this.gcolUserID.Caption = "序号";
            this.gcolUserID.FieldName = "UserID";
            this.gcolUserID.Name = "gcolUserID";
            this.gcolUserID.OptionsColumn.AllowEdit = false;
            this.gcolUserID.Visible = true;
            this.gcolUserID.VisibleIndex = 0;
            this.gcolUserID.Width = 36;
            // 
            // gcolUserName
            // 
            this.gcolUserName.Caption = "用户名";
            this.gcolUserName.FieldName = "UserName";
            this.gcolUserName.Name = "gcolUserName";
            this.gcolUserName.OptionsColumn.AllowEdit = false;
            this.gcolUserName.Visible = true;
            this.gcolUserName.VisibleIndex = 1;
            this.gcolUserName.Width = 50;
            // 
            // gcolUserMail
            // 
            this.gcolUserMail.Caption = "邮件地址";
            this.gcolUserMail.FieldName = "UserEmail";
            this.gcolUserMail.Name = "gcolUserMail";
            this.gcolUserMail.Visible = true;
            this.gcolUserMail.VisibleIndex = 2;
            this.gcolUserMail.Width = 50;
            // 
            // gcolUserType
            // 
            this.gcolUserType.Caption = "用户类型";
            this.gcolUserType.FieldName = "UserType";
            this.gcolUserType.Name = "gcolUserType";
            this.gcolUserType.OptionsColumn.AllowEdit = false;
            this.gcolUserType.Visible = true;
            this.gcolUserType.VisibleIndex = 3;
            this.gcolUserType.Width = 50;
            // 
            // gcolHostName
            // 
            this.gcolHostName.Caption = "主机名";
            this.gcolHostName.FieldName = "HostName";
            this.gcolHostName.Name = "gcolHostName";
            this.gcolHostName.OptionsColumn.AllowEdit = false;
            this.gcolHostName.Visible = true;
            this.gcolHostName.VisibleIndex = 4;
            this.gcolHostName.Width = 50;
            // 
            // gcolMacAddr
            // 
            this.gcolMacAddr.Caption = "物理地址";
            this.gcolMacAddr.FieldName = "MacAddress";
            this.gcolMacAddr.Name = "gcolMacAddr";
            this.gcolMacAddr.OptionsColumn.AllowEdit = false;
            this.gcolMacAddr.Visible = true;
            this.gcolMacAddr.VisibleIndex = 5;
            this.gcolMacAddr.Width = 50;
            // 
            // gcolIPAddr
            // 
            this.gcolIPAddr.Caption = "IP地址";
            this.gcolIPAddr.FieldName = "NoWebVisitIP";
            this.gcolIPAddr.Name = "gcolIPAddr";
            this.gcolIPAddr.OptionsColumn.AllowEdit = false;
            this.gcolIPAddr.Visible = true;
            this.gcolIPAddr.VisibleIndex = 6;
            this.gcolIPAddr.Width = 50;
            // 
            // gcolIsLocked
            // 
            this.gcolIsLocked.Caption = "是否锁定";
            this.gcolIsLocked.ColumnEdit = this.chkIsLocked;
            this.gcolIsLocked.FieldName = "IsLocked";
            this.gcolIsLocked.Name = "gcolIsLocked";
            this.gcolIsLocked.Visible = true;
            this.gcolIsLocked.VisibleIndex = 7;
            this.gcolIsLocked.Width = 50;
            // 
            // chkIsLocked
            // 
            this.chkIsLocked.AutoHeight = false;
            this.chkIsLocked.Name = "chkIsLocked";
            this.chkIsLocked.NullStyle = DevExpress.XtraEditors.Controls.StyleIndeterminate.Unchecked;
            this.chkIsLocked.ValueChecked = "0";
            this.chkIsLocked.ValueGrayed = "0";
            this.chkIsLocked.ValueUnchecked = "1";
            // 
            // gcolCreateBy
            // 
            this.gcolCreateBy.Caption = "账户创建人";
            this.gcolCreateBy.FieldName = "CreateBy";
            this.gcolCreateBy.Name = "gcolCreateBy";
            this.gcolCreateBy.OptionsColumn.AllowEdit = false;
            this.gcolCreateBy.Visible = true;
            this.gcolCreateBy.VisibleIndex = 8;
            this.gcolCreateBy.Width = 50;
            // 
            // gcolCreateTime
            // 
            this.gcolCreateTime.Caption = "账户创建时间";
            this.gcolCreateTime.FieldName = "CreateTime";
            this.gcolCreateTime.Name = "gcolCreateTime";
            this.gcolCreateTime.OptionsColumn.AllowEdit = false;
            this.gcolCreateTime.Visible = true;
            this.gcolCreateTime.VisibleIndex = 9;
            this.gcolCreateTime.Width = 55;
            // 
            // bingdingUser
            // 
            this.bingdingUser.AddNewItem = null;
            this.bingdingUser.CountItem = this.bindingNavigatorCountItem;
            this.bingdingUser.DeleteItem = null;
            this.bingdingUser.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.bindingNavigatorAddNewItem,
            this.bindingNavigatorDeleteItem});
            this.bingdingUser.Location = new System.Drawing.Point(0, 0);
            this.bingdingUser.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bingdingUser.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bingdingUser.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bingdingUser.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bingdingUser.Name = "bingdingUser";
            this.bingdingUser.PositionItem = this.bindingNavigatorPositionItem;
            this.bingdingUser.Size = new System.Drawing.Size(876, 25);
            this.bingdingUser.TabIndex = 1;
            this.bingdingUser.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "/ {0}";
            this.bindingNavigatorCountItem.ToolTipText = "总项数";
            // 
            // bindingNavigatorDeleteItem
            // 
            this.bindingNavigatorDeleteItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorDeleteItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorDeleteItem.Image")));
            this.bindingNavigatorDeleteItem.Name = "bindingNavigatorDeleteItem";
            this.bindingNavigatorDeleteItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorDeleteItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorDeleteItem.Text = "删除";
            this.bindingNavigatorDeleteItem.Click += new System.EventHandler(this.bindingNavigatorDeleteItem_Click);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "移到第一条记录";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "移到上一条记录";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "位置";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "当前位置";
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
            this.bindingNavigatorMoveNextItem.Text = "移到下一条记录";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "移到最后一条记录";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorAddNewItem
            // 
            this.bindingNavigatorAddNewItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorAddNewItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorAddNewItem.Image")));
            this.bindingNavigatorAddNewItem.Name = "bindingNavigatorAddNewItem";
            this.bindingNavigatorAddNewItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorAddNewItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorAddNewItem.Text = "新添";
            this.bindingNavigatorAddNewItem.Click += new System.EventHandler(this.bindingNavigatorAddNewItem_Click);
            // 
            // ucUserList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "ucUserList";
            this.Size = new System.Drawing.Size(876, 387);
            this.Load += new System.EventHandler(this.ucUserList_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdUserList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvUserList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsLocked)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bingdingUser)).EndInit();
            this.bingdingUser.ResumeLayout(false);
            this.bingdingUser.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl grdUserList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvUserList;
        private DevExpress.XtraGrid.Columns.GridColumn gcolUserID;
        private DevExpress.XtraGrid.Columns.GridColumn gcolUserName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolUserMail;
        private DevExpress.XtraGrid.Columns.GridColumn gcolUserType;
        private DevExpress.XtraGrid.Columns.GridColumn gcolHostName;
        private DevExpress.XtraGrid.Columns.GridColumn gcolMacAddr;
        private DevExpress.XtraGrid.Columns.GridColumn gcolIPAddr;
        private DevExpress.XtraGrid.Columns.GridColumn gcolIsLocked;
        private DevExpress.XtraGrid.Columns.GridColumn gcolCreateBy;
        private DevExpress.XtraGrid.Columns.GridColumn gcolCreateTime;
        private System.Windows.Forms.BindingNavigator bingdingUser;
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
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit chkIsLocked;
    }
}
