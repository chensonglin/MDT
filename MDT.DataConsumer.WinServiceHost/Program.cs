using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.ServiceProcess;

namespace MDT.DataConsumer.WinServiceHost
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 同一个进程可以运行多个服务
            ServiceBase[] serviceToRun = new ServiceBase[] { new Service1() };
            ServiceBase.Run(serviceToRun);
        }
    }
}
