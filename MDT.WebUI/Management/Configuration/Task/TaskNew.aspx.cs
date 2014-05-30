using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Text;

using MDT.Utility;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.ServiceImplement;
using MDT.ManageCenter.DataContract;

namespace MDT.WebUI.Management.Configuration.Task
{
    public partial class TaskNew : System.Web.UI.Page
    {
        private DataTable mappingDt;
        private SourceType sourcDbType;
        private SourceType targetDbType;
        private EDatabase sourceEDatabase;
        private EDatabase targetEDatabase;
        private List<EDatabase> dbList;
        private DbSchemaService dbSchema;
        private MDT.ManageCenter.DataContract.Table sourceTable;
        private MDT.ManageCenter.DataContract.Table targetTable;
        private Dictionary<string, string> defaultColumns;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Context.User.Identity.Name.Split(new char[] { '|' })[0] != "MDT2.0")
            {
                Response.Redirect("/Account/Login.aspx");
            }
            try
            {
                EDatabaseDAL dbDAL = new EDatabaseDAL();
                dbList = dbDAL.GetDatabases();
                dbSchema = new DbSchemaService();
                if (Page.IsPostBack)
                {
                    sourceTable = (MDT.ManageCenter.DataContract.Table)ViewState["sourceTable"];
                    targetTable = (MDT.ManageCenter.DataContract.Table)ViewState["targetTable"];
                    sourcDbType = (SourceType)ViewState["sourcDbType"];
                    targetDbType = (SourceType)ViewState["targetDbType"];
                    defaultColumns = (Dictionary<string, string>)ViewState["defaultColumns"];
                    mappingDt = (DataTable)ViewState["mappingDt"];
                }
                else
                {
                    defaultColumns = new Dictionary<string, string>();
                    mappingDt = new DataTable();
                    mappingDt.Columns.Add("sourceName", typeof(System.String));
                    mappingDt.Columns.Add("sourceType", typeof(System.String));
                    mappingDt.Columns.Add("targetName", typeof(System.String));
                    mappingDt.Columns.Add("targetType", typeof(System.String));
                    mappingDt.Columns.Add("defaultValue", typeof(System.String));

                    ViewState["sourceTable"] = sourceTable;
                    ViewState["targetTable"] = targetTable;
                    ViewState["sourcDbType"] = sourcDbType;
                    ViewState["targetDbType"] = targetDbType;
                    ViewState["defaultColumns"] = defaultColumns;
                    ViewState["mappingDt"] = mappingDt;

                    InitDataBase();//获取数据源信息
                    InitSTableName();//获取来源表信息
                    InitTTableName();//获取目标表信息
                    InitSColumn();//获取来源列信息
                    InitTColumn();//获取目标列信息
                    BindSTTable();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "") + "');</script>", false);
                return;
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>colunmWidth();changeColor();initDiv();initColAndType();initTable();</script>", false);
        }
        //初始化数据库
        private void InitDataBase()
        {
            StringBuilder strDataBase = new StringBuilder();
            foreach (var item in dbList)
            {
                strDataBase.Append(item.ID + "*" + item.Alias + "|");
            }
            if (dbList != null && dbList.Count > 0)
            {
                txtSDataBase.Value = dbList[0].Alias;
                txtSDataBaseId.Value = dbList[0].ID.ToString();
                txtTDataBase.Value = dbList[0].Alias;
                txtTDataBaseId.Value = dbList[0].ID.ToString();
                string str = strDataBase.ToString();
                hdb.Value = str.TrimEnd('|');
            }
        }
        //初始化表名 来源表
        private void InitSTableName()
        {
            sourceEDatabase = findEDatabase(txtSDataBase.Value);
            sourcDbType = (SourceType)Enum.Parse(typeof(SourceType), sourceEDatabase.DatabaseType);
            DataTable tb = dbSchema.GetTableInfo(sourcDbType, sourceEDatabase.Server, sourceEDatabase.Port, sourceEDatabase.Database, sourceEDatabase.UserId, sourceEDatabase.Password);
            StringBuilder strSTable = new StringBuilder();
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                strSTable.Append("0*" + tb.Rows[i]["TABLE_NAME"].ToString() + "|");
            }
            txtSTable.Value = tb.Rows[0]["TABLE_NAME"].ToString();
            string str = strSTable.ToString();
            hideSTable.Value = str.TrimEnd('|');
        }
        //初始化表名 目标表
        private void InitTTableName()
        {
            targetEDatabase = findEDatabase(txtTDataBase.Value);
            targetDbType = (SourceType)Enum.Parse(typeof(SourceType), targetEDatabase.DatabaseType);
            DataTable tb = dbSchema.GetTableInfo(targetDbType, targetEDatabase.Server, targetEDatabase.Port, targetEDatabase.Database, targetEDatabase.UserId, targetEDatabase.Password);
            StringBuilder strSTable = new StringBuilder();
            for (int i = 0; i < tb.Rows.Count; i++)
            {
                strSTable.Append("0*" + tb.Rows[i]["TABLE_NAME"].ToString() + "|");
            }
            txtTTable.Value = tb.Rows[0]["TABLE_NAME"].ToString();
            string str = strSTable.ToString();
            hideTTable.Value = str.TrimEnd('|');
        }
        /// <summary>
        /// 初始化来源表列信息
        /// </summary>
        private void InitSColumn()
        {
            DataTable sourceDt = null;
            if (sourceEDatabase == null)
            {
                sourceEDatabase = findEDatabase(this.txtSDataBase.Value);
            }
            sourcDbType = (SourceType)Enum.Parse(typeof(SourceType), sourceEDatabase.DatabaseType);
            ViewState["sourcDbType"] = sourcDbType;
            sourceDt = GetTableColumn(sourceEDatabase, sourcDbType, txtSTable.Value);
            StringBuilder strSColumn = new StringBuilder();
            for (int i = 0; i < sourceDt.Rows.Count; i++)
            {
                strSColumn.Append("0*" + sourceDt.Rows[i]["column_name"].ToString() + "|");
            }
            string strReturn = strSColumn.ToString().TrimEnd('|');
            hideSCol.Value = strReturn;
        }
        /// <summary>
        /// 初始化目标表列信息
        /// </summary>
        private void InitTColumn()
        {
            DataTable targetDt = null;
            if (targetEDatabase == null)
            {
                targetEDatabase = findEDatabase(this.txtTDataBase.Value);
            }
            targetDbType = (SourceType)Enum.Parse(typeof(SourceType), targetEDatabase.DatabaseType);
            ViewState["targetDbType"] = targetDbType;
            targetDt = GetTableColumn(targetEDatabase, targetDbType, txtTTable.Value);
            StringBuilder strTColumn = new StringBuilder();
            for (int i = 0; i < targetDt.Rows.Count; i++)
            {
                strTColumn.Append("0*" + targetDt.Rows[i]["column_name"].ToString() + "|");
            }
            string strReturn = strTColumn.ToString().TrimEnd('|');
            hideTCol.Value = strReturn;
        }
        ///获取表的列信息
        private DataTable GetTableColumn(EDatabase database, SourceType sourcDbType, string tableName)
        {
            DataTable dt;
            DataTable sourceDt = null;
            string sourceDataTypeName = String.Empty;
            dt = dbSchema.GetColumnInfo(sourcDbType, database.Server, database.Port, database.Database, database.UserId, database.Password, tableName);
            DataView dv = dt.DefaultView;
            dv.Sort = "column_name";
            sourceDt = dv.ToTable();
            return sourceDt;
        }
        /// <summary>
        /// 绑定列的映射信息
        /// </summary>
        private void BindSTTable()
        {
            mappingDt.Rows.Clear();
            if (txtSTable.Value.Trim().Length > 0 && txtTTable.Value.Trim().Length > 0)
            {
                int rowCount;
                DataTable dt;
                DataTable sourceDt;
                DataTable targetDt;
                string sourceDataTypeName = String.Empty;
                string targetDataTypeName = String.Empty;

                sourceEDatabase = findEDatabase(txtSDataBase.Value);
                sourcDbType = (SourceType)Enum.Parse(typeof(SourceType), sourceEDatabase.DatabaseType);
                ViewState["sourcDbType"] = sourcDbType;
                dt = dbSchema.GetColumnInfo(sourcDbType, sourceEDatabase.Server, sourceEDatabase.Port, sourceEDatabase.Database, sourceEDatabase.UserId, sourceEDatabase.Password, txtSTable.Value);

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
                ClearItems();

                //目标表
                targetEDatabase = findEDatabase(txtTDataBase.Value);
                targetDbType = (SourceType)Enum.Parse(typeof(SourceType), targetEDatabase.DatabaseType);
                ViewState["targetDbType"] = targetDbType;
                dt = dbSchema.GetColumnInfo(targetDbType, targetEDatabase.Server, targetEDatabase.Port, targetEDatabase.Database, targetEDatabase.UserId, targetEDatabase.Password, txtTTable.Value);
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
                    Repeater1.DataSource = mappingDt;
                    Repeater1.DataBind();
                }
            }
        }
        private ETask getETask()
        {
            int sdbId = int.Parse(txtSDataBaseId.Value);
            int tdbId = int.Parse(txtTDataBaseId.Value);
            ETask t = new ETask()
            {
                Mapping = hideMapping.Value,
                TaskName = txtTaskName.Text,
                Note = this.txtTaskDesc.Text,
                Enable = true,
                SourceESchema = new ESchema()
                {
                    //Customer_ID = 1,
                    ESource = getESource(sdbId, sourcDbType, sourceTable),
                    Schema = getDataTableSchema(txtSDataBase.Value, sourceTable)
                },
                TargetESchema = new ESchema()
                {
                    //Customer_ID = 1,
                    ESource = getESource(tdbId, targetDbType, targetTable),
                    Schema = getDataTableSchema(txtTDataBase.Value, targetTable)
                },
                XSLTInfo = getXSLT(hideMapping.Value)
            };
            return t;
        }

        private ESchema getSESchema()
        {
            int sdbId = int.Parse(txtSDataBaseId.Value);
            var SourceESchema = new ESchema()
            {
                //Customer_ID = 1,
                ESource = getESource(sdbId, sourcDbType, sourceTable),
                Schema = getDataTableSchema(txtSDataBase.Value, sourceTable)
            };
            return SourceESchema;
        }
        private ESchema getTESchema()
        {
            int tdbId = int.Parse(txtTDataBaseId.Value);
            targetTable = new MDT.ManageCenter.DataContract.Table
            {
                TableName = txtTTable.Value,
                Columns = new List<EColumn>(),
                RelatedTables = new List<RelatedTable>(),
                PKColumn = new EColumn()
                {
                    Name = txtTPrimaryKeys.Value
                }
            };
            buildColumnInfo(txtTDataBase.Value,targetTable, "targetName");
            var TargetESchema = new ESchema()
            {
                //Customer_ID = 1,
                ESource = getESource(tdbId, targetDbType, targetTable),
                Schema = getDataTableSchema(txtTDataBase.Value, targetTable)
            };
            return TargetESchema;
        }

        private ESource getESource(int dbId, SourceType type, MDT.ManageCenter.DataContract.Table table)
        {
            var eds = new ESource()
            {
                //Customer_ID = 1,
                ESourceBaseInfo_ID = dbId,
                SourceType = Enum.GetName(typeof(SourceType), type),
                OriginalConfig = CommonUtility.SerializeXml<MDT.ManageCenter.DataContract.Table>(table)
            };
            return eds;
        }

        private string getDataTableSchema(string databaseName, MDT.ManageCenter.DataContract.Table table)
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

        public Dictionary<string, string> GetSelectSQL(MDT.ManageCenter.DataContract.Table t)
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

        private void buildColumnInfo(string databaseName, MDT.ManageCenter.DataContract.Table table, string columnName)
        {
            string sql = "select * from " + table.TableName + " where 1=0";
            EDatabase database = findEDatabase(databaseName);
            DataTable schemaDt = dbSchema.GetDataTableSchema((SourceType)Enum.Parse(typeof(SourceType), database.DatabaseType)
                                                             , database.Server, database.Port, database.Database, database.UserId, database.Password
                                                             , sql, table.TableName);

            table.PKColumn.Type = schemaDt.Columns[table.PKColumn.Name].DataType.ToString();
            for (int i = 0; i < mappingDt.Rows.Count; i++)
            {
                DataRow row = mappingDt.Rows[i];
                // 是否添加默认列
                if (!String.IsNullOrEmpty(row["defaultValue"].ToString()))
                {
                    if (columnName == "sourceName") { continue; }
                }
                if (row[columnName].ToString().Trim() == "")//如果来源列为空
                {
                    continue;
                }
                table.Columns.Add(new EColumn
                {
                    Name = row[columnName].ToString(),
                    Type = schemaDt.Columns[row[columnName].ToString()].DataType.ToString()
                });
            }
        }

        private void buildColumnInfo(string databaseName, RelatedTable relatedTable, string columnName)
        {
            string sql = "select * from " + relatedTable.TableName + " where 1=0";
            EDatabase database = findEDatabase(databaseName);
            DataTable schemaDt = dbSchema.GetDataTableSchema((SourceType)Enum.Parse(typeof(SourceType), database.DatabaseType)
                                                             , database.Server, database.Port, database.Database, database.UserId, database.Password
                                                             , sql, sourceTable.TableName);

            relatedTable.ForeignColumn.Type = schemaDt.Columns[relatedTable.ForeignColumn.Name].DataType.ToString();
            foreach (DataRow row in mappingDt.Rows)
            {
                // 是否添加默认列
                if (!String.IsNullOrEmpty(row["defaultValue"].ToString()))
                {
                    if (columnName == "sourceName") { continue; }
                }
                if (row[columnName].ToString().Trim() == "")//如果来源列为空
                {
                    continue;
                }
                relatedTable.Columns.Add(new EColumn
                {
                    Name = row[columnName].ToString(),
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
                hideMapping.Value = doc.OuterXml;
            }
        }
        // 所有主键  外键选项清除
        private void ClearItems()
        {
            txtSPrimaryKeys.Value = "";
            txtTPrimaryKeys.Value = "";
            txtSForeignKeys.Value = "";
            txtTForeignKeys.Value = "";
        }
        private EDatabase findEDatabase(string alias)
        {
            foreach (EDatabase db in dbList)
            {
                if (db.Alias == alias) { return db; }
            }
            return null;
        }
        public string FormatDataType(string str)
        {//此方法用于转换数据字段的类型。
            string returnStr = "";
            str = str.ToLower();
            switch (str)
            {
                case "smallint":
                case "tinyint":
                case "int":
                case "money":
                case "float":
                case "decimal":
                case "numeric":
                case "number":
                case "smallmoney":
                case "bigint":
                case "real":
                case "xs:decimal":
                    returnStr = "数字型";
                    break;
                case "datetime":
                case "smalldatetime":
                case "date":
                case "xs:dateTime":
                    returnStr = "日期型";
                    break;
                case "":
                    break;
                default:
                    returnStr = "字符型";
                    break;
            }
            return returnStr;
        }
        protected void lbtnDeleteColunm_Click(object sender, EventArgs e)
        {
            try
            {
                mappingDt.Rows.Clear();//清空mapptingDt
                defaultColumns.Clear();
                string defaultValue = "";
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    RepeaterItem ri = Repeater1.Items[i];
                    CheckBox ckDelete = (CheckBox)ri.FindControl("ckDelete");
                    if (!ckDelete.Checked)
                    {
                        DataRow dr = mappingDt.NewRow();
                        dr["sourceName"] = ((TextBox)ri.FindControl("txtSourceCol")).Text;
                        dr["sourceType"] = ((TextBox)ri.FindControl("txtSourceType")).Text;
                        dr["targetName"] = ((TextBox)ri.FindControl("txtTargetCol")).Text;
                        dr["targetType"] = ((TextBox)ri.FindControl("txtTargetType")).Text;
                        defaultValue = ((TextBox)ri.FindControl("txtDefault")).Text;
                        dr["defaultValue"] = defaultValue;
                        if (defaultValue != "" || dr["sourceName"].ToString() == "")
                        {
                            defaultColumns.Add(dr["targetName"].ToString(), defaultValue);
                        }
                        mappingDt.Rows.Add(dr);
                    }
                }
                Repeater1.DataSource = mappingDt;
                Repeater1.DataBind();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1,typeof(UpdatePanel), "onload", "<script>alert('删除成功!');</script>",false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('删除失败！" + ex.Message.Replace("\"", "").Replace("'", "") + "');</script>",false);
                return;
            }
        }
        public void InputTable()
        {
            mappingDt.Rows.Clear();
            defaultColumns.Clear();
            if (txtSTable.Value.Trim().Length < 1 || txtTTable.Value.Trim().Length < 1)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请输入来源表名、目标表名！');</script>",false);
                return;
            }
            string defaultValue = "";
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem ri = Repeater1.Items[i];
                DataRow dr = mappingDt.NewRow();
                dr["sourceName"] = ((TextBox)ri.FindControl("txtSourceCol")).Text.ToLower();
                dr["sourceType"] = ((TextBox)ri.FindControl("txtSourceType")).Text;
                dr["targetName"] = ((TextBox)ri.FindControl("txtTargetCol")).Text.ToLower();
                dr["targetType"] = ((TextBox)ri.FindControl("txtTargetType")).Text;
                defaultValue = ((TextBox)ri.FindControl("txtDefault")).Text;
                dr["defaultValue"] = defaultValue;
                if (defaultValue != "" || dr["sourceName"].ToString() == "")
                {
                    defaultColumns.Add(dr["targetName"].ToString(), defaultValue);
                }
                mappingDt.Rows.Add(dr);
            }
        }
        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            InputTable();
            if (sourceTable == null || sourceTable.Columns == null || sourceTable.Columns.Count == 0)
            {
                // 添加主表信息
                if(txtSPrimaryKeys.Value.Trim().Length == 0 || txtTPrimaryKeys.Value.Trim().Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请选择表的主键信息！');</script>", false);
                    return;
                }
                else
                {
                    sourceTable = new MDT.ManageCenter.DataContract.Table   // 添加来源表
                    {
                        TableName = txtSTable.Value,
                        Columns = new List<EColumn>(),
                        RelatedTables = new List<RelatedTable>(),
                        PKColumn = new EColumn()
                        {
                            Name = txtSPrimaryKeys.Value
                        }
                    };
                    buildColumnInfo(txtSDataBase.Value, sourceTable, "sourceName");
                    ViewState["sourceTable"] = sourceTable;
                    // 添加目标表
                    targetTable = new MDT.ManageCenter.DataContract.Table
                    {
                        TableName = txtTTable.Value,
                        Columns = new List<EColumn>(),
                        RelatedTables = new List<RelatedTable>(),
                        PKColumn = new EColumn()
                        {
                            Name = txtTPrimaryKeys.Value
                        }
                    };
                    buildColumnInfo(txtTDataBase.Value, targetTable, "targetName");
                    ViewState["targetTable"] = targetTable;
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
                if(txtSForeignKeys.Value.Trim().Length == 0 || txtTForeignKeys.Value.Trim().Length == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请选择表的外键信息！');</script>", false);
                    return;
                }
                else
                {
                    RelatedTable relatedTable = new RelatedTable
                    {
                        TableName = txtSTable.Value,
                        Columns = new List<EColumn>(),
                        ForeignColumn = new EColumn()
                        {
                            Name = txtSForeignKeys.Value
                        }
                    };
                    buildColumnInfo(txtSDataBase.Value, relatedTable, "sourceName");//ddlSDatabase.SelectedItem.Text, relatedTable, "sourceName");
                    sourceTable.RelatedTables.Add(relatedTable);

                    relatedTable = new RelatedTable
                    {
                        TableName = txtTTable.Value,
                        Columns = new List<EColumn>(),
                        ForeignColumn = new EColumn()
                        {
                            Name = txtTForeignKeys.Value
                        }
                    };
                    buildColumnInfo(txtTDataBase.Value, relatedTable, "targetName");//ddlTDatabase.Text, relatedTable, "targetName");
                    targetTable.RelatedTables.Add(relatedTable);
                }
            }
            buildMapping();
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>open_file1('mapping', 'hideMapping', 'Mapping');</script>");
            txtPostTaskName.Text = String.Empty;
        }
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (sourceTable == null || sourceTable.Columns == null || sourceTable.Columns.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请配置完整再保存信息！');</script>", false);
                    return;
                }
                ManageCenterService s = new ManageCenterService();
                s.AddTask(getETask());
                sourceTable = null;
                targetTable = null;
                txtSPrimaryKeys.Value = "";
                txtTPrimaryKeys.Value = "";
                txtSForeignKeys.Value = "";
                txtTForeignKeys.Value = "";
                defaultColumns.Clear();
                hideMapping.Value = String.Empty;
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('保存成功！');</script>", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1,typeof(UpdatePanel),"onload","<script>alert('"+ex.Message.Replace("\"","").Replace("'","").Replace("\r\n","")+"');</script>",false);
            }
        }

        protected void lbtnSD_Click(object sender, EventArgs e)
        {
            try
            {
                txtSPrimaryKeys.Value = "";
                txtSForeignKeys.Value = "";
                InitSTableName();
                InitSColumn();
                BindSTTable();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("'", "").Replace("\"", "") + "');</script>",false);
            }
        }
        protected void lblTD_Click(object sender, EventArgs e)
        {
            try
            {
                txtTPrimaryKeys.Value = "";
                txtTForeignKeys.Value = "";
                InitTTableName();
                InitTColumn();
                BindSTTable();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("'", "").Replace("\"", "") + "');</script>",false);
            }
        }
        protected void lblST_Click(object sender, EventArgs e)
        {
            try
            {
                txtSPrimaryKeys.Value = "";
                txtSForeignKeys.Value = "";
                txtTPrimaryKeys.Value = "";
                txtTForeignKeys.Value = "";
                InitSColumn();
                InitTColumn();
                BindSTTable();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("'", "").Replace("\"", "") + "');</script>",false);
                return;
            }
        }

        protected void lbtnDeleteTask_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// 生成空任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSaveNullTask_Click(object sender, EventArgs e)
        {
            try
            {
                ManageCenterService s = new ManageCenterService();
                s.AddTask(txtTaskName.Text, txtTaskDesc.Text);
                sourceTable = null;
                targetTable = null;
                defaultColumns.Clear();
                txtXMLData.Value = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('生成空任务成功！');</script>", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('"+ex.Message.Replace("\"","").Replace("'","").Replace("\r\n","")+"');</script>", false);
            }
        }

        
    }
}