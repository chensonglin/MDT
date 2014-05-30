using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MDT.WebUI.Management.Log
{
    public partial class LogMasterTaskDetail : System.Web.UI.Page
    {
        string id = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            id = Request["ID"].ToString();
            if (!Page.IsPostBack)
            {
                string taskType = "Mapping";
                this.rbtType.SelectedValue = "Mapping";
                string url = "LogMessageDetail.aspx?ID=" + id + "&TYPE=TaskName" + "&TASKTYPE=" + taskType;
                this.iframeDetail.Attributes["src"] = url; 
            }
            
        }

        protected void rbtType_Click(object sender, EventArgs e)
        {
            string taskType = this.rbtType.SelectedValue;
            string url = "LogMessageDetail.aspx?ID=" + id + "&TYPE=TaskName" + "&TASKTYPE=" + taskType;
            this.iframeDetail.Attributes["src"] = url;
        }
    }
}