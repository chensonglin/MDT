﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;

namespace MDT.WebUI.Management.Notice
{
    public partial class AddReceiver : System.Web.UI.Page
    {
        ENoticeReceiverDAL receiverDal = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            receiverDal = new ENoticeReceiverDAL();
            if (Page.IsPostBack)
            {
                return;
            }
            ViewState["uid"] = "";
            if (Request.QueryString["uid"] != null && Request.QueryString["uid"].ToString() != "")
            {
                string uid = Request["uid"].ToString().Trim();
                ViewState["uid"] = uid;
                BindData();
            }
        }                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 
        private void BindData()
        {
            int uid = Convert.ToInt32(ViewState["uid"].ToString());
            var receiver = receiverDal.GetReceiverByUid(uid);
            txtName.Text = receiver.name;
            txtPhone.Text = receiver.phone;
            txtEmail.Text = receiver.email;
            txtRemark.Text = receiver.remark;
            if (receiver.enable == 1)
            {
                RdioYes.Checked = true;
            }
            else
            {
                RdioNo.Checked = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                int uid = 0;
                if (ViewState["uid"].ToString() != "")
                {
                    uid = Convert.ToInt32(ViewState["uid"]);
                }
                string name = txtName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();
                string remark = txtRemark.Text.Trim();
                int enable = 1;
                if (RdioNo.Checked)
                {
                    enable = 0;
                }
                var receivers = receiverDal.GetReceiverByName(uid, name, remark);
                if (receivers.Count > 0)
                {
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('该用户已经存在，请修改用户名或编辑备注以区分同名用户');</script>");
                    return;
                }
                NoticeReceiver receiver = new NoticeReceiver();
                receiver.uid = uid;
                receiver.name = name;
                receiver.phone = phone;
                receiver.email = email;
                receiver.remark = remark;
                receiver.enable = enable;
                if (uid == 0)  //新增
                {
                    receiverDal.AddObject(receiver);
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('新增成功');parent.RefreshWindows();parent.closeDiv2('hideReceiver','iframeReceiver')</script>");
                }
                else
                {
                    receiverDal.ModifyObject(receiver);
                    ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('修改成功');parent.RefreshWindows();parent.closeDiv2('hideReceiver','iframeReceiver')</script>");
                }
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(typeof(Page),"","<script>alert('"+ex.Message.Replace("\"","").Replace("'","").Replace("\r\n","")+"');</script>");
                return;
            }
        }
    }
}