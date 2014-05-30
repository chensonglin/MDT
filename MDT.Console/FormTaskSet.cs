using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.ServiceImplement;
using MDT.ManageCenter.DataContract;
using System.Xml;
using System.Runtime.Serialization;
using System.IO;
using System.Xml.Xsl;
using MDT.Utility;

namespace MDT.Console
{
    public partial class FormTaskSet : Form
    {
        private DataTable mappingDt;
        private SourceType sourcDbType;
        private SourceType targetDbType;
        private EDatabase sourceEDatabase;
        private EDatabase targetEDatabase;
        private List<EDatabase> dbList;
        private DbSchemaService dbSchema;
        private Table sourceTable;
        private Table targetTable;
        private Dictionary<string, string> defaultColumns;

        public FormTaskSet()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;

            dbList = new List<EDatabase>();
            dbSchema = new DbSchemaService();
            defaultColumns = new Dictionary<string, string>();

            cboSDatabase.DisplayMember = "alias";
            cboSDatabase.ValueMember = "id";
            cboTDatabase.DisplayMember = "alias";
            cboTDatabase.ValueMember = "id";

            cboSTableNames.DisplayMember = "TABLE_NAME";
            cboTTableNames.DisplayMember = "TABLE_NAME";

            cboSPrimaryKeys.ValueMember = "datatype";
            cboSPrimaryKeys.DisplayMember = "column_name";

            cboTPrimaryKeys.ValueMember = "datatype";
            cboTPrimaryKeys.DisplayMember = "column_name";

            cboSForeignKeys.DisplayMember = "column_name";
            cboTForeignKeys.DisplayMember = "column_name";
            cboTDefaultColumn.DisplayMember = "column_name";

            sourceName.ValueMember = "column_name";
            targetName.ValueMember = "column_name";

            mappingDt = new DataTable();
            mappingDt.Columns.Add("sourceName", typeof(System.String));
            mappingDt.Columns.Add("sourceType", typeof(System.String));
            mappingDt.Columns.Add("targetName", typeof(System.String));
            mappingDt.Columns.Add("targetType", typeof(System.String));
            mappingDt.Columns.Add("defaultValue", typeof(System.String));
        }

