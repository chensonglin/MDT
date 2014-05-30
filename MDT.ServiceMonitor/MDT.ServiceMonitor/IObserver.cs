using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MDT.ServiceMonitor
{
    /// <summary>
    /// 抽象操作类
    /// </summary>
    public interface IObserver
    {
        /// <summary>
        /// 操作对象
        /// </summary>
        void Action(ParmsObject parm);
    }

}
