using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Text.RegularExpressions;

namespace ExpressRegex
{
    /// <summary>
    /// 检查快递单号是否符合规则  需要根目录下配置ExpressRegex.xml文件
    /// </summary>
    public class DeliverNoRegex
    {
        /// <summary>
        /// 检查快递单号是否符合规则
        /// </summary>
        /// <param name="companyId">快递公司ID</param>
        /// <param name="deliverNo">快递单号</param>
        public bool Check(string companyId, string deliverNo)
        {
            bool isRight = false;

            string regex = GetRegexStrByCompanyCode(companyId);
            if (regex != string.Empty)
            {
                Regex rx = new Regex(regex, RegexOptions.Compiled);
                isRight = rx.IsMatch(deliverNo);
            }
            return isRight;
        }

        /// <summary>
        /// 根据快递公司代码返回该快递公司运单号验证正则表达式
        /// </summary>
        /// <param name="companyId">快递公司代码</param>
        /// <returns></returns>
        private string GetRegexStrByCompanyCode(string companyId)
        {
            string regex = string.Empty;
            string configFileName = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "ExpressRegex.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(configFileName);

            string xPath = "Companys/Company";

            XmlNodeList tradeNodeList = doc.SelectNodes(xPath);

            foreach (XmlNode itemNode in tradeNodeList)
            {
                string id = itemNode.SelectSingleNode("CompanyID").InnerText;
                if (id == companyId)
                {
                    regex = itemNode.SelectSingleNode("Regex").InnerText;
                    break;
                }
            }

            return regex;
        }
    }
}
