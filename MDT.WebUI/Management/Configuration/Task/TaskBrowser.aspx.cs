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

namespace MDT.WebUI.Management.Configuration.Task
{
    public partial class TaskBrowser : System.Web.UI.Page
    {
        private ETaskDAL taskDAL;
        private List<ETask> taskList;
        private ETask etask;
        private string predicate;
        private List<object> values;
        protected void Page_Load(object sender, EventArgs e)
        {
            taskDAL = new ETaskDAL();
            values = new List<object>();
            if (!IsPostBack)
            {
                if (Context.User.Identity.Name.Split(new char[]{'|'})[0] != "MDT2.0")
                {
                    Response.Redirect("/Account/Login.aspx");
                }
                else if (!Context.User.Identity.Name.Split(new char[]{'|'})[4].Contains('1'))
                {
                    aAdd.Visible = false;
                    aModifyXML.Visible = false;
                    aViewXML.Visible = false;
                    lbtnDelete.Visible = false;
                    aNotice.Visible = false;
                }

                ViewState["num"] = "0";//设置序号的初始值
                ViewState["index"] = "0";//设置当前需要展开的任务信息
                GetAllTaskName();  //查询任务名称
                ViewState["TaskName"] = "";
                ViewState["Operater"] = "";
                ViewState["StartTime"] = "";
                ViewState["EndTime"] = "";
                divR.Style.Add("display", "none");
                div1.Style.Add("display", "none");
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script> $(function () {$('#dataTable').trOddHilight();$('#dataTable').trClick();})</script>", false);
        }
        /// <summary>
        /// 查询所有任务名称
        /// </summary>
        public void GetAllTaskName()
        {
            try
            {
                ETaskDAL taskDAL = new ETaskDAL();
                var task = taskDAL.GetTasks().OrderBy(p => p.TaskName);
                string strTaskName = "*全部|";
                foreach (var item in task)
                {
                    strTaskName = strTaskName + item.ID + "*" + item.TaskName + "|";

                }
                strTaskName = strTaskName.TrimEnd('|');
                hiddenTask.Value = strTaskName;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('获取任务名称时发生异常，"+ex.Message.Replace("'","").Replace("\"","")+"');</script>", false);
                return;
            }
        }
        /// <summary>
        /// 显示任务列表
        /// </summary>
        private void BindTaskList()
        {
            try
            {
                var tasks = taskDAL.GetTasks().OrderBy(p => p.TaskName);
                taskList = tasks.ToList();
                RestoreQueryCondition();
                if (!String.IsNullOrEmpty(predicate) && values != null)
                {
                    taskList = tasks.Where(predicate, values.ToArray()).ToList();
                }
                Dictionary<string, List<ETask>> dic = new Dictionary<string, List<ETask>>();
                string typeName = "平台：";
                for (int i = 0; i < taskList.Count; i++)
                {
                    ETask task = taskList[i];
                    if (task.Category == null)
                        typeName = "平台：";
                    else
                        typeName = "平台：" + task.Category;
                    if (dic.Keys.Contains(typeName))
                    {
                        dic[typeName].Add(task);
                    }
                    else
                    {
                        List<ETask> list = new List<ETask>();
                        list.Add(task);
                        dic.Add(typeName, list);
                    }
                }
                StringBuilder sbDivText = new StringBuilder("");
                sbDivText.Append("<table width=\"100%\" id=\"dataTable\" style=\"border:none;\">");
                sbDivText.Append("<thead><tr><td style=\"width:10px; border-right:none;\"></td><td>任务名称</td><td>任务描述</td><td>映射关系</td><td>操作人</td><td>操作日期</td><td>时间间隔</td><td>是否启用</td><td>操作</td></tr></thead>");
                sbDivText.Append("<tbody>");
                List<ETask> listTask = null;
                int rownum = 0;//行号
                foreach (var item in dic.Keys)
                {
                    rownum++;
                    sbDivText.Append("<tr class=\"parent\" id=\"row_" + rownum + "\" style=\"margin-bottom:3px;background-color:White;\" onclick=\"SingleSelect('');\" >");
                    sbDivText.Append("<td style=\"width:10px; \"><img id='imgA_" + rownum + "' alt=\"\" src=\"/images/iconAdd1.GIF\" style=\"display:block;\"  onclick=\"displayTr('" + rownum + "');\" /><img id='imgB_" + rownum + "' alt=\"\" src=\"/images/iconAdd2.GIF\" style=\"display:none;\"  onclick=\"displayTr('" + rownum + "');\" /></td>");
                    sbDivText.Append("<td colspan=\"8\" align=\"left\" valign=\"middle\"><a  onclick=\"SingleSelect(''); displayTr('" + rownum + "');\">" + item + "</a></td>");
                    sbDivText.Append("</tr>");
                    listTask = dic[item];
                    for (int i = 0; i < listTask.Count; i++)
                    {
                        etask = listTask[i];
                        sbDivText.Append("<tr runat=\"server\" class=\"child_row_"+rownum+"\" id=\"TR_"+rownum+"_"+i+"\" onclick=\"SingleSelect(this)\" style=\"border-left:none;display:none;\" >");
                        sbDivText.Append("<td style=\"width:10px; border:none; background-color:White;\"></td>");
                        sbDivText.Append("<td align=\"left\"><span id=\"span_"+rownum+"_"+i+"\" class=\"hide\">"+etask.ID+"</span><span>"+etask.TaskName+"</span></td>");
                        sbDivText.Append("<td align=\"left\">"+etask.Note+"</td>");
                        sbDivText.Append("<td><a onclick=\"show('hideMapping','iframeMapping','TaskMapping.aspx?ID=" + etask.ID + "')\" title=\"单击查看映射信息\" >&lt;Tables&gt;&lt;Tabl......</a></td>");
                        if (etask.EUser == null)
                        {
                            sbDivText.Append("<td align=\"left\"></td>");
                        }
                        else
                        {
                            sbDivText.Append("<td align=\"left\">" + etask.EUser.UserName + "</td>");
                        }
                        sbDivText.Append("<td align=\"left\">" + etask.OperationDate + "</td>");
                        sbDivText.Append("<td align=\"left\">" + etask.Interval + "</td>");
                        string msg = etask.Enable == true ? "1" : "0";
                        string checkStr = etask.Enable == true ? "checked" : "";
                        string action = "";
                        if (etask.Enable == true)
                        {
                            action = "<input disabled=\"disabled\" type='checkbox' " + checkStr + "  onclick=\"Enable('" + etask.ID + "','" + msg + "',this);\" name='checkBoxGroup' value='" + msg + "' Checked='Checked' />";
                        }
                        else
                        {
                            action = "<input disabled=\"disabled\" type='checkbox' " + checkStr + "  onclick=\"Enable('" + etask.ID + "','" + msg + "',this);\" name='checkBoxGroup' value='" + msg + "' />";
                        }
                        sbDivText.Append("<td>"+action+"</td>");
                        sbDivText.Append("<td><a href=\"javascript:show('hideNoticeReceiver', 'iframeNoticeReceiver', '../../Notice/NoticeReceivers.aspx?taskId=" + etask.ID + "');\">查看预警人员</a></td>");
                        sbDivText.Append("</tr>");
                    }
                }
                divTaskList.InnerHtml = sbDivText.ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "") + "');</script>", false);
                return;
            }
        }

        //保存快速查询的  查询条件
        private void RestoreQueryCondition()
        {
            predicate = String.Empty;
            values.Clear();
            int index = 0;
            if (ViewState["TaskName"].ToString() != "")
            {
                predicate = "TaskName.Contains(@" + index.ToString()+")";
                values.Add(ViewState["TaskName"].ToString());
                index++;
            }
            if (ViewState["Operater"].ToString() != "")
            {
                if (String.IsNullOrEmpty(predicate))
                    predicate = "EUser.UserName.Contains(@0) ";
                else
                    predicate += String.Format(" and EUser.UserName.Contains(@{0})", index);
                values.Add(ViewState["Operater"].ToString());
                index++;
            }
            if (ViewState["StartTime"].ToString() != "")
            {
                if (String.IsNullOrEmpty(predicate))
                    predicate = "OperationDate >= @0 ";
                else
                    predicate += String.Format(" and OperationDate >= @{0} ", index);
                values.Add(DateTime.Parse(ViewState["StartTime"].ToString()));
                index++;
            }
            if (ViewState["EndTime"].ToString() != "")
            {
                if (String.IsNullOrEmpty(predicate))
                    predicate = "OperationDate <= @0 ";
                else
                    predicate += String.Format(" and OperationDate <= @{0} ", index);
                values.Add(DateTime.Parse(ViewState["EndTime"].ToString()));
            }
        }
        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.hiddenSelectID.Value);
            etask = (from t in taskDAL.GetTasks()
                     where t.ID == id
                     select t).FirstOrDefault();
            taskDAL.DeleteObject(etask);
            //this.InitTable();
            BindTaskList();
        }
        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void LinkButtonRefresh_Click(object sender, EventArgs e)
        {
            //this.InitTable();
            BindTaskList();
        }

        protected void linkButtonEnable_Click(object sender, EventArgs e)
        {
            int id = int.Parse(this.hiddenEnable.Value.Split('^')[0].ToString());
            string state = this.hiddenEnable.Value.Split('^')[1].ToString();
            etask = (from t in taskDAL.GetTasks()
                        where t.ID == id
                        select t).SingleOrDefault();
            if (state=="1")
            {
                etask.Enable = false;
            }
            else
            {
                etask.Enable = true;
            }
            taskDAL.ModifyObject(etask);
            //this.InitTable();
            BindTaskList();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Label lblID = (Label)e.Item.FindControl("lblID");
        //        Label lblEnable = (Label)e.Item.FindControl("lblEnable");
        //        Label lblCheckBox = (Label)e.Item.FindControl("lblCheckBox");
        //        if (lblEnable != null && lblCheckBox != null)
        //        {
        //            string msg = lblEnable.Text == "True" ? "1" : "0";
        //            string checkStr = lblEnable.Text == "True" ? "checked" : "";
        //            string action = "";
        //            if (lblEnable.Text == "True")
        //            {
        //                action = "<input disabled=\"disabled\" type='checkbox' " + checkStr + "  onclick=\"Enable('" + lblID.Text + "','" + msg + "',this);\" name='checkBoxGroup' value='" + msg + "' Checked='Checked' />";
        //            }
        //            else
        //            {
        //                action = "<input disabled=\"disabled\" type='checkbox' " + checkStr + "  onclick=\"Enable('" + lblID.Text + "','" + msg + "',this);\" name='checkBoxGroup' value='" + msg + "' />";
        //            }
        //            lblCheckBox.Text = action;
        //        }
        //        Label lblMapping = (Label)e.Item.FindControl("lblMapping");
        //        lblMapping.Text = "<a onclick=\"show('hideMapping','iframeMapping','TaskMapping.aspx?ID=" + lblID.Text + "')\" title=\"单击查看映射信息\" >&lt;Tables&gt;&lt;Tabl......</a>";
        //    }
        //}
        //protected void Repeater1_ItemDataBound1(object sender, RepeaterItemEventArgs e)
        //{
        //    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        //    {
        //        Repeater dl = (Repeater)e.Item.FindControl("Repeater2");
        //        Label lblTypeName = (Label)e.Item.FindControl("lblTypeName");

        //        if (ViewState["dic"] != null)
        //        {
        //            Dictionary<string, List<ETask>> dic = (Dictionary<string, List<ETask>>)ViewState["dic"];
        //            dl.DataSource = dic[lblTypeName.Text];
        //            dl.DataBind();
        //        }
        //        dl.Visible = false;
        //    }
        //}
        /// <summary>
        /// 计时器控件  事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            //InitTable();
            BindTaskList();
        }
        /// <summary>
        /// 查询按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSearch_Click(object sender, EventArgs e)
        {
            if (txtRTaskName.Value == "全部")
            {
                ViewState["TaskName"] = "";
            }
            else
            {
                ViewState["TaskName"] = txtRTaskName.Value.Trim();
            }
            ViewState["Operater"] = txtOperater.Value.Trim();
            ViewState["StartTime"] = txtStartTime.Value.Trim();
            ViewState["EndTime"] = txtEndTime.Value.Trim();
            if (txtEndTime.Value != "")
            {
                DateTime dt = Convert.ToDateTime(txtEndTime.Value);
                dt = dt.AddDays(1).AddSeconds(-1);
                ViewState["EndTime"] = dt.ToString();
            }
            BindTaskList();
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>searchDisplay();</script>", false);
        }

        protected void lbtnRTaskName_Click(object sender, EventArgs e)
        {
            DisplayLbtn("TaskName");
        }

        protected void lbtnOperater_Click(object sender, EventArgs e)
        {
            DisplayLbtn("Operater");
        }

        protected void lbtnStartTime_Click(object sender, EventArgs e)
        {
            DisplayLbtn("StartTime");
        }

        protected void lbtnEndTime_Click(object sender, EventArgs e)
        {
            DisplayLbtn("EndTime");
        }
        protected void hideLbtnCloseAll_Click(object sender, EventArgs e)
        {
            ViewState["TaskName"] = "";
            ViewState["Operater"] = "";
            ViewState["StartTime"] = "";
            ViewState["EndTime"] = "";
            BindTaskList();
        }
        private void DisplayLbtn(string str)
        {
            ViewState[str] = "";
            BindTaskList();
        }
        
        public string FormatDate(object date)
        {
            try
            {
                string strReturn = "";
                if (date == null)
                {
                    return "";
                }
                DateTime dt = Convert.ToDateTime(date);
                strReturn  = String.Format("{0:yyyy-MM-dd}",dt);
                return strReturn;
            }
            catch (Exception)
            {
                return "";
            }
        }
        // <summary>
        /// 功能描述：判断字节数，返回固定字节长度的字符串
        /// </summary>
        /// <param name="cOriginalityString">传入字符串</param>
        /// <param name="iLenReturnString">返回的字符串字节长度</param>
        /// <param name="bReturnDefaultString">是否在结尾增加“...”</param>
        /// <returns>截取后的字符串</returns>
        public string GetStringPartContent(string cOriginalityString, int iLenReturnString, bool bReturnDefaultString)
        {
            string cReturnString = "";
            string cReturnDefaultString = "...";		//缺省字符串的内容
            cOriginalityString = cReturnString;

            if (cReturnString.Length > iLenReturnString)
            {
                cReturnString = cReturnString.Substring(0, iLenReturnString);
            }

            int ilength = iLenReturnString;		//此方法不区分汉字，一个汉字只算1
            if (cReturnString.Length < iLenReturnString)
            {
                ilength = cReturnString.Length;
            }
            while (true)
            {
                int ilent = System.Text.ASCIIEncoding.Default.GetByteCount(cReturnString);	//此方法区分汉字，一个汉字算2
                if (ilent > iLenReturnString)
                {
                    ilength--;
                    cReturnString = cReturnString.Substring(0, ilength);
                }
                else
                {
                    break;
                }
            }
            if (bReturnDefaultString == true && cReturnString != cOriginalityString)
            {
                //返回的字符串与原始内容不同，且要求含有缺省字符串的内容。
                cReturnString += cReturnDefaultString;
            }
            return cReturnString;
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

        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string basePath = GetBasePath();
                if (basePath == "")
                {
                    return;
                }
                else
                {
                    Response.Redirect("TaskAdd.aspx?fileName=" + basePath);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('"+ex.Message.Replace("\r\n","").Replace("\"","").Replace("'","")+"');</script>", false);
                return;
            }
        }
        /// <summary>
        /// 生成 保存配置文件所需的文件夹
        /// </summary>
        /// <returns></returns>
        public string GetBasePath()
        {

            string str = "";
            if (Context.User.Identity.Name.Split(new char[] { '|' })[0] != "MDT2.0")
            {
                Response.Redirect("/Account/Login.aspx");
            }
            else
            {
                string userID = Context.User.Identity.Name.Split(new char[] { '|' })[1].ToString();
                DateTime dateTimeNow = DateTime.Now;
                string strDateTime = String.Format("{0:yyyy-MM-dd HH:mm:ss}",dateTimeNow);
                strDateTime = strDateTime.Replace(" ", "").Replace(":", "");

                string strDirectoryPath = "/ManageMent/UserFiles";
                string[] directorys = Directory.GetDirectories(Server.MapPath(strDirectoryPath));
                if (directorys.Length > 0)
                {
                    DateTime dtOld = new DateTime();
                    string dateTime = "";
                    foreach (var item in directorys)
                    {
                        string fileName = item.Substring(item.LastIndexOf("\\") + 1);
                        dateTime = fileName.Substring(1,10);
                        dtOld = Convert.ToDateTime(dateTime);
                        if (dtOld <= dateTimeNow.AddDays(-2))
                        {
                            Directory.Delete(Server.MapPath("/ManageMent/UserFiles/"+fileName),true);
                        }
                    }
                }

                Random random = new Random();
                int randomNum = random.Next(1, 9999);
                str = userID + strDateTime + randomNum;
                string basePath = "/Management/UserFiles/" + str;
                string folderPath = Server.MapPath(basePath);
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>目标路劲已经存在指定名称文件夹，新建文件夹失败！</script>", false);
                    return "";
                }
            }
            return str;
        }

        protected void lbtnModify_Click(object sender, EventArgs e)
        {
            try
            {
                if (hiddenSelectID.Value == "")
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('请选择要修改的行！');</script>", false);
                    return;
                }
                string basePath = GetBasePath();
                if (basePath == "")
                {
                    return;
                }
                else
                {
                    Response.Redirect("TaskAdd.aspx?Id=" + hiddenSelectID.Value + "&fileName=" + basePath);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace("\r\n", "").Replace("\"", "").Replace("'", "") + "');</script>", false);
                return;
            }
            
        }

        //protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        //{
        //    try
        //    {
        //        int index = Convert.ToInt32(ViewState["index"]);
        //        if (index < Repeater1.Items.Count)
        //        {
        //            Repeater repeater2 = (Repeater)Repeater1.Items[index].FindControl("Repeater2");
        //            ImageButton ibtnDisplay = (ImageButton)Repeater1.Items[index].FindControl("ibtnDisplay");
        //            ibtnDisplay.ImageUrl = "/images/iconAdd1.GIF";
        //            repeater2.Visible = false;
        //        }
        //        ViewState["index"] = e.Item.ItemIndex;
        //        ImageButton ibtn = (ImageButton)e.Item.FindControl("ibtnDisplay");
        //        Repeater rp = (Repeater)e.Item.FindControl("Repeater2");
        //        Label lbl = (Label)e.Item.FindControl("lblD");//1表示当前组的任务是处于显示状态，2表示当前组的任务处于隐藏状态
        //        if (lbl.Text == "1" || index != e.Item.ItemIndex)
        //        {
        //            ibtn.ImageUrl = "/images/iconAdd2.GIF";
        //            lbl.Text = "2";
        //            rp.Visible = true;
        //        }
        //        else
        //        {
        //            ibtn.ImageUrl = "/images/iconAdd1.GIF";
        //            lbl.Text = "1";
        //            rp.Visible = false;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('"+ex.Message.Replace("\r\n","").Replace("\"","").Replace("'","")+"');</script>", false);
        //        return;
        //    }
        //}
        /// <summary>
        /// //刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            BindTaskList();
        }
        
    }
}