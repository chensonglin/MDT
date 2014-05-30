using System;
using System.Configuration;
using System.Diagnostics;
using System.ServiceProcess;

namespace MDT.ServiceMonitor
{
    partial class Monitor : ServiceBase
    {
        public Monitor()
        {
            InitializeComponent();
            initConfig();
            DurationTimer.Interval = (parms.ScanSeconds * 1000);
        }

        public static readonly ParmsObject parms = new ParmsObject();

        private void initConfig()
        {
            try
            {
                parms.Content = ConfigurationManager.AppSettings["message"];
                parms.MaxStopSeconds = Convert.ToInt32(ConfigurationManager.AppSettings["max_stop_seconds"]);
                parms.ScanSeconds = Convert.ToDouble(ConfigurationManager.AppSettings["scan_seconds"]);
                parms.NoticeType = ConfigurationManager.AppSettings["notice_type"];
                parms.ToAddress = ConfigurationManager.AppSettings["email_receiver"];
                parms.EmailServiceUrl = ConfigurationManager.AppSettings["email_url"];
                parms.EmailTitle = ConfigurationManager.AppSettings["email_title"];
                parms.SMSSeriveUrl = ConfigurationManager.AppSettings["sms_url"];
                parms.ReceivePhoneNumber = ConfigurationManager.AppSettings["phone_receiver"];
                parms.WinServiceName = ConfigurationManager.AppSettings["winservice_name"];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// 启动时执行      
        protected override void OnStart(string[] args)
        {
            DurationTimer.Enabled = true;
            //EventLog.WriteEntry("MDT启动服务", EventLogEntryType.Information);
        }

        /// 停止时执行
        protected override void OnStop()
        {
            DurationTimer.Enabled = false;
            //EventLog.WriteEntry("MDT停止服务", EventLogEntryType.Information);
        }

        /// 即将关机时记录     
        protected override void OnShutdown()
        {
            EventLog.WriteEntry("MDT停止服务", EventLogEntryType.Information);
            base.OnShutdown();
        }

        //扫描记录
        private void DurationTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string strMessage = ScanTable();

            if (!String.IsNullOrEmpty(strMessage))
            {
                SendMessage(strMessage);
            }
        }

        /// <summary>
        /// 扫描表
        /// </summary>       
        private string ScanTable()
        {
            string[] serverNames = parms.WinServiceName.Split(',');
            string msg = string.Empty;
            foreach (string name in serverNames)
            {
                ServiceController sc = new ServiceController(name);

                if (sc != null && !sc.Status.Equals(ServiceControllerStatus.Running))
                {
                    msg += string.Format("{0}任务停止,", name) + Reset(name);

                }
            }
            return msg;

            //string result = String.Empty;
            //string strSql = String.Empty;
            //DateTime currentTime = new DateTime();
            //DateTime stopTime = new DateTime();
            //int stopAreadySecond = 0;

            //SqlConnection connect = null;
            //SqlCommand command = null;
            //strSql = "select getdate() as [current_time],max(endtime)stoptime,datediff(ss,max(endtime),getdate()) as stopduration from tracelogmaster";

            //try
            //{
            //    connect = new SqlConnection(ConfigurationManager.ConnectionStrings["MDT_ConString"].ConnectionString);
            //    connect.Open();
            //    command = new SqlCommand(strSql, connect);
            //    command.CommandType = CommandType.Text;
            //    using (SqlDataReader dataReader = command.ExecuteReader())
            //    {
            //        if (dataReader.Read())
            //        {
            //            currentTime = Convert.ToDateTime(dataReader["current_time"]);
            //            stopTime = Convert.ToDateTime(dataReader["stoptime"]);
            //            stopAreadySecond = Convert.ToInt32(dataReader["stopduration"]);
            //        }
            //    }
            //    if (stopAreadySecond >= parms.MaxStopSeconds)
            //        result = "STOP";
            //}
            //catch (Exception ex)
            //{
            //    EventLog.WriteEntry("扫描表错误：" + ex.Message, EventLogEntryType.Error);
            //}
            //finally
            //{
            //    if (connect.State != ConnectionState.Closed)
            //    {
            //        connect.Close();
            //        connect.Dispose();
            //    }
            //}
            //return result;
        }

        private string Reset(string serviceName)
        {
            try
            {
                ProcessStartInfo a = new ProcessStartInfo(@"c:/windows/system32/cmd.exe", string.Format("/c  net start {0}", serviceName));
                a.WindowStyle = ProcessWindowStyle.Hidden;
                Process process = Process.Start(a);
                return "服务已启用";
            }
            catch (Exception ex)
            {
                return string.Format("启用服务失败,失败信息:{0}", ex.Message);
            }
        }

        /// <summary>
        /// 发送信息 1邮件 2短信 3短信+邮件
        /// </summary>
        /// <param name="message"></param>
        private void SendMessage(string message)
        {
            Subject subject = new Subject();
            ObserverMail omail = new ObserverMail();
            ObserverShortMessage omsg = new ObserverShortMessage();

            if (parms.NoticeType == "1")
            {
                subject.Register(omail);
            }
            else if (parms.NoticeType == "2")
            {
                subject.Register(omsg);
            }
            else if (parms.NoticeType == "3")
            {
                subject.Register(omail);
                subject.Register(omsg);
            }
            if (!String.IsNullOrEmpty(message))
                parms.Content = message;
            subject.Notify(parms, message);
        }
    }
}
  