using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using MDT.DataConsumer.ServiceImplement;

namespace MDT.DataConsumer.WinServiceHost
{
    partial class Service1 : ServiceBase
    {
        private ServiceHost host = null;

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
                host = new ServiceHost(typeof(DataConsumerService));
                host.Faulted += new EventHandler(host_Faulted);
                host.Open();

                // 监听地址列表
                string baseAddresses = String.Empty;

                foreach (Uri address in host.BaseAddresses)
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

        private void host_Faulted(object sender, EventArgs e)
        {
            EventLog.WriteEntry(String.Format("{0}发生错误，状态为{1}", ServiceName, host.State.ToString()), EventLogEntryType.Error);
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        protected override void OnStop()
        {
            if (host != null && host.State == CommunicationState.Opened)
            {
                host.Close();
            }

            host = null;
            EventLog.WriteEntry(String.Format("{0}已停止", ServiceName), EventLogEntryType.Information);
        }
    }
}
