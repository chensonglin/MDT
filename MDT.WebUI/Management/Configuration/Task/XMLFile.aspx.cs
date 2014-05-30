using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

namespace MDT.WebUI
{
    public partial class XMLFile : System.Web.UI.Page
    {
        string basePath = "";
        public string BasePath
        {
            get { return hideFileName.Value; }
            set { basePath = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }


            trXSLT.Style.Add("display","none");
            trSourceConfig.Style.Add("display", "none");
            trTargetConfig.Style.Add("display", "none");
            if (Request.QueryString["ID"] != null && Request.QueryString["ID"].ToString() != "")
            {
                string id = Request.QueryString["ID"].ToString();
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>ifSrc('"+id+"');</script>");
                return;
            }
            if (Request.QueryString["fileName"] == null || Request.QueryString["fileName"].ToString() == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideView', 'iframeView');</script>");
                return;
            }
            else
            {
                hideFileName.Value = Request.QueryString["fileName"].ToString();
            }
            //if (Session["basePath"] == null || Session["basePath"].ToString() == "")
            //{
            //    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的页面进入！');parent.hide('hideView', 'iframeView');</script>");
            //    return;
            //}
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>ifSrc('');</script>");
        }
    }
}