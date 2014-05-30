using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using MDT.ManageCenter.ServiceImplement;

namespace MDT.ManageCenter.WinServiceHost
{
    partial class Service1 : ServiceBase
    {
        private ServiceHost dbSchemaHost;
        private ServiceHost manageCenterHost;
        private ServiceHost exchangeTraceHost;
        private ServiceHost producerCenterHost;
        private ServiceHost consumerCenterHost;

        public Service1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            try
            {
                // 监听地址列表
                string baseAddresses = String.Empty;

                dbSchemaHost = new ServiceHost(typeof(DbSchemaService));
                dbSchemaHost.Faulted += new EventHandler(dbSchemaHost_Faulted);
                dbSchemaHost.Open();

                foreach (Uri address in dbSchemaHost.BaseAddresses)
                {
                    baseAddresses += address.AbsoluteUri + "\n";
                }

                manageCenterHost = new ServiceHost(typeof(ManageCenterService));
                manageCenterHost.Faulted += new EventHandler(manageCenterHost_Faulted);
                manageCenterHost.Open();

                foreach (Uri address in manageCenterHost.BaseAddresses)
                {
                    baseAddresses += address.AbsoluteUri + "\n";
                }

                exchangeTraceHost = new ServiceHost(typeof(TraceLogCenterService));
                exchangeTraceHost.Faulted += new EventHandler(exchangeTraceHost_Faulted);
                exchangeTraceHost.Open();

                foreach (Uri address in exchangeTraceHost.BaseAddresses)
                {
                    baseAddresses += address.AbsoluteUri + "\n";
                }

                producerCenterHost = new ServiceHost(typeof(DataProducerCenterService));
                producerCenterHost.Faulted += new EventHandler(producerCenterHost_Faulted);
                producerCenterHost.Open();

                foreach (Uri address in producerCenterHost.BaseAddresses)
                {
                    baseAddresses += address.AbsoluteUri + "\n";
                }

                consumerCenterHost = new ServiceHost(typeof(DataConsumerCenterService));
                consumerCenterHost.Faulted += new EventHandler(consumerCenterHost_Faulted);
                consumerCenterHost.Open();

                foreach (Uri address in consumerCenterHost.BaseAddresses)
                {
                    baseAddresses += address.AbsoluteUri + "\n";
                }

                EventLog.WriteEntry(String.Format("{0}已启动，监听地址：{1}", ServiceName, baseAddresses), EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                EventLog.WriteEntry(String.Format("{0}启动失败，{1}", ServiceName, ex.Message), EventLogEntryType.Error);

                Stop();
            }
        }

        private void dbSchemaHost_Faulted(object sender, EventArgs e)
        {
            EventLog.WriteEntry(String.Format("{0}发生错误，状态为{1}", ServiceName, dbSchemaHost.State.ToString()), EventLogEntryType.Error);
        }

        private void manageCenterHost_Faulted(object sender, EventArgs e)
        {
            EventLog.WriteEntry(String.Format("{0}发生错误，状态为{1}", ServiceName, manageCenterHost.State.ToString()), EventLogEntryType.Error);
        }

        private void exchangeTraceHost_Faulted(object sender, EventArgs e)
        {
            EventLog.WriteEntry(String.Format("{0}发生错误，状态为{1}", ServiceName, exchangeTraceHost.State.ToString()), EventLogEntryType.Error);
        }

        private void producerCenterHost_Faulted(object sender, EventArgs e)
        {
            EventLog.WriteEntry(String.Format("{0}发生错误，状态为{1}", ServiceName, producerCenterHost.State.ToString()), EventLogEntryType.Error);
        }

        private void consumerCenterHost_Faulted(object sender, EventArgs e)
        {
            EventLog.WriteEntry(String.Format("{0}发生错误，状态为{1}", ServiceName, consumerCenterHost.State.ToString()), EventLogEntryType.Error);
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        protected override void OnStop()
        {
            if (dbSchemaHost != null && dbSchemaHost.State == CommunicationState.Opened)
            {
                dbSchemaHost.Close();
            }
            dbSchemaHost = null;

            if (manageCenterHost != null && manageCenterHost.State == CommunicationState.Opened)
            {
                manageCenterHost.Close();
            }
            manageCenterHost = null;

            if (exchangeTraceHost != null && exchangeTraceHost.State == CommunicationState.Opened)
            {
                exchangeTraceHost.Close();
            }
            exchangeTraceHost = null;

            if (producerCenterHost != null && producerCenterHost.State == CommunicationState.Opened)
            {
                producerCenterHost.Close();
            }
            producerCenterHost = null;

            if (consumerCenterHost != null && consumerCenterHost.State == CommunicationState.Opened)
            {
                consumerCenterHost.Close();
            }
            consumerCenterHost = null;

            EventLog.WriteEntry(String.Format("{0}已停止", ServiceName), EventLogEntryType.Information);
        }
    }
}
