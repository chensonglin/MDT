using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.ServiceImplement;
using MDT.ManageCenter.ServiceContract;

namespace MDT.WebUI.Management.Configuration.Assignment
{
    public partial class AllotTask : System.Web.UI.Page
    {
        ETaskAllocationDAL eTaskA;
        EClientDAL eClientDAL;
        private IManageCenterService service;
        protected void Page_Load(object sender, EventArgs e)
        {
            eTaskA = new ETaskAllocationDAL();
            eClientDAL = new EClientDAL();
            service = new ManageCenterService();
            if (!Page.IsPostBack)
            {
                if (Context.User.Identity.Name.Split(new char[] { '|' })[0] != "MDT2.0")
                {
                    Response.Redirect("/Account/Login.aspx");
                }
                ViewState["num"] = "0";//设置序号的初始值
                BindClient();
               // BindTask();
            }
            ScriptManager.RegisterStartupScript(UpdatePanel1, typeof(UpdatePanel), "onload", "<script> $(function () {txtNameChange();});colunmWidth();init();</script>", false);
        }
        /// <summary>
        /// 查询客户端信息
        /// </summary>
        private void BindClient()
        {
            try
            {
                List<EClient> eClients = eClientDAL.GetEClients();
                if (eClients == null || eClients.Count == 0)
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('对不起，没有查询到客户端数据！');</script>", false);
                    return;
                }
                txtName.Value = eClients[0].Name;
                txtID.Value = eClients[0].ID.ToString();
                StringBuilder clients = new StringBuilder();
                for (int i = 0; i < eClients.Count; i++)
                {
                    clients.Append(eClients[i].ID + "*" + eClients[i].Name + "|");
                }
                hiddenStr.Value = clients.ToString().TrimEnd('|');
            }
            catch (Exception ex)
            {
                string errorMsg = ex.Message.Replace('\'',' ');
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + errorMsg + "');</script>", false);
            }
        }
        /// <summary>
        /// 绑定任务分配信息
        /// </summary>
        private void BindTask()
        {
            try
            {
                DataSet ds = eTaskA.GetETaskAllocation();
                this.Repeater1.DataSource = ds.Tables[0];
                this.Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace('\'', ' ') + "');</script>", false);
            }
        }

        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int clientId = Convert.ToInt32(txtID.Value);
                List<int> ids = new List<int>();
                for (int i = 0; i < this.Repeater1.Items.Count; i++)
                {
                    CheckBox oneCheckBox = (CheckBox)Repeater1.Items[i].FindControl("cb");
                    if (oneCheckBox != null && oneCheckBox.Checked)
                    {
                        Label lblID = (Label)Repeater1.Items[i].FindControl("lblID");
                        ids.Add(int.Parse(lblID.Text));
                    }
                }
                service.AllocateTask(clientId, ids.ToArray());
                ViewState["num"] = "0";//设置序号的初始值
                BindTask();
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('保存成功！');</script>", false);
                return;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, typeof(UpdatePanel), "onload", "<script>alert('" + ex.Message.Replace('\'', ' ') + "');</script>", false);
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

        protected void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            BindTask();
        }

        protected void lbtnRefresh_Click(object sender, EventArgs e)
        {
            ViewState["num"] = "0";//设置序号的初始值
            BindTask();
        }
    }
}