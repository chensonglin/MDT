using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MDT.ManageCenter.DAL;

namespace MDT.WebUI.Management.Configuration.Assignment
{
    public partial class ClientManage : System.Web.UI.Page
    {
        private EClientDAL eClientDAL;
        protected void Page_Load(object sender, EventArgs e)
        {
            eClientDAL = new EClientDAL();
            if (!IsPostBack)
            {
                search.Style.Add("display","none");
                ViewState["clientID"] = 0;
                EClientBind();//绑定客户端数据
                ViewState["AddOrModify"] = "Add";
            }
        }

        /// <summary>
        /// 绑定客户端数据
        /// </summary>
        private void EClientBind()
        {
            try
            {
                List<EClient> eClients = eClientDAL.GetEClients();
                this.Repeater1.DataSource = eClients;
                this.Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('" + ex.Message.Replace('\'', ' ') + "');</script>");
            }
        }
        /// <summary>
        /// 添加按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnAdd_Click(object sender, EventArgs e)
        {
            search.Style.Add("display","block");
            txtName.Text = "";
            txtIP.Text = "";
            hiddenSelectID.Value = "";
            ViewState["AddOrModify"] = "Add";
            hiddenSelectRow.Value = "";
        }
        /// <summary>
        /// 修改按钮事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnModify_Click(object sender, EventArgs e)
        {
            if (hiddenSelectID.Value.Trim()=="")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请选择需要修改的客户端！');</script>");
                return;
            }
            search.Style.Add("display","block");
            ViewState["AddOrModify"] = "Modify";
            int id = Convert.ToInt32(hiddenSelectID.Value);
            EClient eClient = eClientDAL.GetEClientByID(id);
            if (eClient != null)
            {
                txtName.Text = eClient.Name;
                txtIP.Text = eClient.ServerIP;
            }
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>MarkTrClick();</script>");

        }
        /// <summary>
        /// 删除选中客户端
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnDelete_Click(object sender, EventArgs e)
        {
            search.Style.Add("display","none");
            if (hiddenSelectID.Value.Trim() == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('请选择需要修改的客户端！');</script>");
                return;
            }
            int id = Convert.ToInt32(hiddenSelectID.Value);
            EClient eClient = eClientDAL.GetEClientByID(id);
            try
            {
                eClientDAL.DeleteObject(eClient);
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('删除成功！');</script>");
                EClientBind();
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>MarkTrClick();alert('删除失败！" + ex.Message.Replace('"', ' ') + "');</script>");
            }
        }
        /// <summary>
        /// 保存客户端编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnSave_Click(object sender, EventArgs e)
        {
            string name = txtName.Text.Trim();
            string ip = txtIP.Text.Trim();
            if (name == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>MarkTrClick();alert('请输入客户端名称！');</script>");
                return;
            }
            if (ip == "")
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>MarkTrClick();alert('请输入客户端IP！');</script>");
                return;
            }
            try
            {
                int id = 0;
                if (ViewState["AddOrModify"].ToString() != "Add" && hiddenSelectID.Value != "")
                {
                    id = Convert.ToInt32(hiddenSelectID.Value);
                }
                List<EClient> eClients = eClientDAL.GetEClientByName(name, id);
                if (eClients != null && eClients.Count > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>MarkTrClick();alert('对不起，该客户端名称已经存在请从新输入！');</script>");
                    return;
                }
                eClients = eClientDAL.GetEClientByServerIP(ip, id);
                if (eClients != null && eClients.Count > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>MarkTrClick();alert('对不起，该客户端IP已经存在请重新输入！');</script>");
                    return;
                }
                if (ViewState["AddOrModify"].ToString() == "Add")
                {
                    EClient client = new EClient();
                    client.Name = name;
                    client.ServerIP = ip;
                    var l = eClientDAL.AddObject(client);
                }
                else
                {
                    eClientDAL.ModifyObject(id, name, ip);
                }
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>MarkTrClick();alert('保存成功！');</script>");
                EClientBind();
               
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('保存失败！" + ex.Message.Replace('"', ' ') + "');</script>");
                return;
            }
            search.Style.Add("display","none");
            hiddenSelectID.Value = "";//把选中的客户端的id号清空
        }
        /// <summary>
        /// 取消客户端编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            hiddenSelectID.Value = "";
            hiddenSelectRow.Value = "";
            ClientScript.RegisterStartupScript(typeof(Page), "", "<script>Exit();</script>");
        }
    }
}