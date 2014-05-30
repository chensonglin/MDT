using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Transactions;
using System.IO;
using System.ServiceModel;
using System.Xml;
using System.Xml.Linq;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using MDT.Utility;
using MDT.DatabaseFactory;
using MDT.DataConsumer.ServiceContract;
using MDT.DataConsumer.ServiceImplement.EmailCenter;
using MDT.DataConsumer.ServiceImplement.TraceLogCenter;
using MDT.DataConsumer.ServiceImplement.DataConsumerCenter;
using System.Web;

namespace MDT.DataConsumer.ServiceImplement
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class DataConsumerService : IDataConsumerService
    {
        private int taskId;
        private Dictionary<string, Database> databaseList;
        private Dictionary<string, DbCommand> dbCommandList;

        public DataConsumerService()
        {
            taskId = 0;
            databaseList = new Dictionary<string, Database>();
            dbCommandList = new Dictionary<string, DbCommand>();
        }

        private IDataConsumerCenterService _center;
        private IDataConsumerCenterService center
        {
            get
            {
                //if (!CanUse((ICommunicationObject)_center))
                //    _center = WCFServiceFactory.GetInstance<DataConsumerCenterServiceClient>();
                //return _center;

                ICommunicationObject obj = _center as ICommunicationObject;
                if (obj == null || obj.State == CommunicationState.Faulted || obj.State == CommunicationState.Closed)
                {
                    _center = WCFServiceFactory.GetInstance<DataConsumerCenterServiceClient>();
                }
                return _center;
            }
        }

        private ITraceLogCenterService _trace;
        private ITraceLogCenterService trace
        {
            get
            {
                //if (!CanUse((ICommunicationObject)_trace))
                //    _trace = WCFServiceFactory.GetInstance<TraceLogCenterServiceClient>();
                //return _trace;

                ICommunicationObject obj = _trace as ICommunicationObject;
                if (obj == null || obj.State == CommunicationState.Faulted || obj.State == CommunicationState.Closed)
                {
                    _trace = WCFServiceFactory.GetInstance<TraceLogCenterServiceClient>();
                }
                return _trace;
            }
        }

        private IEmailCenterService _email;
        private IEmailCenterService email
        {
            get
            {
                ICommunicationObject obj = _email as ICommunicationObject;
                if (obj == null || obj.State == CommunicationState.Faulted || obj.State == CommunicationState.Closed)
                {
                    _email = WCFServiceFactory.GetInstance<EmailCenterServiceClient>();
                }
                return _email;
            }
        }

        private ICacheManager _cache;
        private ICacheManager cache
        {
            get
            {
                if (_cache == null)
                    _cache = EnterpriseLibraryContainer.Current.GetInstance<ICacheManager>();
                return _cache;
            }
        }

        public Source GetSourceInfo(int taskId)
        {
            string key = "DS" + taskId.ToString();
            if (!cache.Contains(key))
            {
                Source source = center.GetSourceInfo(taskId);
                cache.Add(key, source, CacheItemPriority.Normal, null, new AbsoluteTime(TimeSpan.FromMinutes(5)));
                return source;
            }
            else
            {
                var source = (Source)cache.GetData(key);
                if (source == null)
                    source = GetSourceInfo(taskId);

                return source;
            }
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="data"></param>
        /// <param name="taskId"></param>
        /// <param name="processLN"></param>
        public void Send(int taskId, string processLN, string data)
        {
            this.taskId = taskId;
            bool hasTraceLog = true;
            Source source = null;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            TraceLogInfo traceInfo = new TraceLogInfo()
            {
                ID = processLN,
                Data = data,
                TaskId = taskId,
                Stage = TraceStage.DataConsumer,
            };

            try
            {
                dic.Add(CommonUtility.MDTDATAROOTNAME, data);
                source = GetSourceInfo(taskId);
               
                // 执行主要任务
                foreach (TaskUnit taskUnit in source.MainTasks)
                {
                    if (taskUnit.Commands == null || taskUnit.Commands.Length == 0) { continue; }

                    hasTraceLog = taskUnit.HasTraceLog;
                    traceInfo.SubTaskId = taskUnit.Id;
                    traceInfo.StartTime = DateTime.Now;
                    traceInfo.Status = TraceStatus.Success;
                    traceInfo.RunInfo = String.Empty;

                    try
                    {
                        executeTask(taskUnit, dic);
                    }
                    catch (Exception ex)
                    {
                        traceInfo.Status = TraceStatus.Failed;
                        traceInfo.RunInfo = CommonUtility.GetExceptionMsg(ex, data);
                    }

                    // 记录详细日志信息（合并）
                    if (traceInfo.Status == TraceStatus.Success && dic.Count > 1)
                    {
                        traceInfo.Data = mergeTraceData(taskUnit, dic);
                    }
                    // 记录日志
                    writeTraceLog(traceInfo, hasTraceLog);
                }
            }
            catch (Exception ex)
            {
                traceInfo.Status = TraceStatus.Failed;
                traceInfo.RunInfo = CommonUtility.GetExceptionMsg(ex, data);

                // 记录日志
                writeTraceLog(traceInfo, hasTraceLog);
            }
        }

        /// <summary>
        /// 合并日志信息
        /// </summary>
        /// <param name="task"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        private string mergeTraceData(TaskUnit task, Dictionary<string, string> dic)
        {
            string data = dic[CommonUtility.MDTDATAROOTNAME];

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(data);
                foreach (ECommand comm in task.Commands)
                {
                    if (comm.HasResult && dic.Keys.Contains(comm.CommandName))
                    {
                        XmlDocument commDoc = new XmlDocument();
                        commDoc.LoadXml(dic[comm.CommandName]);

                        XmlNode node = doc.CreateElement(comm.CommandName);
                        node.InnerXml = commDoc.DocumentElement.InnerXml;
                        doc.DocumentElement.AppendChild(node);
                    }
                }
                data = doc.OuterXml;
            }
            catch { }

            return data;
        }

        /// <summary>
        /// 记录日志信息 Code by wk 2010-12-03
        /// </summary>
        /// <param name="traceInfo"></param>
        private void writeTraceLog(TraceLogInfo traceInfo, bool hasTraceLog)
        {
            if (!hasTraceLog)
                traceInfo.Data = "<?xml version=\"1.0\" encoding=\"utf-16\"?><MDTData/>";

            try
            {
                traceInfo.EndTime = DateTime.Now;
                trace.Write(traceInfo);
            }
            catch (Exception ex)
            {
                // 发送邮件信息
                //email.Send(CommonUtility.GetExceptionMsg(ex));
                //1邮件 2短信集成
                int taskID = traceInfo.TaskId;
                email.ActiveNoticeService(taskID, CommonUtility.GetExceptionMsg(ex));
            }
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="dic"></param>
        private void executeTask(TaskUnit taskUnit, Dictionary<string, string> dic)
        {
            // 构造数据库操作命令
            buildDbCommand(taskUnit.Commands);

            // 是否启用事务（数据库操作事务）
            if (taskUnit.HasTransaction && databaseList.Count > 0)
            {
                DbConnection dbConn = null;
                DbTransaction dbTrans = null;
                Database db = databaseList.FirstOrDefault().Value;

                try
                {
                    dbConn = db.CreateConnection();
                    dbConn.Open();

                    // 启动事务
                    dbTrans = dbConn.BeginTransaction();
                    // 执行任务
                    executeTask(taskUnit.Commands, dic, dbTrans);
                    // 提交事务
                    dbTrans.Commit();
                }
                catch (Exception ex)
                {
                    // 回滚事务
                    if (dbTrans != null)
                        dbTrans.Rollback();

                    throw ex;
                }
                finally
                {
                    if (dbConn != null && dbConn.State != ConnectionState.Closed)
                    {
                        dbTrans = null;
                        dbConn.Close();
                        dbConn.Dispose();
                    }
                }
            }
            else
            {
                // 执行任务
                executeTask(taskUnit.Commands, dic, null);
            }
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="dic"></param>
        /// <param name="dbTrans"></param>
        /// <returns></returns>
        private string executeTask(ECommand[] commands, Dictionary<string, string> dic, DbTransaction dbTrans)
        {
            string result = String.Empty;
            foreach (ECommand command in commands)
            {
                if (!String.IsNullOrEmpty(command.CommandText))
                {
                    if (!String.IsNullOrEmpty(command.ParameterValueFrom) && dic.ContainsKey(command.ParameterValueFrom))
                        command.ParameterValue = dic[command.ParameterValueFrom];

                    switch (command.SourceType)
                    {
                        case SourceType.Http:
                        case SourceType.Tcp:
                            result = executeAPI(command);
                            break;
                        default:
                            result = executeDB(command, dbTrans);
                            break;
                    }

                    if (command.HasResult)
                        dic.Add(command.CommandName, result);

                    // 判断返回结果的有效性
                    //szq modify at 20110923 格式化错误信息
                    if (!CommonUtility.IsResultValid(result))
                        throw new Exception(String.Format("{0}#ER#{1}#ER#{2}", command.CommandName, command.CommandText, result));
                    //throw new Exception(String.Format("执行命令：{0} 时发生错误,命令内容为：{1} 错误信息：{2}", command.CommandName, command.CommandText, result));
                }
            }

            return result;
        }

        /// <summary>
        /// 执行API
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private string executeAPI(ECommand command)
        {
            string result = String.Empty;
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (!String.IsNullOrEmpty(command.ParameterValue))
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(command.ParameterValue);
                var nodes = doc.SelectNodes(command.ParameterValueOjbectName);

                foreach (XmlNode n in nodes)
                {
                    foreach (var p in command.Parameters)
                    {
                        // 判断参数名称是否与命令名称相同
                        if (String.Compare(p.Name, command.ParameterValueFrom, true) == 0
                           || String.Compare(p.Name, CommonUtility.MDTDATAROOTNAME, true) == 0)
                        {
                            //cfl 2011-9-30  当参数值为xml时，需转换格式，其他文本格式参数无影响
                            dic.Add(p.Name, HttpUtility.HtmlEncode(n.OuterXml));
                            //dic.Add(p.Name, n.OuterXml);
                        }
                        else
                        {
                            foreach (XmlNode cn in n.ChildNodes)
                            {
                                if (String.Compare(p.Name, cn.Name, true) == 0)
                                {
                                    //cfl  2011-9-30
                                    //dic.Add(p.Name, HttpUtility.HtmlEncode(cn.InnerText));
                                    dic.Add(p.Name, cn.InnerText);
                                    break;
                                }
                            }
                        }
                    }

                    // 请求TOP数据信息
                    switch (command.CommandType)
                    {
                        case ECommandType.Post:
                            result = HttpRequestUtils.DoPost(command.CommandText, dic);
                            break;
                        case ECommandType.Get:
                            result = HttpRequestUtils.DoGet(command.CommandText, dic);
                            break;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 执行数据库
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dbTrans"></param>
        /// <returns></returns>
        private string executeDB(ECommand command, DbTransaction dbTrans)
        {
            DataSet ds = new DataSet();
            Database db = databaseList[command.SourceLink];
            DbCommand dbComm = dbCommandList[getCommandKey(command)];
            if (!String.IsNullOrEmpty(command.ParameterValue))
            {
                XmlNode paramNode = null;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(command.ParameterValue);
                XmlNodeList nodeList = doc.SelectNodes(command.ParameterValueOjbectName);

                foreach (XmlNode node in nodeList)
                {
                    // 设置差数值 modify by wangk 2012-12-07
                    foreach (DbParameter param in dbComm.Parameters)
                    {
                        paramNode = node.SelectSingleNode(param.ParameterName);
                        if (paramNode != null && !String.IsNullOrEmpty(paramNode.InnerText))
                        {
                            if (param.DbType == DbType.DateTime)
                            {
                                DateTime dt;
                                DateTime.TryParse(paramNode.InnerText, out dt);
                                param.Value = dt;
                            }
                            else
                            {
                                param.Value = paramNode.InnerText;
                            }
                        }
                        else
                        {
                            param.Value = DBNull.Value;
                        }
                    }

                    //// 设置差数值（null）
                    //foreach (DbParameter param in dbComm.Parameters)
                    //{
                    //    param.Value = DBNull.Value;
                    //}

                    //// 设置差数值
                    //foreach (XmlNode cn in node.ChildNodes)
                    //{
                    //    if (dbComm.Parameters.Contains(cn.Name) && !String.IsNullOrEmpty(cn.InnerText))
                    //        dbComm.Parameters[cn.Name].Value = cn.InnerText;
                    //}

                    if (command.HasResult)
                    {
                        var data = executeDataSet(db, dbComm, dbTrans);
                        data.Tables[0].TableName = command.CommandName;
                        ds.Merge(data);
                    }
                    else
                    {
                        executeNonQuery(db, dbComm, dbTrans);
                    }
                }
            }
            else if (command.Parameters == null || command.Parameters.Length == 0)
            {
                if (command.HasResult)
                {
                    ds = executeDataSet(db, dbComm, dbTrans);
                    ds.Tables[0].TableName = command.CommandName;
                }
                else
                {
                    executeNonQuery(db, dbComm, dbTrans);
                }
            }
            return CommonUtility.ConvertDataToXml(ds);
        }

        private DataSet executeDataSet(Database db, DbCommand dbComm, DbTransaction dbTrans)
        {
            if (dbTrans == null)
                return db.ExecuteDataSet(dbComm);
            else
                return db.ExecuteDataSet(dbComm, dbTrans);
        }

        private void executeNonQuery(Database db, DbCommand dbComm, DbTransaction dbTrans)
        {
            if (dbTrans == null)
                db.ExecuteNonQuery(dbComm);
            else
                db.ExecuteNonQuery(dbComm, dbTrans);
        }

        /// <summary>
        /// 构建数据操作命令
        /// </summary>
        /// <param name="commands"></param>
        private void buildDbCommand(ECommand[] commands)
        {
            string commandKey = null;
            Database db = null;
            DbCommand dbComm = null;
            databaseList.Clear();
            dbCommandList.Clear();

            foreach (ECommand command in commands)
            {
                if (command.SourceType != SourceType.Http && command.SourceType != SourceType.Tcp)
                {
                    // 构造Database
                    if (!databaseList.ContainsKey(command.SourceLink))
                    {
                        db = DbFactory.CreateDatabase((MySourceType)command.SourceType, command.SourceLink);
                        databaseList.Add(command.SourceLink, db);
                    }

                    // 构造DbCommand
                    commandKey = getCommandKey(command);
                    if (!dbCommandList.ContainsKey(commandKey))
                    {
                        db = databaseList[command.SourceLink];
                        dbComm = db.CreateConnection().CreateCommand();
                        dbComm.CommandType = (CommandType)Enum.Parse(typeof(CommandType), Enum.GetName(typeof(ECommandType), command.CommandType));
                        dbComm.CommandText = command.CommandText;

                        if (command.Parameters != null)
                        {
                            foreach (var param in command.Parameters)
                            {
                                var n = dbComm.CreateParameter();
                                n.ParameterName = param.Name;
                                n.DbType = CommonUtility.ConvertToDbType(param.Type);
                                dbComm.Parameters.Add(n);
                            }
                        }

                        dbCommandList.Add(commandKey, dbComm);
                    }
                }
            }
        }

        /// <summary>
        /// 获取操作命令键值
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private string getCommandKey(ECommand command)
        {
            return String.Format("{0}-{1}", taskId, command.CommandText);
        }

        /// <summary>
        /// 重发数据信息
        /// </summary>
        /// <param name="traceId"></param>
        public void ReSend(int traceId)
        {
            //var t = trace.Read(traceId);
            //Send(t.TaskId, t.ProcessLN, t.Data);
        }
    }
}
