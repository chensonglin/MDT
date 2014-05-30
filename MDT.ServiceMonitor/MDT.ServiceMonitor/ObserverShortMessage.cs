using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDT.ServiceMonitor
{
    /// <summary>
    /// 发送短信
    /// </summary>
    public class ObserverShortMessage : IObserver
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        public void Action(ParmsObject parm)
        {
            SendShortMessage shortMsg = new SendShortMessage();
            shortMsg.SendMessage(parm);
        }
    }
}
