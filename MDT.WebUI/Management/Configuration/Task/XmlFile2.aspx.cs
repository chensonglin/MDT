using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.IO;
using System.Text;

using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;

namespace MDT.WebUI.Management.Configuration.Task
{
    public partial class XmlFile2 : System.Web.UI.Page
    {
        string basePath = "";
        string strPath = "";
        XmlDocument xmlDoc = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }
            try
            {
                if (Request.QueryString["id"] != null && Request.QueryString["id"].ToString() != "")
                {
                    int id = Convert.ToInt32(Request.QueryString["id"].ToString());
                    ETaskDAL taskDAL = new ETaskDAL();
                    ETask etask = (from t in taskDAL.GetTasks()
                                   where t.ID == id
                                   select t).FirstOrDefault();
                    Response.ContentType = "text/xml";
                    string type = Request.QueryString["Type"].ToString();
                    if (type == "Mapping")
                        Response.Write(etask.Mapping.ToString());
                    else if (type == "XSLT")
                        Response.Write(etask.XSLTInfo);
                    else if (type == "SourceConfig")
                        Response.Write(etask.SourceESchema.ESource.SourceConfig);
                    else if (type == "TargetConfig")
                        Response.Write(etask.TargetESchema.ESource.SourceConfig);
                    return;
                }
                //if (Session["basePath"] == null || Session["basePath"].ToString() == "")
                //{
                //    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');</script>");
                //    return;
                //}
                if (Request.QueryString["fileName"] == null || Request.QueryString["fileName"].ToString() == "")
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');window.close();</script>");
                    return;
                }
                else
                {
                    basePath = Request.QueryString["fileName"].ToString();
                }
                if (Request.QueryString["Type"] != null && Request.QueryString["Type"] != "")
                {
                    string type = Request.QueryString["Type"];
                    //basePath = Session["basePath"].ToString();
                    strPath = Server.MapPath(basePath + type + ".xml");

                    xmlDoc = new XmlDocument();
                    xmlDoc.Load(strPath);
                    XmlNode xl = null;
                    xl = (XmlNode)xmlDoc.DocumentElement;
                    if (xl == null)
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('获取" + type + "数据时出现异常！');</script>");
                        return;
                    }
                    Response.Clear();
                    Response.ContentType = "text/xml";
                    Response.Write(xl.OuterXml);
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('"+ex.Message.Replace("'","").Replace("\"","")+"');</script>");
                return;
            }
        }


    }
}