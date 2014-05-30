using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;
using System.Collections;


namespace MDT.WebUI.Management.TaskMove
{
    public partial class TaskMove : System.Web.UI.Page
    {
        private ETaskDAL taskDAL;
        private List<ETask> taskList;
        private List<EDatabase> dbList;
        private int pageIndex;// 页码
        private int pageCount = 30;// 每页数量
        private int totalCount;// 总数量
        protected void Page_Load(object sender, EventArgs e)
        {
            taskDAL = new ETaskDAL();
            if (!IsPostBack)
            {
                ViewState["num"] = "0";//设置序号的初始值
                ViewState["taskNames"] = "";
                InitTable();
            }
        }
        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void InitTable()
        {
            int count;
            int skipCount;
            try
            {
                var tasks = taskDAL.GetTasks().OrderBy(p => p.TaskName);
                taskList = tasks.ToList();

                if (AspNetPager1.CurrentPageIndex != 1)
                    pageIndex = AspNetPager1.CurrentPageIndex;
                else
                    pageIndex = 1;

                // 设置总页数
                count = taskList.Count();
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
                lblCurrent.Text = pageIndex.ToString();
                lblCountPage.Text = totalCount.ToString();
                skipCount = (pageIndex == 1) ? 0 : ((pageIndex - 1) * pageCount);

                var logsData = taskList.Skip(skipCount).Take(pageCount);

                this.Repeater1.DataSource = logsData;
                this.Repeater1.DataBind();
                this.AspNetPager1.PageSize = pageCount;
                this.AspNetPager1.RecordCount = count;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page),"","<script>alert('"+ex.Message.Replace("'","").Replace("\"","")+"');</script>");
                return;
            }
        }

        protected void lbtnMove_Click(object sender, EventArgs e)
        {
            try 
	        {
                int dbId = Convert.ToInt32(txtDataBaseID.Value);
                EDatabaseDAL dbDAL = new EDatabaseDAL();
                EDatabase dataBase = dbDAL.GetDatabase(dbId);//查询数据库列表

                string connStr = "server=" + dataBase.Server + ";Database=" + dataBase.Database + ";uid=" + dataBase.UserId + ";pwd=" + dataBase.Password + ";Connect Timeout=60";

                ArrayList listId = new ArrayList();
                for (int i = 0; i < Repeater1.Items.Count; i++)
                {
                    RepeaterItem item = Repeater1.Items[i];
                    CheckBox ck = (CheckBox)item.FindControl("CheckBox1");
                    Label lbl = (Label)item.FindControl("lblID");
                    if (ck.Checked)
                    {
                        listId.Add(lbl.Text);
                    }
                }
                if (listId.Count == 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请选择任务！');</script>");
                    return;
                }
                taskDAL.TaskMove(connStr, listId);
	        }
	        catch (Exception ex)
	        {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('任务迁移失败！"+ex.Message.Replace("'","").Replace("\"","")+"');</script>");
                return;
	        }
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('任务迁移成功！');</script>");
        }
        
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Label lblTaskName = (Label)e.Item.FindControl("lblTaskName");
                string taskNames = ViewState["taskNames"].ToString();
                if (taskNames.IndexOf(lblTaskName.Text) > -1)
                {
                    ((CheckBox)e.Item.FindControl("CheckBox1")).Checked = true;
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
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            ViewState["num"] = (AspNetPager1.CurrentPageIndex - 1) * pageCount;
            this.InitTable();
        }

        protected void btnSearchByPage_Click(object sender, EventArgs e)
        {
            if (txtPageForSearch.Text.Trim() == "")
            {
                ClientScript.RegisterStartupScript( typeof(Page), "", "<script>alert('请输入要查询的页次');</script>");
                txtPageForSearch.Focus();
            }
            else if (!IsPlusInt(txtPageForSearch.Text.Trim()))
            {
                txtPageForSearch.Text = "";
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('查询的页次必须为大于0的正整数！');</script>");
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
                strReturn = String.Format("{0:yyyy-MM-dd}", dt);
                return strReturn;
            }
            catch (Exception)
            {
                return "";
            }
        }
        
    }
}