﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace MDT.ServiceMonitor
{
    public class webUtils
    {
        /// <summary>
        /// 执行HTTP POST请求。
        /// </summary>
        /// <param name="url">请求地址</param>
        /// <param name="parameters">请求参数</param>
        /// <returns>HTTP响应</returns>
        public static string DoPostGBK(string url, IDictionary<string, string> parameters)
        {
            int sendCount = 0;
            string err = string.Empty;
            while (sendCount != 5)
            {
                try
                {
                    HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
                    req.Method = "POST";
                    req.KeepAlive = true;

                    req.ContentType = "application/x-www-form-urlencoded;charset=GBK";
                    byte[] postData = System.Text.Encoding.GetEncoding("GBK").GetBytes(BuildPostDataGBK(parameters));

                    Stream reqStream = req.GetRequestStream();
                    reqStream.Write(postData, 0, postData.Length);
                    reqStream.Close();

                    HttpWebResponse rsp = (HttpWebResponse)req.GetResponse();
                    Encoding encoding = string.IsNullOrEmpty(rsp.CharacterSet) || rsp.CharacterSet == "null" ?
                        Encoding.GetEncoding("GBK") : Encoding.GetEncoding(rsp.CharacterSet);
                    return GetResponseAsString(rsp, encoding);

                }
                catch (Exception ex)
                {
                    sendCount++;
                    err = ex.Message;
                }
            }
            return err;
        }
        /// <summary>
        /// 组装普通文本请求参数。
        /// </summary>
        /// <param name="parameters">Key-Value形式请求参数字典</param>
        /// <returns>URL编码后的请求数据</returns>
        private static string BuildPostDataGBK(IDictionary<string, string> parameters)
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
                    postData.Append(System.Web.HttpUtility.UrlEncode(value, Encoding.GetEncoding("GBK")));
                    hasParam = true;
                }
            }

            return postData.ToString();
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

    }
}
