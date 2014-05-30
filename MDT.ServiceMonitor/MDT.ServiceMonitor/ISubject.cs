using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDT.ServiceMonitor
{
    /// <summary>
    /// 抽象对象
    /// </summary>
    public interface ISubject
    {
        /// <summary>
        /// 通知对象
        /// </summary>
        void Notify(ParmsObject parm, string message);

        /// <summary>
        /// 添加对象
        /// </summary>
        void Register(IObserver observer);
    }
}
