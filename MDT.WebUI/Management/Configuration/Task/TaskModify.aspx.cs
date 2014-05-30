using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Xml;
using System.Text;
using System.Drawing;
using System.Xml.Xsl;

using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;
using MDT.ManageCenter.ServiceImplement;

namespace MDT.WebUI.Management.Configuration.Task
{
    public partial class TaskModify : System.Web.UI.Page
    {
        string basePath = "";
        public string BasePath
        {
            get { return hideFileName.Value; }
            set { basePath = value; }
        }

        string strPath = "";
        XmlDocument xmlDoc = null;
        private List<EDatabase> dbList;
        private DbSchemaService dbSchema;
        XmlDocument xmlSSchema = null;
        XmlDocument xmlTSchema = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            EDatabaseDAL dbDAL = new EDatabaseDAL();
            dbList = dbDAL.GetDatabases();//查询数据库列表
            dbSchema = new DbSchemaService();
            if (Page.IsPostBack)
            {
                return;
            }
            //if (Session["basePath"] != null && Session["basePath"].ToString() != "")
            //{
            //    BindData();
            //    return;
            //}
            ViewState["id"] = 0;
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
            {
                string id = Request.QueryString["ID"].ToString();
                ViewState["id"] = id;
                BindDataFromDatabase();
                return;
            }

