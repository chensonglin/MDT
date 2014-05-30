using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Xml.XPath;

using MDT.ManageCenter.DataContract;


namespace MDT.WebUI.Management.Configuration.Task
{
    public partial class SourceConfig : System.Web.UI.Page
    {
        string basePath = "";
        public string BasePath
        {
            get { return hideFileName.Value; }
            set { basePath = value; }
        }

        string strPath = "";
        XmlDocument xmlDoc = null;
        List<EParameter> listEP = new List<EParameter>();
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Session["basePath"] == null || Session["basePath"].ToString() == "")
            //{
            //    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideCommand', 'iframeCommand');</script>");
            //    return;
            //}
            if (Page.IsPostBack)
            {
                return;
            }
            if (Request.QueryString["fileName"] == null || Request.QueryString["fileName"].ToString() == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideCommand', 'iframeCommand');</script>");
                return;
            }
            else
            {
                hideFileName.Value = Request.QueryString["fileName"].ToString();
            }
            List<EParameter> listEP = new List<EParameter>();
            ViewState["listEP"] = listEP;
            ViewState["tid"] = 0;
            ViewState["cid"] = 0;
            if (Request.QueryString["tid"] != null && Request.QueryString["tid"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["tid"]);
                ViewState["tid"] = ID;
                string type = Request.QueryString["Type"].ToString();
                ViewState["Type"] = type;
                //if (type.IndexOf("p") > -1)
                //{
                //    lblTitle.Text = "配置事后任务";
                //}
                if (Request.QueryString["cid"] != null && Request.QueryString["cid"] != "")
                {//如果传入的参数中包含cid的信息则为修改
                    int cid = Convert.ToInt32(Request.QueryString["cid"]);
                    ViewState["cid"] = cid;
                    BindData();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideCommand', 'iframeCommand');</script>");
                return;
            }
        }
        private void BindData()
        {
            string id = ViewState["tid"].ToString();
            string cid = ViewState["cid"].ToString();
            //basePath = Session["basePath"].ToString();
            string type = ViewState["Type"].ToString();
            if (type.IndexOf("s") > -1)
            {
                strPath = Server.MapPath(BasePath + "SourceConfig.xml");
            }
            else if (type.IndexOf("t") > -1)
            {
                strPath = Server.MapPath(BasePath + "TargetConfig.xml");
            }
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            XmlNode nl = null;
            if (type.IndexOf("p") < 0)
            {
                nl = xmlDoc.SelectSingleNode("//MainTasks/TaskUnit[Id=" + id + "]/Commands/ECommand[@Id=" + cid + "]");
            }
            else
            {
                nl = xmlDoc.SelectSingleNode("//PostTasks/TaskUnit[Id=" + id + "]/Commands/ECommand[@Id=" + cid + "]");
            }
            if (nl != null)
            {
                txtName.Text = nl["CommandName"].InnerText;
                txtContent.Value = nl["CommandText"].InnerText;
                txtCommandType.Text = nl["CommandType"].InnerText;
                txtHasResult.Value = nl["HasResult"].InnerText;
                txtPValue.Text = nl["ParameterValue"].InnerText;
                txtPFrom.Text = nl["ParameterValueFrom"].InnerText;
                txtPOjbectName.Text = nl["ParameterValueOjbectName"].InnerText;
                txtSourceLink.Text = nl["SourceLink"].InnerText;
                txtSourceType.Text = nl["SourceType"].InnerText;
                List<EParameter> listP = new List<EParameter>();
                XmlNodeList nl2 = nl["Parameters"].ChildNodes;
                foreach (XmlNode item in nl2)
                {
                    EParameter ep = new EParameter();
                    ep.Name = item["Name"].InnerText;
                    ep.Type = item["Type"].InnerText;
                    listP.Add(ep);
                }
                Repeater1.DataSource = listP;
                Repeater1.DataBind();
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string type = ViewState["Type"].ToString();
                if (type.IndexOf('p') > 0)
                {
                    SavePostTask();
                }
                else
                {
                    SaveMainTask();
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('保存失败！"+ex.Message.Replace("\"","").Replace("'","")+"');</script>");
                return;
            }
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>openerRefresh();alert('保存成功！');</script>");
        }
        /// <summary>
        /// 保存MainTask
        /// </summary>
        private void SaveMainTask()
        {
            int id = Convert.ToInt32(ViewState["tid"]);
            //basePath = Session["basePath"].ToString();
            if (ViewState["Type"] != null && ViewState["Type"].ToString() == "s")
            {
                strPath = Server.MapPath(BasePath + "SourceConfig.xml");
            }
            else if (ViewState["Type"] != null && ViewState["Type"].ToString() == "t")
            {
                strPath = Server.MapPath(BasePath + "TargetConfig.xml");
            }

            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            int cid = Convert.ToInt32(ViewState["cid"]);
            if (cid == 0)
            {
                XmlNode nl = xmlDoc.SelectSingleNode("//MainTasks/TaskUnit[Id=" + id + "]/Commands");
                if (nl != null)
                {
                    int count = 0;
                    if (nl.LastChild != null)
                    {
                        count = Convert.ToInt32(nl.LastChild.Attributes["Id"].Value);
                    }
                    XmlElement xe1 = xmlDoc.CreateElement("ECommand");
                    XmlElement xe2 = xmlDoc.CreateElement("CommandName");
                    XmlElement xe3 = xmlDoc.CreateElement("CommandText");
                    XmlElement xe4 = xmlDoc.CreateElement("CommandType");
                    XmlElement xe5 = xmlDoc.CreateElement("HasResult");
                    XmlElement xe6 = xmlDoc.CreateElement("ParameterValue");
                    XmlElement xe7 = xmlDoc.CreateElement("ParameterValueFrom");
                    XmlElement xe8 = xmlDoc.CreateElement("ParameterValueOjbectName");
                    XmlElement xe9 = xmlDoc.CreateElement("Parameters");
                    XmlElement xe10 = xmlDoc.CreateElement("SourceLink");
                    XmlElement xe11 = xmlDoc.CreateElement("SourceType");
                    xe1.SetAttribute("Id", (count + 1).ToString());
                    xe2.InnerText = txtName.Text;
                    xe3.InnerText = txtContent.Value;
                    xe4.InnerText = txtCommandType.Text;
                    xe5.InnerText = txtHasResult.Value;
                    xe6.InnerText = txtPValue.Text;
                    xe7.InnerText = txtPFrom.Text;
                    xe8.InnerText = txtPOjbectName.Text;
                    xe10.InnerText = txtSourceLink.Text;
                    xe11.InnerText = txtSourceType.Text;
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        XmlElement xe12 = xmlDoc.CreateElement("EParameter");
                        XmlElement xe13 = xmlDoc.CreateElement("Name");
                        XmlElement xe14 = xmlDoc.CreateElement("Type");
                        string pName = ((TextBox)Repeater1.Items[i].FindControl("txtPName")).Text;
                        string pType = ((TextBox)Repeater1.Items[i].FindControl("txtPType")).Text;
                        if (pName.Trim() == "" || pType.Trim() == "")
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请正确填写参数信息，或者删除该行！');</script>");
                            return;
                        }
                        xe13.InnerText = pName;
                        xe14.InnerText = pType;
                        xe12.AppendChild(xe13);
                        xe12.AppendChild(xe14);
                        xe9.AppendChild(xe12);
                    }
                    xe1.AppendChild(xe2);
                    xe1.AppendChild(xe3);
                    xe1.AppendChild(xe4);
                    xe1.AppendChild(xe5);
                    xe1.AppendChild(xe6);
                    xe1.AppendChild(xe7);
                    xe1.AppendChild(xe8);
                    xe1.AppendChild(xe9);
                    xe1.AppendChild(xe10);
                    xe1.AppendChild(xe11);

                    nl.AppendChild(xe1);
                    xmlDoc.Save(strPath);
                }
            }
            else
            {
                XmlNode nl = xmlDoc.SelectSingleNode("//MainTasks/TaskUnit[Id=" + id + "]/Commands/ECommand[@Id="+cid+"]");
                if (nl != null)
                {
                    XmlElement xe1 = (XmlElement)nl;
                    string commandId = xe1.Attributes["Id"].Value;
                    xe1.RemoveAll();
                    XmlElement xe2 = xmlDoc.CreateElement("CommandName");
                    XmlElement xe3 = xmlDoc.CreateElement("CommandText");
                    XmlElement xe4 = xmlDoc.CreateElement("CommandType");
                    XmlElement xe5 = xmlDoc.CreateElement("HasResult");
                    XmlElement xe6 = xmlDoc.CreateElement("ParameterValue");
                    XmlElement xe7 = xmlDoc.CreateElement("ParameterValueFrom");
                    XmlElement xe8 = xmlDoc.CreateElement("ParameterValueOjbectName");
                    XmlElement xe9 = xmlDoc.CreateElement("Parameters");
                    XmlElement xe10 = xmlDoc.CreateElement("SourceLink");
                    XmlElement xe11 = xmlDoc.CreateElement("SourceType");
                    xe1.SetAttribute("Id", commandId);
                    xe2.InnerText = txtName.Text;
                    xe3.InnerText = txtContent.Value;
                    xe4.InnerText = txtCommandType.Text;
                    xe5.InnerText = txtHasResult.Value;
                    xe6.InnerText = txtPValue.Text;
                    xe7.InnerText = txtPFrom.Text;
                    xe8.InnerText = txtPOjbectName.Text;
                    xe10.InnerText = txtSourceLink.Text;
                    xe11.InnerText = txtSourceType.Text;
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        XmlElement xe12 = xmlDoc.CreateElement("EParameter");
                        XmlElement xe13 = xmlDoc.CreateElement("Name");
                        XmlElement xe14 = xmlDoc.CreateElement("Type");
                        string pName = ((TextBox)Repeater1.Items[i].FindControl("txtPName")).Text;
                        string pType = ((TextBox)Repeater1.Items[i].FindControl("txtPType")).Text;
                        if (pName.Trim() == "" || pType.Trim() == "")
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请正确填写参数信息，或者删除该行！');</script>");
                            return;
                        }
                        xe13.InnerText = pName;
                        xe14.InnerText = pType;
                        xe12.AppendChild(xe13);
                        xe12.AppendChild(xe14);
                        xe9.AppendChild(xe12);
                    }
                    xe1.AppendChild(xe2);
                    xe1.AppendChild(xe3);
                    xe1.AppendChild(xe4);
                    xe1.AppendChild(xe5);
                    xe1.AppendChild(xe6);
                    xe1.AppendChild(xe7);
                    xe1.AppendChild(xe8);
                    xe1.AppendChild(xe9);
                    xe1.AppendChild(xe10);
                    xe1.AppendChild(xe11);

                    nl.ParentNode.ReplaceChild(xe1, nl);
                    xmlDoc.Save(strPath);
                }
            }

        }
        /// <summary>
        /// 保存PostTask
        /// </summary>
        private void SavePostTask()
        {
            int id = Convert.ToInt32(ViewState["tid"]);
            //basePath = Session["basePath"].ToString();
            if (ViewState["Type"] != null && ViewState["Type"].ToString() == "sp")
            {
                strPath = Server.MapPath(BasePath + "SourceConfig.xml");
            }
            else if (ViewState["Type"] != null && ViewState["Type"].ToString() == "tp")
            {
                strPath = Server.MapPath(BasePath + "TargetConfig.xml");
            }

            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            XmlNode nl = null;
            int cid = Convert.ToInt32(ViewState["cid"]);
            if (cid == 0)
            {
                nl = xmlDoc.SelectSingleNode("//PostTasks/TaskUnit[Id=" + id + "]/Commands");
                if (nl == null)
                {
                    nl = xmlDoc.SelectSingleNode("//PostTasks");
                    XmlElement tu1 = xmlDoc.CreateElement("TaskUnit");
                    XmlElement tu2 = xmlDoc.CreateElement("Commands");
                    XmlElement tu3 = xmlDoc.CreateElement("HasTraceLog");
                    XmlElement tu4 = xmlDoc.CreateElement("HasTransaction");
                    XmlElement tu5 = xmlDoc.CreateElement("Id");
                    XmlElement xe1 = xmlDoc.CreateElement("ECommand");
                    XmlElement xe2 = xmlDoc.CreateElement("CommandName");
                    XmlElement xe3 = xmlDoc.CreateElement("CommandText");
                    XmlElement xe4 = xmlDoc.CreateElement("CommandType");
                    XmlElement xe5 = xmlDoc.CreateElement("HasResult");
                    XmlElement xe6 = xmlDoc.CreateElement("ParameterValue");
                    XmlElement xe7 = xmlDoc.CreateElement("ParameterValueFrom");
                    XmlElement xe8 = xmlDoc.CreateElement("ParameterValueOjbectName");
                    XmlElement xe9 = xmlDoc.CreateElement("Parameters");
                    XmlElement xe10 = xmlDoc.CreateElement("SourceLink");
                    XmlElement xe11 = xmlDoc.CreateElement("SourceType");
                    xe2.InnerText = txtName.Text;
                    xe3.InnerText = txtContent.Value;
                    xe4.InnerText = txtCommandType.Text;
                    xe5.InnerText = txtHasResult.Value;
                    xe6.InnerText = txtPValue.Text;
                    xe7.InnerText = txtPFrom.Text;
                    xe8.InnerText = txtPOjbectName.Text;
                    xe10.InnerText = txtSourceLink.Text;
                    xe11.InnerText = txtSourceType.Text;
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        XmlElement xe12 = xmlDoc.CreateElement("EParameter");
                        XmlElement xe13 = xmlDoc.CreateElement("Name");
                        XmlElement xe14 = xmlDoc.CreateElement("Type");
                        string pName = ((TextBox)Repeater1.Items[i].FindControl("txtPName")).Text;
                        string pType = ((TextBox)Repeater1.Items[i].FindControl("txtPType")).Text;
                        if (pName.Trim() == "" || pType.Trim() == "")
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请正确填写参数信息，或者删除该行！');</script>");
                            return;
                        }
                        xe13.InnerText = pName;
                        xe14.InnerText = pType;
                        xe12.AppendChild(xe13);
                        xe12.AppendChild(xe14);
                        xe9.AppendChild(xe12);
                    }
                    xe1.SetAttribute("Id", "1");
                    xe1.AppendChild(xe2);
                    xe1.AppendChild(xe3);
                    xe1.AppendChild(xe4);
                    xe1.AppendChild(xe5);
                    xe1.AppendChild(xe6);
                    xe1.AppendChild(xe7);
                    xe1.AppendChild(xe8);
                    xe1.AppendChild(xe9);
                    xe1.AppendChild(xe10);
                    xe1.AppendChild(xe11);
                    tu5.InnerText = id.ToString();
                    tu2.AppendChild(xe1);
                    tu1.AppendChild(tu2);
                    tu1.AppendChild(tu3);
                    tu1.AppendChild(tu4);
                    tu1.AppendChild(tu5);
                    nl.AppendChild(tu1);
                    xmlDoc.Save(strPath);
                }
                else if (nl != null)
                {
                    int count = 0;
                    if (nl.LastChild != null)
                    {
                        count = Convert.ToInt32(nl.LastChild.Attributes["Id"].Value);
                    }
                    XmlElement xe1 = xmlDoc.CreateElement("ECommand");
                    XmlElement xe2 = xmlDoc.CreateElement("CommandName");
                    XmlElement xe3 = xmlDoc.CreateElement("CommandText");
                    XmlElement xe4 = xmlDoc.CreateElement("CommandType");
                    XmlElement xe5 = xmlDoc.CreateElement("HasResult");
                    XmlElement xe6 = xmlDoc.CreateElement("ParameterValue");
                    XmlElement xe7 = xmlDoc.CreateElement("ParameterValueFrom");
                    XmlElement xe8 = xmlDoc.CreateElement("ParameterValueOjbectName");
                    XmlElement xe9 = xmlDoc.CreateElement("Parameters");
                    XmlElement xe10 = xmlDoc.CreateElement("SourceLink");
                    XmlElement xe11 = xmlDoc.CreateElement("SourceType");
                    xe2.InnerText = txtName.Text;
                    xe3.InnerText = txtContent.Value;
                    xe4.InnerText = txtCommandType.Text;
                    xe5.InnerText = txtHasResult.Value;
                    xe6.InnerText = txtPValue.Text;
                    xe7.InnerText = txtPFrom.Text;
                    xe8.InnerText = txtPOjbectName.Text;
                    xe10.InnerText = txtSourceLink.Text;
                    xe11.InnerText = txtSourceType.Text;
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        XmlElement xe12 = xmlDoc.CreateElement("EParameter");
                        XmlElement xe13 = xmlDoc.CreateElement("Name");
                        XmlElement xe14 = xmlDoc.CreateElement("Type");
                        string pName = ((TextBox)Repeater1.Items[i].FindControl("txtPName")).Text;
                        string pType = ((TextBox)Repeater1.Items[i].FindControl("txtPType")).Text;
                        if (pName.Trim() == "" || pType.Trim() == "")
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请正确填写参数信息，或者删除该行！');</script>");
                            return;
                        }
                        xe13.InnerText = pName;
                        xe14.InnerText = pType;
                        xe12.AppendChild(xe13);
                        xe12.AppendChild(xe14);
                        xe9.AppendChild(xe12);
                    }
                    xe1.SetAttribute("Id", (count + 1).ToString());
                    xe1.AppendChild(xe2);
                    xe1.AppendChild(xe3);
                    xe1.AppendChild(xe4);
                    xe1.AppendChild(xe5);
                    xe1.AppendChild(xe6);
                    xe1.AppendChild(xe7);
                    xe1.AppendChild(xe8);
                    xe1.AppendChild(xe9);
                    xe1.AppendChild(xe10);
                    xe1.AppendChild(xe11);
                    nl.AppendChild(xe1);
                    xmlDoc.Save(strPath);
                }
            }
            else
            {
                nl = xmlDoc.SelectSingleNode("//PostTasks/TaskUnit[Id=" + id + "]/Commands/ECommand[@Id="+cid+"]");
                if (nl != null)
                {
                    XmlElement xe1 = (XmlElement)nl;
                    string commandId = xe1.Attributes["Id"].Value;
                    xe1.RemoveAll();
                    XmlElement xe2 = xmlDoc.CreateElement("CommandName");
                    XmlElement xe3 = xmlDoc.CreateElement("CommandText");
                    XmlElement xe4 = xmlDoc.CreateElement("CommandType");
                    XmlElement xe5 = xmlDoc.CreateElement("HasResult");
                    XmlElement xe6 = xmlDoc.CreateElement("ParameterValue");
                    XmlElement xe7 = xmlDoc.CreateElement("ParameterValueFrom");
                    XmlElement xe8 = xmlDoc.CreateElement("ParameterValueOjbectName");
                    XmlElement xe9 = xmlDoc.CreateElement("Parameters");
                    XmlElement xe10 = xmlDoc.CreateElement("SourceLink");
                    XmlElement xe11 = xmlDoc.CreateElement("SourceType");
                    xe2.InnerText = txtName.Text;
                    xe3.InnerText = txtContent.Value;
                    xe4.InnerText = txtCommandType.Text;
                    xe5.InnerText = txtHasResult.Value;
                    xe6.InnerText = txtPValue.Text;
                    xe7.InnerText = txtPFrom.Text;
                    xe8.InnerText = txtPOjbectName.Text;
                    xe10.InnerText = txtSourceLink.Text;
                    xe11.InnerText = txtSourceType.Text;
                    for (int i = 0; i < Repeater1.Items.Count; i++)
                    {
                        XmlElement xe12 = xmlDoc.CreateElement("EParameter");
                        XmlElement xe13 = xmlDoc.CreateElement("Name");
                        XmlElement xe14 = xmlDoc.CreateElement("Type");
                        string pName = ((TextBox)Repeater1.Items[i].FindControl("txtPName")).Text;
                        string pType = ((TextBox)Repeater1.Items[i].FindControl("txtPType")).Text;
                        if (pName.Trim() == "" || pType.Trim() == "")
                        {
                            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请正确填写参数信息，或者删除该行！');</script>");
                            return;
                        }
                        xe13.InnerText = pName;
                        xe14.InnerText = pType;
                        xe12.AppendChild(xe13);
                        xe12.AppendChild(xe14);
                        xe9.AppendChild(xe12);
                    }
                    xe1.SetAttribute("Id", commandId);
                    xe1.AppendChild(xe2);
                    xe1.AppendChild(xe3);
                    xe1.AppendChild(xe4);
                    xe1.AppendChild(xe5);
                    xe1.AppendChild(xe6);
                    xe1.AppendChild(xe7);
                    xe1.AppendChild(xe8);
                    xe1.AppendChild(xe9);
                    xe1.AppendChild(xe10);
                    xe1.AppendChild(xe11);
                    nl.ParentNode.ReplaceChild(xe1, nl);
                    xmlDoc.Save(strPath);
                }
            }
            
        }
        /// <summary>
        /// 新增参数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnAddP_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                EParameter ep = new EParameter();
                ep.Name = ((TextBox)Repeater1.Items[i].FindControl("txtPName")).Text;
                ep.Type = ((TextBox)Repeater1.Items[i].FindControl("txtPType")).Text;
                listEP.Add(ep);
            }
            EParameter ep2 = new EParameter();
            ep2.Name = "";
            ep2.Type = "";
            listEP.Add(ep2);
            Repeater1.DataSource = listEP;
            Repeater1.DataBind();
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>scrollBottom();</script>");
        }
        /// <summary>
        /// 删除参数
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                EParameter ep = new EParameter();
                ep.Name = ((TextBox)Repeater1.Items[i].FindControl("txtPName")).Text;
                ep.Type = ((TextBox)Repeater1.Items[i].FindControl("txtPType")).Text;
                listEP.Add(ep);
            }
            int index = e.Item.ItemIndex;
            listEP.RemoveAt(index);
            Repeater1.DataSource = listEP;
            Repeater1.DataBind();
        }
    }
}