namespace MDT.Console
{
    partial class FormTaskSet
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTaskSet));
            this.btnSave = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.cboSPrimaryKeys = new System.Windows.Forms.ComboBox();
            this.cboTForeignKeys = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTaskName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cboSTableNames = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cboSDatabase = new System.Windows.Forms.ComboBox();
            this.cboTPrimaryKeys = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.cboSForeignKeys = new System.Windows.Forms.ComboBox();
            this.cboTDatabase = new System.Windows.Forms.ComboBox();
            this.cboTTableNames = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txtTaskNode = new System.Windows.Forms.TextBox();
            this.txtAdditionalWhere = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnAddDefaultColumn = new System.Windows.Forms.Button();
            this.cboTDefaultColumn = new System.Windows.Forms.ComboBox();
            this.txtDefaultValue = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtPostTaskName = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.num = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sourceName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.sourceType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.targetName = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.targetType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.defaultValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.删除ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label14 = new System.Windows.Forms.Label();
            this.cmbTaskType = new System.Windows.Forms.ComboBox();
            this.cmbPlateForm = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(941, 590);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(90, 27);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.BackColor = System.Drawing.Color.Transparent;
            this.label19.Location = new System.Drawing.Point(33, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(65, 12);
            this.label19.TabIndex = 86;
            this.label19.Text = "任务名称：";
            // 
            // cboSPrimaryKeys
            // 
            this.cboSPrimaryKeys.FormattingEnabled = true;
            this.cboSPrimaryKeys.Location = new System.Drawing.Point(102, 123);
            this.cboSPrimaryKeys.Name = "cboSPrimaryKeys";
            this.cboSPrimaryKeys.Size = new System.Drawing.Size(227, 20);
            this.cboSPrimaryKeys.TabIndex = 8;
            // 
            // cboTForeignKeys
            // 
            this.cboTForeignKeys.FormattingEnabled = true;
            this.cboTForeignKeys.Location = new System.Drawing.Point(439, 147);
            this.cboTForeignKeys.Name = "cboTForeignKeys";
            this.cboTForeignKeys.Size = new System.Drawing.Size(227, 20);
            this.cboTForeignKeys.TabIndex = 11;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.BackColor = System.Drawing.Color.Transparent;
            this.label10.Location = new System.Drawing.Point(370, 127);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 12);
            this.label10.TabIndex = 90;
            this.label10.Text = "主    键：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Location = new System.Drawing.Point(370, 103);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 83;
            this.label7.Text = "表    名：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(33, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 99;
            this.label2.Text = "表    名：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Location = new System.Drawing.Point(33, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 82;
            this.label3.Text = "源数据库：";
            // 
            // txtTaskName
            // 
            this.txtTaskName.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTaskName.Location = new System.Drawing.Point(102, 25);
            this.txtTaskName.Name = "txtTaskName";
            this.txtTaskName.Size = new System.Drawing.Size(227, 21);
            this.txtTaskName.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.BackColor = System.Drawing.Color.Transparent;
            this.label9.Location = new System.Drawing.Point(33, 127);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 89;
            this.label9.Text = "主    键：";
            // 
            // cboSTableNames
            // 
            this.cboSTableNames.FormattingEnabled = true;
            this.cboSTableNames.Location = new System.Drawing.Point(102, 99);
            this.cboSTableNames.Name = "cboSTableNames";
            this.cboSTableNames.Size = new System.Drawing.Size(227, 20);
            this.cboSTableNames.TabIndex = 6;
            this.cboSTableNames.TextChanged += new System.EventHandler(this.cboTableNames_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(358, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 96;
            this.label1.Text = "目标数据库：";
            // 
            // cboSDatabase
            // 
            this.cboSDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSDatabase.FormattingEnabled = true;
            this.cboSDatabase.Location = new System.Drawing.Point(102, 75);
            this.cboSDatabase.Name = "cboSDatabase";
            this.cboSDatabase.Size = new System.Drawing.Size(227, 20);
            this.cboSDatabase.TabIndex = 4;
            this.cboSDatabase.TextChanged += new System.EventHandler(this.cboSDatabase_TextChanged);
            // 
            // cboTPrimaryKeys
            // 
            this.cboTPrimaryKeys.FormattingEnabled = true;
            this.cboTPrimaryKeys.Location = new System.Drawing.Point(439, 123);
            this.cboTPrimaryKeys.Name = "cboTPrimaryKeys";
            this.cboTPrimaryKeys.Size = new System.Drawing.Size(227, 20);
            this.cboTPrimaryKeys.TabIndex = 9;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Location = new System.Drawing.Point(370, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(65, 12);
            this.label12.TabIndex = 92;
            this.label12.Text = "外    键：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Location = new System.Drawing.Point(33, 151);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(65, 12);
            this.label11.TabIndex = 91;
            this.label11.Text = "外    键：";
            // 
            // cboSForeignKeys
            // 
            this.cboSForeignKeys.FormattingEnabled = true;
            this.cboSForeignKeys.Location = new System.Drawing.Point(102, 147);
            this.cboSForeignKeys.Name = "cboSForeignKeys";
            this.cboSForeignKeys.Size = new System.Drawing.Size(227, 20);
            this.cboSForeignKeys.TabIndex = 10;
            // 
            // cboTDatabase
            // 
            this.cboTDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTDatabase.FormattingEnabled = true;
            this.cboTDatabase.Location = new System.Drawing.Point(439, 75);
            this.cboTDatabase.Name = "cboTDatabase";
            this.cboTDatabase.Size = new System.Drawing.Size(227, 20);
            this.cboTDatabase.TabIndex = 5;
            this.cboTDatabase.TextChanged += new System.EventHandler(this.cboTDatabase_TextChanged);
            // 
            // cboTTableNames
            // 
            this.cboTTableNames.FormattingEnabled = true;
            this.cboTTableNames.Location = new System.Drawing.Point(439, 99);
            this.cboTTableNames.Name = "cboTTableNames";
            this.cboTTableNames.Size = new System.Drawing.Size(227, 20);
            this.cboTTableNames.TabIndex = 7;
            this.cboTTableNames.TextChanged += new System.EventHandler(this.cboTableNames_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.txtTaskNode);
            this.groupBox1.Controls.Add(this.txtAdditionalWhere);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.btnAddDefaultColumn);
            this.groupBox1.Controls.Add(this.cboTDefaultColumn);
            this.groupBox1.Controls.Add(this.txtDefaultValue);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtPostTaskName);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.richTextBox1);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Controls.Add(this.label15);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label19);
            this.groupBox1.Controls.Add(this.cboSPrimaryKeys);
            this.groupBox1.Controls.Add(this.cboTTableNames);
            this.groupBox1.Controls.Add(this.cboTForeignKeys);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.cboTDatabase);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.cboSForeignKeys);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.txtTaskName);
            this.groupBox1.Controls.Add(this.cboTPrimaryKeys);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cmbPlateForm);
            this.groupBox1.Controls.Add(this.cmbTaskType);
            this.groupBox1.Controls.Add(this.cboSDatabase);
            this.groupBox1.Controls.Add(this.cboSTableNames);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.Location = new System.Drawing.Point(13, 9);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1116, 577);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "设置任务";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.BackColor = System.Drawing.Color.Transparent;
            this.label13.Location = new System.Drawing.Point(370, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(65, 12);
            this.label13.TabIndex = 113;
            this.label13.Text = "任务描述：";
            // 
            // txtTaskNode
            // 
            this.txtTaskNode.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtTaskNode.Location = new System.Drawing.Point(439, 26);
            this.txtTaskNode.Name = "txtTaskNode";
            this.txtTaskNode.Size = new System.Drawing.Size(227, 21);
            this.txtTaskNode.TabIndex = 1;
            // 
            // txtAdditionalWhere
            // 
            this.txtAdditionalWhere.Location = new System.Drawing.Point(102, 171);
            this.txtAdditionalWhere.Name = "txtAdditionalWhere";
            this.txtAdditionalWhere.Size = new System.Drawing.Size(227, 21);
            this.txtAdditionalWhere.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Location = new System.Drawing.Point(27, 175);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(71, 12);
            this.label5.TabIndex = 110;
            this.label5.Text = "WHERE条件：";
            // 
            // btnAddDefaultColumn
            // 
            this.btnAddDefaultColumn.Location = new System.Drawing.Point(589, 544);
            this.btnAddDefaultColumn.Name = "btnAddDefaultColumn";
            this.btnAddDefaultColumn.Size = new System.Drawing.Size(86, 23);
            this.btnAddDefaultColumn.TabIndex = 14;
            this.btnAddDefaultColumn.Text = "添加默认列";
            this.btnAddDefaultColumn.UseVisualStyleBackColor = true;
            this.btnAddDefaultColumn.Click += new System.EventHandler(this.btnAddDefaultColumn_Click);
            // 
            // cboTDefaultColumn
            // 
            this.cboTDefaultColumn.FormattingEnabled = true;
            this.cboTDefaultColumn.Location = new System.Drawing.Point(89, 546);
            this.cboTDefaultColumn.Name = "cboTDefaultColumn";
            this.cboTDefaultColumn.Size = new System.Drawing.Size(205, 20);
            this.cboTDefaultColumn.TabIndex = 14;
            // 
            // txtDefaultValue
            // 
            this.txtDefaultValue.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDefaultValue.Location = new System.Drawing.Point(374, 545);
            this.txtDefaultValue.Name = "txtDefaultValue";
            this.txtDefaultValue.Size = new System.Drawing.Size(205, 21);
            this.txtDefaultValue.TabIndex = 15;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Location = new System.Drawing.Point(34, 550);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 106;
            this.label6.Text = "目标列：";
            // 
            // txtPostTaskName
            // 
            this.txtPostTaskName.Location = new System.Drawing.Point(439, 171);
            this.txtPostTaskName.Name = "txtPostTaskName";
            this.txtPostTaskName.Size = new System.Drawing.Size(227, 21);
            this.txtPostTaskName.TabIndex = 13;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.BackColor = System.Drawing.Color.Transparent;
            this.label8.Location = new System.Drawing.Point(315, 550);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 107;
            this.label8.Text = "默认值：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Location = new System.Drawing.Point(346, 175);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 12);
            this.label4.TabIndex = 102;
            this.label4.Text = "事后任务名称：";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(681, 26);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(429, 541);
            this.richTextBox1.TabIndex = 16;
            this.richTextBox1.Text = "";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.num,
            this.sourceName,
            this.sourceType,
            this.targetName,
            this.targetType,
            this.defaultValue});
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(35, 198);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(631, 340);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellValueChanged);
            this.dataGridView1.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            // 
            // num
            // 
            this.num.FillWeight = 45F;
            this.num.HeaderText = "序号";
            this.num.Name = "num";
            this.num.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.num.Visible = false;
            // 
            // sourceName
            // 
            this.sourceName.DataPropertyName = "sourceName";
            this.sourceName.HeaderText = "来源列名";
            this.sourceName.Name = "sourceName";
            this.sourceName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // sourceType
            // 
            this.sourceType.DataPropertyName = "sourceType";
            this.sourceType.FillWeight = 50F;
            this.sourceType.HeaderText = "数据类型";
            this.sourceType.Name = "sourceType";
            this.sourceType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // targetName
            // 
            this.targetName.DataPropertyName = "targetName";
            this.targetName.HeaderText = "目标列名";
            this.targetName.Name = "targetName";
            this.targetName.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            // 
            // targetType
            // 
            this.targetType.DataPropertyName = "targetType";
            this.targetType.FillWeight = 50F;
            this.targetType.HeaderText = "数据类型";
            this.targetType.Name = "targetType";
            this.targetType.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // defaultValue
            // 
            this.defaultValue.DataPropertyName = "defaultValue";
            this.defaultValue.FillWeight = 45F;
            this.defaultValue.HeaderText = "默认值";
            this.defaultValue.Name = "defaultValue";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除ToolStripMenuItem,
            this.添加ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(99, 48);
            // 
            // 删除ToolStripMenuItem
            // 
            this.删除ToolStripMenuItem.Name = "删除ToolStripMenuItem";
            this.删除ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.删除ToolStripMenuItem.Text = "删除";
            this.删除ToolStripMenuItem.Click += new System.EventHandler(this.删除ToolStripMenuItem_Click);
            // 
            // 添加ToolStripMenuItem
            // 
            this.添加ToolStripMenuItem.Name = "添加ToolStripMenuItem";
            this.添加ToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
            this.添加ToolStripMenuItem.Text = "添加";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon_01.png");
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(846, 590);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(90, 27);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "添加";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(751, 590);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(90, 27);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "删除";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1035, 590);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 27);
            this.button1.TabIndex = 3;
            this.button1.Text = "生成空任务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.BackColor = System.Drawing.Color.Transparent;
            this.label14.Location = new System.Drawing.Point(33, 53);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(65, 12);
            this.label14.TabIndex = 86;
            this.label14.Text = "任务类型：";
            // 
            // cmbTaskType
            // 
            this.cmbTaskType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTaskType.FormattingEnabled = true;
            this.cmbTaskType.Items.AddRange(new object[] {
            "交换任务",
            "服务任务",
            "日志任务"});
            this.cmbTaskType.Location = new System.Drawing.Point(102, 50);
            this.cmbTaskType.Name = "cmbTaskType";
            this.cmbTaskType.Size = new System.Drawing.Size(227, 20);
            this.cmbTaskType.TabIndex = 2;
            // 
            // cmbPlateForm
            // 
            this.cmbPlateForm.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbPlateForm.FormattingEnabled = true;
            this.cmbPlateForm.Location = new System.Drawing.Point(439, 50);
            this.cmbPlateForm.Name = "cmbPlateForm";
            this.cmbPlateForm.Size = new System.Drawing.Size(227, 20);
            this.cmbPlateForm.TabIndex = 3;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.BackColor = System.Drawing.Color.Transparent;
            this.label15.Location = new System.Drawing.Point(370, 53);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(65, 12);
            this.label15.TabIndex = 86;
            this.label15.Text = "所属平台：";
            // 
            // FormTaskSet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1148, 624);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnCancel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.Name = "FormTaskSet";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "任务配置";
            this.Load += new System.EventHandler(this.FormTask_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cboSPrimaryKeys;
        private System.Windows.Forms.ComboBox cboTForeignKeys;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTaskName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cboSTableNames;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboSDatabase;
        private System.Windows.Forms.ComboBox cboTPrimaryKeys;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cboSForeignKeys;
        private System.Windows.Forms.ComboBox cboTDatabase;
        private System.Windows.Forms.ComboBox cboTTableNames;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加ToolStripMenuItem;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox txtPostTaskName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDefaultValue;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cboTDefaultColumn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnAddDefaultColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn num;
        private System.Windows.Forms.DataGridViewComboBoxColumn sourceName;
        private System.Windows.Forms.DataGridViewTextBoxColumn sourceType;
        private System.Windows.Forms.DataGridViewComboBoxColumn targetName;
        private System.Windows.Forms.DataGridViewTextBoxColumn targetType;
        private System.Windows.Forms.DataGridViewTextBoxColumn defaultValue;
        private System.Windows.Forms.TextBox txtAdditionalWhere;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txtTaskNode;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.ComboBox cmbTaskType;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.ComboBox cmbPlateForm;
    }
}