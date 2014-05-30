using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using MDT.ManageCenter.DataContract;
using MDT.ManageCenter.ServiceContract;
using MDT.ManageCenter.DAL;
using MDT.Utility;

namespace MDT.ManageCenter.ServiceImplement
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.PerCall)]
    public class ManageCenterService : IManageCenterService
    {
        // 数据转换服务使用 modify by wangk 2012-12-07
        //public string GetXSLT(int taskId)
        //{
        //    ETaskDAL d = new ETaskDAL();
        //    return d.GetTask(taskId).XSLTInfo;
        //}
        
        /// <summary>
        /// 构造数据库连接字符串
        /// </summary>
        /// <param name="edb"></param>
        /// <returns></returns>
        public string BuildConnString(EDatabase edb)
        {
            return DALUtility.BuildConnString((MySourceType)Enum.Parse(typeof(MySourceType)
                                           , edb.DatabaseType)
                                           , edb.Server, edb.Port
                                           , edb.Database
                                           , edb.UserId
                                           , edb.Password);
        }

        /// <summary>
        /// 根据客户端ID获取配置任务信息
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        public List<TaskInfo> GetTasks(int clientId)
        {
            List<TaskInfo> taskList = new List<TaskInfo>();
            ETaskDAL taskDAL = new ETaskDAL();
            var p = taskDAL.GetTasks().Where(c => c.ETaskAllocation.EClient_ID == clientId);

            foreach (var t in p)
            {
                taskList.Add(new TaskInfo()
                {
                    ID = t.ID,
                    TaskName = t.TaskName,
                    SourceXSD_ID = t.SourceESchema_ID,
                    TargetXSD_ID = t.TargetESchema_ID,
                    Interval = t.Interval
                });//TODO:Mapping = t.Mapping,, XSLTInfo = t.XSLTInfo
            }

            return taskList;
        }

        /// <summary>
        /// 获取全部配置任务信息
        /// </summary>
        /// <returns></returns>
        public List<TaskInfo> GetTasks()
        {
            List<TaskInfo> taskList = new List<TaskInfo>();
            ETaskDAL taskDAL = new ETaskDAL();
            var p = taskDAL.GetTasks();

            foreach (var t in p)
            {
                taskList.Add(new TaskInfo()
                {
                    ID = t.ID,
                    TaskName = t.TaskName,
                    SourceXSD_ID = t.SourceESchema_ID,
                    TargetXSD_ID = t.TargetESchema_ID,
                    Interval = t.Interval,
                    Enable = t.Enable
                });//TODO:, XSLTInfo = t.XSLTInfo， Mapping = t.Mapping, 
            }
            return taskList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="taskIds"></param>
        public void AllocateTask(int clientId, int[] taskIds)
        {
            ETaskAllocationDAL dal = new ETaskAllocationDAL();
            dal.DeleteObject(clientId);

            ETaskAllocation[] ts = new ETaskAllocation[taskIds.Length];
            for (int i = 0; i < taskIds.Length; i++)
            {
                ts[i] = new ETaskAllocation() { EClient_ID = clientId, ETask_ID = taskIds[i] };
            }
            dal.AddObject(ts);
        }

        /// <summary>
        /// 添加配置任务信息
        /// </summary>
        /// <param name="task"></param>
        public void AddTask(ETask task)
        {
            EDatabaseDAL dal = new EDatabaseDAL();
            var sSource = task.SourceESchema.ESource;
            var tSource = task.TargetESchema.ESource;

            task.XSLTInfo = CommonUtility.GetXSLTInfo(task.Mapping);

            // 设置源配置文件
            var db = dal.GetDatabase(sSource.ESourceBaseInfo_ID);
            string connString = BuildConnString(db);
            var table = CommonUtility.UnserializeXml<Table>(sSource.OriginalConfig);
            var type = (SourceType)Enum.Parse(typeof(SourceType), sSource.SourceType);
            //szq modify at 20110920
            if (type != SourceType.Http && type != SourceType.Tcp)
                connString = Utility.SecurityHelper.Encode(connString);

            task.SourceESchema.ESource.SourceConfig = CommonUtility.SerializeXml<Source>(buildSSource(table, type, connString));

            // 设置目标配置文件
            db = dal.GetDatabase(tSource.ESourceBaseInfo_ID);
            connString = BuildConnString(db);
            table = CommonUtility.UnserializeXml<Table>(tSource.OriginalConfig);
            type = (SourceType)Enum.Parse(typeof(SourceType), tSource.SourceType);
            //szq modify at 20110920
            if (type != SourceType.Http && type != SourceType.Tcp)
                connString = Utility.SecurityHelper.Encode(connString);

            task.TargetESchema.ESource.SourceConfig = CommonUtility.SerializeXml<Source>(buildTSource(table, type, connString));

            ETaskDAL taskDAL = new ETaskDAL();
            if (task.OperationDate == DateTime.MinValue)
                task.OperationDate = DateTime.Now;
            taskDAL.AddObject(task);
        }

        /// <summary>
        /// 添加配置任务信息
        /// </summary>
        /// <param name="taskName"></param>
        /// <param name="note"></param>
        public void AddTask(string taskName, string note)
        {
            Source ss = new Source()
            {
                MainTasks = new List<TaskUnit>(),
                PostTasks = new List<TaskUnit>(),
                Results = new List<Result>()
            };

            ETask task = new ETask()
            {
                TaskName = taskName,
                Note = note,
                Enable = false,
                //Created = DateTime.Now,
                //Modified = DateTime.Now,
                OperationDate = DateTime.Now,
                SourceESchema = new ESchema()
                {
                    //Customer_ID = 1,
                    ESource = new ESource()
                    {
                        SourceConfig = CommonUtility.SerializeXml<Source>(ss)
                    }
                },
                TargetESchema = new ESchema()
                {
                    //Customer_ID = 1,
                    Schema = String.Empty,
                    ESource = new ESource()
                    {
                        SourceConfig = CommonUtility.SerializeXml<Source>(ss)
                    }
                }
            };

            ETaskDAL taskDAL = new ETaskDAL();
            taskDAL.AddObject(task);
        }

        /// <summary>
        /// 构造源配置文件
        /// </summary>
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <param name="sourceLink"></param>
        /// <returns></returns>
        private Source buildSSource(Table t, SourceType type, string sourceLink)
        {
            Source source = new Source
            {
                MainTasks = new List<TaskUnit>(),
                PostTasks = new List<TaskUnit>(),
                Results = new List<Result>()
            };

            // 添加主任务
            TaskUnit tm = new TaskUnit
            {
                Commands = buildSelECommand(type, t, sourceLink, 10),
                HasTraceLog = true,
                HasTransaction = false
            };
            source.MainTasks.Add(tm);

            //s.PostTasks = new List<TaskUnit>();
            //var dp = new TaskUnit();
            //var det = new ETransaction();
            //dp.Transactions = new List<ETransaction>();
            //dp.Transactions.Add(det);
            //det.Commands = GetDelECommand(type, t, sourceLink);
            //det.Enable = true;
            //s.PostTasks.Add(dp);

            // 添加事后任务
            TaskUnit tp = new TaskUnit
            {
                Commands = buildDelECommand(type, t, sourceLink),
                HasTransaction = false
            };
            source.PostTasks.Add(tp);

            // 添加任务结果
            source.Results.Add(new Result()
            {
                CommandName = t.TableName,
                XmlPath = "//" + t.TableName
            });

            if (t.RelatedTables != null)
            {
                foreach (RelatedTable rt in t.RelatedTables)
                {
                    source.Results.Add(new Result()
                    {
                        CommandName = rt.TableName,
                        XmlPath = "//" + rt.TableName
                    });
                }
            }
            return source;
        }
        
        /// <summary>
        /// 构造目标配置文件
        /// </summary>
        /// <param name="t"></param>
        /// <param name="type"></param>
        /// <param name="sourceLink"></param>
        /// <returns></returns>
        private Source buildTSource(Table t, SourceType type, string sourceLink)
        {
            Source source = new Source
            {
                MainTasks = new List<TaskUnit>(),
                PostTasks = new List<TaskUnit>()
            };

            var task = new TaskUnit
            {
                Commands = buildInsECommand(type, t, sourceLink),
                HasTraceLog = true,
                HasTransaction = false
            };

            if (!String.IsNullOrEmpty(t.PostStoredProcedure))
            {
                task.Commands.Add(new ECommand()
                {
                    CommandName = t.PostStoredProcedure,
                    CommandText = t.PostStoredProcedure,
                    CommandType = ECommandType.StoredProcedure,
                    HasResult = false,
                    SourceLink = sourceLink,
                    SourceType = type
                });
            }

            source.MainTasks.Add(task);
            return source;
        }

        /// <summary>
        /// 构造查询命令集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        private List<ECommand> buildSelECommand(SourceType type, Table config, string sourceLink, int batchSize)
        {
            ECommand relECommand;
            ECommand command = new ECommand
            {
                SourceType = type,
                CommandName = config.TableName,
                SourceLink = sourceLink,
                CommandText = buildSelCommandText(type, config, batchSize),
                HasResult = true
            };
            List<ECommand> commandList = new List<ECommand>();
            commandList.Add(command);

            foreach (RelatedTable relConfig in config.RelatedTables)
            {
                relECommand = new ECommand();
                relECommand.SourceType = type;
                relECommand.SourceLink = sourceLink;
                relECommand.CommandName = relConfig.TableName;
                relECommand.CommandText = buildSelCommandText(type, relConfig);

                relECommand.ParameterValueFrom = command.CommandName;
                relECommand.ParameterValueOjbectName = "//" + config.TableName;
                relECommand.HasResult = true;
                relECommand.Parameters = new List<EParameter>();
                relECommand.Parameters.Add(buildEParameter(relConfig.ForeignColumn));
                commandList.Add(relECommand);
            }

            return commandList;
        }

        /// <summary>
        /// 构造删除命令集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private List<ECommand> buildDelECommand(SourceType type, Table config, string sourceLink)
        {
            ECommand relECommand;
            List<ECommand> commandList = new List<ECommand>();
            ECommand command = new ECommand
            {
                SourceType = type,
                SourceLink = sourceLink,
                CommandName = String.Format("{0}_del", config.TableName),
                CommandText = buildDelCommandText(type, config.TableName, config.PKColumn),
                ParameterValueFrom = config.TableName,
                ParameterValueOjbectName = "//" + config.TableName,
                Parameters = new List<EParameter>(),
                HasResult = false
            };
            command.Parameters.Add(buildEParameter(config.PKColumn));
            commandList.Add(command);

            foreach (RelatedTable relConfig in config.RelatedTables)
            {
                relECommand = new ECommand();
                relECommand.SourceType = type;
                relECommand.SourceLink = sourceLink;
                relECommand.CommandName = String.Format("{0}_del", relConfig.TableName);
                relECommand.CommandText = buildDelCommandText(type, relConfig.TableName, relConfig.ForeignColumn);
                relECommand.ParameterValueFrom = relConfig.TableName;
                relECommand.ParameterValueOjbectName = "//" + relConfig.TableName;
                relECommand.HasResult = false;
                relECommand.Parameters = new List<EParameter>();
                relECommand.Parameters.Add(buildEParameter(relConfig.ForeignColumn));
                commandList.Add(relECommand);
            }

            return commandList;
        }

        /// <summary>
        /// 构造插入命令集合
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        private List<ECommand> buildInsECommand(SourceType type, Table config, string sourceLink)
        {
            ECommand relECommand;
            ECommand command = new ECommand
            {
                SourceType = type,
                SourceLink = sourceLink,
                CommandName = config.TableName,
                CommandText = buildInsCommandText(type, config.TableName, config.Columns),
                ParameterValueFrom = CommonUtility.MDTDATAROOTNAME,
                ParameterValueOjbectName = "//" + config.TableName,
                Parameters = new List<EParameter>(),
                HasResult = false
            };
            command.Parameters.AddRange(buildEParameters(config.Columns));
            List<ECommand> commandList = new List<ECommand>();
            commandList.Add(command);

            foreach (RelatedTable relConfig in config.RelatedTables)
            {
                relECommand = new ECommand();
                relECommand.SourceType = type;
                relECommand.SourceLink = sourceLink;
                relECommand.CommandName = relConfig.TableName;
                relECommand.CommandText = buildInsCommandText(type, relConfig.TableName, relConfig.Columns);
                relECommand.Parameters = new List<EParameter>();
                relECommand.Parameters.AddRange(buildEParameters(relConfig.Columns));
                relECommand.ParameterValueFrom = CommonUtility.MDTDATAROOTNAME;
                relECommand.ParameterValueOjbectName = "//" + relConfig.TableName;
                command.HasResult = false;
                commandList.Add(relECommand);
            }

            return commandList;
        }

        /// <summary>
        /// 构建获取SQL语句
        /// </summary>
        /// <param name="type"></param>
        /// <param name="config"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        private string buildSelCommandText(SourceType type, Table config, int batchSize)
        {
            StringBuilder sb = new StringBuilder();

            switch (type)
            {
                case SourceType.Oracle:
                    sb.Append("select {0}");
                    sb.Append(joinColumns(config.Columns, String.Empty));
                    sb.Append(" from ");
                    sb.Append(config.TableName);
                    sb.Append(" where rownum <= ");
                    sb.Append(batchSize);
                    if (!String.IsNullOrEmpty(config.AdditionalWhere))
                        sb.Append(" and ").Append(config.AdditionalWhere);
                    break;
                case SourceType.MySql:
                    sb.Append("select {0}");
                    sb.Append(joinColumns(config.Columns, String.Empty));
                    sb.Append(" from ");
                    sb.Append(config.TableName);
                    sb.Append(" limit ");
                    sb.Append(batchSize);
                    if (!String.IsNullOrEmpty(config.AdditionalWhere))
                        sb.Append(" where ").Append(config.AdditionalWhere);
                    break;
                case SourceType.SqlServer:
                    sb.Append("select top ");
                    sb.Append(batchSize);
                    sb.Append(" {0}");
                    sb.Append(joinColumns(config.Columns, String.Empty));
                    sb.Append(" from ");
                    sb.Append(config.TableName);
                    if (!String.IsNullOrEmpty(config.AdditionalWhere))
                        sb.Append(" where ").Append(config.AdditionalWhere);
                    break;
            }

            if (isHavPrimaryKey(config))
                return String.Format(sb.ToString(), config.PKColumn.Name + ",");
            else
                return String.Format(sb.ToString(), String.Empty);
        }

        /// <summary>
        /// 构建获取SQL语句
        /// </summary>
        /// <param name="type"></param>
        /// <param name="relConfig"></param>
        /// <returns></returns>
        private string buildSelCommandText(SourceType type, RelatedTable relConfig)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ");
            sb.Append(joinColumns(relConfig.Columns, String.Empty));
            sb.Append(" from ");
            sb.Append(relConfig.TableName);
            sb.Append(" where ");
            sb.Append(relConfig.ForeignColumn.Name);
            sb.Append("=");
            sb.Append(CommonUtility.GetEParameterPrefix(type.ToString()));
            sb.Append(relConfig.ForeignColumn.Name);

            // 添加附加条件
            if (!String.IsNullOrEmpty(relConfig.AdditionalWhere))
            {
                sb.Append(" and ").Append(relConfig.AdditionalWhere);
            }
            return sb.ToString();
        }

        /// <summary>
        /// 是否存在主键列
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        private bool isHavPrimaryKey(Table config)
        {
            foreach (EColumn column in config.Columns)
            {
                if (column.Name == config.PKColumn.Name)
                {
                    return false;
                }
            }
            return true;
        }

        private string buildDelCommandText(SourceType type, string tableName, EColumn column)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("delete from ");
            sb.Append(tableName);
            sb.Append(" where ");
            sb.Append(column.Name);
            sb.Append("=");
            sb.Append(CommonUtility.GetEParameterPrefix(type.ToString()));
            sb.Append(column.Name);
            return sb.ToString();
        }

        private string buildInsCommandText(SourceType type, string tableName, List<EColumn> columns)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("insert into ");
            sb.Append(tableName);
            sb.Append(" (");
            sb.Append(joinColumns(columns, String.Empty));
            sb.Append(") values(");
            sb.Append(joinColumns(columns, CommonUtility.GetEParameterPrefix(type.ToString())));
            sb.Append(")");
            return sb.ToString();
        }

        private List<EParameter> buildEParameters(List<EColumn> columns)
        {
            List<EParameter> dbParams = new List<EParameter>();
            foreach (EColumn column in columns)
            {
                dbParams.Add(buildEParameter(column));
            }
            return dbParams;
        }

        private EParameter buildEParameter(EColumn column)
        {
            return new EParameter { Name = column.Name, Type = column.Type };
        }

        private string joinColumns(List<EColumn> columns, string prefix)
        {
            string columnStr = String.Empty;
            foreach (EColumn column in columns)
            {
                columnStr += String.Format("{0}{1},", prefix, column.Name);
            }
            return columnStr.TrimEnd(new char[] { ',' });
        }
    }
}
