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
    public partial class LogMasterErrorMsg : System.Web.UI.Page
    {
        private TraceLogDAL traceLogDAL;
        private TraceLogMasterDAL traceLogMasterDAL;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                TraceLog traceLog = null;
                string msg = string.Empty;
                traceLogDAL = new TraceLogDAL();
                traceLogMasterDAL = new TraceLogMasterDAL();

                if (Request.QueryString["ID"].ToString() != string.Empty && Request.QueryString["TYPE"].ToString() != string.Empty)
                {
                    //int traceLogMasterId = int.Parse(Request.QueryString["ID"].ToString());
                    string traceLogMasterId = Request.QueryString["ID"];
                    if (Request.QueryString["TYPE"].ToString() == "State")
                    {
                        //traceLog = (from t in traceLogDAL.GetTraceLogs()
                        //            where t.TraceLogMaster_ID == traceLogMasterId && t.State == "Failed"
                        //            select t).FirstOrDefault();
                        traceLog = (from t in traceLogDAL.Read()
                                    where t.TraceLogMaster_ID == traceLogMasterId && t.Status == "Failed"
                                    select t).OrderByDescending(per => per.ID).FirstOrDefault();

                        msg = traceLog.RunInfo;
                    }
                }
                this.divMsg.InnerText = msg;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('"+ex.Message.Replace("'","").Replace("\"","").Replace("\r\n","")+"');</script>");
            }
            
        }


    }
}