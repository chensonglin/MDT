using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Diagnostics;

namespace MDT.EmailCenter.ServiceContract
{
    [ServiceContract(SessionMode = SessionMode.Allowed)]
    public interface IEmailCenterService
    {
        [OperationContract]
        void Send(string message);

        //[OperationContract]
        //void Send(string toAddress, string subject, string message);
        /// <summary>
        /// 信息通知服务
        /// </summary>
        /// <param name="message">信息内容</param>
        /// <param name="type">发送方式 1 邮件 2 短信 3 短信+邮件</param>
        [OperationContract]
        void NoticeService(string account, string message, int type);

        /// <summary>
        /// 激发通知服务
        /// </summary>
        /// <param name="taskid">任务编号</param>
        [OperationContract]
        void ActiveNoticeService(int taskid, string Message);
    }

}