        private void FormTask_Load(object sender, EventArgs e)
        {
            try
            {
                // 获取数据连接信息
                EDatabaseDAL dbDAL = new EDatabaseDAL();
                dbList = dbDAL.GetDatabases();

                DataTable dt = new DataTable();
                dt.TableName = "EDATABASE";
                DataColumn column;
                DataRow row;
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.String");
                column.ColumnName = "alias";
                dt.Columns.Add(column);
                column = new DataColumn();
                column.DataType = System.Type.GetType("System.Int32");
                column.ColumnName = "id";
                dt.Columns.Add(column);

                foreach (EDatabase edb in dbList)
                {
                    row = dt.NewRow();
                    row["id"] = edb.ID;
                    row["alias"] = edb.Alias;
                    dt.Rows.Add(row);
                }

                cboSDatabase.TextChanged -= new System.EventHandler(cboSDatabase_TextChanged);
                cboTDatabase.TextChanged -= new System.EventHandler(cboTDatabase_TextChanged);
                cboSDatabase.DataSource = dt.Copy();
                cboTDatabase.DataSource = dt.Copy();
                cboSDatabase_TextChanged(null, null);
                cboTDatabase_TextChanged(null, null);
                cboSDatabase.TextChanged += new System.EventHandler(cboSDatabase_TextChanged);
                cboTDatabase.TextChanged += new System.EventHandler(cboTDatabase_TextChanged);

                List<string> lstPlateform = new ETaskDAL().GetExsistPlatform();
                cmbPlateForm.Items.AddRange(lstPlateform.ToArray());
                cmbTaskType.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cboSDatabase_TextChanged(object sender, EventArgs e)
        {
            if (cboSDatabase.Text.Trim().Length > 0)
            {
                try
                {
                    sourceEDatabase = findEDatabase(cboSDatabase.Text);
                    sourcDbType = (SourceType)Enum.Parse(typeof(SourceType), sourceEDatabase.DatabaseType);
                    cboSTableNames.DataSource = dbSchema.GetTableInfo(sourcDbType, sourceEDatabase.Server
                                                                     , sourceEDatabase.Port, sourceEDatabase.Database
                                                                     , sourceEDatabase.UserId, sourceEDatabase.Password);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                }
            }
        }

        private void cboTDatabase_TextChanged(object sender, EventArgs e)
        {
            if (cboTDatabase.Text.Trim().Length > 0)
            {
                try
                {
                    targetEDatabase = findEDatabase(cboTDatabase.Text);
                    targetDbType = (SourceType)Enum.Parse(typeof(SourceType), targetEDatabase.DatabaseType);

                    cboTTableNames.DataSource = dbSchema.GetTableInfo(targetDbType, targetEDatabase.Server
                                                                      , targetEDatabase.Port, targetEDatabase.Database
                                                                      , targetEDatabase.UserId, targetEDatabase.Password);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
                }
            }
        }

        private void cboTableNames_TextChanged(object sender, EventArgs e)
        {
            mappingDt.Rows.Clear();

            if (cboSTableNames.Text.Trim().Length > 0 && cboTTableNames.Text.Trim().Length > 0)
            {
                int rowCount;
                DataTable dt;
                DataTable sourceDt;
                DataTable targetDt;
                string sourceDataTypeName = String.Empty;
                string targetDataTypeName = String.Empty;

                dt = dbSchema.GetColumnInfo(sourcDbType, sourceEDatabase.Server, sourceEDatabase.Port, sourceEDatabase.Database, sourceEDatabase.UserId, sourceEDatabase.Password, cboSTableNames.Text);

                if (sourcDbType == SourceType.SqlServer || sourcDbType == SourceType.MySql)
                {
                    sourceDataTypeName = "data_type";
                    dt.DefaultView.Sort = "ordinal_position";
                }
                else if (sourcDbType == SourceType.Oracle)
                {
                    sourceDataTypeName = "datatype";
                }
                sourceDt = dt.DefaultView.ToTable();

                dt.DefaultView.Sort = "column_name";
                sourceName.DataSource = dt.DefaultView.ToTable();
                cboSPrimaryKeys.DataSource = dt.DefaultView.ToTable();
                cboSForeignKeys.DataSource = dt.DefaultView.ToTable();

                dt = dbSchema.GetColumnInfo(targetDbType, targetEDatabase.Server
                                            , targetEDatabase.Port, targetEDatabase.Database
                                            , targetEDatabase.UserId, targetEDatabase.Password
                                            , cboTTableNames.Text);

                if (targetDbType == SourceType.SqlServer || targetDbType == SourceType.MySql)
                {
                    targetDataTypeName = "data_type";
                    dt.DefaultView.Sort = "ordinal_position";
                }
                else
                {
                    targetDataTypeName = "datatype";
                }
                targetDt = dt.DefaultView.ToTable();

                dt.DefaultView.Sort = "column_name";
                targetName.DataSource = dt.DefaultView.ToTable();
                cboTPrimaryKeys.DataSource = dt.DefaultView.ToTable();
                cboTForeignKeys.DataSource = dt.DefaultView.ToTable();
                cboTDefaultColumn.DataSource = dt.DefaultView.ToTable();

                if (sourceDt != null && sourceDt.Rows.Count > 0 && targetDt != null && targetDt.Rows.Count > 0)
                {
                    rowCount = sourceDt.Rows.Count > targetDt.Rows.Count ? sourceDt.Rows.Count : targetDt.Rows.Count;

                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow newRow = mappingDt.NewRow();

                        if (i < sourceDt.Rows.Count)
                        {
                            newRow["sourceName"] = sourceDt.Rows[i]["column_name"];
                            newRow["sourceType"] = sourceDt.Rows[i][sourceDataTypeName];
                        }

                        if (i < targetDt.Rows.Count)
                        {
                            newRow["targetName"] = targetDt.Rows[i]["column_name"];
                            newRow["targetType"] = targetDt.Rows[i][targetDataTypeName];
                        }

                        mappingDt.Rows.Add(newRow);
                    }

                    dataGridView1.DataSource = mappingDt;
                }
            }

            cboSPrimaryKeys.Text = String.Empty;
            cboTPrimaryKeys.Text = String.Empty;
            cboSForeignKeys.Text = String.Empty;
            cboTForeignKeys.Text = String.Empty;
            cboTDefaultColumn.Text = String.Empty;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count == 0) { return; }

            if (sourceTable == null || sourceTable.Columns == null || sourceTable.Columns.Count == 0)
            {
                // 添加主表信息
                if (cboSPrimaryKeys.Text.Trim().Length == 0 || cboTPrimaryKeys.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请选择表的主键信息！", "信息提示", MessageBoxButtons.OK);
                    cboSPrimaryKeys.Focus();
                    return;
                }
                else
                {
                    // 添加来源表
                    sourceTable = new Table
                    {
                        TableName = cboSTableNames.Text.ToLower(),
                        Columns = new List<EColumn>(),
                        RelatedTables = new List<RelatedTable>(),
                        PKColumn = new EColumn()
                        {
                            Name = cboSPrimaryKeys.Text.ToLower()
                        }
                    };
                    buildColumnInfo(cboSDatabase.Text, sourceTable, "sourceName");

                    // 添加目标表
                    targetTable = new Table
                    {
                        TableName = cboTTableNames.Text.ToLower(),
                        Columns = new List<EColumn>(),
                        RelatedTables = new List<RelatedTable>(),
                        PKColumn = new EColumn()
                        {
                            Name = cboTPrimaryKeys.Text.ToLower()
                        }
                    };
                    buildColumnInfo(cboTDatabase.Text, targetTable, "targetName");

                    // 添加Where
                    if (txtAdditionalWhere.Text.Trim().Length > 0)
                    {
                        sourceTable.AdditionalWhere = txtAdditionalWhere.Text;
                    }

                    // 添加事后存储过程
                    if (txtPostTaskName.Text.Trim().Length > 0)
                    {
                        targetTable.PostStoredProcedure = txtPostTaskName.Text;
                    }
                }
            }
            else
            {
                // 添加子表信息
                if (cboSForeignKeys.Text.Trim().Length == 0 || cboTForeignKeys.Text.Trim().Length == 0)
                {
                    MessageBox.Show("请选择表的外键信息！", "信息提示", MessageBoxButtons.OK);
                    cboSForeignKeys.Focus();
                    return;
                }
                else
                {
                    RelatedTable relatedTable = new RelatedTable
                    {
                        TableName = cboSTableNames.Text.ToLower(),
                        Columns = new List<EColumn>(),
                        ForeignColumn = new EColumn()
                        {
                            Name = cboSForeignKeys.Text.ToLower()
                        }
                    };
                    buildColumnInfo(cboSDatabase.Text, relatedTable, "sourceName");
                    sourceTable.RelatedTables.Add(relatedTable);

                    relatedTable = new RelatedTable
                    {
                        TableName = cboTTableNames.Text.ToLower(),
                        Columns = new List<EColumn>(),
                        ForeignColumn = new EColumn()
                        {
                            Name = cboTForeignKeys.Text.ToLower(),
                        }
                    };
                    buildColumnInfo(cboTDatabase.Text, relatedTable, "targetName");
                    targetTable.RelatedTables.Add(relatedTable);
                }
            }

            buildMapping();

            cboSTableNames.Text = String.Empty;
            cboSForeignKeys.Text = String.Empty;
            cboSPrimaryKeys.Text = String.Empty;

            cboTTableNames.Text = String.Empty;
            cboTPrimaryKeys.Text = String.Empty;
            cboTForeignKeys.Text = String.Empty;

            txtPostTaskName.Text = String.Empty;
        }

