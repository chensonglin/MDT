using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Data.Common;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Practices.EnterpriseLibrary.Data;
using MDT.Utility;
using MDT.DatabaseFactory;
using MDT.DataProducer.ServiceImplement.TraceLogCenter;
using MDT.DataProducer.ServiceImplement.DataProducerCenter;
using MDT.DataProducer.ServiceImplement.EmailCenter;
using System.Web;

namespace MDT.DataProducer.ServiceImplement
{
    public class DataProducerService
    {
        protected Source source;
        private TaskInfo _task;
        private IDataTransformService transformer;
        private Dictionary<string, Database> databaseList;
        private Dictionary<string, DbCommand> dbCommandList;

        public DataProducerService(TaskInfo task)
        {
            if (task == null)
                throw new ArgumentNullException("task");

            _task = task;
            source = center.GetSourceInfo(_task.ID);
            databaseList = new Dictionary<string, Database>();
            dbCommandList = new Dictionary<string, DbCommand>();

            //TODO:初始化服务
        }

        private IDataProducerCenterService _center;
        protected IDataProducerCenterService center
        {
            get
            {
                if (_center == null)
                    _center = WCFServiceFactory.GetInstance<DataProducerCenterServiceClient>();
                return _center;
            }
        }

        private ITraceLogCenterService _trace;
        protected ITraceLogCenterService trace
        {
            get
            {
                if (_trace == null)
                    _trace = WCFServiceFactory.GetInstance<TraceLogCenterServiceClient>();
                return _trace;
            }
        }

        private IEmailCenterService _email;
        protected IEmailCenterService email
        {
            get
            {
                if (_email == null)
                    _email = WCFServiceFactory.GetInstance<EmailCenterServiceClient>();
                return _email;
            }
        }

        public CancellationToken Token { get; set; }

        /// <summary>
        /// 执行数据生产指令
        /// </summary>
        public void Execute()
        {
            if (source != null && source.MainTasks.Length > 0)
            {
                int count = 0;
                bool hasdata = false;
                string xml = String.Empty;
                string guid = Guid.NewGuid().ToString();
                bool hasTraceLog = source.MainTasks[0].HasTraceLog;
                Dictionary<string, string> resultValues = new Dictionary<string, string>();
                TraceLogInfo traceInfo = new TraceLogInfo()
                {
                    ID = guid,
                    Stage = TraceStage.DataProducer,
                    Status = TraceStatus.Success,
                    StartTime = DateTime.Now,
                    TaskId = _task.ID,
                    DataCount = count
                };

                try
                {
                    // 执行主要任务
                    executeTask(source.MainTasks, resultValues);
                    if (source.Results != null)
                    {
                        xml = mergeResult(resultValues, source.Results, out hasdata, out count);
                    }

                    // 判断是否需要传输数据
                    if (hasdata)
                    {
                        // 记录日志信息
                        traceInfo.Data = xml;
                        traceInfo.DataCount = count;
                        writeTraceLog(traceInfo, hasTraceLog);

                        // 判断数据交换任务类型
                        if (_task.Type == TaskType.ST)
                        {
                            // TODO ST
                        }
                        else if (_task.Type == TaskType.LT)
                        {
                            // TODO LT
                        }
                        else
                        {
                            // TODO ET
                            transformer = new DataTransformService { Center = center };
                            transformer.Send(_task.ID, guid, xml);
                        }

                        // 执行事后任务
                        executeTask(source.PostTasks, resultValues);
                    }
                }
                catch (Exception ex)
                {
                    // 记录错误日志 code by wk 2012-09-06
                    traceInfo.Status = TraceStatus.Failed;
                    traceInfo.RunInfo = ex.Message;
                    traceInfo.Data = mergeResult(resultValues, source.Results, out hasdata, out count);
                    traceInfo.DataCount = count;
                    writeTraceLog(traceInfo, hasTraceLog);
                }

                // 设置任务挂起时间
                Thread.Sleep(_task.Interval);
            }
        }

