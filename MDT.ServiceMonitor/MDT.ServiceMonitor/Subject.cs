using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDT.ServiceMonitor
{

    /// <summary>
    /// 对象实例
    /// </summary>
    public class Subject : ISubject
    {
        private IList<IObserver> lstObserver = new List<IObserver>();
        /// <summary>
        /// 发起操作
        /// </summary>
        public void Notify(ParmsObject parm, string message)
        {
            foreach (IObserver o in lstObserver)
            {
                o.Action(parm);
            }
        }

        /// <summary>
        /// 注册对象
        /// </summary>       
        public void Register(IObserver observer)
        {
            if (observer != null && !lstObserver.Contains(observer))
            {
                lstObserver.Add(observer);
            }
        }

        /// <summary>
        /// 发送信息
        /// </summary>
        /// <param name="parm"></param> 
        public void SendMessage(ParmsObject parm, string message)
        {
            Notify(parm, message);
        }
    }
}
