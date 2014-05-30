using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Data.Common;
using System.Data.EntityClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using MDT.ManageCenter.DataContract;

namespace MDT.ManageCenter.DAL
{
    public class TraceLogDAL
    {
        private ManageCenterDBEntities _db;

        public TraceLogDAL()
        {
            _db = new ManageCenterDBEntities();
        }

        public void CreateTable()
        {

        }

        /// <summary>
        /// 记录数据生产日志
        /// </summary>
        public void Write(TraceLogInfo tracelog)
        {
            DbConnection dbConn = null;
            DbTransaction dbTrans = null;

            EntityConnectionStringBuilder entityConn = new EntityConnectionStringBuilder();
            entityConn.ConnectionString = ConfigurationManager.ConnectionStrings["ManageCenterDBEntities"].ToString();
            Database db = new GenericDatabase(entityConn.ProviderConnectionString
                                              , DbProviderFactories.GetFactory(entityConn.Provider));

            try
            {
                dbConn = db.CreateConnection();
                dbConn.Open();
                dbTrans = dbConn.BeginTransaction();
                // 记录主日志
                if (tracelog.Stage == TraceStage.DataProducer)
                {
                    if (!existsTraceLogMaster(tracelog, db, dbTrans))
                    {
                        insertTraceLogMaster(tracelog, db, dbTrans);
                        if (tracelog.Status == TraceStatus.Handle)
                            tracelog.Status = TraceStatus.Success;
                    }
                }
                else
                {
                    updateTraceLogMaster(tracelog, db, dbTrans);
                }
                // 记录明细日志
                insertTraceLog(tracelog, db, dbTrans);

                dbTrans.Commit();
            }
            catch (Exception ex)
            {
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

        /// <summary>
        /// 主日志是否已经记录
        /// </summary>
        /// <param name="tracelog"></param>
        /// <param name="db"></param>
        /// <param name="dbTrans"></param>
        /// <returns></returns>
        private bool existsTraceLogMaster(TraceLogInfo tracelog, Database db, DbTransaction dbTrans)
        {
            DbCommand dbComm = db.CreateConnection().CreateCommand();
            dbComm.CommandType = CommandType.Text;
            dbComm.CommandText = "select id from tracelogmaster where etask_id=?etask_id and id=?id";

            // 处理批次
            DbParameter param = dbComm.CreateParameter();
            param.ParameterName = "?id";
            param.DbType = DbType.AnsiString;
            param.Value = tracelog.ID;
            dbComm.Parameters.Add(param);

            // 任务ID
            param = dbComm.CreateParameter();
            param.ParameterName = "?etask_id";
            param.DbType = DbType.Int32;
            param.Value = tracelog.TaskId;
            dbComm.Parameters.Add(param);

            DataSet ds = db.ExecuteDataSet(dbComm, dbTrans);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// 记录主日志
        /// </summary>
        /// <param name="dbComm"></param>
        /// <param name="tracelog"></param>
        private void insertTraceLogMaster(TraceLogInfo tracelog, Database db, DbTransaction dbTrans)
        {
            DbCommand dbComm = db.CreateConnection().CreateCommand();
            dbComm.CommandType = CommandType.Text;
            dbComm.CommandText = "insert into tracelogmaster (id, etask_id, status, datacount, starttime, endtime) values(?id, ?etask_id, ?status, ?datacount, ?starttime, ?endtime)";

            // 处理批次
            DbParameter param = dbComm.CreateParameter();
            param.ParameterName = "?id";
            param.DbType = DbType.AnsiString;
            param.Value = tracelog.ID;
            dbComm.Parameters.Add(param);

            // 任务ID
            param = dbComm.CreateParameter();
            param.ParameterName = "?etask_id";
            param.DbType = DbType.Int32;
            param.Value = tracelog.TaskId;
            dbComm.Parameters.Add(param);

            // 执行状态
            param = dbComm.CreateParameter();
            param.ParameterName = "?status";
            param.DbType = DbType.AnsiString;
            param.Value = Enum.GetName(typeof(TraceStatus), tracelog.Status);
            dbComm.Parameters.Add(param);

            // 总数量
            param = dbComm.CreateParameter();
            param.ParameterName = "?datacount";
            param.DbType = DbType.Int32;
            param.Value = tracelog.DataCount;
            dbComm.Parameters.Add(param);

            // 执行开始时间
            param = dbComm.CreateParameter();
            param.ParameterName = "?starttime";
            param.DbType = DbType.DateTime;
            param.Value = tracelog.StartTime;
            dbComm.Parameters.Add(param);

            // 执行结束时间
            param = dbComm.CreateParameter();
            param.ParameterName = "?endtime";
            param.DbType = DbType.DateTime;
            param.Value = tracelog.EndTime;
            dbComm.Parameters.Add(param);

            db.ExecuteNonQuery(dbComm, dbTrans);
        }

        /// <summary>
        /// 修改主日志
        /// </summary>
        /// <param name="tracelog"></param>
        private void updateTraceLogMaster(TraceLogInfo tracelog, Database db, DbTransaction dbTrans)
        {
            DbCommand dbComm = db.CreateConnection().CreateCommand();
            dbComm.CommandType = CommandType.Text;
            //dbComm.CommandText = "update tracelogmaster set status=?status, endtime = ?endtime where id = ?id";
            // Modify by wangk 2013-02-25
            dbComm.CommandText = "update tracelogmaster set status=?status, endtime = ?endtime where id = ?id and status in('Success','Handle')";

            // 处理批次
            DbParameter param = dbComm.CreateParameter();
            param.ParameterName = "?id";
            param.DbType = DbType.AnsiString;
            param.Value = tracelog.ID;
            dbComm.Parameters.Add(param);

            // 执行状态
            param = dbComm.CreateParameter();
            param.ParameterName = "?status";
            param.DbType = DbType.AnsiString;
            param.Value = Enum.GetName(typeof(TraceStatus), tracelog.Status);
            dbComm.Parameters.Add(param);

            // 执行结束时间
            param = dbComm.CreateParameter();
            param.ParameterName = "?endtime";
            param.DbType = DbType.DateTime;
            param.Value = tracelog.EndTime;
            dbComm.Parameters.Add(param);

            db.ExecuteNonQuery(dbComm, dbTrans);
        }

        /// <summary>
        /// 记录明细日志
        /// </summary>
        /// <param name="dbComm"></param>
        /// <param name="tracelog"></param>
        private void insertTraceLog(TraceLogInfo tracelog, Database db, DbTransaction dbTrans)
        {
            DbCommand dbComm = db.CreateConnection().CreateCommand();
            dbComm.CommandType = CommandType.Text;
            dbComm.CommandText = "insert into tracelog (etask_id, tracelogmaster_id, stage, status, data, runinfo, starttime, endtime) values(?etask_id, ?tracelogmaster_id, ?stage, ?status, ?data, ?runinfo, ?starttime, ?endtime)";

            // 处理批次
            DbParameter param = dbComm.CreateParameter();
            param.ParameterName = "?tracelogmaster_id";
            param.DbType = DbType.AnsiString;
            param.Value = tracelog.ID;
            dbComm.Parameters.Add(param);

            // 任务ID
            param = dbComm.CreateParameter();
            param.ParameterName = "?etask_id";
            param.DbType = DbType.Int32;
            param.Value = tracelog.TaskId;
            dbComm.Parameters.Add(param);

            // 执行状态
            param = dbComm.CreateParameter();
            param.ParameterName = "?status";
            param.DbType = DbType.AnsiString;
            param.Value = Enum.GetName(typeof(TraceStatus), tracelog.Status);
            dbComm.Parameters.Add(param);

            // 处理阶段
            param = dbComm.CreateParameter();
            param.ParameterName = "?stage";
            param.DbType = DbType.AnsiString;
            param.Value = Enum.GetName(typeof(TraceStage), tracelog.Stage);
            dbComm.Parameters.Add(param);

            // 数据信息
            param = dbComm.CreateParameter();
            param.ParameterName = "?data";
            param.DbType = DbType.AnsiString;
            param.Value = tracelog.Data;
            dbComm.Parameters.Add(param);

            // 运行信息
            param = dbComm.CreateParameter();
            param.ParameterName = "?runinfo";
            param.DbType = DbType.AnsiString;
            param.Value = tracelog.RunInfo;
            dbComm.Parameters.Add(param);

            // 执行开始时间
            param = dbComm.CreateParameter();
            param.ParameterName = "?starttime";
            param.DbType = DbType.DateTime;
            param.Value = tracelog.StartTime;
            dbComm.Parameters.Add(param);

            // 执行结束时间
            param = dbComm.CreateParameter();
            param.ParameterName = "?endtime";
            param.DbType = DbType.DateTime;
            param.Value = tracelog.EndTime;
            dbComm.Parameters.Add(param);

            db.ExecuteNonQuery(dbComm, dbTrans);
        }

        /// <summary>
        /// 读取日志信息
        /// </summary>
        /// <returns></returns>
        public IQueryable<TraceLog> Read()
        {
            return _db.tracelog;
        }

        /// <summary>
        /// 读取日志信息
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        /// <param name="state"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public DataSet Read(int taskId, string fieldName, string value, string state, string startTime, string endTime)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT l.ID, l.ETask_ID, t.TaskName, l.DataCount, l.`Status`, l.StartTime, l.EndTime, l.Note, (@rowNum + 1) AS number");
            sb.Append(" FROM TraceLogMaster AS l LEFT JOIN ETask AS t ON l.ETask_ID = t.ID ");
            sb.AppendFormat(" WHERE EXISTS ( SELECT 1 FROM TraceLog r WHERE r.TraceLogMaster_ID = l.ID AND ExtractValue (r.`Data`, '//{0}') LIKE '{1}%' ) AND t.ID = {2} "
                            , fieldName, value, taskId);
            if (!String.IsNullOrEmpty(state))
            {
                sb.AppendFormat(" and l.State='{0}'", state);
            }
            if (!String.IsNullOrEmpty(startTime))
            {
                sb.AppendFormat(" and l.StartTime>='{0}'", startTime);
            }
            if (!String.IsNullOrEmpty(endTime))
            {
                sb.AppendFormat(" and l.EndTime<='{0}'", endTime);
            }
            sb.Append(" ORDER BY l.StartTime DESC ");
            string connString = ConfigurationManager.ConnectionStrings["MDT_ConString"].ToString();
            Database db = DatabaseFactory.CreateDatabase("MDT_ConString");
            DbCommand comm = db.GetSqlStringCommand(sb.ToString());
            //comm.Parameters["rowNum"].Value = 0;
            try
            {
                string commandTimeOut = ConfigurationManager.AppSettings["CommandTimeOut"];
                comm.CommandTimeout = Convert.ToInt32(commandTimeOut);//设置查询超时时间
                db.AddInParameter(comm, "rowNum", DbType.Int32, 0);
                return db.ExecuteDataSet(comm);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
