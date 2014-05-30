using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography;
using OAuth.Core.KeyInterop;
using System.Threading;

namespace MDT.Utility
{
    public class HttpRequestUtils
    {
        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static string DoPost(string url, IDictionary<string, string> parameters)
        {
            string error = String.Empty;
            string result = String.Empty;
            byte[] postData = Encoding.UTF8.GetBytes(BuildPostData(parameters));

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            //req.Timeout = 600000;
            req.Method = "POST";
            req.UseDefaultCredentials = true;
            req.KeepAlive = true;
            req.ContentType = "application/x-www-form-urlencoded;charset=utf-8";

            try
            {
                Stream reqStream = req.GetRequestStream();
                reqStream.Write(postData, 0, postData.Length);
                reqStream.Close();

                HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                Encoding encoding = String.IsNullOrEmpty(rsp.CharacterSet) ? Encoding.UTF8 : Encoding.GetEncoding(rsp.CharacterSet);
                result = GetResponseAsString(rsp, encoding);                                
            }
            catch (Exception er)
            {
                StringBuilder StrBuilder = new StringBuilder();
                try
                {
                    StrBuilder.Append("<data>");
                    foreach (KeyValuePair<string, string> V in parameters)
                    {
                        StrBuilder.Append(String.Format("<{0}>", V.Key));
                        StrBuilder.Append(String.Format("{0}", V.Value));
                        StrBuilder.Append(String.Format("</{0}>", V.Key));
                    }
                    StrBuilder.Append("</data>");
                }
                catch (Exception)
                {

                }
                if (StrBuilder.Length > 15)
                    error = "<error><info>" + er.Message + "</info>" + StrBuilder.ToString() + "</error>";
                else
                    error = er.Message;
            }

            if (!String.IsNullOrEmpty(error))
                throw new Exception(error);
            else
                return result;
        }

        /// <summary>
        /// 执行HTTP GET请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
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
            return "远程服务器返回错误或者操作超时";
        }