        /// <summary>
        /// 记录日志信息 Code by wk 2010-12-03
        /// </summary>
        /// <param name="traceInfo"></param>
        private void writeTraceLog(TraceLogInfo traceInfo, bool hasTraceLog)
        {
            if (!hasTraceLog)
                traceInfo.Data = "<?xml version=\"1.0\" encoding=\"utf-16\"?><MDTData/>";

            if (traceInfo.Status != TraceStatus.Failed && _task.Type == TaskType.ET)
                traceInfo.Status = TraceStatus.Handle;

            try
            {
                
                traceInfo.EndTime = DateTime.Now;
                trace.Write(traceInfo);
            }
            catch (Exception ex)
            {
                string strMsg = String.Format("\r\nID:{0}\r\nTaskName:{1}\r\nData:{2}", _task.ID, _task.TaskName, traceInfo.Data);
                TextWriter.WriteExceptionLog(ex, strMsg, true);
            }
        }

        /// <summary>
        /// 执行任务
        /// </summary>
        /// <param name="tasks"></param>
        /// <param name="dic"></param>
        private void executeTask(TaskUnit[] tasks, Dictionary<string, string> dic)
        {
            foreach (var task in tasks)
            {
                if (task.Commands == null || task.Commands.Length == 0) { continue; }

                // 构造数据库操作命令
                buildDbCommand(task.Commands);

                // 是否启用事务（数据库事务）
                if (task.HasTransaction && databaseList.Count > 0)
                {
                    DbConnection dbConn = null;
                    DbTransaction dbTrans = null;
                    //Database db = databaseList.FirstOrDefault().Value;
                    Database db = databaseList[task.Commands.FirstOrDefault().SourceLink];

                    try
                    {
                        dbConn = db.CreateConnection();
                        dbConn.Open();

                        // 启动事务
                        dbTrans = dbConn.BeginTransaction();
                        // 执行任务
                        executeTask(task.Commands, dic, dbTrans);
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
                    executeTask(task.Commands, dic, null);
                }
            }
        }

        /// <summary>
        /// 构造执行指令
        /// </summary>
        /// <param name="commands"></param>
        private void buildDbCommand(ECommand[] commands)
        {
            string commandKey = null;
            Database db = null;
            DbCommand dbComm = null;

            foreach (ECommand command in commands)
            {
                if (command.SourceType != SourceType.Http && command.SourceType != SourceType.Tcp
                    && command.SourceType != SourceType.DLL)
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
                        case SourceType.DLL:
                            result = executeDLL(command);
                            break;
                        default:
                            result = executeDB(command, dbTrans);
                            break;
                    }

                    if (command.HasResult)
                        dic.Add(command.CommandName, result);

                    //判断返回结果的有效性
                    //szq modify at 20110923 格式化错误信息
                    if (!CommonUtility.IsResultValid(result))
                        throw new Exception(result);
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
            if (!String.IsNullOrEmpty(command.ParameterValue))
            {
                // 构造参数
                Dictionary<string, string> dic = new Dictionary<string, string>();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(command.ParameterValue);
                var nodes = doc.SelectNodes(command.ParameterValueOjbectName);

                foreach (XmlNode n in nodes)
                {
                    foreach (var p in command.Parameters)
                    {
                        // 判断参数名称是否与命令名称相同
                        if (String.Compare(p.Name, command.ParameterValueFrom, true) == 0)
                        {
                            dic.Add(p.Name, HttpUtility.HtmlEncode(n.OuterXml));
                        }
                        else if (String.Compare(p.Name, CommonUtility.MDTDATAROOTNAME, true) == 0)
                        {
                            dic.Add(p.Name, HttpUtility.HtmlEncode(command.ParameterValue));
                        }
                        else
                        {
                            foreach (XmlNode cn in n.ChildNodes)
                            {
                                if (String.Compare(p.Name, cn.Name, true) == 0)
                                {
                                    dic.Add(p.Name, HttpUtility.HtmlEncode(cn.InnerText));
                                    break;
                                }
                            }
                        }
                    }
                }
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

            return result;
        }

        /// <summary>
        /// 执行动态库 code by wk 2013-03-14
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private string executeDLL(ECommand command)
        {
            Dictionary<string, string> paramList = new Dictionary<string, string>();
            foreach (EParameter param in command.Parameters)
            {
                paramList.Add(param.Name, param.Value);
            }
            return new Mic.MopJob.BLL.TaskFactory().Execute(command.CommandText, command.CommandName, paramList);
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
                        if (paramNode == null)
                            param.Value = DBNull.Value;
                        else
                            param.Value = paramNode.InnerText;
                    }
                    //// 设置差数值（null）
                    //foreach (DbParameter param in dbComm.Parameters)
                    //{
                    //    param.Value = DBNull.Value;
                    //}

                    //// 设置差数值
                    //foreach (XmlNode cn in node.ChildNodes)
                    //{
                    //    if (dbComm.Parameters.Contains(cn.Name))
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

        /// <summary>
        /// 执行操作返回数据集
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbComm"></param>
        /// <param name="dbTrans"></param>
        /// <returns></returns>
        private DataSet executeDataSet(Database db, DbCommand dbComm, DbTransaction dbTrans)
        {
            if (dbTrans == null)
                return db.ExecuteDataSet(dbComm);
            else
                return db.ExecuteDataSet(dbComm, dbTrans);
        }

        /// <summary>
        /// 执行操作无返回结果
        /// </summary>
        /// <param name="db"></param>
        /// <param name="dbComm"></param>
        /// <param name="dbTrans"></param>
        private void executeNonQuery(Database db, DbCommand dbComm, DbTransaction dbTrans)
        {
            if (dbTrans == null)
                db.ExecuteNonQuery(dbComm);
            else
                db.ExecuteNonQuery(dbComm, dbTrans);
        }

        /// <summary>
        /// 获取操作命令键值
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private string getCommandKey(ECommand command)
        {
            return String.Format("{0}-{1}", _task.ID, command.CommandText);
        }

        private string mergeResult(Dictionary<string, string> results, Result[] coms, out bool hasdata, out int count)
        {
            count = 0;
            hasdata = false;

            if (results.Count == 0 || coms.Length == 0)
            {
                return String.Empty;
            }
            else
            {
                int sum = 0;
                StringBuilder sb = new StringBuilder();
                sb.Append(String.Format("<{0}>", CommonUtility.MDTDATAROOTNAME));
                foreach (var r in coms)
                {
                    if (!results.Keys.Contains(r.CommandName))
                    {
                        continue;
                    }
                    string xml = results[r.CommandName];
                    if (!String.IsNullOrEmpty(xml))
                    {
                        // Modify by wk 2011-02-26
                        if (!String.IsNullOrEmpty(r.ValueFromField))
                            xml = mergeResultByField(xml, r);

                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(xml);
                        XmlNodeList ns = doc.SelectNodes(r.XmlPath);
                        foreach (XmlNode n in ns)
                        {
                            sb.Append(n.OuterXml);
                            hasdata = true;
                        }

                        if (sum == 0)
                            count = ns.Count;
                    }
                    sum++;
                }
                sb.Append(String.Format("</{0}>", CommonUtility.MDTDATAROOTNAME));
                return sb.ToString();
            }
        }

        private string mergeResultByField(string xml, Result r)
        {
            StringBuilder sb = new StringBuilder();
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);
            XmlNodeList nodelist = doc.SelectNodes(String.Format("//{0}", r.ValueFromField));
            if (nodelist == null || nodelist.Count == 0)
            {
                throw new Exception(String.Format("根据关键字段获取数据失败！{0}", r.ValueFromField));
            }
            else
            {
                sb.Append(String.Format("<{0}>", CommonUtility.MDTDATAROOTNAME));
                foreach (XmlNode node in nodelist)
                {
                    string id = String.Empty;
                    doc.LoadXml(node.InnerText);

                    // 查找主表节点
                    XmlNodeList foot = doc.SelectNodes(r.XmlPath);
                    foreach (XmlNode pareNode in foot)
                    {
                        XmlNode pkNode = pareNode.SelectSingleNode(r.PrimaryKey);
                        if (pkNode == null)
                        {
                            // 生成ID（GUID方式）
                            id = Guid.NewGuid().ToString();
                            XmlElement xe = doc.CreateElement(r.PrimaryKey);
                            xe.InnerText = id;
                            pareNode.AppendChild(xe);
                        }
                        else
                        {
                            id = pkNode.InnerText;
                        }

                        foreach (string subPath in r.SubXmlPath.Split(new char[] { ';' }))
                        {
                            // 查找子表节点
                            XmlNodeList childNodeList = doc.SelectNodes(subPath);
                            foreach (XmlNode childNode in childNodeList)
                            {
                                XmlElement xe = doc.CreateElement(r.PrimaryKey);
                                xe.InnerText = id;
                                childNode.AppendChild(xe);
                            }
                        }
                    }
                    //  添加数据结果集
                    sb.Append(doc.OuterXml);
                }
                sb.Append(String.Format("</{0}>", CommonUtility.MDTDATAROOTNAME));
            }
            return sb.ToString();
        }
    }
}