        private void buildColumnInfo(string databaseName, Table table, string columnName)
        {
            string sql = "select * from " + table.TableName + " where 1=0";
            EDatabase database = findEDatabase(databaseName);
            DataTable schemaDt = dbSchema.GetDataTableSchema((SourceType)Enum.Parse(typeof(SourceType), database.DatabaseType)
                                                             , database.Server, database.Port, database.Database
                                                             , database.UserId, database.Password
                                                             , sql, table.TableName);

            table.PKColumn.Type = schemaDt.Columns[table.PKColumn.Name].DataType.ToString();
            foreach (DataRow row in mappingDt.Rows)
            {
                // 是否添加默认列
                if (!String.IsNullOrEmpty(row["defaultValue"].ToString()))
                {
                    if (columnName == "sourceName") { continue; }
                }

                table.Columns.Add(new EColumn
                {
                    Name = row[columnName].ToString().ToLower(),
                    Type = schemaDt.Columns[row[columnName].ToString()].DataType.ToString()
                });
            }
        }

        private void buildColumnInfo(string databaseName, RelatedTable relatedTable, string columnName)
        {
            string sql = "select * from " + relatedTable.TableName + " where 1=0";
            EDatabase database = findEDatabase(databaseName);
            DataTable schemaDt = dbSchema.GetDataTableSchema((SourceType)Enum.Parse(typeof(SourceType), database.DatabaseType)
                                                             , database.Server, database.Port, database.Database
                                                             , database.UserId, database.Password
                                                             , sql, sourceTable.TableName);

            relatedTable.ForeignColumn.Type = schemaDt.Columns[relatedTable.ForeignColumn.Name].DataType.ToString();
            foreach (DataRow row in mappingDt.Rows)
            {
                relatedTable.Columns.Add(new EColumn
                {
                    Name = row[columnName].ToString().ToLower(),
                    Type = schemaDt.Columns[row[columnName].ToString()].DataType.ToString()
                });
            }
        }

