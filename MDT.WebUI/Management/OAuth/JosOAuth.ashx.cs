using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MDT.WebUI.Management.OAuth
{
    /// <summary>
    /// JosOAuth1 的摘要说明
    /// </summary>
    public class JosOAuthAshx : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            Dictionary<string, string> parameters = new Dictionary<string,string>();
            parameters.Add("grant_type",context.Request["grant_type"]);
            parameters.Add("client_id", context.Request["client_id"]);
            parameters.Add("client_secret", context.Request["client_secret"]);
            parameters.Add("code", context.Request["code"]);
            parameters.Add("redirect_uri", context.Request["redirect_uri"]);
            parameters.Add("state", context.Request["state"]);

            string rspJson = Utils.DoPost("http://auth.360buy.com/oauth/token", parameters);
            context.Response.Write(rspJson);
            context.Response.End();
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