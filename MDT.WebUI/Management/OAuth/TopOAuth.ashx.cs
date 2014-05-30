using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Top.Api.Util;

namespace MDT.WebUI.Management.OAuth
{
    /// <summary>
    /// TopOAuth 的摘要说明
    /// </summary>
    public class TopOAuth : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            string appkey = context.Request["appkey"];
            string appsecret = context.Request["appsecret"];
            string redirecturl = context.Request["redirecturl"];
            string code = context.Request["code"];

            if (!string.IsNullOrEmpty(code))
            {
                string url = "https://oauth.taobao.com/token";

                IDictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("grant_type", "authorization_code");
                dic.Add("code", code);
                dic.Add("client_id", appkey);
                dic.Add("client_secret", appsecret);
                dic.Add("redirect_uri", redirecturl);
                WebUtils wu = new WebUtils();//WebUtils 平台.netSDK封装
                string backstr = wu.DoPost(url, dic);
                
                System.Collections.IDictionary bb = new Dictionary<string, string>();
                bb = TopUtils.ParseJson(backstr);
                string access_token = bb["access_token"].ToString();
                context.Response.Write(access_token);

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

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}