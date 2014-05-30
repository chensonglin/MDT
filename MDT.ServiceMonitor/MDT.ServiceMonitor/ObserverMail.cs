using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDT.ServiceMonitor
{
    /// <summary>
    /// 发送邮件
    /// </summary>
    public class ObserverMail : IObserver
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        public void Action(ParmsObject parm)
        {
            Mail mail = new Mail();
            mail.SendAsync(parm);
        }
    }
}
