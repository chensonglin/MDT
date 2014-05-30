using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MDT.WebUI.Management.OAuth
{
    public partial class JosOAuth : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string code = Request["code"];
            string state = Request["state"];

            if (!string.IsNullOrEmpty(code))
            {
                HttpCookie josCode = new HttpCookie("JosCode", code);
                HttpCookie josState = new HttpCookie("JosState", state);

                Response.Cookies.Add(josCode);
                Response.Cookies.Add(josState);
            }

            //string appKey = context.Session["JosAppKey"].ToString();
            //string appSecret = context.Session["JosAppSecret"].ToString();
            //string josRedirect = context.Session["JosRedirect"].ToString();

            //string hrefStr = "http://auth.360buy.com/oauth/token?grant_type=authorization_code&client_id=" + appKey
            //    + "&redirect_uri=" + josRedirect + "&code=" + code + "&state=" + state + "&client_secret=" + appSecret;

            //Dictionary<string, string> pram = new Dictionary<string,string>();

            //pram.Add("grant_type", "authorization_code");
            //pram.Add("client_id", appKey);
            //pram.Add("redirect_uri", josRedirect);
            //pram.Add("code", code);
            //pram.Add("state", state);
            //pram.Add("client_secret", appSecret);

            //string refStr = Utils.DoGet("http://auth.sandbox.360buy.com/oauth/token", pram);
        }
    }
}