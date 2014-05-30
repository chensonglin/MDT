using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;
using System.Collections;

namespace MDT.WebUI.Management.Notice
{
    public partial class NoticeAllocate : System.Web.UI.Page
    {
        ENoticeReceiverDAL receiverDal = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }
            ViewState["tasks"] = "";
            ViewState["num"] = "0";//设置序号的初始值
            BindTask();
            BindData();
        }
        /// <summary>
        /// 查询并绑定任务信息
        /// </summary>
        private void BindTask()
        {
            try
            {
                ETaskDAL taskDAL = new ETaskDAL();
                var task = taskDAL.GetTasks().Where(p => p.Enable == true).OrderBy(p => p.TaskName);
                StringBuilder strTaskType = new StringBuilder();
                Dictionary<string, ArrayList> dic = new Dictionary<string, ArrayList>();
                string typeName = "";
                foreach (var item in task)
                {
                    if (item.Category == null)
                    {
                        typeName = "未设置类别";
                    }
                    else
                    {
                        typeName = item.Category;
                    }
                    if (dic.Keys.Contains(typeName))
                    {
                        TaskSimple ts = new TaskSimple();
                        ts.TaskId = item.ID;
                        ts.TaskName = item.TaskName;
                        ArrayList arrayListTask = dic[typeName];
                        arrayListTask.Add(ts);
                    }
                    else
                    {
                        strTaskType.Append("0*" + typeName + "|");
                        TaskSimple ts = new TaskSimple();
                        ts.TaskId = item.ID;
                        ts.TaskName = item.TaskName;
                        ArrayList arrayListTask = new ArrayList();
                        arrayListTask.Add(ts);
                        dic.Add(typeName, arrayListTask);
                    }
                }
                ViewState["taskName"] = dic;
                ETask etask = task.ToList()[0];
                string firstType = etask.Category;
                if (firstType == null)
                {
                    firstType = "未设置类别";
                }
                txtTaskType.Value = firstType;
                hideTaskType.Value = strTaskType.ToString().TrimEnd('|');
                Repeater2.DataSource = dic[firstType];
                Repeater2.DataBind();
                ViewState["tasks"] = dic;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "") + "');</script>");
                return;
            }
        }
        /// <summary>
        /// 查询并绑定通知人员信息
        /// </summary>
        private void BindData()
        {
            try
            {
                receiverDal = new ENoticeReceiverDAL();
                var receivers = receiverDal.GetNoticeReceiver();
                Repeater1.DataSource = receivers;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                Repeater1.DataSource = null;  //清空列表
                Repeater1.DataBind();
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "") + "');</script>");
                return;
            }
        }
        /// <summary>
        /// 选择任务类别后触发的事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnTaskTypeSelect_Click(object sender, EventArgs e)
        {
            try
            {
                Dictionary<string, ArrayList> dic = (Dictionary<string, ArrayList>)ViewState["taskName"];
                string type = txtTaskType.Value.Trim();
                ArrayList listTaskSimple = dic[type];
                Repeater2.DataSource = listTaskSimple;
                Repeater2.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "") + "');</script>");
                return;
            }
        }
        /// <summary>
        /// 预警通知人员的数据绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblEnable = (Label)e.Item.FindControl("lblEnable");
                if (lblEnable.Text == "停用")
                {
                    CheckBox ck = (CheckBox)e.Item.FindControl("ckReceiverEmail");
                    ck.Enabled = false;
                    ck.ToolTip = "人员已停用不能设置预警通知";
                    ck = (CheckBox)e.Item.FindControl("ckReceiverPhone");
                    ck.ToolTip = "人员已停用不能设置预警通知";
                    ck.Enabled = false;
                }
                else
                {
                    Label lblPhone = (Label)e.Item.FindControl("lblPhone");
                    Label lblEmail = (Label)e.Item.FindControl("lblEmail");
                    if (lblEmail.Text == "")
                    {
                        CheckBox ck = (CheckBox)e.Item.FindControl("ckReceiverEmail");
                        ck.ToolTip = "人员邮件地址信息不全不能设置邮件通知";
                        ck.Enabled = false;
                    }
                    if (lblPhone.Text == "")
	                {
                        CheckBox ck = (CheckBox)e.Item.FindControl("ckReceiverPhone");
                        ck.ToolTip = "人员手机号码信息不全不能设置短信通知";
                        ck.Enabled = false;
	                }
                }
                int num = Convert.ToInt32(ViewState["num"]);
                num++;
                Label lblNum = (Label)e.Item.FindControl("LblNum");
                if (lblNum != null)
                {
                    lblNum.Text = num.ToString();
                }
                ViewState["num"] = num;
            }
        }
        public string FormatString(string str,int length)
        {
            string strReturn = str;
            if (str.Length > length)
            {
                strReturn = str.Substring(0, length)+"...";
            }
            return strReturn;
        }
        /// <summary>
        /// 刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            ViewState["num"] = 0;
            BindData();
        }
        /// <summary>
        /// 删除预警通知人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDelReceiver_Click(object sender, EventArgs e)
        {
            try
            {
                int uid = Convert.ToInt32(hiddenUid.Value);
                ENoticeReceiverDAL dal = new ENoticeReceiverDAL();
                dal.DeleteObject(uid);
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('删除成功！');RefreshWindows();</script>");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "") + "');</script>");
            }
        }
        /// <summary>
        /// 获取选中的任务id
        /// </summary>
        /// <param name="listTaskId">用于保存选中任务的id</param>
        public void GetCheckedTask(ArrayList listTaskId)
        {
            for (int i = 0; i < Repeater2.Items.Count; i++)
            {
                CheckBox ck = (CheckBox)Repeater2.Items[i].FindControl("ckTask");
                if (ck.Checked)
                {
                    Label lblTaskId = (Label)Repeater2.Items[i].FindControl("lblTaskId");
                    listTaskId.Add(lblTaskId.Text.Trim());
                }
            }
        }
        /// <summary>
        /// 获取 邮件通知人员和短信通知人员
        /// </summary>
        /// <param name="listReceiverEmail">保存邮件通知人员</param>
        /// <param name="listReceiverPhone">保存短信通知人员</param>
        public void GetCheckedReceiver(ArrayList listReceiverEmail, ArrayList listReceiverPhone)
        {
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem ri = Repeater1.Items[i];
                CheckBox ckEmail = (CheckBox)ri.FindControl("ckReceiverEmail");
                CheckBox ckPhone = (CheckBox)ri.FindControl("ckReceiverPhone");
                if (ckEmail.Checked)//选择设置邮件通知
                {
                    Label lblReceiverId = (Label)ri.FindControl("lblID");
                    listReceiverEmail.Add(lblReceiverId.Text);
                }
                if (ckPhone.Checked)//选择设置短信通知
                {
                    Label lblReceiverId = (Label)ri.FindControl("lblID");
                    listReceiverPhone.Add(lblReceiverId.Text);
                }
            }
        }
        /// <summary>
        /// 批量设置预警通知人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnNotice_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList listTaskId = new ArrayList();
                GetCheckedTask(listTaskId);//获取需要设置预警的任务id集合。
                if (listTaskId.Count == 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请选择任务');</script>");
                    return;
                }
                ArrayList listReceiverEmail = new ArrayList();
                ArrayList listReceiverPhone = new ArrayList();
                GetCheckedReceiver(listReceiverEmail, listReceiverPhone);//获取需要设置预警通知的人员
                if (listReceiverEmail.Count == 0 && listReceiverPhone.Count == 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请选择预警通知人员');</script>");
                    return;
                }
                ENoticeServiceDAL dal = new ENoticeServiceDAL();
                dal.NoticeAllocate(listTaskId, listReceiverEmail, listReceiverPhone);
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('设置预警通知成功！');cancelChecked();</script>");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('"+ex.Message.Replace("\"","").Replace("'","").Replace("\r\n","")+"');</script>");
            }
        }
        /// <summary>
        /// 批量取消预警通知人员
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            try
            {
                ArrayList listTaskId = new ArrayList();
                GetCheckedTask(listTaskId);//获取需要设置预警的任务id集合。
                if (listTaskId.Count == 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请选择任务');</script>");
                    return;
                }
                ArrayList listReceiverEmail = new ArrayList();
                ArrayList listReceiverPhone = new ArrayList();
                GetCheckedReceiver(listReceiverEmail, listReceiverPhone);//获取预警通知的人员
                if (listReceiverEmail.Count == 0 && listReceiverPhone.Count == 0)
	            {
		            ClientScript.RegisterStartupScript(typeof(Page),"","<script>alert('请选择预警通知人员');</script>");
                    return;
	            }
                ENoticeServiceDAL dal = new ENoticeServiceDAL();
                dal.CancelAllocate(listTaskId, listReceiverEmail, listReceiverPhone);
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('取消预警通知成功！');cancelChecked();</script>");
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message.Replace("\"", "").Replace("'", "").Replace("\r\n", "") + "');</script>");
            }
        }
        /// <summary>
        /// 单个设置或取消预警通知
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void Repeater1_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                ArrayList listTaskId = new ArrayList();
                GetCheckedTask(listTaskId);
                if (listTaskId.Count == 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请选择任务');</script>");
                    return;
                }
                ArrayList listReceiverEmail = new ArrayList();
                ArrayList listReceiverPhone = new ArrayList();
                CheckBox ckEmail = (CheckBox)e.Item.FindControl("ckReceiverEmail");
                CheckBox ckPhone = (CheckBox)e.Item.FindControl("ckReceiverPhone");
                Label lblReceiverId = (Label)e.Item.FindControl("lblID");
                if (ckEmail.Checked)
                {
                    listReceiverEmail.Add(lblReceiverId.Text);
                }
                if (ckPhone.Checked)
                {
                    listReceiverPhone.Add(lblReceiverId.Text);
                }
                if (listReceiverEmail.Count == 0 && listReceiverPhone.Count == 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请选择预警方式！');</script>");
                    return;
                }
                ENoticeServiceDAL dal = new
                    ENoticeServiceDAL();
                if (e.CommandName == "Notice")
                {
                    dal.NoticeAllocate(listTaskId, listReceiverEmail, listReceiverPhone);
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('设置预警通知成功！');cancelChecked();</script>");
                }
                else if (e.CommandName == "Cancel")
                {
                    dal.CancelAllocate(listTaskId, listReceiverEmail, listReceiverPhone);
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('取消预警通知成功！');cancelChecked();</script>");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page),"","<script>alert('"+ex.Message.Replace("\"","").Replace("'","").Replace("\r\n","")+"');</script>");
            }
        }
    }
    /// <summary>
    /// 新建一个类
    /// </summary>
    [Serializable]
    public class TaskSimple{
        private int taskId;

        public int TaskId
        {
            get { return taskId; }
            set { taskId = value; }
        }
        private string taskName;

        public string TaskName
        {
            get { return taskName; }
            set { taskName = value; }
        }
    }
}