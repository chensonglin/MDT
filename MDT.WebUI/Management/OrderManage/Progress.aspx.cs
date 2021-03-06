﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MDT.WebUI.Management.OrderManage
{
    public partial class Progress : System.Web.UI.Page
    {
        private string state = "";
        private string isComplete = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["State"] != null && Session["isComplete"] != null)
            {
                state = Session["State"].ToString();
                isComplete = Session["isComplete"].ToString();
            }
            else
            {
                Session["State"] = "";
            }
            if (state != "" && isComplete != "true")
            {
                lblWait.Text = state;
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>setTimeout('document.getElementById(\"Form1\").submit()',500);</script>");
            }
            else if (isComplete == "true")
            {
                lblWait.Text = state;
                ClientScript.RegisterStartupScript(typeof(Page), "", "<script>alert('订单同步完毕," + state + "');parent.closeDiv2('hideProgress','iframeProgress');</script>");
            }
        }
    }
}