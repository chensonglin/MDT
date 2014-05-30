using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Top.Api.Util;

namespace MDT.WebUI.Management.OAuth
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        private void TopOAuth()
        {
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                string url = "https://oauth.taobao.com/token";

                IDictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("grant_type", "authorization_code");
                dic.Add("code", txtCode.Text);
                dic.Add("client_id", txtAppKey.Text);
                dic.Add("client_secret", txtAppSecret.Text);
                dic.Add("redirect_uri", txtRedirectUrl.Text);
                WebUtils wu = new WebUtils();//WebUtils 平台.netSDK封装
                string backstr = wu.DoPost(url, dic);

                System.Collections.IDictionary bb = new Dictionary<string, string>();
                bb = TopUtils.ParseJson(backstr);
                string access_token = bb["access_token"].ToString();
                txtSessionKey.Text = access_token;
            }
            else
            {
                /*if (!string.IsNullOrEmpty(appkey))
                {
                    //关于回调地址：http://open.taobao.com/dev/index.php/%E8%8E%B7%E5%8F%96SessionKey
                    //关于用户验证：http://open.taobao.com/dev/index.php/%E7%94%A8%E6%88%B7%E9%AA%8C%E8%AF%81

                    //验证回调地址参数是否合法，如果合法并保存用户数据至Cookie
                    if (TopUtils.VerifyTopResponse(Request.QueryString["top_parameters"], Request.QueryString["top_session"], Request.QueryString["top_sign"], appkey, appsecret) == true)
                    {
                        //验证成功
                        Response.Write("验证成功！");
                        //Response.Redirect("Main.aspx");
                        GetUser(Request.QueryString["top_session"]);
                    }
                    else
                    {
                        //验证失败
                        Response.Write("无效验证！");
                    }
                }
                else
                {
                    Response.Write("无效参数对象，登录验证失败");
                }*/
            }
        }

        private void JosOAuth()
        {
            Session["JosAppKey"] = txtAppKey.Text;
            Session["JosAppSecret"] = txtAppSecret.Text;
            Session["JosRedirect"] = txtRedirectUrl.Text;
            string hrefStr = "http://auth.sandbox.360buy.com/oauth/authorize?response_type=code&client_id=" + txtAppKey.Text
                + "&redirect_uri=" + txtRedirectUrl.Text + "&state=myststeid&scope=read";
            Response.Redirect(hrefStr);
        }

        protected void lbtnSubmit_Click(object sender, EventArgs e)
        {
            switch (rbtPlatfrom.SelectedValue)
            {
                case "0001":
                    TopOAuth();
                    break;
                case "0004":
                    JosOAuth();
                    break;
            }

        }
    }
}