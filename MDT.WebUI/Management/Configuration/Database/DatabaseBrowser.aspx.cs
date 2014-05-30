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

namespace MDT.WebUI.Management.Configuration.Database
{
    public partial class DatabaseBrowser : System.Web.UI.Page
    {
        private List<EDatabase> list;
        private EDatabaseDAL databaseDAL;
        private EDatabase dataBase;
        private int pageIndex;// 页码
        private int pageCount = 30;// 每页数量
        private int totalCount;// 总数量

        protected void Page_Load(object sender, EventArgs e)
        {
            list = new List<EDatabase>();
            databaseDAL = new EDatabaseDAL();

            if (!IsPostBack)
            {
                if (Context.User.Identity.Name.Split(new char[] { '|' })[0] != "MDT2.0")
                {
                    Response.Redirect("/Account/Login.aspx");
                }
                else if (!Context.User.Identity.Name.Split(new char[] { '|' })[4].Contains('1'))
                {
                    lbtnAdd.Visible = false;
                    lbtnModify.Visible = false;
                    LinkButton2.Visible = false;
                }
                ViewState["num"] = "0";//设置序号的初始值
            }  
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script> $(function () {$('#dataTable').trOddHilight();$('#dataTable').trClick();})</script>", false);
        }

        /// <summary>
        /// 绑定数据源
        /// </summary>
        private void InitTable()
        {
            int count;
            int skipCount;

            list.Clear();
            list.AddRange(databaseDAL.GetDatabases());
            if (AspNetPager1.CurrentPageIndex != 1)
                pageIndex = AspNetPager1.CurrentPageIndex;
            else
                pageIndex = 1;

            // 设置总页数
            count = list.Count();
            totalCount = count / pageCount;
            if ((count % pageCount) != 0)
            {
                totalCount++;
            }
            lblCount.Text = count.ToString();
            lblCurrent.Text = pageIndex.ToString();
            lblCountPage.Text = totalCount.ToString();
            skipCount = (pageIndex == 1) ? 0 : ((pageIndex - 1) * pageCount);


            var logsData = databaseDAL.GetDatabases().OrderByDescending(p => p.ID).Skip(skipCount).Take(pageCount);
            this.Repeater1.DataSource = logsData;
            this.Repeater1.DataBind();

            this.AspNetPager1.PageSize = pageCount;
            this.AspNetPager1.RecordCount = count;
        }

        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            ViewState["num"] = (AspNetPager1.CurrentPageIndex - 1) * pageCount;
            this.InitTable();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(this.hiddenSelectID.Value);
                dataBase =  databaseDAL.GetDatabase(id);

                databaseDAL.DeleteObject(dataBase);

                this.InitTable();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('"+ex.Message.Replace("'","").Replace("\"","")+"');</script>");
                return;
            }
        }


        protected void LinkButtonRefresh_Click(object sender, EventArgs e)
        {
            ViewState["num"] = 0;
            this.InitTable();
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
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('查询的页次必须为大于0的正整数！');</script>", false);
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
            timer1.Enabled = false;
            InitTable();
        }
    }
}