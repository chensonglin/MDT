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

using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;

namespace MDT.WebUI.Management.Log
{
    public partial class LogMasterProcessMsg : System.Web.UI.Page
    {
        private string traceLogMasterId = String.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            //traceLogMasterId = int.Parse(Request["ID"].ToString());
            traceLogMasterId = Request["ID"].ToString();

            if (!IsPostBack)
            {
                InitTable();
            }
        }

        private void InitTable()
        {

            TraceLogDAL traceLogDAL = new TraceLogDAL();
            var traceLogsMaster = from t in traceLogDAL.Read()
                                  where t.TraceLogMaster_ID == traceLogMasterId
                                  select new
                                  {
                                      ID = t.ID,
                                      ETask_ID = t.ETask_ID,
                                      Stage = t.Stage,
                                      State = t.Status,
                                      RunInfo = t.RunInfo,
                                      Data = t.Data,
                                      Data2 = "DataMessage...",
                                      StartTime = t.StartTime,
                                      EndTime = t.EndTime
                                  };

            this.Repeater1.DataSource = traceLogsMaster;
            this.Repeater1.DataBind();
        }


        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "DataMessage")
            {
                Label lblD = (Label)e.Item.FindControl("lblID");
                if (lblD != null)
	            {
                     ClientScript.RegisterStartupScript(typeof(Page), "", "<script>ShowDetail(" + lblD.Text + ")</script>");
	            }
            }
        }
    }
}