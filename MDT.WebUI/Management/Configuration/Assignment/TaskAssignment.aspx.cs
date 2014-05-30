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
using MDT.ManageCenter.ServiceContract;
using MDT.ManageCenter.ServiceImplement;

namespace MDT.WebUI.Management.Configuration.Assignment
{
    public partial class TaskAssignment : System.Web.UI.Page
    {
        //private IManageCenterService service;
        private ETaskDAL eTaskDAL;
        private EClientDAL eClientDAL;
        //private ETaskDAL2 eTaskDAL2;
        private ETaskAllocationDAL eTaskA;
        private int pageIndex;// 页码
        private int pageCount = 30;// 每页数量
        private int totalCount;// 总数量
        protected void Page_Load(object sender, EventArgs e)
        {
            eTaskDAL = new ETaskDAL();
            eClientDAL = new EClientDAL();
            eTaskA = new ETaskAllocationDAL();
            if (!IsPostBack)
            {
                if (Context.User.Identity.Name.Split(new char[] { '|' })[0] != "MDT2.0")
                {
                    Response.Redirect("/Account/Login.aspx");
                }
                else if (!Context.User.Identity.Name.Split(new char[] { '|' })[4].Contains('1'))
                {
                    aClientManager.Visible = false;
                    aTaskAlloat.Visible = false;
                }
                ViewState["num"] = "0";//设置序号的初始值
                ViewState["clientID"] = 0;
            }
            Session.Add("TaskA",this);
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script> $(function () {$('#dataTable').trOddHilight();$('#dataTable').trClick();})</script>", false);
        }
        /// <summary>
        /// 绑定任务数据
        /// </summary>
        private void ETaskDataBind()
        {
            //var tasks = eTaskDAL.GetTasks();
            //Repeater1.DataSource = tasks.OrderBy(p => p.TaskName);
            //Repeater1.DataBind();
            if (Repeater2.Items.Count > 0)
            {//默认显示第一个客户端的  任务
                try
                {
                    Label lblIDClient = (Label)this.Repeater2.Items[0].FindControl("lblID");
                    int intClient = Convert.ToInt32(lblIDClient.Text);
                    ViewState["clientID"] = intClient;
                    LinkButton lbtn = (LinkButton)Repeater2.Items[0].FindControl("lbtnClient");
                    lbtn.CssClass = "cflAclick";
                    AspNetPager1.CurrentPageIndex = 1;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message + "');</script>", false);
                    return;
                }
            }
        }
        private void BindData()
        {
            int count;
            int intClient = Convert.ToInt32(ViewState["clientID"]);
            try
            {
                //DataSet ds = eTaskDAL2.GetETaskByClientID(intClient);
                DataSet ds = eTaskA.GetETaskByClientID(intClient);
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
                    totalCount = 1;
                }
                lblCount.Text = count.ToString();
                lblCurrentPage.Text = pageIndex.ToString();
                lblPageCount.Text = totalCount.ToString();
                int startIndex = (pageIndex - 1) * pageCount;
                int endIndex = pageIndex * pageCount;
                //DataView dv = ds.Tables[0].DefaultView; // list.Skip((pageIndex - 1) * pageCount).Take(pageCount);
                DataTable dt = ds.Tables[0];
                DataTable drs = dt.Clone();
                if (endIndex > dt.Rows.Count)
                    endIndex = dt.Rows.Count;

                for (int i = startIndex; i < endIndex; i++)
                {
                    DataRow dr = drs.NewRow();
                    DataRow dr2 = dt.Rows[i];
                    foreach (DataColumn column in dt.Columns)
                    {
                        dr[column.ColumnName] = dr2[column.ColumnName];
                    }
                    drs.Rows.Add(dr);
                }
                this.Repeater1.DataSource = drs;
                this.Repeater1.DataBind();
                this.AspNetPager1.PageSize = pageCount;
                this.AspNetPager1.RecordCount = count;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('"+ex.Message.Replace('\'',' ')+"');</script>", false);
            }
        }
        /// <summary>
        /// 绑定客户端数据
        /// </summary>
        public void EClientBind()
        {
            List<EClient> eClients = eClientDAL.GetEClients();
            this.Repeater2.DataSource = eClients;
            this.Repeater2.DataBind();
        }

        protected void Repeater2_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            ViewState["num"] = "0";//设置序号的初始值
            for (int i = 0; i < Repeater2.Items.Count; i++)
			{
			    ((LinkButton)this.Repeater2.Items[i].FindControl("lbtnClient")).CssClass = "cflA";
			}
            LinkButton lbtn = (LinkButton)e.Item.FindControl("lbtnClient");
            if (lbtn != null)
            {
                try
                {
                    lbtn.CssClass = "cflAclick";
                    Label lblIDClient = (Label)e.Item.FindControl("lblID");
                    int intClient = Convert.ToInt32(lblIDClient.Text);
                    ViewState["clientID"] = intClient;
                    AspNetPager1.CurrentPageIndex = 1;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('"+ex.Message+"');</script>", false);
                    return;
                }
            }
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
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
            BindData();
        }

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

        protected void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            EClientBind();//绑定客户端数据
            ETaskDataBind();
            //this.AspNetPager1.CurrentPageIndex = 1;
        }
        /// <summary>
        /// 隐藏的刷新按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            EClientBind();//绑定客户端数据
            ETaskDataBind();
            //this.AspNetPager1.CurrentPageIndex = 1;
        }
    }

    public class TaskItem
    {
        public int TaskId { get; set; }
        public string TaskName { get; set; }
        public string Note { get; set; }
        public override string ToString()
        {
            return this.TaskName;
        }
    }

}