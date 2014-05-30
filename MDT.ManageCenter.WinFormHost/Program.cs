using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MDT.ManageCenter.ServiceImplement;

namespace MDT.ManageCenter.WinFormHost
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost dbSchemaHost = null;
            ServiceHost manageCenterHost = null;
            ServiceHost exchangeTraceHost = null;
            ServiceHost producerCenterHost = null;
            ServiceHost consumerCenterHost = null;

            try
            {
                dbSchemaHost = new ServiceHost(typeof(DbSchemaService));
                dbSchemaHost.Open();

                manageCenterHost = new ServiceHost(typeof(ManageCenterService));
                manageCenterHost.Open();

                exchangeTraceHost = new ServiceHost(typeof(TraceLogCenterService));
                exchangeTraceHost.Open();

                producerCenterHost = new ServiceHost(typeof(DataProducerCenterService));
                producerCenterHost.Open();

                consumerCenterHost = new ServiceHost(typeof(DataConsumerCenterService));
                consumerCenterHost.Open();

                Console.WriteLine("服务已启动！");
                Console.ReadLine();

                if (dbSchemaHost != null && dbSchemaHost.State == CommunicationState.Opened)
                    dbSchemaHost.Close();

                if (manageCenterHost != null && manageCenterHost.State == CommunicationState.Opened)
                    manageCenterHost.Close();

                if (exchangeTraceHost != null && exchangeTraceHost.State == CommunicationState.Opened)
                    exchangeTraceHost.Close();

                if (producerCenterHost != null && producerCenterHost.State == CommunicationState.Opened)
                    producerCenterHost.Close();

                if (consumerCenterHost != null && consumerCenterHost.State == CommunicationState.Opened)
                    consumerCenterHost.Close();
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
