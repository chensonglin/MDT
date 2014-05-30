using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MDT.EmailCenter.ServiceContract;
using MDT.EmailCenter.ServiceImplement;

namespace MDT.EmailService.WinFormHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = null;

            try
            {
                host = new ServiceHost(typeof(EmailCenterService));
                host.Open();

                Console.WriteLine("服务已启动！");
                Console.Read();

                if (host != null && host.State == CommunicationState.Opened)
                {
                    host.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("服务启动失败！");
                Console.WriteLine(ex.Message);
            }

            Console.Read();
        }
    }
}
