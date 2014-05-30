using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Serialization;
using System.Xml;
using System.IO;
using System.Data.Common;
using System.Data;
using System.Xml.Serialization;
using System.Xml.Xsl;
using System.Runtime.Serialization.Json;

namespace MDT.Utility
{
    public class CommonUtility
    {
        public const string MDTDATAROOTNAME = "MDTData";

        public static bool TryExcute(Func<bool> action, int times, int interval)
        {
            bool sucess = false;
            int retryCount = times;

            while (sucess == false)
            {
                try
                {
                    action.Invoke();
                    sucess = true;
                }
                catch
                {
                    retryCount--;
                    if (retryCount <= 0)
                        throw;
                    Thread.Sleep(interval);
                }
            }
            return sucess;
        }

        public static string SerializeXml<T>(T t)
        {
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb);
            ser.WriteObject(xw, t);
            xw.Flush();
            xw.Close();
            return sb.ToString();
        }

        public static T UnserializeXml<T>(string xml)
        {
            DataContractSerializer ser = new DataContractSerializer(typeof(T));
            StringReader sr = new StringReader(xml);
            XmlReader xr = XmlReader.Create(sr);
            T t = (T)ser.ReadObject(xr);
            sr.Close();
            xr.Close();
            return t;
        }

        public static string GetXSD(DataSet ds)
        {
            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb);
            ds.WriteXml(xw, XmlWriteMode.WriteSchema);
            xw.Flush();
            xw.Close();
            return sb.ToString();
        }

        public static String replaceString(String strData, String regex,String replacement)
        {
            if (strData == null)
            {
                return null;
            }
            int index;
            index = strData.IndexOf(regex);
            String strNew = "";
            if (index >= 0)
            {
                while (index >= 0)
                {
                    strNew += strData.Substring(0, index) + replacement;
                    strData = strData.Substring(index + regex.Length);
                    index = strData.IndexOf(regex);
                }
                strNew += strData;
                return strNew;
            }
            return strData;
        }              

        public static string SerializeObjectToXml<T>(T t) where T : class
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));
            MemoryStream ms = new MemoryStream();
            XmlWriterSettings setting = new XmlWriterSettings()
            {
                Encoding = new UTF8Encoding(false)               
            };
            using (XmlWriter witer = XmlWriter.Create(ms, setting))
            {
                xs.Serialize(ms, t);
            }
            byte[] buffer = ms.ToArray();
            ms.Close();
            ms.Dispose();
            return UTF8Encoding.UTF8.GetString(buffer);
        }

        public static string ConvertDataToXml(DataSet ds)
        {
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                // 把表结构转换成小写
                foreach (DataTable dt in ds.Tables)
                {
                    dt.TableName = dt.TableName.ToLower();
                    foreach (DataColumn c in dt.Columns)
                    {
                        c.ColumnName = c.ColumnName.ToLower();
                    }
                }

                StringBuilder sb = new StringBuilder();
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                XmlWriter xw = XmlWriter.Create(sb, settings);
                ds.WriteXml(xw, XmlWriteMode.IgnoreSchema);
                xw.Flush();
                xw.Close();
                return sb.ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        public static DbType ConvertToDbType(string type)
        {
            //string t = type.ToLower();

            switch (type.ToString())
            {
                case "System.Int16":
                    return DbType.Int16;
                case "System.Int32":
                    return DbType.Int32;
                case "System.Int64":
                    return DbType.Int64;
                case "System.DateTime":
                    return DbType.DateTime;
                case "System.Decimal":
                    return DbType.Decimal;
                case "System.Boolean":
                    return DbType.Boolean;
                case "System.Byte":
                    return DbType.Byte;
                case "System.Double":
                    return DbType.Double;
                default:
                    return DbType.String;
            }
        }

        public static string GetEParameterPrefix(string type)
        {
            switch (type)
            {
                case "SqlServer":
                    return "@";
                case "Oracle":
                    return ":";
                case "MySql":
                    return "?";
                default:
                    throw new Exception(String.Format("没有数据库{0} 对应的前缀", type));
            }
        }

        public static bool IsResultValid(string xml)
        {
            return !xml.Contains("<error_response>");
        }

        public static bool IsResultValid(string xml, string nodeName)
        {
            bool valid = true;
            if (!String.IsNullOrEmpty(xml))
            {
                try
                {
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xml);
                    var list = doc.SelectNodes(String.Format("//{0}", nodeName));
                    if (list.Count == 0)
                        valid = false;
                }
                catch
                {
                    valid = false;
                }
            }
            return valid;
        }

        /// <summary>
        /// 构造异常信息
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string GetExceptionMsg(Exception ex)
        {
            string msg = String.Format("{0}\n{1}", ex.Message, ex.StackTrace);
            if (ex.InnerException != null)
                msg += String.Format("\n{0}", ex.InnerException.Message);

            return msg;
        }

        public static string GetExceptionMsg(Exception ex, string traceData = "")
        {
            return FormatLogMessage.XmlErroMsgFormat(ex, traceData);
        }

        public static string GetXSLTInfo(string mapping)
        {
            XslCompiledTransform tran = new XslCompiledTransform();

            // 适用WCF服务
            //tran.Load(AppDomain.CurrentDomain.BaseDirectory +"/bin/MappingToXSLT.xslt");

            // 适用动态库
            tran.Load(AppDomain.CurrentDomain.BaseDirectory + "/MappingToXSLT.xslt");

            StringReader sr = new StringReader(mapping);
            XmlReader xr = XmlReader.Create(sr);

            StringBuilder sb = new StringBuilder();
            XmlWriter xw = XmlWriter.Create(sb);
            tran.Transform(xr, xw);

            sr.Close();
            xr.Close();
            xw.Flush();
            xw.Close();
            return sb.ToString();
        }
    }
}
