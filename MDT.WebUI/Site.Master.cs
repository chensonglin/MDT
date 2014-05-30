using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Security;


namespace MDT.WebUI
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ViewState["userName"] = "";
            if (Context.User.Identity.Name.Split(new char[] { '|' })[0] != "MDT2.0")
            {
                Response.Redirect("/Account/Login.aspx");
            }
            else
            {
                string userName = Context.User.Identity.Name.Split(new char[] { '|' })[2].ToString();
                ViewState["userName"] = userName;
            }
        }

        protected void lbtnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("/Account/Login.aspx");
        }
    }
}
