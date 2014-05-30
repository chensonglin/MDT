using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using MDT.Utility;
using MDT.DataProducer.ServiceContract;
using MDT.DataProducer.ServiceImplement.EmailCenter;
using MDT.DataProducer.ServiceImplement.DataProducerCenter;

namespace MDT.DataProducer.ServiceImplement
{
    /// <summary>
    /// 监控服务
    /// </summary>
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MonitorService : IMonitorService
    {
        private static bool execute;
        private Dictionary<int, Task> dic;
        private ICacheManager cache;
        private IDataProducerCenterService center;
        private CancellationTokenSource tokenSource;

        public MonitorService()
        {
            if (ConfigurationManager.AppSettings.AllKeys.Contains("ClientId"))
            {
                this.ServiceId = int.Parse(ConfigurationManager.AppSettings["ClientId"]);
            }
            else
            {
                throw new Exception("配置文件中缺少 ClientId");
            }
            cache = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
        }

        public int ServiceId
        {
            get;
            set;
        }

        private IEmailCenterService _email;
        private IEmailCenterService email
        {
            get
            {
                if (_email == null)
                    _email = WCFServiceFactory.GetInstance<EmailCenterServiceClient>();
                return _email;
            }
        }

        private DataProducerService getDataProducerService(TaskInfo task)
        {
            string key = "DPS" + task.ID.ToString();
            if (!cache.Contains(key))
            {
                DataProducerService service = new DataProducerService(task);
                cache.Add(key, service, CacheItemPriority.Normal, null, new AbsoluteTime(TimeSpan.FromMinutes(5)));
                return service;
            }
            else
            {
                var service = (DataProducerService)cache[key];
                if (service == null)
                {
                    service = getDataProducerService(task);
                }
                return service;
            }
        }

        private List<TaskInfo> getTasks()
        {
            if (!cache.Contains("ETask"))
            {
                center = WCFServiceFactory.GetInstance<DataProducerCenterServiceClient>();
                var tasks = center.GetTasks(this.ServiceId).ToList();
                cache.Add("ETask", tasks, CacheItemPriority.Normal, null, new AbsoluteTime(TimeSpan.FromMinutes(1)));
                return tasks;
            }
            else
            {
                var tasks = (List<TaskInfo>)cache.GetData("ETask");
                if (tasks == null)
                {
                    tasks = getTasks();
                }
                return tasks;
            }
        }

        /// <summary>
        /// 启动服务
        /// </summary>
        public void Start()
        {
            execute = true;
            Thread workThread = new Thread(startWork);
            workThread.Start();
        }

        /// <summary>
        /// 停止服务
        /// </summary>
        public void Stop()
        {
            execute = false;
            tokenSource.Cancel();

            foreach (var kvp in dic)
            {
                while (true)
                {
                    if (kvp.Value.Status == TaskStatus.Running
                        || kvp.Value.Status == TaskStatus.Created
                        || kvp.Value.Status == TaskStatus.WaitingToRun
                        || kvp.Value.Status == TaskStatus.WaitingForActivation
                        || kvp.Value.Status == TaskStatus.WaitingForChildrenToComplete)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        private void startWork()
        {
            string message = String.Empty;
            dic = new Dictionary<int, Task>();
            tokenSource = new CancellationTokenSource();

            while (execute)
            {
                try
                {
                    // 获取本服务负责的任务列表    
                    var tasks = getTasks();

                    // 测试开启
                    //Console.WriteLine("任务数：" + tasks.Count);

                    foreach (var t in tasks)
                    {
                        if (!dic.ContainsKey(t.ID))
                        {
                            var nt = executeTask(t);
                            dic.Add(t.ID, nt);
                        }
                        else
                        {
                            var n = dic[t.ID];
                            switch (n.Status)
                            {
                                case TaskStatus.Faulted:
                                    foreach (var e in n.Exception.Flatten().InnerExceptions)
                                    {
                                        message = String.Format("服务节点：数据生产\n任务名称: {0}\n异常信息：{1},{2}", t.TaskName, e.Message, e.StackTrace);
                                        //记录文本日志
                                        MDT.Utility.TextWriter.WriteExceptionLog(e, message, true);
                                        //发送邮件
                                        sendEmail(message);
                                    }
                                    dic[t.ID].Dispose();
                                    dic[t.ID] = executeTask(t);
                                    break;
                                case TaskStatus.RanToCompletion:
                                case TaskStatus.Canceled:
                                    dic[t.ID].Dispose();
                                    dic[t.ID] = executeTask(t);
                                    break;
                            }

                            // 测试开启
                            //Console.WriteLine(String.Format("任务状态：{0}{1}", t.ID, n.Status));
                        }
                    }
                }
                catch (Exception ex)
                {
                    message = String.Format("服务节点：数据生产\n异常信息：{0}\n{1}", ex.Message, ex.StackTrace);

                    //记录文本日志
                    MDT.Utility.TextWriter.WriteExceptionLog(ex, message, true);
                    //发送邮件
                    sendEmail(message);

                    // 测试开启
                    //Console.WriteLine(String.Format("异常信息：{0}", ex.Message));
                }

                // 当前线程挂起时间为10毫秒
                Thread.Sleep(10);
            }
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private Task executeTask(TaskInfo task)
        {
            CancellationToken token = tokenSource.Token;

            return Task.Factory.StartNew(() =>
            {
                DataProducerService _executer = getDataProducerService(task);
                _executer.Token = token;
                _executer.Execute();

            }, token);
        }

        /// <summary>
        /// 发送邮件信息
        /// </summary>
        /// <param name="message"></param>
        private void sendEmail(string message)
        {
            try
            {
                email.Send(message);
            }
            catch
            {
                //TODO:错误信息处理
            }
        }
    }
}
