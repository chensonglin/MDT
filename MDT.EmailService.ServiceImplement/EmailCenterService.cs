using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Diagnostics;
using System.Configuration;
using System.Net.Mail;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using MDT.ManageCenter.DAL;
using MDT.ManageCenter.DataContract;
using MDT.EmailCenter.ServiceContract;

namespace MDT.EmailCenter.ServiceImplement
{
    public class EmailCenterService : IEmailCenterService
    {
        //public void Send(string toAddress, string subject, string message)
        //{
        //    LogEntry log = new LogEntry();
        //    log.Severity = TraceEventType.Warning;
        //    log.Message = body;

        //    Logger.Write(log);
        //}

        public void Send(string message)
        {
            LogEntry log = new LogEntry();
            log.Severity = TraceEventType.Error;
            log.TimeStamp = DateTime.Now;
            log.Message = message;

            Logger.Write(log);
        }

        public void NoticeService(string account, string message, int type)
        {
            INoticeService Service = new NoticeService();
            IServiceAction emailAction = new EmailService();
            IServiceAction smsAction = new SMSService();

            if (type == 1)
            {
                Service.RegisterService(emailAction);
            }
            else if (type == 2)
            {
                Service.RegisterService(smsAction);
            }
            //else if (type == 3)
            //{
            //    Service.RegisterService(emailAction);
            //    Service.RegisterService(smsAction);
            //}
            else
            {
                return;
            }
            Service.ActiveService(account, message);
        }

        public void ActiveNoticeService(int taskid, string message)
        {
            string strAcount = "";
            List<ENoticeService> lstService = new List<ENoticeService>();
            lstService = new ENoticeServiceDAL().GetNoticeServiceList(taskid);

            foreach (ENoticeService notice in lstService)
            {
                if (notice.NoticeMode == 1)
                    strAcount = notice.Email;
                else if (notice.NoticeMode == 2)
                    strAcount = notice.Phone;
                else
                    continue;
                if (String.IsNullOrEmpty(strAcount))
                    continue;
                //控制短信错误信息长度
                int lenMsg = System.Text.Encoding.GetEncoding("gb2312").GetByteCount(message);
                if (lenMsg > 159 && notice.NoticeMode == 2)
                {
                    message = "MDT任务:" + taskid + "发生错误！请检查！";
                }

                NoticeService(strAcount, message, notice.NoticeMode);
            }
        }
    }

    class EmailService : IServiceAction
    {
        public void Action(string account, string message)
        {
            SendMailAsync(account, message);
        }

        /// <summary>  
        /// 异步发送邮件  
        /// </summary>  
        /// <param name="CompletedMethod"></param>  
        public void SendMailAsync(string account, string message)
        {
            string strFrom = ConfigurationManager.AppSettings["smtp_uname"]; //发件人用户名
            string password = ConfigurationManager.AppSettings["smtp_pwd"];    //发件人密码  
            string smtpServer = ConfigurationManager.AppSettings["smtp_server_url"]; //smtp服务器
            string subject = ConfigurationManager.AppSettings["smtp_subject"]; //邮件主题
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtpClient;

            mailMessage.To.Add(account);
            mailMessage.From = new System.Net.Mail.MailAddress(strFrom);

            mailMessage.Body = message;
            mailMessage.Priority = MailPriority.High;
            mailMessage.IsBodyHtml = true;
            mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
            mailMessage.Priority = System.Net.Mail.MailPriority.High;
            mailMessage.Subject = subject;

            if (mailMessage != null)
            {
                smtpClient = new SmtpClient();
                smtpClient.Credentials = new System.Net.NetworkCredential(mailMessage.From.Address, password);//设置发件人身份的票据  
                smtpClient.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtpClient.Host = smtpServer;
                smtpClient.SendAsync(mailMessage, mailMessage.Body);
            }
        }
    }

    class SMSService : IServiceAction
    {
        public void Action(string mobileNumber, string message)
        {
            SendMessage(mobileNumber, message);
        }
        /// <summary>
        /// 短信接口
        /// </summary>
        /// <param name="messageUrl">短信接口地址</param>
        /// <param name="userName">用户名</param>
        /// <param name="userPassword">密码</param>
        /// <param name="mobileNumber">要发送的手机号,多个手机号用逗号分割开</param>
        /// <param name="messageContent">信息内容</param>
        public void SendMessage(string mobileNumber, string messageContent)
        {
            string messageUrl = ConfigurationManager.AppSettings["sms_url"];
            string userName = ConfigurationManager.AppSettings["sms_uname"];
            string userPassword = ConfigurationManager.AppSettings["sms_pwd"];
            string username = userName;
            string password = userPassword;
            string mobile = mobileNumber;
            string content = messageContent;

            if (String.IsNullOrEmpty(username))
                return;
            if (String.IsNullOrEmpty(password))
                return;
            if (String.IsNullOrEmpty(mobile))
                return;
            if (String.IsNullOrEmpty(content))
                return;

            //content = System.Web.HttpContext.Current.Server.UrlEncode(content);
            String Url = messageUrl;
            Url = Url + "&username=" + username + "&password=" + password + "&mobile=" + mobile + "&content=" + content;

            string strResult = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                //声明一个HttpWebRequest请求
                request.Timeout = 30000;
                //设置连接超时时间
                request.Headers.Set("Pragma", "no-cache");
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream streamReceive = response.GetResponseStream();
                Encoding encoding = Encoding.GetEncoding("GB2312");
                StreamReader streamReader = new StreamReader(streamReceive, encoding);
                strResult = streamReader.ReadToEnd();
            }
            catch
            {

            }
        }
    }

    class NoticeService : INoticeService
    {
        private List<IServiceAction> serviceList = new List<IServiceAction>();

        public void RegisterService(IServiceAction action)
        {
            if (action != null && !serviceList.Contains(action))
                serviceList.Add(action);
        }

        public void UnRegisterService(IServiceAction action)
        {
            if (action != null && serviceList.Contains(action))
                serviceList.Remove(action);
        }

        public void ActiveService(string account, string message)
        {
            foreach (IServiceAction action in serviceList)
                action.Action(account, message);
        }
    }

    public interface INoticeService
    {
        void RegisterService(IServiceAction action);

        void UnRegisterService(IServiceAction action);

        void ActiveService(string account, string message);
    }

    public interface IServiceAction
    {
        void Action(string account, string message);
    }
}