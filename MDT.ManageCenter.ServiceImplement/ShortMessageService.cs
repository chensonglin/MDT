using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Net;
using System.IO;
using System.Web;
using MDT.ManageCenter.ServiceContract;

namespace MDT.ManageCenter.ServiceImplement
{
    public class ShortMessageService : IShortMessageService
    {
        public void Send(string toAddress, string message)
        {
            string mobiles = toAddress;
            string url = "http://s.ccme.cc/qxt/send.jsp";
            Hashtable pars = new Hashtable();
            pars["circle"] = "wuzhouzaixian";
            pars["pwd"] = "wuzhouzaixian";
            pars["mobile "] = mobiles;
            pars["service"] = "ff80808124e9f55c0124eb3f37100770";
            pars["mtype"] = "XXXF";
            pars["linkid"] = "S1295747";
            pars["msgid"] = "3956724";
            pars["message"] = message;

            try
            {
                string str_Post = PostWebService(url, pars).ToString();

                if (str_Post.Trim().Equals("0"))
                {
                    // 成功
                }
                else
                {
                    // 失败   
                }
            }
            catch { }
        }

        private string PostWebService(String URL, Hashtable Pars)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(URL);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Credentials = CredentialCache.DefaultCredentials;
            request.Timeout = 20000;//操作超时时间控制

            String str_ParsDecode = HttpUtility.UrlDecode(ParsToString(Pars).ToString());
            Encoding encode = Encoding.GetEncoding("gb2312");
            byte[] data = encode.GetBytes(str_ParsDecode);
            request.ContentLength = data.Length;

            Stream writer = request.GetRequestStream();
            writer.Write(data, 0, data.Length);
            writer.Close();

            WebResponse myWebResponse = request.GetResponse();
            Stream ReceiveStream = myWebResponse.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, encode);
            String str_retXml = sr.ReadToEnd();

            return str_retXml.Trim();
        }

        private string ParsToString(Hashtable Pars)
        {
            StringBuilder sb = new StringBuilder();

            foreach (string k in Pars.Keys)
            {
                if (sb.Length > 0)
                {
                    sb.Append("&");
                }
                sb.Append(String.Format("{0}={1}", HttpUtility.UrlEncode(k), HttpUtility.UrlEncode(Pars[k].ToString())));
            }
            return sb.ToString();
        }
    }
}
