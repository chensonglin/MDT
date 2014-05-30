using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;

namespace MDT.WebUI.Management.Configuration.Task
{
    public partial class ResultEdit : System.Web.UI.Page
    {
        string basePath = "";
        public string BasePath
        {
            get { return hideFileName.Value; }
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
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideResults', 'iframeResults');</script>");
                return;
            }
            else
            {
                hideFileName.Value = Request.QueryString["fileName"].ToString();
            }

            ViewState["ID"] = 0;
            ViewState["cid"] = 0;
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"] != "")
            {
                int ID = Convert.ToInt32(Request.QueryString["ID"]);
                ViewState["ID"] = ID;
                ViewState["Type"] = Request.QueryString["Type"].ToString();
                if (Request.QueryString["cid"] != null && Request.QueryString["cid"] != "")
                {
                    ViewState["cid"] = Request.QueryString["cid"];
                    BindData();
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideResults', 'iframeResults');</script>");
                return;
            }
            //if (Session["basePath"] == null || Session["basePath"].ToString() == "")
            //{
            //    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideResults', 'iframeResults');</script>");
            //    return;
            //}
        }
        public void BindData()
        {
            string id = ViewState["ID"].ToString();
            string cid = ViewState["cid"].ToString();
            //basePath = Session["basePath"].ToString();
            string type = ViewState["Type"].ToString();
            if (type == "s")
            {
                strPath = Server.MapPath(BasePath + "SourceConfig.xml");
            }
            else if (type == "t")
            {
                strPath = Server.MapPath(BasePath + "TargetConfig.xml");
            }
            xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            XmlNode nl = xmlDoc.SelectSingleNode("//MainTasks/TaskUnit[Id="+id+"]/Results/Result[@Id="+cid+"]");
            if (nl != null)
            {
                txtCommandName.Text = nl["CommandName"].InnerText;
                txtXmlPath.Text = nl["XmlPath"].InnerText;
            }
        }
        /// <summary>
        /// 保存 Result
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(ViewState["ID"]); //任务ID
            //basePath = Session["basePath"].ToString();
            if (ViewState["Type"] != null && ViewState["Type"].ToString() == "s")
            {//源SourceConfig
                strPath = Server.MapPath(BasePath + "SourceConfig.xml");
            }
            else if (ViewState["Type"] != null && ViewState["Type"].ToString() == "t")
            {//目标SourceConfig
                strPath = Server.MapPath(BasePath + "TargetConfig.xml");
            }
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(strPath);
            int cid = Convert.ToInt32(ViewState["cid"]); //Result ID
            if (cid == 0)  //新增
            {
                XmlNode nl = xmlDoc.SelectSingleNode("//MainTasks/TaskUnit[Id=" + id + "]/Results");
                if (nl != null)
                {
                    XmlElement xe1 = xmlDoc.CreateElement("Result");
                    XmlElement xe2 = xmlDoc.CreateElement("CommandName");
                    XmlElement xe3 = xmlDoc.CreateElement("XmlPath");
                    xe2.InnerText = txtCommandName.Text;
                    xe3.InnerText = txtXmlPath.Text;
                    xe1.AppendChild(xe2);
                    xe1.AppendChild(xe3);
                    int count = 0;
                    if (nl.ChildNodes.Count >0)
                    {
                        count = Convert.ToInt32(nl.LastChild.Attributes["Id"].Value);
                    }
                    XmlAttribute xa = xmlDoc.CreateAttribute("Id");
                    xa.Value = (count+1)+"";
                    xe1.Attributes.Append(xa);
                    nl.AppendChild(xe1);
                }
                xmlDoc.Save(strPath);
            }
            else //修改
            {
                XmlNode nl = xmlDoc.SelectSingleNode("//MainTasks/TaskUnit[Id=" + id + "]/Results/Result[@Id="+cid+"]");
                if (nl != null)
                {
                    XmlElement xe1 = (XmlElement)nl;
                    xe1.RemoveAll();
                    XmlElement xe2 = xmlDoc.CreateElement("CommandName");
                    XmlElement xe3 = xmlDoc.CreateElement("XmlPath");
                    xe2.InnerText = txtCommandName.Text;
                    xe3.InnerText = txtXmlPath.Text;
                    xe1.AppendChild(xe2);
                    xe1.AppendChild(xe3);
                    XmlAttribute ax = xmlDoc.CreateAttribute("Id");
                    ax.Value = cid.ToString();
                    xe1.Attributes.Append(ax);
                    nl.ParentNode.ReplaceChild(xe1, nl);
                    xmlDoc.Save(strPath);
                }
            }
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>openerRefresh();alert('保存成功！');</script>");
        }
    }
}