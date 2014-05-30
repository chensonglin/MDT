using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MDT.DataProducer.ServiceImplement;

namespace MDT.DataProducer.WinFormHost
{
    class Program
    {
        static void Main(string[] args)
        {
            MonitorService dbMonitorService = null;
            System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);
            try
            {
                dbMonitorService = new MonitorService();
                dbMonitorService.Start();

                Console.WriteLine("服务已启动！");
                Console.Read();

                if (dbMonitorService != null)
                {
                    dbMonitorService.Stop();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务启动失败！");
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }

        private static void ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            MDT.Utility.TextWriter.WriteExceptionLog(e.Exception);
        }

        private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            MDT.Utility.TextWriter.WriteExceptionLog(e.ExceptionObject as Exception);
        }
    }
}
