using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Reflection;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;

namespace MDT.ServiceMonitor
{
    /// <summary>  
    /// 发送邮件的类  
    /// </summary>  
    public class Mail
    {
        
        public void SendAsync(ParmsObject parm)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("email", parm.ToAddress);
            dic.Add("subject", parm.EmailTitle);
            dic.Add("message", parm.Content);
            dic.Add("secret", "wuzhoumail");
            webUtils.DoPostGBK(parm.EmailServiceUrl, dic);
        }
    }

    /// <summary>
    /// 发送手机短信
    /// </summary>
    public class SendShortMessage
    {
        /// <summary>
        /// 发送手机短信
        /// </summary>
        /// <param name="receiveNumber">手机号码</param>
        /// <param name="message">信息内容</param>
        /// <param name="secret">密文</param>
        public void SendMessage(ParmsObject parm)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("receiveNumber", parm.ReceivePhoneNumber);
            dic.Add("message", parm.Content);
            dic.Add("secret", "wuzhousms");
            webUtils.DoPostGBK(parm.SMSSeriveUrl, dic);
        }
    }

}