        /// <summary>
        /// 执行带文件上传的HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="fileParams">请求文件参数</param>
        /// <returns>HTTP响应</returns>
        public static string DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, FileInfo> fileParams)
        {
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.KeepAlive = true;
            req.ContentType = "multipart/form-data;boundary=" + boundary;

            Stream reqStream = req.GetRequestStream();
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes(String.Format("\r\n--{0}\r\n", boundary));
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes(String.Format("\r\n--{0}--\r\n", boundary));

            // 组装文本请求参数
            string entryTemplate = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            IEnumerator<KeyValuePair<string, string>> textEnum = textParams.GetEnumerator();
            while (textEnum.MoveNext())
            {
                string formItem = string.Format(entryTemplate, textEnum.Current.Key, textEnum.Current.Value);
                byte[] itemBytes = Encoding.UTF8.GetBytes(formItem);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);
            }

            // 组装文件请求参数
            string fileTemplate = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            IEnumerator<KeyValuePair<string, FileInfo>> fileEnum = fileParams.GetEnumerator();
            while (fileEnum.MoveNext())
            {
                string key = fileEnum.Current.Key;
                FileInfo file = fileEnum.Current.Value;
                string fileItem = String.Format(fileTemplate, key, file.FullName, GetMimeType(file.FullName));
                byte[] itemBytes = Encoding.UTF8.GetBytes(fileItem);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);

                using (Stream fileStream = file.OpenRead())
                {
                    byte[] buffer = new byte[1024];
                    int readBytes = 0;
                    while ((readBytes = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        reqStream.Write(buffer, 0, readBytes);
                    }
                }
            }

            reqStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            reqStream.Close();

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
            throw new Exception("远程服务器返回错误或者操作超时");
        }

        /// <summary>
        /// 执行带文件上传的HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="textParams">请求文本参数</param>
        /// <param name="fileParams">请求文件参数</param>
        /// <returns>HTTP响应</returns>
        public static string DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, string> fileParams)
        {
            string boundary = DateTime.Now.Ticks.ToString("X"); // 随机分隔线

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.Method = "POST";
            req.KeepAlive = true;
            req.ContentType = "multipart/form-data;boundary=" + boundary;

            Stream reqStream = req.GetRequestStream();
            byte[] itemBoundaryBytes = Encoding.UTF8.GetBytes(String.Format("\r\n--{0}\r\n", boundary));
            byte[] endBoundaryBytes = Encoding.UTF8.GetBytes(String.Format("\r\n--{0}--\r\n", boundary));

            // 组装文本请求参数
            string entryTemplate = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            IEnumerator<KeyValuePair<string, string>> textEnum = textParams.GetEnumerator();
            while (textEnum.MoveNext())
            {
                string formItem = string.Format(entryTemplate, textEnum.Current.Key, textEnum.Current.Value);
                byte[] itemBytes = Encoding.UTF8.GetBytes(formItem);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);
            }

            // 组装文件请求参数
            string fileTemplate = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            IEnumerator<KeyValuePair<string, string>> fileEnum = fileParams.GetEnumerator();
            while (fileEnum.MoveNext())
            {
                string key = fileEnum.Current.Key;
                string file = fileEnum.Current.Value;

                string fileItem = String.Format(fileTemplate, key, file, GetMimeType(file));
                byte[] itemBytes = Encoding.UTF8.GetBytes(fileItem);
                reqStream.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                reqStream.Write(itemBytes, 0, itemBytes.Length);

                try
                {
                    using (Stream fileStream = ImgToStream(file))
                    {
                        byte[] buffer = new byte[1024];
                        int readBytes = 0;
                        while ((readBytes = fileStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            reqStream.Write(buffer, 0, readBytes);
                        }
                    }
                }
                catch
                {
                    throw new Exception("远程服务器返回错误或者操作超时");
                }
            }

            reqStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            reqStream.Close();

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
            throw new Exception("远程服务器返回错误或者操作超时");
        }

        /// <summary>
        /// 把响应流转换为文本。
        /// </summary>
        /// <param name="rsp">响应流对象</param>
        /// <param name="encoding">编码方式</param>
        /// <returns>响应文本</returns>
        public static string GetResponseAsString(HttpWebResponse rsp, Encoding encoding)
        {
            StringBuilder result = new StringBuilder();
            Stream stream = null;
            StreamReader reader = null;

            try
            {
                // 以字符流的方式读取HTTP响应
                stream = rsp.GetResponseStream();

                reader = new StreamReader(stream, encoding);

                // 每次读取不大于512个字符，并写入字符串
                char[] buffer = new char[512];
                int readBytes = 0;
                while ((readBytes = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    result.Append(buffer, 0, readBytes);
                }

            }
            finally
            {
                // 释放资源
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (rsp != null) rsp.Close();
            }

            return result.ToString();
        }

        /// <summary>
        /// 根据文件名后缀获取图片型文件的Mime-Type。
        /// </summary>
        /// <param name="filePath">文件全名</param>
        /// <returns>图片文件的Mime-Type</returns>
        private static string GetMimeType(string filePath)
        {
            string mimeType;

            switch (Path.GetExtension(filePath).ToLower())
            {
                case ".bmp": mimeType = "image/bmp"; break;
                case ".gif": mimeType = "image/gif"; break;
                case ".ico": mimeType = "image/x-icon"; break;
                case ".jpeg": mimeType = "image/jpeg"; break;
                case ".jpg": mimeType = "image/jpeg"; break;
                case ".png": mimeType = "image/png"; break;
                default: mimeType = "application/octet-stream"; break;
            }

            return mimeType;
        }

        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        private static string BuildPostData(IDictionary<string, string> parameters)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;

            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // 忽略参数名或参数值为空的参数
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

        public static Stream ImgToStream(string imgUrl)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(imgUrl);
            request.UserAgent = @"Mozilla/6.0 (MSIE 6.0; Windows NT 5.1; Natas.Robot)\";
            request.Timeout = 3000;
            WebResponse response = request.GetResponse();
            return response.GetResponseStream();
        }

        public static string CreateSign(Dictionary<string, string> dic, string appSecret)
        {
            var result = from pair in dic orderby pair.Key select pair;

            StringBuilder str = new StringBuilder(appSecret);
            foreach (KeyValuePair<string, string> key in result)
            {
                str.Append(key.Key);
                str.Append(key.Value);
            }
            str.Append(appSecret);
            string ret = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(str.ToString(), "MD5").ToUpper();
            return ret;
        }

        /// <summary>
        /// 物流宝 post数据
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="micKey">五洲密匙</param>
        /// <param name="wlbUrl">访问物流宝Url</param>
        /// <returns></returns>
        public static string PostDataWLB(string xml, string micKey, string wlbUrl)
        {
            string responseValue = "";
            if (xml != "")
            {
                //将XML进行URL编码
                xml = xml.Replace("<?xml version=\"1.0\" encoding=\"utf-8\"?>", "").Trim();

                string sign = CreateSignWLB(xml, micKey);

                try
                {
                    responseValue = TbWareHousePost(xml, sign, wlbUrl);
                }
                catch (Exception ex)
                {
                    responseValue = ex.ToString();
                }
            }

            return responseValue;
        }

        /// <summary>
        /// 请求淘宝物流接口
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static string TbWareHousePost(string xml, string sign, string wlbUrl)
        {
            Dictionary<string, string> para = new Dictionary<string, string>();
            para.Add("logistics_interface", xml);
            para.Add("data_digest", sign);
            para.Add("type", "v1.0");
            return DoPost(wlbUrl, para);
        }

        /// <summary>
        /// 生成签名 物流宝
        /// </summary>
        /// <param name="Content">内容</param>
        /// <returns></returns>
        public static string CreateSignWLB(string Content, string micKey)
        {
            DSACryptoServiceProvider dsac = new DSACryptoServiceProvider();
            byte[] privateBytes = null;
            byte[] bytes = Encoding.Default.GetBytes(Content);
            privateBytes = Convert.FromBase64String(micKey);
            AsnKeyParser keyParser = new AsnKeyParser(privateBytes);
            dsac.ImportParameters(keyParser.ParseDSAPrivateKey());
            byte[] sign = dsac.SignData(bytes);
            return Convert.ToBase64String(sign);
        }

        /// <summary>
        /// 组合错误信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetErrMessage(string ex)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<error_response>");
            sb.Append("<code>");
            sb.Append("MDT_TOP");
            sb.Append("</code>");
            sb.Append("<msg>");
            sb.Append(ex);
            sb.Append("</msg>");
            sb.Append("</error_response>");
            return sb.ToString();
        }

        /// <summary>
        /// 组合正确信息
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static string GetSucMessage(string message)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<response>");
            sb.Append("<code>");
            sb.Append("MDT_TOP");
            sb.Append("</code>");
            sb.Append("<msg>");
            sb.Append("suc");
            sb.Append("</msg>");
            sb.Append("</response>");
            return sb.ToString();
        }

        /// <summary>
        /// MD5函数2
        /// </summary>
        /// <param name="str">原始字符串</param>
        /// <returns>MD5结果</returns>
        public static string MD5(string str)
        {
            byte[] b = Encoding.Default.GetBytes(str);
            b = new MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
    }
}
