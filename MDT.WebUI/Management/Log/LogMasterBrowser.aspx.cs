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
using MDT.WebUI.DataTransform;
using MDT.WebUI.DataConsumer;

using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;
using System.Collections;

namespace MDT.WebUI.Management.Log
{
    public partial class LogMasterBrowser : System.Web.UI.Page
    {
        private List<object> values;
        private TraceLogDAL traceLogDAL;
        private TraceLogMasterDAL traceLogMasterDAL;

        private int pageIndex;// 页码
        private int pageCount = 30;// 每页数量
        private int totalCount;// 总数量
        private string predicate;
        private ArrayList listRepeater = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            Ajax.Utility.RegisterTypeForAjax(typeof(LogMasterBrowser));
            traceLogDAL = new TraceLogDAL();
            traceLogMasterDAL = new TraceLogMasterDAL();
            values = new List<object>();
            if (!IsPostBack)
            {
                if (Context.User.Identity.Name.Split(new char[] { '|' })[0] != "MDT2.0")
                {
                    Response.Redirect("/Account/Login.aspx");
                }
                else if (!Context.User.Identity.Name.Split(new char[] { '|' })[4].Contains('1'))
                {
                    aRepeating.Visible = false;
                }
                spanTaskName.Style.Add("display", "none");
                ViewState["RTaskID"] = "";//快速查询的   任务名称
                ViewState["RState"] = "";//快速查询的    执行状态
                ViewState["RStartTime"] = "";//快速查询的   执行开始时间
                ViewState["REndTime"] = "";//快速查询的    执行结束时间
                ViewState["DataCount"] = 0;//快速查询的 数量  
                ViewState["STaskID"] = "";// 高级查询的  任务名称
                ViewState["SDataCol"] = "";//高级查询的   数据字段
                ViewState["SDataMsg"] = ""; //高级查询的  数据信息
                ViewState["SStartTime"] = "";//高级查询的  执行开始时间
                ViewState["SEndTime"] = "";//高级查询的  执行结束时间
                ViewState["SState"] = "";//高级查询的   执行状态
                ViewState["SearchMethod"] = "0";//保存查询状态  0表示快速查询  1表示高级查询
                GetAllTaskName();  //查询任务名称和任务相应的数据字段。
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script> $(function () {$('#dataTable').trOddHilight();$('#dataTable').trClick();});changeRowColor();</script>", false);
        }
        public void GetAllTaskName()
        {
            try
            {
                ETaskDAL taskDAL = new ETaskDAL();
                var task = taskDAL.GetTasks().OrderBy(p => p.TaskName);
                StringBuilder strTaskName = new StringBuilder();
                StringBuilder strAllCol = new StringBuilder();
                strTaskName.Append("*全部|");
                foreach (var item in task)
                {
                    strTaskName.Append(item.ID + "*" + item.TaskName + "|");
                    if (item.Mapping != null && item.Mapping != "")
                    {
                        string mapping = GetMappingString(item.ID, item.Mapping);
                        strAllCol.Append(mapping + "^");
                    }
                }
                hiddenTask.Value = strTaskName.ToString().TrimEnd('|');
                hiddenAllCol.Value = strAllCol.ToString().TrimEnd('^');
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("'", "").Replace("\"", "").Replace("\r", "").Replace("\n", "") + "');</script>", false);
                return;
            }
        }
        /// <summary>
        /// 返回mapping数据结构的字符串 用 ^ 号分隔
        /// </summary>
        /// <param name="strMapping"></param>
        /// <returns></returns>
        public string GetMappingString(int id, string strMapping)
        {
            try
            {
                if (!string.IsNullOrEmpty(strMapping.Trim()))
                {
                    StringBuilder mapping = new StringBuilder();
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(strMapping);
                    string xPath = "Tables/Table";
                    XmlNodeList nodeList = doc.SelectNodes(xPath);
                    XmlNode nodeTable = null;
                    if (nodeList.Count > 0)
                    {
                        nodeTable = nodeList[0];
                    }
                    if (nodeTable == null)
                        return string.Empty;
                    XmlNodeList columNodeList = nodeTable.SelectNodes("Column");
                    foreach (XmlNode columNode in columNodeList)
                    {
                        string value = string.Empty;
                        if (columNode.Attributes["Source"] != null && columNode.Attributes["Target"] != null)
                        {
                            if (columNode.Attributes["Source"].Value == columNode.Attributes["Target"].Value)
                            {
                                value = id + "*" + columNode.Attributes["Source"].Value.ToLower();
                            }
                            else
                            {
                                value = id + "*" + columNode.Attributes["Source"].Value.ToLower() + "|" + id + "*" + columNode.Attributes["Target"].Value.ToLower();
                            }
                        }
                        else
                        {
                            if (columNode.Attributes["Source"] != null)
                                value = id + "*" + columNode.Attributes["Source"].Value;
                            if (columNode.Attributes["Target"] != null)
                                value = id + "*" + columNode.Attributes["Target"].Value;
                        }
                        mapping.Append(value + "|");
                    }
                    return mapping.ToString().TrimEnd('|');
                }
                else
                    return string.Empty;
            }
            catch (Exception)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('获取数据字段时出现异常,部分任务的数据字段未获取成功！');</script>", false);
                return "";
            }
        }
        //快速查询的查询方法
        private void InitTable()
        {
            int count;
            int skipCount;
            var traceLogsMaster = from t in traceLogMasterDAL.GetTraceLogs()
                                  select new
                                  {
                                      ID = t.ID,
                                      ETask_ID = t.ETask_ID,
                                      //ProcessLN = t.ProcessLN,
                                      TaskName = t.ETask.TaskName,
                                      Status = t.Status,
                                      Data = "<DataMessage>...",
                                      DataCount = t.DataCount,
                                      StartTime = t.StartTime,
                                      EndTime = t.EndTime,
                                      Note = t.Note
                                  };
            RestoreQueryCondition();
            if (!String.IsNullOrEmpty(predicate) && values != null)
            {
                traceLogsMaster = traceLogsMaster.Where(predicate, values.ToArray());
            }
            if (AspNetPager1.CurrentPageIndex != 1)
                pageIndex = AspNetPager1.CurrentPageIndex;
            else
                pageIndex = 1;
            // 设置总页数
            count = traceLogsMaster.Count();
            totalCount = count / pageCount;
            if ((count % pageCount) != 0)
            {
                totalCount++;
            }
            if (totalCount == 0)
            {
                totalCount = 1;
            }
            lblCount.Text = count.ToString();
            lblCurrentPage.Text = pageIndex.ToString();
            lblPageCount.Text = totalCount.ToString();
            skipCount = (pageIndex == 1) ? 0 : ((pageIndex - 1) * pageCount);

            var logsData = traceLogsMaster.OrderByDescending(p => p.ID).Skip(skipCount).Take(pageCount);
            if (ViewState["SearchMethod"].ToString() == "0")
            {
                this.Repeater1.DataSource = logsData;
                this.Repeater1.DataBind();
                Repeater2.DataSource = null;
                Repeater2.DataBind();
            }
            else
            {
                Repeater2.DataSource = logsData;
                Repeater2.DataBind();
                Repeater1.DataSource = null;
                Repeater1.DataBind();
            }
            this.AspNetPager1.PageSize = pageCount;
            this.AspNetPager1.RecordCount = count;
        }

        //保存快速查询的  查询条件
        private void RestoreQueryCondition()
        {
            predicate = String.Empty;
            values.Clear();
            int index = 0;
            if (ViewState["RTaskID"].ToString() != "")
            {
                predicate = "ETask_ID = @" + index.ToString();
                values.Add(Convert.ToInt32(ViewState["RTaskID"].ToString()));
                index++;
            }
            if (ViewState["RState"].ToString() != "")
            {
                if (String.IsNullOrEmpty(predicate))
                    predicate = "State = @0 ";
                else
                    predicate += String.Format(" and State = @{0}", index);
                values.Add(ViewState["RState"].ToString());
                index++;
            }
            if (ViewState["RStartTime"].ToString() != "")
            {
                if (String.IsNullOrEmpty(predicate))
                    predicate = "EndTime >= @0 ";
                else
                    predicate += String.Format(" and EndTime >= @{0} ", index);
                values.Add(DateTime.Parse(ViewState["RStartTime"].ToString()));
                index++;
            }
            if (ViewState["REndTime"].ToString() != "")
            {
                if (String.IsNullOrEmpty(predicate))
                    predicate = "EndTime <= @0 ";
                else
                    predicate += String.Format(" and EndTime <= @{0} ", index);
                values.Add(DateTime.Parse(ViewState["REndTime"].ToString()));
                index++;
            }
            if (ViewState["DataCount"].ToString() != "")
            {
                if (String.IsNullOrEmpty(predicate))
                    predicate = "DataCount >=@0";
                else
                    predicate += String.Format(" and DataCount >=@{0} ", index);
                values.Add(Convert.ToInt32(ViewState["DataCount"].ToString()));
            }
        }

        //分页控件  页次发生改变事件
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindData();//绑定数据
        }
        /// <summary>
        /// 绑定数据的方法
        /// </summary>
        private void BindData()
        {
            try
            {
                if (ViewState["SearchMethod"].ToString() == "1")
                {
                    BindSeniorSearch();//高级查询
                }
                else
                {
                    this.InitTable();//快速查询
                }
            }
            catch (Exception ex)
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                Repeater2.DataSource = null;
                Repeater2.DataBind();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("\"", "").Replace("\'", "") + "');<script>", false);
            }
        }

        /// <summary>
        /// repeater1   绑定数据后触发的事件 
        /// </summary>
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataBound(e);
            }
        }
        /// <summary>
        /// repeater1   绑定数据后触发的事件 
        /// </summary>
        protected void Repeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataBound(e);
                Label lblDataCount = (Label)e.Item.FindControl("lblDataCount");
                CheckBox ck = (CheckBox)e.Item.FindControl("ckRepeating");
                if (lblDataCount.Text == "0")
                {
                    ck.Enabled = false;
                    ck.ToolTip = "总数量=0,不能重发。";
                }
            }
        }

        private static void DataBound(RepeaterItemEventArgs e)
        {
            Label lblState = (Label)e.Item.FindControl("lblState");
            Label lblID = (Label)e.Item.FindControl("lblID");
            if (lblState != null && lblID != null)
            {
                if (lblState.Text == "Failed")
                {
                    lblState.Text = "<a onclick=\"show('hideErrorMsg','iframeErrorMsg','LogMasterErrorMsg.aspx?ID=" + lblID.Text + "&TYPE=State')\"  title='查看失败信息'>" + lblState.Text + "</a>";
                }
            }
            Label lblData = (Label)e.Item.FindControl("lblData");
            lblData.Text = "<a onclick=\"show('hideDataMessage','iframeDataMessage','LogMessageDetail.aspx?ID=" + lblID.Text + "&TYPE=DataMessage');\" title='查看数据信息'>DataMessage...</a>";

            Label lblProcess = (Label)e.Item.FindControl("lblProcess");
            lblProcess.Text = "<a onclick=\"window.location.href='LogMasterProcessMsg.aspx?ID=" + lblID.Text + "'\" title='查看执行过程'>查看执行过程</a>";
        }

        /// <summary>
        /// 转页  按钮“go” 的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearchByPage_Click(object sender, EventArgs e)
        {
            if (txtPageForSearch.Text.Trim() == "")
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请输入要查询的页次');</script>", false);
                txtPageForSearch.Focus();
            }
            else if (!IsPlusInt(txtPageForSearch.Text.Trim()))
            {
                txtPageForSearch.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('查询的页次必须为正整数！');</script>", false);
                txtPageForSearch.Focus();
            }
            else
            {
                this.AspNetPager1.CurrentPageIndex = int.Parse(txtPageForSearch.Text.Trim());
            }
        }

        /// <summary>
        /// 功　　能：判断指定字符串是否为正整数
        /// </summary>
        /// <param name="strData">指定字符串</param>
        /// <returns>是则返回true，否则返回false</returns>
        public bool IsPlusInt(string strData)
        {
            int iData = 0;
            bool bValid = false;
            try
            {
                iData = int.Parse(strData);
            }
            catch (Exception)
            {
                bValid = false;
            }
            if (iData > 0)//此处不取等于0的数
            {
                //是正整数
                bValid = true;
            }
            else
            {
                //不是正整数
                bValid = false;
            }
            return bValid;
        }

        //计时器事件
        protected void Timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                this.Timer1.Enabled = false;
                InitTable();
            }
            catch (Exception ex)
            {
                Repeater1.DataSource = null;
                Repeater1.DataBind();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "") + "');</script>", false);
            }
        }

        /// <summary>
        /// 快速查询  查询按钮事件
        /// </summary>
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string state = hiddenState.Value;
            divRepMan.Style.Add("display", "none");
            divSearchMain.Style.Add("display", "block");
            if (state == "1")
            {
                ViewState["SearchMethod"] = "0";
                ViewState["RTaskID"] = txtRTaskID.Value;
                ViewState["RState"] = rblState.SelectedValue;
                ViewState["RStartTime"] = StartDate.Value;
                ViewState["REndTime"] = EndDate.Value;
                if (EndDate.Value != "")
                {
                    DateTime dt = Convert.ToDateTime(EndDate.Value);
                    dt = dt.AddDays(1).AddSeconds(-1);
                    ViewState["REndTime"] = dt.ToString();
                }
                AspNetPager1.CurrentPageIndex = 1;
            }
            else if (state == "2")
            {
                int sTaskID = Convert.ToInt32(txtRTaskID.Value);
                ViewState["STaskID"] = sTaskID;// 高级查询的  任务名称
                ViewState["SDataCol"] = txtSDataCol.Value;//高级查询的   数据字段
                ViewState["SDataMsg"] = txtSDataMsg.Text; //高级查询的  数据信息
                ViewState["SStartTime"] = StartDate.Value;//高级查询的  执行开始时间
                ViewState["SEndTime"] = EndDate.Value;//高级查询的  执行结束时间
                if (EndDate.Value != "")
                {
                    DateTime dt = Convert.ToDateTime(EndDate.Value);
                    dt = dt.AddDays(1).AddSeconds(-1);
                    ViewState["SEndTime"] = dt.ToString();
                }
                if (rblState.SelectedIndex > -1)
                {
                    ViewState["SState"] = rblState.SelectedValue;//高级查询的   执行状态
                }
                ViewState["SearchMethod"] = "1";//1表示高级查询   0表示快速查询
                spanTaskName.Style.Add("display", "inline");
            }
            this.AspNetPager1.CurrentPageIndex = 1;
        }
        protected void hideLbtnSearch_Click(object sender, EventArgs e)
        {
            divRepMan.Style.Add("display", "none");
            divSearchMain.Style.Add("display", "block");
            ViewState["SearchMethod"] = "0";
            ViewState["RTaskID"] = txtRTaskID.Value;
            ViewState["RState"] = rblState.SelectedValue;
            ViewState["RStartTime"] = StartDate.Value;
            ViewState["REndTime"] = EndDate.Value;
            if (EndDate.Value != "")
            {
                DateTime dt = Convert.ToDateTime(EndDate.Value);
                dt = dt.AddDays(1).AddSeconds(-1);
                ViewState["REndTime"] = dt.ToString();
            }
            AspNetPager1.CurrentPageIndex = 1;
        }

        /// <summary>
        /// 高级查询的方法
        /// </summary>
        private void BindSeniorSearch()
        {
            int sTaskID = Convert.ToInt32(ViewState["STaskID"]);
            string sDataCol = ViewState["SDataCol"].ToString();
            string sDataMsg = ViewState["SDataMsg"].ToString();
            string sStartTime = ViewState["SStartTime"].ToString();
            string sEndTime = ViewState["SEndTime"].ToString();
            string sState = ViewState["SState"].ToString();
            DataSet ds = traceLogDAL.Read(sTaskID, sDataCol, sDataMsg, sState, sStartTime, sEndTime);
            if (ds == null)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('对不起，没有查询到数据');</script>", false);
                return;
            }
            int count;
            if (AspNetPager1.CurrentPageIndex != 1)
                pageIndex = AspNetPager1.CurrentPageIndex;
            else
                pageIndex = 1;

            // 设置总页数
            count = ds.Tables[0].Rows.Count;
            totalCount = count / pageCount;
            if ((count % pageCount) != 0)
            {
                totalCount++;
            }
            if (totalCount == 0)
            {
                totalCount = 0;
            }
            lblCount.Text = count.ToString();
            lblCurrentPage.Text = pageIndex.ToString();
            lblPageCount.Text = totalCount.ToString();
            DataView dv = ds.Tables[0].DefaultView;
            dv.RowFilter = "number > " + (pageIndex - 1) * pageCount + " and number <= " + (pageIndex) * pageCount;

            this.Repeater1.DataSource = dv;
            this.Repeater1.DataBind();
            this.AspNetPager1.PageSize = pageCount;
            this.AspNetPager1.RecordCount = count;
        }
        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            AspNetPager1.CurrentPageIndex = 1;
        }

        protected void lbtnRepSearch_Click(object sender, EventArgs e)
        {
            ViewState["SearchMethod"] = "2";
            ViewState["RTaskID"] = txtRepTaskId.Value;
            ViewState["RState"] = "Failed";
            ViewState["RStartTime"] = txtRepStartTime.Value;
            ViewState["REndTime"] = txtRepEndTime.Value;
            ViewState["DataCount"] = 1;
            if (txtRepEndTime.Value != "")
            {
                DateTime dt = Convert.ToDateTime(txtRepEndTime.Value);
                dt = dt.AddDays(1).AddSeconds(-1);
                ViewState["REndTime"] = dt.ToString();
            }
            divRepMan.Style.Add("display", "block");
            divSearchMain.Style.Add("display", "none");
            AspNetPager1.CurrentPageIndex = 1;
        }


        /// <summary>
        /// 数据重发的方法
        /// </summary>
        /// <param name="traceLogMasterId">日志id</param>
        private void RepeatData()
        {
            int faileNum = 0;
            int successNum = 0;
            string errorMsg = "";
            for (int i = 0; i < listRepeater.Count; i++)
            {
                try
                {
                    //System.Threading.Thread.Sleep(500);
                    //int traceLogMasterId = Convert.ToInt32(listRepeater[i]);
                    string traceLogMasterId = listRepeater[i].ToString();
                    var traceLogTemp = (from t in traceLogDAL.Read()
                                        where t.TraceLogMaster_ID == traceLogMasterId && t.Status == "Failed"
                                        select t
                            ).FirstOrDefault();

                    if (traceLogTemp == null || String.IsNullOrEmpty(traceLogTemp.Data))
                    {
                        faileNum++;
                        errorMsg = errorMsg + "第" + i + "条:日志表无数据.";
                        Session["State"] = "共" + listRepeater.Count + "条数据，已发送" + (i + 1) + "条，成功" + successNum + "条，失败" + faileNum + "条。";
                        continue;
                    }
                    //szq modify at 20111021 从生产节点重发数据
                    if (traceLogTemp != null)
                    {
                        TraceLog traceLog = null;
                        if (traceLogTemp.Stage != "DataProducer")
                        {
                            //traceLog = (from t in traceLogDAL.Read()
                            //            where t.ProcessLN == traceLogTemp.ProcessLN && t.TraceLogMaster_ID == traceLogTemp.TraceLogMaster_ID && t.Stage == "DataProducer"
                            //            select t
                            //            ).FirstOrDefault();

                            // 去掉处理批次 code by wk 2013-01-23
                            traceLog = (from t in traceLogDAL.Read()
                                        where t.TraceLogMaster_ID == traceLogTemp.TraceLogMaster_ID && t.Stage == "DataProducer"
                                        select t
                                        ).FirstOrDefault();
                        }
                        else
                        {
                            traceLog = traceLogTemp;
                        }
                        if (traceLog != null && !String.IsNullOrEmpty(traceLog.Data))
                        {
                            using (DataTransformServiceClient client = new DataTransformServiceClient())
                            {
                                client.ReSend(traceLog.ID);
                            }
                        }
                        // 保存处理状态
                        var traceLogMaster = (from t in traceLogMasterDAL.GetTraceLogs()
                                              where t.ID == traceLogMasterId
                                              select t).FirstOrDefault();
                        traceLogMaster.Note = "手动触发";
                        traceLogMasterDAL.Update(traceLogMaster);
                    }
                    successNum++;
                }
                catch (Exception ex)
                {
                    errorMsg = "错误信息:" + ex.Message.Replace("'", "").Replace("\"", "").Replace("\r", "").Replace("\n", "");
                    faileNum++;
                }
                finally
                {
                    Session["State"] = "共" + listRepeater.Count + "条数据，已发送" + (i + 1) + "条，成功" + successNum + "条，失败" + faileNum + "条。";
                }
            }
            Session["errorMsg"] = errorMsg;
            Session["isComplete"] = "true";//任务结束
        }

        public void OpenProgressBar(System.Web.UI.Page Page)
        {
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "", "<script>show('hideProgress','iframeProgress','/Management/Log/Progress.aspx');</script>", false);
            return;
        }
        /// <summary>
        /// 数据重发事件
        /// </summary>
        protected void lbtnRepeating_Click(object sender, EventArgs e)
        {
            try
            {
                listRepeater = new ArrayList();
                for (int i = 0; i < Repeater2.Items.Count; i++)
                {
                    CheckBox ckRepeating = (CheckBox)Repeater2.Items[i].FindControl("ckRepeating");
                    if (!ckRepeating.Checked)
                    {
                        continue;
                    }
                    Label lblID = (Label)Repeater2.Items[i].FindControl("lblID");
                    listRepeater.Add(lblID.Text);
                }
                if (listRepeater.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请选择需要重发的日志信息！');</script>", false);
                    return;
                }
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(RepeatData));
                thread.Start();
                Session["State"] = "正在准备重发数据，请稍后...";
                Session["isComplete"] = "false";
                OpenProgressBar(this.Page);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "") + "');</script>", false);
                return;
            }
        }

    }
}