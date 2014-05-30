using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
namespace MDT.Utility
{
    [XmlRoot(ElementName = "mdt_error")]
    public class LogMessageEntity
    {
        /// <summary>
        /// 标示错误的来源【fromTop fromSystem】
        /// </summary>
        [XmlElement("direction")]
        public string Direction { get; set; }

        /// <summary>
        /// 详细的错误信息
        /// </summary>
        [XmlElement("detail_message")]
        public string DetailErroMsg { get; set; }

        ///// <summary>
        ///// 数据信息（可能为XML字符串）
        ///// </summary>
        //[XmlElement("data_info")]
        //public string DataInfo { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [XmlElement("command_name")]
        public string TaskName { get; set; }

        /// <summary>
        /// 命令名称
        /// </summary>
        [XmlElement("command_text")]
        public string CommandName { get; set; }

        /// <summary>
        /// 简单的错误信息
        /// </summary>
        [XmlElement("message")]
        public string Message { get; set; }
    }


    public class FormatLogMessage
    {
        /// <summary>
        /// 错误XML格式
        /// </summary>
        public static string XmlErroMsgFormat(Exception ex, string traceData)
        {
            string temp = string.Empty;
            string strErroMsg = string.Empty;
            string strDirection = ex.Message.IndexOf("error_response") > 0 ? "fromTop" : "fromSystem";

            LogMessageEntity error = new LogMessageEntity()
            {
                Direction = strDirection
                //Message = ex.Message
                //DetailErroMsg = ex.ToString()
            };
            //szq modify at 20110923 格式化输入错误信息 
            if (!String.IsNullOrEmpty(ex.Message))
            {
                string[] arryMsg = System.Text.RegularExpressions.Regex.Split(ex.Message, "#ER#");
                if (arryMsg.Length == 3)
                {
                    if (!String.IsNullOrEmpty(arryMsg[0]))
                    {
                        error.TaskName = arryMsg[0];
                    }
                    if (!String.IsNullOrEmpty(arryMsg[1]))
                    {
                        error.CommandName = arryMsg[1];
                    }
                    if (!String.IsNullOrEmpty(arryMsg[2]))
                    {
                        strErroMsg = arryMsg[2];
                    }
                    if (strDirection == "fromSystem")
                    {
                        error.Message = strErroMsg;
                    }
                }
                else
                {
                    error.Message = ex.Message;
                }
            }
            temp = CommonUtility.SerializeObjectToXml<LogMessageEntity>(error);
            //XmlDocument docData = new XmlDocument();
            XmlDocument docDestination = new XmlDocument();
            XmlDocument docXmlErro = new XmlDocument();

            try
            {
                docDestination.LoadXml(temp);
                if (strDirection == "fromTop")
                {
                    XmlNode nodeDes = docDestination.SelectSingleNode("mdt_error");
                    if (nodeDes != null)
                    {
                        //xml erroMsg
                        if (!String.IsNullOrEmpty(strErroMsg))
                        {
                            docXmlErro.LoadXml(strErroMsg);

                            XmlNode nodeData = docXmlErro.SelectSingleNode("//*");
                            nodeDes.AppendChild(docDestination.ImportNode(nodeData, true));
                        }                        
                    }
                }
            }
            catch
            {
                error.Message = strErroMsg;
            }
            return docDestination.InnerXml;
        }
    }
}
