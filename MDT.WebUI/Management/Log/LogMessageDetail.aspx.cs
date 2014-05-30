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
    public partial class LogMessageDetail : System.Web.UI.Page
    {
        private TraceLogDAL traceLogDAL;
        private TraceLogMasterDAL traceLogMasterDAL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }
            try
            {
                if (Context.User.Identity.Name.Split(new char[] { '|' })[0] != "MDT2.0")
                {
                    Response.Redirect("/Account/Login.aspx");
                }
                if (Request.QueryString["ID"] == null || Request.QueryString["ID"].ToString() == "")
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请从正确的也没进入！');</script>");
                    return;
                }
                //int traceLogMasterId = int.Parse(Request.QueryString["ID"].ToString());
                string traceLogMasterId = Request.QueryString["ID"].ToString();
                TraceLog traceLog = null;
                string msg = string.Empty;
                traceLogDAL = new TraceLogDAL();
                traceLogMasterDAL = new TraceLogMasterDAL();

                Response.Clear();
                Response.ContentType = "text/xml";

                if (Request.QueryString["ID"].ToString() != string.Empty && Request.QueryString["TYPE"].ToString() != string.Empty)
                {
                    if (Request.QueryString["TYPE"].ToString() == "DataMessage")
                    {
                        traceLog = (from t in traceLogDAL.Read()
                                    where t.TraceLogMaster_ID == traceLogMasterId && t.Stage == "DataProducer"
                                    select t).FirstOrDefault();
                        if (string.IsNullOrEmpty(traceLog.Data))
                        {
                            msg = "<data>无数据信息！</data>";
                        }
                        else
                        {
                            msg = traceLog.Data;
                        }
                    }
                    if (Request["TYPE"].ToString() == "TaskName")
                    {
                        ETask task = getETask(Request["ID"].ToString());
                        if (task != null)
                        {
                            if (Request["TASKTYPE"].ToString() == "Mapping")
                                msg = task.Mapping;
                            if (Request["TASKTYPE"].ToString() == "SourceConfig")
                                msg = task.SourceESchema.ESource.SourceConfig;
                            if (Request["TASKTYPE"].ToString() == "TSourceConfig")
                                msg = task.TargetESchema.ESource.SourceConfig;
                        }
                    }
                    if (Request["TYPE"].ToString() == "State")
                    {
                        traceLog = (from t in traceLogDAL.Read()
                                    where t.TraceLogMaster_ID == traceLogMasterId && t.Status == "Failed"
                                    select t).FirstOrDefault();

                        msg = traceLog.RunInfo;
                    }
                    if (Request["TYPE"].ToString() == "DataMessageProcess")
                    {
                        //traceLog = (from t in traceLogDAL.Read()
                        //            where t.ID == traceLogMasterId
                        //            select t).FirstOrDefault();
                        traceLog = (from t in traceLogDAL.Read()
                                    where t.TraceLogMaster_ID == traceLogMasterId
                                    select t).FirstOrDefault();
                        msg = traceLog.Data;
                    }
                }
                Response.Write(msg);
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page),"","<script>alert('"+ex.Message.Replace("\"","").Replace("'","").Replace("\r\n","")+"');</script>");
            }
        }


        private ETask getETask(string taskId)
        {
            ETask task = null;
            ETaskDAL taskDAL = null;

            try
            {
                int id = Convert.ToInt32(taskId);
                taskDAL = new ETaskDAL();
                task = taskDAL.GetTasks().Where(p => p.ID == id).SingleOrDefault();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page),"","<script>alert('"+ex.Message.Replace("'","").Replace("\"","")+"');</script>");
            }
            return task;
        }
    }
}