        private void buildMapping()
        {
            if (sourceTable.Columns != null && sourceTable.Columns.Count > 0)
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<Tables></Tables>");

                XmlElement parentElement = doc.CreateElement("Table");
                parentElement.SetAttribute("Source", sourceTable.TableName);
                parentElement.SetAttribute("Target", targetTable.TableName);

                for (int i = 0; i < targetTable.Columns.Count; i++)
                {
                    XmlElement element = doc.CreateElement("Column");
                    if (defaultColumns.ContainsKey(targetTable.Columns[i].Name))
                    {
                        element.SetAttribute("Value", defaultColumns[targetTable.Columns[i].Name]);
                        element.SetAttribute("Target", targetTable.Columns[i].Name);
                    }
                    else
                    {
                        element.SetAttribute("Source", sourceTable.Columns[i].Name);
                        element.SetAttribute("Target", targetTable.Columns[i].Name);
                    }
                    parentElement.AppendChild(element);
                }

                if (sourceTable.RelatedTables != null && sourceTable.RelatedTables.Count > 0)
                {
                    for (int i = 0; i < sourceTable.RelatedTables.Count; i++)
                    {
                        RelatedTable sourceRelatedTable = sourceTable.RelatedTables[i];
                        RelatedTable targetRelatedTable = targetTable.RelatedTables[i];

                        XmlElement childElement = doc.CreateElement("Table");
                        childElement.SetAttribute("Source", sourceRelatedTable.TableName);
                        childElement.SetAttribute("Target", targetRelatedTable.TableName);

                        for (int j = 0; j < sourceTable.RelatedTables[i].Columns.Count; j++)
                        {
                            XmlElement element = doc.CreateElement("Column");
                            element.SetAttribute("Source", sourceRelatedTable.Columns[j].Name);
                            element.SetAttribute("Target", targetRelatedTable.Columns[j].Name);

                            childElement.AppendChild(element);
                        }

                        parentElement.AppendChild(childElement);
                    }
                }

                doc.DocumentElement.AppendChild(parentElement);
                richTextBox1.Text = doc.OuterXml;
            }
        }

        private void 删除ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                List<string> list = new List<string>();

                foreach (DataGridViewRow dgvRow in dataGridView1.SelectedRows)
                {
                    if (String.IsNullOrEmpty(dgvRow.Cells["sourceName"].Value.ToString()))
                        list.Add(dgvRow.Cells["targetName"].Value.ToString());
                    else
                        list.Add(dgvRow.Cells["sourceName"].Value.ToString());
                }

                foreach (var name in list)
                {
                    foreach (DataRow row in mappingDt.Rows)
                    {
                        if (String.IsNullOrEmpty(row["sourceName"].ToString()))
                        {
                            if (row["targetName"].ToString() == name)
                            {
                                mappingDt.Rows.Remove(row);
                                break;
                            }
                        }
                        else
                        {
                            if (row["sourceName"].ToString() == name)
                            {
                                mappingDt.Rows.Remove(row);
                                break;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (this.checkBox1.Checked)
                //{
                //    int tdbId = (int)this.cboTDatabase.SelectedValue;
                //    var t = getTESchema();
                //    ManageCenterService s = new ManageCenterService();
                //    s.AddSchema(t);
                //}
                //else
                //{
                ManageCenterService s = new ManageCenterService();
                s.AddTask(getETask());
                sourceTable = null;
                targetTable = null;
                defaultColumns.Clear();
                richTextBox1.Text = String.Empty;
                //}

                MessageBox.Show("保存成功！", "信息提示", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
            }
        }

        /// <summary>
        /// 生成空任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ManageCenterService s = new ManageCenterService();
                s.AddTask(txtTaskName.Text, txtTaskNode.Text);
                sourceTable = null;
                targetTable = null;
                defaultColumns.Clear();
                richTextBox1.Text = String.Empty;
                MessageBox.Show("保存成功！", "信息提示", MessageBoxButtons.OK);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "信息提示", MessageBoxButtons.OK);
            }
        }

        private ESchema getSESchema()
        {
            int sdbId = (int)this.cboSDatabase.SelectedValue;
            var SourceESchema = new ESchema()
                 {
                     //Customer_ID = 1,
                     ESource = getESource(sdbId, sourcDbType, sourceTable),
                     Schema = getDataTableSchema(cboSDatabase.Text, sourceTable)
                 };
            return SourceESchema;
        }

        private ESchema getTESchema()
        {
            int tdbId = (int)this.cboTDatabase.SelectedValue;
            targetTable = new Table
            {
                TableName = cboTTableNames.Text,
                Columns = new List<EColumn>(),
                RelatedTables = new List<RelatedTable>(),
                PKColumn = new EColumn()
                {
                    Name = cboTPrimaryKeys.Text
                }
            };

            buildColumnInfo(cboTDatabase.Text, targetTable, "targetName");

            var TargetESchema = new ESchema()
                 {
                     //Customer_ID = 1,
                     ESource = getESource(tdbId, targetDbType, targetTable),
                     Schema = getDataTableSchema(cboTDatabase.Text, targetTable)
                 };

            return TargetESchema;
        }

        private ETask getETask()
        {
            int sdbId = (int)this.cboSDatabase.SelectedValue;
            int tdbId = (int)this.cboTDatabase.SelectedValue;

            ETask t = new ETask()
            {
                Enable = true,
                Mapping = richTextBox1.Text,
                TaskName = txtTaskName.Text,
                Note = txtTaskNode.Text,
                SourceESchema = new ESchema()
                {
                    //Customer_ID = 1,
                    ESource = getESource(sdbId, sourcDbType, sourceTable),
                    Schema = getDataTableSchema(cboSDatabase.Text, sourceTable)
                },
                TargetESchema = new ESchema()
                {
                    //Customer_ID = 1,
                    ESource = getESource(tdbId, targetDbType, targetTable),
                    Schema = getDataTableSchema(cboTDatabase.Text, targetTable)
                },
                XSLTInfo = getXSLT(this.richTextBox1.Text),
                Category = String.IsNullOrEmpty(cmbPlateForm.Text) ? null : cmbPlateForm.Text
            };
            //交换任务
            if (cmbTaskType.SelectedIndex == 0)
                t.Type = "ET";
            //服务任务
            else if (cmbTaskType.SelectedIndex == 1)
                t.Type = "ST";
            //日志任务
            else if (cmbTaskType.SelectedIndex == 2)
                t.Type = "LT";
            return t;
        }

        private ESource getESource(int dbId, SourceType type, Table table)
        {
            var eds = new ESource()
            {
                //Customer_ID = 1,
                ESourceBaseInfo_ID = dbId,
                SourceType = Enum.GetName(typeof(SourceType), type),
                OriginalConfig = CommonUtility.SerializeXml<Table>(table)
            };

            return eds;
        }

        private string getDataTableSchema(string databaseName, Table table)
        {
            var ed = findEDatabase(databaseName);
            var p = GetSelectSQL(table);
            return dbSchema.GetDataTableSchema((SourceType)Enum.Parse(typeof(SourceType), ed.DatabaseType)
                                                , ed.Server
                                                , ed.Port
                                                , ed.Database
                                                , ed.UserId
                                                , ed.Password
                                                , p.Values.ToArray()
                                                , p.Keys.ToArray());
        }

        private string getXSLT(string mapping)
        {
            XslCompiledTransform tran = new XslCompiledTransform();

            // 适用WCF服务
            //tran.Load(AppDomain.CurrentDomain.BaseDirectory +"/bin/MappingToXSLT.xslt");

            // 适用动态库
            tran.Load(AppDomain.CurrentDomain.BaseDirectory + "/MappingToXSLT.xslt");

            StringReader sr = new StringReader(mapping);
            XmlReader xr = XmlReader.Create(sr);

            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb);
            tran.Transform(xr, xw);

            sr.Close();
            xr.Close();
            xw.Flush();
            xw.Close();

            return sb.ToString();
        }

        private EDatabase findEDatabase(string alias)
        {
            foreach (EDatabase db in dbList)
            {
                if (db.Alias == alias) { return db; }
            }
            return null;
        }

        public Dictionary<string, string> GetSelectSQL(Table t)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();

            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            foreach (EColumn c in t.Columns)
            {
                sb.Append(c.Name).Append(",");
            }
            sb.Remove(sb.Length - 1, 1);
            sb.Append(" from ").Append(t.TableName).Append(" where 1=0");

            dic.Add(t.TableName, sb.ToString());
            if (t.RelatedTables != null)
            {
                foreach (RelatedTable rt in t.RelatedTables)
                {
                    var r = GetSelectSQL(rt);
                    foreach (var p in r)
                    {
                        dic.Add(p.Key, p.Value);
                    }
                }
            }

            return dic;
        }

        /// <summary>
        /// 添加默认列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddDefaultColumn_Click(object sender, EventArgs e)
        {
            if (txtDefaultValue.Text.Trim().Length > 0 && cboTDefaultColumn.Text.Length > 0)
            {
                DataRow row = mappingDt.NewRow();
                row["targetName"] = cboTDefaultColumn.Text;
                row["defaultValue"] = txtDefaultValue.Text;
                mappingDt.Rows.Add(row);

                if (defaultColumns.ContainsKey(cboTDefaultColumn.Text))
                {
                    defaultColumns[cboTDefaultColumn.Text] = txtDefaultValue.Text;
                }
                else
                {
                    defaultColumns.Add(cboTDefaultColumn.Text, txtDefaultValue.Text);
                }

                cboTDefaultColumn.Text = String.Empty;
                txtDefaultValue.Text = String.Empty;
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            setDifferentInfoForeColor();
        }

        private void dataGridView1_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            setDifferentInfoForeColor();
        }

        private void setDifferentInfoForeColor()
        {
            foreach (DataGridViewRow dgvRow in dataGridView1.Rows)
            {
                if (dgvRow.Cells["sourceName"].Value.ToString().ToUpper() != dgvRow.Cells["targetName"].Value.ToString().ToUpper())
                    dgvRow.DefaultCellStyle.ForeColor = Color.DarkOrange;
                else
                    dgvRow.DefaultCellStyle.ForeColor = SystemColors.WindowText;
            }
        }
    }
}