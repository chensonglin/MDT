using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Data;
using System.IO;

using MDT.ManageCenter.DataContract;
using MDT.ManageCenter.ServiceImplement;
using MDT.ManageCenter.DAL;
using System.Xml.Xsl;
using System.Text;


namespace MDT.WebUI.Management.Configuration.Task
{
    public partial class TaskAdd : System.Web.UI.Page
    {
        string basePath = "";
        public string BasePath
        {
            get { return "/Management/UserFiles/" + hideFileName.Value + "/"; }
            set { basePath = value; }
        }
        string strPath = "";
        XmlDocument xmlDoc = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }
            if (Request.QueryString["fileName"] == null || Request.QueryString["fileName"].ToString() == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');window.location.href='TaskBrowser.aspx';</script>");
                return;
            }
            else
            {
                hideFileName.Value = Request.QueryString["fileName"].ToString();
                //将路劲保存在母版页中的hideMasterFileName文本框中。以便离开任务配置页面时删除临时文件
                ((TextBox)this.Master.FindControl("hideMasterFileName")).Text = hideFileName.Value;
            }
            ViewState["id"] = 0;
            if (Request.QueryString["Id"] != null && Request.QueryString["Id"] != "")//修改任务
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                ViewState["id"] = id;
                ETaskDAL taskDAL = new ETaskDAL();
                ETask task = taskDAL.GetTask(id);
                ViewState["ids"] = task.SourceESchema.ID + "|" + task.TargetESchema.ID + "|" + task.SourceESchema.ESource.ID + "|" + task.TargetESchema.ESource.ID;
                txtTaskName.Text = task.TaskName;
                txtNode.Text = task.Note;
                txtInterval.Text = task.Interval.ToString();

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(task.Mapping);
                strPath = Server.MapPath(BasePath + "Mapping.xml");
                xmlDoc.Save(strPath);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(task.XSLTInfo);
                strPath = Server.MapPath(BasePath + "XSLT.xml");
                xmlDoc.Save(strPath);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(task.SourceESchema.Schema);
                strPath = Server.MapPath(BasePath + "SourceSchema.xml");
                xmlDoc.Save(strPath);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(task.TargetESchema.Schema);
                strPath = Server.MapPath(BasePath + "TargetSchema.xml");
                xmlDoc.Save(strPath);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(task.SourceESchema.ESource.SourceConfig);
                strPath = Server.MapPath(BasePath + "SourceConfig.xml");
                xmlDoc.Save(strPath);

                xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(task.TargetESchema.ESource.SourceConfig);
                strPath = Server.MapPath(BasePath + "TargetConfig.xml");
                xmlDoc.Save(strPath);
            }
            BindData();
        }

        private void BindData()
        {
            DataTable dtMDT = new DataTable();
            DataColumn dc1 = new DataColumn("SourceDb");
            DataColumn dc2 = new DataColumn("SourceTable");
            DataColumn dc3 = new DataColumn("TargetDb");
            DataColumn dc4 = new DataColumn("TargetTable");
            DataColumn dc5 = new DataColumn("Id");
            try
            {
                dtMDT.Columns.Add(dc1);
                dtMDT.Columns.Add(dc2);
                dtMDT.Columns.Add(dc3);
                dtMDT.Columns.Add(dc4);
                dtMDT.Columns.Add(dc5);

                strPath = Server.MapPath(BasePath + "Mapping.xml");
                if (!File.Exists(strPath))
                {
                    Repeater1.DataSource = dtMDT;
                    Repeater1.DataBind();
                    return;
                }
                XmlDocument docMDT = new XmlDocument();
                docMDT.Load(strPath);
                XmlNodeList nl = docMDT.GetElementsByTagName("Table");
                foreach (XmlNode item in nl)
                {
                    DataRow dr = dtMDT.NewRow();
                    dr["SourceDb"] = item.Attributes["SourceDb"].Value;
                    dr["TargetDb"] = item.Attributes["TargetDb"].Value;
                    dr["SourceTable"] = item.Attributes["Source"].Value;
                    dr["TargetTable"] = item.Attributes["Target"].Value;
                    dr["Id"] = item.Attributes["Id"].Value;
                    dtMDT.Rows.Add(dr);
                }

                ////绑定源Sourceconfig 部分
                //List<TaskUnit> listTaskUnitS = new List<TaskUnit>();
                //Dictionary<string, TaskUnit> dicTaskUnitS = new Dictionary<string, TaskUnit>();
                //strPath = Server.MapPath(BasePath + "SourceConfig.xml");
                //GetView(strPath, listTaskUnitS, dicTaskUnitS);
                //ViewState["dicTaskUnitS"] = dicTaskUnitS;
                //Repeater2.DataSource = listTaskUnitS;
                //Repeater2.DataBind();
                ////绑定目标SourceConfig部分
                //List<TaskUnit> listTaskUnitT = new List<TaskUnit>();
                //Dictionary<string, TaskUnit> dicTaskUnitT = new Dictionary<string, TaskUnit>();
                //strPath = Server.MapPath(BasePath + "TargetConfig.xml");
                //GetView(strPath, listTaskUnitT, dicTaskUnitT);
                //ViewState["dicTaskUnitT"] = dicTaskUnitT;
                //Repeater4.DataSource = listTaskUnitT;
                //Repeater4.DataBind();
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('加载数据时出现异常！请以 修改xml数据 方式修改此任务。');window.location.href='TaskBrowser.aspx';</script>");
                return;
            }
            Repeater1.DataSource = dtMDT;
            Repeater1.DataBind();
        }

        public void GetView(string path, List<TaskUnit> listTaskUnit, Dictionary<string, TaskUnit> dicTaskUnit)
        {
            //xmlDoc = new XmlDocument();
            //xmlDoc.Load(path);
            //XmlNodeList nlSC = xmlDoc.SelectNodes("//MainTasks/TaskUnit");
            //foreach (XmlNode item in nlSC)
            //{
            //    TaskUnit tu = new TaskUnit();
            //    tu.name = item.Attributes["name"].Value;
            //    tu.Id = Convert.ToInt32(item["Id"].InnerText);
            //    string hasLog = item["HasTraceLog"].InnerText;
            //    if (hasLog == "")
            //    {
            //        hasLog = "false";
            //    }
            //    string hasTran = item["HasTransaction"].InnerText;
            //    if (hasTran == "")
            //    {
            //        hasTran = "false";
            //    }
            //    tu.HasTraceLog = Convert.ToBoolean(hasLog);
            //    tu.HasTransaction = Convert.ToBoolean(hasTran);
            //    //执行命令
            //    tu.Commands = new List<ECommand>();
            //    XmlNodeList nlP = item["Commands"].ChildNodes;
            //    foreach (XmlNode node in nlP)
            //    {
            //        ECommand ec = new ECommand();
            //        ec.CommandName = node["CommandName"].InnerText;
            //        ec.Id = node.Attributes["Id"].Value;
            //        tu.Commands.Add(ec);
            //    }
            //    //子任务
            //    XmlNodeList nlPostTask = xmlDoc.SelectNodes("//PostTasks/TaskUnit[Id=" + tu.Id + "]/Commands/ECommand");
            //    if (nlPostTask.Count > 0)
            //    {
            //        tu.PostTaskCommands = new List<ECommand>();
            //        foreach (XmlNode node2 in nlPostTask)
            //        {
            //            ECommand ecP = new ECommand();
            //            ecP.CommandName = node2["CommandName"].InnerText;
            //            ecP.Id = node2.Attributes["Id"].Value;
            //            tu.PostTaskCommands.Add(ecP);
            //        }
            //    }
            //    //结果集
            //    tu.Results = new List<Result>();
            //    XmlNodeList nlResults = item["Results"].ChildNodes;
            //    foreach (XmlNode node3 in nlResults)
            //    {
            //        Result result = new Result();
            //        result.CommandName = node3["CommandName"].InnerText;
            //        result.XmlPath = node3["XmlPath"].InnerText;
            //        result.Id = node3.Attributes["Id"].Value;
            //        tu.Results.Add(result);
            //    }
            //    listTaskUnit.Add(tu);
            //    dicTaskUnit.Add(tu.Id.ToString(), tu);
            //}
        }

        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                if (ViewState["dicTaskUnitS"] != null)
                {
                    //Dictionary<string, TaskUnit> dic = (Dictionary<string, TaskUnit>)ViewState["dicTaskUnitS"];
                    //Label lblId = (Label)e.Item.FindControl("lblId");
                    //string id = lblId.Text;
                    //Repeater repeater3 = (Repeater)e.Item.FindControl("Repeater3");
                    //repeater3.DataSource = dic[id].Commands;
                    //repeater3.DataBind();
                    //Repeater repeater6 = (Repeater)e.Item.FindControl("Repeater6");
                    //repeater6.DataSource = dic[id].PostTaskCommands;
                    //repeater6.DataBind();
                    //Repeater repeater7 = (Repeater)e.Item.FindControl("Repeater7");
                    //repeater7.DataSource = dic[id].Results;
                    //repeater7.DataBind();
                }
            }
        }
        protected void Repeater4_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            //if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            //{
            //    if (ViewState["dicTaskUnitT"] != null)
            //    {
            //        Dictionary<string, TaskUnit> dic = (Dictionary<string, TaskUnit>)ViewState["dicTaskUnitT"];
            //        Label lblId = (Label)e.Item.FindControl("lblId");
            //        string id = lblId.Text;
            //        Repeater repeater5 = (Repeater)e.Item.FindControl("Repeater5");
            //        repeater5.DataSource = dic[id].Commands;
            //        repeater5.DataBind();
            //        Repeater repeater8 = (Repeater)e.Item.FindControl("Repeater8");
            //        repeater8.DataSource = dic[id].PostTaskCommands;
            //        repeater8.DataBind();
            //        Repeater repeater9 = (Repeater)e.Item.FindControl("Repeater9");
            //        repeater9.DataSource = dic[id].Results;
            //        repeater9.DataBind();
            //    }
            //}
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
                ModifyXML();
                ETaskDAL taskDAL = new ETaskDAL();
                ETask task = getETask();
                int id = Convert.ToInt32(ViewState["id"]);
                var tasks = taskDAL.GetTasks(task.TaskName, id);//根据任务名称和ID获取任务
                ETask[] array = tasks.ToArray();
                if (array.Length > 0)//判断该任务名称是否已经存在
                {
                    txtTaskName.Focus();
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('任务名称已经存在，请重新输入！');</script>");
                    return;
                }
                if (id == 0)//新增任务
                {
                    //task.Created = DateTime.Now;
                    //task.Modified = DateTime.Now;
                    task.OperationDate = DateTime.Now;
                    ETask task1 = taskDAL.AddObject(task);
                }
                else//修改任务
                {
                    string idsStr = ViewState["ids"].ToString();
                    string[] idsArray = idsStr.Split('|');
                    task.ID = id;
                    task.SourceESchema.ID = Convert.ToInt32(idsArray[0]);
                    task.TargetESchema.ID = Convert.ToInt32(idsArray[1]);
                    task.SourceESchema.ESource.ID = Convert.ToInt32(idsArray[2]);
                    task.TargetESchema.ESource.ID = Convert.ToInt32(idsArray[3]);
                    //task.Modified = DateTime.Now;
                    task.OperationDate = DateTime.Now;
                    taskDAL.ModifyTask(task);//修改任务
                }

                strPath = BasePath.TrimEnd('/');
                strPath = Server.MapPath(strPath);
                Directory.Delete(strPath, true);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('保存失败" + ex.Message + "');</script>");
                return;
            }
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('保存成功！');window.location.href='TaskBrowser.aspx';</script>");
        }
        private ETask getETask()
        {
            int userID = Convert.ToInt32(Context.User.Identity.Name.Split(new char[] { '|' })[1]);

            strPath = Server.MapPath(BasePath + "Mapping.xml");
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            string mapping = xmlDoc.OuterXml;

            strPath = Server.MapPath(BasePath + "XSLT.xml");
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            XmlNamespaceManager m = new XmlNamespaceManager(xmlDoc.NameTable);
            m.AddNamespace("xsl", "http://www.w3.org/1999/XSL/Transform");
            XmlNode nl = xmlDoc.SelectSingleNode("/xsl:stylesheet", m);
            string xslt = nl.OuterXml;

            strPath = Server.MapPath(BasePath + "SourceSchema.xml");
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            m = new XmlNamespaceManager(xmlDoc.NameTable);
            m.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
            nl = xmlDoc.SelectSingleNode("/xs:schema", m);
            string sourceSchema = nl.OuterXml;

            strPath = Server.MapPath(BasePath + "TargetSchema.xml");
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            nl = xmlDoc.SelectSingleNode("/xs:schema", m);
            string targetSchema = nl.OuterXml;

            strPath = Server.MapPath(BasePath + "SourceConfig.xml");
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            string sourceConfig = xmlDoc.OuterXml;

            strPath = Server.MapPath(BasePath + "TargetConfig.xml");
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            string targetConfig = xmlDoc.OuterXml;

            ETask t = new ETask()
            {
                SourceESchema = new ESchema()
                {
                    //Customer_ID = 1,
                    ESource = new ESource()
                    {
                        ESourceBaseInfo_ID = 0,
                        //Customer_ID = userID,
                        SourceConfig = sourceConfig,  //SourceConfig 在这里添加进来
                        SourceType = "",
                        OriginalConfig = ""
                    },
                    Schema = sourceSchema
                },
                TargetESchema = new ESchema()
                {
                    //Customer_ID = 1,
                    ESource = new ESource()
                    {
                        ESourceBaseInfo_ID = 0,
                        //Customer_ID = 1,
                        SourceConfig = targetConfig,  //SourceConfig 在这里添加进来
                        SourceType = "",
                        OriginalConfig = ""
                    },
                    Schema = targetSchema
                },
                TaskName = txtTaskName.Text,
                XSLTInfo = xslt,
                Mapping = mapping,
                Enable = true,
                Note = txtNode.Text,
                Interval = Convert.ToInt32(txtInterval.Text),
                OperationID = userID
            };
            return t;
        }
        private void ModifyXML()
        {

            strPath = Server.MapPath(BasePath + "SourceConfig.xml");
            if (!File.Exists(strPath))
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('找不到文件，请验证配置信息是否完整！');</script>");
                return;
            }
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                Label lblId = (Label)Repeater2.Items[i].FindControl("lblId");
                TextBox txtSLog = (TextBox)Repeater2.Items[i].FindControl("txtSLog");
                TextBox txtSTran = (TextBox)Repeater2.Items[i].FindControl("txtSTran");
                XmlNode nls1 = xmlDoc.SelectSingleNode("//MainTasks/TaskUnit[Id=" + lblId.Text + "]");
                if (nls1 != null)
                {
                    nls1["HasTraceLog"].InnerText = txtSLog.Text;
                    nls1["HasTransaction"].InnerText = txtSTran.Text;
                }
                XmlNode nls2 = xmlDoc.SelectSingleNode("//PostTasks/TaskUnit[Id=" + lblId.Text + "]");
                if (nls2 != null)
                {
                    nls2["HasTraceLog"].InnerText = txtSLog.Text;
                    nls2["HasTransaction"].InnerText = txtSTran.Text;
                }
            }
            xmlDoc.Save(strPath);

            strPath = Server.MapPath(BasePath + "TargetConfig.xml");
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            for (int i = 0; i < Repeater4.Items.Count; i++)
            {
                Label lblId = (Label)Repeater4.Items[i].FindControl("lblId");
                TextBox txtTLog = (TextBox)Repeater4.Items[i].FindControl("txtTLog");
                TextBox txtTTran = (TextBox)Repeater4.Items[i].FindControl("txtTTran");
                XmlNode nlt1 = xmlDoc.SelectSingleNode("//MainTasks/TaskUnit[Id=" + lblId.Text + "]");
                if (nlt1 != null)
                {
                    nlt1["HasTraceLog"].InnerText = txtTLog.Text;
                    nlt1["HasTransaction"].InnerText = txtTTran.Text;
                }
                XmlNode nlt2 = xmlDoc.SelectSingleNode("//PostTasks/TaskUnit[Id=" + lblId.Text + "]");
                if (nlt2 != null)
                {
                    nlt2["HasTraceLog"].InnerText = txtTLog.Text;
                    nlt2["HasTransaction"].InnerText = txtTTran.Text;
                }
            }
            xmlDoc.Save(strPath);
        }
        /// <summary>
        /// 删除TaskUnit  
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Label lblId = (Label)e.Item.FindControl("lblTId");
            if (lblId == null || lblId.Text == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('未获取到该行数据id！');</script>");
                return;
            }
            string strPath1 = Server.MapPath(BasePath + "Mapping.xml");
            XmlDocument xmlDoc1 = new XmlDocument();
            xmlDoc1.Load(strPath1);
            XmlNode nl1 = xmlDoc1.SelectSingleNode("/Tables/Table[@Id=" + lblId.Text + "]");
            if (nl1 == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('在Mapping.xml文件中未获取到相应节点，删除失败！');</script>");
                return;
            }
            string strPath2 = Server.MapPath(BasePath + "SourceConfig.xml");
            XmlDocument xmlDoc2 = new XmlDocument();
            xmlDoc2.Load(strPath2);
            XmlNode nl2 = xmlDoc2.SelectSingleNode("//MainTasks/TaskUnit[Id=" + lblId.Text + "]");
            if (nl2 == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('在SourceConfig.xml文件中未获取到相应节点，删除失败！');</script>");
                return;
            }
            XmlNode nl21 = xmlDoc2.SelectSingleNode("//PostTasks/TaskUnit[Id=" + lblId.Text + "]");//删除TaskUnit对应的子任务

            string strPath3 = Server.MapPath(BasePath + "TargetConfig.xml");
            XmlDocument xmlDoc3 = new XmlDocument();
            xmlDoc3.Load(strPath3);
            XmlNode nl3 = xmlDoc3.SelectSingleNode("//MainTasks/TaskUnit[Id=" + lblId.Text + "]");
            if (nl3 == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('在TargetConfig.xml文件中未获取到相应节点，删除失败！');</script>");
                return;
            }
            XmlNode nl31 = xmlDoc3.SelectSingleNode("//PostTasks/TaskUnit[Id=" + lblId.Text + "]");//删除TaskUnit对应的子任务

            string strPath4 = Server.MapPath(BasePath + "SourceSchema.xml");
            XmlDocument xmlDoc4 = new XmlDocument();
            xmlDoc4.Load(strPath4);
            XmlNamespaceManager m = new XmlNamespaceManager(xmlDoc4.NameTable);
            m.AddNamespace("xs", "http://www.w3.org/2001/XMLSchema");
            XmlNode nl4 = xmlDoc4.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element[@Id=" + lblId.Text + "]", m);
            if (nl4 == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('在SourceSchema.xml文件中未获取到相应节点，删除失败！');</script>");
                return;
            }
            string strPath5 = Server.MapPath(BasePath + "TargetSchema.xml");
            XmlDocument xmlDoc5 = new XmlDocument();
            xmlDoc5.Load(strPath5);
            XmlNode nl5 = xmlDoc5.SelectSingleNode("/xs:schema/xs:element/xs:complexType/xs:choice/xs:element[@Id=" + lblId.Text + "]", m);
            if (nl5 == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('在TargetSchema.xml文件中未获取到相应节点，删除失败！');</script>");
                return;
            }
            nl1.ParentNode.RemoveChild(nl1);

            //更新XSLT 
            string strPath6 = Server.MapPath(BasePath + "XSLT.xml");
            XslCompiledTransform tran2 = new XslCompiledTransform();
            tran2.Load(AppDomain.CurrentDomain.BaseDirectory + "/MappingToXSLT.xslt");

            StringBuilder sb2 = new StringBuilder();
            XmlWriterSettings setting = new XmlWriterSettings();
            setting.Encoding = new UTF8Encoding(false);
            setting.Indent = true;
            System.IO.StreamWriter xmlSR2 = new System.IO.StreamWriter(strPath6, false, Encoding.UTF8);
            xmlSR2.Write(sb2.ToString());
            XmlWriter xw2 = XmlWriter.Create(xmlSR2);
            tran2.Transform(xmlDoc1, xw2);
            xw2.Flush();
            xw2.Close();
            xmlSR2.Close();
            xmlSR2.Close();

            nl2.ParentNode.RemoveChild(nl2);
            if (nl21 != null)
            {
                nl21.ParentNode.RemoveChild(nl21);
            }
            nl3.ParentNode.RemoveChild(nl3);
            if (nl31 != null)
            {
                nl31.ParentNode.RemoveChild(nl31);
            }
            nl4.ParentNode.RemoveChild(nl4);
            nl5.ParentNode.RemoveChild(nl5);

            xmlDoc1.Save(strPath1);
            xmlDoc2.Save(strPath2);
            xmlDoc3.Save(strPath3);
            xmlDoc4.Save(strPath4);
            xmlDoc5.Save(strPath5);

            BindData();
        }

        protected void Repeater3_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            RepeaterItem item = (RepeaterItem)e.Item.NamingContainer.NamingContainer;
            Label lblId = (Label)item.FindControl("lblId");
            Label lblId2 = (Label)e.Item.FindControl("lblId2");
            if (e.CommandName == "Modify")
            {
                string url = "SourceConfig.aspx?tid=" + lblId.Text + "&cid=" + lblId2.Text + "&Type=s&fileName=" + BasePath;
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>SourceConfigTitle('配置命令');show('hideCommand','iframeCommand','" + url + "');</script>");
            }
            else
            {
                string selectStr = "//MainTasks/TaskUnit[Id=" + lblId.Text + "]/Commands/ECommand[@Id=" + lblId2.Text + "]";
                Delete("SourceConfig.xml", selectStr);
            }
        }
        protected void Repeater6_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            RepeaterItem item = (RepeaterItem)e.Item.NamingContainer.NamingContainer;
            Label lblId = (Label)item.FindControl("lblId");
            Label lblId2 = (Label)e.Item.FindControl("lblId2");
            if (e.CommandName == "Modify")
            {
                string url = "SourceConfig.aspx?tid=" + lblId.Text + "&cid=" + lblId2.Text + "&Type=sp&fileName=" + BasePath;
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>SourceConfigTitle('配置事后任务');show('hideCommand','iframeCommand','" + url + "');</script>");
            }
            else
            {
                string selectStr = "//PostTasks/TaskUnit[Id=" + lblId.Text + "]/Commands/ECommand[@Id=" + lblId2.Text + "]";
                Delete("SourceConfig.xml", selectStr);
            }
        }
        private void Delete(string xmlName, string selectStr)
        {

            string strPath = Server.MapPath(BasePath + xmlName);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            XmlNode nl = xmlDoc.SelectSingleNode(selectStr);
            if (nl == null)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('在" + xmlName + "中没有找到相应节点，删除失败！');</script>");
                return;
            }
            nl.ParentNode.RemoveChild(nl);
            xmlDoc.Save(strPath);
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('删除成功！');</script>");
            BindData();
        }
        protected void Repeater7_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            RepeaterItem item = (RepeaterItem)e.Item.NamingContainer.NamingContainer;
            Label lblId = (Label)item.FindControl("lblId");
            Label lblResultId = (Label)e.Item.FindControl("lblResultId");
            if (e.CommandName == "Modify")
            {
                string url = "ResultEdit.aspx?Id=" + lblId.Text + "&cid=" + lblResultId.Text + "&Type=s&fileName=" + BasePath;
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>show('hideResults','iframeResults','" + url + "');</script>");
            }
            else
            {
                string selectStr = "//MainTasks/TaskUnit[Id=" + lblId.Text + "]/Results/Result[@Id=" + lblResultId.Text + "]";
                Delete("SourceConfig.xml", selectStr);
            }
        }
        protected void Repeater5_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            RepeaterItem item = (RepeaterItem)e.Item.NamingContainer.NamingContainer;
            Label lblId = (Label)item.FindControl("lblId");
            Label lblId2 = (Label)e.Item.FindControl("lblId2");
            if (e.CommandName == "Modify")
            {
                string url = "SourceConfig.aspx?tid=" + lblId.Text + "&cid=" + lblId2.Text + "&Type=t&fileName=" + BasePath;
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>SourceConfigTitle('配置命令');show('hideCommand','iframeCommand','" + url + "');</script>");
            }
            else
            {
                string selectStr = "//MainTasks/TaskUnit[Id=" + lblId.Text + "]/Commands/ECommand[@Id=" + lblId2.Text + "]";
                Delete("TargetConfig.xml", selectStr);
            }
        }
        protected void Repeater8_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            RepeaterItem item = (RepeaterItem)e.Item.NamingContainer.NamingContainer;
            Label lblId = (Label)item.FindControl("lblId");
            Label lblId2 = (Label)e.Item.FindControl("lblId2");
            if (e.CommandName == "Modify")
            {
                string url = "SourceConfig.aspx?tid=" + lblId.Text + "&cid=" + lblId2.Text + "&Type=tp&fileName=" + BasePath;
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>SourceConfigTitle('配置事后任务');show('hideCommand','iframeCommand','" + url + "');</script>");
            }
            else
            {
                string selectStr = "//PostTasks/TaskUnit[Id=" + lblId.Text + "]/Commands/ECommand[@Id=" + lblId2.Text + "]";
                Delete("TargetConfig.xml", selectStr);
            }
        }
        protected void Repeater9_ItemCommand(object sender, RepeaterCommandEventArgs e)
        {
            RepeaterItem item = (RepeaterItem)e.Item.NamingContainer.NamingContainer;
            Label lblId = (Label)item.FindControl("lblId");
            if (e.CommandName == "Modify")
            {
                string url = "ResultEdit.aspx?Id=" + lblId.Text + "&cid=1&Type=t&fileName=" + BasePath;
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>show('hideResults','iframeResults','" + url + "');</script>");
            }
            else
            {
                string selectStr = "//MainTasks/TaskUnit[Id=" + lblId.Text + "]/Results/Result";
                Delete("TargetConfig.xml", selectStr);
            }
        }
        /// <summary>
        /// 刷新按钮事件，当保存Mapping、Command、Result成功时用js调用此事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            BindData();
        }
        // <summary>
        /// 功能描述：判断字节数，返回固定字节长度的字符串
        /// </summary>
        /// <param name="cOriginalityString">传入字符串</param>
        /// <param name="iLenReturnString">返回的字符串字节长度</param>
        /// <param name="bReturnDefaultString">是否在结尾增加“...”</param>
        /// <returns>截取后的字符串</returns>
        public string GetStringPartContent(string cOriginalityString, int iLenReturnString, bool bReturnDefaultString)
        {
            string cReturnString = "";
            string cReturnDefaultString = "...";		//缺省字符串的内容
            //cOriginalityString = cReturnString;
            cReturnString = cOriginalityString;

            if (cReturnString.Length > iLenReturnString)
            {
                cReturnString = cReturnString.Substring(0, iLenReturnString);
            }

            int ilength = iLenReturnString;		//此方法不区分汉字，一个汉字只算1
            if (cReturnString.Length < iLenReturnString)
            {
                ilength = cReturnString.Length;
            }
            while (true)
            {
                int ilent = System.Text.ASCIIEncoding.Default.GetByteCount(cReturnString);	//此方法区分汉字，一个汉字算2
                if (ilent > iLenReturnString)
                {
                    ilength--;
                    cReturnString = cReturnString.Substring(0, ilength);
                }
                else
                {
                    break;
                }
            }
            if (bReturnDefaultString == true && cReturnString != cOriginalityString)
            {
                //返回的字符串与原始内容不同，且要求含有缺省字符串的内容。
                cReturnString += cReturnDefaultString;
            }
            return cReturnString;
        }

        protected void lblClose_Click(object sender, EventArgs e)
        {
            try
            {
                string strPath = BasePath.TrimEnd('/');
                strPath = Server.MapPath(strPath);
                if (Directory.Exists(strPath))
                {
                    Directory.Delete(strPath, true);
                }
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('删除临时文件时出现异常');</script>");
                return;
            }
        }
        protected void LbtnClose2_Click(object sender, EventArgs e)
        {
            try
            {
                string strPath = BasePath.TrimEnd('/');
                strPath = Server.MapPath(strPath);
                if (Directory.Exists(strPath))
                {
                    Directory.Delete(strPath, true);
                }
                Response.Redirect("TaskBrowser.aspx");
            }
            catch (Exception)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('删除临时文件时出现异常');</script>");
                return;
            }
        }
        /// <summary>
        /// 预览xml
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnXMLView_Click(object sender, EventArgs e)
        {
            ModifyXML();
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>show('hideView','iframeView','XMLFile.aspx?fileName=" + BasePath + "');</script>");
        }
        /// <summary>
        /// 手动修改xml数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnXMLModify_Click(object sender, EventArgs e)
        {
            ModifyXML();
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>show('hideModifyXML','iframeModifyXML','TaskModify.aspx?fileName=" + BasePath + "');</script>");
        }



    }
}