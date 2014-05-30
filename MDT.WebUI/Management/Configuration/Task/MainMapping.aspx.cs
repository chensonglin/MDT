using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Text;
using System.Collections;

using MDT.Utility;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.ServiceImplement;
using MDT.ManageCenter.DataContract;


namespace MDT.WebUI.Management.Configuration.Task
{
    public partial class MainMapping : System.Web.UI.Page
    {
        private DataTable mappingDt; //保存源 ——> 目标  表的配置 
        private SourceType sourcDbType;
        private SourceType targetDbType;
        private EDatabase sourceEDatabase;
        private EDatabase targetEDatabase;
        private List<EDatabase> dbList;
        private DbSchemaService dbSchema;
        private MDT.ManageCenter.DataContract.Table sourceTable;
        private MDT.ManageCenter.DataContract.Table targetTable;

        string basePath = "";
        public string BasePath
        {
            get { return hideFileName.Value; }
            set { basePath = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            EDatabaseDAL dbDAL = new EDatabaseDAL();
            dbList = dbDAL.GetDatabases();//查询数据库列表
            dbSchema = new DbSchemaService();

            if (!Page.IsPostBack)//不是回发
            {
                mappingDt = new DataTable();
                mappingDt.Columns.Add("sourceName", typeof(System.String));  //来源列名
                mappingDt.Columns.Add("sourceType", typeof(System.String));  //来源类型
                mappingDt.Columns.Add("targetName", typeof(System.String));   //目标列名
                mappingDt.Columns.Add("targetType", typeof(System.String));  //目标类型
                mappingDt.Columns.Add("defaultValue", typeof(System.String)); //默认值

                ViewState["sourceTable"] = sourceTable; //来源表
                ViewState["targetTable"] = targetTable; //目标表
                ViewState["sourcDbType"] = sourcDbType; //来源数据库类型
                ViewState["targetDbType"] = targetDbType;  //目标数据库类型
                ViewState["mappingDt"] = mappingDt;

                try
                {
                    InitDataBase();
                    if(Request.QueryString["fileName"] == null || Request.QueryString["fileName"].ToString() == "")
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请从正确的页面进入！');closeDiv2('hideMapping','iframeMapping');</script>", false);
                        return;
                    }
                    hideFileName.Value = Request.QueryString["fileName"].ToString();//将路径值保存到隐藏域中
                    string strPath = Server.MapPath(BasePath + "Mapping.xml");
                    if (!File.Exists(strPath))//如果还没生成Mapping.xml则显示默认数据
	                {
                        InitSTableName();
                        InitTTableName();
                        BindSTTable();//显示默认数据。
                        ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>$(function () {$('#dataTable').trOddHilight();$('#dataTable').trClick();});changeColor();init();colunmWidth();</script>", false);
                        return;
	                }
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(strPath);
                    if (Request.QueryString["Id"] != null && Request.QueryString["Id"].ToString() != "")//如果Id 不为空，则目前是修改操作
                    {
                        int id = Convert.ToInt32(Request.QueryString["Id"]);
                        ViewState["Id"] = id;
                        XmlNode nlMDT = xmlDoc.SelectSingleNode("/Tables/Table[@Id=" + id + "]");
                        if (nlMDT == null)
                        {
                            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('在Mapping.xml文件中没有找到相对应的信息！');</script>", false);
                            return;
                        }
                        txtSDataBase.Value = nlMDT.Attributes["SourceDb"].Value;
                        txtTDataBase.Value = nlMDT.Attributes["TargetDb"].Value;
                        InitSTableName();
                        InitTTableName();
                        txtSTable.Value = nlMDT.Attributes["Source"].Value;
                        txtTTable.Value = nlMDT.Attributes["Target"].Value;
                        XmlNodeList nlDefaultValue = xmlDoc.SelectNodes("/Tables/Table[@Id=" + id + "]/Column[@Value]");
                        Dictionary<string, string> dicDefaultValue = new Dictionary<string, string>();
                        foreach (XmlNode node in nlDefaultValue)
                        {
                            string value = node.Attributes["Value"].Value;
                            string target = node.Attributes["Target"].Value;
                            dicDefaultValue.Add(target, value);
                        }
                        ViewState["dicDefaultValue"] = dicDefaultValue;//保存默认值集合
                        BindSTTableForModify();//如果是修改，则加载页面时加载原有数据。
                        InitSColumn();//将来源表的所有字段填充到hideSCol文本框中
                        InitTColumn();//将目标表的所有字段填充到hideTCol文本框中
                    }
                    else
                    {
                        XmlNode nlOne = xmlDoc.SelectSingleNode("/Tables/Table");
                        if (nlOne != null)
                        {
                            txtSDataBase.Value = nlOne.Attributes["SourceDb"].Value;
                            InitSTableName();
                            InitTTableName();
                            txtSTable.Value = nlOne.Attributes["Source"].Value;
                        }
                        BindSTTable();//如果不是修改，则显示默认数据。
                    }
                    
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "", "<script>alert('加载数据出现异常！"+ex.Message.Replace("'","").Replace("\"","")+"');</script>", false);
                    return;
                }
            }
            else
            {
                sourceTable = (MDT.ManageCenter.DataContract.Table)ViewState["sourceTable"];
                targetTable = (MDT.ManageCenter.DataContract.Table)ViewState["targetTable"];
                sourcDbType = (SourceType)ViewState["sourcDbType"];
                targetDbType = (SourceType)ViewState["targetDbType"];
                mappingDt = (DataTable)ViewState["mappingDt"];
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>$(function () {$('#dataTable').trOddHilight();$('#dataTable').trClick();});changeColor();init();colunmWidth();</script>", false);
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
                txtTDataBase.Value = dbList[0].Alias;
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
        /// <summary>
        /// 修改时加载数据
        /// </summary>
        private void BindSTTableForModify()
        {
            try
            {
                mappingDt.Rows.Clear();
                //if (Session["basePath"] == null && Session["basePath"].ToString() == "")
                //if (ViewState["basePath"] == null && ViewState["basePath"].ToString() == "")
                //{
                //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('没有找到文件路径!');</script>", false);
                //    return;
                //}
                int id = Convert.ToInt32(ViewState["Id"]);
                //string basePath = Session["basePath"].ToString();
                //string basePath = ViewState["basePath"].ToString();
                Dictionary<string, string> dicDefaultValue = new Dictionary<string, string>();
                if (ViewState["dicDefaultValue"] != null)
                {
                    dicDefaultValue = (Dictionary<string, string>)ViewState["dicDefaultValue"];
                }
                string path1 = Server.MapPath(BasePath + "SourceSchema.xml");
                XmlDocument docSourceSchema = new XmlDocument();
                docSourceSchema.Load(path1);
                XmlNamespaceManager m = new XmlNamespaceManager(docSourceSchema.NameTable);
                m.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
                XmlNode nlSource = docSourceSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element[@Id='" + id + "']/xs:complexType/xs:sequence", m);//[@name=" + txtSTable.Value + "]
                XmlNodeList nlSource2 = null;
                if (nlSource == null)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdateException), "onload", "<script>alert('在SourceSchema.xml文件中没有获取到相应节点！');</script>", false);
                    return;
                }
                string path2 = Server.MapPath(BasePath + "TargetSchema.xml");
                XmlDocument docTargetSchema = new XmlDocument();
                docTargetSchema.Load(path2);
                XmlNode nlTarget = docTargetSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element[@Id='" + id + "']/xs:complexType/xs:sequence", m);
                XmlNodeList nlTarget2 = null;
                if (nlTarget == null)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdateException), "onload", "<script>alert('在TargetSchema.xml文件中没有获取到相应节点！');</script>", false);
                    return;
                }
                nlSource2 = nlSource.ChildNodes;
                nlTarget2 = nlTarget.ChildNodes;
                int countRows = nlSource2.Count > nlTarget2.Count ? nlSource2.Count : nlTarget2.Count;
                for (int i = 0; i < countRows; i++)
                {
                    DataRow row1 = mappingDt.NewRow();
                    if (i < nlSource2.Count)
                    {
                        row1["sourceName"] = nlSource2[i].Attributes["name"].Value;
                        row1["sourceType"] = nlSource2[i].Attributes["type"].Value;
                    }
                    if (i < nlTarget2.Count)
                    {
                        string name = nlTarget2[i].Attributes["name"].Value;
                        row1["targetName"] = name;
                        row1["targetType"] = nlTarget2[i].Attributes["type"].Value;
                        if (dicDefaultValue.Keys.Contains(name))
                        {
                            row1["defaultValue"] = dicDefaultValue[name];
                        }
                    }
                    mappingDt.Rows.Add(row1);
                }
                Repeater1.DataSource = mappingDt;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace('\'', ' ') + "');</script>", false);
                return;
            }
        }
        /// <summary>
        /// 绑定对应列数据
        /// </summary>
        private void BindSTTable()
        {
            mappingDt.Rows.Clear();
            if (txtSTable.Value.Trim().Length < 1 || txtTTable.Value.Trim().Length < 1)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请输入来源表名、目标表名！');</script>", false);
                return;
            }
            try
            {
                int rowCount;
                DataTable sourceDt = null;
                DataTable targetDt = null;
                string sourceDataTypeName = String.Empty;
                string targetDataTypeName = String.Empty;

                sourceEDatabase = findEDatabase(this.txtSDataBase.Value);//获取来源数据
                sourcDbType = (SourceType)Enum.Parse(typeof(SourceType), sourceEDatabase.DatabaseType);
                ViewState["sourcDbType"] = sourcDbType;
                if (sourcDbType == SourceType.SqlServer || sourcDbType == SourceType.MySql)
                {
                    sourceDataTypeName = "data_type";
                }
                else if (sourcDbType == SourceType.Oracle)
                {
                    sourceDataTypeName = "datatype";
                }
                sourceDt = GetTableColumn(sourceEDatabase, sourcDbType, txtSTable.Value);
                //目标表
                targetEDatabase = findEDatabase(this.txtTDataBase.Value);
                targetDbType = (SourceType)Enum.Parse(typeof(SourceType), targetEDatabase.DatabaseType);
                ViewState["targetDbType"] = targetDbType;
                if (targetDbType == SourceType.SqlServer || targetDbType == SourceType.MySql)
                {
                    targetDataTypeName = "data_type";
                }
                else
                {
                    targetDataTypeName = "datatype";
                }
                targetDt = GetTableColumn(targetEDatabase, targetDbType, txtTTable.Value);

                if (sourceDt != null && sourceDt.Rows.Count > 0 && targetDt != null && targetDt.Rows.Count > 0)
                {
                    rowCount = sourceDt.Rows.Count > targetDt.Rows.Count ? sourceDt.Rows.Count : targetDt.Rows.Count;
                    StringBuilder strSCol = new StringBuilder();
                    StringBuilder strTCol = new StringBuilder();
                    for (int i = 0; i < rowCount; i++)
                    {
                        DataRow newRow = mappingDt.NewRow();
                        if (i < sourceDt.Rows.Count)
                        {
                            newRow["sourceName"] = sourceDt.Rows[i]["column_name"];
                            newRow["sourceType"] = sourceDt.Rows[i][sourceDataTypeName];
                            strSCol.Append("0*" + sourceDt.Rows[i]["column_name"] + "|");
                        }
                        if (i < targetDt.Rows.Count)
                        {
                            newRow["targetName"] = targetDt.Rows[i]["column_name"];
                            newRow["targetType"] = targetDt.Rows[i][targetDataTypeName];
                            strTCol.Append("0*" + targetDt.Rows[i]["column_name"] + "|");
                        }
                        mappingDt.Rows.Add(newRow);
                    }
                    hideSCol.Value = strSCol.ToString().TrimEnd('|');
                    hideTCol.Value = strTCol.ToString().TrimEnd('|');
                }
                Repeater1.DataSource = mappingDt;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace('\'', ' ') + "');</script>", false);
                return;
            }
        }
        /// <summary>
        /// 获取  表的列信息
        /// </summary>
        /// <param name="database"></param>
        /// <param name="sourcDbType"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DataTable GetTableColumn(EDatabase database,SourceType sourcDbType,string tableName)
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
        /// 根据数据库名称获取数据库对象
        /// </summary>
        /// <param name="alias">数据库名称</param>
        /// <returns>数据库对象</returns>
        private EDatabase findEDatabase(string alias)
        {
            foreach (EDatabase db in dbList)
            {
                if (db.Alias == alias) { return db; }
            }
            return null;
        }
        /// <summary>
        /// 保存按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            try
            {
                if (Repeater1.Items.Count == 0) { return; }
                InputTable();//将repeater 中修改的数据填充到mappingDt中
                if (sourceTable == null || sourceTable.Columns == null || sourceTable.Columns.Count == 0)
                {
                    // 添加来源表
                    sourceTable = new ManageCenter.DataContract.Table
                    {
                        TableName = txtSTable.Value,
                        Columns = new List<EColumn>(),
                        RelatedTables = new List<RelatedTable>(),
                    };
                    buildColumnInfo(txtSDataBase.Value, sourceTable, "sourceName");
                    // 添加目标表
                    targetTable = new ManageCenter.DataContract.Table
                    {
                        TableName = txtTTable.Value,
                        Columns = new List<EColumn>(),
                        RelatedTables = new List<RelatedTable>(),
                    };
                    buildColumnInfo(txtTDataBase.Value, targetTable, "targetName");
                }
                if (ViewState["Id"] != null && ViewState["Id"].ToString() != "")
                {
                    buildMappingForModify(); //修改时 构造Mapping XSLTInfo SourceConfig TargetConfig
                }
                else
                {
                    buildMapping();  //新增时构造Mapping XSLTInfo SourceConfig TargetConfig
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('保存失败！" + ex.Message.Replace('\'', ' ') + "');</script>", false);
            }
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>openerRefresh();alert('保存成功！');</script>", false);
        }

        private void buildColumnInfo(string databaseName, MDT.ManageCenter.DataContract.Table table, string columnName)
        {
            string sql = "select * from " + table.TableName + " where 1=0";
            EDatabase database = findEDatabase(databaseName);
            DataTable schemaDt = dbSchema.GetDataTableSchema((SourceType)Enum.Parse(typeof(SourceType), database.DatabaseType)
                                                             , database.Server, database.Port, database.Database, database.UserId, database.Password
                                                             , sql, table.TableName);
            foreach (DataRow row in mappingDt.Rows)
            {
                // 是否添加默认列
                if (!String.IsNullOrEmpty(row["defaultValue"].ToString()))
                {
                    if (columnName == "sourceName") { continue; }
                }
                if (row[columnName].ToString() != "")
                {
                    table.Columns.Add(new EColumn
                    {
                        Name = row[columnName].ToString(),
                        Type = schemaDt.Columns[row[columnName].ToString()].DataType.ToString()
                    });
                }
            }
        }
        /// <summary>
        /// 修改时 构造  Mapping  XSLT Schema SourceConfig 的xml文件
        /// </summary>
        private void buildMappingForModify()
        {
            if (sourceTable.Columns == null || sourceTable.Columns.Count <= 0)
            {
                return;
            }
            int id = Convert.ToInt32(ViewState["Id"]);
            sourceEDatabase = findEDatabase(txtSDataBase.Value);
            targetEDatabase = findEDatabase(txtTDataBase.Value);
            //if (Session["basePath"] != null && Session["basePath"].ToString() != "")
            //if (ViewState["basePath"] != null && ViewState["basePath"].ToString() != "")
            //{
                //string basePath = Session["basePath"].ToString();
                //string basePath = ViewState["basePath"].ToString();
                //更新Mapping
                string path1 = Server.MapPath(BasePath + "Mapping.xml");
                XmlDocument docMDT2 = new XmlDocument();
                docMDT2.Load(path1);

                XmlNode nlMapping = docMDT2.SelectSingleNode("/Tables/Table[@Id=" + id + "]");
                if (nlMapping == null)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('在Mapping.xml中没有找到相应节点，修改失败！');</script>", false);
                    return;
                }
                string tableId = nlMapping.Attributes["Id"].Value;
                XmlElement xe1m = (XmlElement)nlMapping;
                xe1m.RemoveAll();
                xe1m.SetAttribute("Source", sourceTable.TableName);
                xe1m.SetAttribute("Target", targetTable.TableName);
                xe1m.SetAttribute("SourceDb", txtSDataBase.Value);
                xe1m.SetAttribute("TargetDb", txtTDataBase.Value);
                xe1m.SetAttribute("Id", tableId);

                for (int i = 0; i < mappingDt.Rows.Count; i++)
                {
                    DataRow row2 = mappingDt.Rows[i];
                    XmlElement xe2m = docMDT2.CreateElement("Column");
                    if (row2["defaultValue"].ToString() != "")
                    {
                        xe2m.SetAttribute("Value", row2["defaultValue"].ToString());
                        xe2m.SetAttribute("Target", row2["targetName"].ToString());
                    }
                    else
                    {
                        xe2m.SetAttribute("Source", row2["sourceName"].ToString());
                        xe2m.SetAttribute("Target", row2["targetName"].ToString());
                    }
                    xe1m.AppendChild(xe2m);
                }
                nlMapping.ParentNode.ReplaceChild(xe1m, nlMapping);
                docMDT2.Save(path1);

                //更新XSLT 
                path1 = Server.MapPath(BasePath + "XSLT.xml");
                XslCompiledTransform tran2 = new XslCompiledTransform();
                tran2.Load(AppDomain.CurrentDomain.BaseDirectory + "/MappingToXSLT.xslt");
                StringBuilder sb2 = new StringBuilder();
                XmlWriter xw2 = XmlWriter.Create(sb2);
                tran2.Transform(docMDT2, xw2);
                xw2.Flush();
                xw2.Close();
                XmlDocument xmlXSLT3 = new XmlDocument();
                xmlXSLT3.LoadXml(sb2.ToString());
                xmlXSLT3.Save(path1);

                //更新源Schema
                string sourceSchema2 = getDataTableSchema(txtSDataBase.Value, sourceTable);
                path1 = Server.MapPath(BasePath + "SourceSchema.xml");
                XmlDocument xmlDocSourceSchema = new XmlDocument();
                xmlDocSourceSchema.Load(path1);
                docMDT2 = new XmlDocument();
                docMDT2.LoadXml(sourceSchema2);
                XmlNamespaceManager m = new XmlNamespaceManager(docMDT2.NameTable);
                m.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
                XmlNode nlStr = docMDT2.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m);
                XmlNode nlChoice = xmlDocSourceSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element[@Id=" + id + "]", m);
                if (nlChoice != null && nlStr != null)
                {
                    string elementId = nlChoice.Attributes["Id"].Value;
                    XmlAttribute a1 = docMDT2.CreateAttribute("Id");
                    a1.Value = elementId;
                    nlStr.Attributes.Append(a1);
                    XmlNode x2 = xmlDocSourceSchema.ImportNode(nlStr, true);
                    nlChoice.ParentNode.ReplaceChild(x2, nlChoice);
                }
                xmlDocSourceSchema.Save(path1);

                //更新目标Schema
                string targetSchema2 = getDataTableSchema(txtTDataBase.Value, targetTable);
                path1 = Server.MapPath(BasePath + "TargetSchema.xml");
                XmlDocument xmlDocTargetSchema = new XmlDocument();
                xmlDocTargetSchema.Load(path1);
                docMDT2 = new XmlDocument();
                docMDT2.LoadXml(targetSchema2);
                XmlNode nlStr2 = docMDT2.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m);
                XmlNode nlChoice2 = xmlDocTargetSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element[@Id=" + id + "]", m);
                if (nlChoice2 != null && nlStr2 != null)
                {
                    string elemetnId = nlChoice2.Attributes["Id"].Value;
                    XmlAttribute a1 = docMDT2.CreateAttribute("Id");
                    a1.Value = elemetnId;
                    nlStr2.Attributes.Append(a1);
                    XmlNode x2 = xmlDocTargetSchema.ImportNode(nlStr2, true);
                    nlChoice2.ParentNode.ReplaceChild(x2, nlChoice2);
                }
                xmlDocTargetSchema.Save(path1);

                //更新源SourceConfig
                path1 = Server.MapPath(BasePath + "SourceConfig.xml");
                docMDT2 = new XmlDocument();
                docMDT2.Load(path1);
                XmlNode nlSourceConfig = docMDT2.SelectSingleNode("//MainTasks/TaskUnit[Id=" + id + "]");
                XmlElement xe = (XmlElement)nlSourceConfig;
                xe.SetAttribute("name", sourceEDatabase.Alias + "_" + targetEDatabase.Alias);
                nlSourceConfig.ParentNode.ReplaceChild(xe, nlSourceConfig);
                docMDT2.Save(path1);

                //更新目标SourceConfig
                path1 = Server.MapPath(BasePath + "TargetConfig.xml");
                docMDT2 = new XmlDocument();
                docMDT2.Load(path1);
                XmlNode nlTargetConfig = docMDT2.SelectSingleNode("//MainTasks/TaskUnit[Id=" + id + "]");
                XmlElement xe2 = (XmlElement)nlTargetConfig;
                xe2.SetAttribute("name", sourceEDatabase.Alias + "_" + targetEDatabase.Alias);
                nlTargetConfig.ParentNode.ReplaceChild(xe2, nlTargetConfig);
                docMDT2.Save(path1);
            //}
        }
        /// <summary>
        /// 添加时 构造  Mapping  XSLT  Schema  SourceConfig 的xml文件
        /// </summary>
        private void buildMapping()
        {
            if (sourceTable.Columns == null || sourceTable.Columns.Count <= 0)
            {
                return;
            }
            sourceEDatabase = findEDatabase(txtSDataBase.Value);
            targetEDatabase = findEDatabase(txtTDataBase.Value);
            //if (Session["basePath"] != null && Session["basePath"].ToString() != "")
            //string basePath = ViewState["basePath"].ToString();
            //if (ViewState["basePath"] != null && ViewState["basePath"].ToString() != "")
            //{
            //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1,typeof(UpdatePanel),"onload","<script>alert('在ViewState中没有找到路径！');</script>",false);
            //    return;
            //}
            string path1 = Server.MapPath(BasePath + "Mapping.xml");
            if (File.Exists(path1))
            {
                //string basePath = Session["basePath"].ToString();
                //string basePath = ViewState["basePath"].ToString();
                //更新Mapping
                //string path1 = Server.MapPath(basePath + "Mapping.xml");
                XmlDocument docMDT2 = new XmlDocument();
                docMDT2.Load(path1);
                XmlElement xe1m = docMDT2.CreateElement("Table");
                xe1m.SetAttribute("Source", sourceTable.TableName);
                xe1m.SetAttribute("Target", targetTable.TableName);
                xe1m.SetAttribute("SourceDb", txtSDataBase.Value);
                xe1m.SetAttribute("TargetDb", txtTDataBase.Value);
                XmlNode nlLast = docMDT2.SelectSingleNode("/Tables").LastChild;//取最后一个Table的Id
                int countTable = 0;
                if (nlLast != null)
                {
                    countTable = Convert.ToInt32(nlLast.Attributes["Id"].Value);
                }
                xe1m.SetAttribute("Id", (countTable + 1).ToString());
                for (int i = 0; i < mappingDt.Rows.Count; i++)
                {
                    DataRow row2 = mappingDt.Rows[i];
                    XmlElement xe2m = docMDT2.CreateElement("Column");
                    if (row2["defaultValue"].ToString() != "")
                    {
                        xe2m.SetAttribute("Value", row2["defaultValue"].ToString());
                        xe2m.SetAttribute("Target", row2["targetName"].ToString());
                    }
                    else
                    {
                        xe2m.SetAttribute("Source", row2["sourceName"].ToString());
                        xe2m.SetAttribute("Target", row2["targetName"].ToString());
                    }
                    xe1m.AppendChild(xe2m);
                }
                docMDT2.DocumentElement.AppendChild(xe1m);
                docMDT2.Save(path1);

                //更新XSLT 
                path1 = Server.MapPath(BasePath + "XSLT.xml");
                XslCompiledTransform tran2 = new XslCompiledTransform();
                tran2.Load(AppDomain.CurrentDomain.BaseDirectory + "/MappingToXSLT.xslt");
                StringBuilder sb2 = new StringBuilder();
                XmlWriter xw2 = XmlWriter.Create(sb2);
                tran2.Transform(docMDT2, xw2);
                xw2.Flush();
                xw2.Close();
                XmlDocument xmlXSLT2 = new XmlDocument();
                xmlXSLT2.LoadXml(sb2.ToString());
                xmlXSLT2.Save(path1);

                //更新源Schema
                string sourceSchema2 = getDataTableSchema(txtSDataBase.Value, sourceTable);
                path1 = Server.MapPath(BasePath + "SourceSchema.xml");
                XmlDocument xmlDocSourceSchema = new XmlDocument();
                xmlDocSourceSchema.Load(path1);
                docMDT2 = new XmlDocument();
                docMDT2.LoadXml(sourceSchema2);
                XmlNamespaceManager m = new XmlNamespaceManager(docMDT2.NameTable);
                m.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
                XmlNode nlStr = docMDT2.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m);
                XmlNode nlChoice = xmlDocSourceSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice", m);
                if (nlChoice != null && nlStr != null)
                {
                    XmlAttribute a1 = docMDT2.CreateAttribute("Id");
                    a1.Value = (countTable + 1).ToString();
                    nlStr.Attributes.Append(a1);
                    XmlNode x2 = xmlDocSourceSchema.ImportNode(nlStr, true);
                    nlChoice.AppendChild(x2);
                }
                xmlDocSourceSchema.Save(path1);

                //更新目标Schema
                string targetSchema2 = getDataTableSchema(txtTDataBase.Value, targetTable);
                path1 = Server.MapPath(BasePath + "TargetSchema.xml");
                XmlDocument xmlDocTargetSchema = new XmlDocument();
                xmlDocTargetSchema.Load(path1);
                docMDT2 = new XmlDocument();
                docMDT2.LoadXml(targetSchema2);
                XmlNode nlStr2 = docMDT2.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m);
                XmlNode nlChoice2 = xmlDocTargetSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice", m);
                if (nlChoice2 != null && nlStr2 != null)
                {
                    XmlAttribute a1 = docMDT2.CreateAttribute("Id");
                    a1.Value = (countTable + 1).ToString();
                    nlStr2.Attributes.Append(a1);
                    XmlNode x2 = xmlDocTargetSchema.ImportNode(nlStr2, true);
                    nlChoice2.AppendChild(x2);
                }
                xmlDocTargetSchema.Save(path1);

                //更新源SourceConfig
                path1 = Server.MapPath(BasePath + "SourceConfig.xml");
                docMDT2 = new XmlDocument();
                docMDT2.Load(path1);
                XmlElement xeTU1 = docMDT2.CreateElement("TaskUnit");
                XmlElement xeTU2 = docMDT2.CreateElement("Commands");
                XmlElement xeTU3 = docMDT2.CreateElement("HasTraceLog");
                XmlElement xeTU4 = docMDT2.CreateElement("HasTransaction");
                XmlElement xeTU5 = docMDT2.CreateElement("Id");
                XmlElement xeTU6 = docMDT2.CreateElement("Results");
                xeTU1.SetAttribute("name", sourceEDatabase.Alias + "_" + targetEDatabase.Alias);
                xeTU5.InnerText = (countTable + 1) + "";
                xeTU1.AppendChild(xeTU2);
                xeTU1.AppendChild(xeTU3);
                xeTU1.AppendChild(xeTU4);
                xeTU1.AppendChild(xeTU5);
                xeTU1.AppendChild(xeTU6);
                docMDT2.DocumentElement["MainTasks"].AppendChild(xeTU1);
                docMDT2.Save(path1);

                //更新目标SourceConfig
                path1 = Server.MapPath(BasePath + "TargetConfig.xml");
                docMDT2 = new XmlDocument();
                docMDT2.Load(path1);
                xeTU1 = docMDT2.CreateElement("TaskUnit");
                xeTU2 = docMDT2.CreateElement("Commands");
                xeTU3 = docMDT2.CreateElement("HasTraceLog");
                xeTU4 = docMDT2.CreateElement("HasTransaction");
                xeTU5 = docMDT2.CreateElement("Id");
                xeTU6 = docMDT2.CreateElement("Results");
                xeTU1.SetAttribute("name", sourceEDatabase.Alias + "_" + targetEDatabase.Alias);
                xeTU5.InnerText = (countTable + 1) + "";
                xeTU1.AppendChild(xeTU2);
                xeTU1.AppendChild(xeTU3);
                xeTU1.AppendChild(xeTU4);
                xeTU1.AppendChild(xeTU5);
                xeTU1.AppendChild(xeTU6);
                docMDT2.DocumentElement["MainTasks"].AppendChild(xeTU1);
                docMDT2.Save(path1);
                return;
            }
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("<Tables></Tables>");
            XmlElement parentElement = doc.CreateElement("Table");
            parentElement.SetAttribute("Source", sourceTable.TableName);
            parentElement.SetAttribute("Target", targetTable.TableName);
            parentElement.SetAttribute("Id", "1");
            parentElement.SetAttribute("SourceDb", txtSDataBase.Value);
            parentElement.SetAttribute("TargetDb", txtTDataBase.Value);
            for (int i = 0; i < mappingDt.Rows.Count; i++)
            {
                DataRow row = mappingDt.Rows[i];
                XmlElement element = doc.CreateElement("Column");
                if (row["defaultValue"].ToString() != "")
                {
                    element.SetAttribute("Value", row["defaultValue"].ToString());
                    element.SetAttribute("Target", row["targetName"].ToString());
                }
                else
                {
                    element.SetAttribute("Source", row["sourceName"].ToString());
                    element.SetAttribute("Target", row["targetName"].ToString());
                }
                parentElement.AppendChild(element);
            }
            doc.DocumentElement.AppendChild(parentElement);

            //配置源Schema
            string sourceSchema = getDataTableSchema(txtSDataBase.Value, sourceTable);
            //配置目标Schema
            string targetSchema = getDataTableSchema(txtTDataBase.Value, targetTable);

            //配置源SourceConfig
            XmlDocument docSourceConfig = new XmlDocument();
            docSourceConfig.LoadXml("<Source xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/MDT.ManageCenter.DataContract\"></Source>");
            XmlElement pe1 = docSourceConfig.CreateElement("MainTasks");
            XmlElement pe2 = docSourceConfig.CreateElement("PostTasks");
            XmlElement xe1SourceC = docSourceConfig.CreateElement("TaskUnit");
            XmlElement xe2SourceC = docSourceConfig.CreateElement("Commands");
            XmlElement xe3SourceC = docSourceConfig.CreateElement("HasTraceLog");
            XmlElement xe4SourceC = docSourceConfig.CreateElement("HasTransaction");
            XmlElement xe5SourceC = docSourceConfig.CreateElement("Id");
            XmlElement xe6SourceC = docSourceConfig.CreateElement("Results");
            xe1SourceC.SetAttribute("name", sourceEDatabase.Alias + "_" + targetEDatabase.Alias);
            xe5SourceC.InnerText = "1";
            xe1SourceC.AppendChild(xe2SourceC);
            xe1SourceC.AppendChild(xe3SourceC);
            xe1SourceC.AppendChild(xe4SourceC);
            xe1SourceC.AppendChild(xe5SourceC);
            xe1SourceC.AppendChild(xe6SourceC);
            pe1.AppendChild(xe1SourceC);
            docSourceConfig.DocumentElement.AppendChild(pe1);
            docSourceConfig.DocumentElement.AppendChild(pe2);

            //配置 目标SourceConfig
            XmlDocument docTargetConfig = new XmlDocument();
            docTargetConfig.LoadXml("<Source xmlns:i=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns=\"http://schemas.datacontract.org/2004/07/MDT.ManageCenter.DataContract\"></Source>");
            XmlElement pe1t = docTargetConfig.CreateElement("MainTasks");
            XmlElement pe2t = docTargetConfig.CreateElement("PostTasks");
            XmlElement xe1TargetC = docTargetConfig.CreateElement("TaskUnit");
            XmlElement xe2TargetC = docTargetConfig.CreateElement("Commands");
            XmlElement xe3TargetC = docTargetConfig.CreateElement("HasTraceLog");
            XmlElement xe4TargetC = docTargetConfig.CreateElement("HasTransaction");
            XmlElement xe5TargetC = docTargetConfig.CreateElement("Id");
            XmlElement xe6TargetC = docTargetConfig.CreateElement("Results"); //是否有返回值
            xe1TargetC.SetAttribute("name", sourceEDatabase.Alias + "_" + targetEDatabase.Alias);
            xe5TargetC.InnerText = "1";
            xe1TargetC.AppendChild(xe2TargetC);
            xe1TargetC.AppendChild(xe3TargetC);
            xe1TargetC.AppendChild(xe4TargetC);
            xe1TargetC.AppendChild(xe5TargetC);
            xe1TargetC.AppendChild(xe6TargetC);
            pe1t.AppendChild(xe1TargetC);
            docTargetConfig.DocumentElement.AppendChild(pe1t);
            docTargetConfig.DocumentElement.AppendChild(pe2t);

            //if (Context.User.Identity.Name.Split(new char[] { '|' })[0] != "MDT2.0")
            //{
            //    Response.Redirect("/Account/Login.aspx");
            //}
            //else
            //{
                //string userID = Context.User.Identity.Name.Split(new char[] { '|' })[1].ToString();
                //string dateTimeNow = DateTime.Now.ToString().Replace(" ", "").Replace(":", "").Replace("-", "").Replace("/","");
                //Random random = new Random();
                //int randomNum = random.Next(1, 9999);
                //string basePath = "/Management/UserFiles/" + userID + dateTimeNow + randomNum;
                //string folderPath = Server.MapPath(basePath);
                //if (!Directory.Exists(folderPath))
                //{
                //    Directory.CreateDirectory(folderPath);
                //}
                //else
                //{
                //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>目标路劲已经存在指定名称文件夹，新建文件夹失败！</script>", false);
                //    return;
                //}
                //Session.Add("basePath", basePath + "/");//把basePath保存在session中，以便在其他页面调用。
                //string basePath = ViewState["basePath"].ToString();
                //生成保存XSLTInfo
                string strPath2 = BasePath + "/XSLT.xml";
                strPath2 = Server.MapPath(strPath2);
                XslCompiledTransform tran = new XslCompiledTransform();
                tran.Load(AppDomain.CurrentDomain.BaseDirectory + "/MappingToXSLT.xslt");
                StringReader sr = new StringReader(doc.OuterXml);
                XmlReader xr = XmlReader.Create(sr);
                StringBuilder sb = new StringBuilder();
                XmlWriter xw = XmlWriter.Create(sb);
                tran.Transform(xr, xw);
                sr.Close();
                xr.Close();
                xw.Flush();
                xw.Close();
                XmlDocument xmlXSLT = new XmlDocument();
                xmlXSLT.LoadXml(sb.ToString());
                xmlXSLT.Save(strPath2);

                //配置Mapping.xml
                string strPath = BasePath + "/Mapping.xml";
                strPath = Server.MapPath(strPath);
                doc.Save(strPath);

                //配置源  Schema
                string strPath3 = BasePath + "/SourceSchema.xml";
                strPath3 = Server.MapPath(strPath3);
                XmlDocument docSourceSchema = new XmlDocument();
                docSourceSchema.LoadXml(sourceSchema);
                XmlNamespaceManager m2 = new XmlNamespaceManager(docSourceSchema.NameTable);
                m2.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
                XmlNode nlSSChema = docSourceSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m2);
                if (nlSSChema != null)
                {
                    XmlAttribute attribute = docSourceSchema.CreateAttribute("Id");
                    attribute.Value = "1";
                    nlSSChema.Attributes.Append(attribute);
                }
                docSourceSchema.Save(strPath3);

                //配置目标 Schema
                string strPath4 = BasePath + "/TargetSchema.xml";
                strPath4 = Server.MapPath(strPath4);
                XmlDocument docTargetSchema = new XmlDocument();
                docTargetSchema.LoadXml(targetSchema);
                XmlNode nlTSChema = docTargetSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m2);
                if (nlTSChema != null)
                {
                    XmlAttribute attribute = docTargetSchema.CreateAttribute("Id");
                    attribute.Value = "1";
                    nlTSChema.Attributes.Append(attribute);
                }
                docTargetSchema.Save(strPath4);

                //配置源SourceConfig
                string strPath7 = BasePath + "/SourceConfig.xml";
                strPath7 = Server.MapPath(strPath7);
                docSourceConfig.Save(strPath7);

                //配置目标SourceConfig
                strPath7 = BasePath + "/TargetConfig.xml";
                strPath7 = Server.MapPath(strPath7);
                docTargetConfig.Save(strPath7);
            //}
        }
        /// <summary>
        /// 保存源Schema 文件
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="table"></param>
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

        public string FormatDataType(string str)
        {
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

        protected void lbtnSD_Click(object sender, EventArgs e)
        {
            try
            {
                InitSTableName();
                BindSTTable();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('"+ex.Message.Replace("'","").Replace("\"","")+"');</script>", false);
            }
        }

        protected void lblTD_Click(object sender, EventArgs e)
        {
            try
            {
                InitTTableName();
                BindSTTable();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("'", "").Replace("\"", "") + "');</script>", false);
            }
        }

        protected void lblST_Click(object sender, EventArgs e)
        {
            try
            {
                BindSTTable();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("'", "").Replace("\"", "") + "');</script>", false);
            }
        }

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            InputTable();
            DataRow row = mappingDt.NewRow();
            row["sourceName"] = "";
            row["sourceType"] = "";
            row["targetName"] = "";
            row["targetType"] = "";
            row["defaultValue"] = "";
            mappingDt.Rows.Add(row);
            Repeater1.DataSource = mappingDt;
            Repeater1.DataBind();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>scrollBottom();</script>", false);
        }
        /// <summary>
        /// 将repeater1 中的  数据行填充到datatable中。
        /// </summary>
        public void InputTable()
        {
            mappingDt.Rows.Clear();
            if (txtSTable.Value.Trim().Length < 1 || txtTTable.Value.Trim().Length < 1)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请输入来源表名、目标表名！');</script>", false);
                return;
            }

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem ri = Repeater1.Items[i];
                DataRow dr = mappingDt.NewRow();
                dr["sourceName"] = ((TextBox)ri.FindControl("txtSourceCol")).Text.ToLower();
                dr["sourceType"] = ((TextBox)ri.FindControl("txtSourceType")).Text;
                dr["targetName"] = ((TextBox)ri.FindControl("txtTargetCol")).Text.ToLower();
                dr["targetType"] = ((TextBox)ri.FindControl("txtTargetType")).Text;
                dr["defaultValue"] = ((TextBox)ri.FindControl("txtDefault")).Text;
                mappingDt.Rows.Add(dr);
            }
        }
        /// <summary>
        /// 删除对应列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                mappingDt.Rows.Clear();//清空mapptingDt
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
                        dr["defaultValue"] = ((TextBox)ri.FindControl("txtDefault")).Text;
                        mappingDt.Rows.Add(dr);
                    }
                }
                Repeater1.DataSource = mappingDt;
                Repeater1.DataBind();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('删除成功!');</script>", false);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('删除失败！"+ex.Message.Replace("\"","").Replace("'","")+"');</script>", false);
                return;
            }
        }

    }
}