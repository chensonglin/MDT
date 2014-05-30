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

namespace MDT.WebUI.Management.ErrorWarn
{
    public partial class ErrorWarnList : System.Web.UI.Page
    {
        private int pageIndex;// 页码
        private int pageCount = 30;// 每页数量
        private int totalCount;// 总数量
        ENoticeReceiverDAL receiverDal = null;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }
            ViewState["num"] = "0";//设置序号的初始值
            if (Request.QueryString["taskId"] == null || Request.QueryString["taskId"].ToString() == "")
            {


            }
            else
            {
                int taskId = Convert.ToInt32(Request.QueryString["taskId"].ToString());
                ViewState["taskId"] = taskId;
            }
            BindData();
        }
        private void BindData()
        {
            try
            {
                int taskId = Convert.ToInt32(ViewState["taskId"]);
                receiverDal = new ENoticeReceiverDAL();
                DataSet ds = null;
                ds = receiverDal.GetNoticeReceiver(taskId);
                Repeater1.DataSource = ds;
                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('"+ex.Message.Replace("\"","").Replace("'","").Replace("\r\n","")+"');</script>");
                return;
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
    }
}