using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Text;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace MDT.WebUI
{
    /// <summary>
    /// �������һЩ��̬�������������ط�ʹ��
    /// </summary>
    public class Utils
    {
        public Utils()
        {
        }

        private static HttpStatusCode _statusCode;

        /// <summary>
        /// ���ַ�����ʽ����HTML����
        /// </summary>
        /// <param name="str">Ҫ��ʽ�����ַ���</param>
        /// <returns>��ʽ������ַ���</returns>
        public static String ToHtml(string str)
        {
            if (str == null || str.Equals(""))
            {
                return str;
            }

            StringBuilder sb = new StringBuilder(str);
            sb.Replace("&", "&amp;");
            sb.Replace("<", "&lt;");
            sb.Replace(">", "&gt;");
            sb.Replace("\r\n", "<br>");
            sb.Replace("\n", "<br>");
            sb.Replace("\t", " ");
            sb.Replace(" ", "&nbsp;");
            return sb.ToString();
        }

        /// <summary>
        /// ��HTML����ת�����ı���ʽ
        /// </summary>
        /// <param name="str">Ҫ��ʽ�����ַ���</param>
        /// <returns>��ʽ������ַ���</returns>
        public static String ToTxt(String str)
        {
            if (str == null || str.Equals(""))
            {
                return str;
            }

            StringBuilder sb = new StringBuilder(str);
            sb.Replace("&nbsp;", " ");
            sb.Replace("<br>", "\r\n");
            sb.Replace("&lt;", "<");
            sb.Replace("&gt;", ">");
            sb.Replace("&amp;", "&");
            return sb.ToString();
        }

        /// <summary>
        /// ����Ӧ��ת��Ϊ�ı���
        /// </summary>
        /// <param name="rsp">��Ӧ������</param>
        /// <param name="encoding">���뷽ʽ</param>
        /// <returns>��Ӧ�ı�</returns>
        public static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            StringBuilder result = new StringBuilder();
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // ���ַ����ķ�ʽ��ȡHTTP��Ӧ
                stream = rsp.GetResponseStream();

                reader = new StreamReader(stream, encoding);

                // ÿ�ζ�ȡ������512���ַ�����д���ַ���
                char[] buffer = new char[512];
                int readBytes = 0;
                while ((readBytes = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    result.Append(buffer, 0, readBytes);
                }

            }
            finally
            {
                // �ͷ���Դ
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }

            return result.ToString();
        }

        /// <summary>
        /// ִ��HTTP POST����
        /// </summary>
        /// <param name="url">�����ַ</param>
        /// <param name="parameters">�������</param>
        /// <returns>HTTP��Ӧ</returns>
        public static string DoPost(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.KeepAlive = true;
            req.ContentType = "application/x-www-form-urlencoded;charset=GBK";
            //req.Timeout = 600000;
            int sendCount = 0;
            string error = "Զ�̷��������ش�����߲�����ʱ";
            byte[] postData = Encoding.UTF8.GetBytes(BuildPostData(parameters));

            while (sendCount != 5)
            {
                try
                {
                    Stream reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    reqStream.Close();
                    HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                    _statusCode = rsp.StatusCode;
                    Encoding encoding = String.IsNullOrEmpty(rsp.CharacterSet) ? Encoding.UTF8 : Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch (WebException er)
                {
                    if (er.Response != null)
                    {
                        HttpWebResponse rsp = (HttpWebResponse)er.Response;
                        _statusCode = rsp.StatusCode;
                    }
                    sendCount++;
                    error = er.ToString();
                    return error;
                }
            }

            throw new Exception(error);
        }

        public static HttpStatusCode StatusCode
        {
            get
            {
                return _statusCode;
            }
        }

        /// <summary>
        /// ִ��HTTP GET����
        /// </summary>
        /// <param name="url">�����ַ</param>
        /// <param name="parameters">�������</param>
        /// <returns>HTTP��Ӧ</returns>
        public static string DoGet(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = String.Format("{0}&{1}", url, BuildPostData(parameters));
                }
                else
                {
                    url = String.Format("{0}?{1}", url, BuildPostData(parameters));
                }
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "GET";
            req.KeepAlive = true;
            req.ContentType = "application/x-www-form-urlencoded";

            int sendCount = 0;
            while (sendCount != 5)
            {
                try
                {
                    HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);
                }
                catch
                {
                    sendCount++;
                }
            }
            return "Զ�̷��������ش�����߲�����ʱ";
        }

        /// <summary>
        /// ��װ��ͨ�ı����������
        /// </summary>
        /// <param name="parameters">Key-Value��ʽ��������ֵ�</param>
        /// <returns>URL��������������</returns>
        private static string BuildPostData(IDictionary<string, string> parameters)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;

            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // ���Բ����������ֵΪ�յĲ���
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }

                    postData.Append(name);
                    postData.Append("=");
                    //postData.Append( Uri.EscapeDataString(value));
                    postData.Append(System.Web.HttpUtility.UrlEncode(value, Encoding.UTF8));
                    hasParam = true;
                }
            }

            return postData.ToString();
        }

    }
}