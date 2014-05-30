using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;

namespace MDT.WebUI.Management.Configuration.Task
{
    public partial class TaskMapping : System.Web.UI.Page
    {
        private int id;
        private ETaskDAL taskDAL;
        private ETask etask;
        protected void Page_Load(object sender, EventArgs e)
        {
            taskDAL = new ETaskDAL();
            id = int.Parse(Request["ID"].ToString());
            if (!Page.IsPostBack)
            {
                etask = (from t in taskDAL.GetTasks()
                         where t.ID == id
                         select t).FirstOrDefault();
                Response.Clear();
                Response.ContentType = "text/xml";
                Response.Write(etask.Mapping);
            }
        }
    }
}