            if (Request.QueryString["fileName"] == null || Request.QueryString["fileName"].ToString() == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideModifyXML', 'iframeModifyXML');</script>");
                return;
            }
            else
            {
                hideFileName.Value = Request.QueryString["fileName"].ToString();
                BindData();
            }
            //ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideModifyXML', 'iframeModifyXML');</script>");
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            try
            {
                //basePath = Session["basePath"].ToString();
                strPath = Server.MapPath(BasePath + "Mapping.xml");
                xmlDoc = new XmlDocument();
                xmlDoc.Load(strPath);
                hideMapping.Value = xmlDoc.OuterXml;

                strPath = Server.MapPath(BasePath + "XSLT.xml");
                xmlDoc = new XmlDocument();
                xmlDoc.Load(strPath);
                hideXSLT.Value = xmlDoc.OuterXml;

                strPath = Server.MapPath(BasePath + "SourceConfig.xml");
                xmlDoc = new XmlDocument();
                xmlDoc.Load(strPath);
                hideSConfig.Value = xmlDoc.OuterXml;

                strPath = Server.MapPath(BasePath + "TargetConfig.xml");
                xmlDoc = new XmlDocument();
                xmlDoc.Load(strPath);
                hideTConfig.Value = xmlDoc.OuterXml;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('"+ex.Message.Replace("'","").Replace("\"","")+"');</script>");
                return;
            }
        }

        private void BindDataFromDatabase()
        {
            try
            {
                if (ViewState["id"].ToString() != "0")
                {
                    int id = Convert.ToInt32(ViewState["id"]);
                    ETaskDAL taskDAL = new ETaskDAL();
                    ETask etask = (from t in taskDAL.GetTasks()
                                   where t.ID == id
                                   select t).FirstOrDefault();
                    hideMapping.Value = etask.Mapping;
                    hideXSLT.Value = etask.XSLTInfo;
                    hideSConfig.Value = etask.SourceESchema.ESource.SourceConfig;
                    hideTConfig.Value = etask.TargetESchema.ESource.SourceConfig;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('"+ex.Message.Replace("'","").Replace("\"","")+"');</script>");
                return;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                //if (Session["basePath"] != null && Session["basePath"].ToString() != "")
                //{
                    //basePath = Session["basePath"].ToString();
                    
                //}
                if (BasePath != "")
                {
                    xmlDoc = new XmlDocument();
                    string xml = "";

                    strPath = Server.MapPath(BasePath + "Mapping.xml");//保存Mapping
                    xml = hideMapping.Value;
                    xmlDoc.LoadXml(xml);
                    xmlDoc.Save(strPath);//保存到指定路径 

                    xml = xml.Replace("\r", "").Replace("\n", "");
                    GetESchema(xml); //获取SourceSchema和TargetSchema
                    strPath = Server.MapPath(BasePath + "SourceSchema.xml");
                    xmlSSchema.Save(strPath);  //保存SourceSchema
                    strPath = Server.MapPath(BasePath + "TargetSchema.xml");
                    xmlTSchema.Save(strPath);  //保存TargetSchema

                    strPath = Server.MapPath(BasePath + "XSLT.xml");//保存XSLTInfo
                    xml = hideXSLT.Value;
                    xmlDoc.LoadXml(xml);
                    xmlDoc.Save(strPath);//保存到指定路径

                    strPath = Server.MapPath(BasePath + "SourceConfig.xml");//保存SourceConfig  
                    xml = hideSConfig.Value;
                    xmlDoc.LoadXml(xml);
                    xmlDoc.Save(strPath);//保存到指定路径

                    strPath = Server.MapPath(BasePath + "TargetConfig.xml");//保存TargetConfig
                    xml = hideTConfig.Value;
                    xmlDoc.LoadXml(xml);
                    xmlDoc.Save(strPath);//保存到指定路径

                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>openerRefresh('add');alert('保存成功！');</script>");
                }
                else if (ViewState["id"] != null && ViewState["id"].ToString() != "" && ViewState["id"].ToString() != "0")
                {
                    int id = Convert.ToInt32(ViewState["id"]);
                    xmlDoc = new XmlDocument();
                    GetESchema(hideMapping.Value); //获取SourceSchema和TargetSchema
                    ETaskDAL taskDAL = new ETaskDAL();
                    ETask task = taskDAL.GetTask(id);
                    task.Mapping = hideMapping.Value;  //保存Mapping
                    task.XSLTInfo = hideXSLT.Value;   //保存XSLTInfo
                    if (xmlSSchema != null && xmlTSchema != null)
                    {
                        task.SourceESchema.Schema = xmlSSchema.OuterXml; //保存源SourceSchema
                        task.TargetESchema.Schema = xmlTSchema.OuterXml; //保存目标SourceSchema
                    }
                    task.SourceESchema.ESource.SourceConfig = hideSConfig.Value;  //保存源SourceConfig
                    task.TargetESchema.ESource.SourceConfig = hideTConfig.Value;  //保存目标SourceConfig
                    taskDAL.ModifyTask(task);  //向数据库更新任务信息
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('保存成功！');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('保存失败！没有找到basePath并且传入id值为空');</script>");
                    return;
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('保存失败！"+ex.Message.Replace("'","").Replace("\"","")+"');</script>");
                return;
            }
        }

        private MDT.ManageCenter.DataContract.Table sourceTable;
        private MDT.ManageCenter.DataContract.Table targetTable;
        /// <summary>
        /// 获取sourceESchema 和 targetESchema
        /// </summary>
        /// <param name="xml"></param>
        public void GetESchema(string xml)
        {
            XmlDocument xmlSSchema2 = null;
            XmlDocument xmlTSchema2 = null;
            XmlNamespaceManager m = null;

            xmlDoc.LoadXml(xml);
            XmlNodeList nl = xmlDoc.GetElementsByTagName("Table");
            foreach (XmlNode item in nl)
            {
                if (item.Attributes["SourceDb"] == null || item.Attributes["TargetDb"] == null || item.Attributes["Id"] == null)
                {
                    return;   //如果配置文件中不包含 SourceDb、TargetDb、Id的属性，则为程序更新前的项目，不需执行以下操作
                }
                sourceTable = new ManageCenter.DataContract.Table
                {
                    TableName = item.Attributes["Source"].Value,
                    Columns = new List<EColumn>(),
                    RelatedTables = new List<RelatedTable>(),
                };
                buildColumnInfo(item.Attributes["SourceDb"].Value, sourceTable, "Source", item);
                // 添加目标表
                targetTable = new ManageCenter.DataContract.Table
                {
                    TableName = item.Attributes["Target"].Value,
                    Columns = new List<EColumn>(),
                    RelatedTables = new List<RelatedTable>(),
                };
                buildColumnInfo(item.Attributes["TargetDb"].Value, targetTable, "Target", item);
                //配置源Schema
                string sourceSchema = getDataTableSchema(item.Attributes["SourceDb"].Value, sourceTable);
                sourceSchema = sourceSchema.Replace("\r", "").Replace("\n","");
                if (xmlSSchema == null)
                {
                    xmlSSchema = new XmlDocument();
                    xmlSSchema.LoadXml(sourceSchema);
                    m = new XmlNamespaceManager(xmlSSchema.NameTable);
                    m.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
                    XmlNode nlSSChema = xmlSSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m);
                    if (nlSSChema != null)
                    {
                        XmlAttribute attribute = xmlSSchema.CreateAttribute("Id");
                        attribute.Value = item.Attributes["Id"].Value;
                        nlSSChema.Attributes.Append(attribute);
                    }
                }
                else
                {
                    xmlSSchema2 = new XmlDocument();
                    xmlSSchema2.LoadXml(sourceSchema);
                    XmlNode nlStr = xmlSSchema2.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m);
                    XmlNode nlChoice = xmlSSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice", m);
                    if (nlChoice != null && nlStr != null)
                    {
                        XmlAttribute a1 = xmlSSchema2.CreateAttribute("Id");
                        a1.Value = item.Attributes["Id"].Value;
                        nlStr.Attributes.Append(a1);
                        XmlNode x2 = xmlSSchema.ImportNode(nlStr, true);
                        nlChoice.AppendChild(x2);
                    }
                }
                //配置目标Schema
                string targetSchema = getDataTableSchema(item.Attributes["TargetDb"].Value, targetTable);
                if (xmlTSchema == null)
                {
                    xmlTSchema = new XmlDocument();
                    xmlTSchema.LoadXml(targetSchema);
                    m = new XmlNamespaceManager(xmlTSchema.NameTable);
                    m.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
                    XmlNode nlTSChema = xmlTSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m);
                    if (nlTSChema != null)
                    {
                        XmlAttribute attribute = xmlTSchema.CreateAttribute("Id");
                        attribute.Value = item.Attributes["Id"].Value;
                        nlTSChema.Attributes.Append(attribute);
                    }
                }
                else
                {
                    xmlTSchema2 = new XmlDocument();
                    xmlTSchema2.LoadXml(targetSchema);
                    XmlNode nlStr2 = xmlTSchema2.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element", m);
                    XmlNode nlChoice2 = xmlTSchema.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice", m);
                    if (nlChoice2 != null && nlStr2 != null)
                    {
                        XmlAttribute a1 = xmlTSchema2.CreateAttribute("Id");
                        a1.Value = item.Attributes["Id"].Value;
                        nlStr2.Attributes.Append(a1);
                        XmlNode x2 = xmlTSchema.ImportNode(nlStr2, true);
                        nlChoice2.AppendChild(x2);
                    }
                }
            }
        }
        private void buildColumnInfo(string databaseName, MDT.ManageCenter.DataContract.Table table, string columnName,XmlNode nl)
        {
            string sql = "select * from " + table.TableName + " where 1=0";
            EDatabase database = findEDatabase(databaseName);
            DataTable schemaDt = dbSchema.GetDataTableSchema((SourceType)Enum.Parse(typeof(SourceType), database.DatabaseType)
                                                             , database.Server, database.Port, database.Database, database.UserId, database.Password
                                                             , sql, table.TableName);
            XmlNodeList nlChild = nl.ChildNodes;
            foreach (XmlNode item in nlChild)
            {
                if (item.Attributes[columnName] != null)
                {
                    table.Columns.Add(new EColumn
                    {
                        Name = item.Attributes[columnName].Value,
                        Type = schemaDt.Columns[item.Attributes[columnName].Value].DataType.ToString()
                    });
                }
            }
        }
        /// <summary>
        /// 查找DataBase对象
        /// </summary>
        /// <param name="alias"></param>
        /// <returns></returns>
        private EDatabase findEDatabase(string alias)
        {
            foreach (EDatabase db in dbList)
            {
                if (db.Alias == alias) { return db; }
            }
            return null;
        }
        /// <summary>
        /// 获取Schema 文件
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
    }
}